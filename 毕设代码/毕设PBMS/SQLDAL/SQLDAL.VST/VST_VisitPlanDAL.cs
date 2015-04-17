
// ===================================================================
// 文件： VST_VisitPlanDAL.cs
// 项目名称：
// 创建时间：2015-03-13
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.VST;


namespace MCSFramework.SQLDAL.VST
{
	/// <summary>
	///VST_VisitPlan数据访问DAL类
	/// </summary>
	public class VST_VisitPlanDAL : BaseComplexDAL<VST_VisitPlan,VST_VisitPlan_Detail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_VisitPlanDAL()
		{
			_ProcePrefix = "MCS_VST.dbo.sp_VST_VisitPlan";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_VisitPlan m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Route", SqlDbType.Int, 4, m.Route),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@PlanVisitDate", SqlDbType.DateTime, 8, m.PlanVisitDate),
				SQLDatabase.MakeInParam("@IsMustSequenceVisit", SqlDbType.Char, 1, m.IsMustSequenceVisit),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(VST_VisitPlan m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Route", SqlDbType.Int, 4, m.Route),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@PlanVisitDate", SqlDbType.DateTime, 8, m.PlanVisitDate),
				SQLDatabase.MakeInParam("@IsMustSequenceVisit", SqlDbType.Char, 1, m.IsMustSequenceVisit),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override VST_VisitPlan FillModel(IDataReader dr)
		{
			VST_VisitPlan m = new VST_VisitPlan();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Route"].ToString()))	m.Route = (int)dr["Route"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["PlanVisitDate"].ToString()))	m.PlanVisitDate = (DateTime)dr["PlanVisitDate"];
			if (!string.IsNullOrEmpty(dr["IsMustSequenceVisit"].ToString()))	m.IsMustSequenceVisit = (string)dr["IsMustSequenceVisit"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(VST_VisitPlan_Detail m)
        {
			m.PlanID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PlanID", SqlDbType.Int, 4, m.PlanID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitedFlag", SqlDbType.Int, 4, m.VisitedFlag),
				SQLDatabase.MakeInParam("@VisitedTime", SqlDbType.DateTime, 8, m.VisitedTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(VST_VisitPlan_Detail m)
        {
            m.PlanID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PlanID", SqlDbType.Int, 4, m.PlanID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitedFlag", SqlDbType.Int, 4, m.VisitedFlag),
				SQLDatabase.MakeInParam("@VisitedTime", SqlDbType.DateTime, 8, m.VisitedTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override VST_VisitPlan_Detail FillDetailModel(IDataReader dr)
        {
            VST_VisitPlan_Detail m = new VST_VisitPlan_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["PlanID"].ToString()))	m.PlanID = (int)dr["PlanID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["VisitSequence"].ToString()))	m.VisitSequence = (int)dr["VisitSequence"];
			if (!string.IsNullOrEmpty(dr["VisitedFlag"].ToString()))	m.VisitedFlag = (int)dr["VisitedFlag"];
			if (!string.IsNullOrEmpty(dr["VisitedTime"].ToString()))	m.VisitedTime = (DateTime)dr["VisitedTime"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }
    }
}

