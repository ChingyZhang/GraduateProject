
// ===================================================================
// 文件： FNA_ClientPaymentForcastDAL.cs
// 项目名称：
// 创建时间：2011/4/15
// 作者:	   
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
	///FNA_ClientPaymentForcast数据访问DAL类
	/// </summary>
	public class FNA_ClientPaymentForcastDAL : BaseSimpleDAL<FNA_ClientPaymentForcast>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_ClientPaymentForcastDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_ClientPaymentForcast";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_ClientPaymentForcast m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@PayAmount", SqlDbType.Decimal, 9, m.PayAmount),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(FNA_ClientPaymentForcast m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@PayAmount", SqlDbType.Decimal, 9, m.PayAmount),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_ClientPaymentForcast FillModel(IDataReader dr)
		{
			FNA_ClientPaymentForcast m = new FNA_ClientPaymentForcast();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["PayDate"].ToString()))	m.PayDate = (DateTime)dr["PayDate"];
			if (!string.IsNullOrEmpty(dr["PayAmount"].ToString()))	m.PayAmount = (decimal)dr["PayAmount"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        public void Init(int OrganizeCity, int Client)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                	SQLDatabase.MakeInParam("@Client", SqlDbType.Int,4,Client)                      
                                        };
            SQLDatabase.RunProc(_ProcePrefix + "_Init", parameters);
        }
    }
}

