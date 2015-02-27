
// ===================================================================
// 文件： ORD_OrderLimitFactorDAL.cs
// 项目名称：
// 创建时间：2010/12/8
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Logistics;
using MCSFramework.SQLDAL.Logistics;

namespace MCSFramework.BLL.Logistics
{
	/// <summary>
	///ORD_OrderLimitFactorBLL业务逻辑BLL类
	/// </summary>
	public class ORD_OrderLimitFactorBLL : BaseSimpleBLL<ORD_OrderLimitFactor>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_OrderLimitFactorDAL";
        private ORD_OrderLimitFactorDAL _dal;
		
		#region 构造函数
		///<summary>
		///ORD_OrderLimitFactorBLL
		///</summary>
		public ORD_OrderLimitFactorBLL()
			: base(DALClassName)
		{
			_dal = (ORD_OrderLimitFactorDAL)_DAL;
            _m = new ORD_OrderLimitFactor(); 
		}
		
		public ORD_OrderLimitFactorBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_OrderLimitFactorDAL)_DAL;
            FillModel(id);
        }

        public ORD_OrderLimitFactorBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_OrderLimitFactorDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<ORD_OrderLimitFactor> GetModelList(string condition)
        {
            return new ORD_OrderLimitFactorBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetLimitInfo(int month, int Client)
        {
            ORD_OrderLimitFactorDAL dal = (ORD_OrderLimitFactorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetLimitInfo(month, Client);
        }
	}
}