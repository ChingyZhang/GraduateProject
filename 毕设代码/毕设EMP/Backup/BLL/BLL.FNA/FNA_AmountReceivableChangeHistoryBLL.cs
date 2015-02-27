
// ===================================================================
// 文件： FNA_AmountReceivableChangeHistoryDAL.cs
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
	///FNA_AmountReceivableChangeHistoryBLL业务逻辑BLL类
	/// </summary>
	public class FNA_AmountReceivableChangeHistoryBLL : BaseSimpleBLL<FNA_AmountReceivableChangeHistory>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_AmountReceivableChangeHistoryDAL";
        private FNA_AmountReceivableChangeHistoryDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_AmountReceivableChangeHistoryBLL
		///</summary>
		public FNA_AmountReceivableChangeHistoryBLL()
			: base(DALClassName)
		{
			_dal = (FNA_AmountReceivableChangeHistoryDAL)_DAL;
            _m = new FNA_AmountReceivableChangeHistory(); 
		}
		
		public FNA_AmountReceivableChangeHistoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_AmountReceivableChangeHistoryDAL)_DAL;
            FillModel(id);
        }

        public FNA_AmountReceivableChangeHistoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_AmountReceivableChangeHistoryDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_AmountReceivableChangeHistory> GetModelList(string condition)
        {
            return new FNA_AmountReceivableChangeHistoryBLL()._GetModelList(condition);
        }
		#endregion
	}
}