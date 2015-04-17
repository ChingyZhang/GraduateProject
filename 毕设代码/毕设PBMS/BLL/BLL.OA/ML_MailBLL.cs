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
    public class ML_MailBLL : BaseSimpleBLL<ML_Mail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.OA.ML_MailDAL";
        private ML_MailDAL _dal;

        #region 构造函数
        ///<summary>
        ///ML_MailDAL
        ///</summary>
        public ML_MailBLL()
            : base(DALClassName)
        {
            _dal = (ML_MailDAL)_DAL;
            _m = new ML_Mail();
        }

        public ML_MailBLL(int id)
            : base(DALClassName)
        {
            _dal = (ML_MailDAL)_DAL;
            FillModel(id);
        }

        public ML_MailBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ML_MailDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ML_Mail> GetModelList(string condition)
        {
            return new ML_MailBLL()._GetModelList(condition);
        }
        #endregion

        public static IList<ML_Mail> GetRecieveMail(string Username)
        {
            string condition = " Receiver = '" + Username + "' AND IsDelete='N' AND Folder=1 order by ML_Mail.SendTime desc ";
            return GetModelList(condition);
        }

        #region 根据ID改变邮箱类型
        public int UpdateMailFolder(int mailType)
        {
            return _dal.UpdateMailFolder(_m.ID, mailType);
        }
        #endregion

        #region 根据ID改变邮件IsDelete属性
        public int UpdateIsdelete()
        {
            return _dal.UpdateIsdeleteByID(_m.ID);
        }
        #endregion

        #region 获取邮件的附件列表
        public IList<ML_AttachFile> GetAttachFiles()
        {
            return ML_AttachFileBLL.GetModelList("MailID=" + _m.ID);
        }
        #endregion

        #region 根据ID改变邮件IsRead属性
        public int UpdateIsRead()
        {
            return _dal.UpdateIsReadByID(_m.ID);
        }
        #endregion

        #region 根据Receiver获得未读邮件数
        public int GetNewMailCountByReceiver(string receiver)
        {
            return _dal.GetNewMailCountByReceiver(receiver);
        }
        #endregion

        #region 发送一组邮件
        public static ArrayList MailSend(ML_Mail mailbody)
        {
            ML_MailDAL dal = (ML_MailDAL)DataAccess.CreateObject(DALClassName);
            string[] RecvAr = System.Text.RegularExpressions.Regex.Split(mailbody.ReceiverStr + "," + mailbody.BccToAddr + "," + mailbody.CcToAddr, ",");
            ArrayList listMailID = new ArrayList();

            //给发送者发送一条邮件
            mailbody.Folder = 2;                    //寄件箱
            mailbody.IsRead = "Y";
            int mailid = dal.Add(mailbody);
            if (mailid != 0) listMailID.Add(mailid);

            // 开始循环发送邮件
            for (int i = 0; i < RecvAr.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(RecvAr[i])) continue;

                mailbody.IsRead = "N";
                mailbody.Folder = 1;                //收件箱
                mailbody.Receiver = RecvAr[i].ToString();

                mailid = dal.Add(mailbody);
                if (mailid != 0) listMailID.Add(mailid);
            }

            return listMailID;
        }
        #endregion
    }
}
