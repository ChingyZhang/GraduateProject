
// ===================================================================
// 文件： CM_ClientDAL.cs
// 项目名称：
// 创建时间：2009/2/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;
using MCSFramework.Model;
using MCSFramework.BLL.Pub;

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_ClientBLL业务逻辑BLL类
    /// </summary>
    public class CM_ClientBLL : BaseSimpleBLL<CM_Client>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientDAL";
        private CM_ClientDAL _dal;

        /// <summary>
        /// 默认厂商ID
        /// </summary>
        private const int DefaultManufacturer = 1000;

        #region 构造函数
        ///<summary>
        ///CM_ClientBLL
        ///</summary>
        public CM_ClientBLL()
            : base(DALClassName)
        {
            _dal = (CM_ClientDAL)_DAL;
            _m = new CM_Client();
        }

        public CM_ClientBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ClientDAL)_DAL;
            FillModel(id);
        }

        public CM_ClientBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ClientDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_Client> GetModelList(string condition)
        {
            return new CM_ClientBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取当前客户下所有的联系人信息
        /// </summary>
        /// <returns></returns>
        public IList<CM_LinkMan> GetLinkMan()
        {
            string linkmandalclass = "MCSFramework.SQLDAL.CM.CM_LinkManDAL";
            CM_LinkManDAL dal = (CM_LinkManDAL)DataAccess.CreateObject(linkmandalclass);

            return dal.GetModelList("ClientID=" + _m.ID.ToString());
        }

        /// <summary>
        /// 审核商业客户
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public int Approve(int State)
        {
            return _dal.Approve(_m.ID, State);
        }


        public override int Add()
        {
            int id = base.Add();
            return id;
        }

        #region 客户供货信息管理
        /// <summary>
        /// 获取指定供货商的供货信息
        /// </summary>
        /// <param name="Supplier">供货商ID</param>
        /// <returns></returns>
        public CM_ClientSupplierInfo GetSupplierInfo(int Supplier)
        {
            if (this.Model != null)
            {
                IList<CM_ClientSupplierInfo> supplier = CM_ClientSupplierInfoBLL.GetModelList("Supplier=" + Supplier.ToString() + " AND Client = " + this.Model.ID.ToString());
                if (supplier.Count > 0)
                {
                    return supplier[0];
                }
            }
            return null;
        }

        /// <summary>
        /// 获取当前客户所有有效的供货信息
        /// </summary>
        /// <returns></returns>
        public IList<CM_ClientSupplierInfo> GetSupplierInfo()
        {
            if (this.Model != null)
            {
                return CM_ClientSupplierInfoBLL.GetModelList("Client = " + this.Model.ID.ToString());
            }
            return null;
        }

        public CM_ClientSupplierInfo GetSupplierInfoByManufacturer(int Manufacturer)
        {
            if (this.Model != null)
            {
                IList<CM_ClientSupplierInfo> supplier = CM_ClientSupplierInfoBLL.GetModelList("Client = " + this.Model.ID.ToString()
                    + " AND Supplier IN (SELECT ID FROM MCS_CM.dbo.CM_Client WHERE OwnerClient = " + Manufacturer.ToString() + ")");
                if (supplier.Count > 0)
                {
                    return supplier[0];
                }
            }
            return null;
        }
        /// <summary>
        /// 设置当前客户的供货信息
        /// </summary>
        /// <param name="SupplierInfo"></param>
        /// <returns></returns>
        public int SetSupplierInfo(CM_ClientSupplierInfo SupplierInfo)
        {
            if (SupplierInfo == null) return -1;
            if (SupplierInfo.Supplier == 0) return -2;

            CM_ClientSupplierInfoBLL bll = new CM_ClientSupplierInfoBLL();
            if (SupplierInfo.ID == 0)
            {
                //判断是否已存在指定供货商的供货信息
                CM_ClientSupplierInfo org = GetSupplierInfo(SupplierInfo.Supplier);
                if (org == null)
                {
                    //不存在，新增一条供货信息
                    bll.Model = SupplierInfo;
                    bll.Model.Client = Model.ID;
                    if (bll.Model.BeginDate.Year == 1900) bll.Model.BeginDate = DateTime.Today;
                    if (bll.Model.State == 0) bll.Model.State = 1;
                    if (bll.Model.ApproveFlag == 0) bll.Model.ApproveFlag = 1;

                    #region 如果未指定价表，则获取经销商默认价表
                    if (SupplierInfo.StandardPrice == 0 && _m.ClientType == 3)
                    {
                        SupplierInfo.StandardPrice = PDT_StandardPriceBLL.GetDefaultPrice(SupplierInfo.Supplier);
                    }
                    #endregion

                    return bll.Add();
                }
                else
                {
                    //存在，则将原ID赋给本次更新的ID，执行后续的更新操作
                    SupplierInfo.ID = org.ID;
                }
            }

            //更新现在供货信息
            if (SupplierInfo.ID > 0)
            {
                bll.Model = SupplierInfo;
                return bll.Update();
            }

            return 0;
        }
        #endregion

        #region 厂商信息管理
        /// <summary>
        /// 获取默认厂商的厂家管理信息
        /// </summary>
        /// <returns></returns>
        public CM_ClientManufactInfo GetManufactInfo()
        {
            return GetManufactInfo(DefaultManufacturer);
        }
        /// <summary>
        /// 获取指定厂商的厂家管理信息
        /// </summary>
        /// <param name="Manufacturer"></param>
        /// <returns></returns>
        public CM_ClientManufactInfo GetManufactInfo(int Manufacturer)
        {
            if (this.Model != null)
            {
                IList<CM_ClientManufactInfo> ManufactList = CM_ClientManufactInfoBLL.GetModelList("Manufacturer = " + Manufacturer.ToString()
                    + " AND Client=" + this.Model.ID.ToString());
                if (ManufactList.Count > 0)
                {
                    return ManufactList[0];
                }
            }
            return null;
        }

        /// <summary>
        /// 设置客户的厂家管理信息
        /// </summary>
        /// <param name="ManufactInfo"></param>
        /// <returns></returns>
        public int SetManufactInfo(CM_ClientManufactInfo ManufactInfo)
        {
            if (ManufactInfo == null) return -1;
            if (ManufactInfo.Manufacturer == 0) ManufactInfo.Manufacturer = DefaultManufacturer;

            CM_ClientManufactInfoBLL bll = new CM_ClientManufactInfoBLL();
            if (ManufactInfo.ID == 0)
            {
                //判断是否已存在指定厂商的厂家管理信息
                CM_ClientManufactInfo org = GetManufactInfo(ManufactInfo.Manufacturer);
                if (org == null)
                {
                    //不存在，新增一条厂商管理信息
                    bll.Model = ManufactInfo;
                    bll.Model.Client = Model.ID;
                    if (bll.Model.BeginDate.Year == 1900) bll.Model.BeginDate = DateTime.Today;
                    if (bll.Model.State == 0) bll.Model.State = 1;
                    if (bll.Model.ApproveFlag == 0) bll.Model.ApproveFlag = 2;  //默认为未审核

                    return bll.Add();
                }
                else
                {
                    //存在，则将原ID赋给本次更新的ID，执行后续的更新操作
                    ManufactInfo.ID = org.ID;
                }
            }

            //更新现在供货信息
            if (ManufactInfo.ID > 0)
            {
                bll.Model = ManufactInfo;
                return bll.Update();
            }

            return 0;
        }
        #endregion

        #region 获取指定供货商下级门店列表
        /// <summary>
        /// 获取指定供货商下级门店列表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <param name="Salesman">业务员</param>
        /// <returns></returns>
        public static IList<CM_Client> GetRetailerListBySalesMan(int Supplier, int Salesman)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRetailerListBySalesMan(Supplier, Salesman);
        }
        #endregion

        #region 获取指定TDP的上游往来供货商
        /// <summary>
        /// 获取指定TDP所有的供货商
        /// </summary>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static IList<CM_Client> GetSupplierByTDP(int TDP)
        {
            CM_Client _c = new CM_ClientBLL(TDP).Model;
            if (_c == null) return null;

            string condition = " CM_Client.ClientType = 1 ";

            condition += " AND (CM_Client.OwnerType = 1 OR (CM_Client.OwnerType=2 AND CM_Client.ID=" + _c.OwnerClient.ToString()
                + ") OR (CM_Client.OwnerType=3 AND CM_Client.OwnerClient=" + TDP.ToString() + ") )";

            return GetModelList(condition);
        }

        /// <summary>
        /// 仅获取TDP自己管理的供货商
        /// </summary>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static IList<CM_Client> GetSupplierByTDPSelf(int TDP)
        {
            string condition = " CM_Client.ClientType = 1 ";

            condition += " AND (CM_Client.OwnerType=3 AND CM_Client.OwnerClient=" + TDP.ToString() + ") ";

            return GetModelList(condition);
        }
        #endregion

        #region 获取门店所在的城市编码
        /// <summary>
        /// 获取客户一级城市编码--地区编码
        /// </summary>
        /// <returns></returns>
        public string GetClientORGCOD1()
        {
            string ORGCOD2 = GetClientORGCOD2();
            if (!string.IsNullOrEmpty(ORGCOD2)) return "1" + ORGCOD2.Substring(0, 1);

            return "";
        }

        /// <summary>
        /// 获取客户二级城市编码
        /// </summary>
        /// <returns></returns>
        public string GetClientORGCOD2()
        {
            return GetORGCOD2ByCity(_m.OfficialCity);
        }

        /// <summary>
        /// 获取指定城市的二级城市编码
        /// </summary>
        /// <param name="OfficialCity"></param>
        /// <returns></returns>
        public static string GetORGCOD2ByCity(int OfficialCity)
        {
            Addr_OfficialCity city = new Addr_OfficialCityBLL(OfficialCity).Model;
            if (city == null) return "";

            return city.Code;
        }
        #endregion

        #region 客户资料查重判断
        /// <summary>
        /// 客户资料查重判断
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="OrgClientID"></param>
        /// <param name="Mobile"></param>
        /// <param name="TeleNum"></param>
        /// <param name="FullName"></param>
        /// <param name="Address"></param>
        /// <returns>0:无重复 大于0:与该客户ID号重复</returns>
        public static int CheckRepeat(int Supplier, int OrgClientID, string Mobile, string TeleNum, string FullName, string Address)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckRepeat(Supplier, OrgClientID, Mobile, TeleNum, FullName, Address);
        }
        #endregion
        /// <summary>
        /// 批量替换客户的供货商
        /// </summary>
        /// <param name="OrgSupplier"></param>
        /// <param name="NewSupplier"></param>
        /// <returns></returns>
        public static int ReplaceSupplier(int OrgSupplier, int NewSupplier)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReplaceSupplier(OrgSupplier, NewSupplier);
        }

        /// <summary>
        /// 批量替换客户的客户经理
        /// </summary>
        /// <param name="OrgClientManager"></param>
        /// <param name="NewClientManager"></param>
        /// <returns></returns>
        public static int ReplaceClientManager(int OrgClientManager, int NewClientManager, int ClientType)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReplaceClientManager(OrgClientManager, NewClientManager, ClientType);
        }

        /// <summary>
        /// 批量替换客户的客户经理
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="hospitalstring"></param>
        /// <returns></returns>
        public static int ReplaceClientManager(string ClientIDs, int NewClientManager, int ClientType)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReplaceClientManager(ClientIDs, NewClientManager, ClientType);
        }

        /// <summary>
        /// 子户头切换为主户头
        /// </summary>
        /// <param name="SubACClientID">待升级的经销商子户头</param>
        /// <returns></returns>
        public static int DISubACUpgrade(int SubACClientID)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.DISubACUpgrade(SubACClientID);
        }

        #region 客户覆盖片区
        public int ClientInOrganizeCity_Add(int Organizecity)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ClientInOrganizeCity_Add(_m.ID, Organizecity);
        }

        public int ClientInOrganizeCity_Delete(int Organizecity)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ClientInOrganizeCity_Delete(_m.ID, Organizecity);
        }

        public IList<Addr_OrganizeCity> ClientInOrganizeCity_GetOrganizeCitys()
        {
            return _dal.ClientInOrganizeCity_GetOrganizeCitys(_m.ID);
        }
        #endregion

        #region 物业常住人员
        public int StaffInProperty_Add(int Client, int Staff)
        {
            return _dal.StaffInProperty_Add(Client, Staff);
        }

        public int StaffInProperty_Delete(int Client, int Staff)
        {
            return _dal.StaffInProperty_Delete(Client, Staff);
        }
        #endregion


        #region 获取客户销量信息
        /// <summary>
        /// 获取指定客户指定月份的预计销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public decimal GetSalesForcast(int AccountMonth)
        {
            string CacheKey = "CM_ClientBLL-GetSalesForcast-Client-"
                + _m.ID.ToString() + "-AccountMonth-" + AccountMonth.ToString();
            object v = DataCache.GetCache(CacheKey);

            if (v != null)
                return (decimal)v;
            else
            {
                decimal value = _dal.GetSalesForcast(_m.ID, AccountMonth);

                DataCache.SetCache(CacheKey, value, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                return value;
            }
        }

        /// <summary>
        /// 获取指定客户指定月份的实际销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public decimal GetSalesVolume(int AccountMonth)
        {
            string CacheKey = "CM_ClientBLL-GetSalesVolume-Client-"
                + _m.ID.ToString() + "-AccountMonth-" + AccountMonth.ToString();
            object v = DataCache.GetCache(CacheKey);

            if (v != null)
                return (decimal)v;
            else
            {
                decimal value = _dal.GetSalesVolume(_m.ID, AccountMonth);

                DataCache.SetCache(CacheKey, value, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                return value;
            }
        }

        /// <summary>
        /// 获取指定客户历史平均实际销量
        /// </summary>
        /// <param name="MonthCount"></param>
        /// <returns></returns>
        public decimal GetSalesVolumeAvg(int EndMonth, int MonthCount)
        {
            string CacheKey = "CM_ClientBLL-GetSalesVolumeAvg-Client-"
                + _m.ID.ToString() + "-MonthCount-" + MonthCount.ToString();
            object v = DataCache.GetCache(CacheKey);

            if (v != null)
                return (decimal)v;
            else
            {
                decimal value = _dal.GetSalesVolumeAvg(_m.ID, EndMonth, MonthCount);

                DataCache.SetCache(CacheKey, value, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                return value;
            }
        }

        /// <summary>
        /// 获取指定客户历史平均实际销量(有特指月份之前)
        /// </summary>
        /// <param name="MonthCount"></param>
        /// <returns></returns>
        public decimal GetSalesVolumeAvg(int EndMonth)
        {
            return _dal.GetSalesVolumeAvg(_m.ID, EndMonth, 0);
        }

        /// <summary>
        /// 获取指定客户历史平均实际销量(无特指月份)
        /// </summary>
        /// <param name="MonthCount"></param>
        /// <returns></returns>
        public decimal GetSalesVolumeAvg()
        {
            return _dal.GetSalesVolumeAvg(_m.ID, 0, 0);
        }

        /// <summary>
        /// 获取门店指定品牌的销量占比
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Brand"></param>
        /// <returns></returns>
        public decimal GetBrandSalesVolumeRate(int AccountMonth, int Brand)
        {
            string CacheKey = "CM_ClientBLL-GetBrandSalesVolumeRate-Client-"
                + _m.ID.ToString() + "-Brand-" + Brand.ToString();
            object v = DataCache.GetCache(CacheKey);

            if (v != null)
                return (decimal)v;
            else
            {
                decimal value = _dal.GetBrandSalesVolumeRate(_m.ID, AccountMonth, Brand);

                DataCache.SetCache(CacheKey, value, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                return value;
            }
        }

        /// <summary>
        /// 获取门店指定品牌的销量占比
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Brand"></param>
        /// <returns></returns>
        public decimal GetBrandSalesVolumeRate(int Brand)
        {
            return GetBrandSalesVolumeRate(0, Brand);
        }
        #endregion

        #region 统计各类活跃状态门店数量
        public static DataTable GetClientCountByActiveFlag(int OrganizeCity, int Supplier, int ClientType)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientCountByActiveFlag(OrganizeCity, Supplier, ClientType);
        }
        #endregion
    }
}