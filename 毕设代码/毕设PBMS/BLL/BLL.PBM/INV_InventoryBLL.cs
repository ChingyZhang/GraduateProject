
// ===================================================================
// 文件： INV_InventoryDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
    /// <summary>
    ///INV_InventoryBLL业务逻辑BLL类
    /// </summary>
    public class INV_InventoryBLL : BaseSimpleBLL<INV_Inventory>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.PBM.INV_InventoryDAL";
        private INV_InventoryDAL _dal;

        #region 构造函数
        ///<summary>
        ///INV_InventoryBLL
        ///</summary>
        public INV_InventoryBLL()
            : base(DALClassName)
        {
            _dal = (INV_InventoryDAL)_DAL;
            _m = new INV_Inventory();
        }

        public INV_InventoryBLL(long id)
            : base(DALClassName)
        {
            _dal = (INV_InventoryDAL)_DAL;
            FillModel(id);
        }

        public INV_InventoryBLL(long id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_InventoryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<INV_Inventory> GetModelList(string condition)
        {
            return new INV_InventoryBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取仓库内指定商品的库存信息
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static IList<INV_Inventory> GetInventoryListByProduct(int WareHouse, int Product)
        {
            string condition = string.Format(" WareHouse = {0} AND Product = {1} AND Quantity > 0", WareHouse, Product);
            return GetModelList(condition);
        }

        /// <summary>
        /// 获取指定批号的产品的库存信息，批号为空时，获取最小批号的信息
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static INV_Inventory GetInventoryByProductAndLotNumber(int WareHouse, int Product, string LotNumber)
        {
            string condition = string.Format(" WareHouse = {0} AND Product = {1} AND LotNumber='{2}'", WareHouse, Product, LotNumber);
            IList<INV_Inventory> list = GetModelList(condition);

            if (list.Count > 0)
                return list[0];
            else if (string.IsNullOrEmpty(LotNumber))
            {
                condition = string.Format(" WareHouse = {0} AND Product = {1} AND Quantity > 0", WareHouse, Product);
                list = GetModelList(condition);

                if (list.Count > 0)
                    return list.OrderBy(p => p.ProductDate).ThenBy(p => p.LotNumber).ToList()[0];
            }

            return null;
        }
        /// <summary>
        /// 增加指定仓库商品库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="Price"></param>
        /// <param name="Quantity"></param>
        /// <returns>0:成功</returns>
        public static int IncreaseQuantity(int WareHouse, int Product, string LotNumber, decimal Price, int Quantity)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.IncreaseQuantity(WareHouse, Product, LotNumber, Price, Quantity);
        }

        /// <summary>
        /// 扣减指定仓库商品库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="Quantity"></param>
        /// <returns>0:成功 -1：库存不够</returns>
        public static int DecreaseQuantity(int WareHouse, int Product, string LotNumber, int Quantity)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DecreaseQuantity(WareHouse, Product, LotNumber, Quantity);
        }

        /// <summary>
        /// 查询指定仓库某产品的库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns>库存数量</returns>
        public static int GetProductQuantity(int WareHouse, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductQuantity(WareHouse, Product, LotNumber);
        }

        /// <summary>
        /// 查询指定仓库某产品的库存（不限批号）
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <returns>库存数量</returns>
        public static int GetProductQuantity(int WareHouse, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductQuantity(WareHouse, Product, "");
        }

        /// <summary>
        /// 查询指定客户的所有仓库内某产品的库存
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns>库存数量</returns>
        public static int GetProductQuantityAllWareHouse(int Client, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductQuantityAllWareHouse(Client, Product, LotNumber);
        }

        /// <summary>
        /// 查询指定客户的所有仓库内某产品的库存（不限批号）
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns>库存数量</returns>
        public static int GetProductQuantityAllWareHouse(int Client, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductQuantityAllWareHouse(Client, Product, "");
        }


        /// <summary>
        /// 获取指定客户两年以内指定产品所有的库存批号信息（以便销售退货时可以选择判断）
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static IList<INV_Inventory> GetListByClientAndProduct(int Client, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByClientAndProduct(Client, Product);
        }

        /// <summary>
        /// 获取指定客户所有仓库内指定产品及批号的库存信息（以便销售退货时可以选择判断）
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static INV_Inventory GetInventoryByClientAndProduct(int Client, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetInventoryByClientAndProduct(Client, Product, LotNumber);
        }

    }
}