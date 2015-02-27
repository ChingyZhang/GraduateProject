using System;
using System.Windows.Forms;

namespace SyncERPJXC
{
    public partial class Form1 : Form
    {
        SyncDeliverySheet _Sync = new SyncDeliverySheet();
        private DateTime LastSyncTime = DateTime.Today.AddMonths(-1);

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            dtp_BeginDate.Value = DateTime.Today.AddDays(-7);
            this.Text += "  Version:" + Application.ProductVersion;
            _Sync.LogFilePath = Environment.CurrentDirectory + "\\Log";
            _Sync.OnMessage += new MessageHandel(_service_OnMessage);
            _Sync.OnSyncComplete += new SyncCompleteHandel(_Sync_OnSyncComplete);

        }

        void _Sync_OnSyncComplete(object Sender, EventArgs Args)
        {
            btn_run.Enabled = true;
            bt_CancelSync.Enabled = false;
        }

        //设置输出格式
        void _service_OnMessage(object Sender, MessageEventArgs Args)
        {
            if (tbx_Message.Text.Length > 30000) tbx_Message.Text = "";
            tbx_Message.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string str = "";
            if (Args.MessageCode != 0) str += " 错误号:" + Args.MessageCode.ToString() + "\r\n";
            str += "消息内容:" + Args.Message + "\r\n";
            str += "\r\n";
            tbx_Message.AppendText(str);
        }


        /// <summary>
        /// 手工点击开始同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_run_Click(object sender, EventArgs e)
        {
            if (_Sync.IsSyncing)
            {
                System.Windows.Forms.MessageBox.Show("当前正在同步中，不可再发起同步!");
                return;
            }

            btn_run.Enabled = false;
            bt_CancelSync.Enabled = true;

            DateTime begindate = dtp_BeginDate.Value.Date;		//日期型的控件（不含时间）
            DateTime enddate = dtp_EndDate.Value.Date.AddDays(1).AddSeconds(-1);
            if (enddate > DateTime.Now) enddate = DateTime.Now;

            try
            {
                if (tbx_ClientCode.Text.Trim() == "")
                    _Sync.RunSync(begindate, enddate);
                else
                    _Sync.RunSync(begindate, enddate, tbx_ClientCode.Text.Trim());
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Source, err.Message);
                MCSFramework.Common.LogWriter.WriteLog("Form1.timer1_Tick", err);
            }
        }


        /// <summary>
        /// 开始定时自动同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_autorun_Click(object sender, EventArgs e)
        {
            //设置时间控件
            LastSyncTime = DateTime.Today.AddDays(-3);
            timer1.Interval = (int)numericUpDown1.Value * 60 * 1000;
            timer1.Enabled = true;

            btn_Stop.Enabled = true;
            btn_autorun.Enabled = false;
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            btn_Stop.Enabled = false;
            btn_autorun.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_Sync.IsSyncing) return;

                btn_run.Enabled = false;
                bt_CancelSync.Enabled = true;

                DateTime begin = DateTime.Today.AddDays(-15);      //从最后一次同步时间开始此次的同步
                DateTime end = DateTime.Now;

                _Sync.RunSync(begin, end);
              //  LastSyncTime = end;
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.Source, err.Message);
                MCSFramework.Common.LogWriter.WriteLog("Form1.timer1_Tick", err);


            }
        }


        private void bt_CancelSync_Click(object sender, EventArgs e)
        {
            if (_Sync.IsSyncing)
                _Sync.CancelSync();
        }
    }
}
