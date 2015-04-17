using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 现金收款信息
    /// </summary>
    [Serializable]
    public class CashFlowInfo
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
        ///收款日期
        ///</summary>
        public DateTime PayDate = new DateTime(1900, 1, 1);

        ///<summary>
        ///经办人
        ///</summary>
        public int AgentStaff = 0;
        public string AgentStaffName = "";

        ///<summary>
        ///支付方式
        ///</summary>
        public int PayMode=0;
        public string PayModeName = "";

        ///<summary>
        ///类别
        ///</summary>
        public int PayClassify=0;
        public string PayClassifyName = "";

        ///<summary>
        ///收款金额
        ///</summary>
        public decimal Amount = 0;

        ///<summary>
        ///交款标志
        ///</summary>
        public int ConfirmState=0;
        public string ConfirmStateName = "";

        ///<summary>
        ///关联订货单
        ///</summary>
        public int RelateOrderId=0;
        public string RelateOrderCode = "";

        ///<summary>
        ///关联发货单
        ///</summary>
        public int RelateDeliveryId=0;
        public string RelateDeliveryCode = "";

        ///<summary>
        ///备注
        ///</summary>
        public string Remark="";

        ///<summary>
        ///关联拜访记录
        ///</summary>
        public int WorkList=0;
        #endregion

        public CashFlowInfo() { }
        public CashFlowInfo(AC_CashFlowList m)
        {
            if (m != null) FillModel(m);
        }
        public CashFlowInfo(int id)
        {
            AC_CashFlowList m = new AC_CashFlowListBLL(id).Model;
            if (m != null) FillModel(m);
        }

        private void FillModel(AC_CashFlowList m)
        {
            if (m == null) return;

            ID = m.ID;
            TradeClient = m.TradeClient;
            PayDate = m.PayDate;
            AgentStaff = m.AgentStaff;
            PayMode = m.PayMode;
            PayClassify = m.PayClassify;
            Amount = m.Amount;
            RelateDeliveryId = m.RelateDeliveryId;
            ConfirmState = m.ConfirmState;
            RelateOrderId = m.RelateOrderId;
            WorkList = m.WorkList;
            Remark = m.Remark;

            if (m.TradeClient > 0)
            {
                CM_Client c = new CM_ClientBLL(m.TradeClient).Model;
                if (c != null) TradeClientName = c.FullName;
            }

            if (m.AgentStaff > 0)
            {
                Org_Staff s = new Org_StaffBLL(m.AgentStaff).Model;
                if (s != null) AgentStaffName = s.RealName;
            }

            if (m.RelateDeliveryId > 0)
            {
                PBM_Delivery d = new PBM_DeliveryBLL(m.RelateDeliveryId).Model;
                RelateDeliveryCode = d.SheetCode == "" ? d.ID.ToString() : d.SheetCode;
            }

            if (m.RelateDeliveryId > 0)
            {
                PBM_Delivery d = new PBM_DeliveryBLL(m.RelateDeliveryId).Model;
                RelateDeliveryCode = d.SheetCode == "" ? d.ID.ToString() : d.SheetCode;
            }

            if (m.PayMode > 0)
            {
                Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_AC_PayMode")[m.PayMode.ToString()];
                if (dic != null) PayModeName = dic.Name;
            }
            if (m.PayClassify > 0)
            {
                Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_AC_PayClassify")[m.PayClassify.ToString()];
                if (dic != null) PayClassifyName = dic.Name;
            }
            if (m.ConfirmState > 0)
            {
                Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_AC_BillConfirmState")[m.ConfirmState.ToString()];
                if (dic != null) ConfirmStateName = dic.Name;
            }
        }
    }
}