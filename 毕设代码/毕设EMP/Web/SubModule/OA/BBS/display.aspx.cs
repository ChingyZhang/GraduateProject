using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections.Specialized;
using System.Web.Security;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_OA_BBS_display : System.Web.UI.Page
{
    protected string author;
    protected string title1 = "";
    protected static int board = 0;
    protected static bool right = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["username"] = Session["UserName"].ToString(); //用户名
            ViewState["ItemID"] = (Request.QueryString["ID"] == null) ? 0 : Int32.Parse(Request.QueryString["ID"].ToString());  //贴子ID
            ViewState["BoardID"] = (Request.QueryString["BoardID"] == null) ? 0 : Int32.Parse(Request.QueryString["BoardID"].ToString()); //板块ID
            cbx_Tpp.Visible = false;
            cbx_IsPith.Visible = false;
            ckedit_content.Text = "";
            ModeratorInfo();
            BindData();
            if (ConfigHelper.GetConfigString("BBSEditBoard").Contains(ViewState["BoardID"].ToString()) && new BBS_BoardUserMemberBLL().GetUserRoleByBoard((int)ViewState["BoardID"], Session["UserName"].ToString()) != 1)
            {
                tb_edit.Visible = false;
            }
        }
    }

    #region 判断是否版主及相应的权限的操作
    protected void ModeratorInfo()
    {
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        itembll.UpdateHitTimes(Convert.ToInt32(ViewState["ItemID"]));
        author = itembll.Model.Sender.ToString();
        IList<BBS_BoardUserMember> memberList = BBS_BoardUserMemberBLL.GetModelList(" Board=" + (int)ViewState["BoardID"]);
        bool flag = itembll.Model.Sender==Session["UserName"].ToString();
        foreach (BBS_BoardUserMember member in memberList)
        {
            if (member.Role == 1 && member.UserName == (string)ViewState["username"])
                flag = true;
        }
        if (Roles.IsUserInRole("论坛管理员") || flag)
        {
            itemcontent.InnerHtml += "<b>操 作：</b><a href=javascript:window.open('deleteitem.aspx?ItemID=" + Convert.ToInt32(ViewState["ItemID"]) + "&BoardID=" + Convert.ToInt32(ViewState["BoardID"]) + "','_self','');>删除此贴</a>|<a href='MoveItem.aspx?ItemID=" + Convert.ToInt32(ViewState["ItemID"]) + "'>移动帖子</a><br><hr color='#C0C0C0' size='1'>";
            cbx_IsPith.Visible = true;
            cbx_Tpp.Visible = true;
            right = true;
        }
    }
    #endregion

    #region 显示论坛文章及回复的内容
    public void BindData()
    {
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        this.title1 = lblTitle.Text = itembll.Model.Title.ToString();
        lblForumItemName.Text = itembll.Model.Title.ToString();
        lblCatalog.Text = new BBS_CatalogBLL(new BBS_BoardBLL(itembll.Model.Board).Model.Catalog).Model.Name;
        lblBoardName.Text = new BBS_BoardBLL(itembll.Model.Board).Model.Name;
        board = new BBS_BoardBLL(itembll.Model.Board).Model.ID;

        #region 显示发布者信息
        sendman.Text = DisplayFullInfo(itembll.Model.Sender);
        #endregion

        sendtime.Text = itembll.Model.SendTime.ToString();
        browsetime.Text = itembll.Model.HitTimes.ToString();
        replaytimes.Text = itembll.Model.ReplyTimes.ToString();
        browsetime.Text = itembll.Model.HitTimes.ToString();
        replaytimes.Text = itembll.Model.ReplyTimes.ToString();
        itemcontent.InnerHtml = FormatTxt(BBS_ForumItemDAL.txtMessage(itembll.Model.Content.ToString()));


        if (itembll.Model["IsTop"] == "Y")
        {
            cbx_Tpp.Checked = true;
        }

        if (itembll.Model["IsPith"] == "Y")
        {
            cbx_IsPith.Checked = true;
        }

        BindGrid();
    }

    public void BindGrid()
    {
        dgshow.DataSource = BBS_ForumReplyBLL.GetModelList("Item=" + Convert.ToInt32(ViewState["ItemID"]) + "");
        dgshow.DataBind();
    }
    #endregion


    #region 删除论坛文章的回复
    protected void DelReplay(object sender, System.EventArgs e)
    {
        //删除论坛文章的回复
        BBS_ForumReplyBLL replybll = new BBS_ForumReplyBLL();
        replybll.Model.ID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
        replybll.Delete(replybll.Model.ID);

        //修改论坛文章的评论数量
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        itembll.UpdateRemoveReplyTimes(Convert.ToInt32(ViewState["ItemID"]));
        BindData();
    }
    #endregion

    #region 转换发布者或回复者的全称信息
    protected string DisplayFullInfo(string loginname)
    {
        string ret = "";
        Org_Staff staff = UserBLL.GetStaffByUsername(loginname);

        ret = "登录名称：<a href=\"javascript:SendMsg('" + loginname + "','" + Server.UrlPathEncode(staff.RealName) + "');\" title='发送短内短信' style='color:red' target='_self'>" + loginname + "</a>";
        ret += "<br/>真实姓名：" + staff.RealName + "<br/>所属区域：" + TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "Name", "SuperID", 0, staff.OrganizeCity);

        Org_Position p = new Org_PositionBLL(staff.Position).Model;
        if (p != null)
            ret += "<br/>职位：" + p.Name;
        return ret;
    }
    #endregion

    #region 换行
    protected string FormatTxt(string content)
    {
        return (content.Replace(((char)20).ToString(), "<br>"));
    }
    #endregion

    #region 留言框获取焦点
    protected void btn_SubmitComment_Click(object sender, EventArgs e)
    {
        SetFocus(Title);
    }
    #endregion

    #region 搜索
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search/index.aspx");
    }
    #endregion

    #region 帖子置顶
    protected void cbx_Tpp_Click(object sender, System.EventArgs e)
    {
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        if (itembll.Model["IsTop"] == "N")
        {
            itembll.Model["IsTop"] = "Y";
            itembll.Update();
            MessageBox.Show(this.Page, "帖子已置顶！");
        }
        else
        {
            itembll.Model["IsTop"] = "N";
            itembll.Update();
            MessageBox.Show(this.Page, "帖子已取消置顶！");
        }
    }
    #endregion

    #region 帖子精华
    protected void cbx_IsPith_Click(object sender, System.EventArgs e)
    {
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        if (itembll.Model["IsPith"] == "N")
        {
            itembll.Model["IsPith"] = "Y";
            itembll.Update();
            MessageBox.Show(this.Page, "帖子已经设置为精华贴！");
        }
        else
        {
            itembll.Model["IsPith"] = "N";
            itembll.Update();
            MessageBox.Show(this.Page, "帖已经不是精华贴！");
        }
    }
    #endregion

    protected void dgshow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgshow.PageIndex = e.NewPageIndex;
        BindGrid();

        if (e.NewPageIndex == 0)
            tbl_ForumItem.Visible = true;
        else
            tbl_ForumItem.Visible = false;

    }

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
        if (ViewState["UpattList"] != null)
        {
            foreach (BBS_ForumAttachment att in upattlist)
            {
                count++;
                this.lbx_AttList.Items.Add(new ListItem(att.Name.ToString() + "." + att.ExtName.ToString(), count.ToString()));
            }
        }
        else
        {
            MessageBox.Show(this, "请添加附件！");
            return;
        }
    }
    #endregion

    #region 添加附件
    protected void btn_UpAtt_Click(object sender, EventArgs e)
    {
        UploadAtt();
        btn_OK.Focus();
    }
    #endregion

    #region 添加帖子回复
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        #region 帖子的回复

        #region 添加帖子的回复
        BBS_ForumReplyBLL replybll = new BBS_ForumReplyBLL();
        replybll.Model.ItemID = Convert.ToInt32(ViewState["ItemID"]);
        replybll.Model.Title = Title.Value;
        replybll.Model.Content =ckedit_content.Text;
        replybll.Model.Replyer = Session["UserName"].ToString();
        replybll.Model.ReplyTime = DateTime.Now;
        replybll.Model.IPAddress = Request.ServerVariables.Get("REMOTE_ADDR").ToString();
        int replyid = replybll.Add();    // 返回已经回复的帖子的ID

        //修改论坛文章的留言量
        BBS_ForumItemBLL itembll = new BBS_ForumItemBLL(Convert.ToInt32(ViewState["ItemID"]));
        itembll.UpdateAddReplyTimes(Convert.ToInt32(ViewState["ItemID"]));
        #endregion

        #region 添加留言的附件
        ArrayList upattlist = (ArrayList)ViewState["UpattList"];
        if (upattlist != null && upattlist.Count > 0)
        {
            foreach (BBS_ForumAttachment att in upattlist)
            {
                string path = att.Path;
                if (path.StartsWith("~")) path = Server.MapPath(path);
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buff = new byte[stream.Length];
                stream.Read(buff, 0, buff.Length);
                stream.Close();
                att.Path = "";
                att.ItemID = (int)ViewState["ItemID"];

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

                att.Reply = replyid;
                BBS_ForumAttachmentBLL bll = new BBS_ForumAttachmentBLL();
                bll.Model = att;
                bll.Add(buff);

                BBS_ForumAttachment m = bll.Model;
                string uploadcontent = "";     //插入主表中

                switch (att.ExtName.ToLower())
                {
                    case "jpg":
                    case "gif":
                    case "bmp":
                    case "png":
                        uploadcontent = " [IMG]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/IMG]<br/>";
                        break;
                    case "mp3":
                        uploadcontent = " [MP=320,70]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/MP]<br/>";
                        break;
                    case "avi":
                        uploadcontent = " [MP=320,240]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/MP]<br/>";
                        break;
                    case "swf":
                        uploadcontent = " [FLASH]DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + "[/FLASH]<br/>";
                        break;
                    default:
                        uploadcontent = "<a href=DownloadAttachfile.aspx?GUID=" + m.GUID.ToString() + ">" + att.Name + "." + att.ExtName + "</a><br/>";
                        break;
                }
                ViewState["Content"] += uploadcontent + "<br/>";

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

            //将附件的路径添加到回复主表中去
            replybll.Model.Content += "<br/><font color=red><b>附件列表:</b></font><br/>" + ViewState["Content"].ToString();
            replybll.Update();

            //清空附件的列表
            for (int i = upattlist.Count - 1; i >= 0; i--)
            {
                this.lbx_AttList.Items.RemoveAt(i);
                upattlist.RemoveAt(i);
            }
            ViewState["UpattList"] = upattlist;
        }
        #endregion

        #endregion

        Response.Redirect("display.aspx?ID=" + ViewState["ItemID"].ToString() + "&BoardID=" + ViewState["BoardID"].ToString());
    }
    #endregion
}
