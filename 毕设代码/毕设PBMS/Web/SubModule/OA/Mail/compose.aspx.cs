using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.OA;


public partial class SubModule_OA_Mail_compose : System.Web.UI.Page
{
    protected ArrayList upattlist = new ArrayList();
    public string SendTo = "", CcTo = "", BccTo = "", SendToRealName = "", CcToRealName = "", BccToRealName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int MailID = (Request.QueryString["MailID"] != null) ? int.Parse(Request.QueryString["MailID"].ToString()) : 0;
            Session["upattlist"] = upattlist;

            // Action=1 回复 Action=2 转发 Action=3 指定收件人的寄信
            if (Request.QueryString["Action"] != null)
            {
                MailID = (Request.QueryString["MailID"] == null) ? 0 : int.Parse(Request.QueryString["MailID"].ToString());
                if (Request.QueryString["Action"] == "1")
                {
                    ReplySet(MailID);  // 进行回复邮件设置
                }
                if (Request.QueryString["Action"] == "2")
                {
                    ForwardSet(MailID);  // 进行转发邮件设置
                }
                if (Request.QueryString["Action"] == "3")
                {
                    ReceiverSet();  // 进行发信人设置 
                }
            }

            PopulateListView();
        }

    }

    #region 回复邮件设置
    public void ReplySet(int MailID)
    {
        ML_MailBLL mailbll = new ML_MailBLL(MailID);

        string tmpStr = "<br/>" + mailbll.Model.Content.ToString();
        tmpStr = tmpStr.Replace("<br/>", "\r\n");
        this.txtSubject.Text = "回复:" + mailbll.Model.Subject.ToString();
        SendToRealName = mailbll.Model.Sender.ToString();
        Org_StaffBLL staffbll = new Org_StaffBLL();
        SendTo = mailbll.Model.Sender.ToString();
        if (SendTo != "")
        {
            if (UserBLL.GetStaffByUsername(SendTo) != null)
                SendToRealName = SendTo + "[" + UserBLL.GetStaffByUsername(SendTo).RealName + "]" + ",";
            else
                MessageBox.Show(this, "用户名不存在");
        }
        //SendTo = mailbll.Model.CcToAddr.ToString() + ",";
        this.ckedit_content.Text = SendToRealName + "你好!<br/><br/><br/><br/><hr/>";
        this.ckedit_content.Text += "-----------------" + mailbll.Model.SendTime.ToString("yyyy年MM月dd日 HH:mm") +", "+ SendToRealName+"在来信中写道:" + "-----------------<br/>";
        this.ckedit_content.Text += tmpStr;
    }
    #endregion

    #region 转发邮件设置
    public void ForwardSet(int MailID)
    {
        ML_MailBLL mailbll = new ML_MailBLL(MailID);
        string tmpStr = "<br/>" + mailbll.Model.Content.ToString();
        tmpStr = tmpStr.Replace("<br/>", "\r\n");
        this.txtSubject.Text = "Fw:" + mailbll.Model.Subject.ToString();
        this.ckedit_content.Text = "你好!\n\n\n";
        this.ckedit_content.Text += "=======下面是转发邮件=======\n";
        this.ckedit_content.Text += "原邮件发件人姓名:" + mailbll.Model.Sender.ToString() + "\n";
        this.ckedit_content.Text += "原邮件发送时间:" + mailbll.Model.SendTime.ToString() + "\n";
        this.ckedit_content.Text += "原邮件收件人姓名:" + mailbll.Model.ReceiverStr.ToString() + "\n";
        this.ckedit_content.Text += tmpStr;

        //设置附件信息
        ML_AttachFileBLL attachfiledal = new ML_AttachFileBLL();
        IList<ML_AttachFile> fileList = mailbll.GetAttachFiles();
        if (fileList.Count != 0)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                ML_AttachFile att = new ML_AttachFile();

                att.Size = fileList[i].Size;
                att.Name = fileList[i].Name;
                att.Uploaduser = fileList[i].Uploaduser;
                att.Extname = fileList[i].Extname;
                att.GUID = fileList[i].GUID;
                upattlist.Add(att);
            }
        }
        BindAttList();
    }
    #endregion

    #region 发信人设置
    private void ReceiverSet()
    {
        SendTo = (Request.QueryString["Receiver"] == null) ? "" : Request.QueryString["Receiver"].ToString() + ",";
        if (SendTo != "")
        {
            if (UserBLL.GetStaffByUsername(Request.QueryString["Receiver"].ToString()) != null)
                SendToRealName = Request.QueryString["Receiver"].ToString() + "[" + UserBLL.GetStaffByUsername(Request.QueryString["Receiver"].ToString()).RealName + "]" + ",";
            else
                MessageBox.Show(this, "用户名不存在");
        }
        else
            SendToRealName = "";
    }
    #endregion

    #region 获取附件名称信息并将他添加到下拉框中去
    private void BindAttList()
    {
        this.listUp.Items.Clear();
        int count = 0;
        ArrayList upattlist = (ArrayList)Session["upattlist"];
        foreach (ML_AttachFile att in upattlist)
        {
            count++;
            this.listUp.Items.Add(new ListItem(att.Name.ToString(), count.ToString()));
        }
    }
    #endregion

    #region 初始化下拉列表
    public void PopulateListView()
    {
        listImportance.Items.Clear();
        listImportance.Items.Add(new ListItem("一般", "1"));
        listImportance.Items.Add(new ListItem("重要", "2"));
        listImportance.Items.Add(new ListItem("特别重要", "3"));
        listSendTags.Items.Clear();
        listSendTags.Items.Add(new ListItem("客户邮件", "1"));
        listSendTags.Items.Add(new ListItem("休闲邮件", "2"));
        listSendTags.Items.Add(new ListItem("业务邮件", "3"));
        //if (this.listExtMail.Visible)
        //{
        //    {
        //        ML_AttachFileBLL mail = new ML_AttachFileBLL();
        //        this.listExtMail.DataTextField = "Title";
        //        this.listExtMail.DataValueField = "OrderID";
        //        this.listExtMail.DataSource = mail.ExtGetAva(Session["UserName"].ToString().ToString());
        //        this.listExtMail.DataBind();
        //        this.listExtMail.Items.Insert(0, "全部外部邮箱");
        //        this.listExtMail.Items.FindByText("全部外部邮箱").Value = "0";
        //        this.listExtMail.SelectedIndex = 0;
        //    }
        //}
    }
    #endregion


    protected void btnmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx?FolderType=1");
    }

    protected void btnBeginReceive_Click(object sender, EventArgs e)
    {

    }

    protected void btnExtPopSetup_Click(object sender, EventArgs e)
    {
        Response.Redirect("");
    }

    #region 将附件上传到列表框
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string SavePath = ConfigHelper.GetConfigString("AttachmentPath");
        if (string.IsNullOrEmpty(SavePath)) SavePath = "~/Attachment/";
        if (!SavePath.EndsWith("/") && !SavePath.EndsWith("\\")) SavePath += "/";

        SavePath += "Mail/" + DateTime.Now.ToString("yyyyMMdd") + "/" + (string)Session["UserName"] + "/" + DateTime.Now.ToString("HHmmss");

        if (SavePath.StartsWith("~"))
        {
            if (!Directory.Exists(Server.MapPath(SavePath))) Directory.CreateDirectory(Server.MapPath(SavePath));
        }
        else
        {
            if (!Directory.Exists(SavePath)) Directory.CreateDirectory(SavePath);
        }

        ViewState["SavePath"] = SavePath;
        ArrayList upattlist = (ArrayList)Session["upattlist"];
        int size = 0;


        HtmlInputFile[] hif = { hif1, hif2, hif3, hif4 };

        for (int i = 0; i < 4; i++)
        {
            string filename = hif[i].PostedFile.FileName.Trim();
            if (filename != "")
            {
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

                ML_AttachFile att = new ML_AttachFile();
                // 初始化
                att.Name = filename;
                att.Size = hif[i].PostedFile.ContentLength;
                att.Uploaduser = (string)Session["UserName"];
                att.Extname = filename.Substring(filename.LastIndexOf(".") + 1);
                att.Visualpath = SavePath + "/" + filename;
                if (SavePath.StartsWith("~"))
                    hif[i].PostedFile.SaveAs(Server.MapPath(SavePath) + "/" + filename);
                else
                    hif[i].PostedFile.SaveAs(SavePath + "/" + filename);
                size += att.Size;
                upattlist.Add(att);
            }

        }
        Session["upattlist"] = upattlist;

        if ((size / 1024) == 0)
            size = 1;
        else
            size = size / 1024;

        ViewState["UploadSize"] = size;
        BindAttList();

        this.SendToRealName = Request.Form["txtSendTo"].ToString();
        this.CcToRealName = Request.Form["txtCcTo"].ToString();
        this.BccToRealName = Request.Form["txtBccTo"].ToString();
        this.SendTo = Request.Form["hdnTxtSendTo"].ToString();
        this.CcTo = Request.Form["hdnTxtCcTo"].ToString();
        this.BccTo = Request.Form["hdnTxtBccTo"].ToString();
    }
    #endregion

    #region 移除下拉框中附件的信息
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        ArrayList upattlist = (ArrayList)Session["upattlist"];
        for (int i = listUp.Items.Count - 1; i >= 0; i--)
        {
            if (this.listUp.Items[i].Selected)
            {
                this.listUp.Items.RemoveAt(i);
                upattlist.RemoveAt(i);
            }
        }

        Session["upattlist"] = upattlist;
        this.SendToRealName = Request.Form["txtSendTo"].ToString();

        this.SendTo = Request.Form["hdnTxtSendTo"].ToString();
        this.CcTo = Request.Form["hdnTxtCcTo"].ToString();
        this.BccTo = Request.Form["hdnTxtBccTo"].ToString();
        this.CcToRealName = Request.Form["txtCcTo"].ToString();
        this.BccToRealName = Request.Form["txtBccTo"].ToString();
    }
    #endregion

    //  #region
    private ML_Mail ProcessFormPost()
    {
        //　非空验证
        if (string.IsNullOrEmpty(Request.Form["hdnTxtSendTo"].ToString()))
        {
            Response.Write("<script language=javascript>alert('请选择收件人," + Request.Form["hdnTxtSendTo"].ToString() + "!');history.go(-1);</script>");
            Response.End();
        }

        // 处理表单传递参数
        ML_Mail mailbody = new ML_Mail();

        mailbody.MailType = 1; //邮箱类型   1：内部邮件
        mailbody.ReceiverStr = Request.Form["hdnTxtSendTo"].ToString();
        // Response.Write(mailbody.ReceiverStr);

        mailbody.Sender = (string)Session["UserName"];
        mailbody.Receiver = "";
        mailbody.Subject = (txtSubject.Text == "") ? "无主题" : txtSubject.Text;
        mailbody.Content = ckedit_content.Text;
        mailbody.CcToAddr = Request.Form["hdnTxtCcTo"].ToString();
        mailbody.BccToAddr = Request.Form["hdnTxtBccTo"].ToString();
        mailbody.SendTags = Int32.Parse(listSendTags.SelectedItem.Value);
        mailbody.IsRead = "N";
        mailbody.IsDelete = "N";
        int size = this.ckedit_content.Text.Length + (ViewState["UploadSize"] == null ? 0 : (int)ViewState["UploadSize"]);
        if ((size / 1024) == 0) size = 1;
        else size = size / 1024;
        mailbody.Size = size;
        mailbody.Type = 0;
        mailbody.ExtPropertys = "";
        mailbody.Importance = Int32.Parse(listImportance.SelectedItem.Value);
        return mailbody;
    }

    #region 短信处理
    private List<int> ProcessSM()
    {
        SM_MsgBLL smBLL = new SM_MsgBLL();
        smBLL.Model.Sender = (string)Session["UserName"];
        smBLL.Model.SendTime = DateTime.Now;
        smBLL.Model.Content = "您从" + (string)Session["UserName"] + "处收到了一封新的邮件";
        smBLL.Model.Type = 1;
        smBLL.Model.IsDelete = "N";
        int id = smBLL.Add();

        string recipients = Request.Form["hdnTxtSendTo"].ToString() + Request.Form["hdnTxtCcTo"].ToString() + Request.Form["hdnTxtBccTo"].ToString();
        string[] separators = new string[] { "," };
        string[] senders;
        senders = recipients.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        SM_ReceiverBLL _bll = new SM_ReceiverBLL();
        List<int> receiverList = new List<int>();
        foreach (string s in senders)
        {
            _bll.Model.MsgID = id;
            _bll.Model.IsRead = "N";
            _bll.Model.IsDelete = "N";
            _bll.Model.Receiver = s;
            receiverList.Add(_bll.Add());
        }
        return receiverList;
    }
    #endregion

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        ML_Mail mailbody = new ML_Mail();
        mailbody = ProcessFormPost();
        ArrayList mails_id = ML_MailBLL.MailSend(mailbody); // 返回已经发送的邮件ID列表(包括抄送和密抄)

        List<int> receiverList = new List<int>();

        ArrayList upattlist = (ArrayList)Session["upattlist"];

        #region 将附件文件读取到字节组中
        ArrayList upattbuff = new ArrayList(upattlist.Count);
        for (int i = 0; i < upattlist.Count; i++)
        {
            byte[] buff = null;
            ML_AttachFile att = (ML_AttachFile)upattlist[i];
            string path = att.Visualpath;
            if (path == "")
            {
                //转发的附件
                buff = new ML_AttachFileBLL(att.GUID).GetData();
            }
            else
            {
                if (path.StartsWith("~")) path = Server.MapPath(path);
                FileStream stream = new FileStream(path, FileMode.Open);
                buff = new byte[stream.Length];
                stream.Read(buff, 0, buff.Length);
                stream.Close();

                att.Visualpath = "";
                File.Delete(path);
            }
            upattbuff.Add(buff);
        }

        if (ViewState["SavePath"] != null)
        {
            string path = (string)ViewState["SavePath"];
            path = path.Substring(0, path.LastIndexOf("/"));
            Directory.Delete(path, true);
            ViewState["SavePath"] = null;
        }
        #endregion

        ML_AttachFileBLL bll = new ML_AttachFileBLL();
        for (int i = 0; i < upattlist.Count; i++)
        {
            ML_AttachFile att = (ML_AttachFile)upattlist[i];
            foreach (int mailID in mails_id)
            {
                ML_AttachFileBLL attbll = new ML_AttachFileBLL();
                attbll.Model = att;
                attbll.Model.Mailid = mailID;

                attbll.Add((byte[])upattbuff[i]);
            }
        }

        if (this.cbRemind.Checked == true)
        {
            receiverList = ProcessSM();
        }
        Response.Redirect("index.aspx");
    }
}
