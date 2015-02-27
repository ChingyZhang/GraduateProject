
// ===================================================================
// 文件： FNA_ClientPaymentForcastDAL.cs
// 项目名称：
// 创建时间：2011/4/13
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
	///FNA_ClientPaymentForcastBLL业务逻辑BLL类
	/// </summary>
	public class FNA_ClientPaymentForcastBLL : BaseSimpleBLL<FNA_ClientPaymentForcast>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_ClientPaymentForcastDAL";
        private FNA_ClientPaymentForcastDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_ClientPaymentForcastBLL
		///</summary>
		public FNA_ClientPaymentForcastBLL()
			: base(DALClassName)
		{
			_dal = (FNA_ClientPaymentForcastDAL)_DAL;
            _m = new FNA_ClientPaymentForcast(); 
		}
		
		public FNA_ClientPaymentForcastBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_ClientPaymentForcastDAL)_DAL;
            FillModel(id);
        }

        public FNA_ClientPaymentForcastBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_ClientPaymentForcastDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_ClientPaymentForcast> GetModelList(string condition)
        {
            return new FNA_ClientPaymentForcastBLL()._GetModelList(condition);
        }
		#endregion


        public static void Init(int OrganizeCity, int Client)
        {
            FNA_ClientPaymentForcastDAL dal = (FNA_ClientPaymentForcastDAL)DataAccess.CreateObject(DALClassName);
            dal.Init(OrganizeCity, Client);
        }
	}
}