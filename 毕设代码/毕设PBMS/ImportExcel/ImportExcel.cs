using MCSFramework.BLL;
using MCSFramework.BLL.IPT;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.IPT;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using System.Data;
using ImportExcelTDP;
using MCSFramework.BLL.VST;
using MCSFramework.BLL.PBM;
using System.Net;
using MCSFramework.Model.PBM;

namespace ImportExcel
{
    class ImportExcel
    {
        #region 基础字段及方法
        //private Thread th_DownService;
        private Thread th_ImportService;
        private Thread th_HeartService;

        //从SAP下载发货单
        private Thread th_ImportSAPService;

        private bool bIsRunning = false;
        private DateTime dtHeartTime = DateTime.Now;
        private int ImportTemplateID = 0;   //当前正在导入处理的EXCEL数据表ID
        public delegate void MessageHandel(object Sender, MessageEventArgs Args);
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
        //构造方法
        public ImportExcel()
        {

        }
        #endregion

        #region 消息时间定义

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
        public string trim(string Str)
        {
            string newstr = Str.Replace("　", "");
            newstr = newstr.Trim();
            return newstr;
        }
        #endregion

        #region 线程开始
        public void Start()
        {
            bIsRunning = true;

            th_ImportService = new Thread(new ThreadStart(UploadExcel));
            th_ImportService.Start();

            //th_HeartService = new Thread(new ThreadStart(ThreadCheckHeartTime));
            //th_HeartService.Start();

            th_ImportSAPService = new Thread(new ThreadStart(ImportSAPService));
            th_ImportSAPService.Start();

            SendMessage("", "导入服务成功发起！");
        }
        #endregion

        #region 线程结束
        public void Stop()
        {
            bIsRunning = false;
            Thread.Sleep(1000);

            if (th_HeartService != null &&
                (th_HeartService.ThreadState == ThreadState.Running || th_HeartService.ThreadState == ThreadState.WaitSleepJoin)
                )
            {
                th_HeartService.Abort();
                th_HeartService.Join();
            }

            if (th_ImportService != null &&
                (th_ImportService.ThreadState == ThreadState.Running || th_ImportService.ThreadState == ThreadState.WaitSleepJoin)
                )
            {
                th_ImportService.Abort();
                th_ImportService.Join();
            }

            if (th_ImportSAPService != null &&
                (th_ImportSAPService.ThreadState == ThreadState.Running || th_ImportSAPService.ThreadState == ThreadState.WaitSleepJoin)
                )
            {
                th_ImportSAPService.Abort();
                th_ImportSAPService.Join();
            }


            SendMessage("", "导入服务成功停止！");

        }
        #endregion

        #region 线程检查
        private void ThreadCheckHeartTime()
        {
            while (bIsRunning)
            {
                int TimeLimit = ConfigHelper.GetConfigInt("TimeLimit");
                if (dtHeartTime < DateTime.Now.AddSeconds(-TimeLimit) && ImportTemplateID > 0)
                {
                    IPT_UploadTemplateBLL _bll = new IPT_UploadTemplateBLL(ImportTemplateID);
                    _bll.Model.State = 3;
                    _bll.Model.Remark = "导入出错，导入超时，请确认文件没有损坏或病毒再上传!";
                    _bll.Model.ImportTime = DateTime.Now;
                    _bll.Update();

                    if (th_ImportService != null && th_ImportService.ThreadState == System.Threading.ThreadState.Running)
                    {
                        this.ImportTemplateID = 0;
                        th_ImportService.Abort();
                        th_ImportService.Join(5000);

                        th_ImportService = new Thread(new ThreadStart(UploadExcel));
                        th_ImportService.Start();
                    }
                }
                Thread.Sleep(50000);
            }
        }
        #endregion

        #region 导入Excel
        public void UploadExcel()
        {
            while (bIsRunning)
            {
                try
                {
                    #region 获取最迟的销量月份
                    int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
                    #endregion
                    IList<IPT_UploadTemplate> uplists = IPT_UploadTemplateBLL.GetModelList("State=1");
                    if (uplists.Count == 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }

                    foreach (IPT_UploadTemplate m in uplists)
                    {
                        dtHeartTime = DateTime.Now;
                        ImportTemplateID = m.ID;

                        try
                        {
                            #region 组织文件路径及文件名
                            string path = m.FullFileName;
                            if (path.EndsWith("\\")) path = path + "\\";

                            string filename = m.ShortFileName;
                            #endregion
                            SendMessage("UploadExcel.UploadExcel", "开始处理文件" + filename);
                            switch (m.FileType)
                            {
                                case 1:
                                    this.ImportBaseClientInfo(m.ID, path);
                                    break;
                                default:
                                    break;
                            }
                            this.ImportTemplateID = 0;
                        }
                        catch (System.Exception err)
                        {
                            IPT_UploadTemplateBLL _bll = new IPT_UploadTemplateBLL(m.ID);
                            _bll.Model.State = 3;
                            _bll.Model.Remark = "导入出错，异常：" + err.Message;
                            _bll.Model.ImportTime = DateTime.Now;
                            _bll.Update();
                            this.ImportTemplateID = 0;

                        }
                    }
                }
                catch (ThreadAbortException e)
                {
                    this.SendMessage("UploadExcel.ThreadAbortException", this.ImportTemplateID.ToString() + e.Message);
                    Thread.Sleep(10000);
                    continue;
                }
                catch (Exception ex)
                {
                    this.SendMessage("UploadExcel.Exception", ex.Message);
                    Thread.Sleep(10000);
                    continue;
                }

            }
        }

        #endregion

        #region 导入方法
        private void ImportBaseClientInfo(int templateid, string path)
        {
            IPT_UploadTemplateBLL _bll = new IPT_UploadTemplateBLL(templateid);
            if (_bll.Model.State != 1) return;//模版作废
            string message = "";
            int State = 0;//五个Sheet导入结果的合计
            #region 导入Excel文件
            if (!File.Exists(path))
            {
                message += "Excel在当前路径中不存在！\r\n";
                State = 20;
            }
            else
            {
                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open);
                    IWorkbook _ibook = WorkbookFactory.Create(fs);
                    if (_ibook.GetSheet("商品资料") == null || _ibook.GetSheet("客户资料") == null || _ibook.GetSheet("供货单位信息") == null || _ibook.GetSheet("期初库存") == null)
                    {
                        message += "Excel表格缺少Sheet表单\r\n";
                        State = 20;
                    }
                    else
                    {
                        ClientInit _ClientInit = new ClientInit();
                        int _State = 0;
                        message += _ClientInit.DoImportProduct(templateid, _bll.Model.ClientID, _ibook.GetSheet("商品资料"), out _State);
                        message += "\r\n";
                        State += _State;
                        message += _ClientInit.DoImportClient(templateid, _bll.Model.ClientID, _ibook.GetSheet("客户资料"), out _State);
                        message += "\r\n";
                        State += _State;
                        message += _ClientInit.DoImportSupplier(templateid, _bll.Model.ClientID, _ibook.GetSheet("供货单位信息"), out _State);
                        message += "\r\n";
                        State += _State;
                        message += _ClientInit.DoImportInventory(templateid, _bll.Model.ClientID, _ibook.GetSheet("期初库存"), out _State);
                        message += "\r\n";
                        State += _State;
                        fs.Close();
                        fs.Dispose();
                    }
                }
                catch (System.Exception err)
                {
                    string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                        "StackTrace:" + err.StackTrace + "<br/>";
                    message += "系统错误-4!" + err.Message;
                    _bll.Model.Remark = message;
                }
            }
            #endregion

            if (State == 15) _bll.Model.State = 3;
            else if (State == 20) _bll.Model.State = 4;
            else _bll.Model.State = 5;
            _bll.Update();
            this.SendMessage("UploadExcel.导入提示!", message);
        }
        #endregion

        public void ImportSAPService()
        {
            while (bIsRunning)
            {
                try
                {
                    ImportExcelTDP.SAP_ZSD.Z_SD_SD_SHService sd = new ImportExcelTDP.SAP_ZSD.Z_SD_SD_SHService();

                    NetworkCredential c = new NetworkCredential();
                    c.UserName = ConfigHelper.GetConfigString("SAP_ZSD_User");
                    c.Password = ConfigHelper.GetConfigString("SAP_ZSD_Password");
                    sd.Credentials = c;

                    ImportExcelTDP.SAP_ZSD.Ztdplog[] results1 = new ImportExcelTDP.SAP_ZSD.Ztdplog[1024];
                    ImportExcelTDP.SAP_ZSD.Ztdplog[] results2 = new ImportExcelTDP.SAP_ZSD.Ztdplog[1024];
                    sd.ZSdSdSh("1", "5200", DateTime.Today.AddDays(-1).ToString("yyyyMMdd"), ref results1);
                    sd.ZSdSdSh("2", "5200", DateTime.Today.AddDays(-1).ToString("yyyyMMdd"), ref results1);
                    sd.ZSdSdSh("1", "5200", DateTime.Today.ToString("yyyyMMdd"), ref results1);
                    sd.ZSdSdSh("2", "5200", DateTime.Today.ToString("yyyyMMdd"), ref results2);

                    List<ImportExcelTDP.SAP_ZSD.Ztdplog> results = new List<ImportExcelTDP.SAP_ZSD.Ztdplog>();
                    results.AddRange(results1);
                    results.AddRange(results2);
                    SendMessage("ImportSAPService", "获取到" + results.Count().ToString() + "条发货数量！");

                    if (results.Count() == 0) { Thread.Sleep(3600 * 1000); continue; }

                    PBM_DeliveryBLL bll = new PBM_DeliveryBLL();
                    decimal totalamount = 0;
                    foreach (ImportExcelTDP.SAP_ZSD.Ztdplog item in results.OrderBy(p => p.Vgbel))
                    {
                        if (item.Vgbel != bll.Model.SheetCode)
                        {
                            #region 更新发货单
                            if (bll.Model.SheetCode != "" && bll.Items.Count > 0)
                            {
                                //计算折扣金额
                                bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                                    Math.Round(p.Price * p.ConvertFactor, 2) * p.DeliveryQuantity / p.ConvertFactor);

                                //明细合计金额
                                decimal actamount = Math.Round(bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor), 2);

                                //优惠金额(明细合计金额与实际采购金额的差额)
                                bll.Model.WipeAmount = actamount - totalamount;

                                //计算实际销售金额
                                bll.Model.ActAmount = (bll.Model.Classify == 12 ? -1 : 1) * (actamount - bll.Model.WipeAmount);



                                int ret = bll.Add();
                                if (ret > 0)
                                    SendMessage("ImportSAPService", "单据号" + item.Vgbel + ",正在提交，总金额" + bll.Model.ActAmount.ToString("0.##") + "!");
                                else
                                    SendMessage("ImportSAPService", "单据号" + item.Vgbel + ",提交失败!Ret=" + ret.ToString());

                                bll = null;
                                totalamount = 0;
                            }
                            #endregion

                            #region 新增采购单据

                            //判断单号有没有导入过
                            if (PBM_DeliveryBLL.GetModelList("SheetCode='" + item.Vgbel + "' AND Supplier=1000 AND Classify IN (11,12)").Count > 0)
                            {
                                SendMessage("ImportSAPService", "单据号" + item.Vgbel + ",之前已存在!");
                                continue;
                            }

                            bll = new PBM_DeliveryBLL();
                            bll.Model.Classify = 11;

                            #region 判断TDP编码Kunage
                            string tdpcode = item.Kunag.Substring(3);
                            IList<CM_ClientManufactInfo> _clients = CM_ClientManufactInfoBLL.GetModelList("Code='" + tdpcode + "' AND Manufacturer=1000 AND State = 1");
                            if (_clients.Count == 0)
                            {
                                SendMessage("ImportSAPService", "门店编码无效，单据号" + item.Vgbel + ",客户编号:" + tdpcode);
                                continue;
                            }
                            #endregion

                            bll.Model.SheetCode = item.Vgbel;
                            bll.Model.Supplier = 1000;
                            bll.Model.Client = _clients[0].Client;

                            #region 获取经销商主仓库
                            IList<CM_WareHouse> _warehouses = CM_WareHouseBLL.GetEnbaledByClient(bll.Model.Client).Where(p => p.Classify == 1).OrderBy(p => p.ID).ToList();
                            if (_warehouses.Count == 0)
                            {
                                SendMessage("ImportSAPService", "未找到客户主仓库，单据号" + item.Vgbel + ",客户编号:" + tdpcode);
                                continue;
                            }
                            #endregion

                            bll.Model.ClientWareHouse = _warehouses[0].ID;
                            bll.Model.PrepareMode = 1;
                            bll.Model.State = 1;        //默认制单状态
                            bll.Model.WipeAmount = 0;
                            bll.Model.Remark = "SAP导入";
                            bll.Model.ApproveFlag = 2;
                            bll.Model.InsertStaff = 1;
                            DateTime _prearrivaldate = DateTime.Today;
                            DateTime.TryParse(item.WadatIst, out _prearrivaldate);
                            bll.Model.PreArrivalDate = _prearrivaldate;
                            #endregion
                        }

                        #region 新增商品明细
                        string productcode = item.Matnr.Substring(item.Matnr.Length - 6);
                        IList<PDT_Product> _products = PDT_ProductBLL.GetModelList("FactoryCode='" + productcode + "' AND State=1");
                        if (_products.Count == 0)
                        {
                            SendMessage("ImportSAPService", "商品编码无效，单据号" + item.Vgbel + ",商品编号:" + productcode);
                            continue;
                        }
                        int productid = _products[0].ID;
                        PDT_ProductBLL productbll = new PDT_ProductBLL(productid);


                        PBM_DeliveryDetail d = new PBM_DeliveryDetail();
                        d.Product = productid;
                        d.ConvertFactor = _products[0].ConvertFactor == 0 ? 1 : _products[0].ConvertFactor;
                        d.SalesMode = 1;
                        d.DeliveryQuantity = (int)Math.Round(item.Fkimg * d.ConvertFactor, 0);

                        if (d.DeliveryQuantity < 0)
                        {
                            bll.Model.Classify = 12;        //采购退库单
                            d.DeliveryQuantity = 0 - d.DeliveryQuantity;
                        }

                        d.SignInQuantity = d.DeliveryQuantity;
                        d.CostPrice = Math.Round(item.Kzwi / d.DeliveryQuantity, 4);
                        d.Price = d.CostPrice;
                        d.DiscountRate = Math.Round(item.Kzwi5 / item.Kzwi, 4);
                        d.LotNumber = item.Charg;
                        totalamount += item.Kzwi5;

                        DateTime _productdate = new DateTime(1900, 1, 1);
                        DateTime.TryParse(item.Hsdat, out _productdate);

                        if (_productdate.Year < 1900) _productdate = new DateTime(1900, 1, 1);
                        d.ProductDate = _productdate;
                        d.Remark = item.Fkart;

                        bll.Items.Add(d);
                        #endregion

                        #region 判断是否在默认价表中
                        PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Client);
                        if (extinfo == null)
                        {
                            extinfo = new PDT_ProductExtInfo();
                            extinfo.Supplier = bll.Model.Client;
                            extinfo.Product = productid;
                            extinfo.Category = productbll.Model.Category;
                            extinfo.Code = productbll.Model.FactoryCode;
                            extinfo.SalesState = 1;
                            extinfo.BuyPrice = d.Price;
                            extinfo.SalesPrice = d.Price;
                            new PDT_ProductBLL(productid).SetProductExtInfo(extinfo);
                        }
                        #endregion
                    }

                    #region 更新发货单
                    if (bll.Model.SheetCode != "" && bll.Items.Count > 0)
                    {
                        //计算折扣金额
                        bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                            Math.Round(p.Price * p.ConvertFactor, 2) * p.DeliveryQuantity / p.ConvertFactor);

                        //计算实际销售金额
                        bll.Model.ActAmount = Math.Round((bll.Model.Classify == 12 ? -1 : 1) *
        bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor)
        - bll.Model.WipeAmount, 2);

                        int ret = bll.Add();
                        if (ret > 0)
                            SendMessage("ImportSAPService", "单据号" + bll.Model.SheetCode + ",提交成功，总金额" + bll.Model.ActAmount.ToString("0.##") + "!");
                        else
                            SendMessage("ImportSAPService", "单据号" + bll.Model.SheetCode + ",提交失败!Ret=" + ret.ToString());
                    }
                    #endregion

                    Thread.Sleep(3600 * 1000);
                }
                catch (ThreadAbortException e)
                {
                    this.SendMessage("ImportSAPService.ThreadAbortException", e.Message);
                    Thread.Sleep(10000);
                    continue;
                }
                catch (Exception ex)
                {
                    this.SendMessage("ImportSAPService.Exception", ex.Message);
                    Thread.Sleep(10000);
                    continue;
                }
            }
        }

    }

}