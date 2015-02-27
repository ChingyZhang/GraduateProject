
// ===================================================================
// 文件： FNA_BudgetTransferDAL.cs
// 项目名称：
// 创建时间：2010/5/15
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;


namespace MCSFramework.SQLDAL.FNA
{
	/// <summary>
	///FNA_BudgetTransfer数据访问DAL类
	/// </summary>
	public class FNA_BudgetTransferDAL : BaseSimpleDAL<FNA_BudgetTransfer>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_BudgetTransferDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetTransfer";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetTransfer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FromOrganizeCity", SqlDbType.Int, 4, m.FromOrganizeCity),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, m.ToOrganizeCity),
				SQLDatabase.MakeInParam("@FromAccountMonth", SqlDbType.Int, 4, m.FromAccountMonth),
				SQLDatabase.MakeInParam("@ToAccountMonth", SqlDbType.Int, 4, m.ToAccountMonth),
				SQLDatabase.MakeInParam("@FromFeeType", SqlDbType.Int, 4, m.FromFeeType),
				SQLDatabase.MakeInParam("@ToFeeType", SqlDbType.Int, 4, m.ToFeeType),
				SQLDatabase.MakeInParam("@BudgetType", SqlDbType.Int, 4, m.BudgetType),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(FNA_BudgetTransfer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@FromOrganizeCity", SqlDbType.Int, 4, m.FromOrganizeCity),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, m.ToOrganizeCity),
				SQLDatabase.MakeInParam("@FromAccountMonth", SqlDbType.Int, 4, m.FromAccountMonth),
				SQLDatabase.MakeInParam("@ToAccountMonth", SqlDbType.Int, 4, m.ToAccountMonth),
				SQLDatabase.MakeInParam("@FromFeeType", SqlDbType.Int, 4, m.FromFeeType),
				SQLDatabase.MakeInParam("@ToFeeType", SqlDbType.Int, 4, m.ToFeeType),
				SQLDatabase.MakeInParam("@BudgetType", SqlDbType.Int, 4, m.BudgetType),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_BudgetTransfer FillModel(IDataReader dr)
		{
			FNA_BudgetTransfer m = new FNA_BudgetTransfer();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["FromOrganizeCity"].ToString()))	m.FromOrganizeCity = (int)dr["FromOrganizeCity"];
			if (!string.IsNullOrEmpty(dr["ToOrganizeCity"].ToString()))	m.ToOrganizeCity = (int)dr["ToOrganizeCity"];
			if (!string.IsNullOrEmpty(dr["FromAccountMonth"].ToString()))	m.FromAccountMonth = (int)dr["FromAccountMonth"];
			if (!string.IsNullOrEmpty(dr["ToAccountMonth"].ToString()))	m.ToAccountMonth = (int)dr["ToAccountMonth"];
			if (!string.IsNullOrEmpty(dr["FromFeeType"].ToString()))	m.FromFeeType = (int)dr["FromFeeType"];
			if (!string.IsNullOrEmpty(dr["ToFeeType"].ToString()))	m.ToFeeType = (int)dr["ToFeeType"];
			if (!string.IsNullOrEmpty(dr["BudgetType"].ToString()))	m.BudgetType = (int)dr["BudgetType"];
			if (!string.IsNullOrEmpty(dr["Amount"].ToString()))	m.Amount = (decimal)dr["Amount"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

