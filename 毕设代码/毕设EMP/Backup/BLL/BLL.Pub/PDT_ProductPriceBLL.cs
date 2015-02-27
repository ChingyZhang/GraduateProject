
// ===================================================================
// 文件： PDT_ProductPriceDAL.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   
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
    ///PDT_ProductPriceBLL业务逻辑BLL类
    /// </summary>
    public class PDT_ProductPriceBLL : BaseComplexBLL<PDT_ProductPrice, PDT_ProductPrice_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductPriceDAL";
        private PDT_ProductPriceDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_ProductPriceBLL
        ///</summary>
        public PDT_ProductPriceBLL()
            : base(DALClassName)
        {
            _dal = (PDT_ProductPriceDAL)_DAL;
            _m = new PDT_ProductPrice();
        }

        public PDT_ProductPriceBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductPriceDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductPriceBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductPriceDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        public void Approve(int StaffID)
        {
            _dal.Approve(StaffID);
        }

        public void UnApprove(int StaffID)
        {
            _dal.UnApprove(StaffID);
        }

        #region	静态GetModelList方法
        public static IList<PDT_ProductPrice> GetModelList(string condition)
        {
            return new PDT_ProductPriceBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetProductPriceList(int ClientID)
        {
            PDT_ProductPriceDAL dal = (PDT_ProductPriceDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetProductPriceList(ClientID));
        }

        /// <summary>
        /// 从价表中取某个客户指定产品的价格
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="Product"></param>
        /// <param name="Type">销量类型 1:经销商进货 2:门店从经销商进货 3:门店售出</param>
        /// <returns></returns>
        public static int GetPriceByClientAndType(int ClientID, int Product, int Type, out decimal FactoryPrice, out decimal Price)
        {
            PDT_ProductPriceDAL dal = (PDT_ProductPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPriceByClientAndType(ClientID, Product, Type, out FactoryPrice, out Price);
        }

        /// <summary>
        /// 从上级供货商复制价表
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int CopyFromSupplier(int Client, int Staff)
        {
            PDT_ProductPriceDAL dal = (PDT_ProductPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.CopyFromSupplier(Client, Staff);
        }

        /// <summary>
        /// 从标准价表复制价表
        /// </summary>
        /// <param name="StandardPrice"></param>
        /// <param name="Client"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int CopyFromStandardPrice(int StandardPrice, int Client, int Staff)
        {
            PDT_ProductPriceDAL dal = (PDT_ProductPriceDAL)DataAccess.CreateObject(DALClassName);
            return dal.CopyFromStandardPrice(StandardPrice, Client, Staff);
        }
    }
}