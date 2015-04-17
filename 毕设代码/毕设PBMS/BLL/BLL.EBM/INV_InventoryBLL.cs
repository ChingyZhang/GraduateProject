
// ===================================================================
// 文件： INV_InventoryDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
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
    ///INV_InventoryBLL业务逻辑BLL类
    /// </summary>
    public class INV_InventoryBLL : BaseSimpleBLL<INV_Inventory>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_InventoryDAL";
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

        public INV_InventoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_InventoryDAL)_DAL;
            FillModel(id);
        }

        public INV_InventoryBLL(int id, bool bycache)
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

        #region 获取某客户产品库存列表
        public static IList<INV_Inventory> GetInventoryListByWareHouse(int WareHouse)
        {
            return GetModelList("WareHouse = " + WareHouse.ToString() + " AND Quantity <> 0");
        }

        public static IList<INV_Inventory> GetInventoryListByClient(int Client)
        {
            return GetModelList("WareHouse IN (SELECT ID FROM MCS_CM.dbo.CM_WareHouse WHERE Client= " + Client.ToString() + ") AND Quantity <> 0");
        }
        #endregion

        #region 获取指定仓库指定产品的所有产品明细码
        public static IList<INV_InventoryCodeLib> GetCodLibList(int WareHouse, int Product, string LotNumber)
        {
            return INV_InventoryCodeLibBLL.GetModelList("WareHouse=" + WareHouse.ToString() + " AND Product=" + Product.ToString() + " AND LotNumber='" + LotNumber + "'");
        }

        public static IList<INV_InventoryCodeLib> GetCodLibList(int WareHouse, int Product, string LotNumber, int State)
        {
            return INV_InventoryCodeLibBLL.GetModelList("WareHouse=" + WareHouse.ToString() + " AND Product=" + Product.ToString() + " AND LotNumber='" + LotNumber + "' AND State=" + State.ToString());
        }
        #endregion

        #region 增加或扣减库存
        public static int IncreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber, Decimal Price)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.IncreaseQuantity(WareHouse, Product, Quantity, LotNumber, Price);
        }

        public static int IncreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.IncreaseQuantity(WareHouse, Product, Quantity, LotNumber);
        }

        public static int IncreaseQuantity(int WareHouse, int Product, int Quantity)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.IncreaseQuantity(WareHouse, Product, Quantity, "");
        }

        public static int DecreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DecreaseQuantity(WareHouse, Product, Quantity, LotNumber);
        }

        public static int DecreaseQuantity(int WareHouse, int Product, int Quantity)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DecreaseQuantity(WareHouse, Product, Quantity, "");
        }
        #endregion

        #region 根据指定的物流码增减库存(零售时或零售后退货时用)
        /// <summary>
        /// 根据产品物流码增加门店库存(零售后退货时用)
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为已零售状态</returns>
        public static int IncreaseByPieceCode(string PieceCode)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.IncreaseByPieceCode(PieceCode);
        }

        /// <summary>
        /// 根据产品物流码扣减门店库存(零售时用)
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为在库状态</returns>
        public static int DecreaseByPieceCode(string PieceCode)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DecreaseByPieceCode(PieceCode);
        }

        /// <summary>
        /// 根据产品物流码扣减门店库存
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <param name="InvCodeLibState">9:零售 10:礼品兑换 11:作废 12:直销配货</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为在库状态</returns>
        public static int DecreaseByPieceCode(string PieceCode, int InvCodeLibState)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.DecreaseByPieceCode(PieceCode, InvCodeLibState);
        }
        #endregion

        #region 获取指定客户某产品的库存
        /// <summary>
        /// 获取指定客户指定批号某产品的库存数量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static int GetClientQuantityByProduct(int Client, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetQuantityByProduct(Client, Product, 0, LotNumber);
        }
        /// <summary>
        /// 获取指定客户所有批号某产品的库存数量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static int GetClientQuantityByProduct(int Client, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetQuantityByProduct(Client, Product, 0, "");
        }

        /// <summary>
        /// 获取指定仓库指定批号某产品的库存数量
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static int GetWareHouseQuantityByProduct(int WareHouse, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetQuantityByProduct(0, Product, WareHouse, LotNumber);
        }
        /// <summary>
        /// 获取指定仓库所有批号某产品的库存数量
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static int GetWareHouseQuantityByProduct(int WareHouse, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetQuantityByProduct(0, Product, WareHouse, "");
        }
        #endregion

        #region 获取指定客户某产品的价格
        /// <summary>
        /// 获取指定客户指定批号某产品的价格
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static decimal GetClientPriceByProduct(int Client, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPriceByProduct(Client, Product, 0, LotNumber);
        }
        /// <summary>
        /// 获取指定客户所有批号某产品的价格
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static decimal GetClientPriceByProduct(int Client, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPriceByProduct(Client, Product, 0, "");
        }

        /// <summary>
        /// 获取指定仓库指定批号某产品的价格
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public static decimal GetWareHousePriceByProduct(int WareHouse, int Product, string LotNumber)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPriceByProduct(0, Product, WareHouse, LotNumber);
        }
        /// <summary>
        /// 获取指定仓库所有批号某产品的价格
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static decimal GetWareHousePriceByProduct(int WareHouse, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPriceByProduct(0, Product, WareHouse, "");
        }
        #endregion

        #region 查询库存中的产品物流码
        /// <summary>
        /// 查询库存中的产品物流码
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static DataTable GetCodeLib(int WareHouse, int DisplayMode, int Product, string LotNumber, int State)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCodeLib(WareHouse, DisplayMode, Product, LotNumber, State);
        }

        /// <summary>
        /// 查询库存中的产品物流码
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <returns></returns>
        public static DataTable GetCodeLib(int WareHouse, int DisplayMode)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCodeLib(WareHouse, DisplayMode, 0, "", 0);
        }
        #endregion

        #region 查询门店进销存
        /// <summary>
        /// 查询门店进销存
        /// </summary>
        public static DataTable GetClientJXC(int Retailer)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXC(Retailer);
        }
        #endregion

        #region 统计指定范围内客户的进销存数据
        public static DataTable GetClientJXCSummary(int Month, int OrganizeCity, int ClientType)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXCSummary(Month, OrganizeCity, ClientType, 0, 0);
        }
        public static DataTable GetClientJXCSummary_BySupplier(int Month, int Supplier)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXCSummary(Month, 0, 0, Supplier, 0);
        }
        public static DataTable GetClientJXCSummary_BySupplier(int Supplier)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXCSummary(0, 0, 0, Supplier, 0);
        }
        public static DataTable GetClientJXCSummary_ByClient(int Month, int Client)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXCSummary(Month, 0, 0, 0, Client);
        }
        public static DataTable GetClientJXCSummary_ByClient(int Client)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientJXCSummary(0, 0, 0, 0, Client);
        }
        #endregion

        #region 根据物流码查询追溯
        /// <summary>
        /// 根据物流码查询追溯
        /// </summary>
        /// <param name="PieceCode"></param>
        /// <returns></returns>
        public static DataTable TraceQuery(string PieceCode)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.TraceQuery(PieceCode);
        }
        #endregion

        #region 获取客户库存明细(含有效期信息)
        /// <summary>
        /// 获取客户库存明细(含有效期信息)
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="WareHouse">仓库</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public static DataTable GetProductList(int OrganizeCity, int ClientType, int Supplier, int Client, int WareHouse, int PDTBrand, int PDTClassify, int Product)
        {
            INV_InventoryDAL dal = (INV_InventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetProductList(OrganizeCity, ClientType, Supplier, Client, WareHouse, PDTBrand, PDTClassify, Product);
        }
        #endregion

    }
}