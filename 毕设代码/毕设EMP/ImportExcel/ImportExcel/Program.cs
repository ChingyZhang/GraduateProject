using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImportExcel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int ProceedingCount = 0;
            Process[] ProceddingCon = Process.GetProcesses();
            foreach (Process IsProcedding in ProceddingCon)
            {
                if (IsProcedding.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    ProceedingCount++;
                }
            }
            if (ProceedingCount > 1)
            {
                MessageBox.Show("该系统已经在运行中。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Application.Run(new Form1());
            }
           
        }
    }
}
