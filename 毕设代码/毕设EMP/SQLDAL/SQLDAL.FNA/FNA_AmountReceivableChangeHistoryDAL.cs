
// ===================================================================
// 文件： FNA_AmountReceivableChangeHistoryDAL.cs
// 项目名称：
// 创建时间：2009/5/16
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
	///FNA_AmountReceivableChangeHistory数据访问DAL类
	/// </summary>
	public class FNA_AmountReceivableChangeHistoryDAL : BaseSimpleDAL<FNA_AmountReceivableChangeHistory>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_AmountReceivableChangeHistoryDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_AmountReceivableChangeHistory";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_AmountReceivableChangeHistory m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, m.ChangeAmount),
				SQLDatabase.MakeInParam("@BalanceAmount", SqlDbType.Decimal, 9, m.BalanceAmount),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@DebitCreditFlag", SqlDbType.Int, 4, m.DebitCreditFlag),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@RelatedInfo", SqlDbType.VarChar, 1000, m.RelatedInfo)
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
        public override int Update(FNA_AmountReceivableChangeHistory m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, m.ChangeAmount),
				SQLDatabase.MakeInParam("@BalanceAmount", SqlDbType.Decimal, 9, m.BalanceAmount),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@DebitCreditFlag", SqlDbType.Int, 4, m.DebitCreditFlag),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@RelatedInfo", SqlDbType.VarChar, 1000, m.RelatedInfo)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_AmountReceivableChangeHistory FillModel(IDataReader dr)
		{
			FNA_AmountReceivableChangeHistory m = new FNA_AmountReceivableChangeHistory();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString()))	m.AccountMonth = (int)dr["AccountMonth"];
			if (!string.IsNullOrEmpty(dr["ChangeAmount"].ToString()))	m.ChangeAmount = (decimal)dr["ChangeAmount"];
			if (!string.IsNullOrEmpty(dr["BalanceAmount"].ToString()))	m.BalanceAmount = (decimal)dr["BalanceAmount"];
			if (!string.IsNullOrEmpty(dr["ChangeType"].ToString()))	m.ChangeType = (int)dr["ChangeType"];
			if (!string.IsNullOrEmpty(dr["DebitCreditFlag"].ToString()))	m.DebitCreditFlag = (int)dr["DebitCreditFlag"];
			if (!string.IsNullOrEmpty(dr["ChangeStaff"].ToString()))	m.ChangeStaff = (int)dr["ChangeStaff"];
			if (!string.IsNullOrEmpty(dr["ChangeTime"].ToString()))	m.ChangeTime = (DateTime)dr["ChangeTime"];
			if (!string.IsNullOrEmpty(dr["RelatedInfo"].ToString()))	m.RelatedInfo = (string)dr["RelatedInfo"];
						
			return m;
		}

        
    }
}

