
// ===================================================================
// 文件： FNA_BudgetChangeHistoryDAL.cs
// 项目名称：
// 创建时间：2009/2/22
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
    ///FNA_BudgetChangeHistory数据访问DAL类
    /// </summary>
    public class FNA_BudgetChangeHistoryDAL : BaseSimpleDAL<FNA_BudgetChangeHistory>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetChangeHistoryDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetChangeHistory";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, m.ChangeAmount),
				SQLDatabase.MakeInParam("@Balance", SqlDbType.Decimal, 9, m.Balance),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@RelatedInfo", SqlDbType.VarChar, 1000, m.RelatedInfo)
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
        public override int Update(FNA_BudgetChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, m.ChangeAmount),
				SQLDatabase.MakeInParam("@Balance", SqlDbType.Decimal, 9, m.Balance),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@RelatedInfo", SqlDbType.VarChar, 1000, m.RelatedInfo)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_BudgetChangeHistory FillModel(IDataReader dr)
        {
            FNA_BudgetChangeHistory m = new FNA_BudgetChangeHistory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["ChangeAmount"].ToString())) m.ChangeAmount = (decimal)dr["ChangeAmount"];
            if (!string.IsNullOrEmpty(dr["Balance"].ToString())) m.Balance = (decimal)dr["Balance"];
            if (!string.IsNullOrEmpty(dr["ChangeType"].ToString())) m.ChangeType = (int)dr["ChangeType"];
            if (!string.IsNullOrEmpty(dr["ChangeStaff"].ToString())) m.ChangeStaff = (int)dr["ChangeStaff"];
            if (!string.IsNullOrEmpty(dr["ChangeTime"].ToString())) m.ChangeTime = (DateTime)dr["ChangeTime"];
            if (!string.IsNullOrEmpty(dr["RelatedInfo"].ToString())) m.RelatedInfo = (string)dr["RelatedInfo"];

            return m;
        }
    }
}

