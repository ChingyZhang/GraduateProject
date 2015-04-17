
// ===================================================================
// 文件： VST_WorkListDAL.cs
// 项目名称：
// 创建时间：2015-02-01
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
	///VST_WorkList数据访问DAL类
	/// </summary>
	public class VST_WorkListDAL : BaseComplexDAL<VST_WorkList,VST_WorkItem>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_WorkListDAL()
		{
			_ProcePrefix = "MCS_VST.dbo.sp_VST_WorkList";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_WorkList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@Route", SqlDbType.Int, 4, m.Route),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Template", SqlDbType.Int, 4, m.Template),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@IsComplete", SqlDbType.Char, 1, m.IsComplete),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@PlanID", SqlDbType.Int, 4, m.PlanID),
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
        public override int Update(VST_WorkList m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@Route", SqlDbType.Int, 4, m.Route),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Template", SqlDbType.Int, 4, m.Template),
				SQLDatabase.MakeInParam("@WorkingClassify", SqlDbType.Int, 4, m.WorkingClassify),
				SQLDatabase.MakeInParam("@IsComplete", SqlDbType.Char, 1, m.IsComplete),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
                SQLDatabase.MakeInParam("@PlanID", SqlDbType.Int, 4, m.PlanID),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override VST_WorkList FillModel(IDataReader dr)
		{
			VST_WorkList m = new VST_WorkList();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["Route"].ToString()))	m.Route = (int)dr["Route"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["Template"].ToString()))	m.Template = (int)dr["Template"];
			if (!string.IsNullOrEmpty(dr["WorkingClassify"].ToString()))	m.WorkingClassify = (int)dr["WorkingClassify"];
			if (!string.IsNullOrEmpty(dr["IsComplete"].ToString()))	m.IsComplete = (string)dr["IsComplete"];
			if (!string.IsNullOrEmpty(dr["BeginTime"].ToString()))	m.BeginTime = (DateTime)dr["BeginTime"];
			if (!string.IsNullOrEmpty(dr["EndTime"].ToString()))	m.EndTime = (DateTime)dr["EndTime"];
            if (!string.IsNullOrEmpty(dr["PlanID"].ToString())) m.PlanID = (int)dr["PlanID"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(VST_WorkItem m)
        {
			m.WorkList = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList),
				SQLDatabase.MakeInParam("@Process", SqlDbType.Int, 4, m.Process),
				SQLDatabase.MakeInParam("@WorkTime", SqlDbType.DateTime, 8, m.WorkTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(VST_WorkItem m)
        {
            m.WorkList = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList),
				SQLDatabase.MakeInParam("@Process", SqlDbType.Int, 4, m.Process),
				SQLDatabase.MakeInParam("@WorkTime", SqlDbType.DateTime, 8, m.WorkTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override VST_WorkItem FillDetailModel(IDataReader dr)
        {
            VST_WorkItem m = new VST_WorkItem();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["WorkList"].ToString()))	m.WorkList = (int)dr["WorkList"];
			if (!string.IsNullOrEmpty(dr["Process"].ToString()))	m.Process = (int)dr["Process"];
			if (!string.IsNullOrEmpty(dr["WorkTime"].ToString()))	m.WorkTime = (DateTime)dr["WorkTime"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }
    }
}

