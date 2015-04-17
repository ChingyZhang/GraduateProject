
// ===================================================================
// 文件： UD_DetailViewDAL.cs
// 项目名称：
// 创建时间：2009/3/5
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Web.Caching;

namespace MCSFramework.BLL
{
    /// <summary>
    ///UD_DetailViewBLL业务逻辑BLL类
    /// </summary>
    public class UD_DetailViewBLL : BaseSimpleBLL<UD_DetailView>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.UD_DetailViewDAL";
        private UD_DetailViewDAL _dal;

        #region 构造函数
        ///<summary>
        ///UD_DetailViewBLL
        ///</summary>
        public UD_DetailViewBLL()
            : base(DALClassName)
        {
            _dal = (UD_DetailViewDAL)_DAL;
            _m = new UD_DetailView();
        }

        public UD_DetailViewBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_DetailViewDAL)_DAL;
            FillModel(id);
        }

        public UD_DetailViewBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_DetailViewDAL)_DAL;
            FillModel(id, bycache);
        }

        public UD_DetailViewBLL(string code)
            : base(DALClassName)
        {
            _dal = (UD_DetailViewDAL)_DAL;
            FillModel(code);
        }

        public UD_DetailViewBLL(string code, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_DetailViewDAL)_DAL;
            FillModel(code, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<UD_DetailView> GetModelList(string condition)
        {
            return new UD_DetailViewBLL()._GetModelList(condition);
        }
        #endregion

        public IList<UD_Panel> GetPanels()
        {
            return UD_PanelBLL.GetModelList("DetailViewID='" + _m.ID.ToString() + "'"); ;
        }

        /// <summary>
        /// 获取当前页面里所有的Detail模式的Panel列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_Panel> GetDetailPanels()
        {
            string CacheKey = "UD_DetailView-DetailPanels-" + _m.ID.ToString();
            IList<UD_Panel> list = (IList<UD_Panel>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_PanelBLL.GetModelList("DetailViewID='" + _m.ID.ToString() + "' and DisplayType=1");

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_Panel"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }
    }
}