
// ===================================================================
// 文件： FNA_BudgetDAL.cs
// 项目名称：
// 创建时间：2009/2/21
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
    ///FNA_BudgetBLL业务逻辑BLL类
    /// </summary>
    public class FNA_BudgetBLL : BaseSimpleBLL<FNA_Budget>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetDAL";
        private FNA_BudgetDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_BudgetBLL
        ///</summary>
        public FNA_BudgetBLL()
            : base(DALClassName)
        {
            _dal = (FNA_BudgetDAL)_DAL;
            _m = new FNA_Budget();
        }

        public FNA_BudgetBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_Budget> GetModelList(string condition)
        {
            return new FNA_BudgetBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 将预算分配设为审核通过
        /// </summary>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Approve(int Staff)
        {
            return _dal.Approve(_m.ID, Staff);
        }

        #region 获取指定片区及所有下属片区的预算分配总额及余额
        /// <summary>
        /// 获取指定片区及所有下属片区的预算分配总额及余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public static DataTable GetSumBudgetAndBalance(int AccountMonth, int OrganizeCity)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumBudgetAndBalance(AccountMonth, OrganizeCity);
        }
        #endregion

        #region 预算调配
        /// <summary>
        /// 预算调配，将指定月的预算额度从某个管理单元移至另一预算月的另一管理单元
        /// </summary>
        /// <param name="FromAccountMonth">调拔预算的源会计月</param>
        /// <param name="FromOrganizeCity">调拔预算的源额度单元</param>
        /// <param name="ToAccountMonth">调拔预算的目的月</param>
        /// <param name="ToOrganizeCity">调拔预算的目的额度单元</param>
        /// <param name="TransfeeAmount">调拔金额</param>
        /// <param name="BudgetType">调拔预算的类型</param>
        /// <param name="Staff">调拔操作人</param>
        /// <param name="Remark">调拔备注说明</param>
        /// <returns></returns>
        public static int Transfer(int FromAccountMonth, int FromOrganizeCity, int FromFeeType, int ToAccountMonth, int ToOrganizeCity, int ToFeeType, decimal TransfeeAmount, int BudgetType, int Staff, string Remark)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.Transfer(FromAccountMonth, FromOrganizeCity, FromFeeType, ToAccountMonth, ToOrganizeCity, ToFeeType, TransfeeAmount, BudgetType, Staff, Remark);
        }
        #endregion

        #region 获取指定管理片区指定月的可用预算额度
        /// <summary>
        /// 获取指定管理片区指定月的可用预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <param name="IncludeChildOrganizeCity">是否包括下级子管理片区</param>
        /// <returns>可用余额</returns>
        public static decimal GetUsableAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetUsableAmount(AccountMonth, OrganizeCity, FeeType, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// 获取指定管理片区(不含下级片区)指定月的可用预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <returns>可用余额</returns>
        public static decimal GetUsableAmount(int AccountMonth, int OrganizeCity, int FeeType)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetUsableAmount(AccountMonth, OrganizeCity, FeeType, false);
        }
        #endregion

        #region 获取指定管理片区指定月的预算分配额度
        /// <summary>
        /// 获取指定管理片区指定月的预算分配额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <param name="IncludeChildOrganizeCity">是否包括下级子管理片区</param>
        /// <returns>预算分配额度</returns>
        public static decimal GetSumBudgetAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumBudgetAmount(AccountMonth, OrganizeCity, FeeType, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// 获取指定管理片区(不含下级片区)指定月的预算分配额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <returns>预算分配额度</returns>
        public static decimal GetSumBudgetAmount(int AccountMonth, int OrganizeCity, int FeeType)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumBudgetAmount(AccountMonth, OrganizeCity, FeeType, false);
        }
        #endregion

        #region 获取会计月的医务费用总额
        /// <summary>
        /// 获取会计月的医务费用总额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public static decimal GetSumYWCost(int AccountMonth)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumYWCost(AccountMonth);
        }
        #endregion

        /// <summary>
        /// 获取各管理片区预算分配记录
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static DataTable GetAssignInfo(int OrganizeCity, int AccountMonth, int Level)
        {
            FNA_BudgetDAL dal = (FNA_BudgetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAssignInfo(OrganizeCity, AccountMonth, Level);
        }
    }
}
