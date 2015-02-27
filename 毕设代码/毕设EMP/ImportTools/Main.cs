using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportTools
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void bt_ImportFee_Click(object sender, EventArgs e)
        {
            Import_GiftApplyAmount form = new Import_GiftApplyAmount();
            form.ShowDialog();
        }

        private void bt_ImportFeeRate_Click(object sender, EventArgs e)
        {
            Import_GiftApplyFeeRate form = new Import_GiftApplyFeeRate();
            form.ShowDialog();
        }
    }
}
