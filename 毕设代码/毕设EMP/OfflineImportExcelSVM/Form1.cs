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
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.SVM;
using MCSFramework.Model;
using System.IO;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;
using MCSFramework.Model.Promotor;

namespace OfflineImportExcelSVM
{
    public partial class Form1 : Form
    {
        private string fileName = "";
        string improtmessage = "", errormessage = "";

        public Form1()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
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
       
        private int getColumnCount(IRow headerRow)
        {
            int column = 0;
            while (true)
            {
                if (headerRow.GetCell(column)==null||headerRow.GetCell(column).CellType == CellType.BLANK || headerRow.GetCell(column).ToString() == string.Empty)
                {                  
                    break;
                }
                column++;
            }
            return column;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime minday, maxday=DateTime.Now;
            HSSFWorkbook hssfworkbook;
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int i = 0, count = getRowsCount()-1;
            bool flag = true;
            try
            {
                IRow headerRow = sheet.GetRow(0);
                if (headerRow.GetCell(0).ToString() != "零售商ID" ||
                    headerRow.GetCell(1).ToString() != "零售商编号" ||
                    headerRow.GetCell(2).ToString() != "零售商名称" ||
                    headerRow.GetCell(3).ToString() != "零售商分类" ||
                    headerRow.GetCell(4).ToString() != "归属月份" ||
                    headerRow.GetCell(5).ToString() != "导购ID" ||
                    headerRow.GetCell(6).ToString() != "导购姓名")
                {

                    flag = false;
                }
                if (!flag)
                {
                    MessageBox.Show(this, "表头(1~7列)错误!");
                    return;
                }
                int column = getColumnCount(headerRow) + 1;
                int month = 0;
                rows.MoveNext();
                
                while (rows.MoveNext())
                {
                    int datacolumn = 7;

                    i++;
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (row.GetCell(0).ToString() == "")
                    {
                        break;
                    }
                    
                   
                    int clientid = 0; int promotorid = 0;
                    if (!int.TryParse(row.GetCell(0).ToString(), out clientid))
                    {
                        errormessage += "零售商：" + row.GetCell(2).ToString() + "的ID错误;\r\n";
                        row.GetCell(column).SetCellValue(errormessage);
                        continue;
                    }
                    CM_Client client = new CM_ClientBLL(clientid).Model;

                    if (client == null)
                    {
                        errormessage += "ID号：" + clientid.ToString() + "零售商在系统中不存在！\r\n";
                        row.GetCell(column).SetCellValue(errormessage);
                        continue;
                    }
                    if (int.TryParse(row.GetCell(5).ToString(), out promotorid))
                    {                        
                        PM_Promotor pm = new PM_PromotorBLL(promotorid).Model;
                        if (pm == null)
                        {
                            errormessage += "导购ID号：" + promotorid.ToString() + "导购在系统中不存在！\r\n";
                            row.GetCell(column).SetCellValue(errormessage);
                            continue;
                        }
                        
                    }                   

                    if (month == 0 && headerRow.GetCell(4).ToString() == "归属月份")
                    {
                        IList<AC_AccountMonth> _monthlist = AC_AccountMonthBLL.GetModelList("Name='" + row.GetCell(4).ToString() + "'");
                        if (_monthlist.Count > 0)
                        {
                            month = _monthlist[0].ID;
                            minday = _monthlist[0].BeginDate;
                            maxday = DateTime.Today < _monthlist[0].EndDate ? DateTime.Today : _monthlist[0].EndDate;
                        }
                        else
                        {
                            errormessage += "会计月错误;\r\n";
                            row.GetCell(column).SetCellValue(errormessage);
                            continue;
                        }
                    }
                    #region 组织销量头
                    SVM_SalesVolumeBLL bll = null;
                    string conditon = "";

                    conditon = "Supplier=" + clientid.ToString()
                  + "AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',4)='7' AND Type=3 AND AccountMonth=" + month.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                  + " AND Flag=1 AND ISNULL(Promotor,0)=" + promotorid.ToString();

                    IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList(conditon);
                    if (svmlists.Count > 0)
                    {
                        if (svmlists.FirstOrDefault(p => p.ApproveFlag == 1) != null)
                        {
                            errormessage += "ID号：" + row.GetCell(0).ToString() + "," + client.FullName
                                           + "，导购:" + row.GetCell(6).ToString() + "  当月的销量单" + "已审核，不可再次导入！\r\n";
                            row.GetCell(column).SetCellValue(errormessage);
                            continue;
                        }
                        if (svmlists.Count == 1)
                        {
                            bll = new SVM_SalesVolumeBLL(svmlists[0].ID);
                            bll.Items.Clear();
                        }
                    }
                    if (bll == null)
                    {
                        bll = new SVM_SalesVolumeBLL();

                        bll.Model.Client = 0;
                        bll.Model.Supplier = client.ID;
                        bll.Model.Promotor = promotorid;
                        bll.Model.Type = 3;


                        bll.Model.OrganizeCity = client.OrganizeCity;
                        bll.Model.AccountMonth = month;
                        bll.Model.SalesDate = maxday;
                        bll.Model.ApproveFlag = 2;
                        bll.Model.Flag = 1;             //成品销售                 
                        bll.Model["IsCXP"] = "N";
                        bll.Model.InsertStaff = 1;
                        bll.Model.Remark = "线下补录导入";

                    }
                    #endregion
                    bll.Model["SubmitFlag"] = "1";
                    bll.Model["DataSource"] = "7";
                    IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
                    bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                    int quantity = 0;
                    bool isnumber = false;
                    while (true)
                    {
                      
                        PDT_Product product = null;
                        quantity = 0;

                        if (headerRow.GetCell(datacolumn) == null||headerRow.GetCell(datacolumn).CellType == CellType.BLANK || headerRow.GetCell(datacolumn).ToString() == string.Empty)
                        {                           
                            break;
                        }

                        IList<PDT_Product> products = PDT_ProductBLL.GetModelList("ShortName='" + headerRow.GetCell(datacolumn).ToString() + "' AND State=1");
                        if (products.Count > 0)
                        {
                            product = products[0];
                        }
                        else
                        {
                            errormessage += "产品名称：" + headerRow.GetCell(datacolumn).ToString() + "在产品列表中不存在！\r\n";
                            datacolumn++;
                            row.GetCell(column).SetCellValue(errormessage);
                            continue;
                        }

                        if ((product != null) && row.GetCell(datacolumn).CellType != CellType.BLANK)
                        {
                            int.TryParse(row.GetCell(datacolumn).ToString(), out quantity);
                            if (int.TryParse(row.GetCell(datacolumn).ToString(), out quantity) && !isnumber)
                            {
                                isnumber = true;
                            }
                            if (quantity != 0 && quantity <= 5000 && quantity >= 0)
                            {
                           
                                decimal factoryprice = 0, salesprice = 0;
                                PDT_ProductPriceBLL.GetPriceByClientAndType(client.ID, product.ID, 3, out factoryprice, out salesprice);

                                if (factoryprice == 0) factoryprice = product.FactoryPrice;
                                if (salesprice == 0) salesprice = product.NetPrice;

                                SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
                                detail.Product = product.ID;
                                detail.FactoryPrice = factoryprice;
                                detail.SalesPrice = salesprice;
                                detail.Quantity = quantity;
                                details.Add(detail); 
                            }
                            else if (row.GetCell(datacolumn).CellType != CellType.BLANK && (row.GetCell(datacolumn).ToString() != "0"))
                            {
                                wrongflag = true;
                                break;
                            }
                            else if (quantity < 0)
                            {
                                wrongflag = true;
                                break;
                            }
                        }
                        datacolumn++;

                    }
                    if (wrongflag)
                    {
                        errormessage += "ID号：" + clientid.ToString() + "，" + client.FullName
                                     + "，导购:" + row.GetCell(6).ToString() + "  当月的线下补录销量单"
                                          + "未能导入！产品名称：" + headerRow.GetCell(datacolumn).ToString() + "数量填写错误。\r\n";
                        row.GetCell(column).SetCellValue(errormessage);
                        continue;
                    }

                    #region 更新销量至数据库
                    if (bll.Model.ID > 0)
                    {
                        if (details.Count > 0)
                        {
                            bll.DeleteDetail();     //先清除原先导入的数据
                            bll.Items = details;
                            bll.Model.UpdateStaff = 1;
                            bll.AddDetail();
                            bll.Update();

                            string message = " ID号：" + clientid.ToString() + "，该零售商：" + client.FullName
                                + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + "的销量单" + "被成功更新！产品SKU数："
                          + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "\r\n";
                            improtmessage += message; 
                            row.CreateCell(column).SetCellValue(message);
                        }
                        if (details.Count == 0 && isnumber)
                        {
                            bll.DeleteDetail();
                        }


                    }
                    else
                    {
                        if (details.Count > 0 || svmlists.Count == 0)    //没有产品也新增一条空销量头
                        {
                            bll.Items = details;
                            if (bll.Add() > 0)
                            {
                                foreach (SVM_SalesVolume m in svmlists)
                                {
                                    bll = new SVM_SalesVolumeBLL(m.ID);
                                    bll.DeleteDetail();
                                    bll.Delete();
                                }
                            }
                            string message = "ID号：" + clientid.ToString() + "，该零售商：" + client.FullName
                          + "的销量单" + "已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                         + bll.Items.Sum(p => p.Quantity).ToString() + "\r\n";
                            improtmessage += message; 
                            row.CreateCell(column).SetCellValue(message);
                        }
                    }
                    #endregion
                    ((BackgroundWorker)sender).ReportProgress(i * 100 / count, i);
                }

            }
            catch (Exception ex)
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
           

            label2.Text = "正在处理EXCEL文件，请稍后... 已处理" + progressBar1.Value + "%,第" + (int.Parse(e.UserState.ToString())).ToString() + "条";
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
    }
}