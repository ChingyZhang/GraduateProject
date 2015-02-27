
// ===================================================================
// 文件： CM_ContractDAL.cs
// 项目名称：
// 创建时间：
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.CM
{
    /// <summary>
    ///CM_Contract数据访问DAL类
    /// </summary>
    public class CM_ContractDAL : BaseComplexDAL<CM_Contract, CM_ContractDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_ContractDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_Contract";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_Contract m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@ContractCode", SqlDbType.VarChar, 50, m.ContractCode),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@SignMan", SqlDbType.VarChar, 50, m.SignMan),
				SQLDatabase.MakeInParam("@SignDate", SqlDbType.DateTime, 8, m.SignDate),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
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
        public override int Update(CM_Contract m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@ContractCode", SqlDbType.VarChar, 50, m.ContractCode),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@SignMan", SqlDbType.VarChar, 50, m.SignMan),
				SQLDatabase.MakeInParam("@SignDate", SqlDbType.DateTime, 8, m.SignDate),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_Contract FillModel(IDataReader dr)
        {
            CM_Contract m = new CM_Contract();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["ContractCode"].ToString())) m.ContractCode = (string)dr["ContractCode"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["SignMan"].ToString())) m.SignMan = (string)dr["SignMan"];
            if (!string.IsNullOrEmpty(dr["SignDate"].ToString())) m.SignDate = (DateTime)dr["SignDate"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(CM_ContractDetail m)
        {
            m.ContractID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ContractID", SqlDbType.Int, 4, m.ContractID),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@FeeCycle", SqlDbType.Int, 4, m.FeeCycle),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@BearMode", SqlDbType.Int, 4, m.BearMode),
                SQLDatabase.MakeInParam("@ApplyLimit", SqlDbType.Int, 4, m.ApplyLimit),
				SQLDatabase.MakeInParam("@BearPercent", SqlDbType.Decimal, 9, m.BearPercent),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(CM_ContractDetail m)
        {
            m.ContractID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ContractID", SqlDbType.Int, 4, m.ContractID),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
                SQLDatabase.MakeInParam("@ApplyLimit", SqlDbType.Int, 4, m.ApplyLimit),
				SQLDatabase.MakeInParam("@FeeCycle", SqlDbType.Int, 4, m.FeeCycle),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@BearMode", SqlDbType.Int, 4, m.BearMode),
				SQLDatabase.MakeInParam("@BearPercent", SqlDbType.Decimal, 9, m.BearPercent),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        public int Approve(int ID, int State, int ApproveStaff)
        {
            SqlParameter[] prams = { 
               SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID) ,
               SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State) ,
               SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, ApproveStaff) 
           };
            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        public int Disable(int ID, int Staff)
        {
            SqlParameter[] prams = { 
               SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID) ,
               SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff) 
           };
            return SQLDatabase.RunProc(_ProcePrefix + "_Disable", prams);
        }

        protected override CM_ContractDetail FillDetailModel(IDataReader dr)
        {
            CM_ContractDetail m = new CM_ContractDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ContractID"].ToString())) m.ContractID = (int)dr["ContractID"];
            if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString())) m.AccountTitle = (int)dr["AccountTitle"];
            if (!string.IsNullOrEmpty(dr["Amount"].ToString())) m.Amount = (decimal)dr["Amount"];
            if (!string.IsNullOrEmpty(dr["ApplyLimit"].ToString())) m.ApplyLimit = (decimal)dr["ApplyLimit"];
            if (!string.IsNullOrEmpty(dr["FeeCycle"].ToString())) m.FeeCycle = (int)dr["FeeCycle"];
            if (!string.IsNullOrEmpty(dr["PayMode"].ToString())) m.PayMode = (int)dr["PayMode"];
            if (!string.IsNullOrEmpty(dr["BearMode"].ToString())) m.BearMode = (int)dr["BearMode"];
            if (!string.IsNullOrEmpty(dr["BearPercent"].ToString())) m.BearPercent = (decimal)dr["BearPercent"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }
        /// <summary>
        /// 获取柜台合同甲方
        /// </summary>
        /// <param name="ClinetID"></param>
        /// <returns></returns>
        public SqlDataReader GetOwners(int ClinetID)
        {
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClinetID) };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetOwners", prams, out dr);
            return dr;
        }
        /// <summary>
        /// 获取丙方
        /// </summary>
        /// <param name="ClinetID"></param>
        /// <returns></returns>
        public SqlDataReader GetPartyC(int ClinetID)
        {
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClinetID) };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPartyC", prams, out dr);
            return dr;
        }

        /// <summary>
        /// 将合同陈列协议批量生成费用申请单
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="IsNKA"></param>
        /// <returns></returns>
        public int CreateCLFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, bool IsNKA, int FeeType)
        {
            SqlParameter[] prams = 
            {   SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@DIClient", SqlDbType.Int, 4, DIClient),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@IsNKA", SqlDbType.Int, 4, IsNKA ? 1 : 2 ),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType )
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_CreateCLFeeApply", prams, 900);
        }

        /// <summary>
        /// 批量申请门店返利费用
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public int CreateFLFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, int FeeType)
        {
            SqlParameter[] prams = 
            {   SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@DIClient", SqlDbType.Int, 4, DIClient),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType )
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_CreateFLFeeApply", prams, 900);
        }

        /// <summary>
        /// 批量申请导购管理费用
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public int CreatePMFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, int FeeType)
        {
            SqlParameter[] prams = 
            {   SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@DIClient", SqlDbType.Int, 4, DIClient),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType )
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_CreatePMFeeApply", prams, 900);
        }
        /// <summary>
        /// 获取批量审批数据
        /// </summary>
        /// <param name="OrganizeCity">片区</param>
        /// <param name="State">状态：0所有 1待我审批 2我已审批</param>
        /// <param name="Staff">审批人</param>
        /// <param name="Classify">合同类型</param>
        /// <returns></returns>
        public SqlDataReader GetApproveSummary(int OrganizeCity, int State, int Staff, int Classify,int PayMode,string ExtCondition)
        {
            SqlParameter[] prams = 
            {   SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, PayMode),
                SQLDatabase.MakeInParam("@ExtCondition", SqlDbType.VarChar, 1000, ExtCondition)
            };
            string _Endfix = "";
            switch (Classify)
            {
                case 2:
                    _Endfix = "_FL";
                    break;
                case 3:
                default:
                    _Endfix = "_PM";
                    break;
            }
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetApproveSummary" + _Endfix, prams, out dr);
            return dr;
        }
        /// <summary>
        /// 获取导购合同申请的最大月份
        /// </summary>
        /// <param name="ContractID">合同ID</param>
        /// <returns></returns>
        public int CheckPMFeeApplyLastMonth(int ContractID,int ClientID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ContractID),
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClientID),
            };
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_CheckPMFeeApplyLastMonth", prams, 2000);
        }
        /// <summary>
        /// 获取导购协议汇总
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public DataTable GetPMSummary(int OrganizeCity, int Level, int State,  int StaffID, int RTChannel)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID),
                SQLDatabase.MakeInParam("@RTClassify", SqlDbType.Int, 4, RTChannel)
            };
            #endregion
            SqlDataReader sdr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPMSummary", prams, out sdr);
            return Tools.ConvertDataReaderToDataTable(sdr);
        }
        /// <summary>
        /// 获取导购协议列表
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public DataTable GetPMList(int OrganizeCity, int Level, int State, int StaffID, int RTChannel)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID),
                SQLDatabase.MakeInParam("@RTClassify", SqlDbType.Int, 4, RTChannel)
            };
            #endregion
            SqlDataReader sdr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPMList", prams, out sdr);
            return Tools.ConvertDataReaderToDataTable(sdr);
        }
        /// <summary>
        /// 获取导购协议列表详细信息
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public DataTable GetPMListDetail(int OrganizeCity, int Level, int State, int StaffID, int RTChannel)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID),
                SQLDatabase.MakeInParam("@RTClassify", SqlDbType.Int, 4, RTChannel)
            };
            #endregion
            SqlDataReader sdr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPMListDetail", prams, out sdr);
            return Tools.ConvertDataReaderToDataTable(sdr);
        }
    }
}

