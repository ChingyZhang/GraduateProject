
// ===================================================================
// 文件： PM_SalaryDAL.cs
// 项目名称：
// 创建时间：2009/2/27
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
    ///PM_Salary数据访问DAL类
    /// </summary>
    public class PM_SalaryDAL : BaseComplexDAL<PM_Salary, PM_SalaryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDAL()
        {
            _ProcePrefix = "MCS_Promotor.dbo.sp_PM_Salary";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_Salary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(PM_Salary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PM_Salary FillModel(IDataReader dr)
        {
            PM_Salary m = new PM_Salary();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InputTime"].ToString())) m.InputTime = (DateTime)dr["InputTime"];
            if (!string.IsNullOrEmpty(dr["InputStaff"].ToString())) m.InputStaff = (int)dr["InputStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(PM_SalaryDetail m)
        {
            m.SalaryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SalaryID", SqlDbType.Int, 4, m.SalaryID),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Decimal, 9, m.ActWorkDays),
				SQLDatabase.MakeInParam("@TargetSalesVolume", SqlDbType.Decimal, 9, m.TargetSalesVolume),
				SQLDatabase.MakeInParam("@ActSalesVolume", SqlDbType.Decimal, 9, m.ActSalesVolume),
				SQLDatabase.MakeInParam("@KPIScore", SqlDbType.Decimal, 9, m.KPIScore),
				SQLDatabase.MakeInParam("@Bonus", SqlDbType.Decimal, 9, m.Bonus),
				SQLDatabase.MakeInParam("@TotalSalary", SqlDbType.Decimal, 9, m.TotalSalary),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@Pay1", SqlDbType.Decimal, 9, m.Pay1),
				SQLDatabase.MakeInParam("@Pay2", SqlDbType.Decimal, 9, m.Pay2),
				SQLDatabase.MakeInParam("@Pay3", SqlDbType.Decimal, 9, m.Pay3),
				SQLDatabase.MakeInParam("@Pay4", SqlDbType.Decimal, 9, m.Pay4),
				SQLDatabase.MakeInParam("@Pay5", SqlDbType.Decimal, 9, m.Pay5),
				SQLDatabase.MakeInParam("@Pay6", SqlDbType.Decimal, 9, m.Pay6),
				SQLDatabase.MakeInParam("@Pay7", SqlDbType.Decimal, 9, m.Pay7),
				SQLDatabase.MakeInParam("@Pay8", SqlDbType.Decimal, 9, m.Pay8),
				SQLDatabase.MakeInParam("@Pay9", SqlDbType.Decimal, 9, m.Pay9),
				SQLDatabase.MakeInParam("@Pay10", SqlDbType.Decimal, 9, m.Pay10),
				SQLDatabase.MakeInParam("@Pay11", SqlDbType.Decimal, 9, m.Pay11),
				SQLDatabase.MakeInParam("@Pay12", SqlDbType.Decimal, 9, m.Pay12),
				SQLDatabase.MakeInParam("@Pay13", SqlDbType.Decimal, 9, m.Pay13),
				SQLDatabase.MakeInParam("@Pay14", SqlDbType.Decimal, 9, m.Pay14),
				SQLDatabase.MakeInParam("@Pay15", SqlDbType.Decimal, 9, m.Pay15),
				SQLDatabase.MakeInParam("@Pay16", SqlDbType.Decimal, 9, m.Pay16),
				SQLDatabase.MakeInParam("@Pay17", SqlDbType.Decimal, 9, m.Pay17),
				SQLDatabase.MakeInParam("@Pay18", SqlDbType.Decimal, 9, m.Pay18),
				SQLDatabase.MakeInParam("@Pay19", SqlDbType.Decimal, 9, m.Pay19),
				SQLDatabase.MakeInParam("@Pay20", SqlDbType.Decimal, 9, m.Pay20),
				SQLDatabase.MakeInParam("@Sum1", SqlDbType.Decimal, 9, m.Sum1),
				SQLDatabase.MakeInParam("@Sum2", SqlDbType.Decimal, 9, m.Sum2),
				SQLDatabase.MakeInParam("@Sum3", SqlDbType.Decimal, 9, m.Sum3),
				SQLDatabase.MakeInParam("@Sum4", SqlDbType.Decimal, 9, m.Sum4),
				SQLDatabase.MakeInParam("@Tax", SqlDbType.Decimal, 9, m.Tax),
				SQLDatabase.MakeInParam("@CoPMFee", SqlDbType.Decimal, 9, m.CoPMFee),
				SQLDatabase.MakeInParam("@DIPMFee", SqlDbType.Decimal, 9, m.DIPMFee),
				SQLDatabase.MakeInParam("@PMFeeTotal", SqlDbType.Decimal, 9, m.PMFeeTotal)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PM_SalaryDetail m)
        {
            m.SalaryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SalaryID", SqlDbType.Int, 4, m.SalaryID),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@ActWorkDays", SqlDbType.Decimal, 9, m.ActWorkDays),
				SQLDatabase.MakeInParam("@TargetSalesVolume", SqlDbType.Decimal, 9, m.TargetSalesVolume),
				SQLDatabase.MakeInParam("@ActSalesVolume", SqlDbType.Decimal, 9, m.ActSalesVolume),
				SQLDatabase.MakeInParam("@KPIScore", SqlDbType.Decimal, 9, m.KPIScore),
				SQLDatabase.MakeInParam("@Bonus", SqlDbType.Decimal, 9, m.Bonus),
				SQLDatabase.MakeInParam("@TotalSalary", SqlDbType.Decimal, 9, m.TotalSalary),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@Pay1", SqlDbType.Decimal, 9, m.Pay1),
				SQLDatabase.MakeInParam("@Pay2", SqlDbType.Decimal, 9, m.Pay2),
				SQLDatabase.MakeInParam("@Pay3", SqlDbType.Decimal, 9, m.Pay3),
				SQLDatabase.MakeInParam("@Pay4", SqlDbType.Decimal, 9, m.Pay4),
				SQLDatabase.MakeInParam("@Pay5", SqlDbType.Decimal, 9, m.Pay5),
				SQLDatabase.MakeInParam("@Pay6", SqlDbType.Decimal, 9, m.Pay6),
				SQLDatabase.MakeInParam("@Pay7", SqlDbType.Decimal, 9, m.Pay7),
				SQLDatabase.MakeInParam("@Pay8", SqlDbType.Decimal, 9, m.Pay8),
				SQLDatabase.MakeInParam("@Pay9", SqlDbType.Decimal, 9, m.Pay9),
				SQLDatabase.MakeInParam("@Pay10", SqlDbType.Decimal, 9, m.Pay10),
				SQLDatabase.MakeInParam("@Pay11", SqlDbType.Decimal, 9, m.Pay11),
				SQLDatabase.MakeInParam("@Pay12", SqlDbType.Decimal, 9, m.Pay12),
				SQLDatabase.MakeInParam("@Pay13", SqlDbType.Decimal, 9, m.Pay13),
				SQLDatabase.MakeInParam("@Pay14", SqlDbType.Decimal, 9, m.Pay14),
				SQLDatabase.MakeInParam("@Pay15", SqlDbType.Decimal, 9, m.Pay15),
				SQLDatabase.MakeInParam("@Pay16", SqlDbType.Decimal, 9, m.Pay16),
				SQLDatabase.MakeInParam("@Pay17", SqlDbType.Decimal, 9, m.Pay17),
				SQLDatabase.MakeInParam("@Pay18", SqlDbType.Decimal, 9, m.Pay18),
				SQLDatabase.MakeInParam("@Pay19", SqlDbType.Decimal, 9, m.Pay19),
				SQLDatabase.MakeInParam("@Pay20", SqlDbType.Decimal, 9, m.Pay20),
				SQLDatabase.MakeInParam("@Sum1", SqlDbType.Decimal, 9, m.Sum1),
				SQLDatabase.MakeInParam("@Sum2", SqlDbType.Decimal, 9, m.Sum2),
				SQLDatabase.MakeInParam("@Sum3", SqlDbType.Decimal, 9, m.Sum3),
				SQLDatabase.MakeInParam("@Sum4", SqlDbType.Decimal, 9, m.Sum4),
				SQLDatabase.MakeInParam("@Tax", SqlDbType.Decimal, 9, m.Tax),
				SQLDatabase.MakeInParam("@CoPMFee", SqlDbType.Decimal, 9, m.CoPMFee),
				SQLDatabase.MakeInParam("@DIPMFee", SqlDbType.Decimal, 9, m.DIPMFee),
				SQLDatabase.MakeInParam("@PMFeeTotal", SqlDbType.Decimal, 9, m.PMFeeTotal)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override PM_SalaryDetail FillDetailModel(IDataReader dr)
        {
            PM_SalaryDetail m = new PM_SalaryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SalaryID"].ToString())) m.SalaryID = (int)dr["SalaryID"];
            if (!string.IsNullOrEmpty(dr["Promotor"].ToString())) m.Promotor = (int)dr["Promotor"];
            if (!string.IsNullOrEmpty(dr["ActWorkDays"].ToString())) m.ActWorkDays = (decimal)dr["ActWorkDays"];
            if (!string.IsNullOrEmpty(dr["TargetSalesVolume"].ToString())) m.TargetSalesVolume = (decimal)dr["TargetSalesVolume"];
            if (!string.IsNullOrEmpty(dr["ActSalesVolume"].ToString())) m.ActSalesVolume = (decimal)dr["ActSalesVolume"];
            if (!string.IsNullOrEmpty(dr["KPIScore"].ToString())) m.KPIScore = (decimal)dr["KPIScore"];
            if (!string.IsNullOrEmpty(dr["Bonus"].ToString())) m.Bonus = (decimal)dr["Bonus"];
            if (!string.IsNullOrEmpty(dr["TotalSalary"].ToString())) m.TotalSalary = (decimal)dr["TotalSalary"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["Pay1"].ToString())) m.Pay1 = (decimal)dr["Pay1"];
            if (!string.IsNullOrEmpty(dr["Pay2"].ToString())) m.Pay2 = (decimal)dr["Pay2"];
            if (!string.IsNullOrEmpty(dr["Pay3"].ToString())) m.Pay3 = (decimal)dr["Pay3"];
            if (!string.IsNullOrEmpty(dr["Pay4"].ToString())) m.Pay4 = (decimal)dr["Pay4"];
            if (!string.IsNullOrEmpty(dr["Pay5"].ToString())) m.Pay5 = (decimal)dr["Pay5"];
            if (!string.IsNullOrEmpty(dr["Pay6"].ToString())) m.Pay6 = (decimal)dr["Pay6"];
            if (!string.IsNullOrEmpty(dr["Pay7"].ToString())) m.Pay7 = (decimal)dr["Pay7"];
            if (!string.IsNullOrEmpty(dr["Pay8"].ToString())) m.Pay8 = (decimal)dr["Pay8"];
            if (!string.IsNullOrEmpty(dr["Pay9"].ToString())) m.Pay9 = (decimal)dr["Pay9"];
            if (!string.IsNullOrEmpty(dr["Pay10"].ToString())) m.Pay10 = (decimal)dr["Pay10"];
            if (!string.IsNullOrEmpty(dr["Pay11"].ToString())) m.Pay11 = (decimal)dr["Pay11"];
            if (!string.IsNullOrEmpty(dr["Pay12"].ToString())) m.Pay12 = (decimal)dr["Pay12"];
            if (!string.IsNullOrEmpty(dr["Pay13"].ToString())) m.Pay13 = (decimal)dr["Pay13"];
            if (!string.IsNullOrEmpty(dr["Pay14"].ToString())) m.Pay14 = (decimal)dr["Pay14"];
            if (!string.IsNullOrEmpty(dr["Pay15"].ToString())) m.Pay15 = (decimal)dr["Pay15"];
            if (!string.IsNullOrEmpty(dr["Pay16"].ToString())) m.Pay16 = (decimal)dr["Pay16"];
            if (!string.IsNullOrEmpty(dr["Pay17"].ToString())) m.Pay17 = (decimal)dr["Pay17"];
            if (!string.IsNullOrEmpty(dr["Pay18"].ToString())) m.Pay18 = (decimal)dr["Pay18"];
            if (!string.IsNullOrEmpty(dr["Pay19"].ToString())) m.Pay19 = (decimal)dr["Pay19"];
            if (!string.IsNullOrEmpty(dr["Pay20"].ToString())) m.Pay20 = (decimal)dr["Pay20"];
            if (!string.IsNullOrEmpty(dr["Sum1"].ToString())) m.Sum1 = (decimal)dr["Sum1"];
            if (!string.IsNullOrEmpty(dr["Sum2"].ToString())) m.Sum2 = (decimal)dr["Sum2"];
            if (!string.IsNullOrEmpty(dr["Sum3"].ToString())) m.Sum3 = (decimal)dr["Sum3"];
            if (!string.IsNullOrEmpty(dr["Sum4"].ToString())) m.Sum4 = (decimal)dr["Sum4"];
            if (!string.IsNullOrEmpty(dr["Tax"].ToString())) m.Tax = (decimal)dr["Tax"];
            if (!string.IsNullOrEmpty(dr["CoPMFee"].ToString())) m.CoPMFee = (decimal)dr["CoPMFee"];
            if (!string.IsNullOrEmpty(dr["DIPMFee"].ToString())) m.DIPMFee = (decimal)dr["DIPMFee"];
            if (!string.IsNullOrEmpty(dr["PMFeeTotal"].ToString())) m.PMFeeTotal = (decimal)dr["PMFeeTotal"];


            return m;
        }

        /// <summary>
        /// 提交促销员工资申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int id, int staff, int taskid, int feetype)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,staff),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4,taskid),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4,feetype)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        /// <summary>
        /// 获取促销员工资单总额
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
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

        public SqlDataReader PM_Salary_GetActSalesVolume(int AccountMonth, int Promotor)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int,4,Promotor)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetActSalesVolume", prams, out dr);
            return dr;
        }

        public decimal PM_Salary_ComputeBonus(int Promotor, int Accountmonth, decimal SalesActual, decimal ActComplete)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),
                SQLDatabase.MakeInParam("@SalesActual", SqlDbType.Decimal, 10, SalesActual),
                SQLDatabase.MakeInParam("@Accountmonth", SqlDbType.Decimal, 10, Accountmonth),
                SQLDatabase.MakeInParam("@ActComplete", SqlDbType.Decimal, 10, ActComplete),
                new SqlParameter("@Bonus", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,2,"Bonus", DataRowVersion.Current,0)

			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_ComputeBonus", prams);
            return (decimal)prams[4].Value;

        }

        public void UpdateAdjustRecord(int ID, int Staff, string OldAdjustCost, string AdjustCost, string promotorName)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@OldAdjustCost", SqlDbType.VarChar, 20,OldAdjustCost),
                SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.VarChar,20, AdjustCost),
                SQLDatabase.MakeInParam("@PromotorName",SqlDbType.VarChar,50,promotorName)
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

        /// <summary>
        /// 根据员工获取工资状态
        /// </summary>
        /// <param name="promotor"></param>
        /// <param name="accountmonth"></param>
        /// <returns></returns>
        public int PM_Salary_GetStateByPromotor(int promotor, int accountmonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {               
                SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, promotor),           
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, accountmonth)                   
                                   };
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_GetStateByPromotor", prams);

        }

        public int Merge(int AccountMonth, string SalaryIDs, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {               
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),           
                SQLDatabase.MakeInParam("@SalaryIDs", SqlDbType.VarChar,2000 , SalaryIDs),   
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
                                   };
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_Merge", prams);
        }

        /// <summary>
        /// 汇总显示指定区域(包括子区域)所有工资单汇总信息         
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State">0:所有已提交及已批复 1：已提交待我审批 2：已批复</param>
        /// <param name="Staff"></param>
        /// <param name="SalaryClassify"></param>
        /// <returns></returns>
        public SqlDataReader GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),            
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@SalaryClassify", SqlDbType.Int, 4, SalaryClassify)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal", prams, out dr);

            return dr;
        }
        /// <summary>
        /// 统计工资合计项汇总信息
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="SalaryClassify"></param>
        /// <returns></returns>
        public SqlDataReader GetSummary(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),            
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@SalaryClassify", SqlDbType.Int, 4, SalaryClassify)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);

            return dr;
        }

        /// <summary>
        /// 获取PM_SalaryDetail实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public SqlDataReader GetSalaryDetailByID(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetDetail", prams, out dr);

            return dr;
        }

        public SqlDataReader GetDetailByState(int AccountMonth, int OrganizeCity, int Level, int State, int Staff, int SalaryClassify, int RTChannel)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),            
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@SalaryClassify", SqlDbType.Int, 4, SalaryClassify),
                SQLDatabase.MakeInParam("@RTChannel", SqlDbType.Int, 4, RTChannel)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetDetailByState", prams, out dr);

            return dr;
        }

        /// <summary>
        /// 导出工行和建行工资表所需数据
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="BankCode">银行代号 1：农行；2：建行</param>
        /// <returns></returns>
        public SqlDataReader PM_Salary_Export(int AccountMonth, int OrganizeCity, int BankCode)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@BankCode", SqlDbType.Int,4,BankCode)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_Export", prams, out dr);
            return dr;
        }
    }
}

