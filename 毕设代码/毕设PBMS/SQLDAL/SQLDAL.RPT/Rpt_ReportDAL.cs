
// ===================================================================
// 文件： Rpt_ReportDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;


namespace MCSFramework.SQLDAL.RPT
{
    /// <summary>
    ///Rpt_Report数据访问DAL类
    /// </summary>
    public class Rpt_ReportDAL : BaseSimpleDAL<Rpt_Report>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_ReportDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_Report";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_Report m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
                SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@ReportType", SqlDbType.Int, 4, m.ReportType),
				SQLDatabase.MakeInParam("@AddRowTotal", SqlDbType.Char, 1, m.AddRowTotal),
				SQLDatabase.MakeInParam("@AddColumnTotal", SqlDbType.Char, 1, m.AddColumnTotal),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);


        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_Report m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
                SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@ReportType", SqlDbType.Int, 4, m.ReportType),
				SQLDatabase.MakeInParam("@AddRowTotal", SqlDbType.Char, 1, m.AddRowTotal),
				SQLDatabase.MakeInParam("@AddColumnTotal", SqlDbType.Char, 1, m.AddColumnTotal),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_Report FillModel(IDataReader dr)
        {
            Rpt_Report m = new Rpt_Report();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Folder"].ToString())) m.Folder = (int)dr["Folder"];
            if (!string.IsNullOrEmpty(dr["DataSet"].ToString())) m.DataSet = (Guid)dr["DataSet"];
            if (!string.IsNullOrEmpty(dr["Title"].ToString())) m.Title = (string)dr["Title"];
            if (!string.IsNullOrEmpty(dr["ReportType"].ToString())) m.ReportType = (int)dr["ReportType"];
            if (!string.IsNullOrEmpty(dr["AddRowTotal"].ToString())) m.AddRowTotal = (string)dr["AddRowTotal"];
            if (!string.IsNullOrEmpty(dr["AddColumnTotal"].ToString())) m.AddColumnTotal = (string)dr["AddColumnTotal"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
        /// <summary>
        /// 获取常用报表
        /// </summary>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public SqlDataReader GetFrequentByStaff(int Staff)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, Staff)};
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetFrequentByStaff", prams, out dr);
            return dr;
        }

        #region 报表浏览记录维护
        /// <summary>
        /// 增加浏览次数
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public void AddViewTimes(Guid ReportID, int StaffID)
        {

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ReportID", SqlDbType.UniqueIdentifier, 16, ReportID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
				 
			};
            #endregion

            SQLDatabase.RunProc("MCS_Reports.dbo.sp_Rpt_ReprotViewHistory_AddViewTimes", prams);
        }

        /// <summary>
        ///清除浏览次数
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public void ClearViewTimes(Guid ReportID, int StaffID)
        {

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ReportID", SqlDbType.UniqueIdentifier, 16, ReportID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
				 
			};
            #endregion

            SQLDatabase.RunProc("MCS_Reports.dbo.sp_Rpt_ReprotViewHistory_ClearViewTimes", prams);
        }

        /// <summary>
        ///清除浏览记录
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="StaffID"></param>
        public void DeleteViewTimes(Guid ReportID, int StaffID)
        {

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ReportID", SqlDbType.UniqueIdentifier, 16, ReportID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
				 
			};
            #endregion

            SQLDatabase.RunProc("MCS_Reports.dbo.sp_Rpt_ReprotViewHistory_Delete", prams);
        }

        #endregion
    }
}

