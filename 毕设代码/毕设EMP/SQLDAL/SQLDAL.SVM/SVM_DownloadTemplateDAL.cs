
// ===================================================================
// 文件： SVM_DownloadTemplateDAL.cs
// 项目名称：
// 创建时间：2012/6/19
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
    ///SVM_DownloadTemplate数据访问DAL类
    /// </summary>
    public class SVM_DownloadTemplateDAL : BaseSimpleDAL<SVM_DownloadTemplate>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_DownloadTemplateDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_DownloadTemplate";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_DownloadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, m.IsOpponent),
				SQLDatabase.MakeInParam("@ProductGifts", SqlDbType.VarChar, 500, m.ProductGifts),
				SQLDatabase.MakeInParam("@Testers", SqlDbType.VarChar, 500, m.Testers),
				SQLDatabase.MakeInParam("@Gifts", SqlDbType.VarChar, 500, m.Gifts),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 4000, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@DownStaff", SqlDbType.Int, 4, m.DownStaff),
				SQLDatabase.MakeInParam("@DownTime", SqlDbType.DateTime, 8, m.DownTime),
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
        public override int Update(SVM_DownloadTemplate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 500, m.Path),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, m.IsOpponent),
				SQLDatabase.MakeInParam("@ProductGifts", SqlDbType.VarChar, 500, m.ProductGifts),
				SQLDatabase.MakeInParam("@Testers", SqlDbType.VarChar, 500, m.Testers),
				SQLDatabase.MakeInParam("@Gifts", SqlDbType.VarChar, 500, m.Gifts),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 4000, m.Remark),
				SQLDatabase.MakeInParam("@DownStaff", SqlDbType.Int, 4, m.DownStaff),
				SQLDatabase.MakeInParam("@DownTime", SqlDbType.DateTime, 8, m.DownTime),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_DownloadTemplate FillModel(IDataReader dr)
        {
            SVM_DownloadTemplate m = new SVM_DownloadTemplate();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Path"].ToString())) m.Path = (string)dr["Path"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["IsOpponent"].ToString())) m.IsOpponent = (int)dr["IsOpponent"];
            if (!string.IsNullOrEmpty(dr["ProductGifts"].ToString())) m.ProductGifts = (string)dr["ProductGifts"];
            if (!string.IsNullOrEmpty(dr["Testers"].ToString())) m.Testers = (string)dr["Testers"];
            if (!string.IsNullOrEmpty(dr["Gifts"].ToString())) m.Gifts = (string)dr["Gifts"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["DownStaff"].ToString())) m.DownStaff = (int)dr["DownStaff"];
            if (!string.IsNullOrEmpty(dr["DownTime"].ToString())) m.DownTime = (DateTime)dr["DownTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

