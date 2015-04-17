
// ===================================================================
// 文件： KB_CommentDAL.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   WJX
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
	///KB_Comment数据访问DAL类
	/// </summary>
	public class KB_CommentDAL : BaseSimpleDAL<KB_Comment>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_CommentDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_KB_Comment";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(KB_Comment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Article", SqlDbType.Int, 4, m.Article),
				SQLDatabase.MakeInParam("@CommentStaff", SqlDbType.Int, 4, m.CommentStaff),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 16, m.Content),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete)
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
        public override int Update(KB_Comment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Article", SqlDbType.Int, 4, m.Article),
				SQLDatabase.MakeInParam("@CommentStaff", SqlDbType.Int, 4, m.CommentStaff),
				SQLDatabase.MakeInParam("@CommentTime", SqlDbType.DateTime, 8, m.CommentTime),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 16, m.Content),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override KB_Comment FillModel(IDataReader dr)
		{
			KB_Comment m = new KB_Comment();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Article"].ToString()))	m.Article = (int)dr["Article"];
			if (!string.IsNullOrEmpty(dr["CommentStaff"].ToString()))	m.CommentStaff = (int)dr["CommentStaff"];
			if (!string.IsNullOrEmpty(dr["Content"].ToString()))	m.Content = (string)dr["Content"];
			if (!string.IsNullOrEmpty(dr["IsDelete"].ToString()))	m.IsDelete = (string)dr["IsDelete"];
						
			return m;
		}

        public void DeleteByID(int id)
        {
            SqlParameter[] parms ={
               SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id)                     
            };
            SQLDatabase.RunProc("MCS_OA.dbo.sp_KB_Comment_DeleteByID", parms);
        }

    }
}

