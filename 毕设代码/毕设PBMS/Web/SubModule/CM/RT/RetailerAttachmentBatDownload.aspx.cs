using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections;

public partial class SubModule_CM_RT_RetailerAttachmentBatDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {//客户类型，２：经销商，３：终端门店
        if (!IsPostBack)
        {
            if (Session["UserID"] == null)
            {
                MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", this.ResolveClientUrl("~/Default.aspx"));
                return;
            }
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            #endregion

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                //if (client.Model.ClientType == 3)
                //{
                //    select_Client.SelectValue = ViewState["ClientID"].ToString();
                //    select_Client.SelectText = client.Model.FullName;
                //    select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" +
                //    client.Model.ClientType.ToString() + "&OrganizeCity=" + client.Model.OrganizeCity.ToString();
                //    tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                //}
            }

            tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.BindDropDown();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)Session["AccountType"] == 1)
        {
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
            tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

            if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
            {
                tr_OrganizeCity.RootValue = "0";
                tr_OrganizeCity.SelectValue = "1";
            }
            else
            {
                tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
                tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
            }
        }
       
    }
    #endregion

    public void BindGrid()
    {
        IList<MCSFramework.Model.Pub.ATMT_Attachment> attachmentList = this.GetPageCondition();
        if (attachmentList == null || attachmentList.Count == 0)
        {
            return;
        }

        GridAttachment.DataSource = attachmentList;
        GridAttachment.DataBind();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    protected void btnDownAll_Click(object sender, EventArgs e)
    {
        IList<MCSFramework.Model.Pub.ATMT_Attachment> attachmentList = this.GetPageCondition();
        List<Guid> guidList = new List<Guid>();
        foreach (var attach in attachmentList)
        {
            guidList.Add(attach.GUID);
        }
        this.BtnDownload(guidList);
    }
    protected void btnDownSelected_Click(object sender, EventArgs e)
    {
        List<Guid> guidList = new List<Guid>();
        foreach (GridViewRow row in GridAttachment.Rows)
        {
            CheckBox ckb = (CheckBox)row.FindControl("AttachChecked");
            if (ckb.Checked)
            {
                Guid guid = (Guid)GridAttachment.DataKeys[row.RowIndex]["GUID"];
                guidList.Add(guid);
            }
        }
        this.BtnDownload(guidList);
    }

    protected void GridAttachment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridAttachment.PageIndex = e.NewPageIndex;
        this.BindGrid();  //gridview绑定数据
    }

    protected void GridAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)//判断当前行是否是数据行
        {
            int rowIndex = (int)e.Row.RowIndex;
            Image img = (Image)e.Row.FindControl("img");
            string a = GridAttachment.DataKeys[rowIndex]["ID"].ToString();
            Guid imgGUID = (Guid)GridAttachment.DataKeys[rowIndex]["GUID"];
            img.ImageUrl = "~/SubModule/DownloadAttachment.aspx?GUID=" + imgGUID.ToString() + "&PreViewImage=Y";

            Guid AttGUID = (Guid)GridAttachment.DataKeys[rowIndex]["GUID"];
            ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(AttGUID);
            CM_ClientBLL clientBLL = new CM_ClientBLL(bll.Model.RelateID);
            if (clientBLL.Model != null)
            {
                e.Row.Cells[1].Text = clientBLL.Model.FullName;
                e.Row.Cells[2].Text = clientBLL.Model.Code;
            }
        }
    }

    #region 封装按钮下载事件
    /// <summary>
    /// 封装按钮下载事件
    /// </summary>
    /// <param name="guidList"></param>
    public void BtnDownload(List<Guid> guidList)
    {
        if (guidList == null || guidList.Count == 0)
        {
            MessageBox.Show(this, "无可下载文件");
            return;
        }
        string downloadDir = this.GetDownloadDir();
        foreach (var guid in guidList)
        {
            this.SaveImgToDisk(downloadDir, guid);
        }
        string zipFile = this.GetAttDoc() + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
        this.ZipDir(downloadDir, zipFile, 9);
        this.DownLoadFile(zipFile);

    }
    #endregion

    #region 获取符合页面查询条件的附件信息
    /// <summary>
    /// 获取符合页面查询条件的附件信息
    /// </summary>
    /// <returns></returns>
    public IList<MCSFramework.Model.Pub.ATMT_Attachment> GetPageCondition()
    {
        DateTime timeBegin = DateTime.Parse(tbx_begin.Text);
        DateTime timeEnd = DateTime.Parse(tbx_end.Text);

        string condition = " RelateType = 30 AND lower(ExtName) in ('bmp','jpg','gif','png') AND IsDelete!='Y' AND UploadTime BETWEEN '" + timeBegin.ToString("yyyy-MM-dd") + "' AND '" + timeEnd.ToString("yyyy-MM-dd 23:59:59") + "'";
        if (tbx_FindName.Text != "") condition += " AND Name like '%" + tbx_FindName.Text + "%'";

        if (select_Client.SelectValue != "")
            condition += " AND RelateID = " + select_Client.SelectValue.ToString();
        else
        {
            if (tr_OrganizeCity.SelectValue == "1" || tr_OrganizeCity.SelectValue == "0")
            {
                condition += @" AND RelateID IN 
                    (SELECT ID FROM MCS_CM.dbo.CM_Client WHERE ActiveFlag=1 AND ApproveFlag=1 AND ClientType=3)";
            }
            else
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (!string.IsNullOrEmpty(orgcitys)) orgcitys += "," + tr_OrganizeCity.SelectValue;
                else orgcitys += tr_OrganizeCity.SelectValue;
                if (orgcitys != "") condition += @" AND RelateID IN 
                (SELECT ID FROM MCS_CM.dbo.CM_Client 
                 WHERE ActiveFlag=1 AND ApproveFlag=1 AND ClientType=3  AND OrganizeCity IN (" + orgcitys + ") )";
            }
        }
        return ATMT_AttachmentBLL.GetModelList(condition);
    }
    #endregion

    #region 获取附件文件夹
    /// <summary>
    /// 获取附件文件夹
    /// </summary>
    /// <returns></returns>
    public string GetAttDoc()
    {
        string AttPath = ConfigHelper.GetConfigString("AttachmentPath");
        if (string.IsNullOrEmpty(AttPath)) AttPath = "D:\\Attachment";
        return AttPath;
    }

    #endregion

    #region 创建临时文件下载目录
    /// <summary>
    /// 创建临时文件下载目录
    /// </summary>
    /// <returns></returns>
    public string GetDownloadDir()
    {
        //创建临时文件下载目录
        string downloadDir = GetAttDoc() + "\\" + "RetailerAttachmentBatDownload" + DateTime.Now.ToString("yyyyMMddhhmmss");
        if (Directory.Exists(downloadDir)) Directory.Delete(downloadDir, true);
        Directory.CreateDirectory(downloadDir);
        return downloadDir;
    }
    #endregion

    #region 将文件存放至指定目录
    /// <summary>
    /// 将文件存放至指定目录
    /// </summary>
    /// <param name="downloadDir">文件指定存放目录</param>
    /// <param name="AttGUID">图片GUID编码</param>
    public void SaveImgToDisk(string downloadDir, Guid AttGUID)
    {
        ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(AttGUID);
        if (bll.Model == null || !ATMT_AttachmentBLL.IsImage(bll.Model.ExtName))
        {
            MessageBox.Show(this, "文件不存在或无文件类型" + AttGUID.ToString());
            return;
        }

        //创建文件名
        CM_Client client = new CM_ClientBLL(bll.Model.RelateID).Model;
        string fileName = client.FullName + "_" + client.Code + "_" + bll.Model.Name + "_" + bll.Model.UploadTime.ToString("yyyMMddHHmmss") + "." + bll.Model.ExtName;

        #region 替换不能包括的非法字符
        fileName = fileName.Replace("/", "");
        fileName = fileName.Replace("\\", "");
        fileName = fileName.Replace(":", "");
        fileName = fileName.Replace("*", "");
        fileName = fileName.Replace("?", "");
        fileName = fileName.Replace("<", "");
        fileName = fileName.Replace(">", "");
        fileName = fileName.Replace("|", "");
        fileName = fileName.Replace("\"", "");
        #endregion

        try
        {
            //数据库中存在该文件路径则直接从文件目录查找并移动
            if (!string.IsNullOrEmpty(bll.Model.Path))
            {
                string tempPath = bll.Model.Path.StartsWith("~") ? Server.MapPath(bll.Model.Path) : bll.Model.Path.Replace("/", "\\");
                string filename = bll.Model.Name + "." + bll.Model.ExtName;
                if (File.Exists(bll.Model.Path))
                {
                    File.Copy(tempPath, downloadDir + "\\" + fileName);
                    return;
                }
            }
            else//读取二进制流并生成文件
            {
                byte[] data = bll.GetData();
                if (data != null)
                {
                    FileStream fs = File.Create(downloadDir + "\\" + fileName);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    return;
                }
            }
        }
        catch { }
    }
    #endregion

    #region 压缩文件夹
    /// <summary>  
    /// 压缩文件夹的方法  
    /// </summary>  
    /// <param name="DirToZip">被压缩的文件名称(包含文件路径)</param>  
    /// <param name="ZipedFile">压缩后的文件名称(包含文件路径)</param>  
    /// <param name="CompressionLevel">压缩率0（无压缩）,9（压缩率最高）</param>  
    public void ZipDir(string DirToZip, string ZipedFile, int CompressionLevel)
    {
        //压缩文件为空时默认与压缩文件夹同一级目录  
        if (ZipedFile == string.Empty)
        {
            ZipedFile = DirToZip.Substring(DirToZip.LastIndexOf("/") + 1);
            ZipedFile = DirToZip.Substring(0, DirToZip.LastIndexOf("/")) + "//" + ZipedFile + ".zip";
        }

        if (Path.GetExtension(ZipedFile) != ".zip")
        {
            ZipedFile = ZipedFile + ".zip";
        }

        using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(ZipedFile)))
        {
            zipoutputstream.SetLevel(CompressionLevel);
            Crc32 crc = new Crc32();
            Hashtable fileList = getAllFies(DirToZip);
            foreach (DictionaryEntry item in fileList)
            {
                FileStream fs = File.OpenRead(item.Key.ToString());
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(item.Key.ToString().Substring(DirToZip.Length + 1));
                entry.DateTime = (DateTime)item.Value;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                zipoutputstream.PutNextEntry(entry);
                zipoutputstream.Write(buffer, 0, buffer.Length);
            }
        }
    }

    /// <summary>  
    /// 获取所有文件  
    /// </summary>  
    /// <returns></returns>  
    private Hashtable getAllFies(string dir)
    {
        Hashtable FilesList = new Hashtable();
        DirectoryInfo fileDire = new DirectoryInfo(dir);
        if (!fileDire.Exists)
        {
            throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
        }

        this.getAllDirFiles(fileDire, FilesList);
        this.getAllDirsFiles(fileDire.GetDirectories(), FilesList);
        return FilesList;
    }

    /// <summary>  
    /// 获取一个文件夹下的所有文件夹里的文件  
    /// </summary>  
    /// <param name="dirs"></param>  
    /// <param name="filesList"></param>  
    private void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
    {
        foreach (DirectoryInfo dir in dirs)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                filesList.Add(file.FullName, file.LastWriteTime);
            }
            this.getAllDirsFiles(dir.GetDirectories(), filesList);
        }
    }
    /// <summary>  
    /// 获取一个文件夹下的文件  
    /// </summary>  
    /// <param name="strDirName">目录名称</param>  
    /// <param name="filesList">文件列表HastTable</param>  
    private void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
    {
        foreach (FileInfo file in dir.GetFiles("*.*"))
        {
            filesList.Add(file.FullName, file.LastWriteTime);
        }
    }

    #endregion

    #region ZIP文件下载
    /// <summary>
    /// ZIP文件下载
    /// </summary>
    /// <param name="filePath"></param>
    public void DownLoadFile(string filePath)
    {
        //获取文件名（包括文件类型）
        string fileName = filePath.Remove(0, filePath.LastIndexOf("\\") + 1);

        Response.Clear();
        Response.BufferOutput = true;
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        if (File.Exists(filePath))
        {
            Response.WriteFile(filePath);//通知浏览器下载文件
        }
        Response.Flush();
    }
    #endregion

}
