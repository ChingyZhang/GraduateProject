
// ===================================================================
// 文件： Rpt_ReportChartsDAL.cs
// 项目名称：
// 创建时间：2010/9/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;

namespace MCSFramework.BLL.RPT
{
	/// <summary>
	///Rpt_ReportChartsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_ReportChartsBLL : BaseSimpleBLL<Rpt_ReportCharts>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportChartsDAL";
        private Rpt_ReportChartsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_ReportChartsBLL
		///</summary>
		public Rpt_ReportChartsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_ReportChartsDAL)_DAL;
            _m = new Rpt_ReportCharts(); 
		}
		
		public Rpt_ReportChartsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportChartsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportChartsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportChartsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_ReportCharts> GetModelList(string condition)
        {
            return new Rpt_ReportChartsBLL()._GetModelList(condition);
        }
		#endregion
	}
}