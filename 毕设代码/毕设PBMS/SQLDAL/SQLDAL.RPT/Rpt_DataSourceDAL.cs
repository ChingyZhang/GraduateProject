
// ===================================================================
// 文件： Rpt_DataSourceDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;


namespace MCSFramework.SQLDAL.RPT
{
	/// <summary>
	///Rpt_DataSource数据访问DAL类
	/// </summary>
	public class Rpt_DataSourceDAL : BaseSimpleDAL<Rpt_DataSource>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSourceDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSource";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSource m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@ProviderName", SqlDbType.VarChar, 200, m.ProviderName),
				SQLDatabase.MakeInParam("@ConnectionString", SqlDbType.VarChar, 500, m.ConnectionString),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_DataSource m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@ProviderName", SqlDbType.VarChar, 200, m.ProviderName),
				SQLDatabase.MakeInParam("@ConnectionString", SqlDbType.VarChar, 500, m.ConnectionString),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_DataSource FillModel(IDataReader dr)
		{
			Rpt_DataSource m = new Rpt_DataSource();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["ProviderName"].ToString()))	m.ProviderName = (string)dr["ProviderName"];
			if (!string.IsNullOrEmpty(dr["ConnectionString"].ToString()))	m.ConnectionString = (string)dr["ConnectionString"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

