// ===================================================================
// 文件： BBS_ForumAttachment.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    ///BBS_ForumAttachment数据实体类
    /// </summary>
    [Serializable]
    public class BBS_ForumAttachment : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _item = 0;
        private int _reply = 0;
        private string _name = string.Empty;
        private string _path = string.Empty;
        private string _extname = string.Empty;
        private int _filesize = 0;
        private DateTime _uploadtime = new DateTime(1900, 1, 1);
        private Guid _guid = Guid.NewGuid();
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public BBS_ForumAttachment()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public BBS_ForumAttachment(int id, int item, int reply, string name, string path, string extname, int filesize, DateTime uploadtime)
        {
            _id = id;
            _item = item;
            _reply = reply;
            _name = name;
            _path = path;
            _extname = extname;
            _filesize = filesize;
            _uploadtime = uploadtime;

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
        ///所属帖子
        ///</summary>
        public int ItemID
        {
            get { return _item; }
            set { _item = value; }
        }

        ///<summary>
        ///所属回复
        ///</summary>
        public int Reply
        {
            get { return _reply; }
            set { _reply = value; }
        }

        ///<summary>
        ///文件名称
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///文件路径
        ///</summary>
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        ///<summary>
        ///扩展名
        ///</summary>
        public string ExtName
        {
            get { return _extname; }
            set { _extname = value; }
        }

        ///<summary>
        ///文件大小
        ///</summary>
        public int FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }

        ///<summary>
        ///上传时间
        ///</summary>
        public DateTime UploadTime
        {
            get { return _uploadtime; }
            set { _uploadtime = value; }
        }

        public Guid GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "BBS_ForumAttachment"; }
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
                    case "Item":
                        return _item.ToString();
                    case "Reply":
                        return _reply.ToString();
                    case "Name":
                        return _name;
                    case "Path":
                        return _path;
                    case "ExtName":
                        return _extname;
                    case "FileSize":
                        return _filesize.ToString();
                    case "UploadTime":
                        return _uploadtime.ToString();
                    case "GUID":
                        return _guid.ToString();
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "Item":
                        int.TryParse(value, out _item);
                        break;
                    case "Reply":
                        int.TryParse(value, out _reply);
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
                    case "UploadTime":
                        DateTime.TryParse(value, out _uploadtime);
                        break;
                    case "GUID":
                        _guid = new Guid(value);
                        break;
                }
            }
        }
        #endregion
    }
}
