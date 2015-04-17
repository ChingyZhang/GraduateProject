using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using System.Collections.Generic;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.OA
{
    public class ML_AttachFileDAL : BaseSimpleDAL<ML_AttachFile>
    {
        #region 构造函数
        public ML_AttachFileDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_ML_AttachFile";
        }
        #endregion

        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ML_AttachFile m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@MailID", SqlDbType.Int, 4,m.Mailid),
				SQLDatabase.MakeInParam("@Name", SqlDbType.NVarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Size", SqlDbType.Int, 4, m.Size),
				SQLDatabase.MakeInParam("@VisualPath", SqlDbType.VarChar, 255, m.Visualpath),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.Extname),
				SQLDatabase.MakeInParam("@UploadUser", SqlDbType.VarChar, 50, m.Uploadtime),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.Uploadtime),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.Isdelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 800, CombineExtProperty(m.ExtPropertys,m.ModelName))	
			};
            #endregion

            m.Id = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.Id;
        }

        public override int Update(ML_AttachFile m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.Id),
				SQLDatabase.MakeInParam("@MailID", SqlDbType.Int, 4,m.Mailid),
				SQLDatabase.MakeInParam("@Name", SqlDbType.NVarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Size", SqlDbType.Int, 4, m.Size),
				SQLDatabase.MakeInParam("@VisualPath", SqlDbType.VarChar, 255, m.Visualpath),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.Extname),
				SQLDatabase.MakeInParam("@UploadUser", SqlDbType.VarChar, 50, m.Uploadtime),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.Uploadtime),
				SQLDatabase.MakeInParam("@IsDelete", SqlDbType.Char, 1, m.Isdelete),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 800, CombineExtProperty(m.ExtPropertys,m.ModelName))	
			};
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ML_AttachFile FillModel(IDataReader dr)
        {
            ML_AttachFile m = new ML_AttachFile();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.Id = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["MailID"].ToString())) m.Mailid = (int)dr["MailID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Size"].ToString())) m.Size = (int)dr["Size"];
            if (!string.IsNullOrEmpty(dr["VisualPath"].ToString())) m.Visualpath = (string)dr["VisualPath"];
            if (!string.IsNullOrEmpty(dr["ExtName"].ToString())) m.Extname = (string)dr["ExtName"];
            if (!string.IsNullOrEmpty(dr["UploadUser"].ToString())) m.Uploaduser = (string)dr["UploadUser"];
            if (!string.IsNullOrEmpty(dr["UploadTime"].ToString())) m.Uploadtime = (DateTime)dr["UploadTime"];
            if (!string.IsNullOrEmpty(dr["IsDelete"].ToString())) m.Isdelete = (string)dr["IsDelete"];
            if (!string.IsNullOrEmpty(dr["GUID"].ToString())) m.GUID = new Guid(dr["GUID"].ToString());
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            return m;
        }

        /// <summary>
        /// 根据GUID获取Model
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ML_AttachFile GetModelGUID(Guid guid)
        {
            #region	设置参数集
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 4, guid) };
            #endregion


            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetModelGUID", prams, out dr);

            ML_AttachFile m = null;
            if (dr.Read()) m = FillModel(dr);
            dr.Close();

            return m;
        }  
    }
}
