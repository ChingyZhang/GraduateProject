
// ===================================================================
// 文件： PDT_ProductCostDAL.cs
// 项目名称：
// 创建时间：2009-3-3
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///PDT_ProductCostBLL业务逻辑BLL类
	/// </summary>
	public class PDT_ProductCostBLL : BaseSimpleBLL<PDT_ProductCost>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductCostDAL";
        private PDT_ProductCostDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_ProductCostBLL
		///</summary>
		public PDT_ProductCostBLL()
			: base(DALClassName)
		{
			_dal = (PDT_ProductCostDAL)_DAL;
            _m = new PDT_ProductCost(); 
		}
		
		public PDT_ProductCostBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductCostDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductCostBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductCostDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PDT_ProductCost> GetModelList(string condition)
        {
            return new PDT_ProductCostBLL()._GetModelList(condition);
        }
		#endregion

        public static DataTable GetProductPriceList(int OrganizeCity)
        {
            PDT_ProductCostDAL dal = (PDT_ProductCostDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetProductPriceList(OrganizeCity));
        }

        public static IList<PDT_ProductCost> GetListByOrganizeCity(int OrganizeCity)
        {
            PDT_ProductCostDAL dal = (PDT_ProductCostDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByOrganizeCity(OrganizeCity);
        }
	}
}