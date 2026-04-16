using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Concurrent;

namespace Pdf_Compressed
{
    public partial class Pdf_Compressed : Form
    {
        private string rootFolder = "";
        private bool isProcessing = false;

        // Ghostscript path
        private string gsPath = @"C:\Program Files\gs\gs10.06.0\bin\gswin64c.exe";

        // fast worker threads
        private readonly int workerCount = Math.Max(2, Environment.ProcessorCount * 2);

        // Retry for reliability
        private const int retryAttempts = 2;

        public Pdf_Compressed()
        {
            InitializeComponent();
        }

        // ================= SELECT FOLDER =================
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    rootFolder = dialog.SelectedPath;
                    txtFolder.Text = rootFolder;
                    Log("Selected Folder: " + rootFolder);
                }
            }
        }

        // ================= START =================
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rootFolder))
            {
                MessageBox.Show("Please select folder first.");
                return;
            }

            if (!File.Exists(gsPath))
            {
                MessageBox.Show("Ghostscript not found:\n" + gsPath);
                return;
            }

            if (isProcessing) return;

            isProcessing = true;
            btnStart.Enabled = false;
            progressBar1.Value = 0;
            lblPercent.Text = "0%";

            Log("Compression Started...\n");

            await Task.Run(ProcessPipeline);

            isProcessing = false;
            btnStart.Enabled = true;

            // Completion popup + auto close
            var result = MessageBox.Show("Compression Completed!", "Done",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        // ================= PIPELINE PROCESS =================
        private void ProcessPipeline()
        {
            string outputRoot = Path.Combine(rootFolder, "Compressed");

            var allFiles = Directory.EnumerateFiles(rootFolder, "*.pdf", SearchOption.AllDirectories)
                                    .Where(f => !f.StartsWith(outputRoot))
                                    .ToList();

            int total = allFiles.Count;
            int done = 0;

            // Group files folder-wise
            var groupedFolders = allFiles.GroupBy(f => Path.GetDirectoryName(f));

            foreach (var folderGroup in groupedFolders)
            {
                UpdateUI(null, $"Processing Folder: {GetRelativePath(rootFolder, folderGroup.Key)}");

                var queue = new BlockingCollection<string>();

                foreach (var file in folderGroup)
                    queue.Add(file);

                queue.CompleteAdding();

                Parallel.For(0, workerCount, i =>
                {
                    foreach (var file in queue.GetConsumingEnumerable())
                    {
                        bool success = false;

                        for (int attempt = 0; attempt < retryAttempts; attempt++)
                        {
                            try
                            {
                                CompressPdf(file, outputRoot);
                                success = true;
                                break;
                            }
                            catch
                            {
                                if (attempt == retryAttempts - 1)
                                    UpdateUI(null, $"❌ Failed: {Path.GetFileName(file)}");
                            }
                        }

                        if (success)
                        {
                            int finished = Interlocked.Increment(ref done);
                            int percent = (finished * 100) / total;
                            UpdateUI(percent, $"✔ {GetRelativePath(rootFolder, file)}");
                        }
                    }
                });
            }
        }

        // ================= COMPRESSION =================
        private void CompressPdf(string inputPath, string outputRoot)
        {
            string relative = GetRelativePath(rootFolder, inputPath);
            string outputPath = Path.Combine(outputRoot, relative);

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            string args =
                "-sDEVICE=pdfwrite " +
                "-dCompatibilityLevel=1.4 " +
                "-dNOPAUSE -dBATCH -dQUIET " +
                "-dPDFSETTINGS=/screen " +
                "-dDetectDuplicateImages=true " +
                "-dCompressFonts=true " +
                "-dSubsetFonts=true " +
                "-dDownsampleColorImages=true -dColorImageResolution=120 " +
                "-dDownsampleGrayImages=true -dGrayImageResolution=120 " +
                "-dDownsampleMonoImages=true -dMonoImageResolution=300 " +
                $"-sOutputFile=\"{outputPath}\" \"{inputPath}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = gsPath,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            using (Process proc = new Process())
            {
                proc.StartInfo = psi;

                proc.OutputDataReceived += (s, e) => { };
                proc.ErrorDataReceived += (s, e) => { };

                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                    throw new Exception("Ghostscript failed");
            }

            if (!File.Exists(outputPath))
                throw new Exception("Output not created");
        }

        // ================= SAFE UI UPDATE =================
        private void UpdateUI(int? percent, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => UpdateUI(percent, message)));
                return;
            }

            if (percent.HasValue)
            {
                progressBar1.Value = percent.Value;
                lblPercent.Text = percent + "%";
            }

            Log(message);
        }

        // ================= RELATIVE PATH =================
        private string GetRelativePath(string basePath, string fullPath)
        {
            Uri baseUri = new Uri(basePath.EndsWith("\\") ? basePath : basePath + "\\");
            Uri fullUri = new Uri(fullPath);

            return Uri.UnescapeDataString(
                baseUri.MakeRelativeUri(fullUri)
                       .ToString()
                       .Replace('/', '\\'));
        }

        // ================= LOG =================
        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke(new Action(() =>
                    txtLog.AppendText(message + Environment.NewLine)));
            }
            else
            {
                txtLog.AppendText(message + Environment.NewLine);
            }
        }
    }
}
