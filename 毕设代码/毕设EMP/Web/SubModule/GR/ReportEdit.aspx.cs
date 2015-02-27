using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using WebApplication1.grf;

public partial class SubModule_GR_ReportEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string GetGrPath()
    {
        //int GrID = int.TryParse(Request.QueryString["GrID"],out GrID) ? GrID : 0;

        if (!string.IsNullOrEmpty(Request.QueryString["GrName"]))
        {
            return GetAttachmentDirectory() + Request.QueryString["GrName"];
        }
        return null;
    }

    #region 获取附件文件夹路径
    /// <summary>
    /// 获取下载文件夹路径
    /// </summary>
    /// <returns></returns>
    public string GetAttachmentDirectory()
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "GridReport\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path;
    }
    #endregion
}
