
// ===================================================================
// 文件： KB_ArticleDAL.cs
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
	///KB_Article数据访问DAL类
	/// </summary>
	public class KB_ArticleDAL : BaseSimpleDAL<KB_Article>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_ArticleDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_KB_Article";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(KB_Article m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@Catalog", SqlDbType.Int, 4, m.Catalog),
				SQLDatabase.MakeInParam("@KeyWord", SqlDbType.VarChar, 200, m.KeyWord),
				SQLDatabase.MakeInParam("@Source", SqlDbType.VarChar, 200, m.Source),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 2147483647, m.Content),
				SQLDatabase.MakeInParam("@Author", SqlDbType.VarChar, 50, m.Author),
				SQLDatabase.MakeInParam("@UploadStaff", SqlDbType.Int, 4, m.UploadStaff),
				SQLDatabase.MakeInParam("@HasApproved", SqlDbType.Char, 1, m.HasApproved),
				SQLDatabase.MakeInParam("@ApproveStaffIdeas", SqlDbType.VarChar, 500, m.ApproveStaffIdeas),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@ReadCount", SqlDbType.Int, 4, m.ReadCount),
				SQLDatabase.MakeInParam("@UsefullCount", SqlDbType.Int, 4, m.UsefullCount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(KB_Article m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@Catalog", SqlDbType.Int, 4, m.Catalog),
				SQLDatabase.MakeInParam("@KeyWord", SqlDbType.VarChar, 200, m.KeyWord),
				SQLDatabase.MakeInParam("@Source", SqlDbType.VarChar, 200, m.Source),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 2147483647, m.Content),
				SQLDatabase.MakeInParam("@Author", SqlDbType.VarChar, 50, m.Author),
				SQLDatabase.MakeInParam("@UploadStaff", SqlDbType.Int, 4, m.UploadStaff),
				SQLDatabase.MakeInParam("@HasApproved", SqlDbType.Char, 1, m.HasApproved),
                SQLDatabase.MakeInParam("@ApproveTime", SqlDbType.DateTime, 8, m.ApproveTime),
				SQLDatabase.MakeInParam("@ApproveStaffIdeas", SqlDbType.VarChar, 500, m.ApproveStaffIdeas),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@ReadCount", SqlDbType.Int, 4, m.ReadCount),
				SQLDatabase.MakeInParam("@UsefullCount", SqlDbType.Int, 4, m.UsefullCount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.IsDelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override KB_Article FillModel(IDataReader dr)
		{
			KB_Article m = new KB_Article();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Title"].ToString()))	m.Title = (string)dr["Title"];
			if (!string.IsNullOrEmpty(dr["Catalog"].ToString()))	m.Catalog = (int)dr["Catalog"];
			if (!string.IsNullOrEmpty(dr["KeyWord"].ToString()))	m.KeyWord = (string)dr["KeyWord"];
			if (!string.IsNullOrEmpty(dr["Source"].ToString()))	m.Source = (string)dr["Source"];
			if (!string.IsNullOrEmpty(dr["Content"].ToString()))	m.Content = (string)dr["Content"];
			if (!string.IsNullOrEmpty(dr["Author"].ToString()))	m.Author = (string)dr["Author"];
			if (!string.IsNullOrEmpty(dr["UploadStaff"].ToString()))	m.UploadStaff = (int)dr["UploadStaff"];
			if (!string.IsNullOrEmpty(dr["UploadTime"].ToString()))	m.UploadTime = (DateTime)dr["UploadTime"];
			if (!string.IsNullOrEmpty(dr["HasApproved"].ToString()))	m.HasApproved = (string)dr["HasApproved"];
			if (!string.IsNullOrEmpty(dr["ApproveTime"].ToString()))	m.ApproveTime = (DateTime)dr["ApproveTime"];
			if (!string.IsNullOrEmpty(dr["ApproveStaffIdeas"].ToString()))	m.ApproveStaffIdeas = (string)dr["ApproveStaffIdeas"];
			if (!string.IsNullOrEmpty(dr["ApproveStaff"].ToString()))	m.ApproveStaff = (int)dr["ApproveStaff"];
			if (!string.IsNullOrEmpty(dr["ReadCount"].ToString()))	m.ReadCount = (int)dr["ReadCount"];
			if (!string.IsNullOrEmpty(dr["UsefullCount"].ToString()))	m.UsefullCount = (int)dr["UsefullCount"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["IsDelete"].ToString()))	m.IsDelete = (string)dr["IsDelete"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        public void DeleteByID(int id)
        {
            SqlParameter[] parms ={
               SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id)                     
            };
            SQLDatabase.RunProc("MCS_OA.dbo.sp_KB_Article_DeleteByID", parms);
        }

        public void UpdateReadcount(int id)
        {
            SqlParameter[] parms ={
               SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id)                     
            };
            SQLDatabase.RunProc("MCS_OA.dbo.sp_KB_Article_UpdateReadCount", parms);
        }


        public void UpdateApprov(int id, int approvestaff, string ideas)
        {
            SqlParameter[] parms = {
              SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,id),
              SQLDatabase.MakeInParam("@ApproveStaff",SqlDbType.Int,4,approvestaff),
              SQLDatabase.MakeInParam("@ApproveStaffIdeas",SqlDbType.VarChar,200,ideas)
            };
            SQLDatabase.RunProc("MCS_OA.dbo.sp_KB_Article_ApprovUpdate",parms);
        }
    }
}

