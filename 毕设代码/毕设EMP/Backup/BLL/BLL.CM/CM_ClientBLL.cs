
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

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_ClientBLL业务逻辑BLL类
    /// </summary>
    public class CM_ClientBLL : BaseSimpleBLL<CM_Client>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientDAL";
        private CM_ClientDAL _dal;

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

        /// <summary>
        /// 批量替换客户的供货商
        /// </summary>
        /// <param name="OrgSupplier"></param>
        /// <param name="NewSupplier"></param>
        /// <returns></returns>
        public static int ReplaceSupplier(int OrgSupplier, int NewSupplier, int NewSupplier2)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReplaceSupplier(OrgSupplier, NewSupplier, NewSupplier2);
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

        public static IList<CM_DIAddressID> GetAddressByClient(int Client)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAddressByClient(Client);
        }

        public string CheckRealClassifyShowMessage(int ChangeType)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckRealClassifyShowMessage(ChangeType, this.Model.ID);
        }
        #region 统计各类活跃状态门店数量
        public static DataTable GetClientCountByActiveFlag(int OrganizeCity, int Supplier, int ClientType)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientCountByActiveFlag(OrganizeCity, Supplier, ClientType);
        }
        #endregion

        /// <summary>
        /// 获取金蝶数据
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="clientCode"></param>
        /// <returns></returns>
        public DataTable GetStatement(string procName, int year, int month, string clientCode)
        {
            CM_ClientDAL dal = (CM_ClientDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt = null;
            using (SqlDataReader dr = dal.GetStatement(procName, year, month, clientCode))
            {
                dt = Tools.ConvertDataReaderToDataTable(dr);
            }
            return dt;
        }

    }
}
