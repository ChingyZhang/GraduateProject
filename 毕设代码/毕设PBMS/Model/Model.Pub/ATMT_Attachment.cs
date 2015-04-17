// ===================================================================
// 文件： ATMT_Attachment.cs
// 项目名称：
// 创建时间：2008-12-26
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///ATMT_Attachment数据实体类
    /// </summary>
    [Serializable]
    public class ATMT_Attachment : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _relatetype = 0;
        private int _relateid = 0;
        private string _name = string.Empty;
        private string _path = string.Empty;
        private string _extname = string.Empty;
        private int _filesize = 0;
        private string _description = string.Empty;
        private string _uploaduser = string.Empty;
        private DateTime _uploadtime = new DateTime(1900, 1, 1);
        private string _isdelete = string.Empty;
        private Guid _guid = Guid.NewGuid();

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ATMT_Attachment()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ATMT_Attachment(int id, int relatetype, int relateid, string name, string path, string extname, string description, string uploaduser, DateTime uploadtime, string isdelete)
        {
            _id = id;
            _relatetype = relatetype;
            _relateid = relateid;
            _name = name;
            _path = path;
            _extname = extname;
            _description = description;
            _uploaduser = uploaduser;
            _uploadtime = uploadtime;
            _isdelete = isdelete;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///RelateType
        ///</summary>
        public int RelateType
        {
            get { return _relatetype; }
            set { _relatetype = value; }
        }

        ///<summary>
        ///RelateID
        ///</summary>
        public int RelateID
        {
            get { return _relateid; }
            set { _relateid = value; }
        }

        ///<summary>
        ///Name
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///Path
        ///</summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        ///<summary>
        ///ExtName
        ///</summary>
        public string ExtName
        {
            get { return _extname; }
            set { _extname = value; }
        }

        /// <summary>
        /// FileSize
        /// </summary>
        public int FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }
        ///<summary>
        ///Description
        ///</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        ///<summary>
        ///UploadUser
        ///</summary>
        public string UploadUser
        {
            get { return _uploaduser; }
            set { _uploaduser = value; }
        }

        ///<summary>
        ///UploadTime
        ///</summary>
        public DateTime UploadTime
        {
            get { return _uploadtime; }
            set { _uploadtime = value; }
        }

        ///<summary>
        ///IsDelete
        ///</summary>
        public string IsDelete
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
        /// 扩展属性集合
        /// </summary>
        public NameValueCollection ExtPropertys
        {
            get { return _extpropertys; }
            set { _extpropertys = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "ATMT_Attachment"; }
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
                    case "RelateType":
                        return _relatetype.ToString();
                    case "RelateID":
                        return _relateid.ToString();
                    case "Name":
                        return _name;
                    case "Path":
                        return _path;
                    case "ExtName":
                        return _extname;
                    case "FileSize":
                        return _filesize.ToString();
                    case "Description":
                        return _description;
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
                    case "RelateType":
                        int.TryParse(value, out _relatetype);
                        break;
                    case "RelateID":
                        int.TryParse(value, out _relateid);
                        break;
                    case "Name":
                        _name = value;
                        break;
                    case "Path":
                        _path = value;
                        break;
                    case "ExtName":
                        _extname = value;
                        break;
                    case "FileSize":
                        int.TryParse(value, out _filesize);
                        break;
                    case "Description":
                        _description = value;
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
        }
        #endregion
    }
}
