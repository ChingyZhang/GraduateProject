
// ===================================================================
// 文件： Rpt_DataSetTableRelationsDAL.cs
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
	///Rpt_DataSetTableRelationsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_DataSetTableRelationsBLL : BaseSimpleBLL<Rpt_DataSetTableRelations>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSetTableRelationsDAL";
        private Rpt_DataSetTableRelationsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_DataSetTableRelationsBLL
		///</summary>
		public Rpt_DataSetTableRelationsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_DataSetTableRelationsDAL)_DAL;
            _m = new Rpt_DataSetTableRelations(); 
		}

        public Rpt_DataSetTableRelationsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetTableRelationsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSetTableRelationsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetTableRelationsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_DataSetTableRelations> GetModelList(string condition)
        {
            return new Rpt_DataSetTableRelationsBLL()._GetModelList(condition);
        }
		#endregion
	}
}