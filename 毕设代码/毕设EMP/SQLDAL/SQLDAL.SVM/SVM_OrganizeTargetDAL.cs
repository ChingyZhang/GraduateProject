
// ===================================================================
// 文件： SVM_OrganizeTargetDAL.cs
// 项目名称：
// 创建时间：2013/4/7
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;


namespace MCSFramework.SQLDAL.SVM
{
    /// <summary>
    ///SVM_OrganizeTarget数据访问DAL类
    /// </summary>
    public class SVM_OrganizeTargetDAL : BaseComplexDAL<SVM_OrganizeTarget, SVM_KeyProductTarget_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_OrganizeTargetDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_OrganizeTarget";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_OrganizeTarget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@FeeRateTarget", SqlDbType.Decimal, 9, m.FeeRateTarget),
                SQLDatabase.MakeInParam("@FeeYieldRate", SqlDbType.Decimal, 9, m.FeeYieldRate),
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
        public override int Update(SVM_OrganizeTarget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
                SQLDatabase.MakeInParam("@SalesTargetAdjust", SqlDbType.Decimal, 9, m.SalesTargetAdjust),
				SQLDatabase.MakeInParam("@FeeRateTarget", SqlDbType.Decimal, 9, m.FeeRateTarget),
                SQLDatabase.MakeInParam("@FeeYieldRate", SqlDbType.Decimal, 9, m.FeeYieldRate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_OrganizeTarget FillModel(IDataReader dr)
        {
            SVM_OrganizeTarget m = new SVM_OrganizeTarget();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["FeeRateTarget"].ToString())) m.FeeRateTarget = (decimal)dr["FeeRateTarget"];
            if (!string.IsNullOrEmpty(dr["FeeYieldRate"].ToString())) m.FeeYieldRate = (decimal)dr["FeeYieldRate"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(SVM_KeyProductTarget_Detail m)
        {
            m.TargetID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4, m.TargetID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_KeyProductTarget_Detail m)
        {
            m.TargetID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4, m.TargetID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_KeyProductTarget_Detail FillDetailModel(IDataReader dr)
        {
            SVM_KeyProductTarget_Detail m = new SVM_KeyProductTarget_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["TargetID"].ToString())) m.TargetID = (int)dr["TargetID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Amount"].ToString())) m.Amount = (decimal)dr["Amount"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["OriginalValue"].ToString())) m.OriginalValue = (decimal)dr["OriginalValue"];

            return m;
        }

        public decimal GetSumByID(int ID)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4,ID),
                new SqlParameter("@Sum", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"Sum", DataRowVersion.Current,0)                   
                                   };
            SQLDatabase.RunProc(_ProcePrefix + "_GetSumByID", prams);
            return (decimal)prams[1].Value;
        }

        public int UnApprove(int AccountMonth, int OrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity) 
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UnApprove", prams);

            return ret;

        }
        public int Approve(int AccountMonth, int OrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity) 
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

            return ret;

        }
    }
}

