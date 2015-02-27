
// ===================================================================
// 文件： SVM_InventoryDAL.cs
// 项目名称：
// 创建时间：2009/3/8
// 作者:	   Shen Gang
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
    ///SVM_InventoryBLL业务逻辑BLL类
    /// </summary>
    public class SVM_InventoryBLL : BaseComplexBLL<SVM_Inventory, SVM_Inventory_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_InventoryDAL";
        private SVM_InventoryDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_InventoryBLL
        ///</summary>
        public SVM_InventoryBLL()
            : base(DALClassName)
        {
            _dal = (SVM_InventoryDAL)_DAL;
            _m = new SVM_Inventory();
        }

        public SVM_InventoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_InventoryDAL)_DAL;
            FillModel(id);
        }

        public SVM_InventoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_InventoryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_Inventory> GetModelList(string condition)
        {
            return new SVM_InventoryBLL()._GetModelList(condition);
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

        public static int BatApprove(string IDs, int StaffID)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.BatApprove(IDs, StaffID);
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
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitProductList(Month, ClientID, InventoryDate, Staff, IsCXP);
        }

        public static DataTable GetSummary(int OrganizeCity, int ClientID, int beginMonth, int endMonth, int ClientType)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, ClientID, beginMonth, endMonth, ClientType));
        }
        public static DataTable GetSummaryTotal(int organizecity, int accountmonth, int clienttype, int level, int state, int iscxp, int staff)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummaryTotal(organizecity, accountmonth, clienttype, level, state, iscxp, staff));
        }
        /// <summary>
        /// 返回-1时,该员工下还有进货或销量未填写完成.
        /// </summary>
        /// <param name="Staff"></param>
        /// <param name="accountmonth"></param>
        /// <param name="clienttype"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int ApproveByStaff(int Organizecity, int staff, int accountmonth, int clienttype, int iscxp)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ApproveByStaff(Organizecity, staff, accountmonth, clienttype, iscxp);
        }

        public static int SubmitByStaff(int Organizecity, int staff, int accountmonth, int clienttype, int iscxp)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.SubmitByStaff(Organizecity, staff, accountmonth, clienttype, iscxp);
        }

        public static DataTable GetOPIOverview(int organizecity, int accountmonth, int IsOpponent, int ActiveFlag)
        {
            SVM_InventoryDAL dal = (SVM_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetOPIOverview(organizecity, accountmonth, IsOpponent, ActiveFlag));
        }

    }
}