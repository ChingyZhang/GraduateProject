
// ===================================================================
// 文件： PM_PromotorInRetailerDAL.cs
// 项目名称：
// 创建时间：2009-4-29
// 作者:	   shh
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
	///PM_PromotorInRetailerBLL业务逻辑BLL类
	/// </summary>
	public class PM_PromotorInRetailerBLL : BaseSimpleBLL<PM_PromotorInRetailer>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_PromotorInRetailerDAL";
        private PM_PromotorInRetailerDAL _dal;
		
		#region 构造函数
		///<summary>
		///PM_PromotorInRetailerBLL
		///</summary>
		public PM_PromotorInRetailerBLL()
			: base(DALClassName)
		{
			_dal = (PM_PromotorInRetailerDAL)_DAL;
            _m = new PM_PromotorInRetailer(); 
		}
		
		public PM_PromotorInRetailerBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_PromotorInRetailerDAL)_DAL;
            FillModel(id);
        }

        public PM_PromotorInRetailerBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_PromotorInRetailerDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PM_PromotorInRetailer> GetModelList(string condition)
        {
            return new PM_PromotorInRetailerBLL()._GetModelList(condition);
        }
		#endregion
	}
}