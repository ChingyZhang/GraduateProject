
// ===================================================================
// 文件： PM_StdBasePayDAL.cs
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
	///PM_StdBasePay数据访问DAL类
	/// </summary>
	public class PM_StdBasePayDAL : BaseSimpleDAL<PM_StdBasePay>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_StdBasePayDAL()
		{
			_ProcePrefix = "MCS_Promotor.dbo.sp_PM_StdBasePay";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_StdBasePay m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@City", SqlDbType.Int, 4, m.City),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@MinBasePay", SqlDbType.Decimal, 9, m.MinBasePay),
				SQLDatabase.MakeInParam("@MaxBasePay", SqlDbType.Decimal, 9, m.MaxBasePay),
				SQLDatabase.MakeInParam("@MinimumWage", SqlDbType.Decimal, 9, m.MinimumWage),
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
        public override int Update(PM_StdBasePay m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@City", SqlDbType.Int, 4, m.City),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@MinBasePay", SqlDbType.Decimal, 9, m.MinBasePay),
				SQLDatabase.MakeInParam("@MaxBasePay", SqlDbType.Decimal, 9, m.MaxBasePay),
				SQLDatabase.MakeInParam("@MinimumWage", SqlDbType.Decimal, 9, m.MinimumWage),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PM_StdBasePay FillModel(IDataReader dr)
		{
			PM_StdBasePay m = new PM_StdBasePay();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["City"].ToString()))	m.City = (int)dr["City"];
			if (!string.IsNullOrEmpty(dr["Classify"].ToString()))	m.Classify = (int)dr["Classify"];
			if (!string.IsNullOrEmpty(dr["MinBasePay"].ToString()))	m.MinBasePay = (decimal)dr["MinBasePay"];
			if (!string.IsNullOrEmpty(dr["MaxBasePay"].ToString()))	m.MaxBasePay = (decimal)dr["MaxBasePay"];
			if (!string.IsNullOrEmpty(dr["MinimumWage"].ToString()))	m.MinimumWage = (decimal)dr["MinimumWage"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 获取指定促销员所在门店的底薪标准
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public decimal GetBasePayByPromotor(int Promotor)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),
                new SqlParameter("@BasePay", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"BasePay", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetBasePayByPromotor", prams);

            return (decimal)prams[1].Value;
        }

        /// <summary>
        /// 获取指定促销员所在门店的保底标准
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public decimal GetMinimumWageByPromotor(int Promotor)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, Promotor),
                new SqlParameter("@MinimumWage", SqlDbType.Decimal,18, ParameterDirection.Output,false,10,3,"MinimumWage", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetMinimumWageByPromotor", prams);

            return (decimal)prams[1].Value;
        }
    }
}

