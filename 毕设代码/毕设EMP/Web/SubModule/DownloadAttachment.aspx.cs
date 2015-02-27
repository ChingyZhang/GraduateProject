using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using System.IO;

public partial class SubModule_DownloadAttachment : System.Web.UI.Page
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
                MessageBox.ShowAndRedirect(this, "下载附件时参数错误1!", "desktop.aspx");
                return;
            }

            try
            {
                Guid guid = Request.QueryString["GUID"] == null ? Guid.Empty : new Guid(Request.QueryString["GUID"]);

                if (string.IsNullOrEmpty(Request.QueryString["PreViewImage"]))
                    Download(guid);
                else
                    DownPreViewImage(guid);
            }
            catch
            {
                MessageBox.ShowAndRedirect(this, "下载附件时参数错误2!", "desktop.aspx");
                return;
            }
        }
    }

    private void Download(Guid guid)
    {
        ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(guid);
        if (bll.Model == null)
        {
            MessageBox.ShowAndRedirect(this, "指定ID的附件不存在，参数错误!", "desktop.aspx");
            return;
        }

        Response.Clear();
        Response.BufferOutput = true;

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
                break;
            case "xls":
            case "xlsx":
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                break;
            case "ppt":
            case "pptx":
                Response.ContentType = "application/ms-powerpoint";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                break;
            case "pdf":
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                break;
            case "zip":
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
                break;
            case "rar":
                Response.ContentType = "application/rar";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name + "." + bll.Model.ExtName));
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

            if (filepath.StartsWith("~")) filepath = Server.MapPath(filepath);

            if (!File.Exists(filepath))
            {
                MessageBox.ShowAndRedirect(this, "附件在服务器上不存在!", "desktop.aspx");
                return;
            }
            Response.WriteFile(filepath);
        }

        Response.Flush();
        Response.End();
    }

    private void DownPreViewImage(Guid guid)
    {
        ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(guid);
        if (bll.Model == null)
        {
            MessageBox.ShowAndRedirect(this, "指定ID的附件不存在，参数错误!", "desktop.aspx");
            return;
        }
        if (!ATMT_AttachmentBLL.IsImage(bll.Model.ExtName))
        {
            return;
        }
        Response.Clear();
        Response.BufferOutput = true;

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
            default:
                Response.ContentType = "application/octet-stream";
                break;
        }
        #endregion

        if (string.IsNullOrEmpty(bll.Model.Path))
        {
            byte[] data = bll.GetThumbnailData();
            if (data != null && data.Length > 0)
                Response.OutputStream.Write(data, 0, data.Length);
            else
            {
                data = bll.GetData();
                if (data != null && data.Length > 0)
                {
                    Stream filestream = new MemoryStream(data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(filestream);
                    filestream.Close();

                    System.Drawing.Image preview = ImageProcess.MakeThumbnail(image, 0, 80, "H");

                    Stream prestream = new MemoryStream();
                    preview.Save(prestream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    prestream.Position = 0;

                    byte[] predata = new byte[prestream.Length];
                    prestream.Read(predata, 0, (int)prestream.Length);

                    bll.UploadThumbnailData(predata);

                    Response.OutputStream.Write(predata, 0, predata.Length);
                }

            }
        }
        else
        {
            string filepath = "";

            if (bll.Model.Path.StartsWith("~"))
                filepath = Server.MapPath(bll.Model.Path);
            else
                filepath = bll.Model.Path.Replace("/", "\\");

            if (!File.Exists(filepath))
            {
                MessageBox.ShowAndRedirect(this, "附件在服务器上不存在!" + filepath, "desktop.aspx");
                return;
            }

            string filename = filepath.Substring(filepath.LastIndexOf('\\') + 1);
            string previewimagepath = filepath.Substring(0, filepath.LastIndexOf('\\') + 1) + "Preview_" + filename;            //取得缩略图文件名
            previewimagepath = previewimagepath.Substring(0, previewimagepath.LastIndexOf('.') + 1) + "jpg";    //将缩略图扩展名改为JPG

            if (!System.IO.File.Exists(previewimagepath))
                ImageProcess.MakeThumbnail(filepath, previewimagepath, 0, 80, "H");

            Response.WriteFile(previewimagepath);
        }

        Response.Flush();
        Response.End();
    }
}
