
// ===================================================================
// 文件： Rpt_DataSourceDAL.cs
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
	///Rpt_DataSourceBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_DataSourceBLL : BaseSimpleBLL<Rpt_DataSource>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSourceDAL";
        private Rpt_DataSourceDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_DataSourceBLL
		///</summary>
		public Rpt_DataSourceBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_DataSourceDAL)_DAL;
            _m = new Rpt_DataSource(); 
		}
		
		public Rpt_DataSourceBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSourceDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSourceBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSourceDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_DataSource> GetModelList(string condition)
        {
            return new Rpt_DataSourceBLL()._GetModelList(condition);
        }
		#endregion
	}
}