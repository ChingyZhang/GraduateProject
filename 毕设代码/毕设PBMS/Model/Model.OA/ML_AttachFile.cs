using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;


namespace MCSFramework.Model.OA
{
    /// <summary>
    /// ML_AttachFile 数据实体类
    /// </summary>
    [Serializable]
    public class ML_AttachFile : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _mailid = 0;
        private string _name = String.Empty;
        private int _size = 0;
        private string _visualpath = String.Empty;
        private string _extname = String.Empty;
        private string _uploaduser = String.Empty;
        private DateTime _uploadtime = new DateTime(1900, 1, 1);
        private string _isdelete = String.Empty;
        private Guid _guid = Guid.NewGuid();
        private NameValueCollection _extpropertys;
        #endregion


        #region 够造函数
        public ML_AttachFile() { }

        public ML_AttachFile(int id, int mailid, string name, int size, string visualpath, string extname, string uploaduser, DateTime uploadtime, string isdelete)
        {
            _id = id;
            _mailid = mailid;
            _name = name;
            _size = size;
            _visualpath = visualpath;
            _extname = extname;
            _uploaduser = uploaduser;
            _uploadtime = uploadtime;
            _isdelete = isdelete;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 邮件
        /// </summary>
        public int Mailid
        {
            get { return _mailid; }
            set { _mailid = value; }
        }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 大小
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string Visualpath
        {
            get { return _visualpath; }
            set { _visualpath = value; }
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extname
        {
            get { return _extname; }
            set { _extname = value; }
        }

        /// <summary>
        /// 上传人
        /// </summary>
        public string Uploaduser
        {
            get { return _uploaduser; }
            set { _uploaduser = value; }
        }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime Uploadtime
        {
            get { return _uploadtime; }
            set { _uploadtime = value; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string Isdelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }

        /// <summary>
        /// GUID
        /// </summary>
        public Guid GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public NameValueCollection ExtPropertys
        {
            get { return _extpropertys; }
            set { _extpropertys = value; }
        }
        #endregion


        public string ModelName
        {
            get { return ("ML_AttachFile"); }
        }


        #region 索引器访问
        public string this[string FieldName]
        {
            get
            {
                switch (FieldName)
                {
                    case "ID":
                        return _id.ToString();
                    case "MailID":
                        return _mailid.ToString();
                    case "Name":
                        return _name;
                    case "Size":
                        return _size.ToString();
                    case "VisualPath":
                        return _visualpath;
                    case "ExtName":
                        return _extname;
                    case "UploadUser":
                        return _uploaduser;
                    case "UploadTime":
                        return _uploadtime.ToShortDateString();
                    case "IsDelete":
                        return _isdelete;
                    case "GUID":
                        return _guid.ToString();
                    default:
                        if (_extpropertys == null)
                            return "";
                        else
                            return _extpropertys[FieldName];
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "MailID":
                        int.TryParse(value, out _mailid);
                        break;
                    case "Name":
                        _name = value;
                        break;
                    case "Size":
                        int.TryParse(value, out _size);
                        break;
                    case "VisualPath":
                        _visualpath = value;
                        break;
                    case "ExtName":
                        _extname = value;
                        break;
                    case "UploadUser":
                        _uploaduser = value;
                        break;
                    case "UploadTime":
                        DateTime.TryParse(value, out _uploadtime);
                        break;
                    case "IsDelete":
                        _isdelete = value;
                        break;
                    case "GUID":
                        _guid = new Guid(value);
                        break;
                    default:
                        if (_extpropertys == null)
                            _extpropertys = new NameValueCollection();
                        if (_extpropertys[FieldName] == null)
                            _extpropertys.Add(FieldName, value);
                        else
                            _extpropertys[FieldName] = value;
                        break;
                }
            }

        #endregion
        }
    }
}
