using System;
using System.Windows.Forms;

namespace Pdf_Compressed
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Pdf_Compressed());
        }
    }
}
