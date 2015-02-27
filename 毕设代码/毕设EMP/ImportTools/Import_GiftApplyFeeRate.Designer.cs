namespace ImportTools
{
    partial class Import_GiftApplyFeeRate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gv_List = new System.Windows.Forms.DataGridView();
            this.lb_Current = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.bt_StopImport = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_Count = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_StartImport = new System.Windows.Forms.Button();
            this.bt_RetrieveData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gv_List)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel表格|*.xls";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // gv_List
            // 
            this.gv_List.AllowUserToAddRows = false;
            this.gv_List.AllowUserToDeleteRows = false;
            this.gv_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_List.Location = new System.Drawing.Point(6, 46);
            this.gv_List.Name = "gv_List";
            this.gv_List.ReadOnly = true;
            this.gv_List.RowTemplate.Height = 23;
            this.gv_List.Size = new System.Drawing.Size(964, 631);
            this.gv_List.TabIndex = 42;
            // 
            // lb_Current
            // 
            this.lb_Current.Location = new System.Drawing.Point(440, 21);
            this.lb_Current.Name = "lb_Current";
            this.lb_Current.Size = new System.Drawing.Size(51, 18);
            this.lb_Current.TabIndex = 41;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // bt_StopImport
            // 
            this.bt_StopImport.Location = new System.Drawing.Point(167, 17);
            this.bt_StopImport.Name = "bt_StopImport";
            this.bt_StopImport.Size = new System.Drawing.Size(75, 23);
            this.bt_StopImport.TabIndex = 40;
            this.bt_StopImport.Text = "停止导入";
            this.bt_StopImport.UseVisualStyleBackColor = true;
            this.bt_StopImport.Click += new System.EventHandler(this.bt_StopImport_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(497, 17);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(473, 23);
            this.progressBar1.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "条记录，当前正在处理";
            // 
            // lb_Count
            // 
            this.lb_Count.Location = new System.Drawing.Point(266, 22);
            this.lb_Count.Name = "lb_Count";
            this.lb_Count.Size = new System.Drawing.Size(52, 18);
            this.lb_Count.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(250, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 18);
            this.label1.TabIndex = 36;
            this.label1.Text = "共";
            // 
            // bt_StartImport
            // 
            this.bt_StartImport.Location = new System.Drawing.Point(86, 17);
            this.bt_StartImport.Name = "bt_StartImport";
            this.bt_StartImport.Size = new System.Drawing.Size(75, 23);
            this.bt_StartImport.TabIndex = 35;
            this.bt_StartImport.Text = "开始导入";
            this.bt_StartImport.UseVisualStyleBackColor = true;
            this.bt_StartImport.Click += new System.EventHandler(this.bt_StartImport_Click);
            // 
            // bt_RetrieveData
            // 
            this.bt_RetrieveData.Location = new System.Drawing.Point(5, 17);
            this.bt_RetrieveData.Name = "bt_RetrieveData";
            this.bt_RetrieveData.Size = new System.Drawing.Size(75, 23);
            this.bt_RetrieveData.TabIndex = 34;
            this.bt_RetrieveData.Text = "检索数据";
            this.bt_RetrieveData.UseVisualStyleBackColor = true;
            this.bt_RetrieveData.Click += new System.EventHandler(this.bt_RetrieveData_Click);
            // 
            // Import_GiftApplyFeeRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 694);
            this.Controls.Add(this.gv_List);
            this.Controls.Add(this.lb_Current);
            this.Controls.Add(this.bt_StopImport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_Count);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_StartImport);
            this.Controls.Add(this.bt_RetrieveData);
            this.Name = "Import_GiftApplyFeeRate";
            this.Text = "导入赠品费率";
            ((System.ComponentModel.ISupportInitialize)(this.gv_List)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView gv_List;
        private System.Windows.Forms.Label lb_Current;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button bt_StopImport;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_Count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_StartImport;
        private System.Windows.Forms.Button bt_RetrieveData;


    }
}