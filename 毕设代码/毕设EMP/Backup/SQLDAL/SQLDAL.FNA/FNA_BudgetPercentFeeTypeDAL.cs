
// ===================================================================
// 文件： FNA_BudgetPercentFeeTypeDAL.cs
// 项目名称：
// 创建时间：2010/5/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.FNA
{
	/// <summary>
	///FNA_BudgetPercentFeeType数据访问DAL类
	/// </summary>
	public class FNA_BudgetPercentFeeTypeDAL : BaseSimpleDAL<FNA_BudgetPercentFeeType>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_BudgetPercentFeeTypeDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetPercentFeeType";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetPercentFeeType m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@BudgetPercent", SqlDbType.Decimal, 9, m.BudgetPercent),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
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
        public override int Update(FNA_BudgetPercentFeeType m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@BudgetPercent", SqlDbType.Decimal, 9, m.BudgetPercent),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_BudgetPercentFeeType FillModel(IDataReader dr)
		{
			FNA_BudgetPercentFeeType m = new FNA_BudgetPercentFeeType();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["FeeType"].ToString()))	m.FeeType = (int)dr["FeeType"];
			if (!string.IsNullOrEmpty(dr["BudgetPercent"].ToString()))	m.BudgetPercent = (decimal)dr["BudgetPercent"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 获取指定管理片区的预算占比 DataTable
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public DataTable GetList(int OrganizeCity, int Level)
        {
            SqlDataReader dr = null;

            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4,Level),
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetList", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

