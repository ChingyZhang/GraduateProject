
// ===================================================================
// 文件： FNA_BudgetPercentFeeTypeDAL.cs
// 项目名称：
// 创建时间：2010/5/15
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
	///FNA_BudgetPercentFeeTypeBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetPercentFeeTypeBLL : BaseSimpleBLL<FNA_BudgetPercentFeeType>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetPercentFeeTypeDAL";
        private FNA_BudgetPercentFeeTypeDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetPercentFeeTypeBLL
		///</summary>
		public FNA_BudgetPercentFeeTypeBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetPercentFeeTypeDAL)_DAL;
            _m = new FNA_BudgetPercentFeeType(); 
		}
		
		public FNA_BudgetPercentFeeTypeBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetPercentFeeTypeDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetPercentFeeTypeBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetPercentFeeTypeDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetPercentFeeType> GetModelList(string condition)
        {
            return new FNA_BudgetPercentFeeTypeBLL()._GetModelList(condition);
        }
		#endregion

        public static DataTable GetList(int OrganizeCity, int Level)
        {
            FNA_BudgetPercentFeeTypeDAL dal = (FNA_BudgetPercentFeeTypeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetList(OrganizeCity, Level);
        }
	}
}