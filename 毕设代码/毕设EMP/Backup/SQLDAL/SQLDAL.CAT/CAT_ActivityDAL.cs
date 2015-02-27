
// ===================================================================
// 文件： CAT_ActivityDAL.cs
// 项目名称：
// 创建时间：2009/12/24
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
    ///CAT_Activity数据访问DAL类
    /// </summary>
    public class CAT_ActivityDAL : BaseSimpleDAL<CAT_Activity>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CAT_ActivityDAL()
        {
            _ProcePrefix = "MCS_CAT.dbo.sp_CAT_Activity";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CAT_Activity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PlanBeginDate", SqlDbType.DateTime, 8, m.PlanBeginDate),
				SQLDatabase.MakeInParam("@PlanEndDate", SqlDbType.DateTime, 8, m.PlanEndDate),
				SQLDatabase.MakeInParam("@StageClient", SqlDbType.Int, 4, m.StageClient),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@AnchorPerson", SqlDbType.VarChar, 50, m.AnchorPerson),
				SQLDatabase.MakeInParam("@Prinipal", SqlDbType.VarChar, 50, m.Prinipal),
				SQLDatabase.MakeInParam("@FaceTo", SqlDbType.VarChar, 500, m.FaceTo),
				SQLDatabase.MakeInParam("@Detail", SqlDbType.VarChar, 2000, m.Detail),
				SQLDatabase.MakeInParam("@JoinFee", SqlDbType.Decimal, 9, m.JoinFee),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@RelateQuestionnaire", SqlDbType.Int, 4, m.RelateQuestionnaire),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@FeeApply", SqlDbType.Int, 4, m.FeeApply),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
                SQLDatabase.MakeInParam("@Officialcity", SqlDbType.Int, 4, m.Officialcity),
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
        public override int Update(CAT_Activity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PlanBeginDate", SqlDbType.DateTime, 8, m.PlanBeginDate),
				SQLDatabase.MakeInParam("@PlanEndDate", SqlDbType.DateTime, 8, m.PlanEndDate),
				SQLDatabase.MakeInParam("@StageClient", SqlDbType.Int, 4, m.StageClient),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 500, m.Address),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@AnchorPerson", SqlDbType.VarChar, 50, m.AnchorPerson),
				SQLDatabase.MakeInParam("@Prinipal", SqlDbType.VarChar, 50, m.Prinipal),
				SQLDatabase.MakeInParam("@FaceTo", SqlDbType.VarChar, 500, m.FaceTo),
				SQLDatabase.MakeInParam("@Detail", SqlDbType.VarChar, 2000, m.Detail),
				SQLDatabase.MakeInParam("@JoinFee", SqlDbType.Decimal, 9, m.JoinFee),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@RelateQuestionnaire", SqlDbType.Int, 4, m.RelateQuestionnaire),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@FeeApply", SqlDbType.Int, 4, m.FeeApply),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
                  SQLDatabase.MakeInParam("@Officialcity", SqlDbType.Int, 4, m.Officialcity),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CAT_Activity FillModel(IDataReader dr)
        {
            CAT_Activity m = new CAT_Activity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Topic"].ToString())) m.Topic = (string)dr["Topic"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["PlanBeginDate"].ToString())) m.PlanBeginDate = (DateTime)dr["PlanBeginDate"];
            if (!string.IsNullOrEmpty(dr["PlanEndDate"].ToString())) m.PlanEndDate = (DateTime)dr["PlanEndDate"];
            if (!string.IsNullOrEmpty(dr["StageClient"].ToString())) m.StageClient = (int)dr["StageClient"];
            if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
            if (!string.IsNullOrEmpty(dr["TeleNum"].ToString())) m.TeleNum = (string)dr["TeleNum"];
            if (!string.IsNullOrEmpty(dr["AnchorPerson"].ToString())) m.AnchorPerson = (string)dr["AnchorPerson"];
            if (!string.IsNullOrEmpty(dr["Prinipal"].ToString())) m.Prinipal = (string)dr["Prinipal"];
            if (!string.IsNullOrEmpty(dr["FaceTo"].ToString())) m.FaceTo = (string)dr["FaceTo"];
            if (!string.IsNullOrEmpty(dr["Detail"].ToString())) m.Detail = (string)dr["Detail"];
            if (!string.IsNullOrEmpty(dr["JoinFee"].ToString())) m.JoinFee = (decimal)dr["JoinFee"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["RelateQuestionnaire"].ToString())) m.RelateQuestionnaire = (int)dr["RelateQuestionnaire"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["FeeApply"].ToString())) m.FeeApply = (int)dr["FeeApply"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["Officialcity"].ToString())) m.Officialcity = (int)dr["Officialcity"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 活动审核
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int Approve(int ID, int State, int ApproveStaff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, ApproveStaff)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }
    }
}

