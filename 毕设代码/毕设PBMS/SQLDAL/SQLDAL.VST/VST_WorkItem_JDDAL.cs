
// ===================================================================
// 文件： VST_WorkItem_JDBLL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.VST;


namespace MCSFramework.SQLDAL.VST
{
	/// <summary>
	///VST_WorkItem_JD数据访问DAL类
	/// </summary>
	public class VST_WorkItem_JDDAL : BaseSimpleDAL<VST_WorkItem_JD>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_WorkItem_JDDAL()
		{
			_ProcePrefix = "MCS_VST.dbo.sp_VST_WorkItem_JD";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_WorkItem_JD m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Job", SqlDbType.Int, 4, m.Job),
				SQLDatabase.MakeInParam("@JobType", SqlDbType.Int, 4, m.JobType),
				SQLDatabase.MakeInParam("@JudgeMode", SqlDbType.Int, 4, m.JudgeMode),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Float, 8, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Float, 8, m.Latitude),
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
        public override int Update(VST_WorkItem_JD m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Job", SqlDbType.Int, 4, m.Job),
				SQLDatabase.MakeInParam("@JobType", SqlDbType.Int, 4, m.JobType),
				SQLDatabase.MakeInParam("@JudgeMode", SqlDbType.Int, 4, m.JudgeMode),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Float, 8, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Float, 8, m.Latitude),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override VST_WorkItem_JD FillModel(IDataReader dr)
		{
			VST_WorkItem_JD m = new VST_WorkItem_JD();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Job"].ToString()))	m.Job = (int)dr["Job"];
			if (!string.IsNullOrEmpty(dr["JobType"].ToString()))	m.JobType = (int)dr["JobType"];
			if (!string.IsNullOrEmpty(dr["JudgeMode"].ToString()))	m.JudgeMode = (int)dr["JudgeMode"];
			if (!string.IsNullOrEmpty(dr["Longitude"].ToString()))	m.Longitude = (double)dr["Longitude"];
			if (!string.IsNullOrEmpty(dr["Latitude"].ToString()))	m.Latitude = (double)dr["Latitude"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

