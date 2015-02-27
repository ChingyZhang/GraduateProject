
// ===================================================================
// 文件： FNA_StaffSalaryDataObjectDAL.cs
// 项目名称：
// 创建时间：2013/11/19
// 作者:	   chf
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
    ///FNA_StaffSalaryDataObject数据访问DAL类
    /// </summary>
    public class FNA_StaffSalaryDataObjectDAL : BaseSimpleDAL<FNA_StaffSalaryDataObject>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObjectDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffSalaryDataObject";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffSalaryDataObject m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff), 
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesTargetAdujst", SqlDbType.Decimal, 9, m.SalesTargetAdujst),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@KeyTarget", SqlDbType.Decimal, 9, m.KeyTarget),
				SQLDatabase.MakeInParam("@KeyTargetAdjust", SqlDbType.Decimal, 9, m.KeyTargetAdjust),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@KPIYieldRate", SqlDbType.Decimal, 9, m.KPIYieldRate),
				SQLDatabase.MakeInParam("@BounsBase", SqlDbType.Decimal, 9, m.BounsBase),
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
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SubmitFlag", SqlDbType.Int, 4, m.SubmitFlag),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(FNA_StaffSalaryDataObject m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff), 
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesTargetAdujst", SqlDbType.Decimal, 9, m.SalesTargetAdujst),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@KeyTarget", SqlDbType.Decimal, 9, m.KeyTarget),
				SQLDatabase.MakeInParam("@KeyTargetAdjust", SqlDbType.Decimal, 9, m.KeyTargetAdjust),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@KPIYieldRate", SqlDbType.Decimal, 9, m.KPIYieldRate),
				SQLDatabase.MakeInParam("@BounsBase", SqlDbType.Decimal, 9, m.BounsBase),
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
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SubmitFlag", SqlDbType.Int, 4, m.SubmitFlag),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_StaffSalaryDataObject FillModel(IDataReader dr)
        {
            FNA_StaffSalaryDataObject m = new FNA_StaffSalaryDataObject();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Staff"].ToString())) m.Staff = (int)dr["Staff"];
            if (!string.IsNullOrEmpty(dr["DirectorOp"].ToString())) m.DirectorOp = (int)dr["DirectorOp"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["SalesTargetAdujst"].ToString())) m.SalesTargetAdujst = (decimal)dr["SalesTargetAdujst"];
            if (!string.IsNullOrEmpty(dr["ActSales"].ToString())) m.ActSales = (decimal)dr["ActSales"];
            if (!string.IsNullOrEmpty(dr["KeyTarget"].ToString())) m.KeyTarget = (decimal)dr["KeyTarget"];
            if (!string.IsNullOrEmpty(dr["KeyTargetAdjust"].ToString())) m.KeyTargetAdjust = (decimal)dr["KeyTargetAdjust"];
            if (!string.IsNullOrEmpty(dr["ActKeySales"].ToString())) m.ActKeySales = (decimal)dr["ActKeySales"];
            if (!string.IsNullOrEmpty(dr["KPIYieldRate"].ToString())) m.KPIYieldRate = (decimal)dr["KPIYieldRate"];
            if (!string.IsNullOrEmpty(dr["BounsBase"].ToString())) m.BounsBase = (decimal)dr["BounsBase"];
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
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["SubmitFlag"].ToString())) m.SubmitFlag = (int)dr["SubmitFlag"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int SubmitFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, PositionType)    
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SubmitFlag", prams);

        }
        public int ApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, PositionType)    
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_ApproveFlag", prams);


        }
        public int UnApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, PositionType)    
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_UnApproveFlag", prams);


        }
        public int Approve(int AccountMonth, int OrganizeCity, int PositionType, int ApproveFlag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, PositionType),
                SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, ApproveFlag)
                   
             
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

        }
        public int Adjust(int AccountMonth, decimal AdjustRate, int ClientManager)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@AdjustRate", SqlDbType.Decimal, 9, AdjustRate),
                SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, ClientManager)
             
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Adjust", prams);

        }
    }
}

