
// ===================================================================
// 文件： AC_CurrentAccountDAL.cs
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
	///AC_CurrentAccount数据访问DAL类
	/// </summary>
	public class AC_CurrentAccountDAL : BaseSimpleDAL<AC_CurrentAccount>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_CurrentAccountDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_AC_CurrentAccount";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_CurrentAccount m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@PrePaymentAmount", SqlDbType.Decimal, 9, m.PrePaymentAmount),
				SQLDatabase.MakeInParam("@AP", SqlDbType.Decimal, 9, m.AP),
				SQLDatabase.MakeInParam("@PrePaymentBalance", SqlDbType.Decimal, 9, m.PrePaymentBalance),
				SQLDatabase.MakeInParam("@PreReceivedAmount", SqlDbType.Decimal, 9, m.PreReceivedAmount),
				SQLDatabase.MakeInParam("@AR", SqlDbType.Decimal, 9, m.AR),
				SQLDatabase.MakeInParam("@PreReceivedBalance", SqlDbType.Decimal, 9, m.PreReceivedBalance),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
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
        public override int Update(AC_CurrentAccount m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@PrePaymentAmount", SqlDbType.Decimal, 9, m.PrePaymentAmount),
				SQLDatabase.MakeInParam("@AP", SqlDbType.Decimal, 9, m.AP),
				SQLDatabase.MakeInParam("@PrePaymentBalance", SqlDbType.Decimal, 9, m.PrePaymentBalance),
				SQLDatabase.MakeInParam("@PreReceivedAmount", SqlDbType.Decimal, 9, m.PreReceivedAmount),
				SQLDatabase.MakeInParam("@AR", SqlDbType.Decimal, 9, m.AR),
				SQLDatabase.MakeInParam("@PreReceivedBalance", SqlDbType.Decimal, 9, m.PreReceivedBalance),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_CurrentAccount FillModel(IDataReader dr)
		{
			AC_CurrentAccount m = new AC_CurrentAccount();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["TradeClient"].ToString()))	m.TradeClient = (int)dr["TradeClient"];
			if (!string.IsNullOrEmpty(dr["PrePaymentAmount"].ToString()))	m.PrePaymentAmount = (decimal)dr["PrePaymentAmount"];
			if (!string.IsNullOrEmpty(dr["AP"].ToString()))	m.AP = (decimal)dr["AP"];
			if (!string.IsNullOrEmpty(dr["PrePaymentBalance"].ToString()))	m.PrePaymentBalance = (decimal)dr["PrePaymentBalance"];
			if (!string.IsNullOrEmpty(dr["PreReceivedAmount"].ToString()))	m.PreReceivedAmount = (decimal)dr["PreReceivedAmount"];
			if (!string.IsNullOrEmpty(dr["AR"].ToString()))	m.AR = (decimal)dr["AR"];
			if (!string.IsNullOrEmpty(dr["PreReceivedBalance"].ToString()))	m.PreReceivedBalance = (decimal)dr["PreReceivedBalance"];
			if (!string.IsNullOrEmpty(dr["LastUpdateTime"].ToString()))	m.LastUpdateTime = (DateTime)dr["LastUpdateTime"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

