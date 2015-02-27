
// ===================================================================
// 文件： FNA_StaffSalaryDataObjectBetaDAL.cs
// 项目名称：
// 创建时间：2014/7/14
// 作者:	   Jace
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
    ///FNA_StaffSalaryDataObjectBeta数据访问DAL类
    /// </summary>
    public class FNA_StaffSalaryDataObjectBetaDAL : BaseSimpleDAL<FNA_StaffSalaryDataObjectBeta>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObjectBetaDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffSalaryDataObjectBeta";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffSalaryDataObjectBeta m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, m.PositionType),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesTargetAdjust", SqlDbType.Decimal, 9, m.SalesTargetAdjust),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SubmitFlag", SqlDbType.Int, 4, m.SubmitFlag),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
                SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 8000, m.Remark)
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
        public override int Update(FNA_StaffSalaryDataObjectBeta m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, m.PositionType),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@SalesTargetAdjust", SqlDbType.Decimal, 9, m.SalesTargetAdjust),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@Data06", SqlDbType.Decimal, 9, m.Data06),
				SQLDatabase.MakeInParam("@Data07", SqlDbType.Decimal, 9, m.Data07),
				SQLDatabase.MakeInParam("@Data08", SqlDbType.Decimal, 9, m.Data08),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SubmitFlag", SqlDbType.Int, 4, m.SubmitFlag),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
                SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 8000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_StaffSalaryDataObjectBeta FillModel(IDataReader dr)
        {
            FNA_StaffSalaryDataObjectBeta m = new FNA_StaffSalaryDataObjectBeta();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Staff"].ToString())) m.Staff = (int)dr["Staff"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["PositionType"].ToString())) m.PositionType = (int)dr["PositionType"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["SalesTargetAdjust"].ToString())) m.SalesTargetAdjust = (decimal)dr["SalesTargetAdjust"];
            if (!string.IsNullOrEmpty(dr["Data01"].ToString())) m.Data01 = (decimal)dr["Data01"];
            if (!string.IsNullOrEmpty(dr["Data02"].ToString())) m.Data02 = (decimal)dr["Data02"];
            if (!string.IsNullOrEmpty(dr["Data03"].ToString())) m.Data03 = (decimal)dr["Data03"];
            if (!string.IsNullOrEmpty(dr["Data04"].ToString())) m.Data04 = (decimal)dr["Data04"];
            if (!string.IsNullOrEmpty(dr["Data05"].ToString())) m.Data05 = (decimal)dr["Data05"];
            if (!string.IsNullOrEmpty(dr["Data06"].ToString())) m.Data06 = (decimal)dr["Data06"];
            if (!string.IsNullOrEmpty(dr["Data07"].ToString())) m.Data07 = (decimal)dr["Data07"];
            if (!string.IsNullOrEmpty(dr["Data08"].ToString())) m.Data08 = (decimal)dr["Data08"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["SubmitFlag"].ToString())) m.SubmitFlag = (int)dr["SubmitFlag"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];

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
        public int SigleApprove(int ID, int ApproveFlag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, ApproveFlag)
                   
             
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SigleApprove", prams);

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

