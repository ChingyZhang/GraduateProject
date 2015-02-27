using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImportTools_KPI
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
            try
            {
                Application.Run(new main());
            }
            catch (Exception e)
            {
                MessageBox.Show("导入成功!");
                Application.ExitThread();
                Application.Exit();
            }
        }
    }
}
