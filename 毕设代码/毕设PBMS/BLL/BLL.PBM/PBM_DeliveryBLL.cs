
// ===================================================================
// 文件： PBM_DeliveryBLL.cs
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
    ///PBM_DeliveryBLL业务逻辑BLL类
    /// </summary>
    public class PBM_DeliveryBLL : BaseComplexBLL<PBM_Delivery, PBM_DeliveryDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.PBM.PBM_DeliveryDAL";
        private PBM_DeliveryDAL _dal;

        #region 构造函数
        ///<summary>
        ///PBM_DeliveryBLL
        ///</summary>
        public PBM_DeliveryBLL()
            : base(DALClassName)
        {
            _dal = (PBM_DeliveryDAL)_DAL;
            _m = new PBM_Delivery();
        }

        public PBM_DeliveryBLL(int id)
            : base(DALClassName)
        {
            _dal = (PBM_DeliveryDAL)_DAL;
            FillModel(id);
        }

        public PBM_DeliveryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PBM_DeliveryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PBM_Delivery> GetModelList(string condition)
        {
            return new PBM_DeliveryBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 确认提交发货单
        /// </summary>
        /// <returns>-1:未找到符合条件的出库单 -2:出库单未指定供库仓库 -10:库存不足</returns>
        public int Approve()
        {
            return _dal.Approve(_m.ID);
        }

        /// <summary>
        /// 取消发货单
        /// </summary>
        /// <param name="Staff"></param>
        /// <param name="CancelReason"></param>
        /// <returns></returns>
        public int Cancel(int Staff, string CancelReason)
        {
            return _dal.Cancel(_m.ID, Staff, CancelReason);
        }
        /// <summary>
        /// 清除付款信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int ClearPayInfo()
        {
            return _dal.ClearPayInfo(_m.ID);
        }

        /// <summary>
        /// 将预售模式发货单设为派单状态
        /// </summary>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="PreArrivalDate"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Assign(int DeliveryMan, int DeliveryVehicle, DateTime PreArrivalDate, int Staff)
        {
            return _dal.Assign(_m.ID, DeliveryMan, DeliveryVehicle, PreArrivalDate, Staff);
        }

        /// <summary>
        /// 单据确认
        /// </summary>
        /// <param name="Staff"></param>
        /// <returns>0：成功 -1：单据状态不可操作 -2、-3：未指定正确的仓库 -10：库存不够</returns>
        public int Confirm(int Staff)
        {
            switch (_m.Classify)
            {
                case 1:     //销售单
                case 4:     //物品赠送单
                    return _dal.ConfirmSaleOut(_m.ID, Staff);

                case 2:     //退货单
                    return _dal.ConfirmSaleReturn(_m.ID, Staff);

                case 3:     //调拨单
                case 5:     //车销移库
                case 6:     //车销退库
                    return _dal.ConfirmTrans(_m.ID, Staff);

                case 11:     //采购入库单
                case 14:     //市场物品入库单
                    return _dal.ConfirmPurchaseIn(_m.ID, Staff);

                case 12:     //采购退库单
                    return _dal.ConfirmPurchaseOut(_m.ID, Staff);

                case 21:     //盘点单
                    return _dal.ConfirmAdjust(_m.ID, Staff);
                default:
                    return -100;
            }
        }

        #region 初始化盘点单
        /// <summary>
        /// 初始化盘点单
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="OpStaff"></param>
        /// <param name="InventoryIDs"></param>
        /// <returns></returns>
        public static int AdjustInit(int WareHouse, int OpStaff, string InventoryIDs)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.AdjustInit(WareHouse, OpStaff, InventoryIDs);
        }

        /// <summary>
        /// 初始化盘点单
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="OpStaff"></param>
        /// <returns></returns>
        public static int AdjustInit(int WareHouse, int OpStaff)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.AdjustInit(WareHouse, OpStaff, "");
        }
        #endregion


        /// <summary>
        /// 获取发货单付款信息列表
        /// </summary>
        /// <returns></returns>
        public IList<PBM_DeliveryPayInfo> GetPayInfoList()
        {
            return PBM_DeliveryPayInfoBLL.GetModelList("DeliveryID=" + _m.ID.ToString());
        }

        /// <summary>
        /// 汇总预售待送货列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetNeedDeliverySummary(DateTime PreArrivalBeginDate, DateTime PreArrivalEndDate,
            int Supplier, int SupplierWareHouse, int SalesMan, int DeliveryMan)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetNeedDeliverySummary(PreArrivalBeginDate, PreArrivalEndDate, Supplier, SupplierWareHouse, SalesMan, DeliveryMan);
        }

        /// <summary>
        /// 按零售门店汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetDeliverySummary_ByClient(int Supplier, int SalesMan, int DeliveryMan, int DeliveryVehicle, DateTime BeginDate, DateTime EndDate)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliverySummary_ByClient(Supplier, SalesMan, DeliveryMan, DeliveryVehicle, BeginDate, EndDate);
        }

        /// <summary>
        /// 按产品汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetDeliverySummary_ByProduct(int Supplier, int SalesMan, int DeliveryMan, int DeliveryVehicle, DateTime BeginDate, DateTime EndDate)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliverySummary_ByProduct(Supplier, SalesMan, DeliveryMan, DeliveryVehicle, BeginDate, EndDate);
        }

        /// <summary>
        /// 按送货人查询销售收款明细
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetPayInfoDetail(int Supplier, int SalesMan, int DeliveryMan, DateTime BeginDate, DateTime EndDate)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPayInfoDetail(Supplier, SalesMan, DeliveryMan, BeginDate, EndDate);
        }

        /// <summary>
        /// 按送货人查询销售收款汇总
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetPayInfoSummary(int Supplier, int SalesMan, int DeliveryMan, DateTime BeginDate, DateTime EndDate)
        {
            PBM_DeliveryDAL dal = (PBM_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPayInfoSummary(Supplier, SalesMan, DeliveryMan, BeginDate, EndDate);
        }
    }
}