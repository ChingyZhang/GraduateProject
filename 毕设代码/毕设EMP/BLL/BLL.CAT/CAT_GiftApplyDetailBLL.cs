
// ===================================================================
// 文件： CAT_GiftApplyDetailDAL.cs
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
	///CAT_GiftApplyDetailBLL业务逻辑BLL类
	/// </summary>
	public class CAT_GiftApplyDetailBLL : BaseSimpleBLL<CAT_GiftApplyDetail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_GiftApplyDetailDAL";
        private CAT_GiftApplyDetailDAL _dal;
		
		#region 构造函数
		///<summary>
		///CAT_GiftApplyDetailBLL
		///</summary>
		public CAT_GiftApplyDetailBLL()
			: base(DALClassName)
		{
			_dal = (CAT_GiftApplyDetailDAL)_DAL;
            _m = new CAT_GiftApplyDetail(); 
		}
		
		public CAT_GiftApplyDetailBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_GiftApplyDetailDAL)_DAL;
            FillModel(id);
        }

        public CAT_GiftApplyDetailBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_GiftApplyDetailDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CAT_GiftApplyDetail> GetModelList(string condition)
        {
            return new CAT_GiftApplyDetailBLL()._GetModelList(condition);
        }
		#endregion
	}
}