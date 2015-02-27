
// ===================================================================
// 文件： FNA_BudgetBalanceDAL.cs
// 项目名称：
// 创建时间：2009/2/22
// 作者:	   Shen Gang
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
	///FNA_BudgetBalanceBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetBalanceBLL : BaseSimpleBLL<FNA_BudgetBalance>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetBalanceDAL";
        private FNA_BudgetBalanceDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetBalanceBLL
		///</summary>
		public FNA_BudgetBalanceBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetBalanceDAL)_DAL;
            _m = new FNA_BudgetBalance(); 
		}
		
		public FNA_BudgetBalanceBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetBalanceDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetBalanceBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetBalanceDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetBalance> GetModelList(string condition)
        {
            return new FNA_BudgetBalanceBLL()._GetModelList(condition);
        }
		#endregion

        public static DataTable GetBalance(int OrganizeCity, int AccountMonth, int Level)
        {
            FNA_BudgetBalanceDAL dal = (FNA_BudgetBalanceDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetBalance(OrganizeCity, AccountMonth, Level);
        }
	}
}