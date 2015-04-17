
// ===================================================================
// 文件： PN_ToPositionDAL.cs
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
	///PN_ToPositionBLL业务逻辑BLL类
	/// </summary>
	public class PN_ToPositionBLL : BaseSimpleBLL<PN_ToPosition>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_ToPositionDAL";
        private PN_ToPositionDAL _dal;
		
		#region 构造函数
		///<summary>
		///PN_ToPositionBLL
		///</summary>
		public PN_ToPositionBLL()
			: base(DALClassName)
		{
			_dal = (PN_ToPositionDAL)_DAL;
            _m = new PN_ToPosition(); 
		}
		
		public PN_ToPositionBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_ToPositionDAL)_DAL;
            FillModel(id);
        }

        public PN_ToPositionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_ToPositionDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PN_ToPosition> GetModelList(string condition)
        {
            return new PN_ToPositionBLL()._GetModelList(condition);
        }
		#endregion

        #region 根据公告ID获取职位
        public static List<int> GetPositionByNoticeID(int noticeID)
        {
            PN_ToPositionDAL dal = (PN_ToPositionDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPositionByNoticeID(noticeID);
        }
        #endregion

        #region 根据公告ID删除所有职位
        public static int DeleteByNoticeID(int noticeID)
        {
            PN_ToPositionDAL dal = (PN_ToPositionDAL)DataAccess.CreateObject(DALClassName);
            return dal.DeleteByNoticeID(noticeID);
        }
        #endregion

         #region 根据公告ID删除一个职位
        public static int DeletePosition(int noticeID, int position)
        {
            PN_ToPositionDAL dal = (PN_ToPositionDAL)DataAccess.CreateObject(DALClassName);
            return dal.DeletePosition(noticeID, position);
        }
         #endregion
    }
}