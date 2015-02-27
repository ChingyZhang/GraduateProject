namespace ImportTools
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_SalesStaff = new System.Windows.Forms.TabPage();
            this.gv_SalesStaff = new System.Windows.Forms.DataGridView();
            this.tab_Distributor = new System.Windows.Forms.TabPage();
            this.gv_Distributor = new System.Windows.Forms.DataGridView();
            this.tab_Retailer = new System.Windows.Forms.TabPage();
            this.gv_Retailer = new System.Windows.Forms.DataGridView();
            this.tab_Promotor = new System.Windows.Forms.TabPage();
            this.gv_Promotor = new System.Windows.Forms.DataGridView();
            this.bt_RetrieveData = new System.Windows.Forms.Button();
            this.bt_StopImport = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_Count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_StartImport = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lb_Current = new System.Windows.Forms.Label();
            this.cbx_Promotor = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tab_SalesStaff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_SalesStaff)).BeginInit();
            this.tab_Distributor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Distributor)).BeginInit();
            this.tab_Retailer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Retailer)).BeginInit();
            this.tab_Promotor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Promotor)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_SalesStaff);
            this.tabControl1.Controls.Add(this.tab_Distributor);
            this.tabControl1.Controls.Add(this.tab_Retailer);
            this.tabControl1.Controls.Add(this.tab_Promotor);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 61);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(991, 537);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_SalesStaff
            // 
            this.tab_SalesStaff.Controls.Add(this.gv_SalesStaff);
            this.tab_SalesStaff.Location = new System.Drawing.Point(4, 21);
            this.tab_SalesStaff.Name = "tab_SalesStaff";
            this.tab_SalesStaff.Padding = new System.Windows.Forms.Padding(3);
            this.tab_SalesStaff.Size = new System.Drawing.Size(983, 512);
            this.tab_SalesStaff.TabIndex = 1;
            this.tab_SalesStaff.Text = "销售人员";
            this.tab_SalesStaff.UseVisualStyleBackColor = true;
            // 
            // gv_SalesStaff
            // 
            this.gv_SalesStaff.AllowUserToAddRows = false;
            this.gv_SalesStaff.AllowUserToDeleteRows = false;
            this.gv_SalesStaff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_SalesStaff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_SalesStaff.Location = new System.Drawing.Point(6, 6);
            this.gv_SalesStaff.Name = "gv_SalesStaff";
            this.gv_SalesStaff.ReadOnly = true;
            this.gv_SalesStaff.Size = new System.Drawing.Size(974, 504);
            this.gv_SalesStaff.TabIndex = 0;
            // 
            // tab_Distributor
            // 
            this.tab_Distributor.Controls.Add(this.gv_Distributor);
            this.tab_Distributor.Location = new System.Drawing.Point(4, 21);
            this.tab_Distributor.Name = "tab_Distributor";
            this.tab_Distributor.Size = new System.Drawing.Size(982, 505);
            this.tab_Distributor.TabIndex = 2;
            this.tab_Distributor.Text = "经销商";
            this.tab_Distributor.UseVisualStyleBackColor = true;
            // 
            // gv_Distributor
            // 
            this.gv_Distributor.AllowUserToAddRows = false;
            this.gv_Distributor.AllowUserToDeleteRows = false;
            this.gv_Distributor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_Distributor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Distributor.Location = new System.Drawing.Point(5, 5);
            this.gv_Distributor.Name = "gv_Distributor";
            this.gv_Distributor.ReadOnly = true;
            this.gv_Distributor.Size = new System.Drawing.Size(973, 497);
            this.gv_Distributor.TabIndex = 1;
            // 
            // tab_Retailer
            // 
            this.tab_Retailer.Controls.Add(this.gv_Retailer);
            this.tab_Retailer.Location = new System.Drawing.Point(4, 21);
            this.tab_Retailer.Name = "tab_Retailer";
            this.tab_Retailer.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Retailer.Size = new System.Drawing.Size(982, 505);
            this.tab_Retailer.TabIndex = 0;
            this.tab_Retailer.Text = "终端门店";
            this.tab_Retailer.UseVisualStyleBackColor = true;
            // 
            // gv_Retailer
            // 
            this.gv_Retailer.AllowUserToAddRows = false;
            this.gv_Retailer.AllowUserToDeleteRows = false;
            this.gv_Retailer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_Retailer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Retailer.Location = new System.Drawing.Point(5, 5);
            this.gv_Retailer.Name = "gv_Retailer";
            this.gv_Retailer.ReadOnly = true;
            this.gv_Retailer.Size = new System.Drawing.Size(973, 497);
            this.gv_Retailer.TabIndex = 1;
            // 
            // tab_Promotor
            // 
            this.tab_Promotor.Controls.Add(this.gv_Promotor);
            this.tab_Promotor.Location = new System.Drawing.Point(4, 21);
            this.tab_Promotor.Name = "tab_Promotor";
            this.tab_Promotor.Size = new System.Drawing.Size(982, 505);
            this.tab_Promotor.TabIndex = 3;
            this.tab_Promotor.Text = "促销员";
            this.tab_Promotor.UseVisualStyleBackColor = true;
            // 
            // gv_Promotor
            // 
            this.gv_Promotor.AllowUserToAddRows = false;
            this.gv_Promotor.AllowUserToDeleteRows = false;
            this.gv_Promotor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_Promotor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_Promotor.Location = new System.Drawing.Point(5, 5);
            this.gv_Promotor.Name = "gv_Promotor";
            this.gv_Promotor.ReadOnly = true;
            this.gv_Promotor.Size = new System.Drawing.Size(973, 497);
            this.gv_Promotor.TabIndex = 1;
            // 
            // bt_RetrieveData
            // 
            this.bt_RetrieveData.Location = new System.Drawing.Point(13, 4);
            this.bt_RetrieveData.Name = "bt_RetrieveData";
            this.bt_RetrieveData.Size = new System.Drawing.Size(75, 23);
            this.bt_RetrieveData.TabIndex = 1;
            this.bt_RetrieveData.Text = "检索数据";
            this.bt_RetrieveData.UseVisualStyleBackColor = true;
            this.bt_RetrieveData.Click += new System.EventHandler(this.bt_RetrieveData_Click);
            // 
            // bt_StopImport
            // 
            this.bt_StopImport.Location = new System.Drawing.Point(175, 4);
            this.bt_StopImport.Name = "bt_StopImport";
            this.bt_StopImport.Size = new System.Drawing.Size(75, 23);
            this.bt_StopImport.TabIndex = 23;
            this.bt_StopImport.Text = "停止导入";
            this.bt_StopImport.UseVisualStyleBackColor = true;
            this.bt_StopImport.Click += new System.EventHandler(this.bt_StopImport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(505, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(473, 23);
            this.progressBar1.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "条记录，当前正在处理";
            // 
            // lb_Count
            // 
            this.lb_Count.Location = new System.Drawing.Point(274, 9);
            this.lb_Count.Name = "lb_Count";
            this.lb_Count.Size = new System.Drawing.Size(52, 18);
            this.lb_Count.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(258, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "共";
            // 
            // bt_StartImport
            // 
            this.bt_StartImport.Location = new System.Drawing.Point(94, 4);
            this.bt_StartImport.Name = "bt_StartImport";
            this.bt_StartImport.Size = new System.Drawing.Size(75, 23);
            this.bt_StartImport.TabIndex = 18;
            this.bt_StartImport.Text = "开始导入";
            this.bt_StartImport.UseVisualStyleBackColor = true;
            this.bt_StartImport.Click += new System.EventHandler(this.bt_StartImport_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel表格|*.xls";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // lb_Current
            // 
            this.lb_Current.Location = new System.Drawing.Point(448, 8);
            this.lb_Current.Name = "lb_Current";
            this.lb_Current.Size = new System.Drawing.Size(51, 18);
            this.lb_Current.TabIndex = 24;
            // 
            // cbx_Promotor
            // 
            this.cbx_Promotor.AutoSize = true;
            this.cbx_Promotor.Location = new System.Drawing.Point(18, 39);
            this.cbx_Promotor.Name = "cbx_Promotor";
            this.cbx_Promotor.Size = new System.Drawing.Size(96, 16);
            this.cbx_Promotor.TabIndex = 1;
            this.cbx_Promotor.Text = "导入销售人员";
            this.cbx_Promotor.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(104, 39);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "导入经销商";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(190, 39);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 26;
            this.checkBox3.Text = "导入门店";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(276, 39);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(84, 16);
            this.checkBox4.TabIndex = 27;
            this.checkBox4.Text = "导入促销员";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 598);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.cbx_Promotor);
            this.Controls.Add(this.lb_Current);
            this.Controls.Add(this.bt_StopImport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_Count);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_StartImport);
            this.Controls.Add(this.bt_RetrieveData);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tab_SalesStaff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_SalesStaff)).EndInit();
            this.tab_Distributor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_Distributor)).EndInit();
            this.tab_Retailer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_Retailer)).EndInit();
            this.tab_Promotor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gv_Promotor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_SalesStaff;
        private System.Windows.Forms.TabPage tab_Distributor;
        private System.Windows.Forms.TabPage tab_Retailer;
        private System.Windows.Forms.TabPage tab_Promotor;
        private System.Windows.Forms.Button bt_RetrieveData;
        private System.Windows.Forms.Button bt_StopImport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_Count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_StartImport;
        private System.Windows.Forms.DataGridView gv_SalesStaff;
        private System.Windows.Forms.DataGridView gv_Distributor;
        private System.Windows.Forms.DataGridView gv_Retailer;
        private System.Windows.Forms.DataGridView gv_Promotor;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lb_Current;
        private System.Windows.Forms.CheckBox cbx_Promotor;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

