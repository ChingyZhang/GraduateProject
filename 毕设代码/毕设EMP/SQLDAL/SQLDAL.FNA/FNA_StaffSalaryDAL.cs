
// ===================================================================
// 文件： FNA_StaffSalaryDAL.cs
// 项目名称：
// 创建时间：2009/3/22
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
    ///FNA_StaffSalary数据访问DAL类
    /// </summary>
    public class FNA_StaffSalaryDAL : BaseComplexDAL<FNA_StaffSalary, FNA_StaffSalaryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffSalary";
        }
        #endregion



        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffSalary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputTime", SqlDbType.DateTime, 8, m.InputTime),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
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
        public override int Update(FNA_StaffSalary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputTime", SqlDbType.DateTime, 8, m.InputTime),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_StaffSalary FillModel(IDataReader dr)
        {
            FNA_StaffSalary m = new FNA_StaffSalary();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InputTime"].ToString())) m.InputTime = (DateTime)dr["InputTime"];
            if (!string.IsNullOrEmpty(dr["InputStaff"].ToString())) m.InputStaff = (int)dr["InputStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(FNA_StaffSalaryDetail m)
        {
            m.SalaryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SalaryID", SqlDbType.Int, 4, m.SalaryID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Decimal, 9, m.ActWorkDays),
				SQLDatabase.MakeInParam("@BounsBase", SqlDbType.Decimal, 9, m.BounsBase),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@SalesYieldRate", SqlDbType.Decimal, 9, m.SalesYieldRate),
				SQLDatabase.MakeInParam("@SalesWtdYieldRate", SqlDbType.Decimal, 9, m.SalesWtdYieldRate),
				SQLDatabase.MakeInParam("@KeyTarget", SqlDbType.Decimal, 9, m.KeyTarget),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@KeyYieldRate", SqlDbType.Decimal, 9, m.KeyYieldRate),
				SQLDatabase.MakeInParam("@KeyWtdYieldRate", SqlDbType.Decimal, 9, m.KeyWtdYieldRate),
				SQLDatabase.MakeInParam("@FeeRateTarget", SqlDbType.Decimal, 9, m.FeeRateTarget),
				SQLDatabase.MakeInParam("@ActFeeRate", SqlDbType.Decimal, 9, m.ActFeeRate),
				SQLDatabase.MakeInParam("@FeeYieldRate", SqlDbType.Decimal, 9, m.FeeYieldRate),
				SQLDatabase.MakeInParam("@FeeWtdYieldRate", SqlDbType.Decimal, 9, m.FeeWtdYieldRate),
				SQLDatabase.MakeInParam("@KPIYieldRate", SqlDbType.Decimal, 9, m.KPIYieldRate),
				SQLDatabase.MakeInParam("@KPIBonus", SqlDbType.Decimal, 9, m.KPIBonus),
				SQLDatabase.MakeInParam("@DeductedBonus", SqlDbType.Decimal, 9, m.DeductedBonus),
				SQLDatabase.MakeInParam("@TotalYieldRate", SqlDbType.Decimal, 9, m.TotalYieldRate),
				SQLDatabase.MakeInParam("@BonusAdd", SqlDbType.Decimal, 9, m.BonusAdd),
				SQLDatabase.MakeInParam("@Bounsdeduction", SqlDbType.Decimal, 9, m.Bounsdeduction),
				SQLDatabase.MakeInParam("@Bonus", SqlDbType.Decimal, 9, m.Bonus),
				SQLDatabase.MakeInParam("@TotalSalary", SqlDbType.Decimal, 9, m.TotalSalary),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(FNA_StaffSalaryDetail m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SalaryID", SqlDbType.Int, 4, m.SalaryID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Decimal, 9, m.ActWorkDays),
				SQLDatabase.MakeInParam("@BounsBase", SqlDbType.Decimal, 9, m.BounsBase),
				SQLDatabase.MakeInParam("@SalesTarget", SqlDbType.Decimal, 9, m.SalesTarget),
				SQLDatabase.MakeInParam("@ActSales", SqlDbType.Decimal, 9, m.ActSales),
				SQLDatabase.MakeInParam("@SalesYieldRate", SqlDbType.Decimal, 9, m.SalesYieldRate),
				SQLDatabase.MakeInParam("@SalesWtdYieldRate", SqlDbType.Decimal, 9, m.SalesWtdYieldRate),
				SQLDatabase.MakeInParam("@KeyTarget", SqlDbType.Decimal, 9, m.KeyTarget),
				SQLDatabase.MakeInParam("@ActKeySales", SqlDbType.Decimal, 9, m.ActKeySales),
				SQLDatabase.MakeInParam("@KeyYieldRate", SqlDbType.Decimal, 9, m.KeyYieldRate),
				SQLDatabase.MakeInParam("@KeyWtdYieldRate", SqlDbType.Decimal, 9, m.KeyWtdYieldRate),
				SQLDatabase.MakeInParam("@FeeRateTarget", SqlDbType.Decimal, 9, m.FeeRateTarget),
				SQLDatabase.MakeInParam("@ActFeeRate", SqlDbType.Decimal, 9, m.ActFeeRate),
				SQLDatabase.MakeInParam("@FeeYieldRate", SqlDbType.Decimal, 9, m.FeeYieldRate),
				SQLDatabase.MakeInParam("@FeeWtdYieldRate", SqlDbType.Decimal, 9, m.FeeWtdYieldRate),
				SQLDatabase.MakeInParam("@KPIYieldRate", SqlDbType.Decimal, 9, m.KPIYieldRate),
				SQLDatabase.MakeInParam("@KPIBonus", SqlDbType.Decimal, 9, m.KPIBonus),
				SQLDatabase.MakeInParam("@DeductedBonus", SqlDbType.Decimal, 9, m.DeductedBonus),
				SQLDatabase.MakeInParam("@TotalYieldRate", SqlDbType.Decimal, 9, m.TotalYieldRate),
				SQLDatabase.MakeInParam("@BonusAdd", SqlDbType.Decimal, 9, m.BonusAdd),
				SQLDatabase.MakeInParam("@Bounsdeduction", SqlDbType.Decimal, 9, m.Bounsdeduction),
				SQLDatabase.MakeInParam("@Bonus", SqlDbType.Decimal, 9, m.Bonus),
				SQLDatabase.MakeInParam("@TotalSalary", SqlDbType.Decimal, 9, m.TotalSalary),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override FNA_StaffSalaryDetail FillDetailModel(IDataReader dr)
        {
            FNA_StaffSalaryDetail m = new FNA_StaffSalaryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SalaryID"].ToString())) m.SalaryID = (int)dr["SalaryID"];
            if (!string.IsNullOrEmpty(dr["Staff"].ToString())) m.Staff = (int)dr["Staff"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ActWorkDays"].ToString())) m.ActWorkDays = (decimal)dr["ActWorkDays"];
            if (!string.IsNullOrEmpty(dr["BounsBase"].ToString())) m.BounsBase = (decimal)dr["BounsBase"];
            if (!string.IsNullOrEmpty(dr["SalesTarget"].ToString())) m.SalesTarget = (decimal)dr["SalesTarget"];
            if (!string.IsNullOrEmpty(dr["ActSales"].ToString())) m.ActSales = (decimal)dr["ActSales"];
            if (!string.IsNullOrEmpty(dr["SalesYieldRate"].ToString())) m.SalesYieldRate = (decimal)dr["SalesYieldRate"];
            if (!string.IsNullOrEmpty(dr["SalesWtdYieldRate"].ToString())) m.SalesWtdYieldRate = (decimal)dr["SalesWtdYieldRate"];
            if (!string.IsNullOrEmpty(dr["KeyTarget"].ToString())) m.KeyTarget = (decimal)dr["KeyTarget"];
            if (!string.IsNullOrEmpty(dr["ActKeySales"].ToString())) m.ActKeySales = (decimal)dr["ActKeySales"];
            if (!string.IsNullOrEmpty(dr["KeyYieldRate"].ToString())) m.KeyYieldRate = (decimal)dr["KeyYieldRate"];
            if (!string.IsNullOrEmpty(dr["KeyWtdYieldRate"].ToString())) m.KeyWtdYieldRate = (decimal)dr["KeyWtdYieldRate"];
            if (!string.IsNullOrEmpty(dr["FeeRateTarget"].ToString())) m.FeeRateTarget = (decimal)dr["FeeRateTarget"];
            if (!string.IsNullOrEmpty(dr["ActFeeRate"].ToString())) m.ActFeeRate = (decimal)dr["ActFeeRate"];
            if (!string.IsNullOrEmpty(dr["FeeYieldRate"].ToString())) m.FeeYieldRate = (decimal)dr["FeeYieldRate"];
            if (!string.IsNullOrEmpty(dr["FeeWtdYieldRate"].ToString())) m.FeeWtdYieldRate = (decimal)dr["FeeWtdYieldRate"];
            if (!string.IsNullOrEmpty(dr["KPIYieldRate"].ToString())) m.KPIYieldRate = (decimal)dr["KPIYieldRate"];
            if (!string.IsNullOrEmpty(dr["KPIBonus"].ToString())) m.KPIBonus = (decimal)dr["KPIBonus"];
            if (!string.IsNullOrEmpty(dr["DeductedBonus"].ToString())) m.DeductedBonus = (decimal)dr["DeductedBonus"];
            if (!string.IsNullOrEmpty(dr["TotalYieldRate"].ToString())) m.TotalYieldRate = (decimal)dr["TotalYieldRate"];
            if (!string.IsNullOrEmpty(dr["BonusAdd"].ToString())) m.BonusAdd = (decimal)dr["BonusAdd"];
            if (!string.IsNullOrEmpty(dr["Bounsdeduction"].ToString())) m.Bounsdeduction = (decimal)dr["Bounsdeduction"];
            if (!string.IsNullOrEmpty(dr["Bonus"].ToString())) m.Bonus = (decimal)dr["Bonus"];
            if (!string.IsNullOrEmpty(dr["TotalSalary"].ToString())) m.TotalSalary = (decimal)dr["TotalSalary"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }


        public int Submit(int id, int staff, int taskid, int feetype)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4,taskid),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4,feetype)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }


        public decimal GetSumSalary(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                new SqlParameter("@SumSalary", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"SumSalary", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumSalary", prams);

            return (decimal)prams[1].Value;
        }

        public void UpdateAdjustRecord(int ID, int Staff, string OldAdjustCost, string AdjustCost, string staffName)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@OldAdjustCost", SqlDbType.VarChar, 20,OldAdjustCost),
                SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.VarChar,20, AdjustCost),
                SQLDatabase.MakeInParam("@StaffName",SqlDbType.VarChar,50,staffName)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_UpdateAdjustRecord", prams);
        }

        /// <summary>
        /// 计算个人所得税
        /// </summary>
        /// <param name="Income"></param>
        /// <returns></returns>
        public decimal ComputeIncomeTax(decimal Income)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Income", SqlDbType.Decimal, 10, Income),
                new SqlParameter("@Tax", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,2,"Tax", DataRowVersion.Current,0)

			};
            #endregion
            SQLDatabase.RunProc("MCS_Pub.dbo.sp_Pub_ComputeIncomeTax", prams);
            return (decimal)prams[1].Value;
        }

        public int Generate(int AccountMonth, int PositionType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth) ,
                SQLDatabase.MakeInParam("@PositionType", SqlDbType.Int, 4, PositionType)

			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Generate", prams);
            return ret;
        }

        /// <summary>
        /// 获取办事处主任或业务代表员工工资详细
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="StaffType">员工类型（1：办事处主任；2：业务代表）</param>
        /// <returns></returns>
        public IDataReader GetStaffSalaryDetail(string Condition, int StaffType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Condition", SqlDbType.VarChar,2000, Condition) 
			};
            #endregion

            string procName = _ProcePrefix + "_Detail00" + StaffType;
            SqlDataReader adr = null;
            SQLDatabase.RunProc(procName, prams, out adr);
            return adr;
        }
    }
}

