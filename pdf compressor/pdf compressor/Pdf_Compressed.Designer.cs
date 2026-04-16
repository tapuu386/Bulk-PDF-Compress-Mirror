namespace Pdf_Compressed
{
    partial class Pdf_Compressed
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.RichTextBox txtLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            // ===== HEADER =====
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 45;
            this.lblTitle.Text = "PDF Compressor Dashboard";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblTitle.ForeColor = System.Drawing.Color.White;

            // ===== FOLDER TEXTBOX =====
            this.txtFolder.Location = new System.Drawing.Point(20, 70);
            this.txtFolder.Width = 420;
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Font = new System.Drawing.Font("Segoe UI", 9F);

            // ===== SELECT BUTTON =====
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.Location = new System.Drawing.Point(450, 68);
            this.btnSelectFolder.Size = new System.Drawing.Size(120, 30);
            this.btnSelectFolder.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnSelectFolder.ForeColor = System.Drawing.Color.White;
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);

            // ===== START BUTTON =====
            this.btnStart.Text = "Start Compression";
            this.btnStart.Location = new System.Drawing.Point(20, 110);
            this.btnStart.Size = new System.Drawing.Size(170, 40);
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // ===== PROGRESS LABEL =====
            this.lblProgress.Text = "Progress";
            this.lblProgress.Location = new System.Drawing.Point(20, 165);
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // ===== PROGRESS BAR =====
            this.progressBar1.Location = new System.Drawing.Point(20, 190);
            this.progressBar1.Size = new System.Drawing.Size(550, 25);

            // ===== PERCENT LABEL =====
            this.lblPercent.Text = "0%";
            this.lblPercent.Location = new System.Drawing.Point(580, 190);
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // ===== LOG BOX =====
            this.txtLog.Location = new System.Drawing.Point(20, 230);
            this.txtLog.Size = new System.Drawing.Size(650, 220);
            this.txtLog.ReadOnly = true;
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(0, 255, 156);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 10F);

            // ===== FORM SETTINGS =====
            this.ClientSize = new System.Drawing.Size(700, 470);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.txtLog);

            this.Text = "PDF Compressor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
