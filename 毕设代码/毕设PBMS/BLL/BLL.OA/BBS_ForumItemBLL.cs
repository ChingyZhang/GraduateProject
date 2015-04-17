
// ===================================================================
// 文件： BBS_ForumItemDAL.cs
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
	///BBS_ForumItemBLL业务逻辑BLL类
	/// </summary>
	public class BBS_ForumItemBLL : BaseSimpleBLL<BBS_ForumItem>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_ForumItemDAL";
        private BBS_ForumItemDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_ForumItemBLL
		///</summary>
		public BBS_ForumItemBLL()
			: base(DALClassName)
		{
			_dal = (BBS_ForumItemDAL)_DAL;
            _m = new BBS_ForumItem(); 
		}
		
		public BBS_ForumItemBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_ForumItemDAL)_DAL;
            FillModel(id);
        }

        public BBS_ForumItemBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_ForumItemDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_ForumItem> GetModelList(string condition)
        {
            return new BBS_ForumItemBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetByCondition(string condition)
        {
            return _dal.GetByCondition(condition);
        }

        public int UpdateHitTimes(int id)
        {
            return _dal.UpdateHitTimes(id);
        }

        public int UpdateAddReplyTimes(int id)
        {
            return _dal.UpdateAddReplyTimes(id);
        }

        public int UpdateRemoveReplyTimes(int id)
        {
            return _dal.UpdateRemoveReplyTimes(id);
        }
        public int DeleteForumItem(int id)
        {
            return _dal.DeleteForumItem(id);
        }
        public int GetTopDayForumItemCount()
        {
            return _dal.GetTopDayForumItemCount();
        }
        public static DataTable GetTopLastList(string Num, string condition)
        {
            BBS_ForumItemDAL dal = (BBS_ForumItemDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetTopLastList(Num, condition));
        }
	}
}