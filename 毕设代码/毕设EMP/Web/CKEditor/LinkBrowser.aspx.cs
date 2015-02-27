using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class ckeditor_LinkBrowser : System.Web.UI.Page
{
    private const string AttachmentDownloadURL = "~/SubModule/DownloadAttachment.aspx?GUID={0}";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.LoginUrl);
            }

            BindDirectoryList();
            ddl_DirectoryList.SelectedIndex = 0;
            ChangeDirectory(null, null);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "FocusImageList", "window.setTimeout(\"document.getElementById('" + ImageList.ClientID + "').focus();\", 1);", true);
    }

    protected void BindDirectoryList()
    {
        List<DateTime> lists = ATMT_AttachmentBLL.GetCKEditUploadDate(Session["UserName"].ToString(), false);
        foreach (DateTime date in lists.OrderByDescending(p => p))
        {
            ddl_DirectoryList.Items.Add(date.ToString("yyyy-MM-dd"));
        }
    }

    protected void ChangeDirectory(object sender, EventArgs e)
    {
        SearchTerms.Text = "";
        BindImageList();
        SelectImage(null, null);
    }

    protected void BindImageList()
    {
        ImageList.Items.Clear();
        DateTime uploaddate = DateTime.Today;
        DateTime.TryParse(ddl_DirectoryList.SelectedItem.Text, out uploaddate);

        IList<ATMT_Attachment> atts = ATMT_AttachmentBLL.GetCKEditAttachmentList(uploaddate, Session["UserName"].ToString(), false);

        foreach (ATMT_Attachment att in atts)
        {
            ImageList.Items.Add(new ListItem(att.Name, att.GUID.ToString()));
        }

        if (atts.Count > 0) ImageList.SelectedIndex = 0;
    }

    protected void Search(object sender, EventArgs e)
    {
        BindImageList();
    }

    protected void SelectImage(object sender, EventArgs e)
    {
        int pos = ImageList.SelectedItem.Text.LastIndexOf('.');
        if (pos == -1) return;

        string link = string.Format(AttachmentDownloadURL, ImageList.SelectedValue); ;
        bt_OkButton.OnClientClick = "window.top.opener.CKEDITOR.dialog.getCurrent().setValueOf('info', 'url', encodeURI('" + Page.ResolveUrl(link) + "')); window.top.close(); window.top.opener.focus();";
    }

    protected void DeleteFile(object sender, EventArgs e)
    {
        new ATMT_AttachmentBLL(new Guid(ImageList.SelectedValue)).Delete();
        
        BindImageList();
        SelectImage(null, null);
    }

    protected void Upload(object sender, EventArgs e)
    {
        string filename = bt_UploadedImageFile.FileName;
        byte[] data = bt_UploadedImageFile.FileBytes;

        #region 写入新附件
        ATMT_AttachmentBLL atm = new ATMT_AttachmentBLL();

        atm.Model.RelateType = 75;
        atm.Model.Name = filename;
        atm.Model.ExtName = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
        atm.Model.FileSize = data.Length / 1024;
        atm.Model.UploadUser = Session["UserName"].ToString();
        atm.Model.IsDelete = "N";
        int atm_id = atm.Add(data);
        if (atm_id > 0)
        {
            ddl_DirectoryList.SelectedIndex = 0;
            BindImageList();

            ImageList.SelectedValue = new ATMT_AttachmentBLL(atm_id).Model.GUID.ToString();
            SelectImage(null, null);
        }
        #endregion
    }

    protected void Clear(object sender, EventArgs e)
    {
        Session.Remove("viewstate");
    }
}