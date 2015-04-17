
// ===================================================================
// 文件： PN_NoticeDAL.cs
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
using MCSFramework.Model;

namespace MCSFramework.BLL.OA
{
    /// <summary>
    ///PN_NoticeBLL业务逻辑BLL类
    /// </summary>
    public class PN_NoticeBLL : BaseSimpleBLL<PN_Notice>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.OA.PN_NoticeDAL";
        private PN_NoticeDAL _dal;

        #region 构造函数
        ///<summary>
        ///PN_NoticeBLL
        ///</summary>
        public PN_NoticeBLL()
            : base(DALClassName)
        {
            _dal = (PN_NoticeDAL)_DAL;
            _m = new PN_Notice();
        }

        public PN_NoticeBLL(int id)
            : base(DALClassName)
        {
            _dal = (PN_NoticeDAL)_DAL;
            FillModel(id);
        }

        public PN_NoticeBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PN_NoticeDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PN_Notice> GetModelList(string condition)
        {
            return new PN_NoticeBLL()._GetModelList(condition);
        }
        #endregion

        #region  通过ID删除公告
        public int SetIsDeleted()
        {
            return _dal.IsDeleteByID(_m.ID);
        }
        #endregion

        /// <summary>
        /// 设置公告审核标志
        /// </summary>
        /// <param name="ApproveFlag"></param>
        /// <param name="Staff"></param>
        public void Approve(int ApproveFlag, int Staff)
        {
            _dal.Approve(_m.ID, ApproveFlag, Staff);
        }

        public static IList<PN_Notice> GetNoticeByStaff(int Staff)
        {
            return GetNoticeByStaff(Staff, DateTime.Today.AddMonths(-1), DateTime.Now, 1);
        }

        public static IList<PN_Notice> GetNoticeByStaff(int Staff, DateTime BeginDate, DateTime EndDate)
        {
            return GetNoticeByStaff(Staff, BeginDate, EndDate, 1);
        }

        public static IList<PN_Notice> GetNoticeByStaff(int Staff, DateTime BeginDate, DateTime EndDate, int ApproveFlag)
        {
            PN_NoticeDAL dal = (PN_NoticeDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetNoticeByStaff(Staff, BeginDate, EndDate, ApproveFlag);
        }
    }
}