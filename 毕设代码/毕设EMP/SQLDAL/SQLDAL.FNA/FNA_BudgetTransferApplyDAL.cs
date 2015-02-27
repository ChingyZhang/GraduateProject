
// ===================================================================
// 文件： FNA_BudgetTransferApplyDAL.cs
// 项目名称：
// 创建时间：2010/8/19
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
    ///FNA_BudgetTransferApply数据访问DAL类
    /// </summary>
    public class FNA_BudgetTransferApplyDAL : BaseSimpleDAL<FNA_BudgetTransferApply>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetTransferApplyDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetTransferApply";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetTransferApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FromOrganizeCity", SqlDbType.Int, 4, m.FromOrganizeCity),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, m.ToOrganizeCity),
				SQLDatabase.MakeInParam("@FromAccountMonth", SqlDbType.Int, 4, m.FromAccountMonth),
				SQLDatabase.MakeInParam("@ToAccountMonth", SqlDbType.Int, 4, m.ToAccountMonth),
				SQLDatabase.MakeInParam("@FromFeeType", SqlDbType.Int, 4, m.FromFeeType),
				SQLDatabase.MakeInParam("@ToFeeType", SqlDbType.Int, 4, m.ToFeeType),
				SQLDatabase.MakeInParam("@TransferAmount", SqlDbType.Decimal, 9, m.TransferAmount),
				SQLDatabase.MakeInParam("@AdjustAmount", SqlDbType.Decimal, 9, m.AdjustAmount),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
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
        public override int Update(FNA_BudgetTransferApply m)
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
				SQLDatabase.MakeInParam("@TransferAmount", SqlDbType.Decimal, 9, m.TransferAmount),
				SQLDatabase.MakeInParam("@AdjustAmount", SqlDbType.Decimal, 9, m.AdjustAmount),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_BudgetTransferApply FillModel(IDataReader dr)
        {
            FNA_BudgetTransferApply m = new FNA_BudgetTransferApply();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["FromOrganizeCity"].ToString())) m.FromOrganizeCity = (int)dr["FromOrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ToOrganizeCity"].ToString())) m.ToOrganizeCity = (int)dr["ToOrganizeCity"];
            if (!string.IsNullOrEmpty(dr["FromAccountMonth"].ToString())) m.FromAccountMonth = (int)dr["FromAccountMonth"];
            if (!string.IsNullOrEmpty(dr["ToAccountMonth"].ToString())) m.ToAccountMonth = (int)dr["ToAccountMonth"];
            if (!string.IsNullOrEmpty(dr["FromFeeType"].ToString())) m.FromFeeType = (int)dr["FromFeeType"];
            if (!string.IsNullOrEmpty(dr["ToFeeType"].ToString())) m.ToFeeType = (int)dr["ToFeeType"];
            if (!string.IsNullOrEmpty(dr["TransferAmount"].ToString())) m.TransferAmount = (decimal)dr["TransferAmount"];
            if (!string.IsNullOrEmpty(dr["AdjustAmount"].ToString())) m.AdjustAmount = (decimal)dr["AdjustAmount"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Submit(int ID, int Staff, int ApproveTask)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, ApproveTask)				
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }
    }
}

