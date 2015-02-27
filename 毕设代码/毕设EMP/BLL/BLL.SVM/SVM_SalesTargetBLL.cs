
// ===================================================================
// 文件： SVM_SalesTargetDAL.cs
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
    ///SVM_SalesTargetBLL业务逻辑BLL类
    /// </summary>
    public class SVM_SalesTargetBLL : BaseComplexBLL<SVM_SalesTarget, SVM_SalesTarget_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_SalesTargetDAL";
        private SVM_SalesTargetDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_SalesTargetBLL
        ///</summary>
        public SVM_SalesTargetBLL()
            : base(DALClassName)
        {
            _dal = (SVM_SalesTargetDAL)_DAL;
            _m = new SVM_SalesTarget();
        }

        public SVM_SalesTargetBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_SalesTargetDAL)_DAL;
            FillModel(id);
        }

        public SVM_SalesTargetBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_SalesTargetDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_SalesTarget> GetModelList(string condition)
        {
            return new SVM_SalesTargetBLL()._GetModelList(condition);
        }
        #endregion

        public DataTable GetInputDetail()
        {
            return Tools.ConvertDataReaderToDataTable(_dal.GetInputDetail(_m.ID));
        }

        public int Approve(int StaffID)
        {
            return _dal.Approve(_m.ID, StaffID);
        }

        public static int InitProductList(int OrganizeCity, int AccountMonth, int ClientID, int StaffID)
        {
            SVM_SalesTargetDAL dal = (SVM_SalesTargetDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitProductList(OrganizeCity, AccountMonth, ClientID, StaffID);
        }

        public static DataTable GetListByOrganizeCity(int OrganizeCity, int BeginMonth, int EndMonth, int Client)
        {
            SVM_SalesTargetDAL dal = (SVM_SalesTargetDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetListByOrganizeCity(OrganizeCity, BeginMonth, EndMonth, Client));
        }

        public static decimal GetTargetSumPrice(int TargetID)
        {
            SVM_SalesTargetDAL dal = (SVM_SalesTargetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetTargetSumPrice(TargetID);
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
            SVM_SalesTargetDAL dal = (SVM_SalesTargetDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(OrganizeCity, ClientID, beginMonth, endMonth, ClientType));
        }
    }
}