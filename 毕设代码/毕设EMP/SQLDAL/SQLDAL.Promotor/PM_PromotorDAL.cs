
// ===================================================================
// 文件： PM_PromotorDAL.cs
// 项目名称：
// 创建时间：2008-12-30
// 作者:	   yangwei
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.Promotor
{
    /// <summary>
    ///PM_Promotor数据访问DAL类
    /// </summary>
    public class PM_PromotorDAL : BaseSimpleDAL<PM_Promotor>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_PromotorDAL()
        {
            _ProcePrefix = "MCS_Promotor.dbo.sp_PM_Promotor";
        }
        #endregion

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_Promotor m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Retailer", SqlDbType.Int, 4, m.Retailer),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@JobTitle", SqlDbType.Int, 4, m.JobTitle),
				SQLDatabase.MakeInParam("@JourneyWorker", SqlDbType.Char, 1, m.JourneyWorker),
				SQLDatabase.MakeInParam("@SalaryGrade", SqlDbType.Int, 4, m.SalaryGrade),
				SQLDatabase.MakeInParam("@MobileNumber", SqlDbType.VarChar, 50, m.MobileNumber),
				SQLDatabase.MakeInParam("@Dimission", SqlDbType.Int, 4, m.Dimission),
				SQLDatabase.MakeInParam("@BeginWorkDate", SqlDbType.DateTime, 8, m.BeginWorkDate),
				SQLDatabase.MakeInParam("@EndWorkDate", SqlDbType.DateTime, 8, m.EndWorkDate),
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
        public override int Update(PM_Promotor m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Retailer", SqlDbType.Int, 4, m.Retailer),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@JobTitle", SqlDbType.Int, 4, m.JobTitle),
				SQLDatabase.MakeInParam("@JourneyWorker", SqlDbType.Char, 1, m.JourneyWorker),
				SQLDatabase.MakeInParam("@SalaryGrade", SqlDbType.Int, 4, m.SalaryGrade),
				SQLDatabase.MakeInParam("@MobileNumber", SqlDbType.VarChar, 50, m.MobileNumber),
                SQLDatabase.MakeInParam("@Dimission", SqlDbType.Int, 4, m.Dimission),
				SQLDatabase.MakeInParam("@BeginWorkDate", SqlDbType.DateTime, 8, m.BeginWorkDate),
				SQLDatabase.MakeInParam("@EndWorkDate", SqlDbType.DateTime, 8, m.EndWorkDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PM_Promotor FillModel(IDataReader dr)
        {
            PM_Promotor m = new PM_Promotor();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Retailer"].ToString())) m.Retailer = (int)dr["Retailer"];
            if (!string.IsNullOrEmpty(dr["Sex"].ToString())) m.Sex = (int)dr["Sex"];
            if (!string.IsNullOrEmpty(dr["JobTitle"].ToString())) m.JobTitle = (int)dr["JobTitle"];
            if (!string.IsNullOrEmpty(dr["JourneyWorker"].ToString())) m.JourneyWorker = (string)dr["JourneyWorker"];
            if (!string.IsNullOrEmpty(dr["SalaryGrade"].ToString())) m.SalaryGrade = (int)dr["SalaryGrade"];
            if (!string.IsNullOrEmpty(dr["MobileNumber"].ToString())) m.MobileNumber = (string)dr["MobileNumber"];
            if (!string.IsNullOrEmpty(dr["Dimission"].ToString())) m.Dimission = (int)dr["Dimission"];
            if (!string.IsNullOrEmpty(dr["BeginWorkDate"].ToString())) m.BeginWorkDate = (DateTime)dr["BeginWorkDate"];
            if (!string.IsNullOrEmpty(dr["EndWorkDate"].ToString())) m.EndWorkDate = (DateTime)dr["EndWorkDate"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InputTime"].ToString())) m.InputTime = (DateTime)dr["InputTime"];
            if (!string.IsNullOrEmpty(dr["InputStaff"].ToString())) m.InputStaff = (int)dr["InputStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            return m;
        }

        /// <summary>
        /// 导购入职提交申请
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="TaskID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Submit(int ID, int TaskID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4,ID),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4,TaskID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,Staff)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);
        }

        public DataTable GetClientList(int Promotor)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4,Promotor)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetClientList", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }


        /// <summary>
        /// 根据导购员所在的门店，获取其底薪标准、保底标准、门店管理费
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public int GetStdPay(int Promotor, out decimal BasePay, out decimal MinimumWage, out decimal RTManageCost)
        {
            BasePay = 0;
            MinimumWage = 0;
            RTManageCost = 0;

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),
                new SqlParameter("@BasePay", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"BasePay", DataRowVersion.Current,0),
                new SqlParameter("@MinimumWage", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"MinimumWage", DataRowVersion.Current,0),
                new SqlParameter("@RTManageCost", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"RTManageCost", DataRowVersion.Current,0)
            };
            #endregion

            int iret = SQLDatabase.RunProc(_ProcePrefix + "_GetStdPay", prams);

            if (prams[1].Value != null && prams[1].Value != DBNull.Value) BasePay = (decimal)prams[1].Value;
            if (prams[2].Value != null && prams[2].Value != DBNull.Value) MinimumWage = (decimal)prams[2].Value;
            if (prams[3].Value != null && prams[3].Value != DBNull.Value) RTManageCost = (decimal)prams[3].Value;

            return iret;
        }

        public DataTable GetSalaryConsult(int Promotor, int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),      
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth)       
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSalaryConsult", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取待审批入职的导购列表
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="App"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetApproveList(int OrganizeCity, string AppCode, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				 
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),      
                SQLDatabase.MakeInParam("@AppCode", SqlDbType.VarChar, 100, AppCode ),  
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetApproveList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 在导购入职等审批流程中，获取指定区域的经营情况供审批参考
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public DataTable GetApproveConsult(int OrganizeCity, int level)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				 
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),      
                SQLDatabase.MakeInParam("@level", SqlDbType.Int, 4, level)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetApproveConsult", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        ///  根据经销商获取促销员资料及门店
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public DataTable GetByDIClient(int OrganizeCity, int DIClient, int Month)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),                       
                SQLDatabase.MakeInParam("@DIClient", SqlDbType.Int, 4, DIClient),      
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetByDIClient", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetAnalysisOverview(int OrganizeCity, int Month)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),              
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, Month)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_OPI.dbo.sp_OPI_Analysis_RT_GetOverview", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

