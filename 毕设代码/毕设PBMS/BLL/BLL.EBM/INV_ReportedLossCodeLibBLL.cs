
// ===================================================================
// 文件： INV_ReportedLossCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-23
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
	///INV_ReportedLossCodeLibBLL业务逻辑BLL类
	/// </summary>
	public class INV_ReportedLossCodeLibBLL : BaseSimpleBLL<INV_ReportedLossCodeLib>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_ReportedLossCodeLibDAL";
        private INV_ReportedLossCodeLibDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_ReportedLossCodeLibBLL
		///</summary>
		public INV_ReportedLossCodeLibBLL()
			: base(DALClassName)
		{
			_dal = (INV_ReportedLossCodeLibDAL)_DAL;
            _m = new INV_ReportedLossCodeLib(); 
		}
		
		public INV_ReportedLossCodeLibBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_ReportedLossCodeLibDAL)_DAL;
            FillModel(id);
        }

        public INV_ReportedLossCodeLibBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_ReportedLossCodeLibDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_ReportedLossCodeLib> GetModelList(string condition)
        {
            return new INV_ReportedLossCodeLibBLL()._GetModelList(condition);
        }
		#endregion
	}
}