
// ===================================================================
// 文件： BBS_ForumAttachmentDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
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
	///BBS_ForumAttachment数据访问DAL类
	/// </summary>
	public class BBS_ForumAttachmentDAL : BaseSimpleDAL<BBS_ForumAttachment>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_ForumAttachmentDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_BBS_ForumAttachment";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(BBS_ForumAttachment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Item", SqlDbType.Int, 4, m.ItemID),
				SQLDatabase.MakeInParam("@Reply", SqlDbType.Int, 4, m.Reply),
				SQLDatabase.MakeInParam("@Name", SqlDbType.NVarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Path", SqlDbType.NVarChar, 1000, m.Path),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.ExtName),
				SQLDatabase.MakeInParam("@FileSize", SqlDbType.Int, 4, m.FileSize),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime)
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
        public override int Update(BBS_ForumAttachment m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Item", SqlDbType.Int, 4, m.ItemID),
				SQLDatabase.MakeInParam("@Reply", SqlDbType.Int, 4, m.Reply),
				SQLDatabase.MakeInParam("@Name", SqlDbType.NVarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Path", SqlDbType.NVarChar, 1000, m.Path),
				SQLDatabase.MakeInParam("@ExtName", SqlDbType.VarChar, 50, m.ExtName),
				SQLDatabase.MakeInParam("@FileSize", SqlDbType.Int, 4, m.FileSize),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override BBS_ForumAttachment FillModel(IDataReader dr)
		{
			BBS_ForumAttachment m = new BBS_ForumAttachment();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Item"].ToString())) m.ItemID = (int)dr["Item"];
			if (!string.IsNullOrEmpty(dr["Reply"].ToString()))	m.Reply = (int)dr["Reply"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Path"].ToString()))	m.Path = (string)dr["Path"];
			if (!string.IsNullOrEmpty(dr["ExtName"].ToString()))	m.ExtName = (string)dr["ExtName"];
			if (!string.IsNullOrEmpty(dr["FileSize"].ToString()))	m.FileSize = (int)dr["FileSize"];
			if (!string.IsNullOrEmpty(dr["UploadTime"].ToString()))	m.UploadTime = (DateTime)dr["UploadTime"];
            if (!string.IsNullOrEmpty(dr["GUID"].ToString())) m.GUID = new Guid(dr["GUID"].ToString());
			return m;
		}

        /// 根据GUID获取Model
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public BBS_ForumAttachment GetModelGUID(Guid guid)
        {
            #region	设置参数集
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@GUID", SqlDbType.UniqueIdentifier, 4, guid) };
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetModelGUID", prams, out dr);

            BBS_ForumAttachment m = null;
            if (dr.Read()) m = FillModel(dr);
            dr.Close();

            return m;
        } 
    }
}

