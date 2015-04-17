
// ===================================================================
// 文件： ORD_OrderDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
    /// <summary>
    ///ORD_OrderBLL业务逻辑BLL类
    /// </summary>
    public class ORD_OrderBLL : BaseComplexBLL<ORD_Order, ORD_OrderDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.ORD_OrderDAL";
        private ORD_OrderDAL _dal;

        /// <summary>
        /// 合计总金额
        /// </summary>
        public decimal TotalConfirmCost
        {
            get
            {
                if (Items != null)
                    return Items.Sum(p => p.ConfirmQuantity * p.Price);
                else
                    return 0;
            }
        }

        /// <summary>
        /// 合计发放金额
        /// </summary>
        public decimal TotalDeliveredCost
        {
            get
            {
                if (Items != null)
                    return Items.Sum(p => p.DeliveredQuantity * p.Price);
                else
                    return 0;
            }
        }
        /// <summary>
        /// 合计总积分额
        /// </summary>
        public decimal TotalPoints
        {
            get
            {
                if (Items != null)
                    return Items.Sum(p => p.ConfirmQuantity * p.Points);
                else
                    return 0;
            }
        }
        #region 构造函数
        ///<summary>
        ///ORD_OrderBLL
        ///</summary>
        public ORD_OrderBLL()
            : base(DALClassName)
        {
            _dal = (ORD_OrderDAL)_DAL;
            _m = new ORD_Order();
        }

        public ORD_OrderBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_OrderDAL)_DAL;
            FillModel(id);
        }

        public ORD_OrderBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_OrderDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_Order> GetModelList(string condition)
        {
            return new ORD_OrderBLL()._GetModelList(condition);
        }
        #endregion

        #region 生成发货单号
        public static string GenerateSheetCode(int WareHouse)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(WareHouse);
        }
        #endregion

        #region 设置订单状态
        /// <summary>
        /// 请购商提交订单
        /// </summary>
        /// <param name="User"></param>
        /// <returns>0:成功 -10:积分余额不足 </returns>
        public int Submit(Guid User)
        {
            ORD_Order neworder = new ORD_OrderBLL(_m.ID).Model;
            if (neworder == null || neworder.State != 1) return -1;

            if (_m.PayMode == 8)
            {
                #region 扣减客户积分
                /*
                //判断积分是否足够
                MCSFramework.SQLDAL.RM.RM_AccountDAL accountdal = new MCSFramework.SQLDAL.RM.RM_AccountDAL();
                decimal totalbalance = 0;
                ORD_PublishBLL publish = new ORD_PublishBLL(_m.PublishID);
                foreach (ORD_PublishWithAccountType item in publish.GetAccountType())
                {
                    totalbalance += accountdal.GetBalance(_m.Client, item.AccountType);
                }
                decimal totalpoints = Items.Sum(p => p.BookQuantity * p.Points);
                if (totalpoints > totalbalance) return -10;

                //依次扣减积分
                decimal points = totalpoints;
                foreach (ORD_PublishWithAccountType item in publish.GetAccountType())
                {
                    decimal b = accountdal.GetBalance(_m.Client, item.AccountType);
                    if (b < 0) continue;    //某账户小于0分时，不处理

                    if (b >= points)
                    {
                        accountdal.ModifyPoints(_m.Client, item.AccountType, 0 - points, 0, 11, "提交积分兑换订单,订单号:" + _m.SheetCode);
                        AddSpentPoints(item.AccountType, 1, 0 - points);
                        break;
                    }
                    else
                    {
                        accountdal.ModifyPoints(_m.Client, item.AccountType, 0 - b, 0, 11, "提交积分兑换订单,订单号:" + _m.SheetCode);
                        AddSpentPoints(item.AccountType, 1, 0 - b);
                        points -= b;
                    }
                }
                */
                #endregion
            }

            return _dal.SetState(_m.ID, 2, User);
        }

        /// <summary>
        /// 供货商确认订单
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Confirm(Guid User)
        {
            //获取订单的最新状态
            ORD_Order neworder = new ORD_OrderBLL(_m.ID).Model;
            if (neworder == null || neworder.State != 2) return -1;

            if (_m.PayMode == 8)
            {
                /*
                MCSFramework.SQLDAL.RM.RM_AccountDAL accountdal = new MCSFramework.SQLDAL.RM.RM_AccountDAL();

                #region 向供货商增加积分
                foreach (ORD_OrderSpentPoints item in GetSpentPointsList())
                {
                    decimal p = 0 - item.Points;    //原订单提交时扣减的积分（取正值）
                    accountdal.ModifyPoints(_m.Supplier, item.AccountType, p, 0, 12, "确认积分兑换订单,订单号:" + _m.SheetCode);
                }
                #endregion

                #region 退还调整的积分余额
                decimal adjustpoints = Items.Sum(p => (p.BookQuantity - p.ConfirmQuantity) * p.Points);
                if (adjustpoints > 0)
                {
                    foreach (ORD_OrderSpentPoints item in GetSpentPointsList())
                    {
                        decimal p = 0 - item.Points;    //原订单提交时扣减的积分（取正值）
                        if (p > adjustpoints)
                        {
                            accountdal.ModifyPoints(_m.Client, item.AccountType, adjustpoints, 0, 11, "确认积分兑换订单,退还确认后的差额,订单号:" + _m.SheetCode);
                            AddSpentPoints(item.AccountType, 2, adjustpoints);
                            break;
                        }
                        else
                        {
                            accountdal.ModifyPoints(_m.Client, item.AccountType, p, 0, 11, "确认积分兑换订单,退还确认后的差额,订单号:" + _m.SheetCode);
                            AddSpentPoints(item.AccountType, 2, p);
                            adjustpoints -= p;
                        }
                    }
                              
                #endregion
              * */
            }
            return _dal.SetState(_m.ID, 3, User);
        }

        /// <summary>
        /// 完成订单发货
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Complete(Guid User)
        {
            return _dal.SetState(_m.ID, 5, User);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Cancel(Guid User)
        {
            ORD_Order neworder = new ORD_OrderBLL(_m.ID).Model;
            if (neworder == null || neworder.State != 2) return -1;

            if (_m.PayMode == 8)
            {
            //    #region 依次返还原扣除的积分
            //    MCSFramework.SQLDAL.RM.RM_AccountDAL accountdal = new MCSFramework.SQLDAL.RM.RM_AccountDAL();
            //    foreach (ORD_OrderSpentPoints item in GetSpentPointsList())
            //    {
            //        accountdal.ModifyPoints(_m.Client, item.AccountType, 0 - item.Points, 0, 11, "取消积分兑换订单,订单号:" + _m.SheetCode);
            //        AddSpentPoints(item.AccountType, 2, 0 - item.Points);
            //    }
            //    #endregion
            }
            return _dal.SetState(_m.ID, 8, User);
        }
        #endregion

        #region 设置订单完成结算
        /// <summary>
        /// 设置订单完成结算
        /// </summary>
        /// <param name="PayMode"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int SetBalanced(int PayMode, Guid User)
        {
            return _dal.SetBalanced(_m.ID, PayMode, User);
        }

        /// <summary>
        /// 设置订单完成结算
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int SetBalanced(Guid User)
        {
            return _dal.SetBalanced(_m.ID, 0, User);
        }
        #endregion

        #region 获取可发货的订单列表
        /// <summary>
        /// 获取可以发货的订单列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static IList<ORD_Order> GetCanDeliveryOrderList(int Supplier, int Client)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCanDeliveryOrderList(Supplier, Client);
        }

        /// <summary>
        /// 获取可以发货的订单列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        public static IList<ORD_Order> GetCanDeliveryOrderList(int Supplier)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCanDeliveryOrderList(Supplier, 0);
        }

        /// <summary>
        /// 获取该订单中可以发货的产品明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetCanDeliveryOrderDetail()
        {
            return _dal.GetCanDeliveryOrderDetail(_m.ID);
        }
        #endregion

        #region 积分兑换订单的积分扣减情况
        /// <summary>
        /// 增加订单的扣减积分情况
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="AccountType"></param>
        /// <param name="Flag"></param>
        /// <param name="Points"></param>
        /// <returns></returns>
        public int AddSpentPoints(int AccountType, int Flag, decimal Points)
        {
            return _dal.AddSpentPoints(_m.ID, AccountType, Flag, Points);
        }

        /// <summary>
        /// 获取订单的积分扣减(或返还)信息
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IList<ORD_OrderSpentPoints> GetSpentPointsList()
        {
            return _dal.GetSpentPointsList(_m.ID);
        }
        #endregion

        #region 获取某个客户指定月份、指定产品已订货数量
        public static int GetSubmitQuantity(int AccountMonth, int Client, int Product, int OrderClassify)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSubmitQuantity(AccountMonth, Client, Product, OrderClassify);
        }
        #endregion

        #region 统计各客户订货汇总信息
        public static DataTable GetClientSummary(int OrganizeCity, int ClientType, int OrderType, DateTime BeginDate, DateTime EndDate)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientSummary(OrganizeCity, 0, 0, ClientType, OrderType, BeginDate, EndDate);
        }
        public static DataTable GetClientSummary_BySupplier(int Supplier, int OrderType, DateTime BeginDate, DateTime EndDate)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientSummary(0, Supplier, 0, 0, OrderType, BeginDate, EndDate);
        }
        public static DataTable GetClientSummary_ByClient(int Client, int OrderType, DateTime BeginDate, DateTime EndDate)
        {
            ORD_OrderDAL dal = (ORD_OrderDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientSummary(0, 0, Client, 0, OrderType, BeginDate, EndDate);
        }
        #endregion
    }
}