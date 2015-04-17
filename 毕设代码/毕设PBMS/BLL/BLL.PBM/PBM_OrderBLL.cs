
// ===================================================================
// 文件： PBM_OrderDAL.cs
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
    ///PBM_OrderBLL业务逻辑BLL类
    /// </summary>
    public class PBM_OrderBLL : BaseComplexBLL<PBM_Order, PBM_OrderDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.PBM.PBM_OrderDAL";
        private PBM_OrderDAL _dal;

        #region 构造函数
        ///<summary>
        ///PBM_OrderBLL
        ///</summary>
        public PBM_OrderBLL()
            : base(DALClassName)
        {
            _dal = (PBM_OrderDAL)_DAL;
            _m = new PBM_Order();
        }

        public PBM_OrderBLL(int id)
            : base(DALClassName)
        {
            _dal = (PBM_OrderDAL)_DAL;
            FillModel(id);
        }

        public PBM_OrderBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PBM_OrderDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PBM_Order> GetModelList(string condition)
        {
            return new PBM_OrderBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 确认提交订货单
        /// </summary>
        /// <returns>-1:未找到符合条件的订货单</returns>
        public int Submit(int Staff)
        {
            return _dal.Submit(_m.ID, Staff);
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
        /// 获取订货单付款信息列表
        /// </summary>
        /// <returns></returns>
        public IList<PBM_OrderPayInfo> GetPayInfoList()
        {
            return PBM_OrderPayInfoBLL.GetModelList("OrderID=" + _m.ID.ToString());
        }


        /// <summary>
        /// 根据订单创建发货单(到派车状态)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="SupplierWareHouse"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="PreArrivalDate"></param>
        /// <param name="Staff"></param>
        /// <returns>大于0:发货单号 -1:无符合条件的订单 -2:无效的出库仓库 </returns>
        public int CreateDelivery(int SupplierWareHouse, int DeliveryMan, int DeliveryVehicle, DateTime PreArrivalDate, int Staff)
        {
            return _dal.CreateDelivery(_m.ID, SupplierWareHouse, DeliveryMan, DeliveryVehicle, PreArrivalDate, Staff);
        }

        /// <summary>
        /// 根据订单创建发货单(未派车状态)
        /// </summary>
        /// <param name="SupplierWareHouse"></param>
        /// <param name="PreArrivalDate"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int CreateDelivery(int SupplierWareHouse, DateTime PreArrivalDate, int Staff)
        {
            return CreateDelivery(SupplierWareHouse, 0, 0, PreArrivalDate, Staff);
        }

        /// <summary>
        /// 按零售门店汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetOrderSummary_ByClient(int Supplier, int SalesMan, DateTime BeginDate, DateTime EndDate)
        {
            PBM_OrderDAL dal = (PBM_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetOrderSummary_ByClient(Supplier, SalesMan, BeginDate, EndDate);
        }

        /// <summary>
        /// 按产品汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static DataTable GetOrderSummary_ByProduct(int Supplier, int SalesMan, DateTime BeginDate, DateTime EndDate)
        {
            PBM_OrderDAL dal = (PBM_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetOrderSummary_ByProduct(Supplier, SalesMan, BeginDate, EndDate);
        }
    }
}