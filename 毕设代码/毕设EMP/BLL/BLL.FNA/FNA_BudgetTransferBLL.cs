
// ===================================================================
// 文件： FNA_BudgetTransferDAL.cs
// 项目名称：
// 创建时间：2009/3/22
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
	///FNA_BudgetTransferBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetTransferBLL : BaseSimpleBLL<FNA_BudgetTransfer>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetTransferDAL";
        private FNA_BudgetTransferDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetTransferBLL
		///</summary>
		public FNA_BudgetTransferBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetTransferDAL)_DAL;
            _m = new FNA_BudgetTransfer(); 
		}
		
		public FNA_BudgetTransferBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetTransferDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetTransferBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetTransferDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetTransfer> GetModelList(string condition)
        {
            return new FNA_BudgetTransferBLL()._GetModelList(condition);
        }
		#endregion
	}
}