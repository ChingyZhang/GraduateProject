using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using System.Collections.Generic;

public partial class SubModule_OA_Mail_download : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
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

    private void Download(Guid guid)
    {
        ML_AttachFileBLL bll = new ML_AttachFileBLL(guid);
        if (bll.Model == null) Response.End();

        Response.Clear();
        Response.Buffer = true;

        #region 确定ContentType
        if (bll.Model.Extname == "")
        {
            FileInfo f = new FileInfo(bll.Model.Visualpath);
            bll.Model.Extname = f.Extension;
            bll.Model.Name = f.Name.Substring(0, f.Name.LastIndexOf("."));
        }
        switch (bll.Model.Extname.ToLower())
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
            case "xls":
            case "xlsx":
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
            case "ppt":
            case "pptx":
                Response.ContentType = "application/ms-powerpoint";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
            case "pdf":
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
            case "zip":
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
            case "rar":
                Response.ContentType = "application/rar";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
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
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(bll.Model.Name));
                Response.AppendHeader("Content-Length", bll.Model.Size.ToString());
                break;
        }
        #endregion

        if (string.IsNullOrEmpty(bll.Model.Visualpath))
        {
            byte[] data = bll.GetData();
            if (data != null) Response.OutputStream.Write(data, 0, data.Length);
        }
        else
        {
            string destFileName = "";  //变量接收附件的路径
            if (bll.Model.Visualpath.StartsWith("~"))
                destFileName = Server.MapPath(bll.Model.Visualpath);
            else
                destFileName = bll.Model.Visualpath;

            //根据路径打开或另保存附件
            if (File.Exists(destFileName))
            {
                Response.WriteFile(destFileName);
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Write("<script langauge=javascript>alert('文件不存在!" + destFileName + "');</script>");
                Response.Write("<script>window.close();</script>");
            }
        }
    }
}
