using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.DBUtility;
using System.Data.Common;

namespace MCSFramework.SQLDAL
{
    public class Right_Assign_DAL : BaseSimpleDAL<Right_Assign>
    {
        #region 权限的增加、删除、修改
        public Right_Assign_DAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Right_Assign";
        }

        public override int Add(Right_Assign m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
				SQLDatabase.MakeInParam("@Action", SqlDbType.Int, 4, m.Action),
				SQLDatabase.MakeInParam("@Based_On", SqlDbType.Int, 4, m.Based_On),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@RoleName", SqlDbType.NVarChar, 256, m.RoleName),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        public override int Update(Right_Assign m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
				SQLDatabase.MakeInParam("@Action", SqlDbType.Int, 4, m.Action),
				SQLDatabase.MakeInParam("@Based_On", SqlDbType.Int, 4, m.Based_On),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@RoleName", SqlDbType.NVarChar, 256, m.RoleName),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        
        protected override Right_Assign FillModel(System.Data.IDataReader dr)
        {
            Right_Assign m = new Right_Assign();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Module"].ToString())) m.Module = (int)dr["Module"];
            if (!string.IsNullOrEmpty(dr["Action"].ToString())) m.Action = (int)dr["Action"];
            if (!string.IsNullOrEmpty(dr["Based_On"].ToString())) m.Based_On = (int)dr["Based_On"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["RoleName"].ToString())) m.RoleName = (string)dr["RoleName"];
            if (!string.IsNullOrEmpty(dr["UserName"].ToString())) m.UserName = (string)dr["UserName"];

            //if (!string.IsNullOrEmpty(dr["ModuleName"].ToString())) m.ModuleName = (string)dr["ModuleName"];
            //if (!string.IsNullOrEmpty(dr["ActionCode"].ToString())) m.ActionCode = (string)dr["ActionCode"];
            //if (!string.IsNullOrEmpty(dr["ActionName"].ToString())) m.ActionName = (string)dr["ActionName"];
            return m;
        }
        #endregion

        /// <summary>
        /// 根据用户名获取已分配的权限列表
        /// </summary>
        /// <param name="applicationname">应用程序名称</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public DbDataReader GetAssignedRightList(string applicationname, string username)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ApplicationName",SqlDbType.NVarChar , 256, applicationname),
				SQLDatabase.MakeInParam("@UserName",SqlDbType.NVarChar , 256, username)
					};
            SQLDatabase.RunProc(_ProcePrefix + "_GetAssignedRightByUser", prams, out dr);

            return dr;
        }
    }
}
