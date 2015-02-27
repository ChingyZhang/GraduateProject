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
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Model;
using System.IO;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;

namespace ImportTools_KPI
{
    public partial class ImportFeeRateTarget : Form
    {
        private string fileName = "";
        string improtmessage = "";
        string errormessage = "";

        public ImportFeeRateTarget()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
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
            PDT_Product product;
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
                       headerRow.GetCell(3).ToString() != "归属月份" ||
                       headerRow.GetCell(4).ToString() != "办事处月度费率目标" ||
                       headerRow.GetCell(5).ToString() != "办事处上月发生费用")
                {
                    MessageBox.Show("工作表表头(1~6列)错误！\r\n");
                    return;
                }
                int month = 0;
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
                    int cloumn = 4;
                    int cityid = 0;
                    if (!int.TryParse(row.GetCell(1).ToString(), out cityid))
                    {
                        errormessage += "办事处：" + row.GetCell(2).ToString() + "ID错误;\r\n";
                        row.GetCell(6).SetCellValue(errormessage);
                        continue;
                    }
                    Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(cityid).Model;
                    if (_city == null || _city.Name != row.GetCell(2).ToString())
                    {

                        errormessage += "办事处ID号：" + cityid.ToString() + "与办事处名称不匹配！\r\n";
                        row.GetCell(6).SetCellValue(errormessage);
                        continue;
                    }
                    ICell cell = row.GetCell(3);
                    if (month == 0 && headerRow.GetCell(3).ToString() == "归属月份")
                    {
                        //ICellStyle cellStyle = cell.CellStyle;
                        //IDataFormat format = hssfworkbook.CreateDataFormat();
                        //cellStyle.DataFormat = format.GetFormat("yyyy-MM");
                        //cell.CellStyle = cellStyle;
                        IList<AC_AccountMonth> _monthlist = AC_AccountMonthBLL.GetModelList("Name='" + cell.ToString() + "'");
                        if (_monthlist.Count > 0)
                        {
                            month = _monthlist[0].ID;
                        }
                        else
                        {
                            errormessage += "会计月错误;\r\n";
                            row.GetCell(6).SetCellValue(errormessage);
                            continue;
                        }
                    }

                    SVM_OrganizeTargetBLL bll = null;
                    IList<SVM_OrganizeTarget> _targetlist = SVM_OrganizeTargetBLL.GetModelList("OrganizeCity=" + cityid.ToString() + "AND AccountMonth=" + month.ToString());
                    if (_targetlist.Count > 0)
                    {
                        if (_targetlist.FirstOrDefault<SVM_OrganizeTarget>(p => (p.ApproveFlag == 1)) != null)
                        {
                            errormessage += "办事处：" + row.GetCell(2).ToString() + "当月的重点品项已审核，不可再次导入！\r\n";
                            row.GetCell(6).SetCellValue(errormessage);
                            continue;
                        }
                        if (_targetlist.Count == 1)
                        {
                            bll = new SVM_OrganizeTargetBLL(_targetlist[0].ID);
                        }
                    }
                    if (bll == null)
                    {
                        bll = new SVM_OrganizeTargetBLL
                        {
                            Model = { OrganizeCity = cityid, AccountMonth = month, ApproveFlag = 2 }
                        };
                    }
                    decimal amount = 0M;

                    if (row.GetCell(cloumn) != null && decimal.TryParse(row.GetCell(cloumn).ToString(), out amount))
                    {
                        bll.Model.FeeRateTarget = amount;
                    }
                    else if (row.GetCell(cloumn) != null && row.GetCell(cloumn).CellType != CellType.BLANK)
                    {
                        errormessage += "ID号：" + cityid.ToString() + "，" + _city.Name + "办事处月度费率目标：" + headerRow.GetCell(cloumn).ToString() + "金额填写错误\r\n";
                        row.GetCell(6).SetCellValue(errormessage);
                    }


                    amount = 0M;
                    if (row.GetCell(++cloumn) != null && decimal.TryParse(row.GetCell(cloumn).ToString(), out amount))
                    {
                        bll.Model["ActFee"] = amount.ToString();
                        decimal actSales=0;
                        if (decimal.TryParse(bll.Model["ActSales"],out actSales) && actSales != 0)
                        {
                            bll.Model.FeeYieldRate = amount * 100 / actSales;
                        }
                        else bll.Model.FeeYieldRate = 0;
                    }
                    else if (row.GetCell(cloumn) != null && row.GetCell(cloumn).CellType != CellType.BLANK)
                    {
                        errormessage += "ID号：" + cityid.ToString() + "，" + _city.Name + "办事处上月发生费用：" + headerRow.GetCell(cloumn).ToString() + "金额填写错误\r\n";
                        row.GetCell(6).SetCellValue(errormessage);
                    }

                    #region 更新销量至数据库
                    if (bll.Model.ID > 0)
                    {
                        bll.Update();
                        improtmessage += "ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处费率被成功更新！\r\n";
                    }
                    else
                    {
                        bll.Add();
                        improtmessage += "ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处费率已成功导入！\r\n";
                    }
                    row.GetCell(6).SetCellValue("导入成功");
                    #endregion

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

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileName = openFileDialog1.FileName;

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
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
