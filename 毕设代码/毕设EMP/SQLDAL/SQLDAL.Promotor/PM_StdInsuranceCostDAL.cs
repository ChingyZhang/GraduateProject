
// ===================================================================
// 文件： PM_StdInsuranceCostDAL.cs
// 项目名称：
// 创建时间：2011/10/21
// 作者:	   
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
	///PM_StdInsuranceCost数据访问DAL类
	/// </summary>
	public class PM_StdInsuranceCostDAL : BaseSimpleDAL<PM_StdInsuranceCost>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_StdInsuranceCostDAL()
		{
			_ProcePrefix = "MCS_Promotor.dbo.sp_PM_StdInsuranceCost";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_StdInsuranceCost m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@InsuranceMode", SqlDbType.Int, 4, m.InsuranceMode),
				SQLDatabase.MakeInParam("@CompanyCost", SqlDbType.Decimal, 9, m.CompanyCost),
				SQLDatabase.MakeInParam("@StaffCost", SqlDbType.Decimal, 9, m.StaffCost),
				SQLDatabase.MakeInParam("@ServiceCost", SqlDbType.Decimal, 9, m.ServiceCost),
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
        public override int Update(PM_StdInsuranceCost m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@InsuranceMode", SqlDbType.Int, 4, m.InsuranceMode),
				SQLDatabase.MakeInParam("@CompanyCost", SqlDbType.Decimal, 9, m.CompanyCost),
				SQLDatabase.MakeInParam("@StaffCost", SqlDbType.Decimal, 9, m.StaffCost),
				SQLDatabase.MakeInParam("@ServiceCost", SqlDbType.Decimal, 9, m.ServiceCost),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PM_StdInsuranceCost FillModel(IDataReader dr)
		{
			PM_StdInsuranceCost m = new PM_StdInsuranceCost();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["InsuranceMode"].ToString()))	m.InsuranceMode = (int)dr["InsuranceMode"];
			if (!string.IsNullOrEmpty(dr["CompanyCost"].ToString()))	m.CompanyCost = (decimal)dr["CompanyCost"];
			if (!string.IsNullOrEmpty(dr["StaffCost"].ToString()))	m.StaffCost = (decimal)dr["StaffCost"];
			if (!string.IsNullOrEmpty(dr["ServiceCost"].ToString()))	m.ServiceCost = (decimal)dr["ServiceCost"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

