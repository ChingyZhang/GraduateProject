using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImportTools_KPI
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportKeyTarget from = new ImportKeyTarget();
            from.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImportFeeRateTarget from = new ImportFeeRateTarget();
            from.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 from = new Form1();
            from.ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {
          
        }

        private void bt_downquarterfeetemp_Click(object sender, EventArgs e)
        {
            DownQuarterFeeRateTemp from = new DownQuarterFeeRateTemp();
            from.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ImportQuarterFeeRate from = new ImportQuarterFeeRate();
            from.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DownOrganizeCityTargetTemp from = new DownOrganizeCityTargetTemp();
            from.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DownFeeRateTargetTemp from = new DownFeeRateTargetTemp();
            from.ShowDialog();
        }       
    }
}
