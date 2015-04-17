
// ===================================================================
// 文件： Rpt_DataSetFieldsDAL.cs
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
	///Rpt_DataSetFieldsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_DataSetFieldsBLL : BaseSimpleBLL<Rpt_DataSetFields>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSetFieldsDAL";
        private Rpt_DataSetFieldsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_DataSetFieldsBLL
		///</summary>
		public Rpt_DataSetFieldsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_DataSetFieldsDAL)_DAL;
            _m = new Rpt_DataSetFields(); 
		}
		
		public Rpt_DataSetFieldsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetFieldsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSetFieldsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetFieldsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_DataSetFields> GetModelList(string condition)
        {
            return new Rpt_DataSetFieldsBLL()._GetModelList(condition);
        }
		#endregion
	}
}