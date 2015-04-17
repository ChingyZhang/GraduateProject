using System;
using System.Collections.Generic;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.CM;
using MCSFramework.Model;
using MCSFramework.Common;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class Order
    {
        #region 订单字段
        /// <summary>
        /// 订货单ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 订货单号
        /// </summary>
        public string SheetCode = "";

        /// <summary>
        /// 发货商
        /// </summary>
        public int Supplier = 0;
        public string SupplierName = "";

        /// <summary>
        /// 收货商
        /// </summary>
        public int Client = 0;
        public string ClientName = "";

        /// <summary>
        /// 业务员
        /// </summary>
        public int SalesMan = 0;
        public string SalesManName = "";

        /// <summary>
        /// 订单来源    1:PC后台订单	2:APP拜访订单 3:APP电话订单
        /// </summary>
        public int OrderSource = 1;
        public string OrderSourceName = "";

        /// <summary>
        /// 订货单类别 1销售订单 2退货订单 3换货订单 4赠送订单
        /// </summary>
        public int Classify = 0;
        public string ClassifyName = "";

        /// <summary>
        /// 订货单状态 1拟定 2提交 3派单 4发货中 5已完成 9取消
        /// </summary>
        public int State = 0;
        public string StateName = "";

        /// <summary>
        /// 供货价表
        /// </summary>
        public int StandardPrice = 0;
        public string StandardPriceName = "";

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountAmount = 0;

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal WipeAmount = 0;

        /// <summary>
        /// 实际销售额
        /// </summary>
        public decimal ActAmount = 0;

        /// <summary>
        /// 要求到货日期
        /// </summary>
        public DateTime ArriveTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 提交时间日期
        /// </summary>
        public DateTime SubmitTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 订货单创建日期
        /// </summary>
        public DateTime InsertTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 关联拜访记录
        /// </summary>
        public int WorkList = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";

        /// <summary>
        /// 审核标志 1：已审核 2：未审核
        /// </summary>
        public int ApproveFlag = 2;

        /// <summary>
        /// 订货单明细
        /// </summary>
        public List<OrderDetail> Items = new List<OrderDetail>();

        /// <summary>
        /// 订货单收款信息
        /// </summary>
        public List<OrderPayInfo> PayInfos = new List<OrderPayInfo>();
        #endregion

        public Order() { }

        public Order(int OrderID)
        {
            FillModel(OrderID);
        }

        public Order(PBM_Order m)
        {
            if (m != null) FillModel(m.ID);
        }

        private void FillModel(int OrderID)
        {
            PBM_OrderBLL bll = new PBM_OrderBLL(OrderID);
            if (bll.Model == null) { ID = -1; return; }

            PBM_Order m = bll.Model;
            ID = m.ID;
            SheetCode = m.SheetCode;
            Supplier = m.Supplier;
            Client = m.Client;
            SalesMan = m.SalesMan;
            Classify = m.Classify;
            StandardPrice = m.StandardPrice;
            State = m.State;
            DiscountAmount = m.DiscountAmount;
            WipeAmount = m.WipeAmount;
            ActAmount = m.ActAmount;
            ArriveTime = m.ArriveTime;
            SubmitTime = m.SubmitTime;
            ConfirmTime = m.ConfirmTime;
            InsertTime = m.InsertTime;
            Remark = m.Remark;
            WorkList = m.WorkList;
            ApproveFlag = m.ApproveFlag;

            int.TryParse(m["OrderSource"], out OrderSource);

            //订货单明细
            Items = new List<OrderDetail>();
            foreach (PBM_OrderDetail item in bll.Items)
            {
                Items.Add(new OrderDetail(item));
            }

            //预收款信息列表
            PayInfos = new List<OrderPayInfo>();
            foreach (PBM_OrderPayInfo item in bll.GetPayInfoList())
            {
                PayInfos.Add(new OrderPayInfo(item));
            }

            #region 获取各字段ID对应的名称
            if (Supplier > 0)
            {
                CM_Client c = new CM_ClientBLL(Supplier).Model;
                if (c != null) SupplierName = c.FullName;
            }
            if (Client > 0)
            {
                CM_Client c = new CM_ClientBLL(Client).Model;
                if (c != null) ClientName = c.FullName;
            }
            if (SalesMan > 0)
            {
                Org_Staff s = new Org_StaffBLL(SalesMan).Model;
                if (s != null) SalesManName = s.RealName;
            }
            if (StandardPrice > 0)
            {
                PDT_StandardPrice s = new PDT_StandardPriceBLL(StandardPrice).Model;
                if (s != null) StandardPriceName = s.Name;
            }
            #endregion

            #region 获取字典表名称
            try
            {
                if (m.Classify > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_OrderClassify")[m.Classify.ToString()];
                    if (dic != null) ClassifyName = dic.Name;
                }

                if (m.State > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_OrderState")[m.State.ToString()];
                    if (dic != null) StateName = dic.Name;
                }

                if (OrderSource > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_OrderSource")[OrderSource.ToString()];
                    if (dic != null) OrderSourceName = dic.Name;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MCSFramework.WSI.Order", err);
            }
            #endregion
        }

        #region 订货单商品明细
        [Serializable]
        public class OrderDetail
        {
            /// <summary>
            /// 明细ID
            /// </summary>
            public int DetailID = 0;

            /// <summary>
            /// 商品
            /// </summary>
            public int Product = 0;
            public string ProductName = "";

            /// <summary>
            /// 折扣率，1为全价，0为免费，0.7为7折销售
            /// </summary>
            public decimal DiscountRate = 1;

            /// <summary>
            /// 销售价
            /// </summary>
            public decimal Price = 0;

            /// <summary>
            /// 订货数量
            /// </summary>
            public int BookQuantity = 0;

            /// <summary>
            /// 确认数量
            /// </summary>
            public int ConfirmQuantity = 0;

            /// <summary>
            /// 已发货数量
            /// </summary>
            public int DeliveredQuantity = 0;

            /// <summary>
            /// 销售方式
            /// </summary>
            public int SalesMode = 0;
            public string SalesModeName = "";


            /// <summary>
            /// 备注
            /// </summary>
            public string Remark = "";

            /// <summary>
            /// 运输包装（箱）
            /// </summary>
            public string PackingName_T = "";

            /// <summary>
            /// 零售包装（单件）
            /// </summary>
            public string PackingName_P = "";

            /// <summary>
            /// 批零系数
            /// </summary>
            public int ConvertFactor = 0;

            /// <summary>
            ///商品种类
            /// <summary>
            public string CategoryName = "";



            public OrderDetail() { }

            public OrderDetail(PBM_OrderDetail m)
            {
                DetailID = m.ID;
                Product = m.Product;
                Price = m.Price;
                DiscountRate = m.DiscountRate;
                BookQuantity = m.BookQuantity;
                ConfirmQuantity = m.ConfirmQuantity;
                DeliveredQuantity = m.DeliveredQuantity;
                Remark = m.Remark;

                PDT_Product p = new PDT_ProductBLL(Product).Model;
                if (p == null) return;

                ProductName = p.ShortName;

                if (p.ConvertFactor == 0) p.ConvertFactor = 1;
                ConvertFactor = p.ConvertFactor;

                if (p.Category > 0)
                {
                    CategoryName = PDT_CategoryBLL.GetFullCategoryName(p.Category);
                }

                #region 获取字典表名称
                try
                {
                    if (m.SalesMode > 0)
                    {
                        Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_SalseMode")[m.SalesMode.ToString()];
                        if (dic != null) SalesModeName = dic.Name;
                    }
                    if (p.TrafficPackaging > 0)
                    {
                        PackingName_T = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
                    }
                    if (p.Packaging > 0)
                    {
                        PackingName_P = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
                    }
                }
                catch (System.Exception err)
                {
                    LogWriter.WriteLog("MCSFramework.WSI.OrderDetail", err);
                }
                #endregion

            }
        }
        #endregion


        #region 订货单预收款信息
        /// <summary>
        /// 订货单预收款信息
        /// </summary>
        [Serializable]
        public class OrderPayInfo
        {
            #region 公共属性
            ///<summary>
            ///ID
            ///</summary>
            public int ID = 0;

            ///<summary>
            ///收款方式，1现金付款、2POS付款
            ///</summary>
            public int PayMode = 1;
            public string PayModeName = "";

            ///<summary>
            ///收款金额
            ///</summary>
            public decimal Amount = 0;

            ///<summary>
            ///备注
            ///</summary>
            public string Remark = "";

            ///<summary>
            ///审核标志
            ///</summary>
            public int ApproveFlag = 2;

            #endregion

            public OrderPayInfo() { }
            public OrderPayInfo(PBM_OrderPayInfo m)
            {
                if (m != null) FillModel(m);
            }
            public OrderPayInfo(int id)
            {
                PBM_OrderPayInfo m = new PBM_OrderPayInfoBLL(id).Model;
                if (m != null) FillModel(m);
            }

            private void FillModel(PBM_OrderPayInfo m)
            {
                ID = m.ID;
                PayMode = m.PayMode;
                Amount = m.Amount;
                Remark = m.Remark;
                ApproveFlag = m.ApproveFlag;

                #region 获取字典表名称
                try
                {
                    if (m.PayMode > 0)
                    {
                        Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_AC_PayMode")[m.PayMode.ToString()];
                        if (dic != null) PayModeName = dic.Name;
                    }
                }
                catch (System.Exception err)
                {
                    LogWriter.WriteLog("MCSFramework.WSI.PBM_OrderPayInfo", err);
                }
                #endregion
            }

        }
        #endregion
    }


}
