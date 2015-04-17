using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class ckeditor_ImageBrowser : System.Web.UI.Page
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

        ResizeMessage.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "FocusImageList", "window.setTimeout(\"document.getElementById('" + ImageList.ClientID + "').focus();\", 1);", true);
    }

    protected void BindDirectoryList()
    {
        List<DateTime> lists = ATMT_AttachmentBLL.GetCKEditUploadDate(Session["UserName"].ToString(), true);
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

        IList<ATMT_Attachment> atts = ATMT_AttachmentBLL.GetCKEditAttachmentList(uploaddate, Session["UserName"].ToString(), true);

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
        bt_DeleteImage.Enabled = (ImageList.Items.Count != 0);
        bt_ResizeImage.Enabled = (ImageList.Items.Count != 0);
        ResizeWidth.Enabled = (ImageList.Items.Count != 0);
        ResizeHeight.Enabled = (ImageList.Items.Count != 0);

        if (ImageList.Items.Count == 0)
        {
            Image1.ImageUrl = "";
            ResizeWidth.Text = "";
            ResizeHeight.Text = "";
            return;
        }

        Image1.ImageUrl = string.Format(AttachmentDownloadURL, ImageList.SelectedValue);
        ATMT_AttachmentBLL att = new ATMT_AttachmentBLL(new Guid(ImageList.SelectedValue));
        if (att.Model != null && att.GetData() != null)
        {
            ImageMedia img = ImageMedia.Create(att.GetData());
            ResizeWidth.Text = img.Width.ToString();
            ResizeHeight.Text = img.Height.ToString();
            ImageAspectRatio.Value = "" + img.Width / (float)img.Height;

            int pos = ImageList.SelectedItem.Text.LastIndexOf('.');
            if (pos == -1) return;

            bt_OkButton.OnClientClick = "window.top.opener.CKEDITOR.dialog.getCurrent().setValueOf('info', 'txtUrl', encodeURI('" + Page.ResolveUrl(Image1.ImageUrl) + "')); window.top.close(); window.top.opener.focus();";
        }
    }

    protected void DeleteImage(object sender, EventArgs e)
    {
        new ATMT_AttachmentBLL(new Guid(ImageList.SelectedValue)).Delete();

        BindImageList();
        SelectImage(null, null);
    }

    protected void ResizeWidthChanged(object sender, EventArgs e)
    {
        float ratio = Util.Parse<float>(ImageAspectRatio.Value);
        int width = Util.Parse<int>(ResizeWidth.Text);

        ResizeHeight.Text = "" + (int)(width / ratio);
    }

    protected void ResizeHeightChanged(object sender, EventArgs e)
    {
        float ratio = Util.Parse<float>(ImageAspectRatio.Value);
        int height = Util.Parse<int>(ResizeHeight.Text);

        ResizeWidth.Text = "" + (int)(height * ratio);
    }

    protected void ResizeImage(object sender, EventArgs e)
    {
        ATMT_AttachmentBLL att = new ATMT_AttachmentBLL(new Guid(ImageList.SelectedValue));
        int width = Util.Parse<int>(ResizeWidth.Text);
        int height = Util.Parse<int>(ResizeHeight.Text);

        ImageMedia img = ImageMedia.Create(att.GetData());
        img.Resize(width, height);

        att.UploadFileData(img.ToByteArray());

        ResizeMessage.Text = "Image successfully resized!";
        SelectImage(null, null);
    }

    protected void Upload(object sender, EventArgs e)
    {
        if (IsImage(bt_UploadedImageFile.FileName))
        {
            string filename = bt_UploadedImageFile.FileName;

            byte[] data = ImageMedia.Create(bt_UploadedImageFile.FileBytes).Resize(960, null).ToByteArray();

            #region 写入新附件
            ATMT_AttachmentBLL atm = new ATMT_AttachmentBLL();

            atm.Model.RelateType = 75;
            atm.Model.Name = filename;
            atm.Model.ExtName = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            atm.Model.FileSize = data.Length / 1024;
            atm.Model.UploadUser = Session["UserName"].ToString();
            atm.Model.IsDelete = "N";
            int atm_id = atm.Add(data);
            #endregion

            if (atm_id > 0)
            {
                ddl_DirectoryList.SelectedIndex = 0;
                BindImageList();

                ImageList.SelectedValue = new ATMT_AttachmentBLL(atm_id).Model.GUID.ToString();
                SelectImage(null, null);
            }
        }
    }

    protected void Clear(object sender, EventArgs e)
    {
        Session.Remove("viewstate");
    }

    //util methods
    private bool IsImage(string file)
    {
        return file.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
            file.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
            file.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
            file.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase);
    }

    public static class Util
    {
        public static T Parse<T>(object value)
        {
            try { return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value.ToString()); }
            catch (Exception) { return default(T); }
        }
    }

    #region ImageMedia
    public class ImageMedia
    {
        private System.Drawing.Image _source;
        private byte[] _data;

        public int Width { get { return _source.Width; } }
        public int Height { get { return _source.Height; } }

        /// <summary>
        /// Resizes image to fit within the specified with and height, aspect ratio is maintained.
        /// </summary>
        /// <param name="width">The maximum width the image has to fit within, set to null to not restrict width.</param>
        /// <param name="height">The maximum height the image has to fit within, set to null to not restrict height.</param>
        /// <returns>A reference to this object to allow chaining operations together.</returns>
        public ImageMedia Resize(int? width, int? height)
        {
            if (width == null && height == null || width == 0 && height == 0)
                return this;

            int w = (width == null || width == 0) ? _source.Width : width.Value;
            int h = (height == null || height == 0) ? _source.Height : height.Value;
            float desiredRatio = (float)w / h;
            float scale, posx, posy;
            float ratio = (float)_source.Width / _source.Height;

            if (_source.Width < w && _source.Height < h)
            {
                scale = 1f;
                posy = (h - _source.Height) / 2f;
                posx = (w - _source.Width) / 2f;
            }
            else if (ratio > desiredRatio)
            {
                scale = (float)w / _source.Width;
                posy = (h - (_source.Height * scale)) / 2f;
                posx = 0f;
            }
            else
            {
                scale = (float)h / _source.Height;
                posx = (w - (_source.Width * scale)) / 2f;
                posy = 0f;
            }

            //TODO: I have commented out the next statement.  by Sun Ji Lei
            //if (!background.HasValue)
            {
                w = (int)(_source.Width * scale);
                h = (int)(_source.Height * scale);
                posx = 0f;
                posy = 0f;
            }

            System.Drawing.Image resizedImage = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(resizedImage);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(_source, posx, posy, _source.Width * scale, _source.Height * scale);

            foreach (PropertyItem item in _source.PropertyItems)
                resizedImage.SetPropertyItem(item);

            _source = resizedImage;
            _data = null;
            return this;
        }

        /// <summary>
        /// Crops the image in the middle resizing it down to fit as much of the image as possible. Note, image is not enlarged to fit desired width and height.
        /// </summary>
        /// <param name="width">The desired width to crop the image to, set to null to not perform horizontal cropping.</param>
        /// <param name="height">The desired height to crop the image to, set to null to not perform vertical cropping.</param>
        /// <returns>A reference to this object to allow chaining operations together.</returns>
        public ImageMedia Crop(int? width, int? height)
        {
            if (width == null && height == null)
                return this;

            int w = (width == null) ? _source.Width : width.Value;
            int h = (height == null) ? _source.Height : height.Value;

            float scaleX = (float)w / (float)_source.Width;
            float scaleY = (float)h / (float)_source.Height;
            float scale = Math.Max(scaleX, scaleY);
            int marginX = -((int)(_source.Width * scale - w)) / 2;
            int marginY = -((int)(_source.Height * scale - h)) / 2;

            System.Drawing.Image resizedImage = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(resizedImage);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, w, h);
            g.DrawImage(_source, marginX, marginY, _source.Width * scale, _source.Height * scale);

            foreach (PropertyItem item in _source.PropertyItems)
                resizedImage.SetPropertyItem(item);

            _source = resizedImage;
            _data = null;
            return this;
        }

        /// <summary>
        /// Returns the binary data of the image in jpg format.
        /// </summary>
        public byte[] ToByteArray()
        {
            return ToByteArray("jpg");
        }

        /// <summary>
        /// Returns the binary data of the image in the specifed format.
        /// </summary>
        /// <param name="format">The format of the bitmap image specified as "gif", "jpg", "png", "bmp", "jxr" or "tiff".</param>
        public byte[] ToByteArray(string format)
        {
            //if image not altered return orig data
            if (_data != null)
                return _data;

            using (MemoryStream resized = new MemoryStream())
            {
                _source.Save(resized, GetEncoder(format));
                return resized.ToArray();
            }
        }

        private ImageFormat GetEncoder(string format)
        {
            if (format == "jpg")
                return ImageFormat.Jpeg;
            if (format == "png")
                return ImageFormat.Png;
            if (format == "gif")
                return ImageFormat.Gif;
            if (format == "tiff")
                return ImageFormat.Tiff;
            if (format == "bmp")
                return ImageFormat.Bmp;
            throw new Exception("Unrecognised image format: " + format);
        }

        /// <summary>
        /// Create a image object that can be interacted with from binary data.
        /// </summary>
        /// <exception cref="System.ArgumentException">If the supplied binary data is not a valid image, the ArgumentException will be thrown.</exception>
        public static ImageMedia Create(byte[] data)
        {
            ImageMedia result = new ImageMedia();
            result._source = System.Drawing.Image.FromStream(new MemoryStream(data));
            result._data = data;
            return result;
        }
    }
    #endregion
}