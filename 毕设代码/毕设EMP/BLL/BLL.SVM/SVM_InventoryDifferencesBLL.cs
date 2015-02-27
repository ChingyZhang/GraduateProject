
// ===================================================================
// 文件： SVM_InventoryDifferencesDAL.cs
// 项目名称：
// 创建时间：2011/12/14
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.SVM;
using MCSFramework.SQLDAL.SVM;

namespace MCSFramework.BLL.SVM
{
	/// <summary>
	///SVM_InventoryDifferencesBLL业务逻辑BLL类
	/// </summary>
	public class SVM_InventoryDifferencesBLL : BaseComplexBLL<SVM_InventoryDifferences,SVM_InventoryDifferences_Detail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_InventoryDifferencesDAL";
        private SVM_InventoryDifferencesDAL _dal;
		
		#region 构造函数
		///<summary>
		///SVM_InventoryDifferencesBLL
		///</summary>
		public SVM_InventoryDifferencesBLL()
			: base(DALClassName)
		{
			_dal = (SVM_InventoryDifferencesDAL)_DAL;
            _m = new SVM_InventoryDifferences(); 
		}
		
		public SVM_InventoryDifferencesBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_InventoryDifferencesDAL)_DAL;
            FillModel(id);
        }

        public SVM_InventoryDifferencesBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_InventoryDifferencesDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<SVM_InventoryDifferences> GetModelList(string condition)
        {
            return new SVM_InventoryDifferencesBLL()._GetModelList(condition);
        }
		#endregion

        public void Approve(int StaffID)
        {
            _dal.Approve(StaffID);
        }

        public void Cancel_Approve(int StaffID)
        {
            _dal.Cancel_Approve(StaffID);
        }

        public decimal GetTotalFactoryPriceValue()
        {
            return _dal.GetTotalFactoryPriceValue();
        }

        /// <summary>
        /// 初始化客户库存产品列表
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="ClientID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int InitProductList(int Month, int ClientID, DateTime InventoryDate, int Staff, bool IsCXP)
        {
            SVM_InventoryDifferencesDAL dal = (SVM_InventoryDifferencesDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitProductList(Month, ClientID, InventoryDate, Staff, IsCXP);
        }

        public static DataTable GetSummary(int OrganizeCity, int ClientID, int beginMonth, int endMonth, int ClientType)
        {
            SVM_InventoryDifferencesDAL dal = (SVM_InventoryDifferencesDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, ClientID, beginMonth, endMonth, ClientType));
        }
        public decimal GetOPIInventory()
        {
            SVM_InventoryDifferencesDAL dal = new SVM_InventoryDifferencesDAL();
            if (this.Model.ID > 0)
                return dal.GetOPIInventory(this.Model.AccountMonth-1, this.Model.Client);
            else return 0;
        }
	}
}