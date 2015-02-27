
// ===================================================================
// 文件： SVM_ClientSalesTargetDAL.cs
// 项目名称：
// 创建时间：2013/10/23
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
    ///SVM_ClientSalesTarget数据访问DAL类
    /// </summary>
    public class SVM_ClientSalesTargetDAL : BaseSimpleDAL<SVM_ClientSalesTarget>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_ClientSalesTargetDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_ClientSalesTarget";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_ClientSalesTarget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@KeySalesTarget", SqlDbType.Decimal, 9, m.KeySalesTarget),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@SalesTargetAdjust", SqlDbType.Decimal, 9, m.SalesTargetAdjust),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Data09", SqlDbType.Decimal, 9, m.Data09),
				SQLDatabase.MakeInParam("@Data10", SqlDbType.Decimal, 9, m.Data10),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag)
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
        public override int Update(SVM_ClientSalesTarget m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@KeySalesTarget", SqlDbType.Decimal, 9, m.KeySalesTarget),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@SalesTargetAdjust", SqlDbType.Decimal, 9, m.SalesTargetAdjust),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Data09", SqlDbType.Decimal, 9, m.Data09),
				SQLDatabase.MakeInParam("@Data10", SqlDbType.Decimal, 9, m.Data10),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_ClientSalesTarget FillModel(IDataReader dr)
        {
            SVM_ClientSalesTarget m = new SVM_ClientSalesTarget();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["DISupplier"].ToString())) m.DISupplier = (int)dr["DISupplier"];
            if (!string.IsNullOrEmpty(dr["ClientManager"].ToString())) m.ClientManager = (int)dr["ClientManager"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["KeySalesTarget"].ToString())) m.KeySalesTarget = (decimal)dr["KeySalesTarget"];
            if (!string.IsNullOrEmpty(dr["ActSales"].ToString())) m.ActSales = (decimal)dr["ActSales"];
            if (!string.IsNullOrEmpty(dr["ActKeySales"].ToString())) m.ActKeySales = (decimal)dr["ActKeySales"];
            if (!string.IsNullOrEmpty(dr["SalesTargetAdjust"].ToString())) m.SalesTargetAdjust = (decimal)dr["SalesTargetAdjust"];
            if (!string.IsNullOrEmpty(dr["Data01"].ToString())) m.Data01 = (decimal)dr["Data01"];
            if (!string.IsNullOrEmpty(dr["Data02"].ToString())) m.Data02 = (decimal)dr["Data02"];
            if (!string.IsNullOrEmpty(dr["Data03"].ToString())) m.Data03 = (decimal)dr["Data03"];
            if (!string.IsNullOrEmpty(dr["Data04"].ToString())) m.Data04 = (decimal)dr["Data04"];
            if (!string.IsNullOrEmpty(dr["Data05"].ToString())) m.Data05 = (decimal)dr["Data05"];
            if (!string.IsNullOrEmpty(dr["Data06"].ToString())) m.Data06 = (decimal)dr["Data06"];
            if (!string.IsNullOrEmpty(dr["Data07"].ToString())) m.Data07 = (decimal)dr["Data07"];
            if (!string.IsNullOrEmpty(dr["Data08"].ToString())) m.Data08 = (decimal)dr["Data08"];
            if (!string.IsNullOrEmpty(dr["Data09"].ToString())) m.Data09 = (decimal)dr["Data09"];
            if (!string.IsNullOrEmpty(dr["Data10"].ToString())) m.Data10 = (decimal)dr["Data10"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];

            return m;
        }
    }
}

