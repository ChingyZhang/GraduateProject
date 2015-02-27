using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImportExcel
{
    public partial class Form1 : Form
    {
        ImportExcelSVM _importsevice;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.Text += "  Version:" + Application.ProductVersion;

            string title = MCSFramework.Common.ConfigHelper.GetConfigString("PageTitle");
            if (title != null)
            {
                this.Text += "  " + title;
            }

            _importsevice = new ImportExcelSVM();
            _importsevice.OnMessage += new ImportExcelSVM.MessageHandel(_service_OnMessage);
            bt_Begin_Click(null, null);
        }


        void _service_OnMessage(object Sender, ImportExcelSVM.MessageEventArgs Args)
        {
            if (tbx_Message.Text.Length > 30000) tbx_Message.Text = "";

            tbx_Message.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (Args.Task != "") tbx_Message.AppendText(" 当前操作:" + Args.Task.ToString());
            tbx_Message.AppendText("\r\n");
            tbx_Message.AppendText("消息内容:" + Args.Message + "\r\n");
            tbx_Message.AppendText("\r\n");
        }

        private void bt_Begin_Click(object sender, EventArgs e)
        {
            _importsevice.Start();
            bt_Begin.Enabled = false;
            bt_Stop.Enabled = true;
            timer1.Enabled = true;
        }

        private void bt_Stop_Click(object sender, EventArgs e)
        {
            _importsevice.Stop();
            bt_Begin.Enabled = true;
            bt_Stop.Enabled = false;
            timer1.Enabled = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_importsevice.IsRunning)
            {
                if (MessageBox.Show("进销服务正在运行中，是否确定退出？", "确认退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    e.Cancel = true;
                else
                {
                    _importsevice.Stop();
                    timer1.Enabled = false;
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
