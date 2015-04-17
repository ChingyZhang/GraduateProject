using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Data;

namespace MCSFramework.BLL
{
    public class Right_Module_BLL:BaseSimpleBLL<Right_Module>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Right_Module_DAL";
        private Right_Module_DAL _dal;

        #region 构建BLL
        public Right_Module_BLL()
            : base(DALClassName)
        {
            _dal = (Right_Module_DAL)_DAL;
            _m = new Right_Module();    //实例化Model
        }

        public Right_Module_BLL(int id)
            : base(DALClassName)
        {
            _dal = (Right_Module_DAL)_DAL;
            FillModel(id);
        }

        public Right_Module_BLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Right_Module_DAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        public static IList<Right_Module> GetModelList(string condition)
        {
            return (new Right_Module_BLL())._GetModelList(condition);
        }

        /// <summary>
        /// 获取指定用户的模块浏览权限列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static DataTable GetBrowseMoudleByUser(string username)
        {
            string CacheKey = "Right-BrowseModuleList-" + username;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                #region 从数据库中获取指定用户的模块浏览权限列表
                Right_Module_DAL dal = (Right_Module_DAL)DataAccess.CreateObject("MCSFramework.SQLDAL.Right_Module_DAL");
                dt = dal.GetBroweModuleByUser(ConfigHelper.GetConfigString("ApplicationName"), username);
                #endregion

                #region 写入缓存
                //创建缓存SQL依赖
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Right_Assign"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Right_Module"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "aspnet_UsersInRoles"));
                //cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Org_Staff"));

                DataCache.SetCache(CacheKey, dt, cachedependency);
                #endregion
            }
            
            return dt;
        }

    }

}
