using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using MCSFramework.BLL;
using MCSFramework.Model;
using System.IO;
using MCSFramework.BLL.Pub;

namespace ImportTools_KPI
{
    public partial class DownFeeRateTargetTemp : Form
    {
        public string path;
        public DownFeeRateTargetTemp()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            HSSFWorkbook book = new HSSFWorkbook();
            ISheet sheet = book.CreateSheet("季度费率");
            sheet.SetColumnWidth(0, 12 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 12 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 16 * 256);
            sheet.SetColumnWidth(6, 12 * 256);
            int rowcount = 0;
            IRow row = sheet.CreateRow(rowcount);
            row.CreateCell(0).SetCellValue("营业部");
            row.CreateCell(1).SetCellValue("办事处ID");
            row.CreateCell(2).SetCellValue("办事处");
            row.CreateCell(3).SetCellValue("归属月份"); 
            row.CreateCell(4).SetCellValue("办事处月度费率目标");
            row.CreateCell(5).SetCellValue("办事处上月发生费用");
            row.CreateCell(6).SetCellValue("导入标志");

            IList<Addr_OrganizeCity> _cityList = Addr_OrganizeCityBLL.GetModelList("Level=4 Order By Level3_SuperID");
            AC_AccountMonthBLL monthbll = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-1)));
            foreach (Addr_OrganizeCity city in _cityList)
            {
                row = sheet.CreateRow(++rowcount);
                row.CreateCell(0).SetCellValue(new Addr_OrganizeCityBLL(city.SuperID).Model.Name);
                row.CreateCell(1).SetCellValue(city.ID);
                row.CreateCell(2).SetCellValue(city.Name);
                row.CreateCell(3).SetCellValue(monthbll.Model.Name);
                row.CreateCell(4).SetCellValue("");
                row.CreateCell(5).SetCellValue("");
                row.CreateCell(6).SetCellValue(""); 

                worker.ReportProgress(rowcount * 100 / _cityList.Count, rowcount);
            }


            //保存  
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                book.Write(fs);
            }
            book = null;
            sheet = null;

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = progressBar1.Value.ToString() + "%";

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bt_Down.Enabled = true;
            MessageBox.Show("生成完成");
            this.Close();
        }

        private void bt_Down_Click(object sender, EventArgs e)
        {
            string fileName = string.Format("{0}{1}记录", DateTime.Now.Date.ToString("yyyyMMdd"), "办事处费率模版");                                                  // 设置默认的保存文件名


            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "Excel文件|*.xls";
            saveFileDialog1.FileName = fileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                backgroundWorker1.RunWorkerAsync();
            }


            bt_Down.Enabled = false;
        }
    }
}
