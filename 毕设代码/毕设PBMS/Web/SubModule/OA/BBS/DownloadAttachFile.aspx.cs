using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using System.IO;

public partial class SubModule_OA_BBS_DownloadAttachFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", "/Default.aspx");
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

    private void Download(Guid id)
    {
        BBS_ForumAttachmentBLL bll = new BBS_ForumAttachmentBLL(id);
        if (bll == null) Response.End();

        Response.Clear();

        #region 确定ContentType
        switch (bll.Model.ExtName.ToLower())
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
            case "xls":
            case "xlsx":
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
            case "ppt":
            case "pptx":
                Response.ContentType = "application/ms-powerpoint";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
            case "pdf":
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
            case "zip":
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
            case "rar":
                Response.ContentType = "application/rar";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                Response.AppendHeader("Content-Length", bll.Model.FileSize.ToString());
                break;
        }
        #endregion

        if (string.IsNullOrEmpty(bll.Model.Path))
        {
            byte[] data = bll.GetData();
            if (data != null) Response.OutputStream.Write(data, 0, data.Length);
        }
        else
        {
            string filepath = bll.Model.Path;

            if (filepath.StartsWith("~") || filepath.StartsWith("AttachFiles")) filepath = Server.MapPath(filepath);

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
