
// ===================================================================
// 文件： AC_CashAccountDAL.cs
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
	///AC_CashAccount数据访问DAL类
	/// </summary>
	public class AC_CashAccountDAL : BaseSimpleDAL<AC_CashAccount>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_CashAccountDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_AC_CashAccount";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_CashAccount m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@CashAccountType", SqlDbType.Int, 4, m.CashAccountType),
				SQLDatabase.MakeInParam("@BankName", SqlDbType.VarChar, 100, m.BankName),
				SQLDatabase.MakeInParam("@BankAccount", SqlDbType.VarChar, 100, m.BankAccount),
				SQLDatabase.MakeInParam("@BankAccountName", SqlDbType.VarChar, 100, m.BankAccountName),
				SQLDatabase.MakeInParam("@AccountBalance", SqlDbType.Decimal, 9, m.AccountBalance),
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
        public override int Update(AC_CashAccount m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@CashAccountType", SqlDbType.Int, 4, m.CashAccountType),
				SQLDatabase.MakeInParam("@BankName", SqlDbType.VarChar, 100, m.BankName),
				SQLDatabase.MakeInParam("@BankAccount", SqlDbType.VarChar, 100, m.BankAccount),
				SQLDatabase.MakeInParam("@BankAccountName", SqlDbType.VarChar, 100, m.BankAccountName),
				SQLDatabase.MakeInParam("@AccountBalance", SqlDbType.Decimal, 9, m.AccountBalance),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_CashAccount FillModel(IDataReader dr)
		{
			AC_CashAccount m = new AC_CashAccount();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["CashAccountType"].ToString()))	m.CashAccountType = (int)dr["CashAccountType"];
			if (!string.IsNullOrEmpty(dr["BankName"].ToString()))	m.BankName = (string)dr["BankName"];
			if (!string.IsNullOrEmpty(dr["BankAccount"].ToString()))	m.BankAccount = (string)dr["BankAccount"];
			if (!string.IsNullOrEmpty(dr["BankAccountName"].ToString()))	m.BankAccountName = (string)dr["BankAccountName"];
			if (!string.IsNullOrEmpty(dr["AccountBalance"].ToString()))	m.AccountBalance = (decimal)dr["AccountBalance"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

