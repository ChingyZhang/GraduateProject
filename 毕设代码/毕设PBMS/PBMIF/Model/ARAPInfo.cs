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
    /// 应收(应付)欠款信息单据
    /// </summary>
    [Serializable]
    public class ARAPInfo
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
        ///应收应付类别 1应收 2应付
        ///</summary>
        public int Type = 1;
        public string TypeName = "";

        ///<summary>
        ///金额
        ///</summary>
        public decimal Amount = 0;

        ///<summary>
        ///经办人
        ///</summary>
        public int OpStaff = 0;
        public string OpStaffName = "";

        ///<summary>
        ///关联销售单
        ///</summary>
        public int RelateDeliveryId;
        public string RelateDeliveryCode = "";

        ///<summary>
        ///结款标记 1未结 2已结
        ///</summary>
        public int BalanceFlag = 1;

        ///<summary>
        ///结款日期
        ///</summary>
        public DateTime BalanceDate = new DateTime(1900, 1, 1);

        ///<summary>
        ///结款收付款单ID
        ///</summary>
        public int CashFlowId = 0;

        ///<summary>
        ///备注
        ///</summary>
        public string Remark = "";

        ///<summary>
        ///新增日期
        ///</summary>
        public DateTime InsertTime = new DateTime(1900, 1, 1);
        #endregion

        public ARAPInfo() { }
        public ARAPInfo(AC_ARAPList m)
        {
            if (m != null) FillModel(m);
        }
        public ARAPInfo(int id)
        {
            AC_ARAPList m = new AC_ARAPListBLL(id).Model;
            if (m != null) FillModel(m);
        }

        private void FillModel(AC_ARAPList m)
        {
            if (m == null) return;

            ID = m.ID;
            TradeClient = m.TradeClient;
            Type = m.Type;
            Amount = m.Amount;
            OpStaff = m.OpStaff;
            RelateDeliveryId = m.RelateDeliveryId;
            BalanceFlag = m.BalanceFlag;
            BalanceDate = m.BalanceDate;
            CashFlowId = m.CashFlowId;
            Remark = m.Remark;
            InsertTime = m.InsertTime;

            TypeName = m.Type == 1 ? "应收" : "应付";

            if (m.TradeClient > 0)
            {
                CM_Client c = new CM_ClientBLL(m.TradeClient).Model;
                if (c != null) TradeClientName = c.FullName;
            }

            if (m.OpStaff > 0)
            {
                Org_Staff s = new Org_StaffBLL(m.OpStaff).Model;
                if (s != null) OpStaffName = s.RealName;
            }

            if (m.RelateDeliveryId > 0)
            {
                PBM_Delivery d = new PBM_DeliveryBLL(m.RelateDeliveryId).Model;
                RelateDeliveryCode = d.SheetCode == "" ? d.ID.ToString() : d.SheetCode;
            }
        }
    }
}