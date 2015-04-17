
// ===================================================================
// 文件： INV_CheckInventoryDAL.cs
// 项目名称：
// 创建时间：2012-8-8
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
	///INV_CheckInventoryBLL业务逻辑BLL类
	/// </summary>
	public class INV_CheckInventoryBLL : BaseComplexBLL<INV_CheckInventory,INV_CheckInventoryDetail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_CheckInventoryDAL";
        private INV_CheckInventoryDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_CheckInventoryBLL
		///</summary>
		public INV_CheckInventoryBLL()
			: base(DALClassName)
		{
			_dal = (INV_CheckInventoryDAL)_DAL;
            _m = new INV_CheckInventory(); 
		}
		
		public INV_CheckInventoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_CheckInventoryDAL)_DAL;
            FillModel(id);
        }

        public INV_CheckInventoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_CheckInventoryDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_CheckInventory> GetModelList(string condition)
        {
            return new INV_CheckInventoryBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 盘点初始化
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static int CheckInit(int WareHouse, Guid User)
        {
            INV_CheckInventoryDAL dal = (INV_CheckInventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckInit(WareHouse, User);
        }

        public static int CheckByOneCode(int CheckID,string Code)
        {
            INV_CheckInventoryDAL dal = (INV_CheckInventoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckByOneCode(CheckID, Code);
        }

        /// <summary>
        /// 确认仓库盘点
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Confirm(Guid User)
        {
            return _dal.Confirm(_m.ID, User);
        }

        /// <summary>
        /// 取消盘点
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int Cancel(Guid User)
        {
            return _dal.Cancel(_m.ID, User);
        }

        /// <summary>
        /// 撤销盘点
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public int UnConfirm(Guid User)
        {
            return _dal.UnConfirm(_m.ID, User);
        }
        #region 查询已盘点扫描的产品码
        /// <summary>
        /// 按产品查询已入库扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetCheckCodeLibByProduct()
        {
            return _dal.GetCheckCodeLib(_m.ID, 1);
        }
        /// <summary>
        /// 按产品箱码查询已入库扫描的产品码
        /// </summary>
        /// <returns></returns>
        public DataTable GetCheckCodeLibByCaseCode()
        {
            return _dal.GetCheckCodeLib(_m.ID, 2);
        }
        /// <summary>
        /// 查询已入库扫描的产品码明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetCheckCodeLibDetail()
        {
            return _dal.GetCheckCodeLib(_m.ID, 3);
        }
        #endregion
	}
}