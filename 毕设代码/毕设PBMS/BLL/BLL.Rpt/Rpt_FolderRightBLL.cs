
// ===================================================================
// 文件： Rpt_FolderRightDAL.cs
// 项目名称：
// 创建时间：2010/10/12
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;
using System.Web.Caching;

namespace MCSFramework.BLL.RPT
{
    /// <summary>
    ///Rpt_FolderRightBLL业务逻辑BLL类
    /// </summary>
    public class Rpt_FolderRightBLL : BaseSimpleBLL<Rpt_FolderRight>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_FolderRightDAL";
        private Rpt_FolderRightDAL _dal;

        #region 构造函数
        ///<summary>
        ///Rpt_FolderRightBLL
        ///</summary>
        public Rpt_FolderRightBLL()
            : base(DALClassName)
        {
            _dal = (Rpt_FolderRightDAL)_DAL;
            _m = new Rpt_FolderRight();
        }

        public Rpt_FolderRightBLL(int id)
            : base(DALClassName)
        {
            _dal = (Rpt_FolderRightDAL)_DAL;
            FillModel(id);
        }

        public Rpt_FolderRightBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_FolderRightDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Rpt_FolderRight> GetModelList(string condition)
        {
            return new Rpt_FolderRightBLL()._GetModelList(condition);
        }
        #endregion

        public static IList<Rpt_FolderRight> GetAssignedRightByUser(string UserName)
        {
            string CacheKey = "Rpt_FolderRight-AssignedRightList-" + UserName;
            IList<Rpt_FolderRight> list = (IList<Rpt_FolderRight>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                Rpt_FolderRightDAL dal = (Rpt_FolderRightDAL)DataAccess.CreateObject(DALClassName);
                string Application = ConfigHelper.GetConfigString("ApplicationName");
                list = dal.GetAssignedRightByUser(Application, UserName);

                #region 写入缓存
                //创建缓存SQL依赖
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_Reports", "Rpt_Folder"));
                cachedependency.Add(new SqlCacheDependency("MCS_Reports", "Rpt_FolderRight"));
                DataCache.SetCache(CacheKey, list, cachedependency);
                #endregion
            }
            return list;
        }
    }
}