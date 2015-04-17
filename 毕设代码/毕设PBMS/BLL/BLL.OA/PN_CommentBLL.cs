
// ===================================================================
// 文件： PN_CommentDAL.cs
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
	///PN_CommentBLL业务逻辑BLL类
	/// </summary>
	public class PN_CommentBLL : BaseSimpleBLL<PN_Comment>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_CommentDAL";
        private PN_CommentDAL _dal;
		
		#region 构造函数
		///<summary>
		///PN_CommentBLL
		///</summary>
		public PN_CommentBLL()
			: base(DALClassName)
		{
			_dal = (PN_CommentDAL)_DAL;
            _m = new PN_Comment(); 
		}
		
		public PN_CommentBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_CommentDAL)_DAL;
            FillModel(id);
        }

        public PN_CommentBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_CommentDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PN_Comment> GetModelList(string condition)
        {
            return new PN_CommentBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetUserList(int notice)
        {
            return _dal.GetUsertbList(notice);
        }

        #region 根据邮件ID获得评论人数
        public int GetCommentCountByNotice(int notice)
        {
            return _dal.GetCommentCountByNotice(notice);
        }
        #endregion
    }
}