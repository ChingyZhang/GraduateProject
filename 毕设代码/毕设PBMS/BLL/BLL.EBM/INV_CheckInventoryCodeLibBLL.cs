
// ===================================================================
// 文件： INV_CheckInventoryCodeLibDAL.cs
// 项目名称：
// 创建时间：2014-07-27
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
	///INV_CheckInventoryCodeLibBLL业务逻辑BLL类
	/// </summary>
	public class INV_CheckInventoryCodeLibBLL : BaseSimpleBLL<INV_CheckInventoryCodeLib>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_CheckInventoryCodeLibDAL";
        private INV_CheckInventoryCodeLibDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_CheckInventoryCodeLibBLL
		///</summary>
		public INV_CheckInventoryCodeLibBLL()
			: base(DALClassName)
		{
			_dal = (INV_CheckInventoryCodeLibDAL)_DAL;
            _m = new INV_CheckInventoryCodeLib(); 
		}
		
		public INV_CheckInventoryCodeLibBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_CheckInventoryCodeLibDAL)_DAL;
            FillModel(id);
        }

        public INV_CheckInventoryCodeLibBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_CheckInventoryCodeLibDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_CheckInventoryCodeLib> GetModelList(string condition)
        {
            return new INV_CheckInventoryCodeLibBLL()._GetModelList(condition);
        }
		#endregion
	}
}