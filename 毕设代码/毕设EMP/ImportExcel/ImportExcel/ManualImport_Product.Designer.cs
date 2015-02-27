namespace ImportExcel
{
    partial class ManualImport_Product
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
            this.bt_OpenFile = new System.Windows.Forms.Button();
            this.tbx_FileName = new System.Windows.Forms.TextBox();
            this.bt_Import = new System.Windows.Forms.Button();
            this.tbx_Msg = new System.Windows.Forms.RichTextBox();
            this.tbx_Remark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_DataSource = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel文件|*.xls";
            // 
            // bt_OpenFile
            // 
            this.bt_OpenFile.Location = new System.Drawing.Point(9, 10);
            this.bt_OpenFile.Name = "bt_OpenFile";
            this.bt_OpenFile.Size = new System.Drawing.Size(75, 23);
            this.bt_OpenFile.TabIndex = 0;
            this.bt_OpenFile.Text = "打开Excel";
            this.bt_OpenFile.UseVisualStyleBackColor = true;
            this.bt_OpenFile.Click += new System.EventHandler(this.bt_OpenFile_Click);
            // 
            // tbx_FileName
            // 
            this.tbx_FileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_FileName.Location = new System.Drawing.Point(90, 11);
            this.tbx_FileName.Name = "tbx_FileName";
            this.tbx_FileName.Size = new System.Drawing.Size(317, 21);
            this.tbx_FileName.TabIndex = 1;
            // 
            // bt_Import
            // 
            this.bt_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Import.Location = new System.Drawing.Point(674, 11);
            this.bt_Import.Name = "bt_Import";
            this.bt_Import.Size = new System.Drawing.Size(84, 23);
            this.bt_Import.TabIndex = 2;
            this.bt_Import.Text = "开始导入";
            this.bt_Import.UseVisualStyleBackColor = true;
            this.bt_Import.Click += new System.EventHandler(this.bt_Import_Click);
            // 
            // tbx_Msg
            // 
            this.tbx_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_Msg.Location = new System.Drawing.Point(9, 41);
            this.tbx_Msg.Name = "tbx_Msg";
            this.tbx_Msg.Size = new System.Drawing.Size(749, 465);
            this.tbx_Msg.TabIndex = 3;
            this.tbx_Msg.Text = "";
            // 
            // tbx_Remark
            // 
            this.tbx_Remark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_Remark.Location = new System.Drawing.Point(448, 11);
            this.tbx_Remark.Name = "tbx_Remark";
            this.tbx_Remark.Size = new System.Drawing.Size(82, 21);
            this.tbx_Remark.TabIndex = 4;
            this.tbx_Remark.Text = "RMS全品项";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "备注";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(536, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "来源ID";
            // 
            // tbx_DataSource
            // 
            this.tbx_DataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_DataSource.Location = new System.Drawing.Point(584, 11);
            this.tbx_DataSource.Name = "tbx_DataSource";
            this.tbx_DataSource.Size = new System.Drawing.Size(35, 21);
            this.tbx_DataSource.TabIndex = 6;
            this.tbx_DataSource.Text = "6";
            // 
            // ManualImport_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 518);
            this.Controls.Add(this.tbx_DataSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_Remark);
            this.Controls.Add(this.tbx_Msg);
            this.Controls.Add(this.bt_Import);
            this.Controls.Add(this.tbx_FileName);
            this.Controls.Add(this.bt_OpenFile);
            this.Name = "ManualImport_Product";
            this.Text = "ManualImport_Product";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button bt_OpenFile;
        private System.Windows.Forms.TextBox tbx_FileName;
        private System.Windows.Forms.Button bt_Import;
        private System.Windows.Forms.RichTextBox tbx_Msg;
        private System.Windows.Forms.TextBox tbx_Remark;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_DataSource;
    }
}