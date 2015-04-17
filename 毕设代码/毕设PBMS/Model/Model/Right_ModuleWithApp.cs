using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///Right_ModuleWithApp数据实体类
    /// </summary>
    [Serializable]
    public class Right_ModuleWithApp : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _superid = 0;
        private int _sortvalue = 0;
        private string _enableflag = string.Empty;
        private int _defaultico = 0;
        private string _ismenu = string.Empty;
        private string _ishttp = string.Empty;
        private string _pagename = string.Empty;
        private string _isanonymous = string.Empty;
        private int _insertstaff = 0;
        private DateTime _insertdate = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private DateTime _updatedate = new DateTime(1900, 1, 1);
        private string _remark = string.Empty;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Right_ModuleWithApp()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Right_ModuleWithApp(int id, string name, int superid, int sortvalue, string enableflag, int defaultico, string ismenu, string ishttp, string pagename, string isanonymous, int insertstaff, DateTime insertdate, int updatestaff, DateTime updatedate, string remark)
        {
            _id = id;
            _name = name;
            _superid = superid;
            _sortvalue = sortvalue;
            _enableflag = enableflag;
            _defaultico = defaultico;
            _ismenu = ismenu;
            _ishttp = ishttp;
            _pagename = pagename;
            _isanonymous = isanonymous;
            _insertstaff = insertstaff;
            _insertdate = insertdate;
            _updatestaff = updatestaff;
            _updatedate = updatedate;
            _remark = remark;

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
        ///Name
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///SuperID
        ///</summary>
        public int SuperID
        {
            get { return _superid; }
            set { _superid = value; }
        }

        ///<summary>
        ///SortValue
        ///</summary>
        public int SortValue
        {
            get { return _sortvalue; }
            set { _sortvalue = value; }
        }

        ///<summary>
        ///EnableFlag
        ///</summary>
        public string EnableFlag
        {
            get { return _enableflag; }
            set { _enableflag = value; }
        }

        ///<summary>
        ///DefaultIco
        ///</summary>
        public int DefaultIco
        {
            get { return _defaultico; }
            set { _defaultico = value; }
        }

        ///<summary>
        ///IsMenu
        ///</summary>
        public string IsMenu
        {
            get { return _ismenu; }
            set { _ismenu = value; }
        }

        ///<summary>
        ///IsHttp
        ///</summary>
        public string IsHttp
        {
            get { return _ishttp; }
            set { _ishttp = value; }
        }

        ///<summary>
        ///PageName
        ///</summary>
        public string PageName
        {
            get { return _pagename; }
            set { _pagename = value; }
        }

        ///<summary>
        ///IsAnonymous
        ///</summary>
        public string IsAnonymous
        {
            get { return _isanonymous; }
            set { _isanonymous = value; }
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
        ///InsertDate
        ///</summary>
        public DateTime InsertDate
        {
            get { return _insertdate; }
            set { _insertdate = value; }
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
        ///UpdateDate
        ///</summary>
        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set { _updatedate = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion

        public string ModelName
        {
            get { return "Right_ModuleWithApp"; }
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
                    case "Name":
                        return _name;
                    case "SuperID":
                        return _superid.ToString();
                    case "SortValue":
                        return _sortvalue.ToString();
                    case "EnableFlag":
                        return _enableflag;
                    case "DefaultIco":
                        return _defaultico.ToString();
                    case "IsMenu":
                        return _ismenu;
                    case "IsHttp":
                        return _ishttp;
                    case "PageName":
                        return _pagename;
                    case "IsAnonymous":
                        return _isanonymous;
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "InsertDate":
                        return _insertdate.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
                    case "UpdateDate":
                        return _updatedate.ToString();
                    case "Remark":
                        return _remark;
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
                    case "Name":
                        _name = value;
                        break;
                    case "SuperID":
                        int.TryParse(value, out _superid);
                        break;
                    case "SortValue":
                        int.TryParse(value, out _sortvalue);
                        break;
                    case "EnableFlag":
                        _enableflag = value;
                        break;
                    case "DefaultIco":
                        int.TryParse(value, out _defaultico);
                        break;
                    case "IsMenu":
                        _ismenu = value;
                        break;
                    case "IsHttp":
                        _ishttp = value;
                        break;
                    case "PageName":
                        _pagename = value;
                        break;
                    case "IsAnonymous":
                        _isanonymous = value;
                        break;
                    case "InsertStaff":
                        int.TryParse(value, out _insertstaff);
                        break;
                    case "InsertDate":
                        DateTime.TryParse(value, out _insertdate);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
                        break;
                    case "UpdateDate":
                        DateTime.TryParse(value, out _updatedate);
                        break;
                    case "Remark":
                        _remark = value;
                        break;

                }
            }
        }
        #endregion
    }
}
