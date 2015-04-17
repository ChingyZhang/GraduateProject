
// ===================================================================
// 文件： INV_PutInStoreageDAL.cs
// 项目名称：
// 创建时间：2012-7-23
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
    /// <summary>
    ///INV_PutInStoreageBLL业务逻辑BLL类
    /// </summary>
    public class INV_PutInStoreageBLL : BaseComplexBLL<INV_PutInStoreage, INV_PutInStoreageDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_PutInStoreageDAL";
        private INV_PutInStoreageDAL _dal;

        /// <summary>
        /// 合计总金额
        /// </summary>
        public decimal TotalCost
        {
            get
            {
                if (Items != null)
                    return Items.Sum(p => p.Quantity * p.Price);
                else
                    return 0;
            }
        }
        #region 构造函数
        ///<summary>
        ///INV_PutInStoreageBLL
        ///</summary>
        public INV_PutInStoreageBLL()
            : base(DALClassName)
        {
            _dal = (INV_PutInStoreageDAL)_DAL;
            _m = new INV_PutInStoreage();
        }

        public INV_PutInStoreageBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_PutInStoreageDAL)_DAL;
            FillModel(id);
        }

        public INV_PutInStoreageBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_PutInStoreageDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<INV_PutInStoreage> GetModelList(string condition)
        {
            return new INV_PutInStoreageBLL()._GetModelList(condition);
        }
        #endregion

        #region 生成发货单号
        public static string GenerateSheetCode(int WareHouse)
        {
            INV_PutInStoreageDAL dal = (INV_PutInStoreageDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(WareHouse);
        }
        #endregion

        #region 确认入库 与 撤销入库
        /// <summary>
        /// 确认入库
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:指定的ID号不存在或不为备单状态 -2:无待入库的产品扫描码 -3:备单明细与扫描产品码数量不符 -4:扫码待入库的产品码中有部分已在仓库产品码库中</returns>
        public int ConfirmPutIn(Guid User)
        {
            return _dal.ConfirmPutIn(_m.ID, User);
        }

        /// <summary>
        /// 撤销入库
        /// </summary>
        /// <param name="User"></param>
        /// <returns>-1:撤销入库单ID不存在，或不为已入库状态 -2:入库单产品码库中不存在已入库的产品	-3:已入库的产品码中，有部分已不在本仓库，或不为在库状态</returns>
        public int UndoPutIn(Guid User)
        {
            return _dal.UndoPutIn(_m.ID, User);
        }
        #endregion

        #region 逐码扫描产品
        /// <summary>
        /// 逐码扫描产品(指定产品及批号、入库价格)
        /// </summary>
        /// <param name="PutInID"></param>
        /// <param name="Code"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns>-1:入库单不为备单状态 -2:该物料码已在入库 -3:扫描的产品码无效 -4:未指定入库产品 -5:整箱扫描时，箱码包含的产品数量不等于产品包装系数</returns>
        public static int PutInByOneCode(int PutInID, string Code, int Product, string LotNumber)
        {
            INV_PutInStoreageDAL dal = (INV_PutInStoreageDAL)DataAccess.CreateObject(DALClassName);
            return dal.PutInByOneCode(PutInID, Code, Product, LotNumber);
        }

        /// <summary>
        /// 逐码扫描产品(指定入库价格)
        /// </summary>
        /// <param name="PutInID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:入库单不为备单状态 -2:该物料码已在入库 -3:扫描的产品码无效 -4:未指定入库产品 -5:整箱扫描时，箱码包含的产品数量不等于产品包装系数</returns>
        public static int PutInByOneCode(int PutInID, string Code)
        {
            INV_PutInStoreageDAL dal = (INV_PutInStoreageDAL)DataAccess.CreateObject(DALClassName);
            return dal.PutInByOneCode(PutInID, Code, 0, "");
        }
        #endregion

        #region 查询已入库扫描的产品码
        /// <summary>
        /// 按产品查询已入库扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetPutInCodeLibByProduct()
        {
            return _dal.GetPutInCodeLib(_m.ID, 1);
        }
        /// <summary>
        /// 按产品箱码查询已入库扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetPutInCodeLibByCaseCode()
        {
            return _dal.GetPutInCodeLib(_m.ID, 2);
        }
        /// <summary>
        /// 查询已入库扫描的产品码明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetPutInCodeLibDetail()
        {
            return _dal.GetPutInCodeLib(_m.ID, 3);
        }
        #endregion

        #region 退库操作

        #region 确认退库
        /// <summary>
        /// 确认退库
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="User"></param>
        /// <returns>-1:指定的ID号不存在或不为备单状态 -2:无待退库的产品扫描码 -3:备单明细与扫描产品码数量不符 -4:扫码待退库的产品码中，有部分已被积分</returns>
        public int ConfirmPutOut(Guid User)
        {
            return _dal.ConfirmPutOut(_m.ID, User);
        }
        #endregion

        #region 逐码扫描产品
        /// <summary>
        /// -1:退库单不为备单状态 -2:该物料码已在本退库单 -3:扫描的产品码无效
        /// </summary>
        /// <param name="PutInID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static int PutOutByOneCode(int PutInID, string Code)
        {
            INV_PutInStoreageDAL dal = (INV_PutInStoreageDAL)DataAccess.CreateObject(DALClassName);
            return dal.PutOutByOneCode(PutInID, Code);
        }
        #endregion

        #endregion
    }
}