
// ===================================================================
// 文件： FNA_BudgetChangeHistoryDAL.cs
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
	///FNA_BudgetChangeHistoryBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetChangeHistoryBLL : BaseSimpleBLL<FNA_BudgetChangeHistory>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetChangeHistoryDAL";
        private FNA_BudgetChangeHistoryDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetChangeHistoryBLL
		///</summary>
		public FNA_BudgetChangeHistoryBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetChangeHistoryDAL)_DAL;
            _m = new FNA_BudgetChangeHistory(); 
		}
		
		public FNA_BudgetChangeHistoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetChangeHistoryDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetChangeHistoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetChangeHistoryDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetChangeHistory> GetModelList(string condition)
        {
            return new FNA_BudgetChangeHistoryBLL()._GetModelList(condition);
        }
		#endregion
	}
}