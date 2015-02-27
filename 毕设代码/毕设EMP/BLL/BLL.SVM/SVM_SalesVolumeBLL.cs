
// ===================================================================
// 文件： SVM_SalesVolumeDAL.cs
// 项目名称：
// 创建时间：2009-2-19
// 作者:	   
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
    ///SVM_SalesVolumeBLL业务逻辑BLL类
    /// </summary>
    public class SVM_SalesVolumeBLL : BaseComplexBLL<SVM_SalesVolume, SVM_SalesVolume_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_SalesVolumeDAL";
        private SVM_SalesVolumeDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_SalesVolumeBLL
        ///</summary>
        public SVM_SalesVolumeBLL()
            : base(DALClassName)
        {
            _dal = (SVM_SalesVolumeDAL)_DAL;
            _m = new SVM_SalesVolume();
        }

        public SVM_SalesVolumeBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_SalesVolumeDAL)_DAL;
            FillModel(id);
        }

        public SVM_SalesVolumeBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_SalesVolumeDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_SalesVolume> GetModelList(string condition)
        {
            return new SVM_SalesVolumeBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取按销售价计算的总销售额，含DDF
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalValue()
        {
            return _dal.GetTotalValue();
        }

        public int GetTotalBox()
        {
            return _dal.GetTotalBox();
        }

        /// <summary>
        /// 获取按出厂价计算的总销售额，不含DDF
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalFactoryPriceValue()
        {
            return _dal.GetTotalFactoryPriceValue();
        }

        public void Approve(int StaffID)
        {
            _dal.Approve(StaffID);
        }
        public static int BatApprove(string IDs, int StaffID)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.BatApprove(IDs, StaffID);
        }
        /// <summary>
        /// 获取指定管理片区合计实际销量
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public static decimal GetTotalVolume(int OrganizeCity, int AccountMonth, bool IncludeChildOrganizeCity)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalVolume(OrganizeCity, AccountMonth, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// 获取指定管理片区历史平均实际销量(门店实销)
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="MonthCount"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public static decimal GetAvgVolume(int OrganizeCity, int EndMonth, int MonthCount, bool IncludeChildOrganizeCity)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAvgVolume(OrganizeCity, EndMonth, MonthCount, IncludeChildOrganizeCity);
        }
        /// <summary>
        /// 获取指定管理片区历史平均实际销量(门店实销)
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public static decimal GetAvgVolume(int OrganizeCity, int EndMonth, bool IncludeChildOrganizeCity)
        {
            return GetAvgVolume(OrganizeCity, EndMonth, 0, IncludeChildOrganizeCity);
        }
        /// <summary>
        /// 获取指定管理片区历史平均实际销量(门店实销)
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public static decimal GetAvgVolume(int OrganizeCity, bool IncludeChildOrganizeCity)
        {
            return GetAvgVolume(OrganizeCity, 0, 0, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// Get summary info by retailer,begindate,enddate
        /// </summary>
        /// <param name="Retailer"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetSummary(int OrganizeCity, int Supplier, int ClientID, DateTime begintime, DateTime endtime, int Type, int Flag)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, Supplier, ClientID, begintime, endtime, Type, Flag));
        }
        public static DataTable GetSummary_GroupByDate(int OrganizeCity, int Supplier, int ClientID, DateTime begintime, DateTime endtime, int Type, int Flag)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary_GroupByDate(OrganizeCity, Supplier, ClientID, begintime, endtime, Type, Flag, 0));
        }
        public static DataTable GetSummary_GroupByDate(int OrganizeCity, int Supplier, int ClientID, DateTime begintime, DateTime endtime, int Type, int Flag, int DataSource)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary_GroupByDate(OrganizeCity, Supplier, ClientID, begintime, endtime, Type, Flag, DataSource));
        }
        public static DataTable InitProductList(int VolumeID, int ClientID, int Type, int Month, bool IsCXP)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.InitProductList(VolumeID, ClientID, Type, Month, IsCXP));
        }

        public static int CheckSalesVolume(int ClientID, DateTime volumedate, int Type, int Flag)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckSalesVolume(ClientID, volumedate, Type, Flag);
        }

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SheetCode"></param>
        /// <param name="SalesDate"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static int SVMinsert(string SheetCode, DateTime SalesDate, int Client)
        {
            SVM_SalesVolumeDAL dal = new SVM_SalesVolumeDAL();
            return dal.SVMinsert(SheetCode, SalesDate, Client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SalesVolume"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="Client"></param>
        /// <param name="Quantity"></param>
        /// <param name="FactoryPrice"></param>
        public static void SVMinsertdetail(int SalesVolume, string Code, string LotNumber, int Client, int Quantity, decimal FactoryPrice)
        {
            SVM_SalesVolumeDAL dal = new SVM_SalesVolumeDAL();
            dal.SVMinsertdetail(SalesVolume, Code, LotNumber, Client, Quantity, FactoryPrice);
        }

        public static decimal GetTotalSalesPriceByOrganizeCity(int organizecity, int accountmonth, int ClientType, int Type)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalSalesPriceByOrganizeCity(organizecity, accountmonth, ClientType, Type);
        }
        #endregion

        /// <summary>
        /// 获取客户销量(进货、销量、库存)录入记录数量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static DataTable GetCountByClient(int AccountMonth, int Client)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCountByClient(AccountMonth, Client);
        }

        public decimal GetByClassify(int Classify, int AccountMonth, int Client)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByClassify(Classify, AccountMonth, Client);
        }

        public static DataTable GetSummaryTotal(int organizecity, int accountmonth, int clienttype, int flag, int level, int state, int type, int staff)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummaryTotal(organizecity, accountmonth, clienttype, flag, level, state, type, staff));
        }

        public static DataTable GetSummaryTotal2(int organizecity, int accountmonth, int clienttype, int flag, int state, int type, int staff)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummaryTotal2(organizecity, accountmonth, clienttype, flag, state, type, staff));
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
        public static int ApproveByStaff(int Organizecity, int Staff, int accountmonth, int clienttype, int flag, int type)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.ApproveByStaff(Organizecity, Staff, accountmonth, clienttype, flag, type);
        }

        public static int SubmitByStaff(int Organizecity, int Staff, int accountmonth, int clienttype, int flag, int type)
        {
            SVM_SalesVolumeDAL dal = (SVM_SalesVolumeDAL)DataAccess.CreateObject(DALClassName);
            return dal.SubmitByStaff(Organizecity, Staff, accountmonth, clienttype, flag, type);
        }
    }
}