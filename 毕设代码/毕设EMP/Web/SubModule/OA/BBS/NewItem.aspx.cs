using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.Common;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class SubModule_OA_BBS_NewItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["UpattList"] = new ArrayList();
            ViewState["username"] = Session["UserName"].ToString();          //用户名
            ViewState["ItemID"] = (Request.QueryString["ID"] == null) ? 0 : Int32.Parse(Request.QueryString["ID"].ToString());  //贴子ID

            ViewState["BoardID"] = Request.QueryString["BoardID"] == null ? 0 : int.Parse(Request.QueryString["BoardID"]);

        }
    }

    #region 添加发帖
    protected void cmdOK_ServerClick(object sender, System.EventArgs e)
    {
        #region 添加论文的主题内容
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL();
        itembll.Model.Board = Convert.ToInt32(ViewState["BoardID"]);
        itembll.Model.Title = Title.Value;
        itembll.Model.Content = ckedit_content.Text;
        itembll.Model.Sender = Session["UserName"].ToString();
        itembll.Model.SendTime = DateTime.Now;
        itembll.Model.HitTimes = 0;                                        //点击次数默认的设置为0  
        itembll.Model.ReplyTimes = 0;                                      //回复次数默认的设置为0
        itembll.Model.LastReplyer = Session["UserName"].ToString();        //默认的设置为本人
        itembll.Model.LastReplyTime = DateTime.Now;
        itembll.Model.IPAddr = Request.ServerVariables.Get("REMOTE_ADDR").ToString();

        itembll.Model["IsTop"] = "N";       //是否置顶
        itembll.Model["IsPith"] = "N";      //是否是精贴

        int itemid = itembll.Add();// 返回已经发布的论文的ID
        #endregion

        #region 添加论文的附件
        ArrayList upattlist = (ArrayList)ViewState["UpattList"];
        if (upattlist != null && upattlist.Count > 0)
        {
            string _content = "<br/><font color=red><b>附件列表:</b></font><br/>";

            foreach (BBS_ForumAttachment att in upattlist)
            {
                string path = att.Path;
                if (path.StartsWith("~")) path = Server.MapPath(path);
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buff = new byte[stream.Length];
                stream.Read(buff, 0, buff.Length);
                stream.Close();
                att.Path = "";
                att.ItemID = itemid;

                #region 自动压缩上传的图片
                if (ATMT_AttachmentBLL.IsImage(att.ExtName))
                {
                    try
                    {
                        MemoryStream s = new MemoryStream(buff);
                        System.Drawing.Image originalImage = System.Drawing.Image.FromStream(s);
                        s.Close();

                        int width = originalImage.Width;

                        if (width > 1024 || att.ExtName == "bmp")
                        {
                            if (width > 1024) width = 1024;

                            System.Drawing.Image thumbnailimage = ImageProcess.MakeThumbnail(originalImage, width, 0, "W");

                            MemoryStream thumbnailstream = new MemoryStream();
                            thumbnailimage.Save(thumbnailstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            thumbnailstream.Position = 0;
                            att.FileSize = (int)(thumbnailstream.Length / 1024);
                            att.ExtName = "jpg";

                            byte[] b = new byte[thumbnailstream.Length];
                            thumbnailstream.Read(b, 0, b.Length);
                            thumbnailstream.Close();
                            buff = b;
                        }
                    }
                    catch { }
                }
                #endregion

                BBS_ForumAttachmentBLL bll = new BBS_ForumAttachmentBLL();
                bll.Model = att;
                bll.Add(buff);

                BBS_ForumAttachment m = bll.Model;
                string _uploadcontent = "";     //插入主表中

                switch (att.ExtName.ToLower())
                {
                    case "jpg":
                    case "gif":
                    case "bmp":
                    case "png":
                        _uploadcontent = " [IMG]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/IMG]<br/>";
                        break;
                    case "mp3":
                        _uploadcontent = " [MP=320,70]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/MP]<br/>";
                        break;
                    case "avi":
                        _uploadcontent = " [MP=320,240]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/MP]<br/>";
                        break;
                    case "swf":
                        _uploadcontent = " [FLASH]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/FLASH]<br/>";
                        break;
                    default:
                        _uploadcontent = "<a href=DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + ">" + att.Name + "." + att.ExtName + "</a><br/>";
                        break;
                }
                _content += _uploadcontent + "<br/>";
            }

            if (ViewState["SavePath"] != null)
            {
                try
                {
                    string path = (string)ViewState["SavePath"];
                    path = path.Substring(0, path.LastIndexOf("\\"));
                    Directory.Delete(path, true);
                    ViewState["SavePath"] = null;
                }
                catch { }
            }

            //将附件的路径添加到发帖主表中去
            itembll.Model.Content += _content;
            itembll.Update();

            //清空附件的列表
            for (int i = upattlist.Count - 1; i >= 0; i--)
            {
                upattlist.RemoveAt(i);
            }
            ViewState["UpattList"] = upattlist;
        }
        #endregion

        Server.Transfer("listview.aspx?Board=" + ViewState["BoardID"].ToString());
    }
    #endregion

    #region 添加附件
    protected void btn_UpAtt_Click(object sender, EventArgs e)
    {
        UploadAtt();
        cmdOK.Focus();
    }
    #endregion

    #region 上载文件
    private void UploadAtt()
    {
        HtmlForm FrmCompose = (HtmlForm)this.Page.FindControl("NewItem");
        Random TempNameInt = new Random();
        string NewMailDirName = TempNameInt.Next(100000000).ToString();

        string SavePath = ConfigHelper.GetConfigString("AttachmentPath");
        if (string.IsNullOrEmpty(SavePath)) SavePath = "~/Attachment/";
        if (!SavePath.EndsWith("/") && !SavePath.EndsWith("\\")) SavePath += "/";

        // 存放附件至提交人目录中，随机生成目录名
        SavePath += "BBS/TMP/" + NewMailDirName + "/" + Session["UserName"].ToString();
        String MapSavePath = "";
        if (SavePath.StartsWith("~"))
            MapSavePath = Server.MapPath(SavePath);
        else
            MapSavePath = SavePath;
        MapSavePath = MapSavePath.Replace("/", "\\");

        Directory.CreateDirectory(MapSavePath);
        ViewState["SavePath"] = MapSavePath;

        ArrayList upattlist = (ArrayList)ViewState["UpattList"];
        if (hif.PostedFile.FileName.Trim() != "")
        {
            string FileName = System.IO.Path.GetFileName(hif.PostedFile.FileName);
            hif.PostedFile.SaveAs(MapSavePath + "/" + FileName);

            string[] attfile = FileName.Split('.');
            BBS_ForumAttachment att = new BBS_ForumAttachment();
            att.Reply = 0;                    //不属于某个回复默认设置为0
            att.Name = attfile[0];
            att.Path = SavePath + "/" + FileName;
            if (attfile.Length > 1) att.ExtName = attfile[attfile.Length - 1];
            att.FileSize = hif.PostedFile.ContentLength;
            att.UploadTime = DateTime.Now;

            if (upattlist == null) upattlist = new ArrayList();
            upattlist.Add(att);
        }
        ViewState["UpattList"] = upattlist;
        BindAttList();
    }
    #endregion

    #region 删除绑定到ListBox里的附件
    protected void btn_DelAtt_Click(object sender, EventArgs e)
    {
        ArrayList upattlist = (ArrayList)ViewState["UpattList"];
        for (int i = lbx_AttList.Items.Count - 1; i >= 0; i--)
        {
            if (this.lbx_AttList.Items[i].Selected)
            {
                this.lbx_AttList.Items.RemoveAt(i);
                upattlist.RemoveAt(i);
            }
        }
        ViewState["UpattList"] = upattlist;
    }
    #endregion

    #region 将附件绑定到ListBox中
    private void BindAttList()
    {

        this.lbx_AttList.Items.Clear();
        int count = 0;
        ArrayList upattlist = (ArrayList)ViewState["UpattList"];
        foreach (BBS_ForumAttachment att in upattlist)
        {
            count++;
            this.lbx_AttList.Items.Add(new ListItem(att.Name.ToString() + "." + att.ExtName.ToString(), count.ToString()));
        }
    }
    #endregion
}