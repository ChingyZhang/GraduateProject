
// ===================================================================
// 文件： JN_JournalCommentDAL.cs
// 项目名称：
// 创建时间：2009-4-25
// 作者:	   shh
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
	///JN_JournalComment数据访问DAL类
	/// </summary>
	public class JN_JournalCommentDAL : BaseSimpleDAL<JN_JournalComment>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public JN_JournalCommentDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_JN_JournalComment";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(JN_JournalComment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@JournalID", SqlDbType.Int, 4, m.JournalID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 2000, m.Content),
				SQLDatabase.MakeInParam("@CommentTime", SqlDbType.DateTime, 8, m.CommentTime),
				SQLDatabase.MakeInParam("@ExtProperty", SqlDbType.VarChar, 8000, m.ExtProperty)
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
        public override int Update(JN_JournalComment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@JournalID", SqlDbType.Int, 4, m.JournalID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 2000, m.Content),
				SQLDatabase.MakeInParam("@CommentTime", SqlDbType.DateTime, 8, m.CommentTime),
				SQLDatabase.MakeInParam("@ExtProperty", SqlDbType.VarChar, 8000, m.ExtProperty)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override JN_JournalComment FillModel(IDataReader dr)
		{
			JN_JournalComment m = new JN_JournalComment();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["JournalID"].ToString()))	m.JournalID = (int)dr["JournalID"];
			if (!string.IsNullOrEmpty(dr["Staff"].ToString()))	m.Staff = (int)dr["Staff"];
			if (!string.IsNullOrEmpty(dr["Content"].ToString()))	m.Content = (string)dr["Content"];
			if (!string.IsNullOrEmpty(dr["CommentTime"].ToString()))	m.CommentTime = (DateTime)dr["CommentTime"];
			if (!string.IsNullOrEmpty(dr["ExtProperty"].ToString()))	m.ExtProperty = (string)dr["ExtProperty"];
						
			return m;
		}
    }
}

