using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class Delivery
    {
        #region 发货单字段
        /// <summary>
        /// 发货单ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 发货单号
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
        /// 收货商信息
        /// </summary>
        public string ClientAddress = "";
        public string ClientLinkMan = "";
        public string ClientTeleNum = "";

        /// <summary>
        /// 发货商仓库
        /// </summary>
        public int SupplierWareHouse = 0;
        public string SupplierWareHouseName = "";

        /// <summary>
        /// 收货商仓库
        /// </summary>
        public int ClientWareHouse = 0;
        public string ClientWareHouseName = "";

        /// <summary>
        /// 业务员
        /// </summary>
        public int SalesMan = 0;
        public string SalesManName = "";

        /// <summary>
        /// 送货员
        /// </summary>
        public int DeliveryMan = 0;
        public string DeliveryManName = "";

        /// <summary>
        /// 发货单类别 1:销售单 2:退货单 3:调拨单 4:赠送单
        /// </summary>
        public int Classify = 0;
        public string ClassifyName = "";

        /// <summary>
        /// 备单标志 1:快捷模式、2:车销模式 3：完整模式
        /// </summary>
        public int PrepareMode = 0;
        public string PrepareModeName = "";

        /// <summary>
        /// 发货单状态 1:备单 2:装车 3:在途 4:到货 5:退单 9:取消
        /// </summary>
        public int State = 0;
        public string StateName = "";

        /// <summary>
        /// 供货价表
        /// </summary>
        public int StandardPrice = 0;
        public string StandardPriceName = "";

        /// <summary>
        /// 关联订单ID
        /// </summary>
        public int OrderID = 0;

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
        /// 送货车辆
        /// </summary>
        public int DeliveryVehicle = 0;
        public string DeliveryVehicleName = "";

        /// <summary>
        /// 预计到达日期
        /// </summary>
        public DateTime PreArrivalDate = new DateTime(1900, 1, 1);

        /// <summary>
        /// 装车日期
        /// </summary>
        public DateTime LoadingTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 发车日期
        /// </summary>
        public DateTime DepartTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 实际到货日期
        /// </summary>
        public DateTime ActArriveTime = new DateTime(1900, 1, 1);

        /// <summary>
        /// 发货单创建日期
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
        /// 发货单明细
        /// </summary>
        public List<DeliveryDetail> Items = new List<DeliveryDetail>();

        /// <summary>
        /// 发货单收款信息
        /// </summary>
        public List<DeliveryPayInfo> PayInfos = new List<DeliveryPayInfo>();
        #endregion

        public Delivery() { }
        public Delivery(int DeliveryID)
        {
            FillModel(DeliveryID);
        }
        public Delivery(PBM_Delivery m)
        {
            if (m != null) FillModel(m.ID);
        }

        private void FillModel(int DeliveryID)
        {
            PBM_DeliveryBLL bll = new PBM_DeliveryBLL(DeliveryID);
            if (bll.Model == null) { ID = -1; return; }

            PBM_Delivery m = bll.Model;
            ID = m.ID;
            SheetCode = m.SheetCode;
            Supplier = m.Supplier;
            SupplierWareHouse = m.SupplierWareHouse;
            ClientWareHouse = m.ClientWareHouse;
            Client = m.Client;
            SalesMan = m.SalesMan;
            DeliveryMan = m.DeliveryMan;
            Classify = m.Classify;
            PrepareMode = m.PrepareMode;
            State = m.State;
            StandardPrice = m.StandardPrice;
            OrderID = m.OrderId;
            DiscountAmount = m.DiscountAmount;
            WipeAmount = m.WipeAmount;
            ActAmount = m.ActAmount;
            DeliveryVehicle = m.DeliveryVehicle;
            PreArrivalDate = m.PreArrivalDate;
            LoadingTime = m.LoadingTime;
            DepartTime = m.DepartTime;
            ActArriveTime = m.ActArriveTime;
            InsertTime = m.InsertTime;

            WorkList = m.WorkList;
            ApproveFlag = m.ApproveFlag;

            Remark = m.Remark;

            //发货单明细
            Items = new List<DeliveryDetail>();
            foreach (PBM_DeliveryDetail item in bll.Items)
            {
                Items.Add(new DeliveryDetail(item));
            }

            //付款信息列表
            PayInfos = new List<DeliveryPayInfo>();
            foreach (PBM_DeliveryPayInfo item in bll.GetPayInfoList())
            {
                PayInfos.Add(new DeliveryPayInfo(item));
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
                if (c != null)
                {
                    ClientName = c.FullName;
                    ClientAddress = c.DeliveryAddress == "" ? c.Address : c.DeliveryAddress;
                    ClientLinkMan = c.LinkManName;
                    ClientTeleNum = c.Mobile == "" ? c.TeleNum : c.Mobile;
                }
            }
            if (SupplierWareHouse > 0)
            {
                CM_WareHouse w = new CM_WareHouseBLL(SupplierWareHouse).Model;
                if (w != null) SupplierWareHouseName = w.Name;
            }
            if (ClientWareHouse > 0)
            {
                CM_WareHouse w = new CM_WareHouseBLL(ClientWareHouse).Model;
                if (w != null) ClientWareHouseName = w.Name;
            }
            if (SalesMan > 0)
            {
                Org_Staff s = new Org_StaffBLL(SalesMan).Model;
                if (s != null) SalesManName = s.RealName;
            }
            if (DeliveryMan > 0)
            {
                Org_Staff s = new Org_StaffBLL(DeliveryMan).Model;
                if (s != null) DeliveryManName = s.RealName;
            }
            if (StandardPrice > 0)
            {
                PDT_StandardPrice s = new PDT_StandardPriceBLL(StandardPrice).Model;
                if (s != null) StandardPriceName = s.Name;
            }
            if (DeliveryVehicle > 0)
            {
                CM_Vehicle v = new CM_VehicleBLL(DeliveryVehicle).Model;
                if (v != null) DeliveryVehicleName = v.VehicleNo;
            }
            #endregion

            #region 获取字典表名称
            try
            {
                if (m.Classify > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_DeliveryClassify")[m.Classify.ToString()];
                    if (dic != null) ClassifyName = dic.Name;
                }
                if (m.PrepareMode > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_DeliveryPrepareMode")[m.PrepareMode.ToString()];
                    if (dic != null) PrepareModeName = dic.Name;
                }
                if (m.State > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("PBM_DeliveryState")[m.State.ToString()];
                    if (dic != null) StateName = dic.Name;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MCSFramework.WSI.Delivery", err);
            }
            #endregion
        }

        #region 发货单明细
        [Serializable]
        public class DeliveryDetail
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
            /// 批号
            /// </summary>
            public string LotNumber = "";

            /// <summary>
            /// 销售价
            /// </summary>
            public decimal Price = 0;

            /// <summary>
            /// 送货数量
            /// </summary>
            public int DeliveryQuantity = 0;

            /// <summary>
            /// 签收数量
            /// </summary>
            public int SignInQuantity = 0;

            /// <summary>
            /// 销售方式
            /// </summary>
            public int SalesMode = 0;
            public string SalesModeName = "";

            /// <summary>
            /// 折扣率，1为全价，0为免费，0.7为7折销售
            /// </summary>
            public decimal DiscountRate = 1;

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

            public DeliveryDetail() { }

            public DeliveryDetail(PBM_DeliveryDetail m)
            {
                DetailID = m.ID;
                Product = m.Product;
                LotNumber = m.LotNumber;
                Price = m.Price;
                DeliveryQuantity = m.DeliveryQuantity;
                SignInQuantity = m.SignInQuantity;
                SalesMode = m.SalesMode;
                DiscountRate = m.DiscountRate;
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
                    LogWriter.WriteLog("MCSFramework.WSI.DeliveryDetail", err);
                }
                #endregion
            }
        }
        #endregion

        #region 发货单付款信息
        /// <summary>
        /// 发货单付款信息
        /// </summary>
        [Serializable]
        public class DeliveryPayInfo
        {
            #region 公共属性
            ///<summary>
            ///ID
            ///</summary>
            public int ID = 0;

            ///<summary>
            ///收款方式，1现金付款、2POS付款  11余额支付 12记应收
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

            public DeliveryPayInfo() { }
            public DeliveryPayInfo(PBM_DeliveryPayInfo m)
            {
                if (m != null) FillModel(m);
            }
            public DeliveryPayInfo(int id)
            {
                PBM_DeliveryPayInfo m = new PBM_DeliveryPayInfoBLL(id).Model;
                if (m != null) FillModel(m);
            }

            private void FillModel(PBM_DeliveryPayInfo m)
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
                    LogWriter.WriteLog("MCSFramework.WSI.DeliveryPayInfo", err);
                }
                #endregion
            }

        }
        #endregion

    }


}