
// ===================================================================
// 文件： PN_ToOrganizeCityDAL.cs
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
	///PN_ToOrganizeCityBLL业务逻辑BLL类
	/// </summary>
	public class PN_ToOrganizeCityBLL : BaseSimpleBLL<PN_ToOrganizeCity>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_ToOrganizeCityDAL";
        private PN_ToOrganizeCityDAL _dal;
		
		#region 构造函数
		///<summary>
		///PN_ToOrganizeCityBLL
		///</summary>
		public PN_ToOrganizeCityBLL()
			: base(DALClassName)
		{
			_dal = (PN_ToOrganizeCityDAL)_DAL;
            _m = new PN_ToOrganizeCity(); 
		}
		
		public PN_ToOrganizeCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_ToOrganizeCityDAL)_DAL;
            FillModel(id);
        }

        public PN_ToOrganizeCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_ToOrganizeCityDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PN_ToOrganizeCity> GetModelList(string condition)
        {
            return new PN_ToOrganizeCityBLL()._GetModelList(condition);
        }
		#endregion

        #region 根据公告ID获取片区
        public static List<int> GetOrganizeCityByNoticeID(int noticeID)
        {
            PN_ToOrganizeCityDAL dal = (PN_ToOrganizeCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetOrganizeCityByNoticeID(noticeID);
        }
        #endregion

        #region 根据公告ID删除所有片区
        public static int DeleteByNoticeID(int noticeID)
        {
            PN_ToOrganizeCityDAL dal = (PN_ToOrganizeCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.DeleteByNoticeID(noticeID);
        }
        #endregion

        #region 根据公告ID删除一个片区
        public static int DeleteOrganizeCity(int noticeID, int organizeCity)
        {
            PN_ToOrganizeCityDAL dal = (PN_ToOrganizeCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.DeleteOrganizeCity(noticeID, organizeCity);
        }
        #endregion
	}
}