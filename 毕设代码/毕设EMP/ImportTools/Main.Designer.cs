namespace ImportTools
{
    partial class Main
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
            this.bt_ImportFee = new System.Windows.Forms.Button();
            this.bt_ImportFeeRate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_ImportFee
            // 
            this.bt_ImportFee.Location = new System.Drawing.Point(124, 61);
            this.bt_ImportFee.Name = "bt_ImportFee";
            this.bt_ImportFee.Size = new System.Drawing.Size(168, 95);
            this.bt_ImportFee.TabIndex = 0;
            this.bt_ImportFee.Text = "导入赠品余额及抵扣额";
            this.bt_ImportFee.UseVisualStyleBackColor = true;
            this.bt_ImportFee.Click += new System.EventHandler(this.bt_ImportFee_Click);
            // 
            // bt_ImportFeeRate
            // 
            this.bt_ImportFeeRate.Location = new System.Drawing.Point(366, 61);
            this.bt_ImportFeeRate.Name = "bt_ImportFeeRate";
            this.bt_ImportFeeRate.Size = new System.Drawing.Size(168, 95);
            this.bt_ImportFeeRate.TabIndex = 1;
            this.bt_ImportFeeRate.Text = "导入赠品费率";
            this.bt_ImportFeeRate.UseVisualStyleBackColor = true;
            this.bt_ImportFeeRate.Click += new System.EventHandler(this.bt_ImportFeeRate_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 237);
            this.Controls.Add(this.bt_ImportFeeRate);
            this.Controls.Add(this.bt_ImportFee);
            this.Name = "Main";
            this.Text = "赠品费用导入工具";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_ImportFee;
        private System.Windows.Forms.Button bt_ImportFeeRate;
    }
}