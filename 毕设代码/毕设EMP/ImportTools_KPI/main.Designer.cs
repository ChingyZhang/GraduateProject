namespace ImportTools_KPI
{
    partial class main
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.bt_downquarterfeetemp = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "导入办事处业绩目标";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(172, 58);
            this.button2.TabIndex = 1;
            this.button2.Text = "导入办事处费率";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(261, 315);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(172, 58);
            this.button4.TabIndex = 3;
            this.button4.Text = "导入员工KPI";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // bt_downquarterfeetemp
            // 
            this.bt_downquarterfeetemp.Location = new System.Drawing.Point(57, 236);
            this.bt_downquarterfeetemp.Name = "bt_downquarterfeetemp";
            this.bt_downquarterfeetemp.Size = new System.Drawing.Size(172, 58);
            this.bt_downquarterfeetemp.TabIndex = 4;
            this.bt_downquarterfeetemp.Text = "下载季度费率模版";
            this.bt_downquarterfeetemp.UseVisualStyleBackColor = true;
            this.bt_downquarterfeetemp.Click += new System.EventHandler(this.bt_downquarterfeetemp_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(261, 236);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(172, 58);
            this.button3.TabIndex = 5;
            this.button3.Text = "导入季度费率";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(57, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(172, 58);
            this.button5.TabIndex = 6;
            this.button5.Text = "下载办事处业绩目标模版";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(57, 156);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(172, 58);
            this.button6.TabIndex = 7;
            this.button6.Text = "下载办事处费率模版";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 491);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.bt_downquarterfeetemp);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "main";
            this.Text = "main";
            this.Load += new System.EventHandler(this.main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button bt_downquarterfeetemp;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}