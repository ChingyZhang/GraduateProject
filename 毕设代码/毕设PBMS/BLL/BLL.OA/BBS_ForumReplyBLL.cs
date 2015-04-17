
// ===================================================================
// 文件： BBS_ForumReplyDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
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
	///BBS_ForumReplyBLL业务逻辑BLL类
	/// </summary>
	public class BBS_ForumReplyBLL : BaseSimpleBLL<BBS_ForumReply>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_ForumReplyDAL";
        private BBS_ForumReplyDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_ForumReplyBLL
		///</summary>
		public BBS_ForumReplyBLL()
			: base(DALClassName)
		{
			_dal = (BBS_ForumReplyDAL)_DAL;
            _m = new BBS_ForumReply(); 
		}
		
		public BBS_ForumReplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_ForumReplyDAL)_DAL;
            FillModel(id);
        }

        public BBS_ForumReplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_ForumReplyDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_ForumReply> GetModelList(string condition)
        {
            return new BBS_ForumReplyBLL()._GetModelList(condition);
        }

        public static DataTable GetTopReplyLatest(int Num)
        {
            BBS_ForumReplyDAL dal = (BBS_ForumReplyDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetTopReplyLatest(Num));
        }
		#endregion
	}
}