
// ===================================================================
// 文件： IPT_UploadTemplateDAL.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.IPT;


namespace MCSFramework.SQLDAL.IPT
{
    /// <summary>
    ///IPT_UploadTemplate数据访问DAL类
    /// </summary>
    public class IPT_UploadTemplateDAL : BaseSimpleDAL<IPT_UploadTemplate>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public IPT_UploadTemplateDAL()
        {
            _ProcePrefix = "MCS_IPT.dbo.sp_IPT_UploadTemplate";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(IPT_UploadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FullFileName", SqlDbType.VarChar, 500, m.FullFileName),
				SQLDatabase.MakeInParam("@ShortFileName", SqlDbType.VarChar, 200, m.ShortFileName),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@FileType", SqlDbType.Int, 4, m.FileType),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ImportTime", SqlDbType.DateTime, 8, m.ImportTime),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, m.UserName),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@ClientName", SqlDbType.VarChar, 50, m.ClientName),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Int, 4, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Int, 4, m.Data02),
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
        public override int Update(IPT_UploadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@FullFileName", SqlDbType.VarChar, 500, m.FullFileName),
				SQLDatabase.MakeInParam("@ShortFileName", SqlDbType.VarChar, 200, m.ShortFileName),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@FileType", SqlDbType.Int, 4, m.FileType),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ImportTime", SqlDbType.DateTime, 8, m.ImportTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, m.UserName),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@ClientName", SqlDbType.VarChar, 50, m.ClientName),
				SQLDatabase.MakeInParam("@Data01", SqlDbType.Int, 4, m.Data01),
				SQLDatabase.MakeInParam("@Data02", SqlDbType.Int, 4, m.Data02),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override IPT_UploadTemplate FillModel(IDataReader dr)
        {
            IPT_UploadTemplate m = new IPT_UploadTemplate();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["FullFileName"].ToString())) m.FullFileName = (string)dr["FullFileName"];
            if (!string.IsNullOrEmpty(dr["ShortFileName"].ToString())) m.ShortFileName = (string)dr["ShortFileName"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["FileType"].ToString())) m.FileType = (int)dr["FileType"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["ImportTime"].ToString())) m.ImportTime = (DateTime)dr["ImportTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UserName"].ToString())) m.UserName = (string)dr["UserName"];
            if (!string.IsNullOrEmpty(dr["ClientID"].ToString())) m.ClientID = (int)dr["ClientID"];
            if (!string.IsNullOrEmpty(dr["ClientName"].ToString())) m.ClientName = (string)dr["ClientName"];
            if (!string.IsNullOrEmpty(dr["Data01"].ToString())) m.Data01 = (int)dr["Data01"];
            if (!string.IsNullOrEmpty(dr["Data02"].ToString())) m.Data02 = (int)dr["Data02"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

