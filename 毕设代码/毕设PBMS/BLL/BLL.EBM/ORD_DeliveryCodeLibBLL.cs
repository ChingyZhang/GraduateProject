
// ===================================================================
// 文件： ORD_DeliveryCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
	/// <summary>
	///ORD_DeliveryCodeLibBLL业务逻辑BLL类
	/// </summary>
	public class ORD_DeliveryCodeLibBLL : BaseSimpleBLL<ORD_DeliveryCodeLib>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.ORD_DeliveryCodeLibDAL";
        private ORD_DeliveryCodeLibDAL _dal;
		
		#region 构造函数
		///<summary>
		///ORD_DeliveryCodeLibBLL
		///</summary>
		public ORD_DeliveryCodeLibBLL()
			: base(DALClassName)
		{
			_dal = (ORD_DeliveryCodeLibDAL)_DAL;
            _m = new ORD_DeliveryCodeLib(); 
		}

        public ORD_DeliveryCodeLibBLL(long id)
            : base(DALClassName)
        {
            _dal = (ORD_DeliveryCodeLibDAL)_DAL;
            FillModel(id);
        }

        public ORD_DeliveryCodeLibBLL(long id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_DeliveryCodeLibDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<ORD_DeliveryCodeLib> GetModelList(string condition)
        {
            return new ORD_DeliveryCodeLibBLL()._GetModelList(condition);
        }
		#endregion
	}
}