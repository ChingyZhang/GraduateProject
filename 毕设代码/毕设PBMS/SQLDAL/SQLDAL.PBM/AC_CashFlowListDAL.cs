
// ===================================================================
// 文件： AC_CashFlowListDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.PBM;


namespace MCSFramework.SQLDAL.PBM
{
    /// <summary>
    ///AC_CashFlowList数据访问DAL类
    /// </summary>
    public class AC_CashFlowListDAL : BaseSimpleDAL<AC_CashFlowList>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public AC_CashFlowListDAL()
        {
            _ProcePrefix = "MCS_PBM.dbo.sp_AC_CashFlowList";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_CashFlowList m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@AgentStaff", SqlDbType.Int, 4, m.AgentStaff),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@PayClassify", SqlDbType.Int, 4, m.PayClassify),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@CashAccount", SqlDbType.Int, 4, m.CashAccount),
				SQLDatabase.MakeInParam("@ConfirmState", SqlDbType.Int, 4, m.ConfirmState),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@RelateOrderId", SqlDbType.Int, 4, m.RelateOrderId),
				SQLDatabase.MakeInParam("@RelateDeliveryId", SqlDbType.Int, 4, m.RelateDeliveryId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(AC_CashFlowList m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, m.OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, m.TradeClient),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@AgentStaff", SqlDbType.Int, 4, m.AgentStaff),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@PayClassify", SqlDbType.Int, 4, m.PayClassify),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@CashAccount", SqlDbType.Int, 4, m.CashAccount),
				SQLDatabase.MakeInParam("@ConfirmState", SqlDbType.Int, 4, m.ConfirmState),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@RelateOrderId", SqlDbType.Int, 4, m.RelateOrderId),
				SQLDatabase.MakeInParam("@RelateDeliveryId", SqlDbType.Int, 4, m.RelateDeliveryId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override AC_CashFlowList FillModel(IDataReader dr)
        {
            AC_CashFlowList m = new AC_CashFlowList();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OwnerClient"].ToString())) m.OwnerClient = (int)dr["OwnerClient"];
            if (!string.IsNullOrEmpty(dr["TradeClient"].ToString())) m.TradeClient = (int)dr["TradeClient"];
            if (!string.IsNullOrEmpty(dr["PayDate"].ToString())) m.PayDate = (DateTime)dr["PayDate"];
            if (!string.IsNullOrEmpty(dr["AgentStaff"].ToString())) m.AgentStaff = (int)dr["AgentStaff"];
            if (!string.IsNullOrEmpty(dr["PayMode"].ToString())) m.PayMode = (int)dr["PayMode"];
            if (!string.IsNullOrEmpty(dr["PayClassify"].ToString())) m.PayClassify = (int)dr["PayClassify"];
            if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString())) m.AccountTitle = (int)dr["AccountTitle"];
            if (!string.IsNullOrEmpty(dr["Amount"].ToString())) m.Amount = (decimal)dr["Amount"];
            if (!string.IsNullOrEmpty(dr["CashAccount"].ToString())) m.CashAccount = (int)dr["CashAccount"];
            if (!string.IsNullOrEmpty(dr["ConfirmState"].ToString())) m.ConfirmState = (int)dr["ConfirmState"];
            if (!string.IsNullOrEmpty(dr["ConfirmStaff"].ToString())) m.ConfirmStaff = (int)dr["ConfirmStaff"];
            if (!string.IsNullOrEmpty(dr["ConfirmDate"].ToString())) m.ConfirmDate = (DateTime)dr["ConfirmDate"];
            if (!string.IsNullOrEmpty(dr["RelateOrderId"].ToString())) m.RelateOrderId = (int)dr["RelateOrderId"];
            if (!string.IsNullOrEmpty(dr["RelateDeliveryId"].ToString())) m.RelateDeliveryId = (int)dr["RelateDeliveryId"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["WorkList"].ToString())) m.WorkList = (int)dr["WorkList"];

            return m;
        }

        /// <summary>
        /// 现金\POS 收款
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
        /// <returns>-10:应收款ID为空 -11:应收款金额与收款金额不匹配 -12:统计应收款总额时出错
        /// </returns>
        public int Receipt(int OwnerClient, int TradeClient, int AgentStaff, int PayMode, int PayClassify, decimal Amount, int RelateOrderId,
            int RelateDeliveryId, string Remark, int InsertStaff, int WorkList, string ARAPIDs)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, OwnerClient),
				SQLDatabase.MakeInParam("@TradeClient", SqlDbType.Int, 4, TradeClient),
				SQLDatabase.MakeInParam("@AgentStaff", SqlDbType.Int, 4, AgentStaff),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, PayMode),
				SQLDatabase.MakeInParam("@PayClassify", SqlDbType.Int, 4, PayClassify),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, Amount),
				SQLDatabase.MakeInParam("@RelateOrderId", SqlDbType.Int, 4, RelateOrderId),
				SQLDatabase.MakeInParam("@RelateDeliveryId", SqlDbType.Int, 4, RelateDeliveryId),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, InsertStaff),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, WorkList),
                SQLDatabase.MakeInParam("@ARAPIDs", SqlDbType.VarChar, ARAPIDs.Length, ARAPIDs)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Receipt", prams);
        }
    }
}

