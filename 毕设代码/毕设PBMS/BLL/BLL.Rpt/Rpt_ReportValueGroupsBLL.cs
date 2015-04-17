
// ===================================================================
// 文件： Rpt_ReportValueGroupsDAL.cs
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
	///Rpt_ReportValueGroupsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_ReportValueGroupsBLL : BaseSimpleBLL<Rpt_ReportValueGroups>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportValueGroupsDAL";
        private Rpt_ReportValueGroupsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_ReportValueGroupsBLL
		///</summary>
		public Rpt_ReportValueGroupsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_ReportValueGroupsDAL)_DAL;
            _m = new Rpt_ReportValueGroups(); 
		}

        public Rpt_ReportValueGroupsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportValueGroupsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportValueGroupsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportValueGroupsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_ReportValueGroups> GetModelList(string condition)
        {
            return new Rpt_ReportValueGroupsBLL()._GetModelList(condition);
        }
		#endregion
	}
}