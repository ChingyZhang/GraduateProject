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
                string AttGUID = Request.QueryString["GUID"];
                bool PreView = !string.IsNullOrEmpty(Request.QueryString["PreViewImage"]);

                string AttPath = ConfigHelper.GetConfigString("AttachmentPath");
                if (string.IsNullOrEmpty(AttPath)) AttPath = "D:\\Attachment";
                AttPath = AttPath + "\\" + (PreView ? "P\\" : "") + AttGUID.ToString().Substring(0, 1);
                if (!Directory.Exists(AttPath)) Directory.CreateDirectory(AttPath);

                string[] filenames = Directory.GetFiles(AttPath, Request.QueryString["GUID"] + ".*", SearchOption.TopDirectoryOnly);

                if (filenames.Length > 0)
                {
                    #region 从本地目录缓存中加载图片
                    byte[] data = File.ReadAllBytes(filenames[0]);

                    string extname = "";
                    int p = filenames[0].LastIndexOf('.');
                    if (p > 0) extname = filenames[0].Substring(p + 1);

                    SendAttachmentData(data, AttGUID, extname);

                    return;
                    #endregion
                }

                Guid guid = Request.QueryString["GUID"] == null ? Guid.Empty : new Guid(Request.QueryString["GUID"]);

                if (string.IsNullOrEmpty(Request.QueryString["PreViewImage"]))
                    Download(AttPath, guid);
                else
                    DownPreViewImage(AttPath, guid);
            }
            catch
            {
                MessageBox.ShowAndRedirect(this, "下载附件时参数错误2!", "desktop.aspx");
                return;
            }
        }
    }

    private void Download(string Path, Guid guid)
    {
        ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(guid);
        if (bll.Model == null)
        {
            MessageBox.ShowAndRedirect(this, "指定ID的附件不存在，参数错误!", "desktop.aspx");
            return;
        }

        if (string.IsNullOrEmpty(bll.Model.Path))
        {
            byte[] data = bll.GetData();
            if (data != null)
            {
                FileStream fs = File.Create(Path + "\\" + bll.Model.GUID + "." + bll.Model.ExtName);
                fs.Write(data, 0, data.Length);
                fs.Close();

                SendAttachmentData(data, bll.Model.Name, bll.Model.ExtName);
            }
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

            #region 从本地目录中加载图片
            byte[] data = File.ReadAllBytes(filepath);

            string extname = "";
            int p = filepath.LastIndexOf('.');
            if (p > 0) extname = filepath.Substring(p + 1);

            SendAttachmentData(data, bll.Model.Name, extname);
            #endregion
        }
    }

    private void DownPreViewImage(string Path, Guid guid)
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

        string AttachmentPath = ConfigHelper.GetConfigString("AttachmentPath");
        if (string.IsNullOrEmpty(AttachmentPath)) AttachmentPath = "D:\\Attachment";

        if (string.IsNullOrEmpty(bll.Model.Path))
        {
            byte[] data = bll.GetThumbnailData();
            if (data != null && data.Length > 0)
            {
                if (data != null)
                {
                    FileStream fs = File.Create(Path + "\\" + bll.Model.GUID + "." + bll.Model.ExtName);
                    fs.Write(data, 0, data.Length);
                    fs.Close();

                    SendAttachmentData(data, bll.Model.Name, bll.Model.ExtName);
                }
            }
            else
            {
                data = bll.GetData();
                if (data != null && data.Length > 0)
                {
                    #region 生成缩略图
                    Stream filestream = new MemoryStream(data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(filestream);
                    filestream.Close();

                    System.Drawing.Image preview = ImageProcess.MakeThumbnail(image, 0, 140, "H");

                    Stream prestream = new MemoryStream();
                    preview.Save(prestream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    prestream.Position = 0;

                    byte[] predata = new byte[prestream.Length];
                    prestream.Read(predata, 0, (int)prestream.Length);

                    bll.UploadThumbnailData(predata);

                    FileStream fs = File.Create(AttachmentPath + "\\" + "P_" + bll.Model.GUID + "." + bll.Model.ExtName);
                    fs.Write(predata, 0, predata.Length);
                    fs.Close();
                    #endregion

                    SendAttachmentData(data, bll.Model.Name, bll.Model.ExtName);
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
                ImageProcess.MakeThumbnail(filepath, previewimagepath, 0, 140, "H");

            #region 从本地目录中加载图片
            byte[] data = File.ReadAllBytes(previewimagepath);

            string extname = "";
            int p = previewimagepath.LastIndexOf('.');
            if (p > 0) extname = previewimagepath.Substring(p + 1);

            SendAttachmentData(data, bll.Model.Name, extname);
            #endregion
        }
    }

    private void SendAttachmentData(byte[] data, string FileName, string ExtName)
    {
        Response.Clear();
        Response.BufferOutput = true;
        Response.CacheControl = "Private";
        Response.Cache.SetMaxAge(new TimeSpan(0, 0, 100));  //100秒

        #region 确定ContentType
        switch (ExtName)
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
            case "xls":
            case "xlsx":
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
            case "ppt":
            case "pptx":
                Response.ContentType = "application/ms-powerpoint";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
            case "pdf":
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
            case "zip":
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
            case "rar":
                Response.ContentType = "application/rar";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(FileName + "." + ExtName));
                break;
        }
        #endregion

        Response.OutputStream.Write(data, 0, data.Length);        
        Response.Flush();
        Response.End();
    }
}
