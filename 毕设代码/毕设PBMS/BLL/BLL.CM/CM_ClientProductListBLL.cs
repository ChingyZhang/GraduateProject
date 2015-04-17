
// ===================================================================
// 文件： CM_ClientProductListDAL.cs
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
	///CM_ClientProductListBLL业务逻辑BLL类
	/// </summary>
	public class CM_ClientProductListBLL : BaseSimpleBLL<CM_ClientProductList>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientProductListDAL";
        private CM_ClientProductListDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_ClientProductListBLL
		///</summary>
		public CM_ClientProductListBLL()
			: base(DALClassName)
		{
			_dal = (CM_ClientProductListDAL)_DAL;
            _m = new CM_ClientProductList(); 
		}
		
		public CM_ClientProductListBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ClientProductListDAL)_DAL;
            FillModel(id);
        }

        public CM_ClientProductListBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ClientProductListDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_ClientProductList> GetModelList(string condition)
        {
            return new CM_ClientProductListBLL()._GetModelList(condition);
        }
		#endregion
	}
}