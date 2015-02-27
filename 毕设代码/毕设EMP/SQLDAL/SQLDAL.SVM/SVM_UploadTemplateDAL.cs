
// ===================================================================
// 文件： SVM_UploadTemplateDAL.cs
// 项目名称：
// 创建时间：2012/6/21
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;


namespace MCSFramework.SQLDAL.SVM
{
    /// <summary>
    ///SVM_UploadTemplate数据访问DAL类
    /// </summary>
    public class SVM_UploadTemplateDAL : BaseSimpleDAL<SVM_UploadTemplate>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_UploadTemplateDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_UploadTemplate";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_UploadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.Text, 0, m.Remark),
				SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, m.IsOpponent),
				SQLDatabase.MakeInParam("@UploadStaff", SqlDbType.Int, 4, m.UploadStaff),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ImprotTime", SqlDbType.DateTime, 8, m.ImprotTime),
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
        public override int Update(SVM_UploadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.Text, 0, m.Remark),
				SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, m.IsOpponent),
				SQLDatabase.MakeInParam("@UploadStaff", SqlDbType.Int, 4, m.UploadStaff),
				SQLDatabase.MakeInParam("@UploadTime", SqlDbType.DateTime, 8, m.UploadTime),
				SQLDatabase.MakeInParam("@ImprotTime", SqlDbType.DateTime, 8, m.ImprotTime),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_UploadTemplate FillModel(IDataReader dr)
        {
            SVM_UploadTemplate m = new SVM_UploadTemplate();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Path"].ToString())) m.Path = (string)dr["Path"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["IsOpponent"].ToString())) m.IsOpponent = (int)dr["IsOpponent"];
            if (!string.IsNullOrEmpty(dr["UploadStaff"].ToString())) m.UploadStaff = (int)dr["UploadStaff"];
            if (!string.IsNullOrEmpty(dr["UploadTime"].ToString())) m.UploadTime = (DateTime)dr["UploadTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["ImprotTime"].ToString())) m.ImprotTime = (DateTime)dr["ImprotTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

