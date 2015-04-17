using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;
using MCSFramework.WSI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.VST;
using MCSFramework.Model.VST;
using System.Data;

namespace MCSFramework.WSI
{
    public class PBMService
    {
        public PBMService()
        {
            LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
        }

        #region TDP经销商信息管理

        #region 获取当前员工可管辖的经销商
        /// <summary>
        /// 获取当前员工可管辖的经销商
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static List<ClientInfo> GetTDPList(UserInfo User)
        {
            LogWriter.WriteLog("PBMIFService.GetTDPList:UserName=" + User.UserName);

            IList<CM_Client> clients = GetManagerTDPByStaff(User);
            List<ClientInfo> lists = new List<ClientInfo>(clients.Count);
            foreach (CM_Client item in clients)
            {
                lists.Add(new ClientInfo(item, User.OwnerType == 2 ? User.ClientID : 0));
            }

            return lists;
        }

        /// <summary>
        /// 获取当前用户可管辖的TDP
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        private static IList<CM_Client> GetManagerTDPByStaff(UserInfo User)
        {
            if (User.OwnerType == 3)
            {
                //TDP员工
                return CM_ClientBLL.GetModelList("ID=" + User.ClientID);
            }
            else
            {
                //公司员工
                string ConditionStr = "ClientType = 2 AND CM_Client.ID IN (SELECT Client FROM dbo.CM_ClientManufactInfo WHERE State = 1 ";

                //获取员工管理的区域
                if (User.OrganizeCity > 1)
                {
                    Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(User.OrganizeCity, true);
                    string orgcitys = orgcity.GetAllChildNodeIDs();
                    if (orgcitys != "") orgcitys += ",";
                    orgcitys += User.OrganizeCity.ToString();

                    if (orgcitys != "") ConditionStr += " AND OrganizeCity IN (" + orgcitys + ")";
                }
                ConditionStr += ")";

                return CM_ClientBLL.GetModelList(ConditionStr);
            }
        }
        #endregion

        #region 获取当前员工关联的送货车辆
        /// <summary>
        /// 获取当前员工关联的送货车辆,如果员工没有关联到车辆，则无法开展车销、送货服务
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static List<Vehicle> GetRelateVehicleList(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetRelateVehicleList:UserName=" + User.UserName);
            IList<CM_Vehicle> vehicles = null;

            if (User.OwnerType == 3)
            {
                vehicles = CM_VehicleInStaffBLL.GetVehicleByStaff(User.StaffID);

            }
            else if (User.OwnerType == 2)
            {
                if (TDP > 0)
                {
                    vehicles = CM_VehicleBLL.GetVehicleByClient(TDP);
                }
                else
                {
                    IList<CM_Client> clients = GetManagerTDPByStaff(User);
                    if (clients == null || clients.Count == 0) return null;
                    vehicles = CM_VehicleBLL.GetVehicleByClient(clients[0].ID);
                }
            }

            if (vehicles == null) return null;
            List<Vehicle> list = new List<Vehicle>(vehicles.Count);

            foreach (CM_Vehicle item in vehicles)
            {
                list.Add(new Vehicle(item));
            }
            return list;
        }
        #endregion

        #region 获取经销商详细资料
        /// <summary>
        /// 获取经销商详细资料
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static ClientInfo GetTDP_Info(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_Info:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            return new ClientInfo(TDP, User.OwnerType == 2 ? User.ClientID : 0);
        }
        #endregion

        #region 获取经销商自营销售渠道
        /// <summary>
        /// 获取经销商自营销售渠道
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetTDP_RTChannel(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_RTChannel:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<CM_RTChannel_TDP> channels = CM_RTChannel_TDPBLL.GetModelList("OwnerClient=" + TDP.ToString());
            List<DicDataItem> list = new List<DicDataItem>(channels.Count);
            foreach (CM_RTChannel_TDP item in channels)
            {
                list.Add(new DicDataItem(item.ID, item.Name, item.Code, item.Remark));
            }
            return list;
        }
        #endregion

        #region 获取经销商自营销售区域
        /// <summary>
        /// 获取经销商自营销售区域
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetTDP_RTSalesArea(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_RTSalesArea:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<CM_RTSalesArea_TDP> channels = CM_RTSalesArea_TDPBLL.GetModelList("OwnerClient=" + TDP.ToString());
            List<DicDataItem> list = new List<DicDataItem>(channels.Count);
            foreach (CM_RTSalesArea_TDP item in channels)
            {
                list.Add(new DicDataItem(item.ID, item.Name, item.Code, item.Remark));
            }
            return list;
        }
        #endregion

        #region 获取经销商销售价表
        /// <summary>
        /// 获取经销商销售价表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<TDPStandardPrice> GetTDP_StandardPrice(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_StandardPrice:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<PDT_StandardPrice> prices = PDT_StandardPriceBLL.GetAllPrice_BySupplier(TDP);
            List<TDPStandardPrice> list = new List<TDPStandardPrice>(prices.Count);
            foreach (PDT_StandardPrice item in prices)
            {
                list.Add(new TDPStandardPrice(item));
            }
            return list;
        }
        #endregion

        #region 获取经销商经营产品目录
        /// <summary>
        /// 获取经销商经营产品目录
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<Product> GetTDP_ProductList(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_ProductList:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<PDT_ProductExtInfo> ProductExtInfos = PDT_ProductExtInfoBLL.GetProductExtInfoList_BySupplier(TDP);

            List<Product> list = new List<Product>(ProductExtInfos.Count);
            foreach (PDT_ProductExtInfo item in ProductExtInfos)
            {
                if (INV_InventoryBLL.GetProductQuantityAllWareHouse(TDP, item.Product) == 0)
                {
                    //如果所有仓库库存均为0,则忽略掉该产品
                    continue;
                }
                list.Add(new Product(item.Product, TDP));
            }
            return list;
        }
        #endregion

        #region 获取TDP所属仓库目录
        /// <summary>
        /// 获取TDP所属仓库目录
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<WareHouse> GetTDP_WareHouseList(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetTDP_WareHouseList:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            List<WareHouse> WareHouses = new List<WareHouse>();
            foreach (CM_WareHouse w in CM_WareHouseBLL.GetEnbaledByClient(TDP))
            {
                WareHouses.Add(new WareHouse(w));
            }

            return WareHouses;
        }
        #endregion

        #endregion

        #region 门店信息管理

        #region 获取所有管辖的门店列表
        /// <summary>
        /// 获取所有管辖的门店列表
        /// </summary>
        /// <returns></returns>
        public static List<ClientInfo> GetRetailerList(UserInfo User, int TDP, string FindKey, int PageSize, int PageIndex, out int Counts)
        {
            Counts = 0;
            LogWriter.WriteLog("PBMIFService.GetRetailerList:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",FindKey=" + FindKey + ",PageSize=" + PageSize.ToString() + ",PageIndex=" + PageIndex.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            #region 设定查询条件
            IList<CM_Client> clients = CM_ClientBLL.GetRetailerListBySalesMan(TDP, User.StaffID);
            FindKey = FindKey.Trim();
            if (FindKey != "")
                clients = clients.Where(p => p.FullName.Contains(FindKey) || p.ShortName.Contains(FindKey) || p.Address.Contains(FindKey)).ToList();
            #endregion

            Counts = clients.Count;

            #region 分页返回
            if (PageSize == 0) PageSize = 99999;
            List<ClientInfo> list = new List<ClientInfo>();
            int fromindex = PageSize * PageIndex;
            int endindex = PageSize * (PageIndex + 1);

            if (fromindex < clients.Count)
            {
                for (int i = fromindex; (i < endindex && i < clients.Count); i++)
                {
                    ClientInfo c = new ClientInfo(clients[i], TDP);
                    list.Add(c);
                }
            }
            return list;
            #endregion
        }
        #endregion

        #region 获取附近门店列表
        /// <summary>
        /// 获取附近门店列表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="FindKey">门店关键字</param>
        /// <param name="Latitude">纬度</param>
        /// <param name="Longitude">经度</param>
        /// <param name="MaxDistance">最大距离范围(米)</param>
        /// <param name="PageSize">分页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="Counts">输出：总记录数</param>
        /// <returns></returns>
        public static List<ClientInfo> GetNearRetailerList(UserInfo User, int TDP, string FindKey, decimal Latitude, decimal Longitude, int MaxDistance, int PageSize, int PageIndex, out int Counts)
        {
            Counts = 0;
            LogWriter.WriteLog("PBMIFService.GetNearRetailerList:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",FindKey=" + FindKey + ",Latitude=" + Latitude.ToString() + ",Longitude=" + Longitude.ToString() + ",MaxDistance=" + MaxDistance
                + ",PageSize=" + PageSize.ToString() + ",PageIndex=" + PageIndex.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            #region 设定查询条件
            IList<CM_Client> clients = CM_ClientBLL.GetRetailerListBySalesMan(TDP, User.StaffID);
            FindKey = FindKey.Trim();
            if (FindKey != "")
                clients = clients.Where(p => p.FullName.Contains(FindKey) || p.ShortName.Contains(FindKey) || p.Address.Contains(FindKey)).ToList();
            #endregion

            Counts = clients.Count;

            #region 如果有传入经、纬度，则计算距离，并排序
            Dictionary<int, int> dic_Distance = new Dictionary<int, int>(clients.Count);
            if (Latitude != 0 && Longitude != 0)
            {
                foreach (CM_Client item in clients)
                {
                    //计算距离
                    CM_ClientGeoInfo geo = CM_ClientGeoInfoBLL.GetGeoInfoByClient(item.ID);
                    if (geo != null)
                    {
                        int _distance = (int)GIS_OfficialCityGeoBLL.DistanceByLatLong((double)Latitude, (double)Longitude, (double)geo.Latitude, (double)geo.Longitude);
                        dic_Distance.Add(item.ID, _distance);
                    }
                    else
                    {
                        dic_Distance.Add(item.ID, int.MaxValue);
                    }
                }
                dic_Distance = dic_Distance.OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);

                if (MaxDistance == 0) MaxDistance = int.MaxValue;

                clients = clients.Where(p => dic_Distance[p.ID] < MaxDistance).OrderBy(p => dic_Distance[p.ID]).ToList();
            }
            #endregion

            #region 分页返回
            if (PageSize == 0) PageSize = 99999;
            List<ClientInfo> list = new List<ClientInfo>();
            int fromindex = PageSize * PageIndex;
            int endindex = PageSize * (PageIndex + 1);

            if (fromindex < clients.Count)
            {
                for (int i = fromindex; (i < endindex && i < clients.Count); i++)
                {
                    ClientInfo c = new ClientInfo(clients[i], User.ClientID);
                    if (dic_Distance != null && dic_Distance.ContainsKey(c.ID)) c.Distance = dic_Distance[c.ID];
                    list.Add(c);
                }
            }
            return list;
            #endregion
        }
        #endregion

        #region 获取指定拜访路线上的门店列表
        /// <summary>
        /// 获取指定拜访路线上的门店列表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="VisitRoute"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Counts"></param>
        /// <returns></returns>
        public static List<ClientInfo> GetRetailerListByVisitRoute(UserInfo User, int TDP, string FindKey, int VisitRoute, int PageSize, int PageIndex, out int Counts)
        {
            Counts = 0;
            LogWriter.WriteLog("PBMIFService.GetRetailerListByVisitRoute:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",FindKey=" + FindKey + ",VisitRoute=" + VisitRoute + ",PageSize=" + PageSize.ToString() + ",PageIndex=" + PageIndex.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            #region 设定查询条件
            string ExtConditionStr = "";
            if (FindKey.Trim() != "")
                ExtConditionStr = " (CM_Client.FullName LIKE '%" + FindKey.Trim() + "%'  OR CM_Client.ShortName LIKE '%" + FindKey.Trim()
                    + "%' OR CM_Client.Address LIKE '%" + FindKey.Trim() + "%')";

            //根据线路筛选
            if (VisitRoute != 0)
            {
                if (User.OwnerType == 3)
                {
                    ExtConditionStr += " AND CM_Client.ID IN (SELECT Client FROM CM_ClientSupplierInfo WHERE Supplier=" + TDP.ToString() + " AND State = 1 "
                        + " AND VisitRoute=" + VisitRoute.ToString() + ")";
                }
                else if (User.OwnerType == 2)
                {
                    ExtConditionStr += " AND CM_Client.ID IN (SELECT Client FROM CM_ClientManufactInfo WHERE Manufacturer=" + User.ClientID.ToString()
                        + " AND VisitRoute=" + VisitRoute.ToString() + ")";

                    ExtConditionStr += " AND CM_Client.ID IN (SELECT Client FROM CM_ClientSupplierInfo WHERE Supplier=" + TDP.ToString() + " AND State = 1 )";
                }
            }

            IList<CM_Client> clients = CM_ClientBLL.GetModelList(ExtConditionStr);
            #endregion

            Counts = clients.Count;

            #region 分页返回
            if (PageSize == 0) PageSize = 10;
            List<ClientInfo> list = new List<ClientInfo>();
            int fromindex = PageSize * PageIndex;
            int endindex = PageSize * (PageIndex + 1);

            if (fromindex < clients.Count)
            {
                for (int i = fromindex; (i < endindex && i < clients.Count); i++)
                {
                    ClientInfo c = new ClientInfo(clients[i], User.ClientID);
                    list.Add(c);
                }
            }
            return list;
            #endregion
        }
        #endregion

        #region 根据门店ID获取门店详细信息
        /// <summary>
        /// 根据门店ID获取门店详细信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public static ClientInfo GetRetailerDeailInfo(UserInfo User, int TDP, int ClientID)
        {
            LogWriter.WriteLog("PBMIFService.GetRetailerDeailInfo:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",ClientID=" + ClientID.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            return new ClientInfo(ClientID, TDP);
        }
        #endregion

        #region 定位门店GPS经纬度
        /// <summary>
        /// 设置门店GPS经纬度
        /// </summary>
        /// <param name="AuthKey">授权码</param>
        /// <param name="ClientID">门店ID</param>
        /// <param name="Latitude">经度</param>
        /// <param name="Longitude">纬度</param>
        /// <returns>0:成功 小于0:失败</returns>
        public static int SetClientGPS(UserInfo User, int ClientID, float Latitude, float Longitude)
        {
            LogWriter.WriteLog("PBMIFService.SetClientGPS:UserName=" + User.UserName + ",ClientID=" + ClientID.ToString() +
                ",Latitude=" + Latitude.ToString() + ",Longitude=" + Longitude.ToString());

            if (ClientID == 0) ClientID = User.ClientID;

            CM_ClientGeoInfoBLL bll = new CM_ClientGeoInfoBLL();
            bll.Model.Client = ClientID;
            bll.Model.Latitude = (decimal)Latitude;
            bll.Model.Longitude = (decimal)Longitude;
            bll.Model.InsertUser = User.aspnetUserId;
            int ret = bll.Add();        //在数据库层面有控制，一个门店只能有一个经纬信息

            if (ret > 0)
                return 0;
            else
                return -1;

        }
        #endregion

        #region 上传门店照片
        /// <summary>
        /// 上传门店照片
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="ClientID">会员店ID，上传当前门店时可置0</param>
        /// <param name="PicName">图片名称(可选)</param>
        /// <param name="PicData">图片数据</param>
        /// <returns>大于0:成功 -1:会员店ID无效 -4:照片格式不正确</returns>
        public static int UploadRetailerPicture(UserInfo User, int ClientID, string PicName, string Description, string PicData, out Guid PicGUID)
        {
            PicGUID = Guid.Empty;
            LogWriter.WriteLog("PBMIFService.UploadRetailerPicture:UserName=" + User.UserName + ",ClientID=" + ClientID.ToString()
                + ",PicName=" + PicName + ",Description=" + Description + ",PicDataLength=" + PicData.Length);

            try
            {
                if (ClientID == 0 && User.ClientID > 0) ClientID = User.ClientID;

                CM_Client _c = new CM_ClientBLL(ClientID).Model;
                if (_c == null) return -1;

                if (string.IsNullOrEmpty(PicData)) return -4;
                byte[] buffer = Convert.FromBase64String(PicData);
                if (buffer.Length == 0 && buffer.Length > 1024 * 1024 * 10) return -4;

                ATMT_AttachmentBLL atm = new ATMT_AttachmentBLL();
                atm.Model.RelateType = 30;          //终端门店
                atm.Model.RelateID = ClientID;
                atm.Model.Name = string.IsNullOrEmpty(PicName) ? "门店照片" : PicName;
                atm.Model.ExtName = "jpg";          //默认为JPG图片
                atm.Model.FileSize = PicData.Length / 1024;
                atm.Model.Description = Description;
                atm.Model.UploadUser = User.UserName;
                atm.Model.IsDelete = "N";

                return atm.Add(buffer, out PicGUID);
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("PBMIFService.UploadRetailerPicture Exception Error!", err);
                return -100;
            }
        }
        #endregion

        #region 新增门店资料
        /// <summary>
        /// 新增门店资料
        /// </summary>
        /// <param name="User">用户</param>
        /// <param name="TDP"></param>
        /// <param name="ClientJson">门店资料Json</param>
        /// <returns>大于0:新增的门店ID -1：Json字符串无法解析 -2：所属经销商无效 -3：新增门店基本资料失败</returns>
        public static int RetailerAdd(UserInfo User, int TDP, ClientInfo Client, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.RetailerAdd:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",ClientInfo=" + JsonConvert.SerializeObject(Client));
            if (User.OwnerType == 3) TDP = User.ClientID;
            try
            {
                ClientInfo c = Client;
                if (c == null) { ErrorInfo = "Json字符串无法解析"; return -1; }        //Json字符串无法解析

                CM_Client TDPClient = new CM_ClientBLL(TDP).Model;
                if (TDPClient == null) { ErrorInfo = "所属经销商无效"; return -2; }   //所属经销商无效

                #region 判断必填与重复检测
                if (c.FullName == "") { ErrorInfo = "门店名称必填"; return -10; }
                if (c.Mobile == "") { ErrorInfo = "联系电话必填"; return -10; }
                if (c.Address == "") { ErrorInfo = "门店地址必填"; return -10; }

                if (c.Mobile.StartsWith("01")) c.Mobile = c.Mobile.Substring(1);
                if (c.TeleNum.Contains("-")) c.TeleNum = c.TeleNum.Replace("-", "");
                if (c.Mobile.Contains("-")) c.Mobile = c.Mobile.Replace("-", "");

                //检查门店资料重复
                int rptclientid = CM_ClientBLL.CheckRepeat(TDP, 0, c.Mobile, c.TeleNum, c.FullName, c.Address);
                if (rptclientid > 0)
                {
                    CM_Client _rptclient = new CM_ClientBLL(rptclientid).Model;
                    ErrorInfo = "门店资料与[" + _rptclient.FullName + "]资料重复，请勿重复新增!";
                    return -11;
                }
                #endregion

                CM_ClientBLL bll = new CM_ClientBLL();

                #region 门店基本资料
                bll.Model.Code = c.Code;
                bll.Model.FullName = c.FullName != "" ? c.FullName : c.ShortName;
                bll.Model.ShortName = c.ShortName == "" ? c.FullName : c.ShortName;

                if (c.OfficialCity > 0)
                    bll.Model.OfficialCity = c.OfficialCity;
                else
                    bll.Model.OfficialCity = TDPClient.OfficialCity;

                bll.Model.Address = c.Address;
                bll.Model.DeliveryAddress = c.Address;
                bll.Model.LinkManName = c.LinkManName;
                bll.Model.TeleNum = c.TeleNum == "" ? c.Mobile : c.TeleNum;
                if (c.Mobile.StartsWith("1") && c.Mobile.Length == 11) bll.Model.Mobile = c.Mobile;

                bll.Model.OpenTime = DateTime.Parse("1900-01-01 08:00");
                bll.Model.CloseTime = DateTime.Parse("1900-01-01 20:00");
                bll.Model.ClientType = 3;
                bll.Model.InsertStaff = User.StaffID;
                bll.Model.OwnerType = 3;
                bll.Model.OwnerClient = TDPClient.ID;
                bll.Model.ApproveFlag = 2;      //默认为未审核
                #endregion

                int retailerid = bll.Add();
                if (retailerid < 0) { ErrorInfo = "新增门店基本资料失败"; return -3; }    //新增门店基本资料失败

                #region 设置供货商信息
                CM_ClientSupplierInfo supplierinfo = new CM_ClientSupplierInfo();
                supplierinfo.Code = c.CodeBySupplier;
                supplierinfo.Client = retailerid;
                supplierinfo.Supplier = TDP;
                if (User.OwnerType == 3)
                {
                    supplierinfo.Salesman = User.StaffID;
                    IList<VST_Route> routes = VST_RouteBLL.GetRouteListByStaff(User.StaffID);
                    if (routes.Count > 0) supplierinfo.VisitRoute = routes[0].ID;
                }
                supplierinfo.StandardPrice = c.StandardPrice;
                supplierinfo.TDPChannel = c.TDPChannel;
                supplierinfo.TDPSalesArea = c.TDPSalesArea;

                supplierinfo.VisitSequence = c.VisitSequence;
                supplierinfo.VisitTemplate = c.VisitTemplate;
                supplierinfo.InsertStaff = User.StaffID;
                bll.SetSupplierInfo(supplierinfo);
                #endregion

                #region 设置厂商管理信息
                CM_ClientManufactInfo manufactinfo = new CM_ClientManufactInfo();
                manufactinfo.Client = retailerid;
                manufactinfo.Code = c.CodeByManufaturer;
                manufactinfo.Manufacturer = TDPClient.OwnerClient;      //门店归属厂商默认为经销商资料的所属厂商

                if (User.OwnerType == 2)
                {
                    manufactinfo.ClientManager = User.StaffID;

                    IList<VST_Route> routes = VST_RouteBLL.GetRouteListByStaff(User.StaffID);
                    if (routes.Count > 0) manufactinfo.VisitRoute = routes[0].ID;

                }

                //门店所属区域为经销商对应区域
                CM_ClientBLL s = new CM_ClientBLL(TDP);
                manufactinfo.OrganizeCity = s.GetManufactInfo().OrganizeCity;

                bll.SetManufactInfo(manufactinfo);
                #endregion

                #region 保存

                #endregion
                return retailerid;
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("PBMIFService.RetailerAdd Exception Error!", err);
                return -100;
            }
        }
        #endregion

        #region 更新门店资料
        /// <summary>
        /// 新增门店资料
        /// </summary>
        /// <param name="User">用户</param>
        /// <param name="TDP"></param>
        /// <param name="ClientJson">门店资料Json</param>
        /// <returns>大于0:新增的门店ID -1：Json字符串无法解析 -2：所属经销商无效 -3：更新门店基本资料失败</returns>
        public static int RetailerUpdate(UserInfo User, int TDP, ClientInfo Client, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.RetailerUpdate:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",ClientInfo=" + JsonConvert.SerializeObject(Client));
            if (User.OwnerType == 3) TDP = User.ClientID;

            try
            {
                ClientInfo c = Client;
                if (c == null) { ErrorInfo = "Json字符串无法解析"; return -1; }        //Json字符串无法解析

                CM_Client TDPClient = new CM_ClientBLL(TDP).Model;
                if (TDPClient == null) { ErrorInfo = "所属经销商无效"; return -2; }   //所属经销商无效

                #region 判断必填与重复检测
                if (c.FullName == "") { ErrorInfo = "门店名称必填"; return -10; }
                if (c.Mobile == "") { ErrorInfo = "联系电话必填"; return -10; }
                if (c.Address == "") { ErrorInfo = "门店地址必填"; return -10; }

                if (c.Mobile.StartsWith("01")) c.Mobile = c.Mobile.Substring(1);
                if (c.TeleNum.Contains("-")) c.TeleNum = c.TeleNum.Replace("-", "");
                if (c.Mobile.Contains("-")) c.Mobile = c.Mobile.Replace("-", "");

                //检查门店资料重复
                int rptclientid = CM_ClientBLL.CheckRepeat(TDP, Client.ID, c.Mobile, c.TeleNum, c.FullName, c.Address);
                if (rptclientid > 0)
                {
                    CM_Client _rptclient = new CM_ClientBLL(rptclientid).Model;
                    ErrorInfo = "门店资料与[" + _rptclient.FullName + "]资料重复，请勿重复新增!";
                    return -11;
                }
                #endregion

                CM_ClientBLL bll = new CM_ClientBLL(c.ID);

                #region 门店基本资料
                bll.Model.Code = c.Code;
                bll.Model.FullName = c.FullName != "" ? c.FullName : c.ShortName;
                bll.Model.ShortName = c.ShortName == "" ? c.FullName : c.ShortName;

                if (c.OfficialCity > 0)
                    bll.Model.OfficialCity = c.OfficialCity;
                else
                    bll.Model.OfficialCity = TDPClient.OfficialCity;

                if (bll.Model.DeliveryAddress == bll.Model.Address)
                    bll.Model.DeliveryAddress = c.Address;
                bll.Model.Address = c.Address;

                bll.Model.LinkManName = c.LinkManName;
                bll.Model.TeleNum = c.TeleNum == "" ? c.Mobile : c.TeleNum;
                if (c.Mobile.StartsWith("1") && c.Mobile.Length == 11) bll.Model.Mobile = c.Mobile;
                bll.Model.UpdateStaff = User.StaffID;
                #endregion

                int retailerid = bll.Update();
                if (retailerid < 0) return -3;      //更新门店基本资料失败

                #region 设置供货商信息
                CM_ClientSupplierInfo supplierinfo = bll.GetSupplierInfo(TDP);
                supplierinfo.Code = c.CodeBySupplier;
                supplierinfo.StandardPrice = c.StandardPrice;
                supplierinfo.TDPChannel = c.TDPChannel;
                supplierinfo.TDPSalesArea = c.TDPSalesArea;
                supplierinfo.VisitRoute = c.VisitRoute;
                supplierinfo.VisitSequence = c.VisitSequence;
                supplierinfo.VisitTemplate = c.VisitTemplate;
                supplierinfo.UpdateStaff = User.StaffID;
                bll.SetSupplierInfo(supplierinfo);
                #endregion

                #region 设置厂商管理信息
                #endregion

                return 0;
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("PBMIFService.RetailerAdd Exception Error!", err);
                return -100;
            }
        }
        #endregion

        #region 上传TDP向门店供货产品品项列表
        /// <summary>
        /// 上传TDP向门店供货产品品项列表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP">供货TDP</param>
        /// <param name="Retailer">门店</param>
        /// <param name="ProductList">TDP向门店的供货目录</param>
        /// <returns></returns>
        public static int UpdateRetailerProductList(UserInfo User, int TDP, int Retailer, ref List<ClientInfo.ProductList> ProductList)
        {
            LogWriter.WriteLog("PBMIFService.UpdateRetailerProductList:UserName=" + User.UserName + ",TDP=" + TDP.ToString()
                + ",Retailer=" + Retailer.ToString() + ",ProductList=" + JsonConvert.SerializeObject(ProductList));

            if (ProductList == null) return -1;

            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<CM_ClientProductList> curlist = CM_ClientProductListBLL.GetModelList("Client=" + Retailer.ToString() + (TDP == 0 ? "" : " AND Supplier=" + TDP.ToString()));

            #region 将系统中存在但上传参数中不存在的产品移出供货目录
            foreach (CM_ClientProductList item in curlist)
            {
                if (ProductList.FirstOrDefault(p => p.Product == item.Product) == null)
                    new CM_ClientProductListBLL(item.ID).Delete();
            }
            #endregion

            #region 逐条新增或更新参数中指定的供货目录
            foreach (ClientInfo.ProductList item in ProductList)
            {
                CM_ClientProductListBLL bll = null;

                CM_ClientProductList info = curlist.FirstOrDefault(p => p.Product == item.Product);
                if (info == null)
                {
                    bll = new CM_ClientProductListBLL();
                    bll.Model.Client = Retailer;
                    bll.Model.Supplier = TDP;
                    bll.Model.Product = item.Product;
                    bll.Model.Remark = item.Remark;
                    bll.Model.InsertStaff = User.StaffID;
                    bll.Model.Price = PDT_StandardPriceBLL.GetSalePrice(Retailer, TDP, item.Product);

                    bll.Add();
                }
                else
                {
                    bll = new CM_ClientProductListBLL(info.ID);
                    bll.Model.Price = PDT_StandardPriceBLL.GetSalePrice(Retailer, TDP, item.Product);
                    bll.Update();
                }
            }
            #endregion

            #region 返回服务器端更新后的经营品项(向手机端反馈默认供货价格)
            ProductList.Clear();
            foreach (CM_ClientProductList item in CM_ClientProductListBLL.GetModelList
                ("Client=" + Retailer.ToString() + (TDP == 0 ? "" : " AND Supplier=" + TDP.ToString())))
            {
                ProductList.Add(new ClientInfo.ProductList(item));
            }
            #endregion

            return 0;
        }
        #endregion

        #endregion

        #region 库存管理

        #region 获取指定车辆的仓库库存
        /// <summary>
        /// 获取指定车辆的仓库库存
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <param name="VehicleID"></param>
        /// <param name="DisplayMode">1：按产品查询库存 2：按批号查询库存</param>
        /// <returns></returns>
        public static List<Inventory> GetInventoryByVehicle(UserInfo User, int VehicleID, int DisplayMode)
        {
            LogWriter.WriteLog("PBMIFService.GetInventoryByVehicle:UserName=" + User.UserName + ",VehicleID=" + VehicleID.ToString()
                + ",IsMergeByProduct=" + DisplayMode.ToString());

            CM_Vehicle v = new CM_VehicleBLL(VehicleID).Model;
            if (v == null) return null;                 //车辆ID无效
            if (v.RelateWareHouse == 0) return null;    //车辆关联仓库无效

            return GetInventoryByWareHouse(User, v.RelateWareHouse, DisplayMode);
        }

        /// <summary>
        /// 获取指定仓库库存
        /// </summary>
        /// <param name="User"></param>
        /// <param name="WareHouse"></param>
        /// <param name="DisplayMode">1：按产品查询库存 2：按批号查询库存</param>
        /// <returns></returns>
        public static List<Inventory> GetInventoryByWareHouse(UserInfo User, int WareHouse, int DisplayMode)
        {
            LogWriter.WriteLog("PBMIFService.GetInventoryByWareHouse:UserName=" + User.UserName + ",WareHouse=" + WareHouse.ToString()
                + ",IsMergeByProduct=" + DisplayMode);

            CM_WareHouse w = new CM_WareHouseBLL(WareHouse).Model;
            if (w == null) return null;

            if (User.OwnerType == 3 && w.Client != User.ClientID) return null;  //仓库不属于当前登录人员的经销商

            IList<INV_Inventory> invtorys = INV_InventoryBLL.GetModelList("WareHouse=" + WareHouse.ToString() + " AND Quantity>0");
            List<Inventory> list = new List<Inventory>(invtorys.Count);

            foreach (INV_Inventory item in invtorys)
            {
                Inventory inv;
                if (DisplayMode == 1)
                {
                    inv = list.FirstOrDefault(p => p.Product == item.Product);
                    if (inv != null)
                    {
                        inv.Quantity += item.Quantity;

                        inv.Quantity_T = inv.Quantity / inv.ConvertFactor;
                        inv.Quantity_P = inv.Quantity % inv.ConvertFactor;
                        continue;
                    }
                }

                inv = new Inventory(item);

                if (DisplayMode == 1) inv.LotNumber = "";

                #region 获取产品默认销售价
                try
                {
                    inv.Price = new PDT_ProductBLL(item.Product).GetProductExtInfo(w.Client).SalesPrice;
                }
                catch { inv.Price = 0; }
                #endregion

                list.Add(inv);

            }

            return list.OrderBy(p => p.CategoryName).ToList();
        }
        #endregion

        #endregion

        #region 拜访管理

        #region 获取当前员工的拜访路线
        /// <summary>
        /// 获取当前员工的拜访路线
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static List<VisitRoute> GetVisitRouteList(UserInfo User)
        {
            LogWriter.WriteLog("PBMIFService.GetVisitRouteList:UserName=" + User.UserName);

            IList<VST_Route> routes = VST_RouteBLL.GetRouteListByStaff(User.StaffID);
            List<VisitRoute> list = new List<VisitRoute>(routes.Count);
            foreach (VST_Route item in routes)
            {
                list.Add(new VisitRoute(item));
            }
            return list;
        }
        #endregion

        #region 获取拜访模板信息
        /// <summary>
        /// 获取指定TDP可使用的拜访模板
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<VisitTemplate> GetVisitTemplateList(UserInfo User, int TDP)
        {
            LogWriter.WriteLog("PBMIFService.GetVisitTemplateList:UserName=" + User.UserName + ",TDP=" + TDP.ToString());
            if (User.OwnerType == 3) TDP = User.ClientID;

            IList<VST_VisitTemplate> templates = VST_VisitTemplateBLL.GetVisitTemplateByTDP(TDP);
            List<VisitTemplate> list = new List<VisitTemplate>(templates.Count);
            foreach (VST_VisitTemplate item in templates)
            {
                list.Add(new VisitTemplate(item));
            }
            return list;
        }
        #endregion

        #region 获取指定步骤编码的环节详细信息
        /// <summary>
        /// 获取指定步骤编码的环节详细信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ProcessCodes">要获取步骤定义信息的编码,多个编码间以,号分隔</param>
        /// <returns></returns>
        public static List<VisitProcess> GetVisitProcessDefineInfo(UserInfo User, string ProcessCodes)
        {
            LogWriter.WriteLog("PBMIFService.GetVisitProcessDefineInfo:UserName=" + User.UserName + ",ProcessCodes=" + ProcessCodes);

            string[] codes = ProcessCodes.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<VisitProcess> list = new List<VisitProcess>(codes.Length);
            foreach (string c in codes)
            {
                list.Add(new VisitProcess(c));
            }
            return list;
        }
        #endregion

        #region 获取员工拜访计划
        /// <summary>
        /// 获取员工拜访计划
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Staff">员工，为0时，默认当前登录员工</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">截止日期</param>
        /// <returns></returns>
        public static List<VisitPlan> GetVisitPlanListByStaff(UserInfo User, int Staff, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetVisitPlanListByStaff:UserName=" + User.UserName + ",Staff=" + Staff.ToString() +
                ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (Staff == 0) Staff = User.StaffID;

            IList<VST_VisitPlan> plans = VST_VisitPlanBLL.GetModelList("PlanVisitDate BETWEEN '" + BeginDate.ToString("yyyy-MM-dd") + "' AND '"
                + EndDate.ToString("yyyy-MM-dd") + " 23:59:59' AND RelateStaff=" + Staff.ToString());

            List<VisitPlan> list = new List<VisitPlan>(plans.Count);
            foreach (VST_VisitPlan item in plans)
            {
                list.Add(new VisitPlan(item));
            }
            return list;
        }
        #endregion

        #region 新增门店拜访记录
        /// <summary>
        /// 新增巡店工作日志(门店拜访记录)
        /// </summary>
        /// <param name="User"></param>
        /// <param name="VisitWorkJson"></param>
        /// <returns>大于0:成功 -1:Json字符串无法解析 -2:新增拜访工作记录失败</returns>
        public static int VisitWorkAdd(UserInfo User, VisitWork Work)
        {
            LogWriter.WriteLog("PBMIFService.VisitWorkAdd:UserName=" + User.UserName +
                ",VisitWorkJson=" + JsonConvert.SerializeObject(Work));

            try
            {
                VisitWork v = Work;
                if (v == null) return -1;           //Json字符串无法解析

                VST_WorkListBLL bll;
                if (Work.ID == 0 || new VST_WorkListBLL(Work.ID).Model == null)
                {
                    bll = new VST_WorkListBLL();
                    bll.Model.RelateStaff = v.RelateStaff == 0 ? User.StaffID : v.RelateStaff;
                    bll.Model.Route = v.Route;
                    bll.Model.Client = v.Client;
                    bll.Model.Template = v.VisitTemplate;
                    bll.Model.WorkingClassify = v.WorkingClassify == 0 ? 1 : v.WorkingClassify;
                    bll.Model.IsComplete = v.IsComplete ? "Y" : "N";
                    bll.Model.BeginTime = v.BeginTime;
                    bll.Model.EndTime = v.EndTime;
                    bll.Model.PlanID = v.PlanID;
                    bll.Model.Remark = v.Remark;
                    bll.Model.ApproveFlag = 2;
                    bll.Model.InsertStaff = User.StaffID;
                }
                else
                {
                    bll = new VST_WorkListBLL(Work.ID);
                    bll.Model.IsComplete = v.IsComplete ? "Y" : "N";
                    bll.Model.EndTime = v.EndTime;
                    bll.Model.Remark = v.Remark;
                }

                if (bll.Model.ID == 0)
                {
                    int worklistid = bll.Add();
                    if (worklistid <= 0) return -2;     //新增拜访工作记录失败

                    Work.ID = worklistid;
                }
                else
                {
                    if (bll.Update() < 0) return -3;    //更新拜访工作记录失败
                }

                foreach (VisitWork.VisitWorkItem item in v.Items)
                {
                    if (item.WorkItemID > 0) continue;
                    VST_WorkItem detail = new VST_WorkItem();

                    detail.WorkList = Work.ID;
                    if (item.ProcessCode != "")
                    {
                        VST_Process m = new VST_ProcessBLL(item.ProcessCode).Model;
                        if (m != null) detail.Process = m.ID;
                    }
                    detail.WorkTime = item.WorkTime;
                    detail.Remark = item.Remark;

                    int detailid = bll.AddDetail(detail);

                    switch (item.ProcessCode)
                    {
                        #region 进店详细属性
                        case "JD":
                            {
                                int JobType = 0, JudgeMode = 0;
                                float Longitude = 0, Latitude = 0;

                                if (item.ExtParams.ContainsKey("JobType")) int.TryParse(item.ExtParams["JobType"].ToString(), out JobType);
                                if (item.ExtParams.ContainsKey("JudgeMode")) int.TryParse(item.ExtParams["JudgeMode"].ToString(), out JudgeMode);
                                if (item.ExtParams.ContainsKey("Longitude")) float.TryParse(item.ExtParams["Longitude"].ToString(), out Longitude);
                                if (item.ExtParams.ContainsKey("Latitude")) float.TryParse(item.ExtParams["Latitude"].ToString(), out Latitude);

                                VST_WorkItem_JDBLL jdbll = new VST_WorkItem_JDBLL();
                                jdbll.Model.Job = detailid;
                                jdbll.Model.JobType = JobType;
                                jdbll.Model.JudgeMode = JudgeMode;
                                jdbll.Model.Longitude = Longitude;
                                jdbll.Model.Latitude = Latitude;
                                if (item.ExtParams.ContainsKey("Remark")) jdbll.Model.Remark = item.ExtParams["Remark"].ToString();
                                jdbll.Add();
                                break;
                            }
                        #endregion
                        default:
                            break;
                    }
                }

                return Work.ID;
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("PBMIFService.VisitWorkAdd Exception Error!", err);
                return -100;
            }

        }

        /// <summary>
        /// 上传巡店工作日志关联照片
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="ClientID">巡店工作日志ID</param>
        /// <param name="PicName">图片名称(可选)</param>
        /// <param name="PicData">图片数据</param>
        /// <returns>大于0:成功 -1:巡店工作日志ID无效 -4:照片格式不正确</returns>
        public static int UploadVisitWorkPicture(UserInfo User, int VisitWorkID, string PicName, string Description, string PicData, out Guid PicGUID)
        {
            PicGUID = Guid.Empty;
            LogWriter.WriteLog("PBMIFService.UploadVisitWorkPicture:UserName=" + User.UserName + ",VisitWorkID=" + VisitWorkID.ToString()
                + ",PicName=" + PicName + ",Description=" + Description + ",PicDataLength=" + PicData.Length);

            try
            {
                if (VisitWorkID == 0) return -1;        //必须指定巡店工作日志ID

                if (string.IsNullOrEmpty(PicData)) return -4;
                byte[] buffer = Convert.FromBase64String(PicData);
                if (buffer.Length == 0 && buffer.Length > 1024 * 1024 * 10) return -4;

                ATMT_AttachmentBLL atm = new ATMT_AttachmentBLL();
                atm.Model.RelateType = 95;          //巡店工作日志
                atm.Model.RelateID = VisitWorkID;
                atm.Model.Name = string.IsNullOrEmpty(PicName) ? "照片" : PicName;
                atm.Model.ExtName = "jpg";          //默认为JPG图片
                atm.Model.FileSize = PicData.Length / 1024;
                atm.Model.Description = Description;
                atm.Model.UploadUser = User.UserName;
                atm.Model.IsDelete = "N";

                int ret = atm.Add(buffer, out PicGUID);
                LogWriter.WriteLog("PBMIFService.UploadVisitWorkPicture Upload Result:UserName=" + User.UserName + ",VisitWorkID=" + VisitWorkID.ToString()
                    + ",PicGUID=" + PicGUID.ToString() + ",Ret=" + ret.ToString());
                return ret;
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("PBMIFService.UploadVisitWorkPicture Exception Error!", err);
                return -100;
            }
        }
        #endregion

        #region 定时上传拜访位置
        /// <summary>
        /// 定时上传拜访位置
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Type">1:GPS定位 2:网络定位</param>
        /// <param name="Latitude">纬度</param>
        /// <param name="Longitude">经度</param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public static int VisitReportLocation(UserInfo User, int Type, float Latitude, float Longitude, string Remark)
        {
            LogWriter.WriteLog("PBMIFService.VisitReportLocation:UserName=" + User.UserName + ",Type=" + Type.ToString() +
                ",Latitude=" + Latitude.ToString() + ",Longitude=" + Longitude.ToString() + ",Remark=" + Remark.ToString());

            VST_ReportLocationBLL bll = new VST_ReportLocationBLL();
            bll.Model.RelateStaff = User.StaffID;
            bll.Model.LocateType = Type;
            bll.Model.Latitude = Latitude;
            bll.Model.Longitude = Longitude;
            bll.Model.Remark = Remark;
            bll.Model.DeviceCode = User.DeviceCode;

            return bll.Add();
        }
        #endregion

        #endregion

        #region 车销管理

        #region 查询指定门店的销售单
        /// <summary>
        /// 查询门店指定日期范围内的销售单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Retailer"></param>
        /// <param name="StateFlag">状态标志 ALL:所有 COMPLETE:已完成的销售单 UNCOMPLETE:未完成的销售单</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<Delivery> GetSaleOutListByRetailer(UserInfo User, int Retailer, string StateFlag, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetSaleOutListByRetailer:UserName=" + User.UserName + ",Retailer=" + Retailer.ToString() +
                ",StateFlag=" + StateFlag + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            string condition = " Client=" + Retailer.ToString() + " AND InsertTime BETWEEN '" + BeginDate.ToString("yyyy-MM-dd")
                + "' AND '" + EndDate.ToString("yyyy-MM-dd") + " 23:59:59' ";

            if (User.OwnerType == 3) condition += " AND Supplier=" + User.ClientID.ToString();

            if (StateFlag.ToUpper() == "ALL")
                condition += " AND State<>9";
            else if (StateFlag.ToUpper() == "COMPLETE")
                condition += " AND State = 4";
            else if (StateFlag.ToUpper() == "UNCOMPLETE")
                condition += " AND State IN (1,2,3)";
            else
                condition += " AND State IN (0)";

            IList<PBM_Delivery> deliverys = PBM_DeliveryBLL.GetModelList(condition);
            List<Delivery> list = new List<Delivery>(deliverys.Count);
            foreach (PBM_Delivery item in deliverys.OrderByDescending(p => p.InsertTime))
            {
                list.Add(new Delivery(item));
            }
            return list;
        }
        #endregion

        #region 新增销售单
        /// <summary>
        /// 新增销售发货单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryInfo">发货单信息</param>
        /// <param name="ErrorInfo">出错消息</param>
        /// <returns></returns>
        public static int SaleOut_Add(UserInfo User, Delivery DeliveryInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.SaleOut_Add:UserName=" + User.UserName +
                ",DeliveryInfo=" + JsonConvert.SerializeObject(DeliveryInfo));

            //经销商级人员，供货商只能是自己所属的经销商
            if (User.OwnerType == 3) DeliveryInfo.Supplier = User.ClientID;

            //如果有送货车辆，默认出货仓库为该车辆所关联仓库
            if (DeliveryInfo.DeliveryVehicle != 0 && DeliveryInfo.SupplierWareHouse == 0)
            {
                CM_Vehicle v = new CM_VehicleBLL(DeliveryInfo.DeliveryVehicle).Model;
                if (v == null) { ErrorInfo = "送货车辆无效!"; return -1; }

                DeliveryInfo.SupplierWareHouse = v.RelateWareHouse;
            }

            //默认业务人员为当前员工
            if (DeliveryInfo.SalesMan == 0) DeliveryInfo.SalesMan = User.StaffID;

            #region 必填字段校验
            if (DeliveryInfo.Supplier == 0) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (DeliveryInfo.Client == 0) { ErrorInfo = "无效的购买客户!"; return -2; }
            if (DeliveryInfo.SupplierWareHouse == 0) { ErrorInfo = "无效的供货仓库!"; return -2; }
            if (DeliveryInfo.Items == null || DeliveryInfo.Items.Count == 0) { ErrorInfo = "无销售产品明细!"; return -10; }
            #endregion

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL();

            #region 保存销售单头信息
            bll.Model.SheetCode = "";
            bll.Model.Supplier = DeliveryInfo.Supplier;
            bll.Model.SupplierWareHouse = DeliveryInfo.SupplierWareHouse;
            bll.Model.Client = DeliveryInfo.Client;
            bll.Model.SalesMan = DeliveryInfo.SalesMan;
            bll.Model.Classify = (DeliveryInfo.Classify == 0 ? 1 : DeliveryInfo.Classify);              //默认销售单
            bll.Model.PrepareMode = (DeliveryInfo.PrepareMode == 0 ? 2 : DeliveryInfo.PrepareMode);     //默认车销模式
            bll.Model.State = 1;        //默认制单状态
            bll.Model.StandardPrice = DeliveryInfo.StandardPrice;
            bll.Model.OrderId = DeliveryInfo.OrderID;
            bll.Model.WipeAmount = DeliveryInfo.WipeAmount;
            bll.Model.DeliveryVehicle = DeliveryInfo.DeliveryVehicle;
            bll.Model.WorkList = DeliveryInfo.WorkList;
            bll.Model.Remark = DeliveryInfo.Remark;
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = User.StaffID;
            #endregion

            #region 循环处理每个订单明细
            foreach (Delivery.DeliveryDetail item in DeliveryInfo.Items)
            {
                if (item.Product == 0) continue;
                if (item.DeliveryQuantity <= 0 && item.SignInQuantity <= 0) continue;

                string lotnumber = item.LotNumber.Trim();
                int deliveryquantity = item.DeliveryQuantity <= 0 ? item.SignInQuantity : item.DeliveryQuantity;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                if (productbll.Model == null) { ErrorInfo = "无效产品项,产品ID:" + item.Product; return -11; }
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Supplier);
                if (productbll.Model == null) { ErrorInfo = "产品不在销售商的经营目录中," + productbll.Model.FullName; return -11; }

                if (bll.Model.Classify != 2)
                {
                    //不为退货单时，判断车库存是否够销售
                    int inv_quantity = INV_InventoryBLL.GetProductQuantity(bll.Model.SupplierWareHouse, item.Product, lotnumber);
                    if (deliveryquantity > inv_quantity)
                    {
                        ErrorInfo = "产品[" + productbll.Model.FullName + "]库存不足，当前库存：" + inv_quantity.ToString();
                        return -11;
                    }
                }

                #region 新增销售明细
                PBM_DeliveryDetail d = new PBM_DeliveryDetail();

                d.Product = item.Product;
                d.LotNumber = lotnumber;
                d.SalesMode = item.SalesMode == 0 ? 1 : item.SalesMode;     //默认为“销售”

                if (item.Price > 0)
                    d.Price = item.Price;
                else
                    d.Price = PDT_StandardPriceBLL.GetSalePrice(bll.Model.Client, bll.Model.Supplier, d.Product);       //默认销售价

                if (d.SalesMode == 1)
                    d.DiscountRate = (item.DiscountRate <= 0 || item.DiscountRate > 1) ? 1 : item.DiscountRate;
                else
                    d.DiscountRate = 0;     //非销售时，0折销售

                d.ConvertFactor = productbll.Model.ConvertFactor == 0 ? 1 : productbll.Model.ConvertFactor;
                d.DeliveryQuantity = deliveryquantity;
                d.SignInQuantity = d.DeliveryQuantity;
                d.Remark = item.Remark;

                bll.Items.Add(d);
                #endregion
            }
            #endregion

            //计算折扣金额
            bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                Math.Round(p.Price * p.ConvertFactor, 2) * p.DeliveryQuantity / p.ConvertFactor);

            //计算实际销售金额
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);


            int deliveryid = bll.Add();
            if (deliveryid <= 0) { ErrorInfo = "销售单保存失败!"; return deliveryid; }

            #region 销售单直接完成
            if (DeliveryInfo.State == 4 && DeliveryInfo.PayInfos != null && DeliveryInfo.PayInfos.Count > 0)
            {
                LogWriter.WriteLog("PBMIFService.SaleOut_Add:UserName=" + User.UserName + ",DeliveryID=" + deliveryid.ToString() + ",Auto submit order!");
                int ret = SaleOut_Confirm(User, deliveryid, DeliveryInfo.WipeAmount, DeliveryInfo.PayInfos, out ErrorInfo);
                if (ret < 0)
                {
                    LogWriter.WriteLog("PBMIFService.SaleOut_Add:UserName=" + User.UserName + ",DeliveryID=" + deliveryid.ToString() +
                        ",Auto confirm failed!ErrorInfo=" + ErrorInfo);
                    return ret;
                }
            }
            #endregion

            return deliveryid;
        }
        #endregion

        #region 更新销售单
        /// <summary>
        /// 验证销售单,当在执行预售送货时，或在收款单变更了销售单明细时，需要调用此接口更新销售单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryInfo"></param>
        /// <param name="ErrorInfo"></param>
        /// <returns></returns>
        public static int SaleOut_Update(UserInfo User, Delivery DeliveryInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.SaleOut_Update:UserName=" + User.UserName +
                ",DeliveryInfo=" + JsonConvert.SerializeObject(DeliveryInfo));

            if (DeliveryInfo.ID == 0) { ErrorInfo = "销售单不存在，请先新增销售单!"; return -1; }

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL(DeliveryInfo.ID);
            if (bll.Model == null) { ErrorInfo = "销售单不存在，请先新增销售单!"; return -1; }
            if (bll.Model.State > 3 || bll.Model.ApproveFlag == 1) { ErrorInfo = "销售单状态不允许执行此操作!"; return -1; }

            if (bll.Model.Supplier == 0) bll.Model.Supplier = DeliveryInfo.Supplier;
            if (bll.Model.Client == 0) bll.Model.Client = DeliveryInfo.Client;

            //如果有送货车辆，默认出货仓库为该车辆所关联仓库
            if (DeliveryInfo.DeliveryVehicle != 0 && DeliveryInfo.SupplierWareHouse == 0)
            {
                CM_Vehicle v = new CM_VehicleBLL(DeliveryInfo.DeliveryVehicle).Model;
                if (v == null) { ErrorInfo = "送货车辆无效!"; return -1; }

                DeliveryInfo.SupplierWareHouse = v.RelateWareHouse;
            }

            //默认业务人员为当前员工
            if (DeliveryInfo.SalesMan == 0) DeliveryInfo.SalesMan = User.StaffID;

            #region 必填字段校验
            if (bll.Model.Supplier == 0) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (bll.Model.Client == 0) { ErrorInfo = "无效的购买客户!"; return -2; }

            if (DeliveryInfo.SupplierWareHouse == 0) { ErrorInfo = "无效的供货仓库!"; return -2; }
            if (DeliveryInfo.Items == null || DeliveryInfo.Items.Count == 0) { ErrorInfo = "无销售产品明细!"; return -10; }
            #endregion

            #region 保存销售单头信息
            bll.Model.SalesMan = DeliveryInfo.SalesMan;
            bll.Model.DeliveryVehicle = DeliveryInfo.DeliveryVehicle;
            bll.Model.SupplierWareHouse = DeliveryInfo.SupplierWareHouse;
            bll.Model.WipeAmount = DeliveryInfo.WipeAmount;

            bll.Model.WorkList = DeliveryInfo.WorkList;
            bll.Model.Remark = DeliveryInfo.Remark;
            #endregion

            #region 循环处理每个订单明细
            foreach (Delivery.DeliveryDetail item in DeliveryInfo.Items)
            {
                if (item.Product == 0) continue;
                if (item.DeliveryQuantity <= 0 && item.SignInQuantity <= 0)
                {
                    if (item.DetailID == 0)
                        continue;
                    else
                        bll.DeleteDetail(item.DetailID);
                }

                string lotnumber = item.LotNumber.Trim();

                int quantity = 0;
                if (bll.Model.State == 1)
                    quantity = item.DeliveryQuantity == 0 ? item.SignInQuantity : item.DeliveryQuantity;
                else
                    quantity = item.SignInQuantity;

                string remark = item.Remark;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                if (productbll.Model == null) { ErrorInfo = "无效产品项,产品ID:" + item.Product; return -11; }
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Supplier);
                if (productbll.Model == null) { ErrorInfo = "产品不在销售商的经营目录中," + productbll.Model.FullName; return -11; }

                if (bll.Model.Classify != 2)
                {
                    //不为退货单时，判断车库存是否够销售
                    int inv_quantity = INV_InventoryBLL.GetProductQuantity(bll.Model.SupplierWareHouse, item.Product, lotnumber);
                    if (quantity > inv_quantity)
                    {
                        ErrorInfo = "产品[" + productbll.Model.FullName + "]库存不足，当前库存：" + inv_quantity.ToString();
                        return -11;
                    }
                }

                if (item.DetailID > 0)
                {
                    PBM_DeliveryDetail d = bll.GetDetailModel(item.DetailID);

                    if (bll.Model.State == 1) d.DeliveryQuantity = quantity;
                    d.SignInQuantity = item.SignInQuantity;
                    d.LotNumber = lotnumber;
                    d.Remark = item.Remark;
                    bll.UpdateDetail(d);
                }
                else
                {
                    #region 新增商品明细品项
                    PBM_DeliveryDetail d = new PBM_DeliveryDetail();

                    d.Product = item.Product;
                    d.LotNumber = lotnumber;
                    d.SalesMode = item.SalesMode == 0 ? 1 : item.SalesMode;     //默认为“销售”

                    d.Price = PDT_StandardPriceBLL.GetSalePrice(bll.Model.Client, bll.Model.Supplier, d.Product);
                    if (d.SalesMode == 1)
                        d.DiscountRate = (item.DiscountRate <= 0 || item.DiscountRate > 1) ? 1 : item.DiscountRate;
                    else
                        d.DiscountRate = 0;

                    d.ConvertFactor = productbll.Model.ConvertFactor == 0 ? 1 : productbll.Model.ConvertFactor;
                    d.DeliveryQuantity = item.DeliveryQuantity;
                    d.SignInQuantity = d.DeliveryQuantity;
                    d.Remark = item.Remark;

                    bll.AddDetail(d);
                    #endregion
                }
            }
            #endregion

            //计算折扣金额
            bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor);

            //计算实际销售金额
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);

            int ret = bll.Update();
            if (ret < 0) { ErrorInfo = "销售单保存失败!"; return ret; }

            return 0;
        }
        #endregion

        #region 提交销售单
        /// <summary>
        /// 提交销售单,并输出提交后的订单信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryID">销售单ID</param>
        /// <param name="DeliveryInfo">输出：销售单结构</param>
        /// <param name="ErrorInfo">输出：出错信息</param>
        /// <returns>0:成功 小于0:失败</returns>
        public static int SaleOut_Submit(UserInfo User, int DeliveryID, out Delivery DeliveryInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            DeliveryInfo = null;
            LogWriter.WriteLog("PBMIFService.SaleOut_Submit:UserName=" + User.UserName + ",DeliveryID=" + DeliveryID.ToString());

            if (DeliveryID <= 0) { ErrorInfo = "销售单ID无效"; return -1; }

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL(DeliveryID);
            if (bll.Model == null) { ErrorInfo = "销售单ID无效"; return -1; }
            if (bll.Model.State > 3 || bll.Model.ApproveFlag == 1) { ErrorInfo = "销售单状态无效"; return -1; }

            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "不可提交该销售单"; return -2; }

            int ret = bll.Approve();
            if (ret < 0) { ErrorInfo = "销售单提交失败!"; return -1; }

            DeliveryInfo = new Delivery(bll.Model.ID);
            return 0;
        }
        #endregion

        #region 确认销售并收款
        /// <summary>
        /// 确认销售并收款
        /// </summary>
        /// <param name="User"></param>
        /// <param name="WipeAmount">优惠金额</param>
        /// <param name="DeliveryID"></param>
        /// <returns></returns>
        public static int SaleOut_Confirm(UserInfo User, int DeliveryID, decimal WipeAmount, List<Delivery.DeliveryPayInfo> PayInfoList, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.SaleOut_Confirm:UserName=" + User.UserName + ",DeliveryID=" + DeliveryID.ToString() +
                ",PayInfoList=" + JsonConvert.SerializeObject(PayInfoList));

            if (DeliveryID <= 0) { ErrorInfo = "销售单ID无效"; return -1; }

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL(DeliveryID);
            if (bll.Model == null) { ErrorInfo = "销售单ID无效"; return -1; }
            if (bll.Model.State > 3) { ErrorInfo = "销售单状态无效"; return -1; }

            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "不可确认该销售单"; return -2; }

            bll.Model.WipeAmount = WipeAmount;
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);

            bll.Update();

            if (Math.Abs(PayInfoList.Sum(p => p.Amount) - bll.Model.ActAmount) >= 0.1m)
            {
                ErrorInfo = "收款额与销售金额不符!";
                return -20;
            }

            if (bll.Model.Classify == 1)
            {
                decimal d = PayInfoList.Sum(p => p.PayMode == 11 ? p.Amount : 0);   //余额支付
                if (d > 0)
                {
                    AC_CurrentAccount ac = AC_CurrentAccountBLL.GetByTradeClient(bll.Model.Supplier, bll.Model.Client);
                    if (ac == null || d > ac.PreReceivedAmount)
                    {
                        ErrorInfo = "预收款不够支付!当前预收款余额为:" + ac.PreReceivedAmount.ToString("0.##");
                        return -21;
                    }
                }
            }

            #region 写入收款明细
            //先清除之前的付款信息
            if (bll.GetPayInfoList().Count > 0) bll.ClearPayInfo();

            foreach (Delivery.DeliveryPayInfo item in PayInfoList)
            {
                PBM_DeliveryPayInfoBLL paybll = new PBM_DeliveryPayInfoBLL();
                paybll.Model.DeliveryID = DeliveryID;
                paybll.Model.PayMode = item.PayMode;
                paybll.Model.Amount = item.Amount;
                paybll.Model.Remark = item.Remark;
                paybll.Model.ApproveFlag = 2;
                paybll.Model.InsertStaff = User.StaffID;
                paybll.Add();
            }
            #endregion

            int ret = bll.Confirm(User.StaffID);

            if (ret < 0)
            {
                switch (ret)
                {
                    case -10:
                        ErrorInfo = "库存不足，确认失败!";
                        break;
                    case -20:
                        ErrorInfo = "收款额与销售金额不符!";
                        break;
                    case -21:
                        ErrorInfo = "预收款不够支付!";
                        break;
                    default:
                        ErrorInfo = "销售单确认失败!";
                        break;
                }
                return ret;
            }

            return 0;
        }
        #endregion

        #region 移库调拨申请
        /// <summary>
        /// 移库调拨申请
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryInfo">移库单信息</param>
        /// <param name="ErrorInfo">出错消息</param>
        /// <returns></returns>
        public static int TransWithVehicle(UserInfo User, Delivery DeliveryInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.TransWithVehicle:UserName=" + User.UserName +
                ",DeliveryInfo=" + JsonConvert.SerializeObject(DeliveryInfo));

            //经销商级人员，供货商只能是自己所属的经销商
            if (User.OwnerType == 3)
            {
                DeliveryInfo.Supplier = User.ClientID;
                DeliveryInfo.Client = User.ClientID;
            }

            if (DeliveryInfo.Classify != 5 && DeliveryInfo.Classify != 6)
            {
                ErrorInfo = "单据类别无效";
                return -1;
            }

            #region 根据车辆设置调入或调出车仓库
            //如果有送货车辆，默认出货仓库为该车辆所关联仓库
            if (DeliveryInfo.DeliveryVehicle != 0)
            {
                CM_Vehicle v = new CM_VehicleBLL(DeliveryInfo.DeliveryVehicle).Model;
                if (v == null) { ErrorInfo = "送货车辆无效!"; return -1; }

                if (DeliveryInfo.Classify == 5)
                {
                    //车销移库    
                    DeliveryInfo.ClientWareHouse = v.RelateWareHouse;
                }
                else if (DeliveryInfo.Classify == 6)
                {
                    //车销退库
                    DeliveryInfo.SupplierWareHouse = v.RelateWareHouse;
                }
            }
            #endregion

            //默认业务人员为当前员工
            if (DeliveryInfo.SalesMan == 0) DeliveryInfo.SalesMan = User.StaffID;

            #region 必填字段校验
            if (DeliveryInfo.Supplier == 0 && DeliveryInfo.SupplierWareHouse != 0)
            {
                DeliveryInfo.Supplier = new CM_WareHouseBLL(DeliveryInfo.SupplierWareHouse).Model.Client;
            }
            if (DeliveryInfo.Supplier == 0) { ErrorInfo = "无效的客户!"; return -2; }
            if (DeliveryInfo.SupplierWareHouse == 0) { ErrorInfo = "无效的调出仓库!"; return -2; }
            if (DeliveryInfo.Client == 0) { DeliveryInfo.Client = DeliveryInfo.Supplier; }
            if (DeliveryInfo.ClientWareHouse == 0) { ErrorInfo = "无效的调入仓库!"; return -2; }
            if (DeliveryInfo.Items == null || DeliveryInfo.Items.Count == 0) { ErrorInfo = "无调拨产品明细!"; return -10; }
            #endregion

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL();

            #region 保存发货单头信息
            bll.Model.SheetCode = "";
            bll.Model.Supplier = DeliveryInfo.Supplier;
            bll.Model.SupplierWareHouse = DeliveryInfo.SupplierWareHouse;
            bll.Model.Client = DeliveryInfo.Client;
            bll.Model.ClientWareHouse = DeliveryInfo.ClientWareHouse;
            bll.Model.Classify = DeliveryInfo.Classify;              //5:车销移库 6:车销退库
            bll.Model.SalesMan = User.StaffID;
            bll.Model.PrepareMode = 1;           //1:快捷模式
            bll.Model.State = 1;                 //默认制单状态
            bll.Model.DeliveryVehicle = DeliveryInfo.DeliveryVehicle;
            bll.Model.Remark = DeliveryInfo.Remark;
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = User.StaffID;
            #endregion

            #region 循环处理每个发货单明细
            foreach (Delivery.DeliveryDetail item in DeliveryInfo.Items)
            {
                if (item.Product == 0) continue;
                if (item.DeliveryQuantity <= 0 && item.SignInQuantity <= 0) continue;

                string lotnumber = item.LotNumber.Trim();
                int quantity = item.DeliveryQuantity <= 0 ? item.SignInQuantity : item.DeliveryQuantity;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                if (productbll.Model == null) { ErrorInfo = "无效产品项,产品ID:" + item.Product; return -11; }
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Supplier);
                if (productbll.Model == null) { ErrorInfo = "产品不在销售商的经营目录中," + productbll.Model.FullName; return -11; }

                //判断库存是否够销售
                int inv_quantity = INV_InventoryBLL.GetProductQuantity(bll.Model.SupplierWareHouse, item.Product, lotnumber);
                if (quantity > inv_quantity)
                {
                    ErrorInfo = "产品[" + productbll.Model.FullName + "]库存不足，当前库存：" + inv_quantity.ToString();
                    return -11;
                }

                #region 新增库存明细
                PBM_DeliveryDetail d = new PBM_DeliveryDetail();

                d.Product = item.Product;
                d.LotNumber = lotnumber;
                d.SalesMode = item.SalesMode == 0 ? 1 : item.SalesMode;     //默认为“销售”

                d.Price = extinfo.SalesPrice;       //默认销售价
                d.DiscountRate = 1;

                d.ConvertFactor = productbll.Model.ConvertFactor == 0 ? 1 : productbll.Model.ConvertFactor;
                d.DeliveryQuantity = quantity;
                d.SignInQuantity = d.DeliveryQuantity;
                d.Remark = item.Remark;

                bll.Items.Add(d);
                #endregion
            }
            #endregion

            //计算折扣金额
            bll.Model.DiscountAmount = 0;
            bll.Model.WipeAmount = 0;

            //计算实际销售金额
            bll.Model.ActAmount = Math.Round(bll.Items.Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor), 2);

            int ret = bll.Add();
            if (ret <= 0) { ErrorInfo = "销售单保存失败!"; return ret; }
            return ret;
        }
        #endregion

        #region 查询车销调拨单
        /// <summary>
        /// 查询车销调拨单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Vehicle">车辆ID</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<Delivery> GetTransDeliveryByVehicle(UserInfo User, int Vehicle, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetTransDeliveryByVehicle:UserName=" + User.UserName + ",Vehicle=" + Vehicle.ToString()
                 + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            string condition = " DeliveryVehicle=" + Vehicle.ToString() + " AND InsertTime BETWEEN '" + BeginDate.ToString("yyyy-MM-dd")
                + "' AND '" + EndDate.ToString("yyyy-MM-dd") + " 23:59:59' AND Classify IN (5,6) ";

            if (User.OwnerType == 3) condition += " AND Supplier=" + User.ClientID.ToString();

            IList<PBM_Delivery> deliverys = PBM_DeliveryBLL.GetModelList(condition);
            List<Delivery> list = new List<Delivery>(deliverys.Count);
            foreach (PBM_Delivery item in deliverys.OrderByDescending(p => p.InsertTime))
            {
                list.Add(new Delivery(item));
            }
            return list;
        }
        #endregion

        #region 获取赠送方式
        /// <summary>
        /// 获取赠送方式
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetDictionarySalseMode(UserInfo User)
        {
            LogWriter.WriteLog("PBMIFService.GetDictionarySalseMode:UserName=" + User.UserName);

            Dictionary<string, Dictionary_Data> dicts = DictionaryBLL.GetDicCollections("PBM_SalseMode");

            List<DicDataItem> list = new List<DicDataItem>(dicts.Count);
            foreach (Dictionary_Data item in dicts.Values)
            {
                list.Add(new DicDataItem(int.Parse(item.Code), item.Name, item.Code, item.Description));
            }
            return list;
        }
        #endregion

        #region 销售单汇总
        /// <summary>
        /// 汇总预售待送货列表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Vehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DisplayMode">1:按产品汇总 2:按客户汇总</param>
        /// <returns></returns>
        public static List<DicDataItem> GetDeliverySummary(UserInfo User, int TDP, int Vehicle, DateTime BeginDate, DateTime EndDate, int DisplayMode)
        {
            LogWriter.WriteLog("PBMIFService.GetDeliverySummary:UserName=" + User.UserName + ",Vehicle=" + Vehicle.ToString() +
            ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd") + ",DisplayMode=" + DisplayMode.ToString());

            if (User.OwnerType == 3) TDP = User.ClientID;

            DataTable dt = null;

            if (DisplayMode == 1)
                dt = PBM_DeliveryBLL.GetDeliverySummary_ByProduct(TDP, User.StaffID, 0, Vehicle, BeginDate, EndDate);
            else
                dt = PBM_DeliveryBLL.GetDeliverySummary_ByClient(TDP, User.StaffID, 0, Vehicle, BeginDate, EndDate);

            List<DicDataItem> list = new List<DicDataItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (DisplayMode == 1)
                {
                    string remark = "";
                    if ((int)dr["Quantity_T"] != 0)
                    {
                        remark += dr["Quantity_T"].ToString() + dr["Packagint_T"].ToString();
                    }

                    if ((int)dr["Quantity_P"] != 0)
                    {
                        remark += dr["Quantity_P"].ToString() + dr["Packagint_P"].ToString();
                    }

                    //remark += " 重量:" + dr["TotalWeight"].ToString() + "Kg";

                    list.Add(new DicDataItem((int)dr["Product"], dr["ProductName"].ToString(), ((decimal)dr["TotalAmount"]).ToString("0.##"), remark));
                }
                else
                    list.Add(new DicDataItem((int)dr["Client"], dr["ClientName"].ToString(), ((decimal)dr["TotalAmount"]).ToString("0.##"), ""));
            }
            return list;

        }

        /// <summary>
        /// 按送货人查询销售收款汇总
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetDeliveryPayInfoSummary(UserInfo User, int TDP, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetDeliveryPayInfoSummary:UserName=" + User.UserName +
           ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            DataTable dt = PBM_DeliveryBLL.GetPayInfoSummary(TDP, User.StaffID, 0, BeginDate, EndDate);

            List<DicDataItem> list = new List<DicDataItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                list.Add(new DicDataItem(0, dr["PayModeName"].ToString(), ((decimal)dr["Amount"]).ToString("0.##"), ""));
            }
            return list;
        }

        /// <summary>
        /// 按送货人查询销售收款明细
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetDeliveryPayInfoDetail(UserInfo User, int TDP, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetDeliveryPayInfoDetail:UserName=" + User.UserName +
           ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            DataTable dt = PBM_DeliveryBLL.GetPayInfoDetail(TDP, User.StaffID, 0, BeginDate, EndDate);

            List<DicDataItem> list = new List<DicDataItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                list.Add(new DicDataItem((int)dr["ID"], dr["PayModeName"].ToString(), ((decimal)dr["Amount"]).ToString("0.##"), dr["ClientName"].ToString()));
            }
            return list;
        }
        #endregion

        #endregion

        #region 送货管理
        /// <summary>
        /// 获取已派发给当前员工的待送货的销售单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Vehicle">车辆，0时不限定车辆</param>
        /// <param name="BeginDate">预计送货日期-开始</param>
        /// <param name="EndDate">预计送货日期-截止</param>
        /// <returns></returns>
        public static List<Delivery> GetNeedDeliveryList(UserInfo User, int TDP, int Vehicle, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetNeedDeliveryList:UserName=" + User.UserName + ",Vehicle=" + Vehicle.ToString() +
            ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            string condition = " PrepareMode = 3 AND Classify IN (1,4) AND State IN (2,3) AND DeliveryMan=" + User.StaffID.ToString()
                + " AND PreArrivalDate BETWEEN '" + BeginDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + " 23:59:59' ";

            if (TDP != 0) condition += " AND Supplier = " + TDP.ToString();

            if (Vehicle != 0) condition += " AND DeliveryVehicle = " + Vehicle.ToString();

            if (User.OwnerType == 3) condition += " AND Supplier=" + User.ClientID.ToString();

            IList<PBM_Delivery> deliverys = PBM_DeliveryBLL.GetModelList(condition);
            List<Delivery> list = new List<Delivery>(deliverys.Count);
            foreach (PBM_Delivery item in deliverys.OrderByDescending(p => p.InsertTime))
            {
                list.Add(new Delivery(item));
            }
            return list;

        }

        /// <summary>
        /// 汇总预售待送货列表
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Vehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<DicDataItem> GetNeedDeliverySummary(UserInfo User, int TDP, int Vehicle, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetNeedDeliverySummary:UserName=" + User.UserName + ",Vehicle=" + Vehicle.ToString() +
                ",TDP=" + TDP.ToString() + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            int warehouse = 0;
            if (Vehicle > 0)
            {
                warehouse = new CM_VehicleBLL(Vehicle).Model.RelateWareHouse;
            }
            DataTable dt = PBM_DeliveryBLL.GetNeedDeliverySummary(BeginDate, EndDate, TDP, warehouse, 0, User.StaffID);

            List<DicDataItem> list = new List<DicDataItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string remark = "";
                if ((int)dr["Quantity_T"] != 0)
                {
                    remark += dr["Quantity_T"].ToString() + dr["Packagint_T"].ToString();
                }

                if ((int)dr["Quantity_P"] != 0)
                {
                    remark += dr["Quantity_P"].ToString() + dr["Packagint_P"].ToString();
                }

                remark += " 重量:" + dr["Weight"].ToString() + "Kg";
                list.Add(new DicDataItem((int)dr["Product"], dr["ProductName"].ToString(), dr["Quantity"].ToString(), remark));
            }
            return list;

        }
        #endregion

        #region 查询指定ID的发货单
        /// <summary>
        /// 查询指定ID的发货单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryID">发货单ID</param>
        /// <returns></returns>
        public static Delivery GetDeliveryByDeliveryID(UserInfo User, int DeliveryID)
        {
            LogWriter.WriteLog("PBMIFService.GetDeliveryByDeliveryID:UserName=" + User.UserName + ",DeliveryID=" + DeliveryID.ToString());

            return new Delivery(DeliveryID);
        }
        #endregion

        #region 取消发货单
        /// <summary>
        /// 取消销售单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryID"></param>
        /// <param name="CancelReason"></param>
        /// <returns></returns>
        public static int Delivery_Cancel(UserInfo User, int DeliveryID, string CancelReason, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.Declivery_Cancel:UserName=" + User.UserName + ",DeliveryID=" + DeliveryID.ToString() +
                ",CancelReason=" + CancelReason);

            if (DeliveryID <= 0) { ErrorInfo = "销售单ID无效"; return -1; }

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL(DeliveryID);
            if (bll.Model == null) { ErrorInfo = "销售单ID无效"; return -1; }
            if (bll.Model.State > 3) { ErrorInfo = "销售单状态无效"; return -1; }

            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "不可取消该销售单"; return -2; }

            int ret = bll.Cancel(User.StaffID, CancelReason);
            if (ret < 0) { ErrorInfo = "销售单取消失败!"; return -1; }

            return 0;

        }
        #endregion

        #region 预售管理

        #region 查询指定门店的预售订单
        /// <summary>
        /// 查询门店指定日期范围内的预售订单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Retailer"></param>
        /// <param name="StateFlag">状态标志 ALL:所有 COMPLETE:已完成的销售单 UNCOMPLETE:未完成的销售单</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<Order> GetOrderListByRetailer(UserInfo User, int Retailer, string StateFlag, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetOrderListByRetailer:UserName=" + User.UserName + ",Retailer=" + Retailer.ToString() +
                ",StateFlag=" + StateFlag + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            string condition = " Client=" + Retailer.ToString() + " AND InsertTime BETWEEN '" + BeginDate.ToString("yyyy-MM-dd")
                + "' AND '" + EndDate.ToString("yyyy-MM-dd") + " 23:59:59' ";

            if (User.OwnerType == 3) condition += " AND Supplier=" + User.ClientID.ToString();

            if (StateFlag.ToUpper() == "ALL")
                condition += " AND State<>9";
            else if (StateFlag.ToUpper() == "COMPLETE")
                condition += " AND State IN (3,4,5)";
            else if (StateFlag.ToUpper() == "UNCOMPLETE")
                condition += " AND State IN (1,2)";
            else
                condition += " AND State IN (0)";

            IList<PBM_Order> deliverys = PBM_OrderBLL.GetModelList(condition);
            List<Order> list = new List<Order>(deliverys.Count);
            foreach (PBM_Order item in deliverys.OrderByDescending(p => p.InsertTime))
            {
                list.Add(new Order(item));
            }
            return list;
        }
        #endregion

        #region 新增预售订单
        /// <summary>
        /// 新增预售订单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="OrderInfo">发货单信息</param>
        /// <param name="ErrorInfo">出错消息</param>
        /// <returns></returns>
        public static int Order_Add(UserInfo User, Order OrderInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.Order_Add:UserName=" + User.UserName +
                ",OrderInfo=" + JsonConvert.SerializeObject(OrderInfo));

            //经销商级人员，供货商只能是自己所属的经销商
            if (User.OwnerType == 3) OrderInfo.Supplier = User.ClientID;

            //默认业务人员为当前员工
            if (OrderInfo.SalesMan == 0) OrderInfo.SalesMan = User.StaffID;

            #region 必填字段校验
            if (OrderInfo.Supplier == 0) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (OrderInfo.Client == 0) { ErrorInfo = "无效的购买客户!"; return -2; }
            if (OrderInfo.Items == null || OrderInfo.Items.Count == 0) { ErrorInfo = "无销售产品明细!"; return -10; }
            #endregion

            PBM_OrderBLL bll = new PBM_OrderBLL();

            #region 保存销售单头信息
            bll.Model.SheetCode = "";
            bll.Model.Supplier = OrderInfo.Supplier;
            bll.Model.Client = OrderInfo.Client;
            bll.Model.SalesMan = OrderInfo.SalesMan;
            bll.Model.Classify = (OrderInfo.Classify == 0 ? 1 : OrderInfo.Classify);              //默认销售单
            bll.Model.State = 1;        //默认制单状态
            bll.Model.StandardPrice = OrderInfo.StandardPrice;

            bll.Model.WipeAmount = OrderInfo.WipeAmount;
            bll.Model.ArriveTime = OrderInfo.ArriveTime < DateTime.Today ? DateTime.Today.AddDays(1) : OrderInfo.ArriveTime;
            bll.Model.WorkList = OrderInfo.WorkList;
            bll.Model.Remark = OrderInfo.Remark;
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = User.StaffID;

            //订单来源
            bll.Model["OrderSource"] = OrderInfo.OrderSource > 0 ? OrderInfo.OrderSource.ToString() : "2";

            #endregion

            #region 循环处理每个订单明细
            foreach (Order.OrderDetail item in OrderInfo.Items)
            {
                if (item.Product == 0) continue;
                if (item.BookQuantity <= 0 && item.ConfirmQuantity <= 0) continue;

                int bookquantity = item.BookQuantity <= 0 ? item.ConfirmQuantity : item.BookQuantity;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                if (productbll.Model == null) { ErrorInfo = "无效产品项,产品ID:" + item.Product; return -11; }
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Supplier);
                if (productbll.Model == null) { ErrorInfo = "产品不在销售商的经营目录中," + productbll.Model.FullName; return -11; }

                #region 新增商品明细
                PBM_OrderDetail d = new PBM_OrderDetail();

                d.Product = item.Product;
                d.SalesMode = item.SalesMode == 0 ? 1 : item.SalesMode;     //默认为“销售”

                if (item.Price > 0)
                    d.Price = item.Price;
                else
                    d.Price = PDT_StandardPriceBLL.GetSalePrice(bll.Model.Client, bll.Model.Supplier, d.Product);       //默认销售价

                if (d.SalesMode == 1)
                    d.DiscountRate = (item.DiscountRate <= 0 || item.DiscountRate > 1) ? 1 : item.DiscountRate;
                else
                    d.DiscountRate = 0;     //非销售时，0折销售

                d.ConvertFactor = productbll.Model.ConvertFactor == 0 ? 1 : productbll.Model.ConvertFactor;
                d.BookQuantity = bookquantity;
                d.ConfirmQuantity = d.BookQuantity;
                d.Remark = item.Remark;

                bll.Items.Add(d);
                #endregion
            }
            #endregion

            //计算折扣金额
            bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                Math.Round(p.Price * p.ConvertFactor, 2) * p.ConfirmQuantity / p.ConvertFactor);

            //计算实际销售金额
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.ConfirmQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);

            int orderid = bll.Add();
            if (orderid <= 0) { ErrorInfo = "销售订单保存失败!"; return orderid; }

            #region 判断订单是否直接提交
            if (OrderInfo.State == 2)
            {
                LogWriter.WriteLog("PBMIFService.Order_Add:UserName=" + User.UserName + ",OrderID=" + orderid.ToString() + ",Auto submit order!");
                Order OutOrderInfo;
                if (OrderInfo.PayInfos == null) OrderInfo.PayInfos = new List<Order.OrderPayInfo>();
                int ret = Order_Submit(User, orderid, OrderInfo.WipeAmount, OrderInfo.PayInfos, out OutOrderInfo, out ErrorInfo);
                if (ret < 0)
                {
                    LogWriter.WriteLog("PBMIFService.Order_Add:UserName=" + User.UserName + ",OrderID=" + orderid.ToString() +
                        ",Auto submit failed!ErrorInfo=" + ErrorInfo);
                    return ret;
                }
            }
            #endregion

            return orderid;
        }
        #endregion

        #region 更新预售订单
        /// <summary>
        /// 更新预售订单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="OrderInfo"></param>
        /// <param name="ErrorInfo"></param>
        /// <returns></returns>
        public static int Order_Update(UserInfo User, Order OrderInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.Order_Update:UserName=" + User.UserName +
                ",OrderInfo=" + JsonConvert.SerializeObject(OrderInfo));

            if (OrderInfo.ID == 0) { ErrorInfo = "销售订单不存在，请先新增销售订单!"; return -1; }

            PBM_OrderBLL bll = new PBM_OrderBLL(OrderInfo.ID);
            if (bll.Model == null) { ErrorInfo = "订货单不存在，请先新增订货单!"; return -1; }
            if (bll.Model.State > 1 || bll.Model.ApproveFlag == 1) { ErrorInfo = "订货单状态不允许执行此操作!"; return -1; }

            if (bll.Model.Supplier == 0) bll.Model.Supplier = OrderInfo.Supplier;
            if (bll.Model.Client == 0) bll.Model.Client = OrderInfo.Client;

            //默认业务人员为当前员工
            if (OrderInfo.SalesMan == 0) OrderInfo.SalesMan = User.StaffID;

            #region 必填字段校验
            if (bll.Model.Supplier == 0) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "无效的供货客户!"; return -2; }
            if (bll.Model.Client == 0) { ErrorInfo = "无效的购买客户!"; return -2; }

            if (OrderInfo.Items == null || OrderInfo.Items.Count == 0) { ErrorInfo = "无订货产品明细!"; return -10; }
            #endregion

            #region 保存订货单头信息
            bll.Model.SalesMan = OrderInfo.SalesMan;
            bll.Model.WipeAmount = OrderInfo.WipeAmount;
            bll.Model.ArriveTime = OrderInfo.ArriveTime < DateTime.Today ? DateTime.Today.AddDays(1) : OrderInfo.ArriveTime;
            bll.Model.WorkList = OrderInfo.WorkList;
            bll.Model.Remark = OrderInfo.Remark;
            #endregion

            #region 循环处理每个订单明细
            foreach (Order.OrderDetail item in OrderInfo.Items)
            {
                if (item.Product == 0) continue;
                if (item.BookQuantity <= 0 && item.ConfirmQuantity <= 0)
                {
                    if (item.DetailID == 0)
                        continue;
                    else
                        bll.DeleteDetail(item.DetailID);
                }

                int quantity = item.BookQuantity == 0 ? item.ConfirmQuantity : item.BookQuantity;

                string remark = item.Remark;

                PDT_ProductBLL productbll = new PDT_ProductBLL(item.Product);
                if (productbll.Model == null) { ErrorInfo = "无效产品项,产品ID:" + item.Product; return -11; }
                PDT_ProductExtInfo extinfo = productbll.GetProductExtInfo(bll.Model.Supplier);
                if (productbll.Model == null) { ErrorInfo = "产品不在销售商的经营目录中," + productbll.Model.FullName; return -11; }

                if (item.DetailID > 0)
                {
                    PBM_OrderDetail d = bll.GetDetailModel(item.DetailID);

                    d.ConvertFactor = productbll.Model.ConvertFactor == 0 ? 1 : productbll.Model.ConvertFactor;
                    d.BookQuantity = quantity;
                    d.ConfirmQuantity = quantity;
                    d.Remark = item.Remark;
                    bll.UpdateDetail(d);
                }
                else
                {
                    #region 新增商品明细品项
                    PBM_OrderDetail d = new PBM_OrderDetail();

                    d.Product = item.Product;
                    d.SalesMode = item.SalesMode == 0 ? 1 : item.SalesMode;     //默认为“销售”

                    d.Price = PDT_StandardPriceBLL.GetSalePrice(bll.Model.Client, bll.Model.Supplier, d.Product);
                    if (d.SalesMode == 1)
                        d.DiscountRate = (item.DiscountRate <= 0 || item.DiscountRate > 1) ? 1 : item.DiscountRate;
                    else
                        d.DiscountRate = 0;

                    d.BookQuantity = quantity;
                    d.ConfirmQuantity = d.BookQuantity;
                    d.Remark = item.Remark;

                    bll.AddDetail(d);
                    #endregion
                }
            }
            #endregion

            //计算折扣金额
            bll.Model.DiscountAmount = bll.Items.Sum(p => (1 - p.DiscountRate) *
                Math.Round(p.Price * p.ConvertFactor, 2) * p.ConfirmQuantity / p.ConvertFactor);

            //计算实际销售金额
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.ConfirmQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);

            int ret = bll.Update();
            if (ret < 0) { ErrorInfo = "订货单保存失败!"; return ret; }

            return 0;
        }
        #endregion

        #region 提交订货单
        /// <summary>
        /// 提交订货单,并输出提交后的订单信息
        /// </summary>
        /// <param name="User"></param>
        /// <param name="OrderID">订货单ID</param>
        /// <param name="OrderInfo">输出：订货单结构</param>
        /// <param name="ErrorInfo">输出：出错信息</param>
        /// <returns>0:成功 小于0:失败</returns>
        public static int Order_Submit(UserInfo User, int OrderID, decimal WipeAmount, List<Order.OrderPayInfo> PayInfoList, out Order OrderInfo, out string ErrorInfo)
        {
            ErrorInfo = "";
            OrderInfo = null;
            LogWriter.WriteLog("PBMIFService.Order_Submit:UserName=" + User.UserName + ",OrderID=" + OrderID.ToString());

            if (OrderID <= 0) { ErrorInfo = "订货单ID无效"; return -1; }

            PBM_OrderBLL bll = new PBM_OrderBLL(OrderID);
            if (bll.Model == null) { ErrorInfo = "订货单ID无效"; return -1; }
            if (bll.Model.State > 1 || bll.Model.ApproveFlag == 1) { ErrorInfo = "订货单状态无效"; return -1; }

            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "不可提交该订货单"; return -2; }


            bll.Model.WipeAmount = WipeAmount;
            bll.Model.ActAmount = Math.Round((bll.Model.Classify == 2 ? -1 : 1) *
                bll.Items.Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.ConfirmQuantity / p.ConvertFactor)
                - bll.Model.WipeAmount, 2);
            bll.Update();

            #region 写入收款明细
            //先清除之前的付款信息
            if (bll.GetPayInfoList().Count > 0) bll.ClearPayInfo();

            foreach (Order.OrderPayInfo item in PayInfoList)
            {
                PBM_DeliveryPayInfoBLL paybll = new PBM_DeliveryPayInfoBLL();
                paybll.Model.DeliveryID = OrderID;
                paybll.Model.PayMode = item.PayMode;
                paybll.Model.Amount = item.Amount;
                paybll.Model.Remark = item.Remark;
                paybll.Model.ApproveFlag = 2;
                paybll.Model.InsertStaff = User.StaffID;
                paybll.Add();
            }
            #endregion

            int ret = bll.Submit(User.StaffID);
            if (ret < 0) { ErrorInfo = "订货单提交失败!"; return -1; }

            OrderInfo = new Order(bll.Model.ID);
            return 0;
        }
        #endregion

        #region 取消预售订单
        /// <summary>
        /// 取消预售订单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="OrderID"></param>
        /// <param name="CancelReason"></param>
        /// <param name="ErrorInfo"></param>
        /// <returns></returns>
        public static int Order_Cancel(UserInfo User, int OrderID, string CancelReason, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.Order_Cancel:UserName=" + User.UserName + ",OrderID=" + OrderID.ToString() +
                ",CancelReason=" + CancelReason);

            if (OrderID <= 0) { ErrorInfo = "预售订单ID无效"; return -1; }

            PBM_OrderBLL bll = new PBM_OrderBLL(OrderID);
            if (bll.Model == null) { ErrorInfo = "预售订单ID无效"; return -1; }
            if (bll.Model.State > 2) { ErrorInfo = "预售订单状态无效"; return -1; }

            if (User.OwnerType == 3 && bll.Model.Supplier != User.ClientID) { ErrorInfo = "不可取消该销售单"; return -2; }

            int ret = bll.Cancel(User.StaffID, CancelReason);
            if (ret < 0) { ErrorInfo = "预售订单取消失败!"; return -1; }

            return 0;
        }
        #endregion

        #region 查询指定ID的预售订单
        /// <summary>
        /// 查询指定ID的发货单
        /// </summary>
        /// <param name="User"></param>
        /// <param name="DeliveryID">发货单ID</param>
        /// <returns></returns>
        public static Order GetOrderByOrderID(UserInfo User, int OrderID)
        {
            LogWriter.WriteLog("PBMIFService.GetOrderByOrderID:UserName=" + User.UserName + ",OrderID=" + OrderID.ToString());

            return new Order(OrderID);
        }
        #endregion

        #region 预售订单汇总
        /// <summary>
        /// 预售订单汇总
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Vehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DisplayMode">1:按产品汇总 2:按客户汇总</param>
        /// <returns></returns>
        public static List<DicDataItem> GetOrderSummary(UserInfo User, int TDP, DateTime BeginDate, DateTime EndDate, int DisplayMode)
        {
            LogWriter.WriteLog("PBMIFService.GetOrderSummary:UserName=" + User.UserName +
            ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd") + ",DisplayMode=" + DisplayMode.ToString());

            if (User.OwnerType == 3) TDP = User.ClientID;

            DataTable dt = null;

            if (DisplayMode == 1)
                dt = PBM_OrderBLL.GetOrderSummary_ByProduct(TDP, User.StaffID, BeginDate, EndDate);
            else
                dt = PBM_OrderBLL.GetOrderSummary_ByClient(TDP, User.StaffID, BeginDate, EndDate);

            List<DicDataItem> list = new List<DicDataItem>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                if (DisplayMode == 1)
                {
                    string remark = "";
                    if ((int)dr["Quantity_T"] != 0)
                    {
                        remark += dr["Quantity_T"].ToString() + dr["Packagint_T"].ToString();
                    }

                    if ((int)dr["Quantity_P"] != 0)
                    {
                        remark += dr["Quantity_P"].ToString() + dr["Packagint_P"].ToString();
                    }

                    //remark += " 重量:" + dr["TotalWeight"].ToString() + "Kg";

                    list.Add(new DicDataItem((int)dr["Product"], dr["ProductName"].ToString(), ((decimal)dr["TotalAmount"]).ToString("0.##"), remark));
                }
                else
                    list.Add(new DicDataItem((int)dr["Client"], dr["ClientName"].ToString(), ((decimal)dr["TotalAmount"]).ToString("0.##"), ""));
            }
            return list;

        }
        #endregion

        #endregion

        #region 财务收款管理

        #region 查询门店预收款可用余额
        /// <summary>
        /// 查询门店预收款可用余额
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <returns></returns>
        public static decimal GetRetailerPreReceivedBalance(UserInfo User, int TDP, int Retailer)
        {
            LogWriter.WriteLog("PBMIFService.GetRetailerPreReceivedBalance:UserName=" + User.UserName
                + ",TDP=" + TDP.ToString() + ",Retailer=" + Retailer.ToString());

            if (User.OwnerType == 3) TDP = User.ClientID;
            IList<AC_CurrentAccount> acc = AC_CurrentAccountBLL.GetModelList(string.Format("OwnerClient={0} AND TradeClient={1}", TDP, Retailer));
            if (acc == null || acc.Count == 0)
                return 0;
            else
                return acc[0].PreReceivedBalance;
        }
        #endregion

        #region 获取零售店预收款及应收款额
        /// <summary>
        /// 获取零售店预收款及应收款额
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="PreReceivedAmount">预收款额</param>
        /// <param name="AR">应收款额</param>
        /// <param name="PreReceivedBalance">合计余额（预收款额 - 应收款额）</param>
        /// <returns></returns>
        public static int GetRetailerCurrentAccount(UserInfo User, int TDP, int Retailer,
            out decimal PreReceivedAmount, out decimal AR, out decimal PreReceivedBalance)
        {
            PreReceivedAmount = 0;
            AR = 0;
            PreReceivedBalance = 0;

            LogWriter.WriteLog("PBMIFService.GetRetailerCurrentAccount:UserName=" + User.UserName
                + ",TDP=" + TDP.ToString() + ",Retailer=" + Retailer.ToString());

            if (User.OwnerType == 3) TDP = User.ClientID;
            AC_CurrentAccount ac = AC_CurrentAccountBLL.GetByTradeClient(TDP, Retailer);

            if (ac == null)
                return 0;
            else
            {
                PreReceivedAmount = ac.PreReceivedAmount;
                AR = ac.AR;
                PreReceivedBalance = PreReceivedAmount - AR;
                return 0;
            }
        }
        #endregion

        #region 收预收款
        /// <summary>
        /// 收预收款
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount"></param>
        /// <param name="WorkList"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public static int Receipt_PreReceived(UserInfo User, int TDP, int Retailer, int PayMode, decimal Amount, int WorkList, string Remark)
        {
            LogWriter.WriteLog("PBMIFService.Receipt_PreReceived:UserName=" + User.UserName + ",TDP=" + TDP.ToString() + ",Retailer=" + Retailer.ToString() +
                ",PayMode=" + PayMode.ToString() + ",Amount=" + Amount.ToString() + ",WorkList=" + WorkList.ToString() + ",Remark=" + Remark);

            if (User.OwnerType == 3) TDP = User.ClientID;

            return AC_CashFlowListBLL.Receipt_PreReceived(TDP, Retailer, User.StaffID, PayMode, Amount, 0, Remark, User.StaffID, WorkList);
        }
        #endregion

        #region 查询应收款记录
        /// <summary>
        /// 查询终端门店应收款记录
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="BalanceFlag">结算标记 0所有 1未结 2已结</param>
        /// <returns></returns>
        public static List<ARAPInfo> GetRetailerARList(UserInfo User, int TDP, int Retailer, DateTime BeginDate, DateTime EndDate, int BalanceFlag)
        {
            LogWriter.WriteLog("PBMIFService.GetRetailerARList:UserName=" + User.UserName + ",TDP=" + TDP.ToString() +
                ",Retailer=" + Retailer.ToString() + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd") +
                ",BalanceFlag=" + BalanceFlag.ToString());

            if (User.OwnerType == 3) TDP = User.ClientID;

            string condition = string.Format("OwnerClient={0} AND TradeClient={1} AND Type=1 AND InsertTime BETWEEN '{2:yyyy-MM-dd}' AND '{3:yyyy-MM-dd} 23:59:59'",
                TDP, Retailer, BeginDate, EndDate);
            if (BalanceFlag != 0) condition += " AND BalanceFlag=" + BalanceFlag.ToString();

            IList<AC_ARAPList> ars = AC_ARAPListBLL.GetModelList(condition);
            List<ARAPInfo> list = new List<ARAPInfo>(ars.Count);
            foreach (AC_ARAPList item in ars.OrderBy(p => p.InsertTime))
            {
                list.Add(new ARAPInfo(item));
            }
            return list;
        }
        #endregion

        #region 结应收款
        /// <summary>
        /// 结应收款_现金结
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="Remark">备注</param>
        /// <param name="WorkList">拜访日志</param>
        /// <param name="ARIDs">应收单据ID,多个单据间以逗号间隔</param>
        /// <param name="ErrorInfo">输出：出错信息</param>
        /// <returns></returns>
        public static int BalanceRetailerAR_Receipt(UserInfo User, int TDP, int Retailer, int PayMode, decimal Amount,
            string Remark, int WorkList, string ARIDs, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.BalanceRetailerAR_Receipt:UserName=" + User.UserName + ",TDP=" + TDP.ToString() +
               ",Retailer=" + Retailer.ToString() + ",PayMode=" + PayMode.ToString() + ",Amount=" + Amount.ToString() +
               ",Remark=" + Remark + ",WorkList=" + WorkList.ToString() + ",ARIDs=" + ARIDs);

            if (User.OwnerType == 3) TDP = User.ClientID;

            int ret = AC_CashFlowListBLL.Receipt_BalanceAR(TDP, Retailer, User.StaffID, PayMode, Math.Round(Amount, 2),
                Remark, User.StaffID, WorkList, ARIDs);

            if (ret < 0)
            {
                switch (ret)
                {
                    case -10:
                        ErrorInfo = "应收款ID为空";
                        break;
                    case -11:
                        ErrorInfo = "应收款金额与收款金额不匹配";
                        break;
                    case -12:
                        ErrorInfo = "统计应收款总额时出错";
                        break;
                    default:
                        ErrorInfo = "收款出错,Ret=" + ret.ToString();
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// 结应收款_余额结
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="Amount"></param>
        /// <param name="Remark"></param>
        /// <param name="ARIDs"></param>
        /// <param name="ErrorInfo"></param>
        /// <returns></returns>
        public static int BalanceRetailerAR_UsageBalance(UserInfo User, int TDP, int Retailer, decimal Amount,
            string Remark, string ARIDs, out string ErrorInfo)
        {
            ErrorInfo = "";
            LogWriter.WriteLog("PBMIFService.BalanceRetailerAR_UsageBalance:UserName=" + User.UserName + ",TDP=" + TDP.ToString() +
               ",Retailer=" + Retailer.ToString() + ",Amount=" + Amount.ToString() + ",Remark=" + Remark + ",ARIDs=" + ARIDs);

            if (User.OwnerType == 3) TDP = User.ClientID;

            int ret = AC_BalanceUsageListBLL.BalanceAR(TDP, Retailer, User.StaffID, Math.Round(Amount, 2), Remark, ARIDs);

            if (ret < 0)
            {
                switch (ret)
                {
                    case -10:
                        ErrorInfo = "预收款金额不够结算";
                        break;
                    case -11:
                        ErrorInfo = "应收款金额与收算金额不匹配";
                        break;
                    case -12:
                        ErrorInfo = "统计应收款总额时出错";
                        break;
                    default:
                        ErrorInfo = "收款出错,Ret=" + ret.ToString();
                        break;
                }
            }

            return ret;
        }
        #endregion

        #region 查询预收款变动记录
        /// <summary>
        /// 查询指定门店的预收款变动记录
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="Retailer"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<BalanceUsageInfo> GetRetailerBalanceUsageList(UserInfo User, int TDP, int Retailer, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetRetailerBalanceUsageList:UserName=" + User.UserName + ",TDP=" + TDP.ToString() +
                ",Retailer=" + Retailer.ToString() + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            string condition = string.Format("OwnerClient={0} AND TradeClient={1} AND InsertTime BETWEEN '{2:yyyy-MM-dd}' AND '{3:yyyy-MM-dd} 23:59:59'",
                TDP, Retailer, BeginDate, EndDate);

            IList<AC_BalanceUsageList> ars = AC_BalanceUsageListBLL.GetModelList(condition);
            List<BalanceUsageInfo> list = new List<BalanceUsageInfo>(ars.Count);
            foreach (AC_BalanceUsageList item in ars.OrderBy(p => p.InsertTime))
            {
                list.Add(new BalanceUsageInfo(item));
            }
            return list;
        }
        #endregion

        #region 查询业务员收款记录
        /// <summary>
        /// 查询指定人员当天的现金、POS收款记录
        /// </summary>
        /// <param name="User"></param>
        /// <param name="TDP"></param>
        /// <param name="PayMode">0:所有 1:现金、2:POS</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static List<CashFlowInfo> GetCashReceiptList(UserInfo User, int TDP, int PayMode, DateTime BeginDate, DateTime EndDate)
        {
            LogWriter.WriteLog("PBMIFService.GetCashReceiptList:UserName=" + User.UserName + ",TDP=" + TDP.ToString() +
               ",PayMode=" + PayMode.ToString() + ",BeginDate=" + BeginDate.ToString("yyyy-MM-dd") + ",EndDate=" + EndDate.ToString("yyyy-MM-dd"));

            if (User.OwnerType == 3) TDP = User.ClientID;

            string condition = string.Format("OwnerClient={0} AND AgentStaff={1} AND InsertTime BETWEEN '{2:yyyy-MM-dd}' AND '{3:yyyy-MM-dd} 23:59:59'",
                 TDP, User.StaffID, BeginDate, EndDate);

            if (PayMode > 0)
            {
                condition += " AND PayMode = " + PayMode.ToString();
            }

            IList<AC_CashFlowList> cashs = AC_CashFlowListBLL.GetModelList(condition);
            List<CashFlowInfo> list = new List<CashFlowInfo>(cashs.Count);
            foreach (AC_CashFlowList item in cashs.OrderBy(p => p.InsertTime))
            {
                list.Add(new CashFlowInfo(item));
            }
            return list;
        }
        #endregion
        #endregion
    }
}

