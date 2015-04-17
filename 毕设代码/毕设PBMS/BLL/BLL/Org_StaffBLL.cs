using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.IFStrategy;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using MCSFramework.Common;
using System.Web.Caching;
using System.Data;

namespace MCSFramework.BLL
{
    public class Org_StaffBLL : BaseSimpleBLL<Org_Staff>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Org_Staff_DAL";
        private Org_Staff_DAL _dal;

        #region 构建BLL
        public Org_StaffBLL()
            : base(DALClassName)
        {
            _dal = (Org_Staff_DAL)_DAL;
            _m = new Org_Staff();    //实例化Model
        }

        public Org_StaffBLL(int id)
            : base(DALClassName)
        {
            _dal = (Org_Staff_DAL)_DAL;
            FillModel(id);
        }

        public Org_StaffBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Org_Staff_DAL)_DAL;
            FillModel(id, bycache);
        }

        #endregion

        #region 缓存移除事件回调
        private static void RemovedCacheCallback(String k, Object v, CacheItemRemovedReason r)
        {

        }
        #endregion

        public static IList<Org_Staff> GetStaffList(string condition)
        {
            Org_StaffBLL bll = new Org_StaffBLL();
            return bll._GetModelList(condition);
        }
        #region 员工离职复职
        /// <summary>
        /// 员工复职
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public int DoRehab()
        {
            return _dal.DoRehab(_m.ID);
        }

        /// <summary>
        /// 员工离职
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public int DoDimission()
        {
            return _dal.DoDimission(_m.ID);
        }
        #endregion

        #region 获取员工所管辖的办事处
        /// <summary>
        /// 获取员工所管辖的办事处的ID字符串
        /// </summary>
        /// <returns></returns>
        public string GetStaffOrganizeCityIDs()
        {
            return _dal.GetStaffOrganizeCityIDs(_m.ID);
        }

        /// <summary>
        /// 获取员工所管辖的办事处
        /// </summary>
        /// <returns></returns>
        public DataTable GetStaffOrganizeCity()
        {
            string CacheKey = "Cache-OrgStaffBLL-GetStaffOrganizeCity-" + _m.ID;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = _dal.GetStaffOrganizeCity(_m.ID);

                #region 写入缓存
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Org_Staff"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Addr_OrganizeCity"));

                DataCache.SetCache(CacheKey, dt, cachedependency);
                #endregion
            }
            return dt;
        }
        #endregion

        /// <summary>
        /// 根据员工ID根据对应的用户帐户
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserList()
        {
            return _dal.GetUserListByStaffID(_m.ID);
        }

        /// <summary>
        /// 根据员工职位获取属于该职位的员工及登录用户名列表
        /// </summary>
        /// <param name="Positions"></param>
        /// <returns></returns>
        public static DataTable GetRealNameAndUserNameByPosition(string Positions)
        {
            Org_Staff_DAL dal = (Org_Staff_DAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRealNameAndUserNameByPosition(Positions);
        }

        public static bool IsMyOrganizeCity(int OrganizeCity, int Staff)
        {
            Org_Staff_DAL dal = (Org_Staff_DAL)DataAccess.CreateObject(DALClassName);
            return dal.IsMyOrganizeCity(OrganizeCity, Staff);
        }

        /// <summary>
        /// 员工审核
        /// </summary>
        /// <returns></returns>
        public int Approve()
        {
            return _dal.Approve(_m.ID, 3);
        }
        /// <summary>
        /// 根据员工ID获取今日任务
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static DataTable GetTodayTask(string username, bool NoCache)
        {
            string CacheKey = "Cache-OrgStaffBLL-TodayTask-" + username;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null || NoCache)
            {
                Org_Staff_DAL dal = (Org_Staff_DAL)DataAccess.CreateObject(DALClassName);
                dt = dal.GetTodayTask(username);

                #region 写入缓存
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 统计数据填报进度情况
        /// </summary>
        /// <param name="Staff"></param>
        /// <param name="Month"></param>
        /// <param name="byCache"></param>
        /// <returns></returns>
        public static DataTable GetFillDataProgress(int Staff, int Month, bool NoCache)
        {
            string CacheKey = "Cache-OrgStaffBLL-FillDataProgress-" + Staff.ToString() + "-" + Month.ToString();
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null || NoCache)
            {
                Org_Staff_DAL dal = (Org_Staff_DAL)DataAccess.CreateObject(DALClassName);
                dt = dal.GetFillDataProgress(Staff, Month);

                #region 写入缓存
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 统计数据填报进度情况
        /// </summary>
        /// <param name="Staff"></param>
        /// <param name="NoCache"></param>
        /// <returns></returns>
        public static DataTable GetFillDataProgress(int Staff, bool NoCache)
        {
            return GetFillDataProgress(Staff, 0, NoCache);
        }

        #region 员工可兼管的管理片区
        public int StaffInOrganizeCity_Add(int organizecity)
        {
            return _dal.StaffInOrganizeCity_Add(_m.ID, organizecity);
        }

        public int StaffInOrganizeCity_Delete(int organizecity)
        {
            return _dal.StaffInOrganizeCity_Delete(_m.ID, organizecity);
        }

        public IList<Addr_OrganizeCity> StaffInOrganizeCity_GetOrganizeCitys()
        {
            return _dal.StaffInOrganizeCity_GetOrganizeCitys(_m.ID);
        }
        #endregion

        public static int GetStaffIDByUserName(string userName)
        {
            return Org_Staff_DAL.GetStaffIDByUserName(userName);
        }
    }
}
