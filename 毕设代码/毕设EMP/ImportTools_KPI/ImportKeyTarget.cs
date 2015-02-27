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
    public partial class ImportKeyTarget : Form
    {
        private string fileName = "";
        string improtmessage = "", errormessage = "";


        public ImportKeyTarget()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            HSSFWorkbook hssfworkbook;
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            PDT_Product product;
            hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int i = 0, count = getRowsCount()-1;

            try
            {
                IRow headerRow = sheet.GetRow(0);
                if (headerRow.GetCell(0).ToString() != "营业部" ||
                       headerRow.GetCell(1).ToString() != "办事处ID" ||
                       headerRow.GetCell(2).ToString() != "办事处" ||
                       headerRow.GetCell(3).ToString() != "归属月份" ||
                       headerRow.GetCell(4).ToString() != "办事处业绩目标额")
                {
                    MessageBox.Show("工作表表头(1~5列)错误！\r\n");
                    return;
                }
                int month = 0;
                rows.MoveNext();
                ((BackgroundWorker)sender).ReportProgress(i * 100 / count, i);
                while (rows.MoveNext())
                {
                    i++;
                    ((BackgroundWorker)sender).ReportProgress(i * 100 / count, i);
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (row.GetCell(0).ToString() == "")
                    {
                        break;
                    }
                    
                    int cloumn = 4;
                    int cityid = 0;
                    if (!int.TryParse(row.GetCell(1).ToString(), out cityid))
                    {
                        errormessage += "办事处：" + row.GetCell(2).ToString() + "ID错误;\r\n";
                        row.GetCell(5).SetCellValue(errormessage);
                        continue;
                    }
                    Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(cityid).Model;
                    if (_city == null || _city.Name != row.GetCell(2).ToString())
                    {
                        errormessage += "办事处ID号：" + cityid.ToString() + "与办事处名称不匹配！\r\n";
                        row.GetCell(5).SetCellValue(errormessage);
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
                            row.GetCell(5).SetCellValue(errormessage);
                            continue;
                        }
                    }

                    SVM_OrganizeTargetBLL bll = null;
                    IList<SVM_OrganizeTarget> _targetlist = SVM_OrganizeTargetBLL.GetModelList("OrganizeCity=" + cityid.ToString() + "AND AccountMonth=" + month.ToString());
                    if (_targetlist.Count > 0)
                    {
                        if (_targetlist.FirstOrDefault<SVM_OrganizeTarget>(p => (p.ApproveFlag == 1)) != null)
                        {
                            errormessage += "办事处：" + row.GetCell(2).ToString() + "当月的办事处目标已审核，不可再次导入！\r\n";
                            row.GetCell(5).SetCellValue(errormessage);
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

                    if (decimal.TryParse(row.GetCell(cloumn).ToString(), out amount))
                    {
                        bll.Model.SalesTarget = amount;
                    }
                    else
                    {
                        errormessage += "办事处：" + row.GetCell(2).ToString() + "的办事处业绩目标额未能导入，办事处业绩目标额：" + headerRow.GetCell(cloumn).ToString() + "金额填写错误\r\n";
                        row.GetCell(5).SetCellValue(errormessage);
                        continue;
                    }
                    ++cloumn;
                    //IList<SVM_KeyProductTarget_Detail> details = new List<SVM_KeyProductTarget_Detail>();


                    //bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                    //while (true)
                    //{
                    //    product = null;
                    //    amount = 0M;

                    //    if (headerRow.GetCell(cloumn).CellType == CellType.BLANK || headerRow.GetCell(cloumn).ToString() == string.Empty)
                    //    {
                    //        break;
                    //    }

                    //    IList<PDT_Product> products = PDT_ProductBLL.GetModelList("ShortName='" + headerRow.GetCell(cloumn).ToString() + "' AND State=1");
                    //    if (products.Count > 0)
                    //    {
                    //        product = products[0];
                    //    }
                    //    else
                    //    {
                    //        errormessage += "产品名称：" + headerRow.GetCell(cloumn).ToString() + "在产品列表中不存在！\r\n";
                    //        cloumn++;
                    //        continue;
                    //    }
                    //    if ((product != null) && row.GetCell(cloumn).CellType != CellType.BLANK)
                    //    {
                    //        decimal.TryParse(row.GetCell(cloumn).ToString(), out amount);
                    //        if (amount != 0M)
                    //        {
                    //            SVM_KeyProductTarget_Detail detail = new SVM_KeyProductTarget_Detail
                    //            {
                    //                Product = product.ID,
                    //                Amount = amount
                    //            };
                    //            details.Add(detail);
                    //        }
                    //        else if (row.GetCell(cloumn).CellType != CellType.BLANK && (row.GetCell(cloumn).ToString() != "0"))
                    //        {
                    //            wrongflag = true;
                    //            break;
                    //        }
                    //    }
                    //    cloumn++;
                    //}
                    //if (wrongflag)
                    //{
                    //    errormessage += "办事处：" + row.GetCell(2).ToString() + "的重点品项未能导入，品项名称：" + headerRow.GetCell(cloumn).ToString() + "金额填写错误\r\n";

                    //    continue;
                    //}
                    #region 更新销量至数据库
                    if (bll.Model.ID > 0)
                    {

                        //bll.DeleteDetail();     //先清除原先导入的数据
                        //bll.Items = details;
                        //bll.AddDetail();

                        bll.Update();
                        improtmessage += "ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处目标被成功更新！\r\n";


                    }
                    else
                    {

                        //if (details.Count > 0)
                        //{
                            //bll.Items = details;
                            if (bll.Add() > 0)
                            {
                                //foreach (SVM_OrganizeTarget m in _targetlist)
                                //{
                                //    bll = new SVM_OrganizeTargetBLL(m.ID);
                                //    bll.DeleteDetail();
                                //    bll.Delete();
                                //}
                                improtmessage += "ID号：" + cityid.ToString() + "，" + _city.Name + " 的重点品项已成功导入！\r\n";
                            }
                          
                        //}
                    }
                    row.GetCell(5).SetCellValue("导入成功");
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


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            label2.Text = "正在处理EXCEL文件，请稍后... 已处理" + progressBar1.Value + "%,第" +(int.Parse( e.UserState.ToString())-1).ToString() + "条";
            tbx_Message.Text = tbx_Message.Text + improtmessage;
            txt_errormessage.Text = txt_errormessage.Text + errormessage;
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
