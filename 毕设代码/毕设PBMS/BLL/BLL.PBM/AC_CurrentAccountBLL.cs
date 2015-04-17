
// ===================================================================
// 文件： AC_CurrentAccountDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
	/// <summary>
	///AC_CurrentAccountBLL业务逻辑BLL类
	/// </summary>
	public class AC_CurrentAccountBLL : BaseSimpleBLL<AC_CurrentAccount>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.AC_CurrentAccountDAL";
        private AC_CurrentAccountDAL _dal;
		
		#region 构造函数
		///<summary>
		///AC_CurrentAccountBLL
		///</summary>
		public AC_CurrentAccountBLL()
			: base(DALClassName)
		{
			_dal = (AC_CurrentAccountDAL)_DAL;
            _m = new AC_CurrentAccount(); 
		}
		
		public AC_CurrentAccountBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_CurrentAccountDAL)_DAL;
            FillModel(id);
        }

        public AC_CurrentAccountBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_CurrentAccountDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<AC_CurrentAccount> GetModelList(string condition)
        {
            return new AC_CurrentAccountBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取指定客户的账户余额信息
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="TradeClient"></param>
        /// <returns></returns>
        public static AC_CurrentAccount GetByTradeClient(int OwnerClient, int TradeClient)
        {
            IList<AC_CurrentAccount> acc = AC_CurrentAccountBLL.GetModelList(
                string.Format("OwnerClient={0} AND TradeClient={1}", OwnerClient, TradeClient));

            if (acc == null || acc.Count == 0)
                return null;
            else
                return acc[0];
        }
	}
}