
// ===================================================================
// 文件： FNA_BudgetBalanceDAL.cs
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
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_BudgetBalance数据访问DAL类
    /// </summary>
    public class FNA_BudgetBalanceDAL : BaseSimpleDAL<FNA_BudgetBalance>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetBalanceDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetBalance";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetBalance m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@CostBalance", SqlDbType.Decimal, 9, m.CostBalance),
				SQLDatabase.MakeInParam("@DDFInitialBalance", SqlDbType.Decimal, 9, m.DDFInitialBalance)
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
        public override int Update(FNA_BudgetBalance m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@CostBalance", SqlDbType.Decimal, 9, m.CostBalance),
				SQLDatabase.MakeInParam("@DDFInitialBalance", SqlDbType.Decimal, 9, m.DDFInitialBalance)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_BudgetBalance FillModel(IDataReader dr)
        {
            FNA_BudgetBalance m = new FNA_BudgetBalance();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["CostBalance"].ToString())) m.CostBalance = (decimal)dr["CostBalance"];
            if (!string.IsNullOrEmpty(dr["DDFInitialBalance"].ToString())) m.DDFInitialBalance = (decimal)dr["DDFInitialBalance"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];

            return m;
        }

        public DataTable GetBalance(int OrganizeCity, int AccountMonth, int Level)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetBalance", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);

        }
    }
}

