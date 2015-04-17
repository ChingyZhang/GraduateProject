using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using Newtonsoft.Json;
using System.Data;
using MCSFramework.WSI.Model;
using MCSFramework.BLL.Pub;
using System.Net;

namespace MCSFramework.WSI
{
    public class NoticeService
    {
        public NoticeService()
        {
            LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
        }

        
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 通知公告
        /// <summary>
        /// 获取通知列表
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static List<Notice> GetMyNoticeList(UserInfo User)
        {
            LogWriter.WriteLog("NoticeService.GetMyNoticeList:UserName=" + User.UserName);
            
            IList<PN_Notice> notices = null;
            if (User.AccountType == 1)  //员工
            {
                notices = PN_NoticeBLL.GetNoticeByStaff(User.StaffID);
            }
            else
            {
                string condition = "PN_Notice.ToAllStaff='Y' AND IsDelete ='N' AND PN_Notice.ApproveFlag=1";
                
                condition += " AND (PN_Notice.ToAllOrganizeCity='Y' OR PN_Notice.ID IN (SELECT NoticeID FROM PN_ToOrganizeCity WHERE OrganizeCity=" + User.OrganizeCity.ToString() + "))";
                notices = PN_NoticeBLL.GetModelList(condition);
            }

            if (notices == null) return null;

            List<Notice> lists = new List<Notice>(notices.Count);
            foreach (PN_Notice item in notices.OrderByDescending(p => p.InsertTime))
            {
                Notice n = new Notice(item);
                n.HasRead = PN_HasReadUserBLL.IsRead(n.ID, User.UserName);
                lists.Add(n);
            }

            return lists;
        }

        /// <summary>
        /// 根据ID号查询通知内容
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="NoticeID"></param>
        /// <returns></returns>
        public static Notice GetNoticeByNoticeID(UserInfo User, int NoticeID)
        {
            LogWriter.WriteLog("RMIFService.GetNoticeByNoticeID:UserName=" + User.UserName + ",NoticeID=" + NoticeID.ToString());

            return new Notice(NoticeID);
        }

        /// <summary>
        /// 设置通知已读标志
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="NoticeID"></param>
        /// <returns></returns>
        public static int SetHasRead(UserInfo User, int NoticeID)
        {
            LogWriter.WriteLog("NoticeService.SetHasRead:UserName=" + User.UserName + ",NoticeID=" + NoticeID.ToString());
            PN_HasReadUserBLL.SetRead(NoticeID, User.UserName, User.DeviceCode);
            return 0;
        }

        /// <summary>
        /// 追加评论
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="NoticeID"></param>
        /// <param name="CommentContent"></param>
        /// <returns></returns>
        public static int AddComment(UserInfo User, int NoticeID, string CommentContent)
        {
            LogWriter.WriteLog("NoticeService.AddComment:UserName=" + User.UserName + ",NoticeID=" + NoticeID.ToString() + ",CommentContent=" + CommentContent);

            if (User.AccountType == 1)
            {
                PN_CommentBLL commentbll = new PN_CommentBLL();
                commentbll.Model.Notice = NoticeID;
                commentbll.Model.Staff = User.StaffID;
                commentbll.Model.Content = CommentContent + "[" + User.UserName + "]";
                commentbll.Model.CommentTime = DateTime.Now;
                commentbll.Add();
            }
            return 0;
        }

        /// <summary>
        /// 获取未读公告数量
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static int GetMyNewNoticeCount(UserInfo User)
        {
            LogWriter.WriteLog("NoticeService.GetMyNewNoticeCount:UserName=" + User.UserName);

            List<Notice> notices = GetMyNoticeList(User);
            return notices.Where(p => p.HasRead == false).Count();
        }
        #endregion

        #region 消息管理
        /// <summary>
        /// 查询我的消息（仅1个月以内的消息）
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static List<Message> GetMyMessageList(UserInfo User)
        {
            LogWriter.WriteLog("NoticeService.GetMyMessageList:UserName=" + User.UserName);

            DataTable dt = SM_ReceiverBLL.GetMyMsg(User.UserName);
            dt.DefaultView.RowFilter = "SendTime > '" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-01") + "'";

            List<Message> lists = new List<Message>(dt.DefaultView.Count);
            foreach (DataRowView row in dt.DefaultView)
            {
                Message m = new Message();
                m.MsgID = (int)row["MsgID"];
                m.Sender = (string)row["Sender"];
                m.SenderRealName = (string)row["SenderRealName"];
                m.Content = (string)row["Content"];
                m.SendTime = (DateTime)row["SendTime"];
                m.IsRead = (string)row["IsRead"];
                m.KeyType = (string)row["KeyType"];
                m.KeyValue = (string)row["KeyValue"];

                lists.Add(m);
            }

            return lists;
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="MsgID"></param>
        /// <returns></returns>
        public static int SetMessageRead(UserInfo User, int MsgID)
        {
            LogWriter.WriteLog("NoticeService.SetMessageRead:UserName=" + User.UserName + ",MsgID=" + MsgID.ToString());

            SM_ReceiverBLL.IsRead(MsgID, User.UserName);
            return 0;
        }

        /// <summary>
        /// 获取未读消息的数量
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        
        public static int GetNewMessageCount(UserInfo User)
        {
            LogWriter.WriteLog("NoticeService.GetNewMessageCount:UserName=" + User.UserName);

            DataTable dt = SM_ReceiverBLL.GetMyMsg(User.UserName);
            if (dt == null) return -1;

            dt.DefaultView.RowFilter = "SendTime > '" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-01") + "' AND IsRead='N'";

            return dt.DefaultView.Count;
        }

        #endregion

        #region 获取指定目录的通知内容
        public static List<Notice> GetNoticeListByCatalog(UserInfo User, int Catalog)
        {
            LogWriter.WriteLog("NoticeService.GetNoticeListByCatalog:UserName=" + User.UserName + ",Catalog=" + Catalog.ToString());

            IList<PN_Notice> notices = null;

            string condition = "PN_Notice.ToAllStaff='Y' AND IsDelete ='N' AND PN_Notice.ApproveFlag=1";

            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_OA.dbo.PN_Notice',ExtPropertys,'Catalog')='" + Catalog.ToString() + "'";
            condition += " AND (PN_Notice.ToAllOrganizeCity='Y' OR PN_Notice.ID IN (SELECT NoticeID FROM PN_ToOrganizeCity WHERE OrganizeCity=" + User.OrganizeCity.ToString() + "))";
            notices = PN_NoticeBLL.GetModelList(condition);

            if (notices == null) return null;

            List<Notice> lists = new List<Notice>(notices.Count);
            foreach (PN_Notice item in notices.OrderByDescending(p => p.InsertTime))
            {
                Notice n = new Notice(item);
                n.HasRead = PN_HasReadUserBLL.IsRead(n.ID, User.UserName);
                lists.Add(n);
            }

            return lists;
        }

        /// <summary>
        /// 获取指定目录的未读通知数量
        /// </summary>
        /// <param name="AuthKey">授权码</param>
        /// <param name="Catalog">目录ID</param>
        /// <returns></returns>
        public static int GetNewNoticeListByCatalog(UserInfo User, int Catalog)
        {
            LogWriter.WriteLog("NoticeService.GetNewNoticeListByCatalog:UserName=" + User.UserName + ",Catalog=" + Catalog.ToString());

            List<Notice> notices = GetNoticeListByCatalog(User, Catalog);
            return notices.Where(p => p.HasRead == false).Count();
        }
        #endregion        
    }
}