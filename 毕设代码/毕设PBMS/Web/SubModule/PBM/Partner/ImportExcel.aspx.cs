using MCSFramework.BLL.IPT;
using MCSFramework.Common;
using MCSFramework.Model.IPT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Product_ImportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
        IList<IPT_UploadTemplate> _listUploadTemplate = IPT_UploadTemplateBLL.GetModelList(" ClientID=" + Session["OwnerClient"].ToString());
        if (_listUploadTemplate == null || _listUploadTemplate.Count == 0)
        {
            div_gift.Visible = false;
            return;
        }
        int _maxID = _listUploadTemplate.Max(m => m.ID);
        IPT_UploadTemplate _uploadTemplate = _listUploadTemplate.First(T => T.ID == _maxID);
        if (_uploadTemplate.State == 1)
        {
            bt_UploadExcel.Enabled = false;
            div_gift.Visible = false;
            return;
        }
        else
        {
            bt_UploadExcel.Enabled = false;
            IList<IPT_UploadTemplateMessage> _listUploadTemplateMessage = IPT_UploadTemplateMessageBLL.GetModelList(" TemplateID=" + _maxID.ToString()).ToList();
            string _reamrk = string.Empty;
            foreach (var _uploadTemplateMessage in _listUploadTemplateMessage)
            {
                _reamrk += _uploadTemplateMessage.Content;
            }
            _reamrk = _reamrk.Replace("\r\n", "<br />");
            Response.Write(_reamrk);

            div_gift.InnerHtml = _reamrk;
            div_gift.Visible = true;
        }
    }

    protected void bt_DownloadTemplate_Click(object sender, EventArgs e)
    {
        string path = ConfigHelper.GetConfigString("TdpModel");
        DownLoadFile(path);
    }

    protected void bt_UploadExcel_Click(object sender, EventArgs e)
    {

        #region 保存文件
        if (!FileUpload1.HasFile)
        {
            MessageBox.Show(this.Page, "请选择要上传的文件！");
            return;
        }
        if (ConfigHelper.GetConfigInt("ExcelMaxAttachmentSize") > 0)
        {
            int _fileSize = (FileUpload1.PostedFile.ContentLength / 1024);

            if (_fileSize > ConfigHelper.GetConfigInt("ExcelMaxAttachmentSize"))
            {
                MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                    "KB!当前上传文件大小为:" + _fileSize.ToString() + "KB");
                return;
            }
        }
        //判断文件格式
        string _fileName = FileUpload1.PostedFile.FileName;
        _fileName = _fileName.Substring(_fileName.LastIndexOf('\\') + 1);
        string _fileExtName = _fileName.Substring(_fileName.LastIndexOf('.') + 1).ToLowerInvariant();
        if (_fileExtName != "xls" && _fileExtName != "xlsx" && _fileExtName != "csv")
        {
            MessageBox.Show(this, "对不起，必须上传Excel文件！");
            return;
        }
        //_fileName = _fileName.Substring(0, _fileName.LastIndexOf('.'));
        string _path = GetAttachmentDirectory() + "ImportExcel\\Upload\\" + Session["UserName"].ToString() + "\\";//存储文件夹
        if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
        _path += Guid.NewGuid().ToString() + "." + _fileExtName;//取唯一命名值
        try { FileUpload1.SaveAs(_path); }
        catch
        {
            MessageBox.Show(this, "上传Excel文件失败！");
            return;
        }
        #endregion
        IPT_UploadTemplateBLL _bllUploadTemplate = new IPT_UploadTemplateBLL();
        _bllUploadTemplate.Model.FullFileName = _path;
        _bllUploadTemplate.Model.ShortFileName = _fileName;
        _bllUploadTemplate.Model.State = 1;
        _bllUploadTemplate.Model.FileType = 1;
        _bllUploadTemplate.Model.InsertStaff = (int)Session["UserID"];
        _bllUploadTemplate.Model.UserName = Session["UserName"].ToString();
        _bllUploadTemplate.Model.ClientID = (int)Session["OwnerClient"];
        _bllUploadTemplate.Model.ClientName = Session["OwnerClientName"] != null ? Session["OwnerClientName"].ToString() : string.Empty;
        _bllUploadTemplate.Add();

        div_gift.Visible = true;
        div_gift.InnerHtml = "文件上传成功，若文件导入成功会刷新通知栏";

    }


    #region Excel文件下载

    #region 获取附件文件夹路径
    /// <summary>
    /// 获取附件文件夹路径
    /// </summary>
    /// <returns></returns>
    public string GetAttachmentDirectory()
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        //path += "ReportsDownload\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path;
    }
    #endregion

    /// <summary>
    /// 对提供下载的附件名进行编码
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string FileNameEncode(string fileName)
    {
        bool isFireFox = false;
        if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
        {
            isFireFox = true;
        }
        if (isFireFox == true)
        {
            //文件名前后加双引号
            fileName = "\"" + fileName + "\"";
        }
        else
        {
            //非火狐浏览器对中文文件名进行HTML编码
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
        }
        return fileName;
    }

    private void DownLoadFile(string filePath)
    {
        string _fileName = "";
        string _fileExt = string.Empty;//文件后缀名
        if (filePath.IndexOf('.') > 0) _fileExt = filePath.Substring(filePath.LastIndexOf('.') + 1, filePath.Length - filePath.LastIndexOf('.') - 1);
        try
        {
            _fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1, filePath.Length - filePath.LastIndexOf("\\") - 1);
            if (!string.IsNullOrEmpty(_fileExt)) _fileName = _fileName.Substring(0, _fileName.LastIndexOf(_fileExt) - 1);
        }
        catch { _fileName = ""; }
        if (string.IsNullOrEmpty(_fileName)) _fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
        _fileName += string.Format(".{0}", _fileExt);

        _fileName = FileNameEncode(_fileName);
        Response.Clear();
        //Response.BufferOutput = true;
        Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileName);
        Response.ContentType = "application/ms-excel";
        if (File.Exists(filePath)) Response.WriteFile(filePath);//通知浏览器下载文件
        MessageBox.Show(this, "文件不存在");
        Response.Flush();
        Response.End();
    }

    #endregion
}