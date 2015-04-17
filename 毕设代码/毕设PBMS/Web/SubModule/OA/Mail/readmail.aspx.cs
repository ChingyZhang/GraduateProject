using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MCSFramework.SQLDAL.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using System.Collections.Generic;
using MCSFramework.Common;

public partial class SubModule_OA_Mail_readmail : System.Web.UI.Page
{
    protected static string CurrentPageIndex = "", FolderType = "", MailID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MailID = Request.QueryString["ID"].ToString();
            FolderType = Session["FolderType"] == null ? "1" : Session["FolderType"].ToString();
            ReadMailInfo();
            this.btnDelete.Attributes["onclick"] = "javascript:return confirm('您确认要删除此邮件吗?');";
        }
    }

    #region 修改邮件的是否读过的状态
    /// </summary>
    public void UpdateIsReadByInfo()
    {
        new ML_MailBLL(int.Parse(MailID)).UpdateIsRead();
    }
    #endregion

    #region 显示邮件内容
    /// </summary>
    public void ReadMailInfo()
    {
        ML_MailBLL mailbll = new ML_MailBLL(int.Parse(MailID));
        mailbll.UpdateIsRead();

        if (mailbll.Model.Sender != null)
        {
            lblSenderName.Text = mailbll.Model.Sender.ToString();       //寄信人
            if (mailbll.Model.Receiver == "" && mailbll.Model.ReceiverStr != "")
            {
                if (mailbll.Model.ReceiverStr.Substring(mailbll.Model.ReceiverStr.Length - 1, 1) == ",")
                    lblReceiver.Text = mailbll.Model.ReceiverStr.Substring(0, mailbll.Model.ReceiverStr.Length - 1);
                else
                    lblReceiver.Text = mailbll.Model.ReceiverStr;
            }
        }
        if (mailbll.Model.Receiver != null && mailbll.Model.Receiver != "")
        {
            lblReceiver.Text = mailbll.Model.ReceiverStr.ToString();       //收信人
        }

        if (mailbll.Model.CcToAddr != null)
        {
            lblCcToAddr.Text = mailbll.Model.CcToAddr.ToString();       //抄送地址
        }
        //if (mailbll.Model.BccToAddr != null)
        //{
        //    lblBccToAddr.Text = mailbll.Model.BccToAddr.ToString();         //密送地址
        //}
        if (mailbll.Model.Subject != null)
        {
            lblSubject.Text = mailbll.Model.Subject.ToString();             //主题
        }
        if (mailbll.Model.SendTime != null)
        {
            lblSendTime.Text = mailbll.Model.SendTime.ToString();               //发送时间
        }
        if (mailbll.Model.Content != null)
        {
            lblContent.Text = mailbll.Model.Content.ToString();         //内容
        }

        #region 显示附件信息

        IList<ML_AttachFile> fileList = mailbll.GetAttachFiles();

        foreach (ML_AttachFile attach in fileList)
        {
            lblAttachFile.Text += "&nbsp;<a href='Download.aspx?GUID=" + attach.GUID.ToString() + "' target='_blank'>" +
                attach.Name.ToString() + "(" + (attach.Size / 1024).ToString() + " KB)</a><br>";
        }
        #endregion
    }
    #endregion

    #region 返回收件箱
    protected void btnreturn_Click(object sender, System.EventArgs e)
    {
        Server.Transfer("index.aspx?FolderType=" + FolderType + "");
    }
    #endregion

    #region 删除邮件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        ML_MailDAL maildal = new ML_MailDAL();
        if (maildal.UpdateIsdeleteByID(int.Parse(MailID)) == 0)
        {
            Response.Write("<script language=javascript>alert('邮件删除成功!');window.location='Index.aspx?FolderType=" + FolderType + "';</script>");
        }
        else
        {
            Server.Transfer("../../Error.aspx");
        }
    }
    #endregion
}
