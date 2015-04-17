
// ===================================================================
// 文件： KB_CatalogDAL.cs
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
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///KB_Catalog数据访问DAL类
	/// </summary>
	public class KB_CatalogDAL : BaseSimpleDAL<KB_Catalog>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_CatalogDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_KB_Catalog";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(KB_Catalog m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@CommentFlag", SqlDbType.Char, 1, m.CommentFlag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Char, 1, m.ApproveFlag)
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
        public override int Update(KB_Catalog m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@CommentFlag", SqlDbType.Char, 1, m.CommentFlag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Char, 1, m.ApproveFlag)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override KB_Catalog FillModel(IDataReader dr)
		{
			KB_Catalog m = new KB_Catalog();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["SuperID"].ToString()))	m.SuperID = (int)dr["SuperID"];
			if (!string.IsNullOrEmpty(dr["ApproveStaff"].ToString()))	m.ApproveStaff = (int)dr["ApproveStaff"];
			if (!string.IsNullOrEmpty(dr["CommentFlag"].ToString()))	m.CommentFlag = (string)dr["CommentFlag"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (string)dr["ApproveFlag"];
						
			return m;
		}

        public DataTable GetAllPosition()
        {
            SqlDataReader dr = null;

            SQLDatabase.RunProc("MCS_OA.dbo.sp_KB_Catalog_GetByCondition", out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

