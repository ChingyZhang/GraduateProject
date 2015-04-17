
// ===================================================================
// 文件： Rpt_FolderRightDAL.cs
// 项目名称：
// 创建时间：2010/10/12
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.RPT
{
    /// <summary>
    ///Rpt_FolderRight数据访问DAL类
    /// </summary>
    public class Rpt_FolderRightDAL : BaseSimpleDAL<Rpt_FolderRight>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_FolderRightDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_FolderRight";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_FolderRight m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
				SQLDatabase.MakeInParam("@Action", SqlDbType.Int, 4, m.Action),
				SQLDatabase.MakeInParam("@Based_On", SqlDbType.Int, 4, m.Based_On),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@RoleName", SqlDbType.NVarChar, 256, m.RoleName),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_FolderRight m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
				SQLDatabase.MakeInParam("@Action", SqlDbType.Int, 4, m.Action),
				SQLDatabase.MakeInParam("@Based_On", SqlDbType.Int, 4, m.Based_On),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@RoleName", SqlDbType.NVarChar, 256, m.RoleName),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_FolderRight FillModel(IDataReader dr)
        {
            Rpt_FolderRight m = new Rpt_FolderRight();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Folder"].ToString())) m.Folder = (int)dr["Folder"];
            if (!string.IsNullOrEmpty(dr["Action"].ToString())) m.Action = (int)dr["Action"];
            if (!string.IsNullOrEmpty(dr["Based_On"].ToString())) m.Based_On = (int)dr["Based_On"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["RoleName"].ToString())) m.RoleName = (string)dr["RoleName"];
            if (!string.IsNullOrEmpty(dr["UserName"].ToString())) m.UserName = (string)dr["UserName"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public IList<Rpt_FolderRight> GetAssignedRightByUser(string UserName)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, UserName)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetAssignedRightByUser", prams, out dr);

            return FillModelList(dr);
        }

        public IList<Rpt_FolderRight> GetAssignedRightByUser(string ApplicationName, string UserName)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ApplicationName", SqlDbType.NVarChar, 256, ApplicationName),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, UserName)
            };

            SQLDatabase.RunProc(_ProcePrefix + "_GetAssignedRightByUser", prams, out dr);

            return FillModelList(dr);
        }
    }
}

