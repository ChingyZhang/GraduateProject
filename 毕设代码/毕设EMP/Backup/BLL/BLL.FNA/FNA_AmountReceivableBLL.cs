
// ===================================================================
// 文件： FNA_AmountReceivableDAL.cs
// 项目名称：
// 创建时间：2009/5/16
// 作者:	   Shen Gang
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
	///FNA_AmountReceivableBLL业务逻辑BLL类
	/// </summary>
	public class FNA_AmountReceivableBLL : BaseSimpleBLL<FNA_AmountReceivable>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_AmountReceivableDAL";
        private FNA_AmountReceivableDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_AmountReceivableBLL
		///</summary>
		public FNA_AmountReceivableBLL()
			: base(DALClassName)
		{
			_dal = (FNA_AmountReceivableDAL)_DAL;
            _m = new FNA_AmountReceivable(); 
		}
		
		public FNA_AmountReceivableBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_AmountReceivableDAL)_DAL;
            FillModel(id);
        }

        public FNA_AmountReceivableBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_AmountReceivableDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_AmountReceivable> GetModelList(string condition)
        {
            return new FNA_AmountReceivableBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 变更应收账款
        /// </summary>
        /// <param name="client"></param>
        /// <param name="changeamount"></param>
        /// <param name="changetype"></param>
        /// <param name="changestaff"></param>
        /// <param name="relateinfo"></param>
        /// <returns></returns>
        public static int Change(int client, decimal changeamount, int changetype, int changestaff, string relateinfo)
        {
            FNA_AmountReceivableDAL dal = (FNA_AmountReceivableDAL)DataAccess.CreateObject(DALClassName);
            return dal.Change(client, changeamount, changetype, changestaff, relateinfo);
        }
        
        /// <summary>
        /// 返回指定客户当前应收账款余额
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static decimal GetAmountByClient(int client)
        {
            FNA_AmountReceivableDAL dal = (FNA_AmountReceivableDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAmountByClient(client);
        }

        
	}
}