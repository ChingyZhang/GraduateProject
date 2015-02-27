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
            this.tbx_Message = new System.Windows.Forms.TextBox();
            this.lb_Status = new System.Windows.Forms.Label();
            this.bt_Begin = new System.Windows.Forms.Button();
            this.bt_Stop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tbx_Message
            // 
            this.tbx_Message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbx_Message.Location = new System.Drawing.Point(12, 37);
            this.tbx_Message.Multiline = true;
            this.tbx_Message.Name = "tbx_Message";
            this.tbx_Message.ReadOnly = true;
            this.tbx_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx_Message.Size = new System.Drawing.Size(411, 280);
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
            // bt_Begin
            // 
            this.bt_Begin.Location = new System.Drawing.Point(12, 8);
            this.bt_Begin.Name = "bt_Begin";
            this.bt_Begin.Size = new System.Drawing.Size(75, 23);
            this.bt_Begin.TabIndex = 4;
            this.bt_Begin.Text = "开始";
            this.bt_Begin.UseVisualStyleBackColor = true;
            this.bt_Begin.Click += new System.EventHandler(this.bt_Begin_Click);
            // 
            // bt_Stop
            // 
            this.bt_Stop.Location = new System.Drawing.Point(93, 8);
            this.bt_Stop.Name = "bt_Stop";
            this.bt_Stop.Size = new System.Drawing.Size(75, 23);
            this.bt_Stop.TabIndex = 5;
            this.bt_Stop.Text = "停止";
            this.bt_Stop.UseVisualStyleBackColor = true;
            this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 329);
            this.Controls.Add(this.tbx_Message);
            this.Controls.Add(this.lb_Status);
            this.Controls.Add(this.bt_Begin);
            this.Controls.Add(this.bt_Stop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbx_Message;
        private System.Windows.Forms.Label lb_Status;
        private System.Windows.Forms.Button bt_Begin;
        private System.Windows.Forms.Button bt_Stop;
        private System.Windows.Forms.Timer timer1;
    }
}

