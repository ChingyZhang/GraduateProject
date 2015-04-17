
// ===================================================================
// 文件： AC_BalanceUsageListDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.PBM;


namespace MCSFramework.SQLDAL.PBM
{
	/// <summary>
	///AC_BalanceUsageList数据访问DAL类
	/// </summary>
	public class AC_BalanceUsageListDAL : BaseSimpleDAL<AC_BalanceUsageList>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_BalanceUsageListDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_AC_BalanceUsageList";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_BalanceUsageList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Balance", SqlDbType.Decimal, 9, m.Balance),
				SQLDatabase.MakeInParam("@CashFlowId", SqlDbType.Int, 4, m.CashFlowId),
				SQLDatabase.MakeInParam("@DeliveryId", SqlDbType.Int, 4, m.DeliveryId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(AC_BalanceUsageList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Balance", SqlDbType.Decimal, 9, m.Balance),
				SQLDatabase.MakeInParam("@CashFlowId", SqlDbType.Int, 4, m.CashFlowId),
				SQLDatabase.MakeInParam("@DeliveryId", SqlDbType.Int, 4, m.DeliveryId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_BalanceUsageList FillModel(IDataReader dr)
		{
			AC_BalanceUsageList m = new AC_BalanceUsageList();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["TradeClient"].ToString()))	m.TradeClient = (int)dr["TradeClient"];
			if (!string.IsNullOrEmpty(dr["Amount"].ToString()))	m.Amount = (decimal)dr["Amount"];
			if (!string.IsNullOrEmpty(dr["Balance"].ToString()))	m.Balance = (decimal)dr["Balance"];
			if (!string.IsNullOrEmpty(dr["CashFlowId"].ToString()))	m.CashFlowId = (int)dr["CashFlowId"];
			if (!string.IsNullOrEmpty(dr["DeliveryId"].ToString()))	m.DeliveryId = (int)dr["DeliveryId"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 使用余额结应收款
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="TradeClient"></param>
        /// <param name="AgentStaff"></param>
        /// <param name="Amount"></param>
        /// <param name="Remark"></param>
        /// <param name="ARIDs"></param>
        /// <returns></returns>
        public int BalanceAR(int OwnerClient, int TradeClient, int AgentStaff, decimal Amount, string Remark, string ARIDs)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, TradeClient),
				SQLDatabase.MakeInParam("@AgentStaff", SqlDbType.Int, 4, AgentStaff),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, Remark),
                SQLDatabase.MakeInParam("@ARIDs", SqlDbType.VarChar, ARIDs.Length, ARIDs)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_BalanceAR", prams);
        }

        /// <summary>
        /// 使用余额结应付款
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="TradeClient"></param>
        /// <param name="AgentStaff"></param>
        /// <param name="Amount"></param>
        /// <param name="Remark"></param>
        /// <param name="APIDs"></param>
        /// <returns></returns>
        public int BalanceAP(int OwnerClient, int TradeClient, int AgentStaff, decimal Amount, string Remark, string APIDs)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, TradeClient),
				SQLDatabase.MakeInParam("@AgentStaff", SqlDbType.Int, 4, AgentStaff),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, Remark),
                SQLDatabase.MakeInParam("@APIDs", SqlDbType.VarChar, APIDs.Length, APIDs)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_BalanceAP", prams);
        }
    }
}

