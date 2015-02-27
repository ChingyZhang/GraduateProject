using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using MCSFramework.Model.Promotor;

namespace ImportExcel
{
    class ImportExcelSVM
    {
        private Thread th_DownService;
        private Thread th_ImportService;
        private Thread th_HeartService;

        private bool bIsRunning = false;
        private DateTime dtHeartTime = DateTime.Now;
        private int ImportTemplateID = 0;   //当前正在导入处理的EXCEL数据表ID
        private MessageHandel _message;

        public event MessageHandel OnMessage
        {
            add
            {
                _message += value;
            }
            remove
            {
                _message -= value;
            }
        }

        public bool IsRunning
        {
            get { return bIsRunning; }
        }


        public void Start()
        {
            bIsRunning = true;
            th_DownService = new Thread(new ThreadStart(DownloadTemplate));
            th_DownService.Start();

            th_ImportService = new Thread(new ThreadStart(UploadExcel));
            th_ImportService.Start();

            th_HeartService = new Thread(new ThreadStart(ThreadCheckHeartTime));
            th_HeartService.Start();

            SendMessage("", "导入服务成功发起！");
        }

        public void Stop()
        {
            bIsRunning = false;
            Thread.Sleep(1000);

            if (th_HeartService != null && th_HeartService.ThreadState == System.Threading.ThreadState.Running)
            {
                th_HeartService.Abort();
                th_HeartService.Join();
            }
            if (th_DownService != null && th_DownService.ThreadState == System.Threading.ThreadState.Running)
            {
                th_DownService.Abort();
                th_DownService.Join();
            }
            if (th_ImportService != null && th_ImportService.ThreadState == System.Threading.ThreadState.Running)
            {
                th_ImportService.Abort();
                th_ImportService.Join();
            }
            SendMessage("", "导入服务成功停止！");
        }

        //构造方法
        public ImportExcelSVM()
        {

        }


        private void SendMessage(string task, string mess)
        {
            if (_message != null)
            {
                MessageEventArgs e = new MessageEventArgs(task, mess);
                _message.Invoke(this, e);
            }
        }

        public class MessageEventArgs : EventArgs
        {
            private string _Task;
            private string _Message;

            public string Task
            {
                get { return _Task; }
            }

            public string Message
            {
                get { return _Message; }
            }

            public MessageEventArgs(string task, string message)
            {
                _Task = task;
                _Message = message;
            }
        }

        /// <summary>
        /// 下载模版
        /// </summary>
        public void DownloadTemplate()
        {
            while (bIsRunning)
            {
                try
                {
                    #region 获取最迟的销量月份
                    int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
                    AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;

                    #endregion
                    IList<SVM_DownloadTemplate> dwonlists = SVM_DownloadTemplateBLL.GetModelList("AccountMonth=" + month.ID + " AND State=1");
                    if (dwonlists.Count == 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }
                    foreach (SVM_DownloadTemplate m in dwonlists)
                    {
                        string path = ConfigHelper.GetConfigString("AttachmentPath");
                        if (!path.EndsWith("\\")) path = path + "\\";
                        if (m.IsOpponent != 2 && m.IsOpponent != 3)
                        {
                            path += "ImportExcelSVM\\Download\\" + m["UserName"] + "\\";
                        }
                        else
                        {
                            path += "ImportExcelSVM\\Download\\" + m.AccountMonth.ToString() + "\\";
                        }
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string filename = m.Name;
                        path = path + filename;
                        switch (m.IsOpponent)
                        {
                            case 1:
                                this.CreateDownTemplate(m.ID, path);
                                break;

                            case 2:
                            case 3:
                                this.CreateKeyTargetDownTemplate(m.ID, path);
                                break;

                            case 9:
                                this.CrateGiftDownTemplate(m.ID, path);
                                break;
                        }

                    }
                }
                catch (ThreadAbortException e)
                {
                    this.SendMessage("ThreadAbortException", e.ToString());
                    break;
                }
            }
        }


        public void UploadExcel()
        {
            while (bIsRunning)
            {
                try
                {

                    #region 获取最迟的销量月份
                    int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
                    AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;
                    #endregion
                    IList<SVM_UploadTemplate> uplists = SVM_UploadTemplateBLL.GetModelList("AccountMonth=" + month.ID + " AND State=1");
                    if (uplists.Count == 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }

                    foreach (SVM_UploadTemplate m in uplists)
                    {
                        dtHeartTime = DateTime.Now;
                        ImportTemplateID = m.ID;

                        try
                        {
                            #region 组织文件路径及文件名
                            string path = ConfigHelper.GetConfigString("AttachmentPath");
                            if (!path.EndsWith("\\")) path = path + "\\";
                            if (m.IsOpponent != 2 && m.IsOpponent != 3)
                            {
                                path += "ImportExcelSVM\\Upload\\" + m["UserName"] + "\\";
                            }
                            else
                            {
                                path += "ImportExcelSVM\\Upload\\" + m.AccountMonth.ToString() + "\\";
                            }

                            string filename = m.Name;
                            path += filename;
                            #endregion
                            SendMessage("当前处理记录", m.Name);
                            switch (m.IsOpponent)
                            {
                                case 1:
                                    this.ImportProduct(m.ID, path);
                                    break;
                                case 2:
                                case 3:
                                    this.ImportKeyProduct(m.ID, path);
                                    break;
                                case 9:
                                    this.ImportGiftProduct(m.ID, path);
                                    break;
                            }
                            this.ImportTemplateID = 0;

                        }
                        catch (System.Exception err)
                        {
                            SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(m.ID);
                            _bll.Model.State = 3;
                            _bll.Model.Remark = "导入出错，异常：" + err.Message;
                            _bll.Model.ImprotTime = DateTime.Now;
                            _bll.Update();
                            this.ImportTemplateID = 0;
                        }
                    }
                }
                catch (ThreadAbortException e)
                {
                    this.SendMessage("ThreadAbortException", this.ImportTemplateID.ToString() + e.ToString());
                    break;
                }

            }
        }


        public string trim(string Str)
        {
            string newstr = Str.Replace("　", "");
            newstr = newstr.Trim();
            return newstr;
        }
        #region 导入赠品
        private void ImportGiftProduct(int templateid, string path)
        {
            SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(templateid);
            if (_bll.Model.State != 1) return;//模版作废
            string message = "";
            string improtmessage = "";
            #region 读取Excel文件
            string ErrorInfo = "";

            int _State = 0;
            object missing = System.Reflection.Missing.Value;
            ApplicationClass ExcelApp = null;
            try
            {
                ExcelApp = new ApplicationClass();
                ExcelApp.Visible = false;
                ExcelApp.DisplayAlerts = false;

                Workbook workbook1 = null;
                Worksheet worksheet1 = null, worksheet2 = null, worksheet3 = null;

                try
                {
                    workbook1 = ExcelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                    #region 验证工作表数据格式
                    if (workbook1.Worksheets.Count != 3)
                    {
                        message += "Excel表格中必须且只能有3张工作表！";
                        goto End;
                    }

                    worksheet1 = (Worksheet)workbook1.Worksheets[1];
                    worksheet2 = (Worksheet)workbook1.Worksheets[2];
                    worksheet3 = (Worksheet)workbook1.Worksheets[3];

                    if (worksheet1.Name != "成品赠品")
                    {
                        ErrorInfo += "Excel表格中第1个工作表名称必须为【成品赠品】！";
                        goto End;
                    }
                    if (worksheet2.Name != "试用装")
                    {
                        ErrorInfo += "Excel表格中第2个工作表名称必须为【试用装】！";
                        goto End;
                    }

                    if (worksheet3.Name != "高值促销品")
                    {
                        ErrorInfo += "Excel表格中第3个工作表名称必须为【高值促销品】！";
                        goto End;
                    }
                    if (_bll.Model["ClientType"] == "2")
                    {
                        if (!VerifyDIWorkSheet(worksheet1))
                        {
                            ErrorInfo += "【成品赠品】工作表表头(1~5列)错误!";
                            goto End;
                        }
                        if (!VerifyDIWorkSheet(worksheet2))
                        {
                            ErrorInfo += "【试用装】工作表表头(1~5列)错误!";
                            goto End;
                        }

                        if (!VerifyDIWorkSheet(worksheet3))
                        {
                            ErrorInfo += "【高值促销品】工作表表头(1~5列)错误!";
                            goto End;
                        }
                    }
                    else
                    {
                        if (!VerifyWorkSheet(worksheet1, 5))
                        {
                            ErrorInfo += "【成品赠品】工作表表头(1~5列)错误!";
                            goto End;
                        }

                        if (!VerifyWorkSheet(worksheet2, 5))
                        {
                            ErrorInfo += "【试用装】工作表表头(1~5列)错误!";
                            goto End;
                        }

                        if (!VerifyWorkSheet(worksheet3, 5))
                        {
                            ErrorInfo += "【高值促销品】工作表表头(1~5列)错误!";
                            goto End;
                        }
                    }
                    #endregion

                    #region 读取Excel表格
                    IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='9') AND State=1 AND ApproveFlag=1 ORDER BY ISNULL(SubUnit,999999),Code");
                    AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
                    improtmessage += ImportGifts(worksheet1, "Excel批量导入本品赠品", month, _bll.Model.InsertStaff, productlists, out _State);
                    if (_State != 0) _bll.Model.State = _State;
                    improtmessage += ImportGifts(worksheet2, "Excel批量导入试用装赠品", month, _bll.Model.InsertStaff, productlists, out _State);
                    if (_State != 0) _bll.Model.State = _State;
                    improtmessage += ImportGifts(worksheet3, "Excel批量导入高值促销品", month, _bll.Model.InsertStaff, productlists, out _State);
                    if (_State != 0) _bll.Model.State = _State;
                    #endregion
                End:
                    ;
                }
                catch (ThreadAbortException exception3)
                {
                    return;
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                        "StackTrace:" + err.StackTrace + "<br/>";

                    message += "系统错误-4!" + err.Message;
                }
                finally
                {
                    if (workbook1 != null) workbook1.Close(false, missing, missing);

                    if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                    if (worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);
                    if (worksheet3 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet3);
                    if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                    worksheet1 = null;
                    worksheet2 = null;
                    worksheet3 = null;
                    workbook1 = null;
                }
            }
            catch (ThreadAbortException exception3)
            {

                return;
            }
            catch (System.Exception err)
            {
                string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                    "StackTrace:" + err.StackTrace + "<br/>";
                message += "系统错误-5!" + err.Message;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    try
                    {
                        ExcelApp.Workbooks.Close();
                        ExcelApp.Quit();
                    }
                    catch (System.Exception err)
                    {
                        string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                            "StackTrace:" + err.StackTrace + "<br/>";
                        message += "系统错误-6,Excel宏报错，请确认文件打开不报错再上传!" + err.Message;
                        KillProcess();
                    }

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                    ExcelApp = null;
                }
                GC.Collect();

                if (ErrorInfo != "")
                {
                    message += "对不起，Excel文件打开错误，请确认格式是否正确。错误提示:" + ErrorInfo;
                }

            }
            if (_bll.Model.State == 1)
                _bll.Model.State = message != "" ? 3 : 2;
            _bll.Model.ImprotTime = DateTime.Now;
            _bll.Model.Remark = message != "" ? message : improtmessage;
            _bll.Update();
            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            SendMessage("导入赠品进货", message != "" ? filename + "-" + message : filename + "导入操作成功！");
            #endregion
        }

        private string ImportGifts(Worksheet worksheet, string remark, AC_AccountMonth month, int insertstaff, IList<PDT_Product> productlists, out int State)
        {
            string ImportInfo = "";
            int cloumn = 6;
            int sellinrow = 1;
            DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;

            ImportInfo += "<br/>";
            ImportInfo += "<span style='color: Red'>-------------------------------------------------------------------------</span><br/>";
            ImportInfo += "<span style='color: Red'>----" + remark + "----</span><br/>";

            State = 0;
            PDT_Product product;

            #region 读取Excel表格
            while (true)
            {

                cloumn = 6;
                sellinrow++;

                if (((Range)worksheet.Cells[sellinrow, 1]).Value2 == null)
                {
                    break;
                }

                int clientid = 0;
                if (!int.TryParse(((Range)worksheet.Cells[sellinrow, 1]).Value2.ToString(), out clientid))
                {
                    continue;
                }
                #region 验证数据
                CM_Client client = new CM_ClientBLL(clientid).Model;
                if (client == null || client.FullName != ((Range)worksheet.Cells[sellinrow, 3]).Text.ToString())
                {

                    ImportInfo += "<span style='color: Red'>ID号：" + ((Range)worksheet.Cells[sellinrow, 1]).Text.ToString() + (client.ClientType == 3 ? "，零售商ID与零售商名称" : "，分销商ID与分销商名称") + "不匹配！</span><br/>";

                    State = 4;
                    continue;
                }
                int uplimit = client.ClientType == 3 ? 5000 : 9000;
                if (((Range)worksheet.Cells[sellinrow, 5]).Text.ToString() != month.Name)
                {
                    ImportInfo += "<span style='color: Red'>ID号：" + ((Range)worksheet.Cells[sellinrow, 1]).Text.ToString() + "，归属月份必须为【" + month.Name + "】</span><br/>";
                    State = 4;
                    continue;
                }
                #endregion

                #region 组织销量头
                SVM_SalesVolumeBLL bll = null;
                IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList("Client=" + clientid.ToString() + " AND InsertStaff!=1"
                    + " AND Type IN(1,2) AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                    + " AND Flag=11 AND (Remark='" + remark + "' OR Remark NOT IN ('Excel批量导入本品赠品','Excel批量导入试用装赠品','Excel批量导入高值促销品'))");
                if (svmlists.Count > 0)
                {
                    if (svmlists.FirstOrDefault(p => p.ApproveFlag == 1) != null)
                    {
                        ImportInfo += "<span style='color: Red'>ID号：" + ((Range)worksheet.Cells[sellinrow, 1]).Text.ToString() + (client.ClientType == 3 ? "，该零售商" : "，该分销商") + "当月的本品赠品进货已审核，不可再次导入！</span><br/>";
                        continue;
                    }
                    //仅存在一条符合条件的赠品,进行覆盖
                    if (svmlists.Count == 1 && svmlists[0].Remark == remark)
                    {
                        bll = new SVM_SalesVolumeBLL(svmlists[0].ID);
                        bll.Items.Clear();
                    }
                }
                if (bll == null)
                {
                    bll = new SVM_SalesVolumeBLL();
                    bll.Model.Client = clientid;
                    bll.Model.OrganizeCity = client.OrganizeCity;
                    bll.Model.Supplier = client.Supplier;
                    bll.Model.AccountMonth = month.ID;
                    bll.Model.SalesDate = maxday;
                    bll.Model.Type = 2;
                    bll.Model.ApproveFlag = 2;
                    bll.Model.Flag = 11;             //赠品进货                  
                    bll.Model["IsCXP"] = "Y";
                    bll.Model.InsertStaff = insertstaff;
                    bll.Model.Remark = remark;
                }
                #endregion

                bll.Model["SubmitFlag"] = "1";

                IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
                int quantity = 0;
                bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                bool isnumber = false;
                while (true)
                {

                    if (((Range)worksheet.Cells[1, cloumn]).Text.ToString() == string.Empty)
                    {
                        break;
                    }

                    IList<PDT_Product> products = PDT_ProductBLL.GetModelList("ShortName='" + ((Range)worksheet.Cells[1, cloumn]).Text + "' AND State=1");
                    if (products.Count > 0)
                    {
                        product = products[0];
                    }
                    else
                    {
                        ImportInfo += "<span style='color: Red'>赠品产品名称：" + ((Range)worksheet.Cells[1, cloumn]).Text + "在赠品列表中不存在！</span><br/>";
                        State = 4;
                        cloumn++;
                        continue;
                    }
                    if (product != null && ((Range)worksheet.Cells[sellinrow, cloumn]).Value2 != null)
                    {
                        #region 读取各产品销量

                        if (!isnumber && int.TryParse(((Range)worksheet.Cells[sellinrow, cloumn]).Value2.ToString(), out quantity))
                        {
                            isnumber = true;
                        }
                        if (quantity != 0 && quantity <= uplimit && quantity >= 0)
                        {
                            decimal factoryprice = 0, salesprice = 0;
                            PDT_ProductPriceBLL.GetPriceByClientAndType(client.ID, product.ID, 2, out factoryprice, out salesprice);

                            if (factoryprice == 0) factoryprice = product.FactoryPrice;
                            if (salesprice == 0) salesprice = product.NetPrice;

                            SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
                            detail.Product = product.ID;
                            detail.FactoryPrice = factoryprice;
                            detail.SalesPrice = salesprice;
                            detail.Quantity = quantity;
                            details.Add(detail);
                        }
                        else if (trim(((Range)worksheet.Cells[sellinrow, cloumn]).Text.ToString()) != "" && ((Range)worksheet.Cells[sellinrow, cloumn]).Text.ToString() != "0")
                        {
                            wrongflag = true;
                            break;
                        }
                        #endregion
                    }
                    cloumn++;
                }
                if (wrongflag)
                {
                    ImportInfo += "<span style='color: Red'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，零售商" : "，分销商：") + client.FullName
                                       + "的赠品进货单未能导入，赠品名称：" + ((Range)worksheet.Cells[1, cloumn]).Text + "数量填写错误";
                    State = 4;
                    continue;
                }
                #region 更新销量至数据库
                if (bll.Model.ID > 0)
                {
                    if (details.Count > 0)
                    {
                        bll.DeleteDetail();     //先清除原先导入的数据
                        bll.Model.UpdateStaff = insertstaff;
                        bll.Items = details;
                        bll.AddDetail();
                        bll.Update();
                        ImportInfo += "<span style='color: Blue'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，零售商：" : "，分销商：") + client.FullName
                      + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + " 的赠品进货单被成功更新！产品SKU数："
                      + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";

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
                        ImportInfo += "<span style='color: Black'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，零售商：" : "，分销商：") + client.FullName
                      + " 的赠品进货单已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                      + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                    }
                }

                #endregion
            }
            #endregion

            return ImportInfo;
        }
        #endregion

        #region 导入成品
        private void ImportProduct(int templateid, string path)
        {
            SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(templateid);
            if (_bll.Model.State != 1) return;//模版作废
            string message = "";
            string improtmessage = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
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
                Worksheet worksheet1 = null, worksheet2 = null;

                try
                {
                    workbook1 = ExcelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    #region 验证工作表数据格式
                    switch (_bll.Model["ClientType"])
                    {
                        case "2":
                            if (workbook1.Worksheets.Count != 1)
                            {
                                ErrorInfo += "Excel表格中必须且只能有1张工作表,请并检查宏是否有问题！";
                                goto End;
                            }
                            break;
                        case "3":
                            if (workbook1.Worksheets.Count != 2)
                            {
                                ErrorInfo += "Excel表格中必须且只能有2张工作表，请并检查宏是否有问题！";
                                goto End;
                            }
                            break;
                    }
                    worksheet1 = (Worksheet)workbook1.Worksheets[1];
                    if (_bll.Model["ClientType"] == "2")
                    {
                        if (worksheet1.Name != "分销商进货")
                        {
                            ErrorInfo += "Excel表格中第1个工作表名称必须为【分销商进货】！";
                            goto End;
                        }
                        if (!VerifyDIWorkSheet(worksheet1))
                        {

                            ErrorInfo += "Excel表格中第1个工作表名称必须为【零售商进货】！";
                            goto End;
                        }

                    }
                    else
                    {
                        worksheet2 = (Worksheet)workbook1.Worksheets[2];
                        if (worksheet1.Name != "零售商进货")
                        {
                            ErrorInfo += "Excel表格中第1个工作表名称必须为【零售商进货】！";
                            goto End;
                        }
                        if (worksheet2.Name != "零售商销货")
                        {
                            ErrorInfo += "Excel表格中第2个工作表名称必须为【零售商销货】！";
                            goto End;
                        }

                        if (!VerifyWorkSheet(worksheet1, 5))
                        {
                            ErrorInfo += "零售商进货工作表表头(1~5列)错误!";
                            goto End;
                        }

                        if (!VerifyWorkSheet(worksheet2, 7))
                        {
                            ErrorInfo += "零售商销货工作表表头(1~7列)错误!";
                            goto End;
                        }


                    }

                    string ParamValue = Addr_OrganizeCityParamBLL.GetValueByType(1, 26);
                    ParamValue = string.IsNullOrEmpty(ParamValue) ? "0" : ParamValue;
                    ParamValue = ParamValue.EndsWith(",") ? ParamValue.Remove(ParamValue.Length - 1) : ParamValue;

                    IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',28)!='1' AND Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1')  AND State=1 AND ApproveFlag=1   ORDER BY ISNULL(SubUnit,999999),Code");

                    if (_bll.Model["ClientType"] == "3")
                    {
                        productlists = PDT_ProductBLL.GetModelList("MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',28)!='1' AND Brand NOT IN(" + ParamValue + ") AND Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1')" + " AND MCS_SYS.dbo.UF_Spilt2('MCS_PUB.dbo.PDT_Product',ExtPropertys,'IntegralPoints')=0" +
                            " AND State=1 AND ApproveFlag=1   ORDER BY ISNULL(SubUnit,999999),Code");
                    }
                    for (int i = 0; i < productlists.Count; i++)
                    {
                        if (((Range)worksheet1.Cells[1, 6 + i]).Text.ToString() != productlists[i].ShortName)
                        {
                            ErrorInfo += (_bll.Model["ClientType"] == "3" ? "零售商" : "分销商") + "进货工作表表头，(" + (6 + i).ToString() + "列)产品名称错误！必须为:" + productlists[i].ShortName;
                            goto End;
                        }
                        if (_bll.Model["ClientType"] == "3" && ((Range)worksheet2.Cells[1, 8 + i]).Text.ToString() != productlists[i].ShortName)
                        {
                            ErrorInfo += "零售商进货工作表表头，(" + (8 + i).ToString() + "列)产品名称错误！必须为:" + productlists[i].ShortName + ";错误表头为" + ((Range)worksheet2.Cells[1, 8 + i]).Text.ToString();
                            goto End;
                        }
                    }
                    #endregion

                    improtmessage += DoImportProduct(worksheet1, _bll.Model.AccountMonth, _bll.Model.InsertStaff, 6, productlists, out _State);
                    if (_State != 0) _bll.Model.State = _State;
                    if (_bll.Model["ClientType"] != "2")
                    {
                        improtmessage += DoImportProduct(worksheet2, _bll.Model.AccountMonth, _bll.Model.InsertStaff, 8, productlists, out _State);
                        if (_State != 0) _bll.Model.State = _State;
                    }

                End:
                    ;
                }
                catch (ThreadAbortException exception3)
                {
                    return;
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                        "StackTrace:" + err.StackTrace + "<br/>";


                    message += "系统错误-4!" + err.Message;
                }
                finally
                {
                    if (workbook1 != null) workbook1.Close(false, missing, missing);

                    if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);

                    if (_bll.Model["ClientType"] != "2" && worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);

                    if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                    worksheet1 = null;
                    if (_bll.Model["ClientType"] != "2")
                    {
                        worksheet2 = null;
                    }
                    workbook1 = null;
                }
            }
            catch (ThreadAbortException exception3)
            {

                return;
            }
            catch (System.Exception err)
            {
                message += "系统错误-5!" + err.Message;
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
                        string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                            "StackTrace:" + err.StackTrace + "<br/>";
                        message += "系统错误-6,Excel宏报错，请确认文件打开不报错再上传!" + err.Message;
                        KillProcess();
                    }

                }
                GC.Collect();
                //GC.WaitForPendingFinalizers();

                if (ErrorInfo != "")
                {
                    message += "对不起，Excel文件打开错误，请确认格式是否正确。错误提示:" + ErrorInfo;
                }



            }
            #endregion

            if (_bll.Model.State == 1)
            {
                _bll.Model.State = message != "" ? 3 : 2;
            }
            _bll.Model.Remark = message != "" ? message : improtmessage;
            _bll.Model.ImprotTime = DateTime.Now;
            _bll.Update();
            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            SendMessage("导入成品进销", message != "" ? filename + "-" + message : filename + "导入操作成功！");

        }

        private string DoImportProduct(Worksheet worksheet, int accountmonth, int insertstaff, int cloumn, IList<PDT_Product> productlists, out int State)
        {
            string ImportInfo = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(accountmonth).Model;
            DateTime minday = month.BeginDate;
            DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;

            State = 0;
            ImportInfo += "<br/>";
            ImportInfo += "<span style='color: Red'>-------------------------------------------------------------------------</span><br/>";
            ImportInfo += cloumn == 6 ? "<span style='color: Red'>----批量导入客户进货----</span><br/>" : "<span style='color: Red'>----批量导入客户销量----<br/>ID号      门店类型       门店名     导购员       错误原因<br/></span>";


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
                    ImportInfo += "<span style='color: Red'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，零售商ID与零售商名称" : "，分销商ID与分销商名称") + "不匹配！</span><br/>";
                    State = 4;
                    continue;
                }
                int uplimit = client.ClientType == 3 ? 5000 : 9000;
                if (((Range)worksheet.Cells[row, 5]).Text.ToString() != month.Name)
                {
                    ImportInfo += "<span style='color: Red'>ID号：" + clientid.ToString() + "，归属月份必须为【" + month.Name + "】</span><br/>";
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
                       + " AND Flag=1 ";
                }
                else
                {
                    conditon = "Supplier=" + clientid.ToString()
                  + " AND Type=3 AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                  + " AND Flag=1 AND ISNULL(Promotor,0)=" + promotorid.ToString();

                }
                IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList(conditon + " AND InsertStaff!=1");
                if (svmlists.Count > 0)
                {
                    if (svmlists.FirstOrDefault(p => p.ApproveFlag == 1) != null)
                    {
                        ImportInfo += "<span style='color: Red'>ID号：" + ((Range)worksheet.Cells[row, 1]).Text.ToString() + (client.ClientType == 3 ? "，" : "，该分销商") + client.FullName
                                       + (cloumn == 6 ? "  当月的进货单" : "，导购:" + ((Range)worksheet.Cells[row, 7]).Text.ToString() + "  当月的销量单") + "已审核，不可再次导入！</span><br/>";
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
                    bll.Model.Remark = "Excel批量导入";

                }
                #endregion
                bll.Model["SubmitFlag"] = "1";
                bll.Model["DataSource"] = "3";
                #region 读取各产品销量
                IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
                bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                int quantity = 0;
                bool isnumber = false;
                foreach (PDT_Product product in productlists)
                {
                    quantity = 0;
                    if (((Range)worksheet.Cells[row, _cloumn]).Value2 != null)
                    {
                        int.TryParse(((Range)worksheet.Cells[row, _cloumn]).Value2.ToString(), out quantity);
                        if (int.TryParse(((Range)worksheet.Cells[row, _cloumn]).Value2.ToString(), out quantity) && !isnumber)
                        {
                            isnumber = true;
                        }
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
                    ImportInfo += "<span style='color: Red'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，" + ((Range)worksheet.Cells[row, 4]).Text + ":" : "，分销商：") + client.FullName
                                      + (client.ClientType == 3 ? "" : "，该分销商") + (cloumn == 6 ? "当月的进货单" : "，导购:" + ((Range)worksheet.Cells[row, 7]).Text.ToString() + "  当月的销量单")
                                      + "未能导入！产品名称：" + ((Range)worksheet.Cells[1, _cloumn]).Text + "数量填写错误。</span><br/>";
                    State = 4;
                    continue;
                }
                #endregion

                #region 更新销量至数据库
                if (bll.Model.ID > 0)
                {
                    if (details.Count > 0)
                    {
                        bll.DeleteDetail();     //先清除原先导入的数据
                        bll.Items = details;
                        bll.Model.UpdateStaff = insertstaff;
                        bll.AddDetail();
                        bll.Update();

                        ImportInfo += "<span style='color:Blue'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，该零售商：" : "，该分销商：") + client.FullName
                            + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + (cloumn == 6 ? " 的进货单" : "的销量单") + "被成功更新！产品SKU数："
                      + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
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
                        ImportInfo += "<span style='color:Black'>ID号：" + clientid.ToString() + (client.ClientType == 3 ? "，该零售商：" : "，该分销商：") + client.FullName
                      + (cloumn == 6 ? " 的进货单" : "的销量单") + "已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                     + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                    }
                }
                #endregion
            }
            #endregion

            return ImportInfo;
        }
        #endregion

        #region 导入办事处重点品项
        private void ImportKeyProduct(int templateid, string path)
        {
            ThreadAbortException e;
            Exception err;
            string error;
            SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(templateid);
            if (_bll.Model.State != 1)
            {
                return;
            }
            string message = "";
            string improtmessage = "";
            AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
            string ErrorInfo = "";
            object missing = Missing.Value;
            ApplicationClass ExcelApp = null;
            try
            {
                ExcelApp = new ApplicationClass
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                Workbook workbook1 = null;
                Worksheet worksheet1 = null;
                try
                {
                    PDT_Product product;
                    workbook1 = ExcelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    worksheet1 = (Worksheet)workbook1.Worksheets[1];
                    if (workbook1.Worksheets.Count != 1)
                    {
                        improtmessage = improtmessage + "Excel表格中必须且只能有1张工作表,请并检查宏是否有问题！";
                        goto End;
                    }

                    switch (_bll.Model.IsOpponent)
                    {
                        case 2:
                            if (worksheet1.Name != "重点品项")
                            {
                                improtmessage = improtmessage + "Excel表格中第1个工作表名称必须为【重点品项】！";
                                goto End;
                            }
                            if (!this.VerifyKeyProductSheet(worksheet1))
                            {
                                improtmessage = improtmessage + "工作表表头(1~7列)错误！";
                                goto End;
                            }
                            break;
                        case 3:
                            if (worksheet1.Name != "月度费率")
                            {
                                improtmessage = improtmessage + "Excel表格中第1个工作表名称必须为【月度费率】！";
                                goto End;
                            }
                            if (!this.VerifyFeeRateSheet(worksheet1))
                            {
                                improtmessage = improtmessage + "工作表表头(1~6列)错误！";
                                goto End;
                            }
                            break;
                    }

                    int cloumn = 5;
                    int sellinrow = 1;
                    DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;

                    improtmessage += "<br/>";
                    improtmessage += "<span style='color: Red'>-------------------------------------------------------------------------</span><br/>";
                    improtmessage += "<span style='color: Red'>---------------------------" + (_bll.Model.IsOpponent == 2 ? "办事处重点品项导入" : "办事处费率导入") + "----------------------------</span><br/>";

                    #region 读取Excel表格
                    while (true)
                    {

                        cloumn = 5;
                        sellinrow++;

                        if (((Range)worksheet1.Cells[sellinrow, 2]).Value2 == null)
                        {
                            break;
                        }

                        int cityid = 0;
                        if (!int.TryParse(((Range)worksheet1.Cells[sellinrow, 2]).Value2.ToString(), out cityid))
                        {
                            continue;
                        }
                        #region 验证数据
                        Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(cityid).Model;
                        if (_city == null || _city.Name != ((Range)worksheet1.Cells[sellinrow, 3]).Text.ToString())
                        {

                            improtmessage += "<span style='color: Red'>办事处ID号：" + ((Range)worksheet1.Cells[sellinrow, 2]).Text.ToString() + "与办事处名称不匹配！</span><br/>";

                            _bll.Model.State = 4;
                            continue;
                        }
                        if (((Range)worksheet1.Cells[sellinrow, 4]).Text.ToString() != month.Name)
                        {
                            improtmessage += "<span style='color: Red'>办事处ID号：" + ((Range)worksheet1.Cells[sellinrow, 2]).Text.ToString() + "，归属月份必须为【" + month.Name + "】</span><br/>";

                            _bll.Model.State = 4;
                            continue;
                        }
                        #endregion
                        #region 办事处目标额
                        SVM_OrganizeTargetBLL bll = null;
                        IList<SVM_OrganizeTarget> _targetlist = SVM_OrganizeTargetBLL.GetModelList("OrganizeCity=" + cityid.ToString() + "AND AccountMonth=" + month.ID.ToString());
                        if (_targetlist.Count > 0)
                        {
                            if (_targetlist.FirstOrDefault<SVM_OrganizeTarget>(p => (p.ApproveFlag == 1)) != null)
                            {
                                improtmessage = improtmessage + "<span style='color: Red'>ID号：" + ((Range)worksheet1.Cells[sellinrow, 2]).Text.ToString() + "当月的重点品项已审核，不可再次导入！</span><br/>";
                                continue;
                            }
                            if (_targetlist.Count == 1)
                            {
                                bll = new SVM_OrganizeTargetBLL(_targetlist[0].ID);
                                if (_bll.Model.IsOpponent == 2)
                                {
                                    bll.Items.Clear();
                                }
                            }
                        }
                        if (bll == null)
                        {
                            bll = new SVM_OrganizeTargetBLL
                            {
                                Model = { OrganizeCity = cityid, AccountMonth = month.ID, ApproveFlag = 2 }
                            };
                        }

                        decimal amount = 0M;
                        if (_bll.Model.IsOpponent == 2)
                        {
                            decimal.TryParse(((Range)worksheet1.Cells[sellinrow, cloumn]).Value2.ToString(), out amount);
                            if (amount != 0M)
                            {
                                bll.Model.SalesTarget = amount;
                            }
                            else if ((this.trim(((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString()) != "") && (((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString() != "0"))
                            {
                                improtmessage += "<span style='color: Red'>ID号：" + cityid.ToString() + "，" + _city.Name + "的重点品项未能导入，办事处业绩目标额：" + ((Range)worksheet1.Cells[1, cloumn]).Text + "金额填写错误";
                                _bll.Model.State = 4;
                                continue;
                            }
                            ++cloumn;
                            amount = 0M;
                        }
                        else
                        {
                            decimal.TryParse(((Range)worksheet1.Cells[sellinrow, cloumn]).Value2.ToString(), out amount);
                            if (amount != 0M)
                            {
                                bll.Model.FeeRateTarget = amount;
                            }
                            else if ((this.trim(((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString()) != "") && (((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString() != "0"))
                            {
                                improtmessage += "<span style='color: Red'>ID号：" + cityid.ToString() + "，" + _city.Name + "的重点品项未能导入，办事处月度费率目标：" + ((Range)worksheet1.Cells[1, cloumn]).Text + "金额填写错误";
                                _bll.Model.State = 4;
                                continue;
                            }


                            amount = 0M;
                            decimal.TryParse(((Range)worksheet1.Cells[sellinrow, ++cloumn]).Value2.ToString(), out amount);
                            if (amount != 0M)
                            {
                                bll.Model.FeeYieldRate = amount;
                            }
                            else if ((this.trim(((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString()) != "") && (((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString() != "0"))
                            {
                                improtmessage += "<span style='color: Red'>ID号：" + cityid.ToString() + "，" + _city.Name + "的重点品项未能导入，办事处月度费率达成：" + ((Range)worksheet1.Cells[1, cloumn]).Text + "金额填写错误";
                                _bll.Model.State = 4;
                                continue;
                            }
                        }
                        #endregion
                        IList<SVM_KeyProductTarget_Detail> details = new List<SVM_KeyProductTarget_Detail>();


                        bool wrongflag = false;//判断导入数量是否正常（除空导致的异常）
                        while (true)
                        {

                            amount = 0M;

                            if (((Range)worksheet1.Cells[1, cloumn]).Text.ToString() == string.Empty)
                            {
                                break;
                            }

                            IList<PDT_Product> products = PDT_ProductBLL.GetModelList("ShortName='" + ((Range)worksheet1.Cells[1, cloumn]).Text + "' AND State=1");
                            if (products.Count > 0)
                            {
                                product = products[0];
                            }
                            else
                            {
                                improtmessage += "<span style='color: Red'>产品名称：" + ((Range)worksheet1.Cells[1, cloumn]).Text + "在产品列表中不存在！</span><br/>";
                                _bll.Model.State = 4;
                                cloumn++;
                                continue;
                            }
                            if ((product != null) && (((Range)worksheet1.Cells[sellinrow, cloumn]).Value2 != null))
                            {
                                decimal.TryParse(((Range)worksheet1.Cells[sellinrow, cloumn]).Value2.ToString(), out amount);
                                if (amount != 0M)
                                {
                                    SVM_KeyProductTarget_Detail detail = new SVM_KeyProductTarget_Detail
                                    {
                                        Product = product.ID,
                                        Amount = amount
                                    };
                                    details.Add(detail);
                                }
                                else if ((this.trim(((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString()) != "") && (((Range)worksheet1.Cells[sellinrow, cloumn]).Text.ToString() != "0"))
                                {
                                    wrongflag = true;
                                    break;
                                }
                            }

                        }
                        if (wrongflag && _bll.Model.IsOpponent == 2)
                        {
                            improtmessage += "<span style='color: Red'>ID号：" + cityid.ToString() + "，" + _city.Name + "的重点品项未能导入，品项名称：" + ((Range)worksheet1.Cells[1, cloumn]).Text + "金额填写错误";
                            _bll.Model.State = 4;
                            continue;
                        }
                        #region 更新销量至数据库
                        if (bll.Model.ID > 0)
                        {
                            switch (_bll.Model.IsOpponent)
                            {
                                case 2:
                                    if (details.Count > 0)
                                    {
                                        bll.DeleteDetail();     //先清除原先导入的数据
                                        bll.Model.UpdateStaff = _bll.Model.InsertStaff;
                                        bll.Items = details;
                                        bll.AddDetail();

                                        bll.Update();
                                        improtmessage += "<span style='color: Blue'>ID号：" + cityid.ToString() + "，" + _city.Name + " 的重点品项被成功更新！</span><br/>";

                                    }
                                    break;
                                case 3:
                                    bll.Update();
                                    improtmessage += "<span style='color: Blue'>ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处费率被成功更新！</span><br/>";
                                    break;

                            }

                        }
                        else
                        {
                            switch (_bll.Model.IsOpponent)
                            {
                                case 2:
                                    if (details.Count > 0)
                                    {
                                        bll.Items = details;
                                        if (bll.Add() > 0)
                                        {
                                            foreach (SVM_OrganizeTarget m in _targetlist)
                                            {
                                                bll = new SVM_OrganizeTargetBLL(m.ID);
                                                bll.DeleteDetail();
                                                bll.Delete();
                                            }
                                        }
                                        improtmessage += "<span style='color: Black'>ID号：" + cityid.ToString() + "，" + _city.Name + " 的重点品项已成功导入！</span><br/>";
                                    }
                                    break;
                                case 3:
                                    bll.Add();
                                    improtmessage += "<span style='color: Black'>ID号：" + cityid.ToString() + "，" + _city.Name + " 的办事处费率已成功导入！</span><br/>";
                                    break;
                            }

                        }

                        #endregion
                    }
                    #endregion
                End:
                    ;
                }
                catch (ThreadAbortException exception1)
                {
                    e = exception1;
                    return;
                }
                catch (Exception exception2)
                {
                    err = exception2;
                    error = "Message:" + err.Message + "<br/>Source:" + err.Source + "<br/>StackTrace:" + err.StackTrace + "<br/>";
                    message = message + "系统错误-4!" + err.Message;
                }
                finally
                {
                    if (workbook1 != null)
                    {
                        workbook1.Close(false, missing, missing);
                    }
                    if (worksheet1 != null)
                    {
                        Marshal.ReleaseComObject(worksheet1);
                    }
                    if (workbook1 != null)
                    {
                        Marshal.ReleaseComObject(workbook1);
                    }
                    worksheet1 = null;
                    workbook1 = null;
                }
            }
            catch (ThreadAbortException exception3)
            {
                e = exception3;
                return;
            }
            catch (Exception exception4)
            {
                err = exception4;
                message = message + "系统错误-5!" + err.Message;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    try
                    {
                        ExcelApp.Workbooks.Close();
                        ExcelApp.Quit();
                        Marshal.ReleaseComObject(ExcelApp);
                        ExcelApp = null;
                    }
                    catch (Exception exception5)
                    {
                        err = exception5;
                        error = "Message:" + err.Message + "<br/>Source:" + err.Source + "<br/>StackTrace:" + err.StackTrace + "<br/>";
                        message = message + "系统错误-6,Excel宏报错，请确认文件打开不报错再上传!" + err.Message;
                    }
                }
                GC.Collect();
                if (ErrorInfo != "")
                {
                    message = message + "对不起，Excel文件打开错误，请确认格式是否正确。错误提示:" + ErrorInfo;
                }
            }

            if (_bll.Model.State == 1)
            {
                _bll.Model.State = (message != "") ? 3 : 2;
            }
            _bll.Model.Remark = (message != "") ? message : improtmessage;
            _bll.Model.ImprotTime = DateTime.Now;
            _bll.Update();
            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            this.SendMessage("导入办事处重点品项", (message != "") ? (filename + "-" + message) : (filename + "导入操作成功！"));
        }
        #endregion

        #region 验证信息
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

        private bool VerifyDIWorkSheet(Worksheet worksheet)
        {
            bool flag = true;
            if (((Range)worksheet.Cells[1, 1]).Text.ToString() != "分销商ID" ||
                         ((Range)worksheet.Cells[1, 2]).Text.ToString() != "分销商编号" ||
                         ((Range)worksheet.Cells[1, 3]).Text.ToString() != "分销商名称" ||
                         ((Range)worksheet.Cells[1, 4]).Text.ToString() != "分销商分类" ||
                          ((Range)worksheet.Cells[1, 5]).Text.ToString() != "归属月份")
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

        private bool VerifyKeyProductSheet(Worksheet worksheet)
        {
            bool flag = true;
            if (((Range)worksheet.Cells[1, 1]).Text.ToString() != "营业部" ||
                ((Range)worksheet.Cells[1, 2]).Text.ToString() != "办事处ID" ||
                ((Range)worksheet.Cells[1, 3]).Text.ToString() != "办事处" ||
                ((Range)worksheet.Cells[1, 4]).Text.ToString() != "归属月份" ||
                ((Range)worksheet.Cells[1, 5]).Text.ToString() != "办事处业绩目标额")
            {
                flag = false;
            }
            return flag;
        }

        private bool VerifyFeeRateSheet(Worksheet worksheet)
        {
            bool flag = true;
            if (((Range)worksheet.Cells[1, 1]).Text.ToString() != "营业部" ||
                ((Range)worksheet.Cells[1, 2]).Text.ToString() != "办事处ID" ||
                ((Range)worksheet.Cells[1, 3]).Text.ToString() != "办事处" ||
                ((Range)worksheet.Cells[1, 4]).Text.ToString() != "归属月份" ||
                ((Range)worksheet.Cells[1, 5]).Text.ToString() != "办事处月度费率目标" ||
                ((Range)worksheet.Cells[1, 6]).Text.ToString() != "办事处月度费率达成")
            {
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 生成成品Excel导入模版
        private void CreateDownTemplate(int templateid, string path)
        {
            SVM_DownloadTemplateBLL _bll = new SVM_DownloadTemplateBLL(templateid);
            if (_bll.Model.State == 5) return;//模版作废
            AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
            DateTime day = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
            string message = "";
            #region 获取业代负责的零售商及所有产品数据
            string condtion = _bll.Model["ClientType"] == "2" ? " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='2'" : " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',89) !='1'";
            IList<CM_Client> clientlists = CM_ClientBLL.GetModelList("ClientType=" + _bll.Model["ClientType"] + " AND ClientManager=" + _bll.Model.DownStaff.ToString() +
               " AND ApproveFlag=1" + condtion + " AND OpenTime<'" + month.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "' AND ISNULL(CloseTime,GETDATE())>='" + month.BeginDate.ToString("yyyy-MM-dd") + "' ORDER BY Code");
            if (clientlists.Count == 0)
            {
                message = "对不起，没有当前人直接负责的客户！";
                _bll.Model.Remark = message;
                _bll.Update();
                return;
            }
            #endregion

            #region 生成Excel文件
            object missing = System.Reflection.Missing.Value;
            ApplicationClass ExcelApp = null;

            try
            {
                ExcelApp = new ApplicationClass();
                ExcelApp.Visible = false;
                ExcelApp.DisplayAlerts = false;

                Workbook workbook1 = null;
                Worksheet worksheet1 = null, worksheet2 = null;
                try
                {
                    workbook1 = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                    worksheet1 = (Worksheet)workbook1.Worksheets["sheet1"];
                    if (_bll.Model["ClientType"] == "2")
                    {
                        worksheet1.Name = "分销商进货";
                        WriteSheet(worksheet1, "MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',28)!='1' AND Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1') AND State=1 AND ApproveFlag=1  ORDER BY ISNULL(SubUnit,999999),Code", clientlists, month, 6);
                    }
                    else
                    {
                        worksheet2 = (Worksheet)workbook1.Worksheets.Add(missing, worksheet1, 1, missing);
                        worksheet1.Name = "零售商进货";
                        worksheet2.Name = "零售商销货";


                        string ParamValue = Addr_OrganizeCityParamBLL.GetValueByType(1, 26);
                        ParamValue = string.IsNullOrEmpty(ParamValue) ? "0" : ParamValue;
                        ParamValue = ParamValue.EndsWith(",") ? ParamValue.Remove(ParamValue.Length - 1) : ParamValue;


                        WriteSheet(worksheet2, "MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',28)!='1' AND Brand NOT IN(" + ParamValue + ") AND Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1')AND MCS_SYS.dbo.UF_Spilt2('MCS_PUB.dbo.PDT_Product',ExtPropertys,'IntegralPoints')!=1 AND State=1 AND ApproveFlag=1  ORDER BY ISNULL(SubUnit,999999),Code", clientlists, month, 8);
                        WriteSheet(worksheet1, "MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',28)!='1' AND Brand NOT IN(" + ParamValue + ") AND Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1')AND MCS_SYS.dbo.UF_Spilt2('MCS_PUB.dbo.PDT_Product',ExtPropertys,'IntegralPoints')!=1 AND State=1 AND ApproveFlag=1  ORDER BY ISNULL(SubUnit,999999),Code", clientlists, month, 6);
                    }



                    worksheet1.Activate();
                    ExcelApp.AlertBeforeOverwriting = false;
                    workbook1.SaveAs(path, XlFileFormat.xlExcel8, "", "", false, false, XlSaveAsAccessMode.xlNoChange, 1, false, missing, missing, missing);
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                        "StackTrace:" + err.StackTrace + "<br/>";

                    message += "系统错误-1!" + err.Message;
                }
                finally
                {
                    if (workbook1 != null) workbook1.Close(false, missing, missing);

                    if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                    if (_bll.Model["ClientType"] != "2" && worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);
                    if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                    worksheet1 = null;
                    if (_bll.Model["ClientType"] != "2")
                    {
                        worksheet2 = null;
                    }
                    workbook1 = null;

                    if (File.Exists(path))
                    {
                        _bll.Model.Path = path;
                        _bll.Model.State = 2;
                    }
                }
            }
            catch (System.Exception err)
            {
                string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                    "StackTrace:" + err.StackTrace + "<br/>";

                message += "系统错误-2!" + err.Message;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    ExcelApp.Workbooks.Close();
                    ExcelApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                    ExcelApp = null;
                }
                GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            #endregion
            if (message != "") _bll.Model.State = 4;

            _bll.Model.Remark = message;
            _bll.Update();
            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            SendMessage("生成成品导入模版", message != "" ? filename + "-" + message : filename + "生成操作成功！");
        }
        #endregion

        #region 生成赠品Excel导入模版
        private void CrateGiftDownTemplate(int templateid, string path)
        {
            SVM_DownloadTemplateBLL _bll = new SVM_DownloadTemplateBLL(templateid);
            if (_bll.Model.State == 5) return;//模版作废
            AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
            DateTime day = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
            string message = "";
            #region 获取业代负责的零售商及所有产品数据
            string condtion = _bll.Model["ClientType"] == "2" ? " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='2'" : "";
            IList<CM_Client> clientlists = CM_ClientBLL.GetModelList("ClientType=" + _bll.Model["ClientType"] + " AND ClientManager=" + _bll.Model.DownStaff.ToString() +
               " AND ActiveFlag=1 AND ApproveFlag=1" + condtion + " AND OpenTime<'" + day.ToString("yyyy-MM-dd") + " 23:59:59' ORDER BY Code");
            if (clientlists.Count == 0)
            {
                message = "对不起，没有当前人直接负责的客户！";
                _bll.Model.Remark = message;
                _bll.Update();
                return;
            }
            #endregion

            #region 生成Excel文件
            object missing = System.Reflection.Missing.Value;
            ApplicationClass ExcelApp = null;

            try
            {
                ExcelApp = new ApplicationClass();
                ExcelApp.Visible = false;
                ExcelApp.DisplayAlerts = false;

                Workbook workbook1 = null;
                Worksheet worksheet1 = null, worksheet2 = null, worksheet3 = null;
                try
                {
                    workbook1 = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                    worksheet1 = (Worksheet)workbook1.Worksheets["sheet1"];
                    worksheet2 = (Worksheet)workbook1.Worksheets.Add(missing, worksheet1, 1, missing);
                    worksheet3 = (Worksheet)workbook1.Worksheets.Add(missing, worksheet2, 1, missing);
                    worksheet1.Name = "成品赠品";
                    worksheet2.Name = "试用装";
                    worksheet3.Name = "高值促销品";


                    //将产品写入表头
                    if (!string.IsNullOrEmpty(_bll.Model.ProductGifts)) WriteSheet(worksheet1, "ID IN(" + _bll.Model.ProductGifts + ")", clientlists, month, 6);
                    if (!string.IsNullOrEmpty(_bll.Model.Testers)) WriteSheet(worksheet2, "ID IN(" + _bll.Model.Testers + ")", clientlists, month, 6);
                    if (!string.IsNullOrEmpty(_bll.Model.Gifts)) WriteSheet(worksheet3, "ID IN(" + _bll.Model.Gifts + ")", clientlists, month, 6);

                    worksheet1.Activate();
                    ExcelApp.AlertBeforeOverwriting = false;
                    workbook1.SaveAs(path, XlFileFormat.xlExcel8, "", "", false, false, XlSaveAsAccessMode.xlNoChange, 1, false, missing, missing, missing);
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                        "StackTrace:" + err.StackTrace + "<br/>";

                    message += "系统错误-1!" + err.Message;
                }
                finally
                {
                    if (workbook1 != null) workbook1.Close(false, missing, missing);

                    if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                    if (worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);
                    if (worksheet3 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet3);
                    if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                    worksheet1 = null;
                    worksheet2 = null;
                    workbook1 = null;

                    if (File.Exists(path))
                    {
                        _bll.Model.Path = path;
                        _bll.Model.State = 2;
                    }
                }
            }
            catch (System.Exception err)
            {
                string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                    "StackTrace:" + err.StackTrace + "<br/>";

                message += "系统错误-2!" + err.Message;
            }
            finally
            {
                if (ExcelApp != null)
                {
                    ExcelApp.Workbooks.Close();
                    ExcelApp.Quit();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                    ExcelApp = null;
                }
                GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            #endregion
            if (message != "") _bll.Model.State = 4;
            _bll.Model.Remark = message;
            _bll.Update();
            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            SendMessage("生成赠品导入模版", message != "" ? filename + "-" + message : filename + "生成操作成功！");
        }
        #endregion

        #region 生成办事处重点品项导入模版
        private void CreateKeyTargetDownTemplate(int templateid, string path)
        {
            int cloumn = 8;
            SVM_DownloadTemplateBLL _bll = new SVM_DownloadTemplateBLL(templateid);
            if (_bll.Model.State != 5)
            {
                Exception err;
                string error;
                AC_AccountMonth month = new AC_AccountMonthBLL(_bll.Model.AccountMonth).Model;
                DateTime day = (DateTime.Today < month.EndDate) ? DateTime.Today : month.EndDate;
                string message = ""; IList<Addr_OrganizeCity> _cityList;
                if (_bll.Model["OrganizeCity"] != "1")
                {
                    _cityList = Addr_OrganizeCityBLL.GetModelList("Level3_SuperID IN (" + _bll.Model["OrganizeCity"] + ") AND Level=" + ConfigHelper.GetConfigString("OrganizeCity-CityLevel") + " Order By Level3_SuperID");
                }
                else
                {
                    _cityList = Addr_OrganizeCityBLL.GetModelList("Level=" + ConfigHelper.GetConfigString("OrganizeCity-CityLevel") + " Order By Level3_SuperID");
                }
                object missing = Missing.Value;
                ApplicationClass ExcelApp = null;

                try
                {
                    ExcelApp = new ApplicationClass
                    {
                        Visible = false,
                        DisplayAlerts = false
                    };
                    Workbook workbook1 = null;

                    Worksheet worksheet1 = null;
                    try
                    {
                        workbook1 = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                        worksheet1 = (Worksheet)workbook1.Worksheets["sheet1"];
                        worksheet1.Name = "重点品项";

                        int bgcolor1 = ColorTranslator.ToOle(Color.LightYellow);
                        int bgcolor2 = ColorTranslator.ToOle(Color.LightGreen);

                        worksheet1.Cells[1, 1] = "营业部";
                        worksheet1.Cells[1, 2] = "办事处ID";
                        worksheet1.Cells[1, 3] = "办事处";
                        worksheet1.Cells[1, 4] = "归属月份";
                        if (_bll.Model.IsOpponent == 2)
                        {
                            worksheet1.Cells[1, 5] = "办事处业绩目标额";
                            cloumn = 6;
                        }
                        else
                        {
                            worksheet1.Name = "月度费率";
                            worksheet1.Cells[1, 5] = "办事处月度费率目标";
                            worksheet1.Cells[1, 6] = "办事处月度费率达成";
                        }
                        worksheet1.get_Range("B2", "B2").ColumnWidth = 15;
                        worksheet1.get_Range("C2", "C2").ColumnWidth = 20;
                        worksheet1.get_Range("D2", "E2").ColumnWidth = 10;
                        worksheet1.get_Range("A1", "A1").RowHeight = 50;
                        worksheet1.get_Range("A1", "CC1").WrapText = true;
                        worksheet1.get_Range("A1", "CC1").Font.Bold = true;
                        worksheet1.get_Range("A1", "CC1000").Font.Size = 9;
                        worksheet1.get_Range("A1", "CC1000").HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        int sellrow = 2;
                        foreach (Addr_OrganizeCity city in _cityList)
                        {
                            worksheet1.Cells[sellrow, 1] = new Addr_OrganizeCityBLL(city.SuperID).Model.Name;
                            worksheet1.Cells[sellrow, 2] = city.ID;
                            worksheet1.Cells[sellrow, 3] = city.Name;
                            worksheet1.Cells[sellrow, 4] = "'" + month.Name;
                            #region 按營業部設置顏色
                            if (sellrow > 0)
                            {
                                if (new Addr_OrganizeCityBLL(city.SuperID).Model.Name == ((Range)worksheet1.Cells[sellrow - 1, 1]).Text.ToString())
                                {
                                    worksheet1.get_Range(worksheet1.Cells[sellrow, 1], worksheet1.Cells[sellrow, 4]).Interior.Color =
                                        worksheet1.get_Range(worksheet1.Cells[sellrow - 1, 1], worksheet1.Cells[sellrow - 1, 4]).Interior.Color;
                                }
                                else
                                {
                                    if (int.Parse(worksheet1.get_Range(worksheet1.Cells[sellrow - 1, 1], worksheet1.Cells[sellrow - 1, 4]).Interior.Color.ToString()) == bgcolor1)
                                        worksheet1.get_Range(worksheet1.Cells[sellrow, 1], worksheet1.Cells[sellrow, 4]).Interior.Color = bgcolor2;
                                    else
                                        worksheet1.get_Range(worksheet1.Cells[sellrow, 1], worksheet1.Cells[sellrow, 4]).Interior.Color = bgcolor1;
                                }
                            }
                            else
                            {
                                worksheet1.get_Range(worksheet1.Cells[sellrow, 1], worksheet1.Cells[sellrow, 4]).Interior.Color = bgcolor1;
                            }
                            #endregion
                            sellrow++;
                        }
                        if (_bll.Model.IsOpponent == 2)
                        {
                            if (!string.IsNullOrEmpty(_bll.Model.ProductGifts))
                            {
                                if (_bll.Model.ProductGifts.EndsWith(","))
                                {
                                    _bll.Model.ProductGifts = _bll.Model.ProductGifts.Substring(0, _bll.Model.ProductGifts.Length - 1);
                                }
                                IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("ID IN (" + _bll.Model.ProductGifts + ") ORDER BY ISNULL(SubUnit,999999),Code");
                                //将产品写入表头
                                for (int i = 0; i < productlists.Count; i++)
                                {
                                    worksheet1.Cells[1, cloumn + i] = productlists[i].ShortName;

                                    #region 按品牌设置产品列颜色
                                    if (i > 0)
                                    {
                                        if (productlists[i].Brand == productlists[i - 1].Brand)
                                        {
                                            worksheet1.get_Range(worksheet1.Cells[1, cloumn + i], worksheet1.Cells[1000, cloumn + i]).Interior.Color =
                                                worksheet1.get_Range(worksheet1.Cells[1, cloumn + i - 1], worksheet1.Cells[1000, cloumn + i - 1]).Interior.Color;
                                        }
                                        else
                                        {
                                            if (int.Parse(worksheet1.get_Range(worksheet1.Cells[1, cloumn + i - 1], worksheet1.Cells[1000, cloumn + i - 1]).Interior.Color.ToString()) == bgcolor1)
                                                worksheet1.get_Range(worksheet1.Cells[1, cloumn + i], worksheet1.Cells[1000, cloumn + i]).Interior.Color = bgcolor2;
                                            else
                                                worksheet1.get_Range(worksheet1.Cells[1, cloumn + i], worksheet1.Cells[1000, cloumn + i]).Interior.Color = bgcolor1;
                                        }
                                    }
                                    else
                                    {
                                        worksheet1.get_Range(worksheet1.Cells[1, cloumn + i], worksheet1.Cells[1000, cloumn + i]).Interior.Color = bgcolor1;
                                    }
                                    #endregion
                                }

                                #region 设置表格格式
                                //设置行高
                                worksheet1.get_Range(worksheet1.Cells[2, 1], worksheet1.Cells[sellrow - 1, 1]).RowHeight = 16;

                                //设置表格单元格格线
                                worksheet1.get_Range(worksheet1.Cells[1, 1], worksheet1.Cells[sellrow - 1, cloumn + productlists.Count - 1]).Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                                #endregion
                            }
                        }
                        worksheet1.Activate();
                        ExcelApp.AlertBeforeOverwriting = false;
                        workbook1.SaveAs(path, XlFileFormat.xlExcel8, "", "", false, false, XlSaveAsAccessMode.xlNoChange, 1, false, missing, missing, missing);
                    }
                    catch (Exception exception1)
                    {
                        err = exception1;
                        error = "Message:" + err.Message + "<br/>Source:" + err.Source + "<br/>StackTrace:" + err.StackTrace + "<br/>";
                        message = message + "系统错误-1!" + err.Message;
                    }
                    finally
                    {
                        if (workbook1 != null)
                        {
                            workbook1.Close(false, missing, missing);
                        }
                        if (worksheet1 != null)
                        {
                            Marshal.ReleaseComObject(worksheet1);
                        }
                        if (workbook1 != null)
                        {
                            Marshal.ReleaseComObject(workbook1);
                        }
                        worksheet1 = null;
                        workbook1 = null;
                        if (File.Exists(path))
                        {
                            _bll.Model.Path = path;
                            _bll.Model.State = 2;
                        }
                    }
                }
                catch (Exception exception2)
                {
                    err = exception2;
                    error = "Message:" + err.Message + "<br/>Source:" + err.Source + "<br/>StackTrace:" + err.StackTrace + "<br/>";
                    message = message + "系统错误-2!" + err.Message;
                }
                finally
                {
                    if (ExcelApp != null)
                    {
                        ExcelApp.Workbooks.Close();
                        ExcelApp.Quit();
                        Marshal.ReleaseComObject(ExcelApp);
                        ExcelApp = null;
                    }
                    GC.Collect();
                }
                if (message != "")
                {
                    _bll.Model.State = 4;
                }
                _bll.Model.Remark = message;
                _bll.Update();
                string filename = path.Substring(path.LastIndexOf('\\') + 1);
                this.SendMessage("生成重点品项导入模版", (message != "") ? (filename + "-" + message) : (filename + "生成操作成功！"));
            }
        }
        #endregion

        private void WriteSheet(Worksheet worksheet, string conditon, IList<CM_Client> clientlists, AC_AccountMonth Month, int cloumn)
        {

            IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList(conditon);

            int bgcolor1 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow);
            int bgcolor2 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            #region 创建表头

            if (clientlists[0].ClientType == 2)
            {
                worksheet.Cells[1, 1] = "分销商ID";
                worksheet.Cells[1, 2] = "分销商编号";
                worksheet.Cells[1, 3] = "分销商名称";
                worksheet.Cells[1, 4] = "分销商分类";
                worksheet.Cells[1, 5] = "归属月份";
            }
            else
            {
                worksheet.Cells[1, 1] = "零售商ID";
                worksheet.Cells[1, 2] = "零售商编号";
                worksheet.Cells[1, 3] = "零售商名称";
                worksheet.Cells[1, 4] = "零售商分类";
                worksheet.Cells[1, 5] = "归属月份";
            }
            if (cloumn == 8)
            {
                worksheet.Cells[1, 6] = "导购ID";
                worksheet.Cells[1, 7] = "导购姓名";
            }

            worksheet.get_Range("B2", "B2").ColumnWidth = 15;
            worksheet.get_Range("C2", "C2").ColumnWidth = 20;
            worksheet.get_Range("D2", "E2").ColumnWidth = 10;
            worksheet.get_Range("A1", "A1").RowHeight = 50;
            worksheet.get_Range("A1", "CC1").WrapText = true;
            worksheet.get_Range("A1", "CC1").Font.Bold = true;
            worksheet.get_Range("A1", "CC1000").Font.Size = 9;
            worksheet.get_Range("A1", "CC1000").HorizontalAlignment = XlHAlign.xlHAlignCenter;


            #region 将零售商信息写入表格内
            int sellrow = 2;
            foreach (CM_Client client in clientlists)
            {
                worksheet.Cells[sellrow, 1] = client.ID;
                worksheet.Cells[sellrow, 2] = client.Code;
                worksheet.Cells[sellrow, 3] = client.FullName;
                try
                {
                    worksheet.Cells[sellrow, 4] = client.ClientType == 2 ? "分销商" : DictionaryBLL.GetDicCollections("CM_RT_Classify")[client["RTClassify"]].Name;
                }
                catch
                { ;}
                worksheet.Cells[sellrow, 5] = "'" + Month.Name;

                if (cloumn == 8)
                {

                    StringBuilder condition = new StringBuilder(" PM_Promotor.BeginWorkDate<'" + Month.EndDate.AddDays(1).ToString("yyyy-MM-dd") + "' AND ISNULL(PM_Promotor.EndWorkDate,GETDATE())>='" + Month.BeginDate.ToString("yyyy-MM-dd") + "' AND PM_Promotor.ApproveFlag=1 ");
                    condition.Append("AND ID in (SELECT Promotor FROM PM_PromotorInRetailer WHERE Client = " + client.ID.ToString() + ")");

                    IList<PM_Promotor> promotorlists = PM_PromotorBLL.GetModelList(condition.ToString());

                    for (int j = 0; j < promotorlists.Count; j++)
                    {
                        if (j > 0)
                        {
                            worksheet.Cells[sellrow, 1] = client.ID;
                            worksheet.Cells[sellrow, 2] = client.Code;
                            worksheet.Cells[sellrow, 3] = client.FullName;
                            try
                            {
                                worksheet.Cells[sellrow, 4] = DictionaryBLL.GetDicCollections("CM_RT_Classify")[client["RTClassify"]].Name;
                            }
                            catch
                            { ;}
                            worksheet.Cells[sellrow, 5] = "'" + Month.Name;
                        }
                        worksheet.Cells[sellrow, 6] = promotorlists[j].ID;
                        worksheet.Cells[sellrow, 7] = promotorlists[j].Name;

                        if (j != promotorlists.Count - 1) sellrow++;
                    }
                }

                sellrow++;
            }
            #endregion
            #endregion

            //将产品写入表头
            for (int i = 0; i < productlists.Count; i++)
            {
                worksheet.Cells[1, cloumn + i] = productlists[i].ShortName;

                #region 按品牌设置产品列颜色
                if (i > 0)
                {
                    if (productlists[i].Brand == productlists[i - 1].Brand)
                    {
                        worksheet.get_Range(worksheet.Cells[1, cloumn + i], worksheet.Cells[1000, cloumn + i]).Interior.Color =
                            worksheet.get_Range(worksheet.Cells[1, cloumn + i - 1], worksheet.Cells[1000, cloumn + i - 1]).Interior.Color;
                    }
                    else
                    {
                        if (int.Parse(worksheet.get_Range(worksheet.Cells[1, cloumn + i - 1], worksheet.Cells[1000, cloumn + i - 1]).Interior.Color.ToString()) == bgcolor1)
                            worksheet.get_Range(worksheet.Cells[1, cloumn + i], worksheet.Cells[1000, cloumn + i]).Interior.Color = bgcolor2;
                        else
                            worksheet.get_Range(worksheet.Cells[1, cloumn + i], worksheet.Cells[1000, cloumn + i]).Interior.Color = bgcolor1;
                    }
                }
                else
                {
                    worksheet.get_Range(worksheet.Cells[1, cloumn + i], worksheet.Cells[1000, cloumn + i]).Interior.Color = bgcolor1;
                }
                #endregion
            }

            #region 设置表格格式
            //设置行高
            worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[sellrow - 1, 1]).RowHeight = 16;

            //设置表格单元格格线
            worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[sellrow - 1, cloumn + productlists.Count - 1]).Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            #endregion
        }
        private void ThreadCheckHeartTime()
        {
            while (bIsRunning)
            {
                int TimeLimit = ConfigHelper.GetConfigInt("TimeLimit");
                if (dtHeartTime < DateTime.Now.AddSeconds(-TimeLimit) && ImportTemplateID > 0)
                {
                    SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(ImportTemplateID);
                    _bll.Model.State = 3;
                    _bll.Model.Remark = "导入出错，导入超时，请确认文件没有损坏或病毒再上传!";
                    _bll.Model.ImprotTime = DateTime.Now;
                    _bll.Update();

                    if (th_ImportService != null && th_ImportService.ThreadState == System.Threading.ThreadState.Running)
                    {
                        this.ImportTemplateID = 0;
                        th_ImportService.Abort();
                        th_ImportService.Join(5000);
                        KillProcess();

                        th_ImportService = new Thread(new ThreadStart(UploadExcel));
                        th_ImportService.Start();
                    }
                }
                Thread.Sleep(50000);
            }
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
        public delegate void MessageHandel(object Sender, MessageEventArgs Args);
    }
}
