
// ===================================================================
// 文件： Rpt_ReportValueGroupsDAL.cs
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
	///Rpt_ReportValueGroups数据访问DAL类
	/// </summary>
	public class Rpt_ReportValueGroupsDAL : BaseSimpleDAL<Rpt_ReportValueGroups>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_ReportValueGroupsDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_ReportValueGroups";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_ReportValueGroups m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@DataSetField", SqlDbType.UniqueIdentifier, 16, m.DataSetField),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@ValueSortID", SqlDbType.Int, 4, m.ValueSortID),
				SQLDatabase.MakeInParam("@StatisticMode", SqlDbType.Int, 4, m.StatisticMode),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            return  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_ReportValueGroups m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@DataSetField", SqlDbType.UniqueIdentifier, 16, m.DataSetField),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@ValueSortID", SqlDbType.Int, 4, m.ValueSortID),
				SQLDatabase.MakeInParam("@StatisticMode", SqlDbType.Int, 4, m.StatisticMode),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_ReportValueGroups FillModel(IDataReader dr)
		{
			Rpt_ReportValueGroups m = new Rpt_ReportValueGroups();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Report"].ToString()))	m.Report = (Guid)dr["Report"];
			if (!string.IsNullOrEmpty(dr["DataSetField"].ToString()))	m.DataSetField = (Guid)dr["DataSetField"];
			if (!string.IsNullOrEmpty(dr["DisplayName"].ToString()))	m.DisplayName = (string)dr["DisplayName"];
			if (!string.IsNullOrEmpty(dr["ValueSortID"].ToString()))	m.ValueSortID = (int)dr["ValueSortID"];
			if (!string.IsNullOrEmpty(dr["StatisticMode"].ToString()))	m.StatisticMode = (int)dr["StatisticMode"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

