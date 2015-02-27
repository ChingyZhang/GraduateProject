
// ===================================================================
// 文件： ORD_OrderDeliveryDAL.cs
// 项目名称：
// 创建时间：2009/4/26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Logistics;
using MCSFramework.SQLDAL.Logistics;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

namespace MCSFramework.BLL.Logistics
{
    /// <summary>
    ///ORD_OrderDeliveryBLL业务逻辑BLL类
    /// </summary>
    public class ORD_OrderDeliveryBLL : BaseComplexBLL<ORD_OrderDelivery, ORD_OrderDeliveryDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_OrderDeliveryDAL";
        private ORD_OrderDeliveryDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_OrderDeliveryBLL
        ///</summary>
        public ORD_OrderDeliveryBLL()
            : base(DALClassName)
        {
            _dal = (ORD_OrderDeliveryDAL)_DAL;
            _m = new ORD_OrderDelivery();
        }

        public ORD_OrderDeliveryBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_OrderDeliveryDAL)_DAL;
            FillModel(id);
        }

        public ORD_OrderDeliveryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_OrderDeliveryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_OrderDelivery> GetModelList(string condition)
        {
            return new ORD_OrderDeliveryBLL()._GetModelList(condition);
        }
        #endregion

        public int Approve(int StaffID)
        {
            return _dal.Approve(_m.ID, StaffID);
        }

        public int Delivery(int StaffID)
        {
            #region 将发货信息写入销量数据库
            //Hashtable ht_SVM = new Hashtable();

            //foreach (ORD_OrderDeliveryDetail m in Items)
            //{
            //    SVM_SalesVolumeBLL svmbll;
            //    if (ht_SVM.ContainsKey(m.Client))
            //    {
            //        svmbll = (SVM_SalesVolumeBLL)ht_SVM[m.Client];
            //    }
            //    else
            //    {
            //        #region 创建销量头类
            //        CM_Client c = new CM_ClientBLL(m.Client).Model;

            //        svmbll = new SVM_SalesVolumeBLL();
            //        svmbll.Model.AccountMonth = _m.AccountMonth;
            //        svmbll.Model.ApproveFlag = 1;
            //        svmbll.Model.Client = m.Client;
            //        svmbll.Model.Flag = 1;                          //进货
            //        svmbll.Model.InsertStaff = StaffID;
            //        svmbll.Model.OrganizeCity = c.OrganizeCity;     //客户所在的片区
            //        svmbll.Model.SalesDate = DateTime.Now;
            //        svmbll.Model.SheetCode = _m.SheetCode + "-" + m.Client.ToString();
            //        svmbll.Model.Supplier = _m.Store;
            //        if (c.ClientType == 1)
            //            svmbll.Model.Type = 4;      //仓库进货
            //        else
            //            svmbll.Model.Type = 1;      //经销商进货
            //        svmbll.Items = new List<SVM_SalesVolume_Detail>();

            //        ht_SVM.Add(m.Client, svmbll);
            //        #endregion
            //    }
            //    #region 加入销量明细
            //    SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
            //    detail.Product = m.Product;
            //    detail.FactoryPrice = m.FactoryPrice;
            //    detail.SalesPrice = m.Price;
            //    detail.Quantity = m.DeliveryQuantity;
            //    svmbll.Items.Add(detail);
            //    #endregion
            //}

            //foreach (object o in ht_SVM.Values)
            //{
            //    SVM_SalesVolumeBLL svmbll = (SVM_SalesVolumeBLL)o;
            //    svmbll.Add();
            //    svmbll.Approve(StaffID);
            //}
            #endregion

            return _dal.Delivery(_m.ID, StaffID);
        }

        public int SignIn(int StaffID)
        {
            return _dal.SignIn(_m.ID, StaffID);
        }
        /// <summary>
        /// 生成定单请购发放单号 格式：ODXF+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public static string GenerateSheetCode(int organizecity, int accountmonth)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(organizecity, accountmonth);
        }

        /// <summary>
        /// 获取定单请购发放单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumCost(int ID)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumCost(ID);
        }


        /// <summary>
        /// 初始化录入发货单产品列表
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="IsCXP">是否是促销品，0：成品，取价表目录 1：促销品，取促销品库目录</param>
        /// <returns></returns>
        public static DataTable InitProductList(int Client, int IsCXP)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitProductList(Client, IsCXP);
        }


        /// <summary>
        /// 初始化录入出库反馈
        /// </summary>
        /// <param name="jxsID"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable InitOrderDeliveryList(string jxsID, string sDate, string eDate)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitOrderDeliveryList(jxsID,sDate,eDate);
        }

        public DataTable InitOrderAlibrayList()
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.InitOrderAlibrayList();
        }

        public DataTable GetOrderAlibrayDetailList(string OrderNo)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetOrderAlibrayDetailList(OrderNo);
        }

        public DataTable GetOrderStorageDetailList(string BillNo)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetOrderStorageDetailList(BillNo);
        }

        /// <summary>
        /// 获取入库实发数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static int GetSumNumber(int ID)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumNumber(ID);
        }

        /// <summary>
        /// 获取入库应反馈数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static int GetSumQuantity(int ID)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumQuantity(ID);
        }

        /// <summary>
        /// 获取入库详细应反馈数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static int GetSumNum(string ProdID)
        {
            ORD_OrderDeliveryDAL dal = (ORD_OrderDeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumNum(ProdID);
        }

    }
}