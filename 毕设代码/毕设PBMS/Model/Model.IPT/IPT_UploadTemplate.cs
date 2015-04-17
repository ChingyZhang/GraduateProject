// ===================================================================
// 文件： IPT_UploadTemplate.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.IPT
{
    /// <summary>
    ///IPT_UploadTemplate数据实体类
    /// </summary>
    [Serializable]
    public class IPT_UploadTemplate : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _fullfilename = string.Empty;
        private string _shortfilename = string.Empty;
        private int _state = 0;
        private int _filetype = 0;
        private string _remark = string.Empty;
        private int _insertstaff = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private DateTime _importtime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private string _username = string.Empty;
        private int _clientid = 0;
        private string _clientname = string.Empty;
        private int _data01 = 0;
        private int _data02 = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public IPT_UploadTemplate()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public IPT_UploadTemplate(int id, string fullfilename, string shortfilename, int state, int filetype, string remark, int insertstaff, DateTime inserttime, DateTime importtime, int updatestaff, DateTime updatetime, string username, int clientid, string clientname, int data01, int data02)
        {
            _id = id;
            _fullfilename = fullfilename;
            _shortfilename = shortfilename;
            _state = state;
            _filetype = filetype;
            _remark = remark;
            _insertstaff = insertstaff;
            _inserttime = inserttime;
            _importtime = importtime;
            _updatestaff = updatestaff;
            _updatetime = updatetime;
            _username = username;
            _clientid = clientid;
            _clientname = clientname;
            _data01 = data01;
            _data02 = data02;

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
        ///FullFileName
        ///</summary>
        public string FullFileName
        {
            get { return _fullfilename; }
            set { _fullfilename = value; }
        }

        ///<summary>
        ///ShortFileName
        ///</summary>
        public string ShortFileName
        {
            get { return _shortfilename; }
            set { _shortfilename = value; }
        }

        ///<summary>
        ///State
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///FileType
        ///</summary>
        public int FileType
        {
            get { return _filetype; }
            set { _filetype = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        ///<summary>
        ///InsertStaff
        ///</summary>
        public int InsertStaff
        {
            get { return _insertstaff; }
            set { _insertstaff = value; }
        }

        ///<summary>
        ///InsertTime
        ///</summary>
        public DateTime InsertTime
        {
            get { return _inserttime; }
            set { _inserttime = value; }
        }

        ///<summary>
        ///ImportTime
        ///</summary>
        public DateTime ImportTime
        {
            get { return _importtime; }
            set { _importtime = value; }
        }

        ///<summary>
        ///UpdateStaff
        ///</summary>
        public int UpdateStaff
        {
            get { return _updatestaff; }
            set { _updatestaff = value; }
        }

        ///<summary>
        ///UpdateTime
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        ///<summary>
        ///UserName
        ///</summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        ///<summary>
        ///ClientID
        ///</summary>
        public int ClientID
        {
            get { return _clientid; }
            set { _clientid = value; }
        }

        ///<summary>
        ///ClientName
        ///</summary>
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }

        ///<summary>
        ///Data01
        ///</summary>
        public int Data01
        {
            get { return _data01; }
            set { _data01 = value; }
        }

        ///<summary>
        ///Data02
        ///</summary>
        public int Data02
        {
            get { return _data02; }
            set { _data02 = value; }
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
            get { return "IPT_UploadTemplate"; }
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
                    case "FullFileName":
                        return _fullfilename;
                    case "ShortFileName":
                        return _shortfilename;
                    case "State":
                        return _state.ToString();
                    case "FileType":
                        return _filetype.ToString();
                    case "Remark":
                        return _remark;
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "InsertTime":
                        return _inserttime.ToString();
                    case "ImportTime":
                        return _importtime.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
                    case "UpdateTime":
                        return _updatetime.ToString();
                    case "UserName":
                        return _username;
                    case "ClientID":
                        return _clientid.ToString();
                    case "ClientName":
                        return _clientname;
                    case "Data01":
                        return _data01.ToString();
                    case "Data02":
                        return _data02.ToString();
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
                    case "FullFileName":
                        _fullfilename = value;
                        break;
                    case "ShortFileName":
                        _shortfilename = value;
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "FileType":
                        int.TryParse(value, out _filetype);
                        break;
                    case "Remark":
                        _remark = value;
                        break;
                    case "InsertStaff":
                        int.TryParse(value, out _insertstaff);
                        break;
                    case "InsertTime":
                        DateTime.TryParse(value, out _inserttime);
                        break;
                    case "ImportTime":
                        DateTime.TryParse(value, out _importtime);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
                        break;
                    case "UserName":
                        _username = value;
                        break;
                    case "ClientID":
                        int.TryParse(value, out _clientid);
                        break;
                    case "ClientName":
                        _clientname = value;
                        break;
                    case "Data01":
                        int.TryParse(value, out _data01);
                        break;
                    case "Data02":
                        int.TryParse(value, out _data02);
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
