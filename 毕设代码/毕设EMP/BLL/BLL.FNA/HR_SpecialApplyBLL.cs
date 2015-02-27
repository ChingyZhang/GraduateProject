
// ===================================================================
// 文件： HR_SpecialApplyDAL.cs
// 项目名称：
// 创建时间：2011/1/5
// 作者:	   MDF
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.FNA;
using MCSFramework.SQLDAL.FNA;

namespace MCSFramework.BLL.FNA
{
	/// <summary>
	///HR_SpecialApplyBLL业务逻辑BLL类
	/// </summary>
	public class HR_SpecialApplyBLL : BaseSimpleBLL<HR_SpecialApply>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.HR_SpecialApplyDAL";
        private HR_SpecialApplyDAL _dal;
		
		#region 构造函数
		///<summary>
		///HR_SpecialApplyBLL
		///</summary>
		public HR_SpecialApplyBLL()
			: base(DALClassName)
		{
			_dal = (HR_SpecialApplyDAL)_DAL;
            _m = new HR_SpecialApply(); 
		}
		
		public HR_SpecialApplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (HR_SpecialApplyDAL)_DAL;
            FillModel(id);
        }

        public HR_SpecialApplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (HR_SpecialApplyDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<HR_SpecialApply> GetModelList(string condition)
        {
            return new HR_SpecialApplyBLL()._GetModelList(condition);
        }
		#endregion
	}
}