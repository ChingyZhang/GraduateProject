using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.BLL.OA;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.OA;
using MCSFramework.Model.Pub;
using MCSFramework.Common;

namespace MCSFramework.WSI.Model
{
    public class Notice
    {
        public int ID = 0;
        public string Topic = "";
        public string KeyWord = "";
        public string Content = "";
        public bool CanComment = false;
        public DateTime InsertTime = new DateTime(1900, 1, 1);
        public bool HasRead = false;

        public string Abstract = "";    //摘要
        public string Author = "";      //作者

        /// <summary>
        /// 首要图片
        /// </summary>
        public Guid ImageGUID = Guid.Empty;

        public List<Attachment> Atts = new List<Attachment>();

        public Notice() { }
        public Notice(int NoticeID)
        {
            PN_Notice m = new PN_NoticeBLL(NoticeID).Model;
            if (m != null) FillModel(m);
        }

        public Notice(PN_Notice m)
        {
            if (m != null) FillModel(m);
        }

        private void FillModel(PN_Notice m)
        {
            if (m == null) return;

            ID = m.ID;
            Topic = m.Topic;
            KeyWord = m.KeyWord;
            Content = m.Content.Replace("/RMS/SubModule/DownloadAttachment.aspx?", MCSFramework.Common.ConfigHelper.GetConfigString("WebSiteURL") + "/DownloadAttachment.aspx?");
            CanComment = m.CanComment.ToUpper() == "Y";
            InsertTime = m.InsertTime;

            Abstract = m["Abstract"];
            if (Abstract == "")
            {
                if (Content.Length > 100)
                    Abstract = Content.Substring(0, 100);
                else
                    Abstract = Content;
            }

            Author = m["Author"];

            #region 替换描述中的图片链接
            Content = Content.Replace("src=\"/Admin/DownloadAttachment.aspx", "src=\"" + ConfigHelper.GetConfigString("WebSiteURL") + "DownloadAttachment.aspx");
            Content = Content.Replace("src=\"/MClub/DownloadAttachment.aspx", "src=\"" + ConfigHelper.GetConfigString("WebSiteURL") + "DownloadAttachment.aspx");
            #endregion

            #region 获取首要图片
            string condition = " RelateType=80 AND RelateID=" + m.ID.ToString() + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='Y'";
            IList<ATMT_Attachment> lists = ATMT_AttachmentBLL.GetModelList(condition);
            if (lists.Count > 0 && ATMT_AttachmentBLL.IsImage(lists[0].ExtName))
            {
                ImageGUID = lists[0].GUID;
            }
            #endregion

            #region 获取附件明细
            Atts = new List<Attachment>();
            IList<ATMT_Attachment> atts = ATMT_AttachmentBLL.GetAttachmentList(80, m.ID, new DateTime(1900, 1, 1), new DateTime(2100, 1, 1));
            foreach (ATMT_Attachment att in atts.OrderBy(p => p.Name))
            {
                Attachment v = new Attachment();
                v.AttName = att.Name;
                v.ExtName = att.ExtName;
                v.GUID = att.GUID;
                v.UploadTime = att.UploadTime;
                v.FileSize = att.FileSize;
                Atts.Add(v);
            }
            #endregion
        }

    }
}