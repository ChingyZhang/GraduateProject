
// ===================================================================
// 文件： SVM_ClientSalesTargetDAL.cs
// 项目名称：
// 创建时间：2013/9/25
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.SVM;
using MCSFramework.SQLDAL.SVM;

namespace MCSFramework.BLL.SVM
{
	/// <summary>
	///SVM_ClientSalesTargetBLL业务逻辑BLL类
	/// </summary>
	public class SVM_ClientSalesTargetBLL : BaseSimpleBLL<SVM_ClientSalesTarget>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_ClientSalesTargetDAL";
        private SVM_ClientSalesTargetDAL _dal;
		
		#region 构造函数
		///<summary>
		///SVM_ClientSalesTargetBLL
		///</summary>
		public SVM_ClientSalesTargetBLL()
			: base(DALClassName)
		{
			_dal = (SVM_ClientSalesTargetDAL)_DAL;
            _m = new SVM_ClientSalesTarget(); 
		}
		
		public SVM_ClientSalesTargetBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_ClientSalesTargetDAL)_DAL;
            FillModel(id);
        }

        public SVM_ClientSalesTargetBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_ClientSalesTargetDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<SVM_ClientSalesTarget> GetModelList(string condition)
        {
            return new SVM_ClientSalesTargetBLL()._GetModelList(condition);
        }
		#endregion
	}
}