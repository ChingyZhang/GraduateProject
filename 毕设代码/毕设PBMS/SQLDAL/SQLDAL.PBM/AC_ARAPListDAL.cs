
// ===================================================================
// 文件： AC_ARAPListDAL.cs
// 项目名称：
// 创建时间：2015-01-27
// 作者:	   Shen Gang
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
	///AC_ARAPList数据访问DAL类
	/// </summary>
	public class AC_ARAPListDAL : BaseSimpleDAL<AC_ARAPList>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_ARAPListDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_AC_ARAPList";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_ARAPList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, m.OpStaff),
				SQLDatabase.MakeInParam("@RelateDeliveryId", SqlDbType.Int, 4, m.RelateDeliveryId),
				SQLDatabase.MakeInParam("@BalanceFlag", SqlDbType.Int, 4, m.BalanceFlag),
				SQLDatabase.MakeInParam("@BalanceDate", SqlDbType.DateTime, 8, m.BalanceDate),
				SQLDatabase.MakeInParam("@CashFlowId", SqlDbType.Int, 4, m.CashFlowId),
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
        public override int Update(AC_ARAPList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, m.OpStaff),
				SQLDatabase.MakeInParam("@RelateDeliveryId", SqlDbType.Int, 4, m.RelateDeliveryId),
				SQLDatabase.MakeInParam("@BalanceFlag", SqlDbType.Int, 4, m.BalanceFlag),
				SQLDatabase.MakeInParam("@BalanceDate", SqlDbType.DateTime, 8, m.BalanceDate),
				SQLDatabase.MakeInParam("@CashFlowId", SqlDbType.Int, 4, m.CashFlowId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_ARAPList FillModel(IDataReader dr)
		{
			AC_ARAPList m = new AC_ARAPList();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["TradeClient"].ToString()))	m.TradeClient = (int)dr["TradeClient"];
			if (!string.IsNullOrEmpty(dr["Type"].ToString()))	m.Type = (int)dr["Type"];
			if (!string.IsNullOrEmpty(dr["Amount"].ToString()))	m.Amount = (decimal)dr["Amount"];
			if (!string.IsNullOrEmpty(dr["OpStaff"].ToString()))	m.OpStaff = (int)dr["OpStaff"];
			if (!string.IsNullOrEmpty(dr["RelateDeliveryId"].ToString()))	m.RelateDeliveryId = (int)dr["RelateDeliveryId"];
			if (!string.IsNullOrEmpty(dr["BalanceFlag"].ToString()))	m.BalanceFlag = (int)dr["BalanceFlag"];
			if (!string.IsNullOrEmpty(dr["BalanceDate"].ToString()))	m.BalanceDate = (DateTime)dr["BalanceDate"];
			if (!string.IsNullOrEmpty(dr["CashFlowId"].ToString()))	m.CashFlowId = (int)dr["CashFlowId"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

