
// ===================================================================
// 文件： FNA_ClientPaymentInfoDAL.cs
// 项目名称：
// 创建时间：2009/2/22
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
	///FNA_ClientPaymentInfoBLL业务逻辑BLL类
	/// </summary>
	public class FNA_ClientPaymentInfoBLL : BaseSimpleBLL<FNA_ClientPaymentInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_ClientPaymentInfoDAL";
        private FNA_ClientPaymentInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_ClientPaymentInfoBLL
		///</summary>
		public FNA_ClientPaymentInfoBLL()
			: base(DALClassName)
		{
			_dal = (FNA_ClientPaymentInfoDAL)_DAL;
            _m = new FNA_ClientPaymentInfo(); 
		}
		
		public FNA_ClientPaymentInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_ClientPaymentInfoDAL)_DAL;
            FillModel(id);
        }

        public FNA_ClientPaymentInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_ClientPaymentInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_ClientPaymentInfo> GetModelList(string condition)
        {
            return new FNA_ClientPaymentInfoBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 确认客户回款到账
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="confirmdate"></param>
        /// <returns></returns>
        public int Confirm(int staff, DateTime confirmdate)
        {
            return _dal.Confirm(_m.ID, staff, confirmdate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accountmounth"></param>
        /// <returns></returns>
        public decimal GetByClientDate(int client, int accountmounth)
        {
            return _dal.GetByClientDate(client, accountmounth);
        }


        public static void Init(int OrganizeCity, int Client)
        {
            FNA_ClientPaymentInfoDAL dal = (FNA_ClientPaymentInfoDAL)DataAccess.CreateObject(DALClassName);
            dal.Init(OrganizeCity, Client);
        }

        public int CancleConfirm(int staff, DateTime confirmdate)
        {
            return _dal.CancleConfirm(_m.ID, staff, confirmdate);
        }
	}
}