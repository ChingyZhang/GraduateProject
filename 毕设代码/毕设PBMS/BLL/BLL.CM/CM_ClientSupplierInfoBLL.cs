
// ===================================================================
// 文件： CM_ClientSupplierInfoDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
	/// <summary>
	///CM_ClientSupplierInfoBLL业务逻辑BLL类
	/// </summary>
	public class CM_ClientSupplierInfoBLL : BaseSimpleBLL<CM_ClientSupplierInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientSupplierInfoDAL";
        private CM_ClientSupplierInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_ClientSupplierInfoBLL
		///</summary>
		public CM_ClientSupplierInfoBLL()
			: base(DALClassName)
		{
			_dal = (CM_ClientSupplierInfoDAL)_DAL;
            _m = new CM_ClientSupplierInfo(); 
		}
		
		public CM_ClientSupplierInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ClientSupplierInfoDAL)_DAL;
            FillModel(id);
        }

        public CM_ClientSupplierInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ClientSupplierInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_ClientSupplierInfo> GetModelList(string condition)
        {
            return new CM_ClientSupplierInfoBLL()._GetModelList(condition);
        }
		#endregion
	}
}