
// ===================================================================
// 文件： Rpt_ReportGridColumnsDAL.cs
// 项目名称：
// 创建时间：2010/9/25
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
	///Rpt_ReportGridColumnsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_ReportGridColumnsBLL : BaseSimpleBLL<Rpt_ReportGridColumns>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportGridColumnsDAL";
        private Rpt_ReportGridColumnsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_ReportGridColumnsBLL
		///</summary>
		public Rpt_ReportGridColumnsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_ReportGridColumnsDAL)_DAL;
            _m = new Rpt_ReportGridColumns(); 
		}

        public Rpt_ReportGridColumnsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportGridColumnsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportGridColumnsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportGridColumnsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_ReportGridColumns> GetModelList(string condition)
        {
            return new Rpt_ReportGridColumnsBLL()._GetModelList(condition);
        }
		#endregion
	}
}