
// ===================================================================
// 文件： UD_PanelDAL.cs
// 项目名称：
// 创建时间：2008-11-27
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Web.Caching;

namespace MCSFramework.BLL
{
    /// <summary>
    ///UD_PanelBLL业务逻辑BLL类
    /// </summary>
    public class UD_PanelBLL : BaseSimpleBLL<UD_Panel>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.UD_PanelDAL";
        private UD_PanelDAL _dal;

        #region 构造函数
        ///<summary>
        ///UD_PanelBLL
        ///</summary>
        public UD_PanelBLL()
            : base(DALClassName)
        {
            _dal = (UD_PanelDAL)_DAL;
            _m = new UD_Panel();
        }

        public UD_PanelBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_PanelDAL)_DAL;
            FillModel(id);
        }

        public UD_PanelBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_PanelDAL)_DAL;
            FillModel(id, bycache);
        }

        public UD_PanelBLL(string code)
            : base(DALClassName)
        {
            _dal = (UD_PanelDAL)_DAL;
            FillModel(code);
        }
        public UD_PanelBLL(string code, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_PanelDAL)_DAL;
            FillModel(code, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<UD_Panel> GetModelList(string condition)
        {
            return new UD_PanelBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 根据当前Panel里所有的Fields列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_Panel_ModelFields> GetModelFields()
        {
            string CacheKey = "UD_Panel-ModelFields-" + _m.ID.ToString();
            IList<UD_Panel_ModelFields> list = (IList<UD_Panel_ModelFields>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_Panel_ModelFieldsBLL.GetModelList("PanelID='" + _m.ID.ToString() + "'");
                
                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_Panel_ModelFields"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }

        /// <summary>
        /// 获取当前Panel包含的Table列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_Panel_Table> GetPanelTables()
        {
            string CacheKey = "UD_Panel-PanelTables-" + _m.ID.ToString();
            IList<UD_Panel_Table> list = (IList<UD_Panel_Table>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_Panel_TableBLL.GetModelList("PanelID='" + _m.ID.ToString() + "'");

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_Panel_Table"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }

        /// <summary>
        /// 获取Panel的表关系列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_Panel_TableRelation> GetTableRelations()
        {
            string CacheKey = "UD_Panel-TableRelations-" + _m.ID.ToString();
            IList<UD_Panel_TableRelation> list = (IList<UD_Panel_TableRelation>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_Panel_TableRelationBLL.GetModelList("PanelID='" + _m.ID.ToString() + "'");

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_Panel_TableRelation"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }

        /// <summary>
        /// 获取已分配的最大排序号
        /// </summary>
        public int GetFieldMaxSort()
        {
            return _dal.GetFieldMaxSort(_m.ID);
        }
    }
}