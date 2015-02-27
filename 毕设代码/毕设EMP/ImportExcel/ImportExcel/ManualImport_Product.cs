using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using System.Diagnostics;
using System.IO;

namespace ImportExcel
{
    public partial class ManualImport_Product : Form
    {
        public ManualImport_Product()
        {
            InitializeComponent();
        }

        private void bt_OpenFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                tbx_FileName.Text = openFileDialog1.FileName;
            }

        }

        private void bt_Import_Click(object sender, EventArgs e)
        {
            if (tbx_FileName.Text != "" && File.Exists(tbx_FileName.Text))
            {

                ImportProduct(tbx_FileName.Text);
            }
        }

        #region 导入成品

        private void ImportProduct(string path)
        {
            string message = "";
            string improtmessage = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth() - 1).Model;
            DateTime minday = month.BeginDate;
            DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;



            int _State = 0;
            #region 读取Excel文件
            string ErrorInfo = "";
            object missing = System.Reflection.Missing.Value;
            ApplicationClass ExcelApp = null;
            try
            {
                ExcelApp = new ApplicationClass();
                ExcelApp.Visible = false;
                ExcelApp.DisplayAlerts = false;

                Workbook workbook1 = null;
                Worksheet worksheet1 = null;

                try
                {
                    workbook1 = ExcelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    #region 验证工作表数据格式

                    worksheet1 = (Worksheet)workbook1.Worksheets[1];

                    if (worksheet1.Name != "零售商销货")
                    {
                        MessageBox.Show("Excel表格中第1个工作表名称必须为【零售商销货】！");
                        goto End;
                    }

                    if (!VerifyWorkSheet(worksheet1, 7))
                    {
                        MessageBox.Show("零售商销货工作表表头(1~7列)错误!");
                        goto End;
                    }

                    IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1') AND State=1 AND ApproveFlag=1   ORDER BY ISNULL(SubUnit,999999),Code");
                    for (int i = 0; i < productlists.Count; i++)
                    {

                        if (((Range)worksheet1.Cells[1, 8 + i]).Text.ToString() != productlists[i].ShortName)
                        {
                            MessageBox.Show("零售商进货工作表表头，(" + (8 + i).ToString() + "列)产品名称错误！必须为:" + productlists[i].ShortName);
                            goto End;
                        }
                    }
                    #endregion


                    improtmessage += DoImportProduct(worksheet1, month.ID, 1, 8, productlists, out _State);


                End:
                    ;
                }
                catch (ThreadAbortException exception3)
                {
                    return;
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "\r\n" + "Source:" + err.Source + "\r\n" +
                        "StackTrace:" + err.StackTrace + "\r\n";

                    MessageBox.Show(error);
                }
                finally
                {
                    if (workbook1 != null) workbook1.Close(false, missing, missing);

                    if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                    if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                    worksheet1 = null;
                    workbook1 = null;
                }
            }
            catch (ThreadAbortException exception3)
            {

                return;
            }
            catch (System.Exception err)
            {
                MessageBox.Show("系统错误-5!" + err.Message);
            }
            finally
            {
                if (ExcelApp != null)
                {
                    try
                    {
                        ExcelApp.Workbooks.Close();
                        ExcelApp.Quit();

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                        ExcelApp = null;
                    }
                    catch (System.Exception err)
                    {
                        string error = "Message:" + err.Message + "\r\n" + "Source:" + err.Source + "\r\n" +
                            "StackTrace:" + err.StackTrace + "\r\n";

                        MessageBox.Show(error + "系统错误-6,Excel宏报错，请确认文件打开不报错再上传!" + err.Message);
                        KillProcess();
                    }

                }
                GC.Collect();
                //GC.WaitForPendingFinalizers();

                if (ErrorInfo != "")
                {
                    MessageBox.Show("对不起，Excel文件打开错误，请确认格式是否正确。错误提示:" + ErrorInfo);
                }



            }
            #endregion

            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            //MessageBox.Show("导入成品进销", message != "" ? filename + "-" + message : filename + "导入操作成功！");

        }

        private string DoImportProduct(Worksheet worksheet, int accountmonth, int insertstaff, int cloumn, IList<PDT_Product> productlists, out int State)
        {
            string ImportInfo = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(accountmonth).Model;
            DateTime minday = month.BeginDate;
            DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;

            State = 0;

            tbx_Msg.AppendText("\r\n");
            tbx_Msg.AppendText("-------------------------------------------------------------------------\r\n");
            tbx_Msg.AppendText("----批量导入客户进货----\r\n+----批量导入客户销量----\r\nID号      门店类型       门店名     导购员       错误原因\r\n");


            #region 读取Excel表格
            int row = 1;
            int emptyrow = 0;
            int _cloumn = 0;
            while (true)
            {
                _cloumn = cloumn;
                row++;
                if (((Range)worksheet.Cells[row, 1]).Value2 == null)
                {
                    emptyrow++;
                    if (emptyrow > 5)
                        break;
                    else
                        continue;
                }

                int clientid = 0; int promotorid = 0;
                if (!int.TryParse(((Range)worksheet.Cells[row, 1]).Value2.ToString(), out clientid))
                {
                    continue;
                }

                if (cloumn == 8 && ((Range)worksheet.Cells[row, 6]).Value2 != null &&
                        !int.TryParse(((Range)worksheet.Cells[row, 6]).Value2.ToString(), out promotorid))
                {
                    continue;
                }

                #region 验证数据

                CM_Client client = new CM_ClientBLL(clientid).Model;

                if (client == null || client.FullName != ((Range)worksheet.Cells[row, 3]).Text.ToString())
                {
                    tbx_Msg.AppendText("ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，零售商ID与零售商名称" : "，分销商ID与分销商名称") + "不匹配！\r\n");
                    State = 4;
                    continue;
                }
                int uplimit = client.ClientType == 3 ? 5000 : 9000;
                if (((Range)worksheet.Cells[row, 5]).Text.ToString() != month.Name)
                {
                    tbx_Msg.AppendText("ID号：" + clientid.ToString() + "，归属月份必须为【" + month.Name + "】\r\n");
                    State = 4;
                    continue;
                }
                #endregion

                #region 组织销量头
                SVM_SalesVolumeBLL bll = null;
                string conditon = "";
                if (cloumn == 6)
                {
                    conditon = "Client=" + clientid.ToString()
                       + " AND Type IN(1,2) AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                       + " AND Flag=1 "
                       + " AND Remark='" + tbx_Remark.Text + "'";
                }
                else
                {
                    conditon = "Supplier=" + clientid.ToString()
                  + " AND Type=3 AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                  + " AND Flag=1 AND ISNULL(Promotor,0)=" + promotorid.ToString()
                  + " AND Remark='" + tbx_Remark.Text + "'";

                }
                IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList(conditon);
                if (svmlists.Count > 0)
                {
                    tbx_Msg.AppendText("行号:" + row.ToString() + ",ID号：" + ((Range)worksheet.Cells[row, 1]).Text.ToString() + client.FullName + "  当月的销量单已导入！\r\n");
                    continue;
                }

                if (bll == null)
                {
                    bll = new SVM_SalesVolumeBLL();
                    if (cloumn == 8)
                    {
                        bll.Model.Client = 0;
                        bll.Model.Supplier = client.ID;
                        bll.Model.Promotor = promotorid;
                        bll.Model.Type = 3;
                    }
                    else
                    {
                        bll.Model.Client = clientid;
                        bll.Model.Supplier = client.Supplier;
                        bll.Model.Type = 2;
                    }

                    bll.Model.OrganizeCity = client.OrganizeCity;
                    bll.Model.AccountMonth = month.ID;
                    bll.Model.SalesDate = maxday;
                    bll.Model.ApproveFlag = 2;
                    bll.Model.Flag = 1;             //成品销售                 
                    bll.Model["IsCXP"] = "N";
                    bll.Model.InsertStaff = insertstaff;
                    bll.Model.Remark = tbx_Remark.Text;
                    bll.Model["DataSource"] = tbx_DataSource.Text;      //6:导购销售拆分 7:补录销量
                }
                #endregion
                bll.Model["SubmitFlag"] = "1";

                #region 读取各产品销量
                IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
                bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                int quantity = 0;
                foreach (PDT_Product product in productlists)
                {

                    if (((Range)worksheet.Cells[row, _cloumn]).Value2 != null)
                    {
                        int.TryParse(((Range)worksheet.Cells[row, _cloumn]).Value2.ToString(), out quantity);
                        if (quantity != 0 && quantity <= uplimit && quantity >= 0)
                        {
                            decimal factoryprice = 0, salesprice = 0;
                            PDT_ProductPriceBLL.GetPriceByClientAndType(client.ID, product.ID, cloumn == 8 ? 3 : 2, out factoryprice, out salesprice);

                            if (factoryprice == 0) factoryprice = product.FactoryPrice;
                            if (salesprice == 0) salesprice = product.NetPrice;

                            SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
                            detail.Product = product.ID;
                            detail.FactoryPrice = factoryprice;
                            detail.SalesPrice = salesprice;
                            detail.Quantity = quantity;
                            details.Add(detail);
                        }
                        else if (trim(((Range)worksheet.Cells[row, _cloumn]).Text.ToString()) != "" && ((Range)worksheet.Cells[row, _cloumn]).Text.ToString() != "0")
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
                    _cloumn++;
                }
                if (wrongflag)
                {
                    tbx_Msg.AppendText("行号:" + row.ToString() + ",ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，" + ((Range)worksheet.Cells[row, 4]).Text + ":" : "，分销商：") + client.FullName
                                      + (client.ClientType == 3 ? "" : "，该分销商") + (cloumn == 6 ? "当月的进货单" : "，导购:" + ((Range)worksheet.Cells[row, 7]).Text.ToString() + "  当月的销量单")
                                      + "未能导入！产品名称：" + ((Range)worksheet.Cells[1, _cloumn]).Text + "数量填写错误。\r\n");
                    State = 4;
                    continue;
                }
                #endregion

                #region 更新销量至数据库
                if (bll.Model.ID > 0)
                {
                    //if (details.Count > 0)
                    //{
                    //    bll.DeleteDetail();     //先清除原先导入的数据
                    //    bll.Items = details;
                    //    bll.Model.UpdateStaff = insertstaff;
                    //    bll.AddDetail();
                    //    bll.Update();

                    //    tbx_Msg.AppendText("<span style='color:Blue'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，该零售商：" : "，该分销商：") + client.FullName
                    //        + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + (cloumn == 6 ? " 的进货单" : "的销量单") + "被成功更新！产品SKU数："
                    //  + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "\r\n");
                    //}
                }
                else
                {
                    if (details.Count > 0)    //没有产品也新增一条空销量头
                    {
                        bll.Items = details;
                        if (bll.Add() > 0)
                        {

                            tbx_Msg.AppendText("行号:" + row.ToString() + ",ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，该零售商：" : "，该分销商：") + client.FullName
                          + (cloumn == 6 ? " 的进货单" : "的销量单") + "已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                         + bll.Items.Sum(p => p.Quantity).ToString() + "\r\n");

                            tbx_Msg.Focus();
                            tbx_Msg.Select(tbx_Msg.TextLength, 0);
                            tbx_Msg.ScrollToCaret();
                        }
                    }
                }
                #endregion
            }
            #endregion

            return ImportInfo;
        }

        private bool VerifyWorkSheet(Worksheet worksheet, int cloumn)
        {
            bool flag = true;
            if (((Range)worksheet.Cells[1, 1]).Text.ToString() != "零售商ID" ||
                      ((Range)worksheet.Cells[1, 2]).Text.ToString() != "零售商编号" ||
                      ((Range)worksheet.Cells[1, 3]).Text.ToString() != "零售商名称" ||
                      ((Range)worksheet.Cells[1, 4]).Text.ToString() != "零售商分类" ||
                      ((Range)worksheet.Cells[1, 5]).Text.ToString() != "归属月份" ||
                      cloumn == 7 && (((Range)worksheet.Cells[1, 6]).Text.ToString() != "导购ID" ||
                        ((Range)worksheet.Cells[1, 7]).Text.ToString() != "导购姓名"))
            {

                flag = false;
            }
            if (((Range)worksheet.Cells[1, 1]).Text.ToString() == "" ||
                     ((Range)worksheet.Cells[1, 2]).Text.ToString() == "" ||
                     ((Range)worksheet.Cells[1, 3]).Text.ToString() == "" ||
                     ((Range)worksheet.Cells[1, 4]).Text.ToString() == "" ||
                     ((Range)worksheet.Cells[1, 5]).Text.ToString() == "")
            {

                flag = true;
            }
            return flag;
        }
        public string trim(string Str)
        {
            string newstr = Str.Replace("　", "");
            newstr = newstr.Trim();
            return newstr;
        }
        private void KillProcess()
        {
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName("EXCEL"))
                {
                    thisproc.Kill();
                }
            }
            catch
            {

            }
        }
        #endregion


    }
}
