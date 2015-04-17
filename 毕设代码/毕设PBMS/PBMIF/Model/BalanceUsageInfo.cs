using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;
using System;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 预收款变动记录
    /// </summary>
    [Serializable]
    public class BalanceUsageInfo
    {
        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID = 0;

        ///<summary>
        ///往来客户
        ///</summary>
        public int TradeClient = 0;
        public string TradeClientName = "";

        ///<summary>
        ///变动金额
        ///</summary>
        public decimal Amount = 0;

        ///<summary>
        ///变动后余额
        ///</summary>
        public decimal Balance = 0;

        ///<summary>
        ///收付款单ID
        ///</summary>
        public int CashFlowId = 0;

        ///<summary>
        ///关联发货单
        ///</summary>
        public int DeliveryId = 0;

        ///<summary>
        ///备注
        ///</summary>
        public string Remark = "";

        ///<summary>
        ///新增日期
        ///</summary>
        public DateTime InsertTime = new DateTime(1900, 1, 1);

        ///<summary>
        ///经办人
        ///</summary>
        public int InsertStaff = 0;
        public string InsertStaffName = "";
        #endregion

        public BalanceUsageInfo() { }
        public BalanceUsageInfo(AC_BalanceUsageList m)
        {
            if (m != null) FillModel(m);
        }
        public BalanceUsageInfo(int id)
        {
            AC_BalanceUsageList m = new AC_BalanceUsageListBLL(id).Model;
            if (m != null) FillModel(m);
        }

        private void FillModel(AC_BalanceUsageList m)
        {
            if (m == null) return;

            ID = m.ID;
            TradeClient = m.TradeClient;
            Amount = m.Amount;
            Balance = m.Balance;
            DeliveryId = m.DeliveryId;
            CashFlowId = m.CashFlowId;
            Remark = m.Remark;
            InsertTime = m.InsertTime;
            InsertStaff = m.InsertStaff;

            if (m.TradeClient > 0)
            {
                CM_Client c = new CM_ClientBLL(m.TradeClient).Model;
                if (c != null) TradeClientName = c.FullName;
            }

            if (m.InsertStaff > 0)
            {
                Org_Staff s = new Org_StaffBLL(m.InsertStaff).Model;
                if (s != null) InsertStaffName = s.RealName;
            }
        }
    }
}