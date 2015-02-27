
// ===================================================================
// 文件： FNA_BudgetSourceDAL.cs
// 项目名称：
// 创建时间：2010-7-22
// 作者:	   madongfang
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
	///FNA_BudgetSource数据访问DAL类
	/// </summary>
	public class FNA_BudgetSourceDAL : BaseSimpleDAL<FNA_BudgetSource>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_BudgetSourceDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetSource";
		}
		#endregion
		
		
		/// <summary>
        //// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetSource m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@BaseVolume", SqlDbType.Decimal, 9, m.BaseVolume),
				SQLDatabase.MakeInParam("@PlanVolume", SqlDbType.Decimal, 9, m.PlanVolume),
				SQLDatabase.MakeInParam("@BaseBudget", SqlDbType.Decimal, 9, m.BaseBudget),
				SQLDatabase.MakeInParam("@OverFullBudget", SqlDbType.Decimal, 9, m.OverFullBudget),
				SQLDatabase.MakeInParam("@RetentionBudget", SqlDbType.Decimal, 9, m.RetentionBudget),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(FNA_BudgetSource m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@BaseVolume", SqlDbType.Decimal, 9, m.BaseVolume),
				SQLDatabase.MakeInParam("@PlanVolume", SqlDbType.Decimal, 9, m.PlanVolume),
				SQLDatabase.MakeInParam("@BaseBudget", SqlDbType.Decimal, 9, m.BaseBudget),
				SQLDatabase.MakeInParam("@OverFullBudget", SqlDbType.Decimal, 9, m.OverFullBudget),
				SQLDatabase.MakeInParam("@RetentionBudget", SqlDbType.Decimal, 9, m.RetentionBudget),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_BudgetSource FillModel(IDataReader dr)
        {
            FNA_BudgetSource m = new FNA_BudgetSource();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["BaseVolume"].ToString())) m.BaseVolume = (decimal)dr["BaseVolume"];
            if (!string.IsNullOrEmpty(dr["PlanVolume"].ToString())) m.PlanVolume = (decimal)dr["PlanVolume"];
            if (!string.IsNullOrEmpty(dr["BaseBudget"].ToString())) m.BaseBudget = (decimal)dr["BaseBudget"];
            if (!string.IsNullOrEmpty(dr["OverFullBudget"].ToString())) m.OverFullBudget = (decimal)dr["OverFullBudget"];
            if (!string.IsNullOrEmpty(dr["RetentionBudget"].ToString())) m.RetentionBudget = (decimal)dr["RetentionBudget"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public DataTable GetSummary(int OrganizeCity, int AccountMonth, int Level)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

