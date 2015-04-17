
// ===================================================================
// 文件： Rpt_ReportColumnGroupsDAL.cs
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
	///Rpt_ReportColumnGroups数据访问DAL类
	/// </summary>
	public class Rpt_ReportColumnGroupsDAL : BaseSimpleDAL<Rpt_ReportColumnGroups>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_ReportColumnGroupsDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_ReportColumnGroups";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_ReportColumnGroups m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@DataSetField", SqlDbType.UniqueIdentifier, 16, m.DataSetField),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@GroupSortID", SqlDbType.Int, 4, m.GroupSortID),
				SQLDatabase.MakeInParam("@AddSummary", SqlDbType.Char, 1, m.AddSummary),
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
        public override int Update(Rpt_ReportColumnGroups m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@DataSetField", SqlDbType.UniqueIdentifier, 16, m.DataSetField),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@GroupSortID", SqlDbType.Int, 4, m.GroupSortID),
				SQLDatabase.MakeInParam("@AddSummary", SqlDbType.Char, 1, m.AddSummary),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_ReportColumnGroups FillModel(IDataReader dr)
		{
			Rpt_ReportColumnGroups m = new Rpt_ReportColumnGroups();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Report"].ToString()))	m.Report = (Guid)dr["Report"];
			if (!string.IsNullOrEmpty(dr["DataSetField"].ToString()))	m.DataSetField = (Guid)dr["DataSetField"];
			if (!string.IsNullOrEmpty(dr["DisplayName"].ToString()))	m.DisplayName = (string)dr["DisplayName"];
			if (!string.IsNullOrEmpty(dr["GroupSortID"].ToString()))	m.GroupSortID = (int)dr["GroupSortID"];
			if (!string.IsNullOrEmpty(dr["AddSummary"].ToString()))	m.AddSummary = (string)dr["AddSummary"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

