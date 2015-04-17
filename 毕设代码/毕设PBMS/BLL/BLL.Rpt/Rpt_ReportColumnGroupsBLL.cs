
// ===================================================================
// 文件： Rpt_ReportColumnGroupsDAL.cs
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
	///Rpt_ReportColumnGroupsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_ReportColumnGroupsBLL : BaseSimpleBLL<Rpt_ReportColumnGroups>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_ReportColumnGroupsDAL";
        private Rpt_ReportColumnGroupsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_ReportColumnGroupsBLL
		///</summary>
		public Rpt_ReportColumnGroupsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_ReportColumnGroupsDAL)_DAL;
            _m = new Rpt_ReportColumnGroups(); 
		}

        public Rpt_ReportColumnGroupsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportColumnGroupsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_ReportColumnGroupsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_ReportColumnGroupsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_ReportColumnGroups> GetModelList(string condition)
        {
            return new Rpt_ReportColumnGroupsBLL()._GetModelList(condition);
        }
		#endregion
	}
}