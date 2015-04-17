
// ===================================================================
// 文件： JN_WorkingPlanDAL.cs
// 项目名称：
// 创建时间：2009/6/18
// 作者:	   Shen Gang
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
    ///JN_WorkingPlan数据访问DAL类
    /// </summary>
    public class JN_WorkingPlanDAL : BaseComplexDAL<JN_WorkingPlan, JN_WorkingPlanDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public JN_WorkingPlanDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_JN_WorkingPlan";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(JN_WorkingPlan m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
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
        public override int Update(JN_WorkingPlan m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override JN_WorkingPlan FillModel(IDataReader dr)
        {
            JN_WorkingPlan m = new JN_WorkingPlan();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Title"].ToString())) m.Title = (string)dr["Title"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Staff"].ToString())) m.Staff = (int)dr["Staff"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(JN_WorkingPlanDetail m)
        {
            m.WorkingPlan = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WorkingPlan", SqlDbType.Int, 4, m.WorkingPlan),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(JN_WorkingPlanDetail m)
        {
            m.WorkingPlan = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WorkingPlan", SqlDbType.Int, 4, m.WorkingPlan),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override JN_WorkingPlanDetail FillDetailModel(IDataReader dr)
        {
            JN_WorkingPlanDetail m = new JN_WorkingPlanDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WorkingPlan"].ToString())) m.WorkingPlan = (int)dr["WorkingPlan"];
            if (!string.IsNullOrEmpty(dr["BeginTime"].ToString())) m.BeginTime = (DateTime)dr["BeginTime"];
            if (!string.IsNullOrEmpty(dr["EndTime"].ToString())) m.EndTime = (DateTime)dr["EndTime"];
            if (!string.IsNullOrEmpty(dr["WorkingClassify"].ToString())) m.WorkingClassify = (int)dr["WorkingClassify"];
            if (!string.IsNullOrEmpty(dr["RelateClient"].ToString())) m.RelateClient = (int)dr["RelateClient"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString())) m.RelateStaff = (int)dr["RelateStaff"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int id, int staff, int taskid)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4, taskid)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        public SqlDataReader GetSummary(DateTime BeginDate, DateTime EndDate, int OrganizeCity, int Position, string StaffName,int  IncludeChildPosition)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, Position),
                SQLDatabase.MakeInParam("@StaffName", SqlDbType.VarChar, 50, StaffName),
                SQLDatabase.MakeInParam("@IncludeChildPosition", SqlDbType.Int, 4, IncludeChildPosition),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);

            return dr;

        }
    }
}

