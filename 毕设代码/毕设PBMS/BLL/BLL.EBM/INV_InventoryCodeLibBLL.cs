
// ===================================================================
// 文件： INV_InventoryCodeLibDAL.cs
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
    ///INV_InventoryCodeLibBLL业务逻辑BLL类
    /// </summary>
    public class INV_InventoryCodeLibBLL : BaseSimpleBLL<INV_InventoryCodeLib>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_InventoryCodeLibDAL";
        private INV_InventoryCodeLibDAL _dal;

        #region 构造函数
        ///<summary>
        ///INV_InventoryCodeLibBLL
        ///</summary>
        public INV_InventoryCodeLibBLL()
            : base(DALClassName)
        {
            _dal = (INV_InventoryCodeLibDAL)_DAL;
            _m = new INV_InventoryCodeLib();
        }

        public INV_InventoryCodeLibBLL(long id)
            : base(DALClassName)
        {
            _dal = (INV_InventoryCodeLibDAL)_DAL;
            FillModel(id);
        }

        public INV_InventoryCodeLibBLL(long id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_InventoryCodeLibDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<INV_InventoryCodeLib> GetModelList(string condition)
        {
            return new INV_InventoryCodeLibBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 按物流码设置库存状态
        /// </summary>
        /// <param name="PieceCode"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static int SetState(string PieceCode, int State)
        {
            INV_InventoryCodeLibDAL dal = (INV_InventoryCodeLibDAL)DataAccess.CreateObject(DALClassName);
            return dal.SetState(PieceCode, State);
        }
    }
}