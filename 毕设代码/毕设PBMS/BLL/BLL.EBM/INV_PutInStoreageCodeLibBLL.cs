
// ===================================================================
// 文件： INV_PutInStoreageCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-23
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
	///INV_PutInStoreageCodeLibBLL业务逻辑BLL类
	/// </summary>
	public class INV_PutInStoreageCodeLibBLL : BaseSimpleBLL<INV_PutInStoreageCodeLib>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.EBM.INV_PutInStoreageCodeLibDAL";
        private INV_PutInStoreageCodeLibDAL _dal;
		
		#region 构造函数
		///<summary>
		///INV_PutInStoreageCodeLibBLL
		///</summary>
		public INV_PutInStoreageCodeLibBLL()
			: base(DALClassName)
		{
			_dal = (INV_PutInStoreageCodeLibDAL)_DAL;
            _m = new INV_PutInStoreageCodeLib(); 
		}
		
		public INV_PutInStoreageCodeLibBLL(int id)
            : base(DALClassName)
        {
            _dal = (INV_PutInStoreageCodeLibDAL)_DAL;
            FillModel(id);
        }

        public INV_PutInStoreageCodeLibBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (INV_PutInStoreageCodeLibDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<INV_PutInStoreageCodeLib> GetModelList(string condition)
        {
            return new INV_PutInStoreageCodeLibBLL()._GetModelList(condition);
        }
		#endregion
	}
}