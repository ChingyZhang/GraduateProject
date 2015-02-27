
// ===================================================================
// 文件： FNA_StaffSalary_ParamDAL.cs
// 项目名称：
// 创建时间：2014/2/18
// 作者:	   chf
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
	///FNA_StaffSalary_Param数据访问DAL类
	/// </summary>
	public class FNA_StaffSalary_ParamDAL : BaseSimpleDAL<FNA_StaffSalary_Param>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_StaffSalary_ParamDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffSalary_Param";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffSalary_Param m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@SalesRateULimit", SqlDbType.Decimal, 9, m.SalesRateULimit),
				SQLDatabase.MakeInParam("@SalesRateLLimit", SqlDbType.Decimal, 9, m.SalesRateLLimit),
				SQLDatabase.MakeInParam("@SalesRateWeight", SqlDbType.Decimal, 9, m.SalesRateWeight),
				SQLDatabase.MakeInParam("@SalesKeyLevel", SqlDbType.Decimal, 9, m.SalesKeyLevel),
				SQLDatabase.MakeInParam("@SalesKeyRateULevelULimit", SqlDbType.Decimal, 9, m.SalesKeyRateULevelULimit),
				SQLDatabase.MakeInParam("@SalesKeyRateLLevelULimit", SqlDbType.Decimal, 9, m.SalesKeyRateLLevelULimit),
				SQLDatabase.MakeInParam("@SalesKeyRateLLimit", SqlDbType.Decimal, 9, m.SalesKeyRateLLimit),
				SQLDatabase.MakeInParam("@SalesKeyRateWeight", SqlDbType.Decimal, 9, m.SalesKeyRateWeight),
				SQLDatabase.MakeInParam("@FeeRateULimit", SqlDbType.Decimal, 9, m.FeeRateULimit),
				SQLDatabase.MakeInParam("@FeeRateLLimit", SqlDbType.Decimal, 9, m.FeeRateLLimit),
				SQLDatabase.MakeInParam("@FeeRateWeight", SqlDbType.Decimal, 9, m.FeeRateWeight),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.Position =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.Position;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(FNA_StaffSalary_Param m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@SalesRateULimit", SqlDbType.Decimal, 9, m.SalesRateULimit),
				SQLDatabase.MakeInParam("@SalesRateLLimit", SqlDbType.Decimal, 9, m.SalesRateLLimit),
				SQLDatabase.MakeInParam("@SalesRateWeight", SqlDbType.Decimal, 9, m.SalesRateWeight),
				SQLDatabase.MakeInParam("@SalesKeyLevel", SqlDbType.Decimal, 9, m.SalesKeyLevel),
				SQLDatabase.MakeInParam("@SalesKeyRateULevelULimit", SqlDbType.Decimal, 9, m.SalesKeyRateULevelULimit),
				SQLDatabase.MakeInParam("@SalesKeyRateLLevelULimit", SqlDbType.Decimal, 9, m.SalesKeyRateLLevelULimit),
				SQLDatabase.MakeInParam("@SalesKeyRateLLimit", SqlDbType.Decimal, 9, m.SalesKeyRateLLimit),
				SQLDatabase.MakeInParam("@SalesKeyRateWeight", SqlDbType.Decimal, 9, m.SalesKeyRateWeight),
				SQLDatabase.MakeInParam("@FeeRateULimit", SqlDbType.Decimal, 9, m.FeeRateULimit),
				SQLDatabase.MakeInParam("@FeeRateLLimit", SqlDbType.Decimal, 9, m.FeeRateLLimit),
				SQLDatabase.MakeInParam("@FeeRateWeight", SqlDbType.Decimal, 9, m.FeeRateWeight),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Decimal, 9, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Decimal, 9, m.Data02),
				SQLDatabase.MakeInParam("@Data03", SqlDbType.Decimal, 9, m.Data03),
				SQLDatabase.MakeInParam("@Data04", SqlDbType.Decimal, 9, m.Data04),
				SQLDatabase.MakeInParam("@Data05", SqlDbType.Decimal, 9, m.Data05),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_StaffSalary_Param FillModel(IDataReader dr)
		{
			FNA_StaffSalary_Param m = new FNA_StaffSalary_Param();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Position"].ToString()))	m.Position = (int)dr["Position"];
			if (!string.IsNullOrEmpty(dr["SalesRateULimit"].ToString()))	m.SalesRateULimit = (decimal)dr["SalesRateULimit"];
			if (!string.IsNullOrEmpty(dr["SalesRateLLimit"].ToString()))	m.SalesRateLLimit = (decimal)dr["SalesRateLLimit"];
			if (!string.IsNullOrEmpty(dr["SalesRateWeight"].ToString()))	m.SalesRateWeight = (decimal)dr["SalesRateWeight"];
			if (!string.IsNullOrEmpty(dr["SalesKeyLevel"].ToString()))	m.SalesKeyLevel = (decimal)dr["SalesKeyLevel"];
			if (!string.IsNullOrEmpty(dr["SalesKeyRateULevelULimit"].ToString()))	m.SalesKeyRateULevelULimit = (decimal)dr["SalesKeyRateULevelULimit"];
			if (!string.IsNullOrEmpty(dr["SalesKeyRateLLevelULimit"].ToString()))	m.SalesKeyRateLLevelULimit = (decimal)dr["SalesKeyRateLLevelULimit"];
			if (!string.IsNullOrEmpty(dr["SalesKeyRateLLimit"].ToString()))	m.SalesKeyRateLLimit = (decimal)dr["SalesKeyRateLLimit"];
			if (!string.IsNullOrEmpty(dr["SalesKeyRateWeight"].ToString()))	m.SalesKeyRateWeight = (decimal)dr["SalesKeyRateWeight"];
			if (!string.IsNullOrEmpty(dr["FeeRateULimit"].ToString()))	m.FeeRateULimit = (decimal)dr["FeeRateULimit"];
			if (!string.IsNullOrEmpty(dr["FeeRateLLimit"].ToString()))	m.FeeRateLLimit = (decimal)dr["FeeRateLLimit"];
			if (!string.IsNullOrEmpty(dr["FeeRateWeight"].ToString()))	m.FeeRateWeight = (decimal)dr["FeeRateWeight"];
			if (!string.IsNullOrEmpty(dr["Data01"].ToString()))	m.Data01 = (decimal)dr["Data01"];
			if (!string.IsNullOrEmpty(dr["Data02"].ToString()))	m.Data02 = (decimal)dr["Data02"];
			if (!string.IsNullOrEmpty(dr["Data03"].ToString()))	m.Data03 = (decimal)dr["Data03"];
			if (!string.IsNullOrEmpty(dr["Data04"].ToString()))	m.Data04 = (decimal)dr["Data04"];
			if (!string.IsNullOrEmpty(dr["Data05"].ToString()))	m.Data05 = (decimal)dr["Data05"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

