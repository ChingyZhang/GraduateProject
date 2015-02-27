
// ===================================================================
// 文件： CAT_FeeApplyDetailDAL.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CAT;


namespace MCSFramework.SQLDAL.CAT
{
    /// <summary>
    ///CAT_FeeApplyDetail数据访问DAL类
    /// </summary>
    public class CAT_FeeApplyDetailDAL : BaseSimpleDAL<CAT_FeeApplyDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CAT_FeeApplyDetailDAL()
        {
            _ProcePrefix = "MCS_CAT.dbo.sp_CAT_FeeApplyDetail";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CAT_FeeApplyDetail m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@AvailCost", SqlDbType.Decimal, 9, m.AvailCost),
				SQLDatabase.MakeInParam("@CancelCost", SqlDbType.Decimal, 9, m.CancelCost),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@DICost", SqlDbType.Decimal, 9, m.DICost),
				SQLDatabase.MakeInParam("@DIAdjustCost", SqlDbType.Decimal, 9, m.DIAdjustCost),
				SQLDatabase.MakeInParam("@RelateContractDetail", SqlDbType.Int, 4, m.RelateContractDetail),
				SQLDatabase.MakeInParam("@SalesForcast", SqlDbType.Decimal, 9, m.SalesForcast),
				SQLDatabase.MakeInParam("@LastWriteOffMonth", SqlDbType.Int, 4, m.LastWriteOffMonth)
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
        public override int Update(CAT_FeeApplyDetail m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@AvailCost", SqlDbType.Decimal, 9, m.AvailCost),
				SQLDatabase.MakeInParam("@CancelCost", SqlDbType.Decimal, 9, m.CancelCost),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@DICost", SqlDbType.Decimal, 9, m.DICost),
				SQLDatabase.MakeInParam("@DIAdjustCost", SqlDbType.Decimal, 9, m.DIAdjustCost),
				SQLDatabase.MakeInParam("@RelateContractDetail", SqlDbType.Int, 4, m.RelateContractDetail),
				SQLDatabase.MakeInParam("@SalesForcast", SqlDbType.Decimal, 9, m.SalesForcast),
				SQLDatabase.MakeInParam("@LastWriteOffMonth", SqlDbType.Int, 4, m.LastWriteOffMonth)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CAT_FeeApplyDetail FillModel(IDataReader dr)
        {
            CAT_FeeApplyDetail m = new CAT_FeeApplyDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Activity"].ToString())) m.Activity = (int)dr["Activity"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString())) m.AccountTitle = (int)dr["AccountTitle"];
            if (!string.IsNullOrEmpty(dr["ApplyCost"].ToString())) m.ApplyCost = (decimal)dr["ApplyCost"];
            if (!string.IsNullOrEmpty(dr["AdjustCost"].ToString())) m.AdjustCost = (decimal)dr["AdjustCost"];
            if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString())) m.AdjustReason = (string)dr["AdjustReason"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["BeginMonth"].ToString())) m.BeginMonth = (int)dr["BeginMonth"];
            if (!string.IsNullOrEmpty(dr["EndMonth"].ToString())) m.EndMonth = (int)dr["EndMonth"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["AvailCost"].ToString())) m.AvailCost = (decimal)dr["AvailCost"];
            if (!string.IsNullOrEmpty(dr["CancelCost"].ToString())) m.CancelCost = (decimal)dr["CancelCost"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["DICost"].ToString())) m.DICost = (decimal)dr["DICost"];
            if (!string.IsNullOrEmpty(dr["DIAdjustCost"].ToString())) m.DIAdjustCost = (decimal)dr["DIAdjustCost"];
            if (!string.IsNullOrEmpty(dr["RelateContractDetail"].ToString())) m.RelateContractDetail = (int)dr["RelateContractDetail"];
            if (!string.IsNullOrEmpty(dr["SalesForcast"].ToString())) m.SalesForcast = (decimal)dr["SalesForcast"];
            if (!string.IsNullOrEmpty(dr["LastWriteOffMonth"].ToString())) m.LastWriteOffMonth = (int)dr["LastWriteOffMonth"];

            return m;
        }
    }
}

