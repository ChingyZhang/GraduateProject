// ===================================================================
// 文件： CAT_ClientJoinInfoDAL.cs
// 项目名称：
// 创建时间：2009/12/27
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CAT;


namespace MCSFramework.SQLDAL.CAT
{
    /// <summary>
    ///CAT_ClientJoinInfo数据访问DAL类
    /// </summary>
    public class CAT_ClientJoinInfoDAL : BaseSimpleDAL<CAT_ClientJoinInfo>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CAT_ClientJoinInfoDAL()
        {
            _ProcePrefix = "MCS_CAT.dbo.sp_CAT_ClientJoinInfo";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CAT_ClientJoinInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@InviteState", SqlDbType.Int, 4, m.InviteState),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@JoinState", SqlDbType.Int, 4, m.JoinState),
				SQLDatabase.MakeInParam("@JoinMan", SqlDbType.VarChar, 50, m.JoinMan),
				SQLDatabase.MakeInParam("@JoinDate", SqlDbType.DateTime, 8, m.JoinDate),
				SQLDatabase.MakeInParam("@Feedback", SqlDbType.VarChar, 1000, m.Feedback),
				SQLDatabase.MakeInParam("@QuestionnairResult", SqlDbType.Int, 4, m.QuestionnairResult),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(CAT_ClientJoinInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@InviteState", SqlDbType.Int, 4, m.InviteState),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@JoinState", SqlDbType.Int, 4, m.JoinState),
				SQLDatabase.MakeInParam("@JoinMan", SqlDbType.VarChar, 50, m.JoinMan),
				SQLDatabase.MakeInParam("@JoinDate", SqlDbType.DateTime, 8, m.JoinDate),
				SQLDatabase.MakeInParam("@Feedback", SqlDbType.VarChar, 1000, m.Feedback),
				SQLDatabase.MakeInParam("@QuestionnairResult", SqlDbType.Int, 4, m.QuestionnairResult),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CAT_ClientJoinInfo FillModel(IDataReader dr)
        {
            CAT_ClientJoinInfo m = new CAT_ClientJoinInfo();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Activity"].ToString())) m.Activity = (int)dr["Activity"];
            if (!string.IsNullOrEmpty(dr["InviteState"].ToString())) m.InviteState = (int)dr["InviteState"];
            if (!string.IsNullOrEmpty(dr["ConfirmDate"].ToString())) m.ConfirmDate = (DateTime)dr["ConfirmDate"];
            if (!string.IsNullOrEmpty(dr["ConfirmStaff"].ToString())) m.ConfirmStaff = (int)dr["ConfirmStaff"];
            if (!string.IsNullOrEmpty(dr["JoinState"].ToString())) m.JoinState = (int)dr["JoinState"];
            if (!string.IsNullOrEmpty(dr["JoinMan"].ToString())) m.JoinMan = (string)dr["JoinMan"];
            if (!string.IsNullOrEmpty(dr["JoinDate"].ToString())) m.JoinDate = (DateTime)dr["JoinDate"];
            if (!string.IsNullOrEmpty(dr["Feedback"].ToString())) m.Feedback = (string)dr["Feedback"];
            if (!string.IsNullOrEmpty(dr["QuestionnairResult"].ToString())) m.QuestionnairResult = (int)dr["QuestionnairResult"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

    }
}


