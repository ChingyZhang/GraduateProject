
// ===================================================================
// 文件： JN_JournalCommentDAL.cs
// 项目名称：
// 创建时间：2009-4-25
// 作者:	   shh
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
	///JN_JournalCommentBLL业务逻辑BLL类
	/// </summary>
	public class JN_JournalCommentBLL : BaseSimpleBLL<JN_JournalComment>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.JN_JournalCommentDAL";
        private JN_JournalCommentDAL _dal;
		
		#region 构造函数
		///<summary>
		///JN_JournalCommentBLL
		///</summary>
		public JN_JournalCommentBLL()
			: base(DALClassName)
		{
			_dal = (JN_JournalCommentDAL)_DAL;
            _m = new JN_JournalComment(); 
		}
		
		public JN_JournalCommentBLL(int id)
            : base(DALClassName)
        {
            _dal = (JN_JournalCommentDAL)_DAL;
            FillModel(id);
        }

        public JN_JournalCommentBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (JN_JournalCommentDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<JN_JournalComment> GetModelList(string condition)
        {
            return new JN_JournalCommentBLL()._GetModelList(condition);
        }
		#endregion
	}
}