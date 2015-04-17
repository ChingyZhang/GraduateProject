
// ===================================================================
// 文件： AC_CashAccountDAL.cs
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
	///AC_CashAccountBLL业务逻辑BLL类
	/// </summary>
	public class AC_CashAccountBLL : BaseSimpleBLL<AC_CashAccount>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.AC_CashAccountDAL";
        private AC_CashAccountDAL _dal;
		
		#region 构造函数
		///<summary>
		///AC_CashAccountBLL
		///</summary>
		public AC_CashAccountBLL()
			: base(DALClassName)
		{
			_dal = (AC_CashAccountDAL)_DAL;
            _m = new AC_CashAccount(); 
		}
		
		public AC_CashAccountBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_CashAccountDAL)_DAL;
            FillModel(id);
        }

        public AC_CashAccountBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_CashAccountDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<AC_CashAccount> GetModelList(string condition)
        {
            return new AC_CashAccountBLL()._GetModelList(condition);
        }
		#endregion

        
	}
}