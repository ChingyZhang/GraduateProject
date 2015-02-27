using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using System.IO;
using MCSFramework.Model.EWF;
using MCSFramework.BLL.EWF;

public partial class SubModule_EWF_DownloadAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", this.ResolveClientUrl("~/Default.aspx"));
                return;
            }

            if (string.IsNullOrEmpty(Request.QueryString["GUID"]))
            {
                MessageBox.ShowAndRedirect(this, "下载附件时参数错误1!", this.ResolveClientUrl("~/SubModule/desktop.aspx"));
                return;
            }

            try
            {
                Guid guid = Request.QueryString["GUID"] == null ? Guid.Empty : new Guid(Request.QueryString["GUID"]);

               Download(guid);
            }
            catch
            {
                MessageBox.ShowAndRedirect(this, "下载附件时参数错误2!", this.ResolveClientUrl("~/SubModule/desktop.aspx"));
                return;
            }
        }
    }

    private void Download(Guid guid)
    {
        EWF_Task_AttachmentBLL bll = new EWF_Task_AttachmentBLL(guid);
        if (bll.Model == null) Response.End();

        Response.Clear();

        #region 确定ContentType
        switch (bll.Model.FileType.ToLower())
        {
            case "jpg":
            case "jpeg":
            case "jpe":
                Response.ContentType = "image/jpeg";
                break;
            case "gif":
                Response.ContentType = "image/gif";
                break;
            case "bmp":
                Response.ContentType = "image/bmp";
                break;
            case "png":
                Response.ContentType = "image/png";
                break;
            case "tiff":
            case "tif":
                Response.ContentType = "image/tiff";
                break;
            case "doc":
            case "docx":
                Response.ContentType = "application/ms-word";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "xls":
            case "xlsx":
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "ppt":
            case "pptx":
                Response.ContentType = "application/ms-powerpoint";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "pdf":
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "zip":
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "rar":
                Response.ContentType = "application/rar";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
            case "txt":
                Response.ContentType = "text/plain";
                break;
            case "htm":
            case "html":
                Response.ContentType = "text/html";
                break;
            default:
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.FileType.ToLower()));
                break;
        }
        #endregion

        if (string.IsNullOrEmpty(bll.Model.FilePath))
        {
            byte[] data = bll.GetData();
            if (data != null) Response.OutputStream.Write(data, 0, data.Length);
        }
        else
        {
            string filepath = bll.Model.FilePath;

            if (filepath.StartsWith("~") || filepath.StartsWith("TaskAttachment")) filepath = Server.MapPath(filepath);

            if (!File.Exists(filepath))
            {
                MessageBox.ShowAndRedirect(this, @"附件在服务器上不存在!", this.ResolveClientUrl("~/SubModule/desktop.aspx"));
                return;
            }
            Response.WriteFile(filepath);
        }
        
        Response.Flush();
        Response.End();
    }
}
