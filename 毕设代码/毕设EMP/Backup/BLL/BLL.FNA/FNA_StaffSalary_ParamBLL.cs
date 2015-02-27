
// ===================================================================
// 文件： FNA_StaffSalary_ParamDAL.cs
// 项目名称：
// 创建时间：2014/2/18
// 作者:	   chf
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
	///FNA_StaffSalary_ParamBLL业务逻辑BLL类
	/// </summary>
	public class FNA_StaffSalary_ParamBLL : BaseSimpleBLL<FNA_StaffSalary_Param>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffSalary_ParamDAL";
        private FNA_StaffSalary_ParamDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_StaffSalary_ParamBLL
		///</summary>
		public FNA_StaffSalary_ParamBLL()
			: base(DALClassName)
		{
			_dal = (FNA_StaffSalary_ParamDAL)_DAL;
            _m = new FNA_StaffSalary_Param(); 
		}
		
		public FNA_StaffSalary_ParamBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalary_ParamDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffSalary_ParamBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalary_ParamDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_StaffSalary_Param> GetModelList(string condition)
        {
            return new FNA_StaffSalary_ParamBLL()._GetModelList(condition);
        }
		#endregion
	}
}