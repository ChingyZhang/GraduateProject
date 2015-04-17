using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using MCSFramework.Common;
using System.Web.Caching;
using System.Data;
using System.Data.Common;

namespace MCSFramework.BLL
{
    public class Right_Assign_BLL:BaseSimpleBLL<Right_Assign>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Right_Assign_DAL";
        private Right_Assign_DAL _dal;

        #region 构建BLL
        public Right_Assign_BLL()
            : base(DALClassName)
        {
            _dal = (Right_Assign_DAL)_DAL;
            _m = new Right_Assign();    //实例化Model
        }

        public Right_Assign_BLL(int id)
            : base(DALClassName)
        {
            _dal = (Right_Assign_DAL)_DAL;
            FillModel(id);
        }

        public Right_Assign_BLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Right_Assign_DAL)_DAL;
            FillModel(id, bycache);
        }

        #endregion

        #region	静态GetModelList方法
        public static IList<Right_Assign> GetModelList(string condition)
        {
            return new Right_Assign_BLL()._GetModelList(condition);
        }
        #endregion
        /// <summary>
        /// 判断指定用户是否具有模块Module的动作权限编号ActionCode的操作权限
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="module">模块标识</param>
        /// <param name="actioncode">动作权限编号</param>
        /// <returns>True:有权限 False:无权限</returns>
        public static bool GetAccessRight(string Username,int Module,string ActionCode)
        {
            string CacheKey = "Right-AssignedRightList-" + Username;
            StringCollection assignedRight = (StringCollection)DataCache.GetCache(CacheKey);
             
            if (assignedRight == null)
            {
                assignedRight = new StringCollection();
                #region 从数据库中获取分配给指定用户的权限
                Right_Assign_DAL dal = (Right_Assign_DAL)DataAccess.CreateObject(DALClassName);
                DbDataReader dr = dal.GetAssignedRightList(ConfigHelper.GetConfigString("ApplicationName"), Username);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        assignedRight.Add(dr["Module"].ToString() + "-" + dr["ActionCode"].ToString());
                    }
                }
                dr.Close();
                #endregion

                #region 写入缓存
                //创建缓存SQL依赖
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Right_Action"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Right_Assign"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "Right_Module"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "aspnet_UsersInRoles"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_WebPage"));
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_WebPageControl"));

                DataCache.SetCache(CacheKey, assignedRight, cachedependency);
                #endregion
            }

            return assignedRight.Contains(Module.ToString() + "-" + ActionCode);
        }

        #region 缓存移除事件回调
        private static void RemovedCacheCallback(String k, Object v, CacheItemRemovedReason r)
        {

        }
        #endregion
    }
}
