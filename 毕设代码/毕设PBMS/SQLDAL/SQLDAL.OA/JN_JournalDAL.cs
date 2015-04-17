
// ===================================================================
// 文件： JN_JournalDAL.cs
// 项目名称：
// 创建时间：2010/6/19
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;


namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///JN_Journal数据访问DAL类
	/// </summary>
	public class JN_JournalDAL : BaseSimpleDAL<JN_Journal>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public JN_JournalDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_JN_Journal";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(JN_Journal m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@JournalType", SqlDbType.Int, 4, m.JournalType),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateLinkMan", SqlDbType.Int, 4, m.RelateLinkMan),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
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
        public override int Update(JN_Journal m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@JournalType", SqlDbType.Int, 4, m.JournalType),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateLinkMan", SqlDbType.Int, 4, m.RelateLinkMan),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override JN_Journal FillModel(IDataReader dr)
		{
			JN_Journal m = new JN_Journal();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Title"].ToString()))	m.Title = (string)dr["Title"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["JournalType"].ToString()))	m.JournalType = (int)dr["JournalType"];
			if (!string.IsNullOrEmpty(dr["Staff"].ToString()))	m.Staff = (int)dr["Staff"];
			if (!string.IsNullOrEmpty(dr["BeginTime"].ToString()))	m.BeginTime = (DateTime)dr["BeginTime"];
			if (!string.IsNullOrEmpty(dr["EndTime"].ToString()))	m.EndTime = (DateTime)dr["EndTime"];
			if (!string.IsNullOrEmpty(dr["WorkingClassify"].ToString()))	m.WorkingClassify = (int)dr["WorkingClassify"];
			if (!string.IsNullOrEmpty(dr["RelateClient"].ToString()))	m.RelateClient = (int)dr["RelateClient"];
			if (!string.IsNullOrEmpty(dr["RelateLinkMan"].ToString()))	m.RelateLinkMan = (int)dr["RelateLinkMan"];
			if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString()))	m.OfficialCity = (int)dr["OfficialCity"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

