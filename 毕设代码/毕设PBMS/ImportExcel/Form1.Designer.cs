namespace ImportExcel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbx_Message = new System.Windows.Forms.TextBox();
            this.lb_Status = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tool_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_End = new System.Windows.Forms.ToolStripMenuItem();
            this.退出系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.TSMI_OpenPCStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.bt_Begin = new System.Windows.Forms.ToolStripButton();
            this.bt_Stop = new System.Windows.Forms.ToolStripButton();
            this.bt_Quit = new System.Windows.Forms.ToolStripButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolbtn_Start = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbtn_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbtnStartRun = new System.Windows.Forms.ToolStripMenuItem();
            this.关于关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbx_Message
            // 
            this.tbx_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_Message.Location = new System.Drawing.Point(12, 100);
            this.tbx_Message.Multiline = true;
            this.tbx_Message.Name = "tbx_Message";
            this.tbx_Message.ReadOnly = true;
            this.tbx_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx_Message.Size = new System.Drawing.Size(772, 438);
            this.tbx_Message.TabIndex = 7;
            // 
            // lb_Status
            // 
            this.lb_Status.AutoSize = true;
            this.lb_Status.Location = new System.Drawing.Point(57, 37);
            this.lb_Status.Name = "lb_Status";
            this.lb_Status.Size = new System.Drawing.Size(0, 12);
            this.lb_Status.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton2,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(796, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_Start,
            this.tool_End,
            this.退出系统ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButton1.Text = "开始";
            // 
            // tool_Start
            // 
            this.tool_Start.Image = ((System.Drawing.Image)(resources.GetObject("tool_Start.Image")));
            this.tool_Start.Name = "tool_Start";
            this.tool_Start.Size = new System.Drawing.Size(152, 22);
            this.tool_Start.Text = "开始导入";
            this.tool_Start.Click += new System.EventHandler(this.bt_Begin_Click);
            // 
            // tool_End
            // 
            this.tool_End.Image = ((System.Drawing.Image)(resources.GetObject("tool_End.Image")));
            this.tool_End.Name = "tool_End";
            this.tool_End.Size = new System.Drawing.Size(152, 22);
            this.tool_End.Text = "结束导入";
            this.tool_End.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // 退出系统ToolStripMenuItem
            // 
            this.退出系统ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("退出系统ToolStripMenuItem.Image")));
            this.退出系统ToolStripMenuItem.Name = "退出系统ToolStripMenuItem";
            this.退出系统ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出系统ToolStripMenuItem.Text = "退出系统";
            this.退出系统ToolStripMenuItem.Click += new System.EventHandler(this.bt_Quit_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_OpenPCStart});
            this.toolStripDropDownButton3.Image = global::ImportExcelTDP.Properties.Resources.settings;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButton3.Text = "设定";
            // 
            // TSMI_OpenPCStart
            // 
            this.TSMI_OpenPCStart.Name = "TSMI_OpenPCStart";
            this.TSMI_OpenPCStart.Size = new System.Drawing.Size(124, 22);
            this.TSMI_OpenPCStart.Text = "开机启动";
            this.TSMI_OpenPCStart.Click += new System.EventHandler(this.toolbtn_StartRun_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI});
            this.toolStripDropDownButton2.Image = global::ImportExcelTDP.Properties.Resources.bubble;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButton2.Text = "帮助";
            // 
            // TSMI
            // 
            this.TSMI.Name = "TSMI";
            this.TSMI.Size = new System.Drawing.Size(152, 22);
            this.TSMI.Text = "关于软件(&A)";
            this.TSMI.Click += new System.EventHandler(this.关于软件ToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::ImportExcelTDP.Properties.Resources.toolbtn_QuitA_Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "退出";
            this.toolStripButton1.Click += new System.EventHandler(this.bt_Quit_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bt_Begin,
            this.bt_Stop,
            this.bt_Quit});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(796, 72);
            this.toolStrip2.TabIndex = 9;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // bt_Begin
            // 
            this.bt_Begin.AutoSize = false;
            this.bt_Begin.Image = ((System.Drawing.Image)(resources.GetObject("bt_Begin.Image")));
            this.bt_Begin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_Begin.Name = "bt_Begin";
            this.bt_Begin.Size = new System.Drawing.Size(75, 69);
            this.bt_Begin.Text = "开始导入";
            this.bt_Begin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bt_Begin.Click += new System.EventHandler(this.bt_Begin_Click);
            // 
            // bt_Stop
            // 
            this.bt_Stop.Image = ((System.Drawing.Image)(resources.GetObject("bt_Stop.Image")));
            this.bt_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(60, 69);
            this.bt_Stop.Text = "结束导入";
            this.bt_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // bt_Quit
            // 
            this.bt_Quit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bt_Quit.Image = ((System.Drawing.Image)(resources.GetObject("bt_Quit.Image")));
            this.bt_Quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_Quit.Name = "bt_Quit";
            this.bt_Quit.Size = new System.Drawing.Size(52, 69);
            this.bt_Quit.Text = "退出";
            this.bt_Quit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bt_Quit.Click += new System.EventHandler(this.bt_Quit_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "软件已隐藏，您可以通过鼠标右击或双击操作";
            this.notifyIcon1.BalloonTipTitle = "南京美驰软件";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "南京美驰数据导入工具";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtn_Start,
            this.toolbtn_Stop,
            this.Menu_Show,
            this.toolbtnStartRun,
            this.关于关于ToolStripMenuItem,
            this.Menu_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 136);
            // 
            // toolbtn_Start
            // 
            this.toolbtn_Start.Image = ((System.Drawing.Image)(resources.GetObject("toolbtn_Start.Image")));
            this.toolbtn_Start.Name = "toolbtn_Start";
            this.toolbtn_Start.Size = new System.Drawing.Size(136, 22);
            this.toolbtn_Start.Text = "开始导入";
            this.toolbtn_Start.Click += new System.EventHandler(this.bt_Begin_Click);
            // 
            // toolbtn_Stop
            // 
            this.toolbtn_Stop.Image = ((System.Drawing.Image)(resources.GetObject("toolbtn_Stop.Image")));
            this.toolbtn_Stop.Name = "toolbtn_Stop";
            this.toolbtn_Stop.Size = new System.Drawing.Size(136, 22);
            this.toolbtn_Stop.Text = "暂停导入";
            this.toolbtn_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // Menu_Show
            // 
            this.Menu_Show.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Show.Image")));
            this.Menu_Show.Name = "Menu_Show";
            this.Menu_Show.Size = new System.Drawing.Size(136, 22);
            this.Menu_Show.Text = "显示";
            this.Menu_Show.Click += new System.EventHandler(this.Menu_Show_Click);
            // 
            // toolbtnStartRun
            // 
            this.toolbtnStartRun.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnStartRun.Image")));
            this.toolbtnStartRun.Name = "toolbtnStartRun";
            this.toolbtnStartRun.Size = new System.Drawing.Size(136, 22);
            this.toolbtnStartRun.Text = "开机自启动";
            this.toolbtnStartRun.Click += new System.EventHandler(this.toolbtn_StartRun_Click);
            // 
            // 关于关于ToolStripMenuItem
            // 
            this.关于关于ToolStripMenuItem.Name = "关于关于ToolStripMenuItem";
            this.关于关于ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.关于关于ToolStripMenuItem.Text = "关于(&A)";
            this.关于关于ToolStripMenuItem.Click += new System.EventHandler(this.关于软件ToolStripMenuItem_Click);
            // 
            // Menu_Exit
            // 
            this.Menu_Exit.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Exit.Image")));
            this.Menu_Exit.Name = "Menu_Exit";
            this.Menu_Exit.Size = new System.Drawing.Size(136, 22);
            this.Menu_Exit.Text = "退出";
            this.Menu_Exit.Click += new System.EventHandler(this.Menu_Exit_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(94, 22);
            this.toolStripButton2.Text = "录入SAP发货单";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 550);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbx_Message);
            this.Controls.Add(this.lb_Status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "南京美驰数据导入工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_Message;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tool_Start;
        private System.Windows.Forms.ToolStripMenuItem tool_End;
        private System.Windows.Forms.ToolStripMenuItem 退出系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem TSMI;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton bt_Begin;
        private System.Windows.Forms.ToolStripButton bt_Stop;
        private System.Windows.Forms.ToolStripButton bt_Quit;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem TSMI_OpenPCStart;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Menu_Show;
        private System.Windows.Forms.ToolStripMenuItem Menu_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolbtn_Start;
        private System.Windows.Forms.ToolStripMenuItem toolbtn_Stop;
        private System.Windows.Forms.ToolStripMenuItem toolbtnStartRun;
        private System.Windows.Forms.ToolStripMenuItem 关于关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}

