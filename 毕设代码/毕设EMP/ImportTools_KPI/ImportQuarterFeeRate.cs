using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Model; 
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.FNA;
namespace ImportTools_KPI
{
    public partial class ImportQuarterFeeRate : Form
    {
        private string fileName = "";
        string improtmessage = "";
        string errormessage = "";
        public ImportQuarterFeeRate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            HSSFWorkbook hssfworkbook;
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

            hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int i = 0, count = getRowsCount() - 1;

            try
            {
                IRow headerRow = sheet.GetRow(0);
                if (headerRow.GetCell(0).ToString() != "营业部" ||
                       headerRow.GetCell(1).ToString() != "办事处ID" ||
                       headerRow.GetCell(2).ToString() != "办事处" ||                    
                       headerRow.GetCell(3).ToString() != "归属季度ID" ||
                       headerRow.GetCell(4).ToString() != "归属季度" ||
                       headerRow.GetCell(5).ToString() != "季度预算费率" ||
                       headerRow.GetCell(6).ToString() != "季度实际费率")
                {
                    MessageBox.Show("工作表表头(1~6列)错误！\r\n");
                    return;
                }
                int quarter = 0;
                rows.MoveNext();
                while (rows.MoveNext())
                {

                    i++;
                    ((BackgroundWorker)sender).ReportProgress(i * 100 / count, i);
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (row.GetCell(0) == null || row.GetCell(0).ToString() == "")
                    {
                        break;
                    }
                    int cloumn = 5;
                    int cityid = 0;
                    if (!int.TryParse(row.GetCell(1).ToString(), out cityid))
                    {
                        errormessage += "办事处：" + row.GetCell(2).ToString() + "ID错误;\r\n";
                        row.GetCell(7).SetCellValue(errormessage);
                        continue;
                    }
                    Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(cityid).Model;
                    if (_city == null || _city.Name != row.GetCell(2).ToString())
                    {

                        errormessage += "办事处ID号：" + cityid.ToString() + "与办事处名称不匹配！\r\n";
                        row.GetCell(7).SetCellValue(errormessage);
                        continue;
                    }
                    ICell cell = row.GetCell(3);
                    if (quarter == 0 && headerRow.GetCell(3).ToString() == "归属季度ID")
                    {
                        IList<AC_AccountQuarter> _quarterlist = AC_AccountQuarterBLL.GetModelList("ID='" + cell.ToString() + "'");
                        if (_quarterlist.Count > 0)
                        {
                            quarter = _quarterlist[0].ID;
                        }
                        else
                        {
                            errormessage += "季度错误;\r\n";
                            row.GetCell(7).SetCellValue(errormessage);
                            continue;
                        }
                    }

                    FNA_StaffBounsLevelDetailBLL bll = null;
                    IList<FNA_StaffBounsLevelDetail> _targetlist = FNA_StaffBounsLevelDetailBLL.GetModelList("OrganizeCity=" + cityid.ToString() + "AND AccountQuarter=" + quarter.ToString());
                    if (_targetlist.Count > 0)
                    {
                        if (_targetlist.FirstOrDefault<FNA_StaffBounsLevelDetail>(p => (p.ApproveFlag == 1)) != null)
                        {
                            errormessage += "办事处：" + row.GetCell(2).ToString() + "当季度的费率已审核，不可再次导入！\r\n";
                            row.GetCell(7).SetCellValue(errormessage);
                            continue;
                        }
                        if (_targetlist.Count == 1)
                        {
                            bll = new FNA_StaffBounsLevelDetailBLL(_targetlist[0].ID);
                        }
                    }
                    if (bll == null)
                    {
                        row.GetCell(7).SetCellValue(errormessage);
                        errormessage += "办事处：" + row.GetCell(2).ToString() + "当季度的信息还未初始化！\r\n";
                        continue;
                    }
                    decimal amount = 0M;

                    if (row.GetCell(cloumn) != null && decimal.TryParse(row.GetCell(cloumn).ToString(), out amount))
                    {
                        bll.Model.BudgetFeeRate = amount;
                    }
                    else if (row.GetCell(cloumn) != null && row.GetCell(cloumn).CellType != CellType.BLANK)
                    {
                        errormessage += "ID号：" + cityid.ToString() + "，" + _city.Name + "办事处季度预算费率：" + headerRow.GetCell(cloumn).ToString() + "填写错误\r\n";
                        row.GetCell(7).SetCellValue(errormessage);
                        continue;
                    }


                    amount = 0M;
                    if (row.GetCell(++cloumn) != null && decimal.TryParse(row.GetCell(cloumn).ToString(), out amount))
                    {
                        bll.Model.ActFeeRate = amount;
                    }
                    else if (row.GetCell(cloumn) != null && row.GetCell(cloumn).CellType != CellType.BLANK)
                    {
                        errormessage += "ID号：" + cityid.ToString() + "，" + _city.Name + "办事处月季度实际费率：" + headerRow.GetCell(cloumn).ToString() + "金额填写错误\r\n";
                        row.GetCell(7).SetCellValue(errormessage);
                        continue;

                    }

                    #region 更新销量至数据库
                    if (bll.Model.ID > 0)
                    {
                        bll.Update();
                        improtmessage += "ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处季度费率被成功更新！\r\n";
                    }
                    

                    #endregion
                    row.GetCell(7).SetCellValue("导入成功");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                FileStream writefile = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                hssfworkbook.Write(writefile);
                writefile.Close();

                sheet = null;
            }
        }
        private int getRowsCount()
        {
            int count = 0;
            HSSFWorkbook hssfworkbook;
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            while (rows.MoveNext() && ((HSSFRow)rows.Current).GetCell(0).ToString() != "")
            {
                count++;
            }
            return count;
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            label2.Text = "正在处理EXCEL文件，请稍后... 已处理" + progressBar1.Value + "%,第" + e.UserState.ToString() + "条";
            tbx_Message.Text = tbx_Message.Text + improtmessage;
            txt_ErrorMessage.Text = txt_ErrorMessage.Text + errormessage;
            improtmessage = "";
            errormessage = "";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
        }
    }
}
