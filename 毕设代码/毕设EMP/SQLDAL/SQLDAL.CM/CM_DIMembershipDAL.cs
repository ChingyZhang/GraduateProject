
// ===================================================================
// 文件： CM_DIMembershipDAL.cs
// 项目名称：
// 创建时间：2013/9/24
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.CM
{
	/// <summary>
	///CM_DIMembership数据访问DAL类
	/// </summary>
	public class CM_DIMembershipDAL : BaseSimpleDAL<CM_DIMembership>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_DIMembershipDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_DIMembership";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_DIMembership m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 250, m.UserName),
				SQLDatabase.MakeInParam("@Password", SqlDbType.VarChar, 128, m.Password),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@IsLockedOut", SqlDbType.Int, 4, m.IsLockedOut),
				SQLDatabase.MakeInParam("@IsApproved", SqlDbType.Int, 4, m.IsApproved),
				SQLDatabase.MakeInParam("@FailedPasswordAttemptCount", SqlDbType.Int, 4, m.FailedPasswordAttemptCount)
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
        public override int Update(CM_DIMembership m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 250, m.UserName),
				SQLDatabase.MakeInParam("@Password", SqlDbType.VarChar, 128, m.Password),
				SQLDatabase.MakeInParam("@IsLockedOut", SqlDbType.Int, 4, m.IsLockedOut),
				SQLDatabase.MakeInParam("@IsApproved", SqlDbType.Int, 4, m.IsApproved),
				SQLDatabase.MakeInParam("@FailedPasswordAttemptCount", SqlDbType.Int, 4, m.FailedPasswordAttemptCount)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_DIMembership FillModel(IDataReader dr)
		{
			CM_DIMembership m = new CM_DIMembership();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["UserName"].ToString()))	m.UserName = (string)dr["UserName"];
			if (!string.IsNullOrEmpty(dr["Password"].ToString()))	m.Password = (string)dr["Password"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["IsLockedOut"].ToString()))	m.IsLockedOut = (int)dr["IsLockedOut"];
			if (!string.IsNullOrEmpty(dr["IsApproved"].ToString()))	m.IsApproved = (int)dr["IsApproved"];
			if (!string.IsNullOrEmpty(dr["FailedPasswordAttemptCount"].ToString()))	m.FailedPasswordAttemptCount = (int)dr["FailedPasswordAttemptCount"];
						
			return m;
		}

        public DataTable GetByUserName(string userName)
        {
            SqlDataReader reader = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 250, userName)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetByUserName", prams, out reader);
            return Tools.ConvertDataReaderToDataTable(reader);
        }
    }
}

