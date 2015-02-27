
// ===================================================================
// 文件： PM_PromotorSalaryDAL.cs
// 项目名称：
// 创建时间：2011/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;


namespace MCSFramework.SQLDAL.Promotor
{
    /// <summary>
    ///PM_PromotorSalary数据访问DAL类
    /// </summary>
    public class PM_PromotorSalaryDAL : BaseSimpleDAL<PM_PromotorSalary>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_PromotorSalaryDAL()
        {
            _ProcePrefix = "MCS_Promotor.dbo.sp_PM_PromotorSalary";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_PromotorSalary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@BasePayMode", SqlDbType.Int, 4, m.BasePayMode),
				SQLDatabase.MakeInParam("@BasePay", SqlDbType.Decimal, 9, m.BasePay),
				SQLDatabase.MakeInParam("@BasePaySubsidyMode", SqlDbType.Int, 4, m.BasePaySubsidyMode),
				SQLDatabase.MakeInParam("@BasePaySubsidy", SqlDbType.Decimal, 9, m.BasePaySubsidy),
				SQLDatabase.MakeInParam("@BasePaySubsidyBeginDate", SqlDbType.DateTime, 8, m.BasePaySubsidyBeginDate),
				SQLDatabase.MakeInParam("@BasePaySubsidyEndDate", SqlDbType.DateTime, 8, m.BasePaySubsidyEndDate),
				SQLDatabase.MakeInParam("@SeniorityPayMode", SqlDbType.Int, 4, m.SeniorityPayMode),
				SQLDatabase.MakeInParam("@MinimumWageMode", SqlDbType.Int, 4, m.MinimumWageMode),
				SQLDatabase.MakeInParam("@MinimumWage", SqlDbType.Decimal, 9, m.MinimumWage),
				SQLDatabase.MakeInParam("@MinimumWageBeginDate", SqlDbType.DateTime, 8, m.MinimumWageBeginDate),
				SQLDatabase.MakeInParam("@MinimumWageEndDate", SqlDbType.DateTime, 8, m.MinimumWageEndDate),
				SQLDatabase.MakeInParam("@InsuranceMode", SqlDbType.Int, 4, m.InsuranceMode),
				SQLDatabase.MakeInParam("@InsuranceSubsidy", SqlDbType.Decimal, 9, m.InsuranceSubsidy),
				SQLDatabase.MakeInParam("@DIBasePaySubsidy", SqlDbType.Decimal, 9, m.DIBasePaySubsidy),
				SQLDatabase.MakeInParam("@DIFeeSubsidy", SqlDbType.Decimal, 9, m.DIFeeSubsidy),
				SQLDatabase.MakeInParam("@RTManageCost", SqlDbType.Decimal, 9, m.RTManageCost),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
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
        public override int Update(PM_PromotorSalary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@BasePayMode", SqlDbType.Int, 4, m.BasePayMode),
				SQLDatabase.MakeInParam("@BasePay", SqlDbType.Decimal, 9, m.BasePay),
				SQLDatabase.MakeInParam("@BasePaySubsidyMode", SqlDbType.Int, 4, m.BasePaySubsidyMode),
				SQLDatabase.MakeInParam("@BasePaySubsidy", SqlDbType.Decimal, 9, m.BasePaySubsidy),
				SQLDatabase.MakeInParam("@BasePaySubsidyBeginDate", SqlDbType.DateTime, 8, m.BasePaySubsidyBeginDate),
				SQLDatabase.MakeInParam("@BasePaySubsidyEndDate", SqlDbType.DateTime, 8, m.BasePaySubsidyEndDate),
				SQLDatabase.MakeInParam("@SeniorityPayMode", SqlDbType.Int, 4, m.SeniorityPayMode),
				SQLDatabase.MakeInParam("@MinimumWageMode", SqlDbType.Int, 4, m.MinimumWageMode),
				SQLDatabase.MakeInParam("@MinimumWage", SqlDbType.Decimal, 9, m.MinimumWage),
				SQLDatabase.MakeInParam("@MinimumWageBeginDate", SqlDbType.DateTime, 8, m.MinimumWageBeginDate),
				SQLDatabase.MakeInParam("@MinimumWageEndDate", SqlDbType.DateTime, 8, m.MinimumWageEndDate),
				SQLDatabase.MakeInParam("@InsuranceMode", SqlDbType.Int, 4, m.InsuranceMode),
				SQLDatabase.MakeInParam("@InsuranceSubsidy", SqlDbType.Decimal, 9, m.InsuranceSubsidy),
				SQLDatabase.MakeInParam("@DIBasePaySubsidy", SqlDbType.Decimal, 9, m.DIBasePaySubsidy),
				SQLDatabase.MakeInParam("@DIFeeSubsidy", SqlDbType.Decimal, 9, m.DIFeeSubsidy),
                SQLDatabase.MakeInParam("@RTManageCost", SqlDbType.Decimal, 9, m.RTManageCost),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PM_PromotorSalary FillModel(IDataReader dr)
        {
            PM_PromotorSalary m = new PM_PromotorSalary();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Promotor"].ToString())) m.Promotor = (int)dr["Promotor"];
            if (!string.IsNullOrEmpty(dr["BasePayMode"].ToString())) m.BasePayMode = (int)dr["BasePayMode"];
            if (!string.IsNullOrEmpty(dr["BasePay"].ToString())) m.BasePay = (decimal)dr["BasePay"];
            if (!string.IsNullOrEmpty(dr["BasePaySubsidyMode"].ToString())) m.BasePaySubsidyMode = (int)dr["BasePaySubsidyMode"];
            if (!string.IsNullOrEmpty(dr["BasePaySubsidy"].ToString())) m.BasePaySubsidy = (decimal)dr["BasePaySubsidy"];
            if (!string.IsNullOrEmpty(dr["BasePaySubsidyBeginDate"].ToString())) m.BasePaySubsidyBeginDate = (DateTime)dr["BasePaySubsidyBeginDate"];
            if (!string.IsNullOrEmpty(dr["BasePaySubsidyEndDate"].ToString())) m.BasePaySubsidyEndDate = (DateTime)dr["BasePaySubsidyEndDate"];
            if (!string.IsNullOrEmpty(dr["SeniorityPayMode"].ToString())) m.SeniorityPayMode = (int)dr["SeniorityPayMode"];
            if (!string.IsNullOrEmpty(dr["MinimumWageMode"].ToString())) m.MinimumWageMode = (int)dr["MinimumWageMode"];
            if (!string.IsNullOrEmpty(dr["MinimumWage"].ToString())) m.MinimumWage = (decimal)dr["MinimumWage"];
            if (!string.IsNullOrEmpty(dr["MinimumWageBeginDate"].ToString())) m.MinimumWageBeginDate = (DateTime)dr["MinimumWageBeginDate"];
            if (!string.IsNullOrEmpty(dr["MinimumWageEndDate"].ToString())) m.MinimumWageEndDate = (DateTime)dr["MinimumWageEndDate"];
            if (!string.IsNullOrEmpty(dr["InsuranceMode"].ToString())) m.InsuranceMode = (int)dr["InsuranceMode"];
            if (!string.IsNullOrEmpty(dr["InsuranceSubsidy"].ToString())) m.InsuranceSubsidy =Convert.ToInt32( (decimal)dr["InsuranceSubsidy"]);
            if (!string.IsNullOrEmpty(dr["DIBasePaySubsidy"].ToString())) m.DIBasePaySubsidy = (decimal)dr["DIBasePaySubsidy"];
            if (!string.IsNullOrEmpty(dr["DIFeeSubsidy"].ToString())) m.DIFeeSubsidy = (decimal)dr["DIFeeSubsidy"];
            if (!string.IsNullOrEmpty(dr["RTManageCost"].ToString())) m.RTManageCost = (decimal)dr["RTManageCost"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Approve(int ID, int State)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State)				
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

            return ret;
        }
        /// <summary>
        /// 查询首两月均销量，及底薪费率
        /// </summary>
        /// <param name="Promotor"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="AvgSales"></param>
        /// <param name="BaseFeeRate"></param>
        /// <returns></returns>
        public int GetFloatingInfo(int Promotor, int AccountMonth, out decimal AvgSales, out decimal BaseFeeRate)
        {
            AvgSales = 0;
            BaseFeeRate = 0;          

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                new SqlParameter("@AvgSales", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"AvgSales", DataRowVersion.Current,0),
                new SqlParameter("@BaseFeeRate", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"BaseFeeRate", DataRowVersion.Current,0) 
            };
            #endregion

            int iret = SQLDatabase.RunProc(_ProcePrefix + "_GetFloatingInfo", prams);

            if (prams[2].Value != null && prams[2].Value != DBNull.Value) AvgSales = (decimal)prams[2].Value;
            if (prams[3].Value != null && prams[3].Value != DBNull.Value) BaseFeeRate = (decimal)prams[3].Value;
          
            return iret;
        }
    }
}

