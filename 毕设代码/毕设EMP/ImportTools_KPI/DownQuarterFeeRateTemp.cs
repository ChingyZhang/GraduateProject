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

    public partial class DownQuarterFeeRateTemp : Form
    {
        public string path;
        
        public DownQuarterFeeRateTemp()
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
            sheet.SetColumnWidth(2, 20*256);
            sheet.SetColumnWidth(3, 12 * 256);
            sheet.SetColumnWidth(4, 20*256);
            sheet.SetColumnWidth(5, 16 * 256);
            sheet.SetColumnWidth(6, 12 * 256);
            int rowcount = 0;
            IRow row = sheet.CreateRow(rowcount);
            row.CreateCell(0).SetCellValue("营业部");
            row.CreateCell(1).SetCellValue("办事处ID");
            row.CreateCell(2).SetCellValue("办事处");
            row.CreateCell(3).SetCellValue("归属季度ID");
            row.CreateCell(4).SetCellValue("归属季度");
            row.CreateCell(5).SetCellValue("季度预算费率");
            row.CreateCell(6).SetCellValue("季度实际费率");
            row.CreateCell(7).SetCellValue("导入标志");

            IList<Addr_OrganizeCity> _cityList = Addr_OrganizeCityBLL.GetModelList("Level=4 Order By Level3_SuperID");
     
            foreach (Addr_OrganizeCity city in _cityList)
            {
                row = sheet.CreateRow(++rowcount);
                row.CreateCell(0).SetCellValue(new Addr_OrganizeCityBLL(city.SuperID).Model.Name);
                row.CreateCell(1).SetCellValue(city.ID);
                row.CreateCell(2).SetCellValue(city.Name);
                row.CreateCell(3).SetCellValue(cmb_Quarter.SelectedValue.ToString());
                row.CreateCell(4).SetCellValue(cmb_Quarter.SelectedText);
                row.CreateCell(5).SetCellValue("");
                row.CreateCell(6).SetCellValue("");
                row.CreateCell(7).SetCellValue("");

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

        private void DownQuarterFeeRateTemp_Load(object sender, EventArgs e)
        {
            cmb_Quarter.DataSource = AC_AccountQuarterBLL.GetModelList("");
            cmb_Quarter.DisplayMember = "Name";
            cmb_Quarter.ValueMember = "ID";    
        }

        private void bt_Down_Click(object sender, EventArgs e)
        {

            string fileName = string.Format("{0}{1}记录", DateTime.Now.Date.ToString("yyyyMMdd"), "季度费率模版");                                                  // 设置默认的保存文件名


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
