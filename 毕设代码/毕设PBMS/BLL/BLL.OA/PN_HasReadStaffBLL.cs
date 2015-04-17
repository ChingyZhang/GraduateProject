
// ===================================================================
// 文件： PN_HasReadStaffDAL.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
	/// <summary>
	///PN_HasReadStaffBLL业务逻辑BLL类
	/// </summary>
	public class PN_HasReadStaffBLL : BaseSimpleBLL<PN_HasReadStaff>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_HasReadStaffDAL";
        private PN_HasReadStaffDAL _dal;
		
		#region 构造函数
		///<summary>
		///PN_HasReadStaffBLL
		///</summary>
		public PN_HasReadStaffBLL()
			: base(DALClassName)
		{
			_dal = (PN_HasReadStaffDAL)_DAL;
            _m = new PN_HasReadStaff(); 
		}
		
		public PN_HasReadStaffBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_HasReadStaffDAL)_DAL;
            FillModel(id);
        }

        public PN_HasReadStaffBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_HasReadStaffDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PN_HasReadStaff> GetModelList(string condition)
        {
            return new PN_HasReadStaffBLL()._GetModelList(condition);
        }
		#endregion

        #region 根据邮件ID获得阅读人数
        public int  GetReadCountByNotice(int notice)
        {
            return _dal.GetReadCountByNotice(notice);
        }
        #endregion
	}
}