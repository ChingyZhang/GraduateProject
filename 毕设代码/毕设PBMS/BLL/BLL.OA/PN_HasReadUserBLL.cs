
// ===================================================================
// 文件： PN_HasReadUserDAL.cs
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
	///PN_HasReadUserBLL业务逻辑BLL类
	/// </summary>
	public class PN_HasReadUserBLL : BaseSimpleBLL<PN_HasReadUser>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_HasReadUserDAL";
        private PN_HasReadUserDAL _dal;
		
		#region 构造函数
		///<summary>
		///PN_HasReadUserBLL
		///</summary>
		public PN_HasReadUserBLL()
			: base(DALClassName)
		{
			_dal = (PN_HasReadUserDAL)_DAL;
            _m = new PN_HasReadUser(); 
		}
		
		public PN_HasReadUserBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_HasReadUserDAL)_DAL;
            FillModel(id);
        }

        public PN_HasReadUserBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_HasReadUserDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PN_HasReadUser> GetModelList(string condition)
        {
            return new PN_HasReadUserBLL()._GetModelList(condition);
        }
		#endregion

        #region 根据公告ID获得阅读人数
        public int  GetReadCountByNotice(int notice)
        {
            return _dal.GetReadCountByNotice(notice);
        }
        #endregion

        /// <summary>
        /// 设置公告已读记录
        /// </summary>
        /// <param name="Notice"></param>
        /// <param name="UserName"></param>
        /// <param name="ReadInfo"></param>
        /// <returns></returns>
        public static int SetRead(int Notice, string UserName, string ReadInfo)
        {
            PN_HasReadUserBLL bll = new PN_HasReadUserBLL();
            bll.Model.Notice = Notice;
            bll.Model.Username = UserName;
            bll.Model.ReadTime = DateTime.Now;
            bll.Model.ReadInfo = ReadInfo;
            int ret = bll.Add();
            return ret >= 0 ? 0 : ret;
        }

        /// <summary>
        /// 判断公告是否已读
        /// </summary>
        /// <param name="Notice"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool IsRead(int Notice, string UserName)
        {
            if (PN_HasReadUserBLL.GetModelList("Notice=" + Notice.ToString() + " AND Username='" + UserName + "'").Count > 0)
                return true;
            else
                return false;
        }
	}
}