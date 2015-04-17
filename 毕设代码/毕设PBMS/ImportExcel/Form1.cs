using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace ImportExcel
{
    public partial class Form1 : Form
    {
        ImportExcel _importsevice;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //this.Text += "  Version:" + Application.ProductVersion;

            string title = MCSFramework.Common.ConfigHelper.GetConfigString("PageTitle");
            if (title != null)
            {
                this.Text += "  " + title;
            }

            _importsevice = new ImportExcel();
            _importsevice.OnMessage += new ImportExcel.MessageHandel(_service_OnMessage);
            bt_Begin_Click(null, null);
        }


        void _service_OnMessage(object Sender, ImportExcel.MessageEventArgs Args)
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
            tool_Start.Enabled = false;
            bt_Stop.Enabled = true;
            tool_End.Enabled = true;
            toolbtn_Start.Enabled = false;
            toolbtn_Stop.Enabled = true;
            timer1.Enabled = true;
        }

        private void bt_Stop_Click(object sender, EventArgs e)
        {
            _importsevice.Stop();
            bt_Begin.Enabled = true;
            tool_Start.Enabled = true;
            bt_Stop.Enabled = false;
            toolbtn_Start.Enabled = true;
            toolbtn_Stop.Enabled = false;
            tool_End.Enabled = false;
            timer1.Enabled = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_importsevice.IsRunning)
            {
                if (MessageBox.Show("进销服务正在运行中，是否确定退出？", "确认退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    notifyIcon1.Icon = this.Icon;
                    this.Visible = false;
                    this.ShowInTaskbar = false;//不显示在任务栏   
                    this.notifyIcon1.Visible = true;//显示托盘图标
                    notifyIcon1.ShowBalloonTip(1000);
                    
                }
                else
                {
                    _importsevice.Stop();
                    timer1.Enabled = false;
                    this.notifyIcon1.Dispose();
                    this.Dispose();
                    Process.GetCurrentProcess().Kill();
                    Application.ExitThread();
                    Application.Exit();
                    
                    System.Environment.Exit(0);   
                }
            }
        }

        private void bt_Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbox = new AboutBox1();
            aboutbox.ShowDialog();
        }

        private bool CheckRegistryIsStart()
        {
            try
            {
                string strAssName = Application.StartupPath + @"\" + Application.ProductName + @".exe";
                string ShortFileName = Application.ProductName;

                RegistryKey rgkRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (rgkRun == null)
                {
                    return false;
                }
                else
                {
                    if (rgkRun.GetValue(ShortFileName) != null)
                        return rgkRun.GetValue(ShortFileName).ToString() == strAssName;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool SetRegistryIsStart(bool IsStart)
        {
            if (IsStart)
            {
                try
                {
                    string strAssName = Application.StartupPath + @"\" + Application.ProductName + @".exe";
                    string ShortFileName = Application.ProductName;

                    RegistryKey rgkRun = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    if (rgkRun == null)
                    {
                        rgkRun = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                        rgkRun.SetValue(ShortFileName, strAssName);
                    }
                    else
                    {
                        rgkRun.SetValue(ShortFileName, strAssName);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    return false;
                }
            }
            else
            {
                try
                {
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue(Application.ProductName, false);
                    return false;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    return true;
                }
            }

        }
        private void toolbtn_StartRun_Click(object sender, EventArgs e)
        {
            toolbtnStartRun.Checked = TSMI_OpenPCStart.Checked = SetRegistryIsStart(!TSMI_OpenPCStart.Checked);
        }

      
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Icon = this.Icon;
                this.Visible = false;
                this.ShowInTaskbar = false;//不显示在任务栏       
                this.notifyIcon1.Visible = true;//显示托盘图标
                notifyIcon1.ShowBalloonTip(1000);
            }
          // this.Hide();

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }

        }

        private void Menu_Show_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolbtnStartRun.Checked = TSMI_OpenPCStart.Checked = CheckRegistryIsStart();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ImportExcel i = new ImportExcel();
            i.ImportSAPService();
        }
    }
}
