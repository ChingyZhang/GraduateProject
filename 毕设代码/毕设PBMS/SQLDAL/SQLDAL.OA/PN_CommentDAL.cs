
// ===================================================================
// 文件： PN_CommentDAL.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///PN_Comment数据访问DAL类
	/// </summary>
	public class PN_CommentDAL : BaseSimpleDAL<PN_Comment>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_CommentDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_PN_Comment";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PN_Comment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 5000, m.Content),
				SQLDatabase.MakeInParam("@CommentTime", SqlDbType.DateTime, 8, m.CommentTime)
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
        public override int Update(PN_Comment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@Content", SqlDbType.VarChar, 5000, m.Content),
				SQLDatabase.MakeInParam("@CommentTime", SqlDbType.DateTime, 8, m.CommentTime)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PN_Comment FillModel(IDataReader dr)
		{
			PN_Comment m = new PN_Comment();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Notice"].ToString()))	m.Notice = (int)dr["Notice"];
			if (!string.IsNullOrEmpty(dr["Staff"].ToString()))	m.Staff = (int)dr["Staff"];
			if (!string.IsNullOrEmpty(dr["Content"].ToString()))	m.Content = (string)dr["Content"];
			if (!string.IsNullOrEmpty(dr["CommentTime"].ToString()))	m.CommentTime = (DateTime)dr["CommentTime"];
						
			return m;
		}
        

        public DataTable GetUsertbList(int notice)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4,notice)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_Select", parms, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }


        #region 根据邮件ID获得评论人数
        public int GetCommentCountByNotice(int notice)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@Notice",SqlDbType.Int,4,notice),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetCommentCountByNotice", prams, out dr);
            int ret = 0;
            if (dr.Read())
                ret = (int)dr[0];
            return ret;
        }
        #endregion
    }
}

