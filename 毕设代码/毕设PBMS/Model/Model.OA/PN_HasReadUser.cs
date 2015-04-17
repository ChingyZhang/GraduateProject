// ===================================================================
// 文件： PN_HasReadUser.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    ///PN_HasReadUser数据实体类
    /// </summary>
    [Serializable]
    public class PN_HasReadUser : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _notice = 0;
        private string _username = string.Empty;
        private DateTime _readtime = new DateTime(1900, 1, 1);
        private string _readinfo = string.Empty;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PN_HasReadUser()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PN_HasReadUser(int id, int notice, string username, DateTime readtime)
        {
            _id = id;
            _notice = notice;
            _username = username;
            _readtime = readtime;

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
        ///公告ID
        ///</summary>
        public int Notice
        {
            get { return _notice; }
            set { _notice = value; }
        }

        ///<summary>
        ///已阅读员工
        ///</summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        ///<summary>
        ///阅读时间
        ///</summary>
        public DateTime ReadTime
        {
            get { return _readtime; }
            set { _readtime = value; }
        }

        /// <summary>
        /// 阅读信息
        /// </summary>
        public string ReadInfo
        {
            get { return _readinfo; }
            set { _readinfo = value; }
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
            get { return "PN_HasReadUser"; }
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
                    case "Notice":
                        return _notice.ToString();
                    case "Username":
                        return _username.ToString();
                    case "ReadTime":
                        return _readtime.ToString();
                    case "ReadInfo":
                        return _readinfo.ToString();
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
                    case "Notice":
                        int.TryParse(value, out _notice);
                        break;
                    case "Username":
                        _username = value;
                        break;
                    case "ReadTime":
                        DateTime.TryParse(value, out _readtime);
                        break;
                    case "ReadInfo":
                        _readinfo = value;
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
