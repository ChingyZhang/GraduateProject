using System;
using System.Collections.Generic;
using System.Data;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.CM;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using SyncERPJXC.SQLDAL;
using System.Threading;
using System.IO;
using System.Linq;

namespace SyncERPJXC
{
    public class SyncDeliverySheet
    {
        private Thread _thSyncThread = null;
        private string _sClientCode = "";
        private DateTime _dtBeginDate, _dtEndDate;
        private bool _bIsSyncing = false;
        private bool _bCancelSync = false;
        private string _logfilepath = "\\Log";

        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string LogFilePath
        {
            get { return _logfilepath; }
            set { _logfilepath = value; }
        }

        /// <summary>
        /// 是否正在进行同步操作
        /// </summary>
        public bool IsSyncing
        {
            get { return _bIsSyncing; }
        }

        #region 事件消息定义
        private MessageHandel _message;
        public event MessageHandel OnMessage
        {
            add { _message += value; }
            remove { _message -= value; }
        }

        private SyncCompleteHandel _synccomplete;
        public event SyncCompleteHandel OnSyncComplete
        {
            add { _synccomplete += value; }
            remove { _synccomplete -= value; }
        }

        private void SendMessage(int code, string mess)
        {
            if (_message != null)
            {
                MessageEventArgs e = new MessageEventArgs(code, mess);
                _message.Invoke(this, e);

                #region 将错误消息写入日志文件
                try
                {
                    string filename = _logfilepath;
                    if (!filename.EndsWith("\\")) filename += "\\";

                    if (!Directory.Exists(filename)) Directory.CreateDirectory(filename);

                    if (code == 0)
                        filename += "Log" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                    else
                        filename += "Error" + DateTime.Today.ToString("yyyyMMdd") + ".txt";

                    StreamWriter stream = null;
                    if (File.Exists(filename))
                        stream = File.AppendText(filename);
                    else
                        stream = File.CreateText(filename);

                    stream.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (code != 0) stream.WriteLine("错误号:" + code.ToString());
                    stream.WriteLine(mess);
                    stream.WriteLine();

                    stream.Close();
                }
                catch { }
                #endregion
            }
        }

        private void SyncComplete()
        {
            _bIsSyncing = false;
            _thSyncThread = null;

            if (_synccomplete != null)
            {
                _synccomplete.Invoke(this, new EventArgs());
            }
        }
        #endregion

        #region 同步开启与停止控制
        /// <summary>
        /// 开始发起同步
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        public void RunSync(DateTime BeginDate, DateTime EndDate, string ClientCode)
        {
            if (_bIsSyncing) return;

            _bIsSyncing = true;
            _bCancelSync = false;

            _sClientCode = ClientCode;
            _dtBeginDate = BeginDate;
            _dtEndDate = EndDate;

            _thSyncThread = new Thread(new ThreadStart(SyncThread));
            _thSyncThread.Start();
        }

        public void RunSync(DateTime BeginDate, DateTime EndDate)
        {
            RunSync(BeginDate, EndDate, "");
        }
        /// <summary>
        /// 取消同步
        /// </summary>
        public void CancelSync()
        {
            if (_bIsSyncing && _thSyncThread != null)
            {
                _bCancelSync = true;
            }
        }
        #endregion

        /// <summary>
        /// 同步进销存
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        private void SyncThread()
        {
            DateTime begindate = _dtBeginDate, enddate = _dtEndDate;
            SendMessage(0, "--------开始同步ERP发货单----------");

            try
            {
                SyncProduct();

                if (string.IsNullOrEmpty(_sClientCode))
                {
                    if (begindate.Date == enddate.Date)
                    {
                        SyncSellOut(begindate, enddate, "");
                    }
                    else
                    {
                        IList<CM_Client> lists = CM_ClientBLL.GetModelList("ClientType=2 AND ActiveFlag=1 AND ApproveFlag=1 AND MCS_SYS.dbo.uf_Spilt2('MCS_CM.dbo.CM_Client',ExtPropertys,'DIClassify') IN ('1','3')");
                        foreach (CM_Client c in lists)
                        {
                            if (_bCancelSync)
                            {
                                SendMessage(0, "同步进行中时,被用户取消！");
                                break;
                            }

                            if (!string.IsNullOrEmpty(c.Code)) SyncSellOut(begindate, enddate, c.Code);
                        }
                    }
                }
                else
                {
                    SyncSellOut(begindate, enddate, _sClientCode.Trim());
                }

            }
            catch (Exception err)
            {
                SendMessage(0, err.StackTrace + err.Source + err.Message);
            }

            SyncComplete();
        }

        /// <summary>
        /// 同步产品
        /// </summary>
        private void SyncProduct()
        {
            try
            {
                int syncproductcount = ERPIF.Sync_Product();
                if (syncproductcount > 0)
                {
                    SendMessage(0, string.Format("从ERP物料库中同步了{0}个新产品物料！", syncproductcount));
                }
            }
            catch (Exception err)
            {
                SendMessage(0, "SyncProduct()" + err.StackTrace + err.Source + err.Message);
            }
        }

        /// <summary>
        /// 同步经销商进货单
        /// </summary>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="clientcode"></param>
        private void SyncSellOut(DateTime begindate, DateTime enddate, string clientcode)
        {
            try
            {
                int success = 0;

                //从进销存数据库中读取出货数据
                DataTable dtShipHeader = null;

                if (string.IsNullOrEmpty(clientcode))
                {
                    dtShipHeader = ERPIF.GetShipHeader(begindate, enddate);
                }
                else
                {
                    SendMessage(0, "经销商代码:" + clientcode);
                    dtShipHeader = ERPIF.GetShipHeader(begindate, enddate, clientcode);
                }
                SendMessage(0, "本次共获取到" + dtShipHeader.Rows.Count.ToString() + "条发货单待同步！" + _sClientCode);

                for (int i = 0; i < dtShipHeader.Rows.Count; i++)
                {
                    DataRow dr = dtShipHeader.Rows[i];

                    if (_bCancelSync)
                    {
                        SendMessage(0, "同步进行中时,被用户取消！");
                        break;
                    }
                    #region 逐条同步发货单
                    try
                    {
                        string SheetCode = dr["DELIVERY_ID"].ToString().Trim();
                        string ClientCode = dr["CUST_NUMBER"].ToString().Trim();
                        DateTime SalesDate = DateTime.Parse(dr["CARRY_SDATE"].ToString().Trim());
                        string DJType = dr["ORDER_TYPE"].ToString().Trim();
                        decimal discount = 0;
                        decimal.TryParse(dr["DISCOUNT"].ToString().Trim(), out discount);

                        if (DJType == "R") continue;

                        if (ORD_OrderDeliveryBLL.GetModelList(" SheetCode = '" + SheetCode + "'").Count > 0)
                        {
                            continue;
                        }

                        #region 判断客户号有效否
                        IList<CM_Client> clients = CM_ClientBLL.GetModelList(" Code= '" + ClientCode.ToString() + "' AND ActiveFlag=1 AND ApproveFlag=1");
                        if (clients.Count == 0)
                        {
                            SendMessage(-11, "经销商编号为" + ClientCode.ToString() + "的匹配错误！");
                            continue;
                        }
                        CM_Client client = clients[0];
                        if (client["DIClassify"] == "3")
                        {
                            //经销商子户头
                            client = new CM_ClientBLL(client.Supplier).Model;
                            if (client == null)
                            {
                                SendMessage(-12, "经销商编号为" + ClientCode.ToString() + "对应的经销商为子户头，但未关联正确的主户头！");
                                continue;
                            }
                        }

                        if (client["DIClassify"] != "1")
                        {
                            SendMessage(-13, "经销商编号为" + ClientCode.ToString() + "对应的经销商不是主户头！");
                            continue;
                        }
                        #endregion

                        #region 生成销量单头
                        ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL();
                        bll.Model.SheetCode = SheetCode;
                        bll.Model.AccountMonth = AC_AccountMonthBLL.GetMonthByDate(SalesDate);
                        bll.Model.OrganizeCity = client.OrganizeCity;
                        bll.Model.State = 1;        //未发货
                        bll.Model.Client = client.ID;
                        bll.Model.Store = client.Supplier;
                        if (dr["CARRY_EDATE"].ToString() != "")
                            bll.Model.PreArrivalDate = DateTime.Parse(dr["CARRY_EDATE"].ToString().Trim());
                        else
                            bll.Model.PreArrivalDate = SalesDate.AddDays(7);      //默认7天后到货
                        bll.Model.ApproveFlag = 1;
                        bll.Model.InsertStaff = 1;
                        bll.Model["DeliveryTime"] = SalesDate.ToString("yyyy-MM-dd HH:mm");
                        bll.Model["TruckNo"] = dr["CARRY_CAR"].ToString().Trim();
                        bll.Model["DeliveryStaff"] = "1";
                        bll.Model["DeliveryRemark"] = "ERP发货单提取";
                        bll.Model["Discount"] = discount.ToString("0.###");
                        bll.Model["CarrySheetCode"] = dr["REQUEST_NUMBER"].ToString().Trim();
                        #endregion

                        //获取进销存明细
                        DataTable dt_detail = ERPIF.GetShipDetail(SheetCode);

                        #region 生成销量明细
                        foreach (DataRow dr_detail in dt_detail.Rows)
                        {
                            string productcode = dr_detail["ITEM_NO"].ToString();
                            decimal price = decimal.Parse(dr_detail["PRICE"].ToString());
                            int quantity = (int)decimal.Parse(dr_detail["SHIP_QTY"].ToString());
                            string packaging = dr_detail["UOM"].ToString();

                            if (quantity == 0) continue;

                            #region 验证产品信息
                            PDT_Product product = null;
                            if (price == 0)
                            {
                                #region 价格为0，表示为赠品,取赠品目录中的物料
                                IList<PDT_Product> _products = PDT_ProductBLL.GetModelList("Code='" + productcode + "' AND Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent=9)");
                                if (_products.Count > 0)
                                {
                                    product = _products[0];
                                }
                                else
                                {
                                    _products = PDT_ProductBLL.GetModelList("Code='" + productcode + "' AND Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent <> 9)");
                                    if (_products.Count == 0)
                                    {
                                        SendMessage(-21, string.Format("发货单号为{0}内,产品编号{1}未在产品目录库中未找到！", SheetCode, productcode));
                                        continue;
                                    }
                                    else
                                    {
                                        //在成品目录里找到，但未在赠品目录中，自动在赠品目录中增加一条本品赠品目录
                                        PDT_ProductBLL productbll = new PDT_ProductBLL();
                                        productbll.Model = _products[0];
                                        productbll.Model.Brand = 3;         //本品赠品
                                        productbll.Model.Classify = 100;    //其他品类
                                        productbll.Model.State = 2;         //停用
                                        productbll.Model.ApproveFlag = 2;   //未审核
                                        int productid = productbll.Add();

                                        product = new PDT_ProductBLL(productid).Model;
                                        SendMessage(0, string.Format("发货单号为{0}内,产品编号{1}未在本品赠品目录库中未找到，自动从产品库中增加该赠品信息！", SheetCode, productcode));
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region 从产品目录中取该产品信息
                                IList<PDT_Product> _products = PDT_ProductBLL.GetModelList("Code='" + productcode + "' AND Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent<>9)");
                                if (_products.Count > 0)
                                {
                                    product = _products[0];
                                }
                                else
                                {
                                    _products = PDT_ProductBLL.GetModelList("Code='" + productcode + "' AND Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent = 9)");
                                    if (_products.Count == 0)
                                    {
                                        SendMessage(-22, string.Format("发货单号为{0}内,产品编号{1}未在产品物料目录库中未找到！", SheetCode, productcode));
                                        continue;
                                    }
                                    else
                                    {
                                        //在赠品目录中找到，则使用赠品目录的产品ID
                                        //不在产品中增加该成品 2012-04-25
                                        product = _products[0];

                                        ////在赠品目录里找到，但未在成品目录中，自动在成品目录中增加一条本品成品目录
                                        //PDT_ProductBLL productbll = new PDT_ProductBLL();
                                        //productbll.Model = _products[0];
                                        //productbll.Model.Brand = 1;
                                        //productbll.Model.Classify = 1;
                                        //productbll.Model.State = 2;         //停用
                                        //productbll.Model.ApproveFlag = 2;   //未审核
                                        //int productid = productbll.Add();

                                        //product = new PDT_ProductBLL(productid).Model;
                                        //SendMessage(0, string.Format("发货单号为{0}内,产品编号{1}未在成品目录库中未找到，自动从赠品库中增加该产品信息！", SheetCode, productcode));
                                    }

                                }
                                #endregion
                            }

                            if (product.State == 2 && product.ApproveFlag == 1)
                            {
                                //确认被停用的产品，不再同步
                                continue;
                            }

                            if (product.State == 3 && !string.IsNullOrEmpty(product["MasterProduct"]) && product["MasterProduct"] != "0")
                            {
                                //是子产品，自动归属到主产品
                                PDT_Product _p = new PDT_ProductBLL(int.Parse(product["MasterProduct"])).Model;
                                if (_p != null) product = _p;
                            }

                            if (product == null)
                            {
                                SendMessage(-23, productcode + "为无效产品编号！");
                                continue;
                            }
                            #endregion

                            if (packaging == "Ea" && product.ConvertFactor > 1)
                            {
                                //整箱单位，转换为零售单位
                                quantity = quantity * product.ConvertFactor;
                                price = price / product.ConvertFactor;
                            }

                            ORD_OrderDeliveryDetail item = bll.Items.FirstOrDefault
                                (p => p.Product == product.ID && p.Client == client.ID);

                            if (item != null)
                            {
                                item.DeliveryQuantity += quantity;
                            }
                            else
                            {
                                item = new ORD_OrderDeliveryDetail();
                                item.Client = client.ID;
                                item.Product = product.ID;
                                item.Price = price;
                                item.FactoryPrice = price == 0 ? product.FactoryPrice : price;
                                item.DeliveryQuantity = quantity;
                                item.SignInQuantity = 0;
                                item.BadQuantity = 0;
                                item.LostQuantity = 0;

                                bll.Items.Add(item);
                            }
                        }
                        #endregion

                        if (bll.Items.Count == 0)
                        {
                            SendMessage(0, "发货单号" + bll.Model.SheetCode + "，未获取到发货单产品明细");
                            continue;
                        }

                        #region 写入发货单
                        if (bll.Add() > 0)
                        {
                            //bll.Delivery(1);        //设为发货状态(20140708修改为数据库作业执行)
                            SendMessage(0, "成功导入发货数据！单号:" + bll.Model.SheetCode + ",发生日期:" + SalesDate.ToString("yyyy-MM-dd") + ",产品明细条数:" + bll.Items.Count.ToString() + ",同步进度:" + (i + 1).ToString() + "/" + dtShipHeader.Rows.Count.ToString());
                        }
                        else
                        {
                            SendMessage(-40, "导入发货数据失败！单号" + bll.Model.SheetCode);
                            continue;
                        }
                        #endregion
                        success += 1;
                    }
                    catch (Exception err)
                    {
                        SendMessage(0, err.StackTrace + err.Source + err.Message);
                        continue;
                    }
                    #endregion
                }

                SendMessage(0, "------" + begindate.ToString("yyyy-MM-dd HH:mm") +
                    "至" + enddate.ToString("yyyy-MM-dd HH:mm") +
                    "。总共同步完成" + success.ToString() + "条发货单记录！-------");
            }
            catch (Exception err)
            {
                SendMessage(0, "SyncSellOut()" + err.StackTrace + err.Source + err.Message);
            }
        }
    }

    public class MessageEventArgs : EventArgs
    {
        private int _MessageCode;
        private string _Message;

        public int MessageCode
        {
            get { return _MessageCode; }
            set { _MessageCode = value; }
        }

        public string Message
        {
            get { return _Message; }
        }

        public MessageEventArgs(int code, string message)
        {
            _MessageCode = code;
            _Message = message;
        }
    }
    public delegate void MessageHandel(object Sender, MessageEventArgs Args);
    public delegate void SyncCompleteHandel(object Sender, EventArgs Args);
}
