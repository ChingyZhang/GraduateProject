
// ===================================================================
// 文件： UD_WebPageDAL.cs
// 项目名称：
// 创建时间：2009/3/7
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
	///UD_WebPageBLL业务逻辑BLL类
	/// </summary>
	public class UD_WebPageBLL : BaseSimpleBLL<UD_WebPage>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.UD_WebPageDAL";
        private UD_WebPageDAL _dal;
		
		#region 构造函数
		///<summary>
		///UD_WebPageBLL
		///</summary>
		public UD_WebPageBLL()
			: base(DALClassName)
		{
			_dal = (UD_WebPageDAL)_DAL;
            _m = new UD_WebPage(); 
		}
		
		public UD_WebPageBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_WebPageDAL)_DAL;
            FillModel(id);
        }

        public UD_WebPageBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_WebPageDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<UD_WebPage> GetModelList(string condition)
        {
            return new UD_WebPageBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取当前页面所有包含的控件列表
        /// </summary>
        /// <returns></returns>
        public IList<UD_WebPageControl> GetWebControls()
        {
            string CacheKey = "UD_WebPage-WebControls-" + _m.ID.ToString();
            IList<UD_WebPageControl> list = (IList<UD_WebPageControl>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                list = UD_WebPageControlBLL.GetModelList("WebPageID='" + _m.ID.ToString() + "'");

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_WebPageControl"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }
	}
}