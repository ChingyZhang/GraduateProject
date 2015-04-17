
// ===================================================================
// 文件： ORD_DeliveryDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Linq;
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
    ///ORD_DeliveryBLL业务逻辑BLL类
    /// </summary>
    public class ORD_DeliveryBLL : BaseComplexBLL<ORD_Delivery, ORD_DeliveryDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.ORD_DeliveryDAL";
        private ORD_DeliveryDAL _dal;

        /// <summary>
        /// 发货单金额
        /// </summary>
        public decimal TotalCost
        {
            get
            {
                if (Items == null) return 0;
                if (_m.State < 4)
                    return Items.Sum(p => p.DeliveryQuantity * p.Price);
                else
                    return Items.Sum(p => p.SignInQuantity * p.Price);
            }
        }
        #region 构造函数
        ///<summary>
        ///ORD_DeliveryBLL
        ///</summary>
        public ORD_DeliveryBLL()
            : base(DALClassName)
        {
            _dal = (ORD_DeliveryDAL)_DAL;
            _m = new ORD_Delivery();
        }

        public ORD_DeliveryBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_DeliveryDAL)_DAL;
            FillModel(id);
        }

        public ORD_DeliveryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_DeliveryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_Delivery> GetModelList(string condition)
        {
            return new ORD_DeliveryBLL()._GetModelList(condition);
        }
        #endregion

        #region 生成发货单号
        public static string GenerateSheetCode(int WareHouse)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(WareHouse);
        }
        #endregion

        #region 发货单操作
        /// <summary>
        /// 发货单从备单状态完成装车
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为备单状态 -2:已装车产品码明细与备单明细数量不相符 -100:更新数据库失败</returns>
        public int Loading(Guid User)
        {
            return _dal.Loading(_m.ID, User);
        }

        /// <summary>
        /// 发货单确认发车
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为装车状态 -2:发货单产品码明细中，有部分产品码在仓库不为装车状态 -100:更新数据库失败</returns>
        public int Depart(Guid User)
        {
            return _dal.Depart(_m.ID, User);
        }

        /// <summary>
        /// 确认签收整个发货单
        /// </summary>
        /// <param name="WareHouseCell"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 >0:签收产品数量</returns>
        public int SignAll(int WareHouseCell, Guid User)
        {
            return _dal.SignAll(_m.ID, WareHouseCell, User);
        }

        /// <summary>
        /// 确认签收整个发货单
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 >0:签收产品数量</returns>
        public int SignAll(Guid User)
        {
            return _dal.SignAll(_m.ID, 0, User);
        }

        /// <summary>
        /// 发货单确认退单
        /// </summary>
        /// <param name="WareHouseCell"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:发货单产品码明细中，有部分产品码不为在途状态 -100:更新数据库失败</returns>
        public int ReturnAll(int WareHouseCell, Guid User)
        {
            return _dal.ReturnAll(_m.ID, WareHouseCell, User);
        }

        /// <summary>
        /// 发货单确认退单
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:发货单产品码明细中，有部分产品码不为在途状态 -100:更新数据库失败</returns>
        public int ReturnAll(Guid User)
        {
            return _dal.ReturnAll(_m.ID, 0, User);
        }

        /// <summary>
        /// 取消发货单
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为备单或装车状态 -2:发货单产品码明细中，有部分产品码不为装车状态 -100:更新数据库失败</returns>
        public int Cancel(Guid User)
        {
            return _dal.Cancel(_m.ID, User);
        }
        #endregion

        #region 逐码扫描物流码操作
        /// <summary>
        /// 按单个物流码装车
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为可装车状态 -2:物流码无效 -3:实际装车数量超过备单数量 -4:订货单中无该产品 -5:上架目录中无该产品 -6:扣减库存失败 >0:装车产品数量</returns>
        public static int LoadingByOneCode(int DeliveryID, string Code)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.LoadingByOneCode(DeliveryID, Code);
        }

        /// <summary>
        /// 按单个物流码签收发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <param name="WareHouseCell"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:签收产品数量</returns>
        public static int SignInByOneCode(int DeliveryID, string Code, int WareHouseCell)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.SignInByOneCode(DeliveryID, Code, WareHouseCell);
        }

        /// <summary>
        /// 按单个物流码签收发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:签收产品数量</returns>
        public static int SignInByOneCode(int DeliveryID, string Code)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.SignInByOneCode(DeliveryID, Code, 0);
        }

        /// <summary>
        /// 按单个物流码退货发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <param name="WareHouseCell"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:退货产品数量</returns>
        public static int ReturnByOneCode(int DeliveryID, string Code, int WareHouseCell)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReturnByOneCode(DeliveryID, Code, WareHouseCell);
        }

        /// <summary>
        /// 按单个物流码退货发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:退货产品数量</returns>
        public static int ReturnByOneCode(int DeliveryID, string Code)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ReturnByOneCode(DeliveryID, Code, 0);
        }

        /// <summary>
        /// 按单个物流码破损
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:退货产品数量</returns>
        public static int BrokenByOneCode(int DeliveryID, string Code)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.BrokenByOneCode(DeliveryID, Code);
        }

        /// <summary>
        /// 按单个物流码丢失
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:丢失产品数量</returns>
        public static int LostByOneCode(int DeliveryID, string Code)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.LostByOneCode(DeliveryID, Code);
        }
        #endregion

        #region 直接签收到下游商户库存
        /// <summary>
        /// 根据物流码直接从指定发货商签收入库到下游商户库存
        /// </summary>
        /// <param name="SupplierWareHouse">供货商仓库</param>
        /// <param name="ClientWareHouse">收货商仓库</param>
        /// <param name="ClientWareHouseCell">签收库位</param>
        /// <param name="Codes">签收物流码</param>
        /// <param name="SignInUser">签收操作用户</param>
        /// <returns>大于0:发货单号 -10:物流码不是归属同一供货商仓库 -11:供货商与收货商间不存在供货关系	-12:无产品码符合收货规则</returns>
        public static int SignInNoDeliverySheet(int SupplierWareHouse, int ClientWareHouse, int ClientWareHouseCell, string Codes, Guid SignInUser)
        {
            if (Codes.Contains(".")) Codes = Codes.Replace(".", ",");
            if (Codes.Contains(" ")) Codes = Codes.Replace(" ", ",");
            if (Codes.Contains("|")) Codes = Codes.Replace("|", ",");
            if (Codes.Contains(";")) Codes = Codes.Replace(";", ",");

            if (string.IsNullOrEmpty(Codes)) return -12;

            string[] _codes = Codes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string _codestr = "";
            foreach (string _c in _codes)
            {
                _codestr += "'" + _c + "',";
            }

            if (_codestr.EndsWith(",")) _codestr = _codestr.Substring(0, _codestr.Length - 1);

            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.SignInNoDeliverySheet(SupplierWareHouse, ClientWareHouse, ClientWareHouseCell, _codestr, SignInUser);
        }
        #endregion

        #region 查询已装车扫描的产品码
        /// <summary>
        /// 按产品查询已装车扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeliveryCodeLibByProduct()
        {
            return _dal.GetDeliveryCodeLib(_m.ID, 1);
        }
        /// <summary>
        /// 按产品箱码查询已装车扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeliveryCodeLibByCaseCode()
        {
            return _dal.GetDeliveryCodeLib(_m.ID, 2);
        }
        /// <summary>
        /// 查询已装车扫描的产品码明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeliveryCodeLibDetail()
        {
            return _dal.GetDeliveryCodeLib(_m.ID, 3);
        }
        #endregion

        #region 获取指定客户某产品的备单状态下待发货数量
        public static int GetClientNeedDeliveryQuantityByProduct(int Supplier, int Product, int WareHouse)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetNeedDeliveryQuantityByProduct(Supplier, Product, WareHouse);
        }
        public static int GetWareHouseNeedDeliveryQuantityByProduct(int WareHouse, int Product)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetNeedDeliveryQuantityByProduct(0, Product, WareHouse);
        }
        #endregion

        #region 获取指定客户某时间段内的收货及发货明细
        /// <summary>
        /// 获取指定客户的收货明细
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public static DataTable GetDeliveryInList(int Client, DateTime BeginDate, DateTime EndDate, int DeliveryClassify, int PDTBrand, int PDTClassify, int Product)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliveryInList(Client, BeginDate, EndDate, DeliveryClassify, PDTBrand, PDTClassify, Product);
        }

        /// <summary>
        /// 获取指定客户的发货明细
        /// </summary>
        /// <param name="Supplier">发货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <param name="Client">收货客户</param>
        /// <returns></returns>
        public static DataTable GetDeliveryOutList(int Supplier, DateTime BeginDate, DateTime EndDate, int DeliveryClassify, int PDTBrand, int PDTClassify, int Product, int Client)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliveryOutList(Supplier, BeginDate, EndDate, DeliveryClassify, PDTBrand, PDTClassify, Product, Client);
        }

        /// <summary>
        /// 获取指定客户的收退货明细
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <param name="Supplier">退货客户</param>
        /// <returns></returns>
        public static DataTable GetReturnInList(int Client, DateTime BeginDate, DateTime EndDate, int PDTBrand, int PDTClassify, int Product, int Supplier)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetReturnInList(Client, BeginDate, EndDate, PDTBrand, PDTClassify, Product, Supplier);
        }

        /// <summary>
        /// 获取指定客户的发出退货明细
        /// </summary>
        /// <param name="Supplier">发出退货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public static DataTable GetReturnOutList(int Supplier, DateTime BeginDate, DateTime EndDate, int PDTBrand, int PDTClassify, int Product)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetReturnOutList(Supplier, BeginDate, EndDate, PDTBrand, PDTClassify, Product);
        }
        #endregion

        #region 获取发货出错的物流码(应发客户与实际签收客户不一致的物流码)
        public static DataTable GetDeliveryErrorCodeLibList_BySupplier(int Supplier, DateTime BeginDate, DateTime EndDate)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliveryErrorCodeLibList(Supplier, 0, BeginDate, EndDate);
        }
        public static DataTable GetDeliveryErrorCodeLibList_ByActClient(int ActClient, DateTime BeginDate, DateTime EndDate)
        {
            ORD_DeliveryDAL dal = (ORD_DeliveryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDeliveryErrorCodeLibList(0, ActClient, BeginDate, EndDate);
        }
        #endregion
    }
}