
// ===================================================================
// 文件： INV_ReportedLossDAL.cs
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
	///INV_ReportedLossBLL业务逻辑BLL类
	/// </summary>
	public class INV_ReportedLossBLL : BaseSimpleBLL<INV_ReportedLoss>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_ReportedLossDAL";
        private INV_ReportedLossDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_ReportedLossBLL
		///</summary>
		public INV_ReportedLossBLL()
			: base(DALClassName)
		{
			_dal = (INV_ReportedLossDAL)_DAL;
            _m = new INV_ReportedLoss(); 
		}
		
		public INV_ReportedLossBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_ReportedLossDAL)_DAL;
            FillModel(id);
        }

        public INV_ReportedLossBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_ReportedLossDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_ReportedLoss> GetModelList(string condition)
        {
            return new INV_ReportedLossBLL()._GetModelList(condition);
        }
		#endregion

        #region 确认审核报损单
        /// <summary>
        /// 确认审核报损单
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:报损单不为备单状态 -2：无需要报损的产品码 -3：要报损的产品码中有部分已不在本仓库或不为在库状态</returns>
        public int Approve(Guid User)
        {
            return _dal.Approve(_m.ID, User);
        }
        #endregion

        #region 逐码扫描新增报损产品(按物流码)
        /// <summary>
        /// 逐码扫描新增报损产品(按物流码)
        /// </summary>
        /// <param name="LossID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:报损单不为备单状态 -2:物流码无效，或不在本仓库中</returns>
        public static int LossByOneCode(int LossID,string Code)
        {
            INV_ReportedLossDAL dal = (INV_ReportedLossDAL)DataAccess.CreateObject(DALClassName);
            return dal.LossByOneCode(LossID, Code);
        }
        #endregion
        
	}
}