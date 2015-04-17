using System;
using System.Collections.Generic;
using System.Linq;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;

using MCSFramework.Model;
using MCSFramework.BLL;
using MCSFramework.Model.VST;
using MCSFramework.BLL.VST;
using MCSFramework.Common;
using MCSFramework.BLL.PBM;
using MCSFramework.Model.PBM;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class ClientInfo
    {
        #region 门店信息字段
        /// <summary>
        /// 门店ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 门店编码(平台级编码)
        /// </summary>
        public string Code = "";

        /// <summary>
        /// 门店全称 
        /// </summary>
        public string FullName = "";

        /// <summary>
        /// 门店名称(必填)
        /// </summary>
        public string ShortName = "";

        /// <summary>
        /// 门店所在行政城市
        /// </summary>
        public int OfficialCity = 0;
        public string OfficialCityName = "";

        /// <summary>
        /// 地址
        /// </summary>
        public string Address = "";

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkManName = "";

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TeleNum = "";

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile = "";

        /// <summary>
        /// 客户类型
        /// </summary>
        public int ClientType = 0;

        /// <summary>
        /// 上级供货商ID
        /// </summary>
        public int Supplier = 0;
        public string SupplierName = "";

        /// <summary>
        /// 编码(经销商给门店设定的编码)
        /// </summary>
        public string CodeBySupplier = "";

        /// <summary>
        /// 供货价盘
        /// </summary>
        public int StandardPrice = 0;
        public string StandardPriceName = "";

        /// <summary>
        /// 经销商自分渠道
        /// </summary>
        public int TDPChannel = 0;
        public string TDPChannelName = "";

        /// <summary>
        /// 经销商自分区域
        /// </summary>
        public int TDPSalesArea = 0;
        public string TDPSalesAreaName = "";

        /// <summary>
        /// 销售路线
        /// </summary>
        public int VisitRoute = 0;
        public string VisitRouteName = "";

        /// <summary>
        /// 拜访顺序
        /// </summary>
        public int VisitSequence = 0;

        /// <summary>
        /// 拜访模板
        /// </summary>
        public int VisitTemplate = 0;
        public string VisitTemplateName = "";

        /// <summary>
        /// 业务员
        /// </summary>
        public int Salesman = 0;
        public string SalesmanName = "";

        /// <summary>
        /// 厂商级编码
        /// </summary>
        public string CodeByManufaturer = "";

        /// <summary>
        /// 纬度
        /// </summary>
        public float Latitude = 0;

        /// <summary>
        /// 经度
        /// </summary>
        public float Longitude = 0;

        /// <summary>
        /// 距离（仅查询时显示）
        /// </summary>
        public int Distance = 0;

        /// <summary>
        /// 应收款余额（仅查询时显示）
        /// </summary>
        public decimal ARBalance = 0;
        #endregion

        /// <summary>
        /// 门店供货价表
        /// </summary>
        public List<ProductList> ClientProductLists = new List<ProductList>();

        /// <summary>
        /// 附件图片
        /// </summary>
        public List<Attachment> Atts = new List<Attachment>();


        public ClientInfo() { }

        public ClientInfo(int ClientID, int TDP)
        {
            CM_ClientBLL bll = new CM_ClientBLL(ClientID);
            if (bll.Model == null) return;

            FillClientInfo(bll.Model, TDP);
        }

        public ClientInfo(CM_Client m, int TDP)
        {
            FillClientInfo(m, TDP);
        }

        private void FillClientInfo(CM_Client m, int TDP)
        {
            if (m == null) return;

            CM_ClientBLL ClientBLL = new CM_ClientBLL(m.ID);
            if (ClientBLL.Model == null) return;

            #region 绑定基本资料
            ID = m.ID;
            Code = m.Code;
            FullName = m.FullName == "" ? m.ShortName : m.FullName;
            ShortName = m.ShortName;
            OfficialCity = m.OfficialCity;
            Address = m.Address;
            ClientType = m.ClientType;
            LinkManName = m.LinkManName;
            TeleNum = m.TeleNum;
            Mobile = m.Mobile == "" ? m.TeleNum : m.Mobile;

            OfficialCityName = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", m.OfficialCity).Replace("->", " ");
            #endregion

            #region 绑定供货信息
            CM_ClientSupplierInfo SupplierInfo = ClientBLL.GetSupplierInfo(TDP);
            if (SupplierInfo != null)
            {
                CodeBySupplier = SupplierInfo.Code;
                Supplier = SupplierInfo.Supplier;
                StandardPrice = SupplierInfo.StandardPrice;
                TDPChannel = SupplierInfo.TDPChannel;
                TDPSalesArea = SupplierInfo.TDPSalesArea;
                VisitRoute = SupplierInfo.VisitRoute;
                VisitSequence = SupplierInfo.VisitSequence;
                VisitTemplate = SupplierInfo.VisitTemplate;

                #region 获取供货商名称
                CM_Client s = new CM_ClientBLL(Supplier).Model;
                if (s != null)
                {
                    SupplierName = s.ShortName == "" ? s.ShortName : s.FullName;
                }
                #endregion

                #region 获取价表名称
                if (StandardPrice != 0)
                {
                    PDT_StandardPrice price = new PDT_StandardPriceBLL(StandardPrice).Model;
                    if (price != null)
                    {
                        StandardPriceName = price.Name;
                    }
                }
                #endregion

                #region 获取渠道名称
                if (TDPChannel > 0)
                {
                    CM_RTChannel_TDP channel = new CM_RTChannel_TDPBLL(TDPChannel).Model;
                    if (channel != null) TDPChannelName = channel.Name;
                }
                #endregion

                #region 获取区域名称
                if (TDPSalesArea > 0)
                {
                    CM_RTSalesArea_TDP area = new CM_RTSalesArea_TDPBLL(TDPSalesArea).Model;
                    if (area != null) TDPSalesAreaName = area.Name;
                }
                #endregion

                #region 获取路线及拜访模板名称
                if (VisitRoute != 0)
                {
                    VST_Route r = new VST_RouteBLL(VisitRoute).Model;
                    if (r != null) VisitRouteName = r.Name;
                }

                if (VisitTemplate != 0)
                {
                    VST_VisitTemplate t = new VST_VisitTemplateBLL(VisitTemplate).Model;
                    if (t != null) VisitTemplateName = t.Name;
                }
                #endregion

                #region 获取销售员名称
                if (Salesman != 0)
                {
                    Org_Staff staff = new Org_StaffBLL(Salesman).Model;
                    if (staff != null) SalesmanName = staff.RealName;
                }
                #endregion


            }
            #endregion

            #region 绑定厂商管理信息
            int manufacturer = 0;           //归属厂商
            if (m.ClientType == 3)
            {
                //门店的归属厂商为当前TDP所归属的厂商
                CM_Client supplier = new CM_ClientBLL(TDP).Model;
                if (supplier != null) manufacturer = supplier.OwnerClient;
            }
            else if (m.ClientType == 2)
            {
                //TDP经销商的归属厂商
                manufacturer = m.OwnerClient;
            }

            CM_ClientManufactInfo ManufactInfo = ClientBLL.GetManufactInfo(manufacturer);
            if (ManufactInfo != null)
            {
                CodeByManufaturer = ManufactInfo.Code;
            }
            #endregion

            #region 绑定客户定位信息
            CM_ClientGeoInfo geo = CM_ClientGeoInfoBLL.GetGeoInfoByClient(m.ID);
            if (geo != null)
            {
                Latitude = (float)geo.Latitude;
                Longitude = (float)geo.Longitude;
            }
            #endregion

            #region 绑定客户供货产品目录
            ClientProductLists = new List<ProductList>();
            foreach (CM_ClientProductList item in CM_ClientProductListBLL.GetModelList
                ("Client=" + m.ID.ToString() + (TDP == 0 ? "" : " AND Supplier=" + TDP.ToString())))
            {
                ClientProductLists.Add(new ProductList(item));
            }
            #endregion

            #region 查询预收款余额
            AC_CurrentAccount ac = AC_CurrentAccountBLL.GetByTradeClient(TDP, m.ID);
            if (ac != null) ARBalance = ac.PreReceivedAmount - ac.AR;
            #endregion

            #region 获取附件明细
            IList<ATMT_Attachment> atts = ATMT_AttachmentBLL.GetAttachmentList(30, m.ID, DateTime.Today.AddMonths(-3), new DateTime(2100, 1, 1));
            Atts = new List<Attachment>(atts.Count);
            foreach (ATMT_Attachment item in atts.OrderByDescending(p => p.UploadTime))
            {
                Atts.Add(new Attachment(item));
            }
            #endregion

        }


        /// <summary>
        /// TDP向门店供货价表
        /// </summary>
        [Serializable]
        public class ProductList
        {
            /// <summary>
            /// 商品ID
            /// </summary>
            public int Product = 0;

            /// <summary>
            /// 销售价
            /// </summary>
            public decimal Price = 0;

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark = "";

            public ProductList() { }

            public ProductList(CM_ClientProductList m)
            {
                Product = m.Product;
                Price = m.Price;
                Remark = m.Remark;
            }
        }
    }

    [Serializable]
    public class WareHouse
    {
        public int ID = 0;
        public string Code = "";
        public string Name = "";
        public string TeleNum = "";
        public string Address = "";
        public string Keeper = "";
        public string Mobile = "";

        /// <summary>
        /// 仓库类别，1:主仓库 2:分仓库 3:车仓库 4:虚拟仓库5:报废仓库
        /// </summary>
        public int Classify = 0;

        public WareHouse() { }

        public WareHouse(CM_WareHouse m)
        {
            ID = m.ID;
            Code = m.Code;
            Name = m.Name;
            TeleNum = m.TeleNum;
            Address = m.Address;
            Keeper = m.Keeper;
            Mobile = m.Mobile;
            Classify = m.Classify;
        }

    }
}
