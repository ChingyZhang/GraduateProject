
// ===================================================================
// 文件： FNA_AmountReceivableDAL.cs
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
	///FNA_AmountReceivable数据访问DAL类
	/// </summary>
	public class FNA_AmountReceivableDAL : BaseSimpleDAL<FNA_AmountReceivable>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_AmountReceivableDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_AmountReceivable";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_AmountReceivable m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(FNA_AmountReceivable m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_AmountReceivable FillModel(IDataReader dr)
		{
			FNA_AmountReceivable m = new FNA_AmountReceivable();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString()))	m.AccountMonth = (int)dr["AccountMonth"];
			if (!string.IsNullOrEmpty(dr["Amount"].ToString()))	m.Amount = (decimal)dr["Amount"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 变更应收账款
        /// </summary>
        /// <param name="client"></param>
        /// <param name="changeamount"></param>
        /// <param name="changetype"></param>
        /// <param name="changestaff"></param>
        /// <param name="relateinfo"></param>
        /// <returns></returns>
        public int Change(int client,decimal changeamount,int changetype,int changestaff,string relateinfo)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, client),
                SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, changeamount),
                SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, changetype),
                SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, changestaff),
                SQLDatabase.MakeInParam("@RelatedInfo", SqlDbType.VarChar, 1000, relateinfo),
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_Change", prams);
        }
        /// <summary>
        /// 返回指定客户当前应收账款余额
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public decimal GetAmountByClient(int client)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, client),
                new SqlParameter("@Amount", SqlDbType.Decimal, 9, ParameterDirection.Output,false, 18, 3,"Amount", DataRowVersion.Current,0)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetAmountByClient", prams);

            return (decimal)prams[1].Value;
        }
    }
}

