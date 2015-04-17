using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using System.IO;
using System.Web.Security;

public partial class Controls_UploadFile : System.Web.UI.UserControl
{
    #region Property
    public int RelateType
    {
        get
        {
            if (ViewState["RelateType"] == null)
                return 0;
            return (int)ViewState["RelateType"];
        }
        set
        {
            ViewState["RelateType"] = value;
            ObjectDataSource1.SelectParameters["RelateType"].DefaultValue = value.ToString();
        }
    }

    public int RelateID
    {
        get
        {

            if (ViewState["RelateID"] == null)
                return 0;
            return (int)ViewState["RelateID"];
        }
        set
        {
            ViewState["RelateID"] = value;
            ObjectDataSource1.SelectParameters["RelateID"].DefaultValue = value.ToString();
        }
    }

    public bool CanUpload
    {
        get
        {
            if (ViewState["CanUpload"] == null)
                return true;
            return (bool)ViewState["CanUpload"];
        }
        set
        {
            ViewState["CanUpload"] = value;
            tr_Upload.Visible = value;
            bt_SetFirstPicture.Visible = value;
        }
    }

    public bool CanPreview
    {
        get
        {
            if (ViewState["CanPreview"] == null)
                return true;
            return (bool)ViewState["CanPreview"];
        }
        set
        {
            ViewState["CanPreview"] = value;
        }
    }

    public bool CanDelete
    {
        get
        {
            if (ViewState["CanDelete"] == null)
                return true;
            return (bool)ViewState["CanDelete"];
        }
        set
        {
            ViewState["CanDelete"] = value;
            bt_Delete.Visible = value;
        }
    }

    public bool CanDownLoad
    {
        get
        {
            if (ViewState["CanDownLoad"] == null)
                return true;
            return (bool)ViewState["CanDownLoad"];
        }
        set
        {
            ViewState["CanDownLoad"] = value;
        }
    }

    public bool CanViewList
    {
        get
        {
            if (ViewState["CanViewList"] == null)
                return true;
            return (bool)ViewState["CanViewList"];
        }
        set
        {
            ViewState["CanViewList"] = value;
            tr_List2.Visible = value;
        }
    }

    public bool CanSetDefaultImage
    {
        get
        {
            if (ViewState["CanSetDefaultImage"] == null)
                return true;
            return (bool)ViewState["CanSetDefaultImage"];
        }
        set
        {
            ViewState["CanSetDefaultImage"] = value;
            bt_SetFirstPicture.Visible = value;
        }
    }
    public DateTime BeginTime
    {
        get
        {
            if (ViewState["BeginTime"] == null)
                return DateTime.Parse("1900-01-01");
            return (DateTime)ViewState["BeginTime"];
        }
        set
        {
            ViewState["BeginTime"] = value;
            ObjectDataSource1.SelectParameters["BeginTime"].DefaultValue = value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    public DateTime EndTime
    {
        get
        {
            if (ViewState["EndTime"] == null)
                return DateTime.Parse("2100-01-01");
            return (DateTime)ViewState["EndTime"];
        }
        set
        {
            ViewState["EndTime"] = value;
            ObjectDataSource1.SelectParameters["EndTime"].DefaultValue = value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

    public string ExtCondition
    {
        get
        {
            if (ViewState["ExtCondition"] == null)
                return "";
            return (string)ViewState["ExtCondition"];
        }
        set
        {
            ViewState["ExtCondition"] = value;
            ObjectDataSource1.SelectParameters["extcondition"].DefaultValue = value.ToString();
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindGrid()
    {
        //ObjectDataSource1.Select();
        lv_list.DataBind();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            if (RelateID == 0)
            {
                MessageBox.Show(this.Page, "请先关联要上传的对象！ RelateID can not null!");
                return;
            }

            #region 保存文件
            if (!FileUpload1.HasFile)
            {
                MessageBox.Show(this.Page, "请选择要上传的文件！");
                return;
            }
            int FileSize = (FileUpload1.PostedFile.ContentLength / 1024);

            if (!Roles.IsUserInRole("管理员") && FileSize > ConfigHelper.GetConfigInt("MaxAttachmentSize"))
            {
                MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                    "KB!当前上传文件大小为:" + FileSize.ToString() + "KB");
                return;
            }

            //判断文件格式
            string FileName = FileUpload1.PostedFile.FileName;
            FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
            FileName = FileName.Substring(0, FileName.LastIndexOf('.'));

            string PicType = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1).ToLowerInvariant();

            byte[] filedata;
            Stream filestream = FileUpload1.PostedFile.InputStream;

            #region 自动压缩上传的图片
            if (cb_AutoCompress.Checked && IsImage(PicType))
            {
                try
                {
                    System.Drawing.Image originalImage = System.Drawing.Image.FromStream(filestream);
                    filestream.Position = 0;

                    int width = originalImage.Width;

                    if (width > 1024 || PicType == "bmp")
                    {
                        if (width > 1024) width = 1024;

                        System.Drawing.Image thumbnailimage = ImageProcess.MakeThumbnail(originalImage, width, 0, "W");

                        MemoryStream thumbnailstream = new MemoryStream();
                        thumbnailimage.Save(thumbnailstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        thumbnailstream.Position = 0;
                        FileSize = (int)(thumbnailstream.Length / 1024);
                        PicType = "jpg";

                        filestream = thumbnailstream;
                    }
                }
                catch { }
            }
            #endregion

            filedata = new byte[filestream.Length];
            filestream.Read(filedata, 0, (int)filestream.Length);
            filestream.Close();
            #endregion

            ATMT_AttachmentBLL atm = new ATMT_AttachmentBLL();

            atm.Model.RelateType = RelateType;
            atm.Model.RelateID = RelateID;
            atm.Model.Name = tbx_Name.Text.Trim() == "" ? FileName : tbx_Name.Text.Trim();
            //atm.Model.Path = SaveFullPath;
            atm.Model.ExtName = PicType;
            atm.Model.FileSize = FileSize;
            atm.Model.Description = tbx_Description.Text;
            atm.Model.UploadUser = Session["UserName"].ToString();
            atm.Model.IsDelete = "N";

            #region 判断当前关联对象有没有其他图片附件
            if (IsImage(PicType))
            {
                IList<ATMT_Attachment> lists = ATMT_AttachmentBLL.GetAttachmentList(RelateType, RelateID, BeginTime, EndTime, "lower(ExtName) in ('bmp','jpg','gif','png')");

                //如果当前上传的图片是第一张图片，则默认设为首要图片
                if (lists.Count == 0) atm.Model["IsFirstPicture"] = "Y";
            }
            #endregion

            if (atm.Add(filedata) > 0)
            {
                MessageBox.Show(this.Page, "上传成功！");
            }
            else
            {
                MessageBox.Show(this.Page, "上传失败请重新上传！");
            }
            BindGrid();
        }
        catch (System.Exception err)
        {
            MessageBox.Show(this.Page, err.Message);
        }
    }

    /// <summary>
    /// 显示缩略图
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    protected string PreViewImagePath(int id)
    {
        ATMT_Attachment m = new ATMT_AttachmentBLL(id).Model;
        if (m == null) return "";

        if (IsImage(m.ExtName))
        {
            return "~/SubModule/DownloadAttachment.aspx?GUID=" + m.GUID.ToString() + "&PreViewImage=Y";
        }
        else
        {
            string filename = "~/Images/icon/" + m.ExtName + ".png";
            if (System.IO.File.Exists(Server.MapPath(filename)))
            {
                return filename;
            }
            else
            {
                return "~/Images/icon/other.png";
            }
        }
    }

    /// <summary>
    /// 判断是否是图片类型
    /// </summary>
    /// <param name="ExtName"></param>
    /// <returns></returns>
    protected bool IsImage(string ExtName)
    {
        switch (ExtName.ToLower())
        {
            case "jpg":
            case "bmp":
            case "gif":
            case "png":
                return true;
            default:
                return false;
        }
    }
    protected void bt_SetFirstPicture_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lv_list.Items.Count; i++)
        {
            ListViewItem item = lv_list.Items[i];
            if (((CheckBox)item.FindControl("cb_ck")).Checked)
            {
                int id = (int)lv_list.DataKeys[i].Value;

                ATMT_AttachmentBLL bll = new ATMT_AttachmentBLL(id);

                if (IsImage(bll.Model.ExtName))
                {

                    bll.Model["IsFirstPicture"] = "Y";
                    bll.Update();


                    IList<ATMT_Attachment> lists = ATMT_AttachmentBLL.GetAttachmentList(RelateType, RelateID, BeginTime, EndTime, "");
                    foreach (ATMT_Attachment att in lists)
                    {
                        if (att.ID != id && att["IsFirstPicture"] == "Y")
                        {
                            ATMT_AttachmentBLL bll2 = new ATMT_AttachmentBLL(att.ID);
                            bll2.Model["IsFirstPicture"] = "N";
                            bll2.Update();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this.Page, "对不起，您选中的附件不是图片文件！");
                    return;
                }
                break;
            }
        }
        lv_list.DataBind();
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lv_list.Items.Count; i++)
        {
            ListViewItem item = lv_list.Items[i];
            if (((CheckBox)item.FindControl("cb_ck")).Checked)
            {
                int id = (int)lv_list.DataKeys[i].Value;

                ATMT_AttachmentBLL att = new ATMT_AttachmentBLL(id);
                try
                {
                    File.Delete(Server.MapPath(att.Model.Path));
                }
                catch { }
                att.Delete();

            }

        }

        lv_list.DataBind();
    }
}
