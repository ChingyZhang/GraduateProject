
// ===================================================================
// 文件： Rpt_DataSetTablesDAL.cs
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
	///Rpt_DataSetTablesBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_DataSetTablesBLL : BaseSimpleBLL<Rpt_DataSetTables>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSetTablesDAL";
        private Rpt_DataSetTablesDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_DataSetTablesBLL
		///</summary>
		public Rpt_DataSetTablesBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_DataSetTablesDAL)_DAL;
            _m = new Rpt_DataSetTables(); 
		}

        public Rpt_DataSetTablesBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetTablesDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSetTablesBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetTablesDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_DataSetTables> GetModelList(string condition)
        {
            return new Rpt_DataSetTablesBLL()._GetModelList(condition);
        }
		#endregion
	}
}