
// ===================================================================
// 文件： PM_SalaryLevelDAL.cs
// 项目名称：
// 创建时间：2009/3/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;

namespace MCSFramework.BLL.Promotor
{
	/// <summary>
	///PM_SalaryLevelBLL业务逻辑BLL类
	/// </summary>
	public class PM_SalaryLevelBLL : BaseComplexBLL<PM_SalaryLevel,PM_SalaryLevelDetail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_SalaryLevelDAL";
        private PM_SalaryLevelDAL _dal;
		
		#region 构造函数
		///<summary>
		///PM_SalaryLevelBLL
		///</summary>
		public PM_SalaryLevelBLL()
			: base(DALClassName)
		{
			_dal = (PM_SalaryLevelDAL)_DAL;
            _m = new PM_SalaryLevel(); 
		}
		
		public PM_SalaryLevelBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_SalaryLevelDAL)_DAL;
            FillModel(id);
        }

        public PM_SalaryLevelBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_SalaryLevelDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PM_SalaryLevel> GetModelList(string condition)
        {
            return new PM_SalaryLevelBLL()._GetModelList(condition);
        }
		#endregion
	}
}