
// ===================================================================
// 文件： VST_RouteDAL.cs
// 项目名称：
// 创建时间：2015-03-24
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.VST;
using System.Collections;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.VST
{
	/// <summary>
	///VST_Route数据访问DAL类
	/// </summary>
	public class VST_RouteDAL : BaseSimpleDAL<VST_Route>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_RouteDAL()
		{
			_ProcePrefix = "MCS_VST.dbo.sp_VST_Route";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_Route m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
				SQLDatabase.MakeInParam("@FirstVisitDate", SqlDbType.DateTime, 8, m.FirstVisitDate),
				SQLDatabase.MakeInParam("@IsMustSequenceVisit", SqlDbType.Char, 1, m.IsMustSequenceVisit),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
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
        public override int Update(VST_Route m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
				SQLDatabase.MakeInParam("@FirstVisitDate", SqlDbType.DateTime, 8, m.FirstVisitDate),
				SQLDatabase.MakeInParam("@IsMustSequenceVisit", SqlDbType.Char, 1, m.IsMustSequenceVisit),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@OwnerType", SqlDbType.Int, 4, m.OwnerType),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override VST_Route FillModel(IDataReader dr)
		{
			VST_Route m = new VST_Route();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["VisitCycle"].ToString()))	m.VisitCycle = (int)dr["VisitCycle"];
			if (!string.IsNullOrEmpty(dr["VisitDay"].ToString()))	m.VisitDay = (int)dr["VisitDay"];
			if (!string.IsNullOrEmpty(dr["FirstVisitDate"].ToString()))	m.FirstVisitDate = (DateTime)dr["FirstVisitDate"];
			if (!string.IsNullOrEmpty(dr["IsMustSequenceVisit"].ToString()))	m.IsMustSequenceVisit = (string)dr["IsMustSequenceVisit"];
			if (!string.IsNullOrEmpty(dr["EnableFlag"].ToString()))	m.EnableFlag = (string)dr["EnableFlag"];
			if (!string.IsNullOrEmpty(dr["OwnerType"].ToString()))	m.OwnerType = (int)dr["OwnerType"];
			if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString()))	m.OwnerClient = (int)dr["OwnerClient"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 获取指定区域内的线路列表
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public IList<VST_Route> GetByOrganizeCity(int OrganizeCity)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetByOrganizeCity", prams,out dr);

            return FillModelList(dr);
			
        }
    }
}

