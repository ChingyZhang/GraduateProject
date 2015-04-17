
// ===================================================================
// 文件： BBS_BoardDAL.cs
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
	///BBS_BoardBLL业务逻辑BLL类
	/// </summary>
	public class BBS_BoardBLL : BaseSimpleBLL<BBS_Board>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_BoardDAL";
        private BBS_BoardDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_BoardBLL
		///</summary>
		public BBS_BoardBLL()
			: base(DALClassName)
		{
			_dal = (BBS_BoardDAL)_DAL;
            _m = new BBS_Board(); 
		}
		
		public BBS_BoardBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_BoardDAL)_DAL;
            FillModel(id);
        }

        public BBS_BoardBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_BoardDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_Board> GetModelList(string condition)
        {
            return new BBS_BoardBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetAllBoard(string condition)
        {
            return _dal.GetAllBoard(condition);
        }

        #region 获取所属板块的未点击论坛贴子数
        public int GetHitCount()
        {
            int count=0;
            IList<BBS_ForumItem>  forumItemList = BBS_ForumItemBLL.GetModelList(" Board=" + _m.ID);
            foreach (BBS_ForumItem forumItem in forumItemList)
            {
                if (forumItem.HitTimes == 0)
                    count++;
            }
            return count;
        }
        #endregion

        #region 获取所属板块的所有回复
        public IList<BBS_ForumItem> GetForumItem()
        {
            return BBS_ForumItemBLL.GetModelList(" Board=" + _m.ID + " order by SendTime desc");      
        }

        #endregion

        #region 获取所属板块的所有回复
        public int GetReplyCount()
        {
            int count = 0;
            IList<BBS_ForumItem> forumItemList = BBS_ForumItemBLL.GetModelList(" Board=" + _m.ID);
            foreach (BBS_ForumItem forumItem in forumItemList)
            {
                IList<BBS_ForumReply> replyList = BBS_ForumReplyBLL.GetModelList(" Item=" + forumItem.ID);
                count += replyList.Count;  
            }
            return count;
        }
      
        #endregion

        public int DeleteBoard(int id)
        {
            return _dal.DeleteBoard(id);
        }
        public DataTable GetIndexInfo()
        {
            return _dal.GetIndexInfo();
        }
	}
}