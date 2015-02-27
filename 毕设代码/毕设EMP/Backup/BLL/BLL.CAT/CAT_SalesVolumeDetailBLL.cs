
// ===================================================================
// 文件： CAT_SalesVolumeDetailDAL.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CAT;
using MCSFramework.SQLDAL.CAT;

namespace MCSFramework.BLL.CAT
{
	/// <summary>
	///CAT_SalesVolumeDetailBLL业务逻辑BLL类
	/// </summary>
	public class CAT_SalesVolumeDetailBLL : BaseSimpleBLL<CAT_SalesVolumeDetail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_SalesVolumeDetailDAL";
        private CAT_SalesVolumeDetailDAL _dal;
		
		#region 构造函数
		///<summary>
		///CAT_SalesVolumeDetailBLL
		///</summary>
		public CAT_SalesVolumeDetailBLL()
			: base(DALClassName)
		{
			_dal = (CAT_SalesVolumeDetailDAL)_DAL;
            _m = new CAT_SalesVolumeDetail(); 
		}
		
		public CAT_SalesVolumeDetailBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_SalesVolumeDetailDAL)_DAL;
            FillModel(id);
        }

        public CAT_SalesVolumeDetailBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_SalesVolumeDetailDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CAT_SalesVolumeDetail> GetModelList(string condition)
        {
            return new CAT_SalesVolumeDetailBLL()._GetModelList(condition);
        }
		#endregion
	}
}