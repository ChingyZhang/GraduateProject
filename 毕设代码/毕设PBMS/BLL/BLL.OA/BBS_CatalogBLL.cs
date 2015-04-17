
// ===================================================================
// 文件： BBS_CatalogDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
	/// <summary>
	///BBS_CatalogBLL业务逻辑BLL类
	/// </summary>
	public class BBS_CatalogBLL : BaseSimpleBLL<BBS_Catalog>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_CatalogDAL";
        private BBS_CatalogDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_CatalogBLL
		///</summary>
		public BBS_CatalogBLL()
			: base(DALClassName)
		{
			_dal = (BBS_CatalogDAL)_DAL;
            _m = new BBS_Catalog(); 
		}
		
		public BBS_CatalogBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_CatalogDAL)_DAL;
            FillModel(id);
        }

        public BBS_CatalogBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_CatalogDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_Catalog> GetModelList(string condition)
        {
            return new BBS_CatalogBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetAllCatalog(string condition)
        {
            return _dal.GetAllCatalog(condition);
        }

        public int DeleteCatalog(int id)
        {
            return _dal.DeleteCatalog(id);
        }
       
	}
}