
// ===================================================================
// 文件： PDT_StandardPriceDAL.cs
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
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///PDT_StandardPriceBLL业务逻辑BLL类
    /// </summary>
    public class PDT_StandardPriceBLL : BaseComplexBLL<PDT_StandardPrice, PDT_StandardPrice_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_StandardPriceDAL";
        private PDT_StandardPriceDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_StandardPriceBLL
        ///</summary>
        public PDT_StandardPriceBLL()
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            _m = new PDT_StandardPrice();
        }

        public PDT_StandardPriceBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            FillModel(id);
        }

        public PDT_StandardPriceBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PDT_StandardPrice> GetModelList(string condition)
        {
            return new PDT_StandardPriceBLL()._GetModelList(condition);
        }
        #endregion

        #region 创建经销商的默认价表
        /// <summary>
        /// 创建经销商的默认价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <returns>创建的默认价表ID，如果已有默认价表，则返回默认价表ID</returns>
        public static int CreateDefaultPrice(int Supplier)
        {
            PDT_StandardPriceDAL dal = (PDT_StandardPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.CreateDefaultPrice(Supplier);
        }
        #endregion

        #region 获取经销商价表
        /// <summary>
        /// 获取经销商指定渠道及区域的适用价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <param name="FitRTChannel">TDP自分渠道</param>
        /// <param name="FitSalesArea">TDP自分区域</param>
        /// <returns></returns>
        public static int GetFitPrice(int Supplier, int FitRTChannel, int FitSalesArea)
        {
            PDT_StandardPriceDAL dal = (PDT_StandardPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetFitPrice(Supplier, FitRTChannel, FitSalesArea);
        }

        /// <summary>
        /// 获取经销商默认价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <returns></returns>
        public static int GetDefaultPrice(int Supplier)
        {
            IList<PDT_StandardPrice> lists = GetModelList("Supplier=" + Supplier.ToString() + " AND IsDefault='Y' AND IsEnabled = 'Y'");
            if (lists.Count == 0)
                return CreateDefaultPrice(Supplier);
            else
                return lists[0].ID;
        }

        /// <summary>
        /// 返回指定供货商的所有价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <returns></returns>
        public static IList<PDT_StandardPrice> GetAllPrice_BySupplier(int Supplier)
        {
            return GetModelList("Supplier=" + Supplier.ToString() + " AND IsEnabled='Y'");
        }
        #endregion

        /// <summary>
        /// 获取向门店销售指定产品里的销售价
        /// </summary>
        /// <param name="Client">门店</param>
        /// <param name="Supplier">经销商</param>
        /// <param name="Product">产品</param>
        /// <returns>销售价，小于0失败</returns>
        public static decimal GetSalePrice(int Client, int Supplier, int Product)
        {
            PDT_StandardPriceDAL dal = (PDT_StandardPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSalePrice(Client, Supplier, Product);
        }
    }
}