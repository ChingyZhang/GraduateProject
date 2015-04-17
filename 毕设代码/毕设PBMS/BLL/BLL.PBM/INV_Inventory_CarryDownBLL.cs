
// ===================================================================
// 文件： INV_Inventory_CarryDownDAL.cs
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
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
	/// <summary>
	///INV_Inventory_CarryDownBLL业务逻辑BLL类
	/// </summary>
	public class INV_Inventory_CarryDownBLL : BaseSimpleBLL<INV_Inventory_CarryDown>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.INV_Inventory_CarryDownDAL";
        private INV_Inventory_CarryDownDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_Inventory_CarryDownBLL
		///</summary>
		public INV_Inventory_CarryDownBLL()
			: base(DALClassName)
		{
			_dal = (INV_Inventory_CarryDownDAL)_DAL;
            _m = new INV_Inventory_CarryDown(); 
		}
		
		public INV_Inventory_CarryDownBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_Inventory_CarryDownDAL)_DAL;
            FillModel(id);
        }

        public INV_Inventory_CarryDownBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_Inventory_CarryDownDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_Inventory_CarryDown> GetModelList(string condition)
        {
            return new INV_Inventory_CarryDownBLL()._GetModelList(condition);
        }
		#endregion
	}
}