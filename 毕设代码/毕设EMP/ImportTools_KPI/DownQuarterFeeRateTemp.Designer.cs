namespace ImportTools_KPI
{
    partial class DownQuarterFeeRateTemp
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
            this.lbl_quarter = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cmb_Quarter = new System.Windows.Forms.ComboBox();
            this.bt_Down = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_quarter
            // 
            this.lbl_quarter.AutoSize = true;
            this.lbl_quarter.Location = new System.Drawing.Point(43, 19);
            this.lbl_quarter.Name = "lbl_quarter";
            this.lbl_quarter.Size = new System.Drawing.Size(29, 12);
            this.lbl_quarter.TabIndex = 0;
            this.lbl_quarter.Text = "季度";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // cmb_Quarter
            // 
            this.cmb_Quarter.FormattingEnabled = true;
            this.cmb_Quarter.Location = new System.Drawing.Point(78, 14);
            this.cmb_Quarter.Name = "cmb_Quarter";
            this.cmb_Quarter.Size = new System.Drawing.Size(169, 20);
            this.cmb_Quarter.TabIndex = 1;
            // 
            // bt_Down
            // 
            this.bt_Down.Location = new System.Drawing.Point(298, 14);
            this.bt_Down.Name = "bt_Down";
            this.bt_Down.Size = new System.Drawing.Size(75, 23);
            this.bt_Down.TabIndex = 2;
            this.bt_Down.Text = "导出模版";
            this.bt_Down.UseVisualStyleBackColor = true;
            this.bt_Down.Click += new System.EventHandler(this.bt_Down_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(46, 60);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(328, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "0%";
            // 
            // DownQuarterFeeRateTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 406);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bt_Down);
            this.Controls.Add(this.cmb_Quarter);
            this.Controls.Add(this.lbl_quarter);
            this.Name = "DownQuarterFeeRateTemp";
            this.Text = "DownQuarterFeeRateTemp";
            this.Load += new System.EventHandler(this.DownQuarterFeeRateTemp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_quarter;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox cmb_Quarter;
        private System.Windows.Forms.Button bt_Down;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}