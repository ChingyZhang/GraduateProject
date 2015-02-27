
// ===================================================================
// 文件： FNA_BudgetSourceDAL.cs
// 项目名称：
// 创建时间：2010-7-22
// 作者:	   madongfang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.FNA;
using MCSFramework.SQLDAL.FNA;

namespace MCSFramework.BLL.FNA
{
	/// <summary>
	///FNA_BudgetSourceBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetSourceBLL : BaseSimpleBLL<FNA_BudgetSource>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetSourceDAL";
        private FNA_BudgetSourceDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetSourceBLL
		///</summary>
		public FNA_BudgetSourceBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetSourceDAL)_DAL;
            _m = new FNA_BudgetSource(); 
		}
		
		public FNA_BudgetSourceBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetSourceDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetSourceBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetSourceDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetSource> GetModelList(string condition)
        {
            return new FNA_BudgetSourceBLL()._GetModelList(condition);
        }
		#endregion

        public static DataTable GetSummary(int OrganizeCity, int AccountMonth, int Level)
        {
            FNA_BudgetSourceDAL dal = (FNA_BudgetSourceDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummary(OrganizeCity, AccountMonth,Level);
        }
	}
}