
// ===================================================================
// 文件： PM_StdInsuranceCostDAL.cs
// 项目名称：
// 创建时间：2011/10/21
// 作者:	   
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
	///PM_StdInsuranceCostBLL业务逻辑BLL类
	/// </summary>
	public class PM_StdInsuranceCostBLL : BaseSimpleBLL<PM_StdInsuranceCost>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_StdInsuranceCostDAL";
        private PM_StdInsuranceCostDAL _dal;
		
		#region 构造函数
		///<summary>
		///PM_StdInsuranceCostBLL
		///</summary>
		public PM_StdInsuranceCostBLL()
			: base(DALClassName)
		{
			_dal = (PM_StdInsuranceCostDAL)_DAL;
            _m = new PM_StdInsuranceCost(); 
		}
		
		public PM_StdInsuranceCostBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_StdInsuranceCostDAL)_DAL;
            FillModel(id);
        }

        public PM_StdInsuranceCostBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_StdInsuranceCostDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PM_StdInsuranceCost> GetModelList(string condition)
        {
            return new PM_StdInsuranceCostBLL()._GetModelList(condition);
        }
		#endregion
	}
}