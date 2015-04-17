
// ===================================================================
// 文件： Rpt_ReportChartsDAL.cs
// 项目名称：
// 创建时间：2010/9/29
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
    ///Rpt_ReportCharts数据访问DAL类
    /// </summary>
    public class Rpt_ReportChartsDAL : BaseSimpleDAL<Rpt_ReportCharts>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_ReportChartsDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_ReportCharts";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_ReportCharts m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@ChartType", SqlDbType.Int, 4, m.ChartType),
				SQLDatabase.MakeInParam("@ChartSortID", SqlDbType.Int, 4, m.ChartSortID),
				SQLDatabase.MakeInParam("@AxisColumns", SqlDbType.VarChar, 2000, m.AxisColumns),
				SQLDatabase.MakeInParam("@SeriesColumns", SqlDbType.VarChar, 2000, m.SeriesColumns),
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
        public override int Update(Rpt_ReportCharts m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Report", SqlDbType.UniqueIdentifier, 16, m.Report),
				SQLDatabase.MakeInParam("@ChartType", SqlDbType.Int, 4, m.ChartType),
				SQLDatabase.MakeInParam("@ChartSortID", SqlDbType.Int, 4, m.ChartSortID),
				SQLDatabase.MakeInParam("@AxisColumns", SqlDbType.VarChar, 2000, m.AxisColumns),
				SQLDatabase.MakeInParam("@SeriesColumns", SqlDbType.VarChar, 2000, m.SeriesColumns),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_ReportCharts FillModel(IDataReader dr)
        {
            Rpt_ReportCharts m = new Rpt_ReportCharts();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Report"].ToString())) m.Report = (Guid)dr["Report"];
            if (!string.IsNullOrEmpty(dr["ChartType"].ToString())) m.ChartType = (int)dr["ChartType"];
            if (!string.IsNullOrEmpty(dr["ChartSortID"].ToString())) m.ChartSortID = (int)dr["ChartSortID"];
            if (!string.IsNullOrEmpty(dr["AxisColumns"].ToString())) m.AxisColumns = (string)dr["AxisColumns"];
            if (!string.IsNullOrEmpty(dr["SeriesColumns"].ToString())) m.SeriesColumns = (string)dr["SeriesColumns"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

