
// ===================================================================
// 文件： UD_TableListDAL.cs
// 项目名称：
// 创建时间：2008-11-25
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Collections.Generic;
using System.Web.Caching;

namespace MCSFramework.BLL
{
    /// <summary>
    ///UD_TableListBLL业务逻辑BLL类
    /// </summary>
    public class UD_TableListBLL : BaseSimpleBLL<UD_TableList>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.UD_TableListDAL";
        private UD_TableListDAL _dal;

        #region 构造函数
        ///<summary>
        ///UD_TableListBLL
        ///</summary>
        public UD_TableListBLL()
            : base(DALClassName)
        {
            _dal = (UD_TableListDAL)_DAL;
            _m = new UD_TableList();
        }

        public UD_TableListBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_TableListDAL)_DAL;
            FillModel(id);
        }

        public UD_TableListBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_TableListDAL)_DAL;
            FillModel(id, bycache);
        }

        public UD_TableListBLL(string name)
            : base(DALClassName)
        {
            _dal = (UD_TableListDAL)_DAL;
            _m = _dal.GetModel(name);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<UD_TableList> GetModelList(string condition)
        {
            return new UD_TableListBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取当前表所有包含的字段列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_ModelFields> GetModelFields()
        {
            string CacheKey = "UD_TableList-ModelFields-" + _m.ID.ToString();
            IList<UD_ModelFields> list = (IList<UD_ModelFields>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_ModelFieldsBLL.GetModelList("TableID='" + _m.ID.ToString() + "'");

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_ModelFields"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }

        
    }
}