
// ===================================================================
// 文件： AC_BalanceUsageListDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
    /// <summary>
    ///AC_BalanceUsageListBLL业务逻辑BLL类
    /// </summary>
    public class AC_BalanceUsageListBLL : BaseSimpleBLL<AC_BalanceUsageList>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.PBM.AC_BalanceUsageListDAL";
        private AC_BalanceUsageListDAL _dal;

        #region 构造函数
        ///<summary>
        ///AC_BalanceUsageListBLL
        ///</summary>
        public AC_BalanceUsageListBLL()
            : base(DALClassName)
        {
            _dal = (AC_BalanceUsageListDAL)_DAL;
            _m = new AC_BalanceUsageList();
        }

        public AC_BalanceUsageListBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_BalanceUsageListDAL)_DAL;
            FillModel(id);
        }

        public AC_BalanceUsageListBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_BalanceUsageListDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<AC_BalanceUsageList> GetModelList(string condition)
        {
            return new AC_BalanceUsageListBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 使用余额结应收款
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="TradeClient"></param>
        /// <param name="AgentStaff"></param>
        /// <param name="Amount"></param>
        /// <param name="Remark"></param>
        /// <param name="ARIDs"></param>
        /// <returns> -10:预收款金额不够结算 -11:应收款金额与收款金额不匹配 -12:统计应收款总额时出错</returns>
        public static int BalanceAR(int OwnerClient, int TradeClient, int AgentStaff, decimal Amount, string Remark, string ARIDs)
        {
            AC_BalanceUsageListDAL dal = (AC_BalanceUsageListDAL)DataAccess.CreateObject(DALClassName);
            return dal.BalanceAR(OwnerClient, TradeClient, AgentStaff, Amount, Remark, ARIDs);
        }

        /// <summary>
        /// 使用余额结应付款
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="TradeClient"></param>
        /// <param name="AgentStaff"></param>
        /// <param name="Amount"></param>
        /// <param name="Remark"></param>
        /// <param name="APIDs"></param>
        /// <returns> -10:预付款金额不够结算 -11:应付款金额与付款金额不匹配 -12:统计应付款总额时出错</returns>
        public static int BalanceAP(int OwnerClient, int TradeClient, int AgentStaff, decimal Amount, string Remark, string APIDs)
        {
            AC_BalanceUsageListDAL dal = (AC_BalanceUsageListDAL)DataAccess.CreateObject(DALClassName);
            return dal.BalanceAP(OwnerClient, TradeClient, AgentStaff, Amount, Remark, APIDs);
        }

    }
}