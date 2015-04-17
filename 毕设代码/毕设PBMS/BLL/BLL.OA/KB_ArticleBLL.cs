
// ===================================================================
// 文件： KB_ArticleDAL.cs
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
	///KB_ArticleBLL业务逻辑BLL类
	/// </summary>
	public class KB_ArticleBLL : BaseSimpleBLL<KB_Article>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.KB_ArticleDAL";
        private KB_ArticleDAL _dal;
		
		#region 构造函数
		///<summary>
		///KB_ArticleBLL
		///</summary>
		public KB_ArticleBLL()
			: base(DALClassName)
		{
			_dal = (KB_ArticleDAL)_DAL;
            _m = new KB_Article(); 
		}
		
		public KB_ArticleBLL(int id)
            : base(DALClassName)
        {
            _dal = (KB_ArticleDAL)_DAL;
            FillModel(id);
        }

        public KB_ArticleBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (KB_ArticleDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<KB_Article> GetModelList(string condition)
        {
            return new KB_ArticleBLL()._GetModelList(condition);
        }
		#endregion

        public void DeleteByID(int id)
        {
            _dal.DeleteByID(id);
        }

        public void UpdateReadCount(int id)
        {
            _dal.UpdateReadcount(id);
        }

        public void UpdateApprov(int id, int approvestaff, string ideas)
        {
            _dal.UpdateApprov(id,approvestaff,ideas);
        }
	}
}