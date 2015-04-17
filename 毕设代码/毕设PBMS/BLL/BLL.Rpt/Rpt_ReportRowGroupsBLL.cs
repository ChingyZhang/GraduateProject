
// ===================================================================
// 文件： Rpt_ReportRowGroupsDAL.cs
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
	///Rpt_ReportRowGroupsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_ReportRowGroupsBLL : BaseSimpleBLL<Rpt_ReportRowGroups>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportRowGroupsDAL";
        private Rpt_ReportRowGroupsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_ReportRowGroupsBLL
		///</summary>
		public Rpt_ReportRowGroupsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_ReportRowGroupsDAL)_DAL;
            _m = new Rpt_ReportRowGroups(); 
		}

        public Rpt_ReportRowGroupsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportRowGroupsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportRowGroupsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportRowGroupsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_ReportRowGroups> GetModelList(string condition)
        {
            return new Rpt_ReportRowGroupsBLL()._GetModelList(condition);
        }
		#endregion
	}
}