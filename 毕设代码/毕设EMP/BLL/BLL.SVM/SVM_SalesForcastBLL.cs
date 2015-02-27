
// ===================================================================
// 文件： SVM_SalesForcastDAL.cs
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
    ///SVM_SalesForcastBLL业务逻辑BLL类
    /// </summary>
    public class SVM_SalesForcastBLL : BaseComplexBLL<SVM_SalesForcast, SVM_SalesForcast_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_SalesForcastDAL";
        private SVM_SalesForcastDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_SalesForcastBLL
        ///</summary>
        public SVM_SalesForcastBLL()
            : base(DALClassName)
        {
            _dal = (SVM_SalesForcastDAL)_DAL;
            _m = new SVM_SalesForcast();
        }

        public SVM_SalesForcastBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_SalesForcastDAL)_DAL;
            FillModel(id);
        }

        public SVM_SalesForcastBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_SalesForcastDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_SalesForcast> GetModelList(string condition)
        {
            return new SVM_SalesForcastBLL()._GetModelList(condition);
        }
        #endregion

        public int Approve(int StaffID)
        {
            return _dal.Approve(_m.ID, StaffID);
        }

        public static int InitProductList(int OrganizeCity, int AccountMonth, int ClientID, int StaffID)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitProductList(OrganizeCity, AccountMonth, ClientID, StaffID);
        }

        public static decimal GetForcastSumPrice(int ForcastID)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetForcastSumPrice(ForcastID);
        }

        #region 获取指定管理片区合计预计销量
        public static decimal GetTotalVolume(int AccountMonth, int OrganizeCity, bool IncludeChildOrganizeCity)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalVolume(AccountMonth, OrganizeCity, IncludeChildOrganizeCity);
        }
        public static decimal GetTotalVolume(int AccountMonth, int OrganizeCity)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalVolume(AccountMonth, OrganizeCity, false);
        }
        public static decimal GetTotalVolumeByClient(int AccountMonth, int Client)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTotalVolumeByClient(AccountMonth, Client);
        }
        #endregion

        /// <summary>
        /// 获取某个客户指定月指定产品的合计进货数量
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static int GetSalesVolume(int Product, int AccountMonth, int Client)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSalesVolume(Product, AccountMonth, Client);
        }
        /// <summary>
        /// 获取指定客户和月份的销量总额
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="ClientID"></param>
        /// <param name="beginMonth"></param>
        /// <param name="endMonth"></param>
        /// <param name="ClientType"></param>
        /// <returns></returns>
        public static DataTable GetSummary(int OrganizeCity, int ClientID, int beginMonth, int endMonth, int ClientType)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, ClientID, beginMonth, endMonth, ClientType));
        }

        public static DataTable RPT_001(string OrganizeCitys, int BeginMonth, int EndMonth)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.RPT_001(OrganizeCitys, BeginMonth, EndMonth));
        }
        public static int Submit(int ForcastID, string TaskID)
        {
            SVM_SalesForcastDAL dal = (SVM_SalesForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.Submit(ForcastID, TaskID);
        }
    }
}
