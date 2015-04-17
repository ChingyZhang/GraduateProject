
// ===================================================================
// 文件： AC_CashFlowListDAL.cs
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
    ///AC_CashFlowListBLL业务逻辑BLL类
    /// </summary>
    public class AC_CashFlowListBLL : BaseSimpleBLL<AC_CashFlowList>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.PBM.AC_CashFlowListDAL";
        private AC_CashFlowListDAL _dal;

        #region 构造函数
        ///<summary>
        ///AC_CashFlowListBLL
        ///</summary>
        public AC_CashFlowListBLL()
            : base(DALClassName)
        {
            _dal = (AC_CashFlowListDAL)_DAL;
            _m = new AC_CashFlowList();
        }

        public AC_CashFlowListBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_CashFlowListDAL)_DAL;
            FillModel(id);
        }

        public AC_CashFlowListBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_CashFlowListDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<AC_CashFlowList> GetModelList(string condition)
        {
            return new AC_CashFlowListBLL()._GetModelList(condition);
        }
        #endregion

        #region 现金\POS 收款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="TradeClient">往来客户</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式	1现金、2POS</param>
        /// <param name="PayClassify">类别	1预收款、2现结收货款、3结应收款、4其他收入、5存款入账 6银行提现 
        /// 101预付款 102现结付货款 103结应付款 104其他支出 105存款付出 106银行存现</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="RelateOrderId">关联订货单</param>
        /// <param name="RelateDeliveryId">关联发货单</param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <param name="WorkList">关联拜访记录</param>
        /// <param name="ARAPIDs">待结应收应付记录ID</param>
        /// <returns>-10:应收款ID为空 -11:应收款金额与收款金额不匹配 -12:统计应收款总额时出错</returns>
        public static int Receipt(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, int PayClassify, decimal Amount, int RelateOrderId,
            int RelateDeliveryId, string Remark, int InsertStaff, int WorkList, string ARAPIDs)
        {
            AC_CashFlowListDAL dal = (AC_CashFlowListDAL)DataAccess.CreateObject(DALClassName);
            return dal.Receipt(OwnerClient, TradeClient, AgentStaff, PayMode, PayClassify, Amount, RelateOrderId,
             RelateDeliveryId, Remark, InsertStaff, WorkList, ARAPIDs);
        }
        #endregion

        #region 结应收
        /// <summary>
        /// 结应收款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="TradeClient">往来客户</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <param name="WorkList">关联拜访记录</param>
        /// <param name="ARAPIDs">待结应收应付记录ID</param>
        /// <returns>-10:应收款ID为空 -11:应收款金额与收款金额不匹配 -12:统计应收款总额时出错</returns>
        public static int Receipt_BalanceAR(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, decimal Amount,
            string Remark, int InsertStaff, int WorkList, string ARIDs)
        {
            return Receipt(OwnerClient, TradeClient, AgentStaff, PayMode, 3, Amount, 0, 0, Remark, InsertStaff, WorkList, ARIDs);
        }
        #endregion

        #region 收预收款
        /// <summary>
        /// 收预收款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="TradeClient">往来客户</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="RelateOrderId"></param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <param name="WorkList">关联拜访记录</param>
        /// <returns></returns>
        public static int Receipt_PreReceived(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, decimal Amount, int RelateOrderId,
            string Remark, int InsertStaff, int WorkList)
        {
            return Receipt(OwnerClient, TradeClient, AgentStaff, PayMode, 1, Amount, RelateOrderId, 0, Remark, InsertStaff, WorkList, "");
        }
        #endregion

        #region 其他收入收款
        /// <summary>
        /// 其他收入收款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <returns></returns>
        public static int Receipt_OtherIncome(int OwnerClient, int AgentStaff, int PayMode, decimal Amount, string Remark, int InsertStaff)
        {
            return Receipt(OwnerClient, 0, AgentStaff, PayMode, 4, Amount, 0, 0, Remark, InsertStaff, 0, "");
        }
        #endregion

        #region 结应付款
        /// <summary>
        /// 结应付款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="TradeClient">往来客户</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <param name="WorkList">关联拜访记录</param>
        /// <param name="ARAPIDs">待结应收应付记录ID</param>
        /// <returns>-10:应付款ID为空 -11:应付款金额与付款金额不匹配 -12:统计应收款总额时出错</returns>
        public static int Receipt_BalanceAP(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, decimal Amount,
            string Remark, int InsertStaff, int WorkList, string APIDs)
        {
            return Receipt(OwnerClient, TradeClient, AgentStaff, PayMode, 103, Amount, 0, 0, Remark, InsertStaff, WorkList, APIDs);
        }
        #endregion

        #region 支预付款
        /// <summary>
        /// 支预付款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="TradeClient">往来客户</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="RelateOrderId"></param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <param name="WorkList">关联拜访记录</param>
        /// <returns></returns>
        public static int Receipt_PrePayment(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, decimal Amount, int RelateOrderId,
            string Remark, int InsertStaff)
        {
            return Receipt(OwnerClient, TradeClient, AgentStaff, PayMode, 101, Amount, RelateOrderId, 0, Remark, InsertStaff, 0, "");
        }
        #endregion

        #region 其他支出付款
        /// <summary>
        /// 其他支出付款
        /// </summary>
        /// <param name="OwnerClient">经销商</param>
        /// <param name="AgentStaff">经办人</param>
        /// <param name="PayMode">支付方式：1现金、2POS</param>
        /// <param name="Amount">收款金额</param>
        /// <param name="Remark">备注</param>
        /// <param name="InsertStaff">新增人</param>
        /// <returns></returns>
        public static int Receipt_OtherExpense(int OwnerClient, int AgentStaff, int PayMode, decimal Amount, string Remark, int InsertStaff)
        {
            return Receipt(OwnerClient, 0, AgentStaff, PayMode, 104, Amount, 0, 0, Remark, InsertStaff, 0, "");
        }
        #endregion
    }
}