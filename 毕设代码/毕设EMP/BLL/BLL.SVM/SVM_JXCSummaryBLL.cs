
// ===================================================================
// 文件： SVM_JXCSummaryDAL.cs
// 项目名称：
// 创建时间：2010/7/8
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
    ///SVM_JXCSummaryBLL业务逻辑BLL类
    /// </summary>
    public class SVM_JXCSummaryBLL : BaseSimpleBLL<SVM_JXCSummary>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_JXCSummaryDAL";
        private SVM_JXCSummaryDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_JXCSummaryBLL
        ///</summary>
        public SVM_JXCSummaryBLL()
            : base(DALClassName)
        {
            _dal = (SVM_JXCSummaryDAL)_DAL;
            _m = new SVM_JXCSummary();
        }

        public SVM_JXCSummaryBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_JXCSummaryDAL)_DAL;
            FillModel(id);
        }

        public SVM_JXCSummaryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_JXCSummaryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_JXCSummary> GetModelList(string condition)
        {
            return new SVM_JXCSummaryBLL()._GetModelList(condition);
        }
        #endregion

        #region 初始化指定客户的进销存
        /// <summary>
        /// 初始化指定客户的进销存
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static int Init(int Client, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.Init(Client, IsOpponent);
        }

        /// <summary>
        /// 初始化指定客户的进销存
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public static int Init(int Client, int AccountMonth, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.Init(Client, AccountMonth, IsOpponent);
        }
        #endregion

        #region 按客户显示进销存汇总列表
        /// <summary>
        /// 按客户显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="Supplier">供货商</param>
        /// <returns></returns>
        public static DataTable GetSummaryListBySupplier(int BeginMonth, int EndMonth, int PriceLevel, int Supplier, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryListBySupplier(BeginMonth, EndMonth, PriceLevel, Supplier, IsOpponent);
        }
        /// <summary>
        /// 按客户类别显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel"></param>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        public static DataTable GetSummaryListBySupplier(int BeginMonth, int EndMonth, int PriceLevel, int Supplier, int ClientType, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryListBySupplier(BeginMonth, EndMonth, PriceLevel, Supplier, ClientType, IsOpponent);
        }

        /// <summary>
        /// 按客户显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static DataTable GetSummaryListByClient(int BeginMonth, int EndMonth, int PriceLevel, int Client, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryListByClient(BeginMonth, EndMonth, PriceLevel, Client, IsOpponent);
        }

        /// <summary>
        /// 按客户类别显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="ClientClassify">1:总经销商 2:分销商 3:促销店 4:返利店</param>
        /// <returns></returns>
        public static DataTable GetSummaryListByClientClassify(int BeginMonth, int EndMonth, int PriceLevel, int OrganizeCity, int ClientClassify, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryListByClientClassify(BeginMonth, EndMonth, PriceLevel, OrganizeCity, ClientClassify, IsOpponent);
        }
        #endregion

        #region 按供货商查询下游客户累计进销存统计
        public static DataTable GetProductListBySupplier(int Supplier, int AccountMonth, int PriceLevel, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductListBySupplier(Supplier, AccountMonth, PriceLevel, IsOpponent);
        }
        public static DataTable GetProductListBySupplier(int Supplier, int AccountMonth, int PriceLevel, int ClientType, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductListBySupplier(Supplier, AccountMonth, PriceLevel, ClientType, IsOpponent);
        }
        #endregion

        #region 计算进销存总表中的计算库存数量
        /// <summary>
        /// 计算进销存总表中的计算库存数量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public static int ComputInventory(int Client, int AccountMonth)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ComputInventory(Client, AccountMonth);
        }
        #endregion

        #region 根据供应商获取下游客户按月汇总进销存
        /// <summary>
        /// 根据供应商获取下游客户按月汇总进销存
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        public static decimal GetMonthSummaryBySupplier(int Month, int PriceLevel, int Supplier, string FieldName, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetMonthSummaryBySupplier(Month, PriceLevel, Supplier, FieldName, 0, IsOpponent);
        }

        public static decimal GetMonthSummaryBySupplier(int Month, int PriceLevel, int Supplier, string FieldName, int Product, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetMonthSummaryBySupplier(Month, PriceLevel, Supplier, FieldName, Product, IsOpponent);
        }
        #endregion

        #region 审核通过
        public static int Approve(int Client, int AccountMonth, int Staff, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.Approve(Client, AccountMonth, Staff, IsOpponent);
        }
        public static int BatApprove(string Clients, int AccountMonth, int Staff, int IsOpponent)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.BatApprove(Clients, AccountMonth, Staff, IsOpponent);
        }
        public static int CancelApprove(int Client, int AccountMonth, int Staff)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.CancelApprove(Client, AccountMonth, Staff);
        }
        #endregion

        #region 获取指定管理片区合计预计销量
        public static decimal GetTotalPurchaseVolume(int AccountMonth, int OrganizeCity, bool IncludeChildOrganizeCity)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalPurchaseVolume(AccountMonth, OrganizeCity, IncludeChildOrganizeCity);
        }
        public static decimal GetTotalPurchaseVolume(int AccountMonth, int OrganizeCity)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalPurchaseVolume(AccountMonth, OrganizeCity, false);
        }
        #endregion

        #region 删除进销存
        public static int DeleteJXC(int Client, int AccountMonth)
        {
            SVM_JXCSummaryDAL dal = (SVM_JXCSummaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DeleteJXC(Client, AccountMonth);
        }
        #endregion
    }
}
