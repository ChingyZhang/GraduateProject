
// ===================================================================
// 文件： FNA_BudgetDAL.cs
// 项目名称：
// 创建时间：2009/2/21
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
    ///FNA_Budget数据访问DAL类
    /// </summary>
    public class FNA_BudgetDAL : BaseSimpleDAL<FNA_Budget>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_Budget";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_Budget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@BudgetType", SqlDbType.Int, 4, m.BudgetType),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@BudgetAmount", SqlDbType.Decimal, 9, m.BudgetAmount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtProperty", SqlDbType.VarChar, 4000, m.ExtProperty)
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
        public override int Update(FNA_Budget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@BudgetType", SqlDbType.Int, 4, m.BudgetType),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@BudgetAmount", SqlDbType.Decimal, 9, m.BudgetAmount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtProperty", SqlDbType.VarChar, 4000, m.ExtProperty)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_Budget FillModel(IDataReader dr)
        {
            FNA_Budget m = new FNA_Budget();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["BudgetType"].ToString())) m.BudgetType = (int)dr["BudgetType"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["BudgetAmount"].ToString())) m.BudgetAmount = (decimal)dr["BudgetAmount"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtProperty"].ToString())) m.ExtProperty = (string)dr["ExtProperty"];

            return m;
        }

        /// <summary>
        /// 将预算分配设为审核通过
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Approve(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        /// <summary>
        /// 获取指定片区及所有下属片区的预算分配总额及余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public DataTable GetSumBudgetAndBalance(int AccountMonth, int OrganizeCity)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumBudgetAndBalance", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 预算调配，将指定月的预算额度从某个管理单元移至另一预算月的另一管理单元
        /// </summary>
        /// <param name="FromAccountMonth">调拔预算的源会计月</param>
        /// <param name="FromOrganizeCity">调拔预算的源额度单元</param>
        /// <param name="ToAccountMonth">调拔预算的目的月</param>
        /// <param name="ToOrganizeCity">调拔预算的目的额度单元</param>
        /// <param name="TransfeeAmount">调拔金额</param>
        /// <param name="BudgetType">调拔预算的类型</param>
        /// <param name="Staff">调拔操作人</param>
        /// <param name="Remark">调拔备注说明</param>
        /// <returns></returns>
        public int Transfer(int FromAccountMonth, int FromOrganizeCity, int FromFeeType, int ToAccountMonth, int ToOrganizeCity, int ToFeeType, decimal TransferAmount, int BudgetType, int Staff, string Remark)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FromAccountMonth", SqlDbType.Int, 4, FromAccountMonth),
				SQLDatabase.MakeInParam("@FromOrganizeCity", SqlDbType.Int, 4, FromOrganizeCity),
                SQLDatabase.MakeInParam("@FromFeeType", SqlDbType.Int, 4, FromFeeType),
                SQLDatabase.MakeInParam("@ToAccountMonth", SqlDbType.Int, 4, ToAccountMonth),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, ToOrganizeCity),                
				SQLDatabase.MakeInParam("@ToFeeType", SqlDbType.Int, 4, ToFeeType),
				SQLDatabase.MakeInParam("@BudgetType", SqlDbType.Int, 4, BudgetType),
                SQLDatabase.MakeInParam("@TransferAmount", SqlDbType.Decimal, 9, TransferAmount),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, Remark)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Transfer", prams);
        }

        /// <summary>
        /// 获取指定管理片区指定月的可用预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <returns>可用余额</returns>
        public decimal GetUsableAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@UsableAmount", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"UsableAmount", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetUsableAmount", prams);

            return (decimal)prams[4].Value;
        }

        /// <summary>
        /// 获取指定管理片区指定月的预算分配额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <returns>预算分配额度</returns>
        public decimal GetSumBudgetAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@SumBudgetAmount", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"SumBudgetAmount", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumBudgetAmount", prams);

            return (decimal)prams[4].Value;
        }

        public decimal GetSumYWCost(int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                new SqlParameter("@SumYWCost", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"SumYWCost", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumYWCost", prams);

            return (decimal)prams[1].Value;
        }

        /// <summary>
        /// 获取各管理片区预算分配记录
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public DataTable GetAssignInfo(int OrganizeCity, int AccountMonth, int Level)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetAssignInfo", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

