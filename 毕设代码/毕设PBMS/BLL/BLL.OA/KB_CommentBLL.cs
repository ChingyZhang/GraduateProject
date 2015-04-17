
// ===================================================================
// 文件： KB_CommentDAL.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   WJX
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
	///KB_CommentBLL业务逻辑BLL类
	/// </summary>
	public class KB_CommentBLL : BaseSimpleBLL<KB_Comment>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.KB_CommentDAL";
        private KB_CommentDAL _dal;
		
		#region 构造函数
		///<summary>
		///KB_CommentBLL
		///</summary>
		public KB_CommentBLL()
			: base(DALClassName)
		{
			_dal = (KB_CommentDAL)_DAL;
            _m = new KB_Comment(); 
		}
		
		public KB_CommentBLL(int id)
            : base(DALClassName)
        {
            _dal = (KB_CommentDAL)_DAL;
            FillModel(id);
        }

        public KB_CommentBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (KB_CommentDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<KB_Comment> GetModelList(string condition)
        {
            return new KB_CommentBLL()._GetModelList(condition);
        }
		#endregion

        public void DeleteByID(int id)
        {
            _dal.DeleteByID(id);
        }
	}
}