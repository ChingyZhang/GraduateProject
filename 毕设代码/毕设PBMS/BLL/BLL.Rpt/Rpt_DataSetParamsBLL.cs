
// ===================================================================
// 文件： Rpt_DataSetParamsDAL.cs
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
	///Rpt_DataSetParamsBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_DataSetParamsBLL : BaseSimpleBLL<Rpt_DataSetParams>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_DataSetParamsDAL";
        private Rpt_DataSetParamsDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_DataSetParamsBLL
		///</summary>
		public Rpt_DataSetParamsBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_DataSetParamsDAL)_DAL;
            _m = new Rpt_DataSetParams(); 
		}

        public Rpt_DataSetParamsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetParamsDAL)_DAL;
            FillModel(id);
        }

        public Rpt_DataSetParamsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_DataSetParamsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_DataSetParams> GetModelList(string condition)
        {
            return new Rpt_DataSetParamsBLL()._GetModelList(condition);
        }
		#endregion
	}
}