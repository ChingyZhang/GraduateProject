
// ===================================================================
// 文件： VST_VisitTemplateDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
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
	///VST_VisitTemplate数据访问DAL类
	/// </summary>
	public class VST_VisitTemplateDAL : BaseComplexDAL<VST_VisitTemplate,VST_VisitTemplateDetail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_VisitTemplateDAL()
		{
			_ProcePrefix = "MCS_VST.dbo.sp_VST_VisitTemplate";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_VisitTemplate m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@IsMustRelateRoute", SqlDbType.Char, 1, m.IsMustRelateRoute),
				SQLDatabase.MakeInParam("@CanTempVisit", SqlDbType.Char, 1, m.CanTempVisit),
				SQLDatabase.MakeInParam("@IsMustSequenceCall", SqlDbType.Char, 1, m.IsMustSequenceCall),
				SQLDatabase.MakeInParam("@CanRepetitionCall", SqlDbType.Char, 1, m.CanRepetitionCall),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
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
        public override int Update(VST_VisitTemplate m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@IsMustRelateRoute", SqlDbType.Char, 1, m.IsMustRelateRoute),
				SQLDatabase.MakeInParam("@CanTempVisit", SqlDbType.Char, 1, m.CanTempVisit),
				SQLDatabase.MakeInParam("@IsMustSequenceCall", SqlDbType.Char, 1, m.IsMustSequenceCall),
				SQLDatabase.MakeInParam("@CanRepetitionCall", SqlDbType.Char, 1, m.CanRepetitionCall),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override VST_VisitTemplate FillModel(IDataReader dr)
		{
			VST_VisitTemplate m = new VST_VisitTemplate();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["EnableFlag"].ToString()))	m.EnableFlag = (string)dr["EnableFlag"];
			if (!string.IsNullOrEmpty(dr["IsMustRelateRoute"].ToString()))	m.IsMustRelateRoute = (string)dr["IsMustRelateRoute"];
			if (!string.IsNullOrEmpty(dr["CanTempVisit"].ToString()))	m.CanTempVisit = (string)dr["CanTempVisit"];
			if (!string.IsNullOrEmpty(dr["IsMustSequenceCall"].ToString()))	m.IsMustSequenceCall = (string)dr["IsMustSequenceCall"];
			if (!string.IsNullOrEmpty(dr["CanRepetitionCall"].ToString()))	m.CanRepetitionCall = (string)dr["CanRepetitionCall"];
			if (!string.IsNullOrEmpty(dr["OwnerType"].ToString()))	m.OwnerType = (int)dr["OwnerType"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(VST_VisitTemplateDetail m)
        {
			m.TemplateID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TemplateID", SqlDbType.Int, 4, m.TemplateID),
				SQLDatabase.MakeInParam("@ProcessID", SqlDbType.Int, 4, m.ProcessID),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@CanSkip", SqlDbType.Char, 1, m.CanSkip),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(VST_VisitTemplateDetail m)
        {
            m.TemplateID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TemplateID", SqlDbType.Int, 4, m.TemplateID),
				SQLDatabase.MakeInParam("@ProcessID", SqlDbType.Int, 4, m.ProcessID),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@CanSkip", SqlDbType.Char, 1, m.CanSkip),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override VST_VisitTemplateDetail FillDetailModel(IDataReader dr)
        {
            VST_VisitTemplateDetail m = new VST_VisitTemplateDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["TemplateID"].ToString()))	m.TemplateID = (int)dr["TemplateID"];
			if (!string.IsNullOrEmpty(dr["ProcessID"].ToString()))	m.ProcessID = (int)dr["ProcessID"];
			if (!string.IsNullOrEmpty(dr["SortID"].ToString()))	m.SortID = (int)dr["SortID"];
			if (!string.IsNullOrEmpty(dr["CanSkip"].ToString()))	m.CanSkip = (string)dr["CanSkip"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }
    }
}

