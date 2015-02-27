
// ===================================================================
// 文件： SVM_ClassifyForcastDAL.cs
// 项目名称：
// 创建时间：2011/10/13
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
	///SVM_ClassifyForcastBLL业务逻辑BLL类
	/// </summary>
    public class SVM_ClassifyForcastBLL : BaseComplexBLL<SVM_ClassifyForcast, SVM_ClassifyForcast_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_ClassifyForcastDAL";
        private SVM_ClassifyForcastDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_ClassifyForcastBLL
        ///</summary>
        public SVM_ClassifyForcastBLL()
            : base(DALClassName)
        {
            _dal = (SVM_ClassifyForcastDAL)_DAL;
            _m = new SVM_ClassifyForcast();
        }

        public SVM_ClassifyForcastBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_ClassifyForcastDAL)_DAL;
            FillModel(id);
        }

        public SVM_ClassifyForcastBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_ClassifyForcastDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_ClassifyForcast> GetModelList(string condition)
        {
            return new SVM_ClassifyForcastBLL()._GetModelList(condition);
        }
        #endregion

        public static int Init(int OrganizeCity, int AccountMonth, int ClientID, int StaffID)
        {
            SVM_ClassifyForcastDAL dal = (SVM_ClassifyForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.Init(OrganizeCity, AccountMonth, ClientID, StaffID);
        }
        public static decimal GetForcastSumPrice(int ForcastID)
        {
            SVM_ClassifyForcastDAL dal = (SVM_ClassifyForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetForcastSumPrice(ForcastID);
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
            SVM_ClassifyForcastDAL dal = (SVM_ClassifyForcastDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, ClientID, beginMonth, endMonth, ClientType));
        }

        public int Approve(int StaffID)
        {
            return _dal.Approve(_m.ID, StaffID);
        }
        public static int Submit(int ForcastID, int TaskID)
        {
            SVM_ClassifyForcastDAL dal = (SVM_ClassifyForcastDAL)DataAccess.CreateObject(DALClassName);
            return dal.Submit(ForcastID, TaskID);
        }

        public static  void RefreshAvgSales(int ForcastID)
        {
            SVM_ClassifyForcastDAL dal = (SVM_ClassifyForcastDAL)DataAccess.CreateObject(DALClassName);
             dal.RefreshAvgSales(ForcastID);
        }
    }

}