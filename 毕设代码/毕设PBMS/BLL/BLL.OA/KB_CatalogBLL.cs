
// ===================================================================
// 文件： KB_CatalogDAL.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   WJX
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
	///KB_CatalogBLL业务逻辑BLL类
	/// </summary>
	public class KB_CatalogBLL : BaseSimpleBLL<KB_Catalog>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.KB_CatalogDAL";
        private KB_CatalogDAL _dal;
		
		#region 构造函数
		///<summary>
		///KB_CatalogBLL
		///</summary>
		public KB_CatalogBLL()
			: base(DALClassName)
		{
			_dal = (KB_CatalogDAL)_DAL;
            _m = new KB_Catalog(); 
		}
		
		public KB_CatalogBLL(int id)
            : base(DALClassName)
        {
            _dal = (KB_CatalogDAL)_DAL;
            FillModel(id);
        }

        public KB_CatalogBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (KB_CatalogDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<KB_Catalog> GetModelList(string condition)
        {
            return new KB_CatalogBLL()._GetModelList(condition);
        }
		#endregion

        public string GetAllChildPosition()
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_OA.dbo.KB_Catalog", "ID", "SuperID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["ID"];
            }

            return ids;

        }

        public static DataTable GetAllPostion()
        {
            ///可以引入缓存
            KB_CatalogDAL dal = (KB_CatalogDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAllPosition();
        }
	}
}