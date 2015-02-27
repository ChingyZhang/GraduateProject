// ===================================================================
// 文件： SVM_DownloadTemplate.cs
// 项目名称：
// 创建时间：2012/6/19
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
    /// <summary>
    ///SVM_DownloadTemplate数据实体类
    /// </summary>
    [Serializable]
    public class SVM_DownloadTemplate : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _accountmonth = 0;
        private string _path = string.Empty;
        private int _state = 0;
        private int _isopponent = 0;
        private string _productgifts = string.Empty;
        private string _testers = string.Empty;
        private string _gifts = string.Empty;
        private string _remark = string.Empty;
        private int _insertstaff = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _downstaff = 0;
        private DateTime _downtime = new DateTime(1900, 1, 1);

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_DownloadTemplate()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SVM_DownloadTemplate(int id, string name, int accountmonth, string path, int state, int isopponent, string productgifts, string testers, string gifts, string remark, int insertstaff, DateTime inserttime, int downstaff, DateTime downtime)
        {
            _id = id;
            _name = name;
            _accountmonth = accountmonth;
            _path = path;
            _state = state;
            _isopponent = isopponent;
            _productgifts = productgifts;
            _testers = testers;
            _gifts = gifts;
            _remark = remark;
            _insertstaff = insertstaff;
            _inserttime = inserttime;
            _downstaff = downstaff;
            _downtime = downtime;

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
        ///AccountMonth
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
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
        ///State
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///IsOpponent
        ///</summary>
        public int IsOpponent
        {
            get { return _isopponent; }
            set { _isopponent = value; }
        }

        ///<summary>
        ///ProductGifts
        ///</summary>
        public string ProductGifts
        {
            get { return _productgifts; }
            set { _productgifts = value; }
        }

        ///<summary>
        ///Testers
        ///</summary>
        public string Testers
        {
            get { return _testers; }
            set { _testers = value; }
        }

        ///<summary>
        ///Gifts
        ///</summary>
        public string Gifts
        {
            get { return _gifts; }
            set { _gifts = value; }
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
        ///DownStaff
        ///</summary>
        public int DownStaff
        {
            get { return _downstaff; }
            set { _downstaff = value; }
        }

        ///<summary>
        ///DownTime
        ///</summary>
        public DateTime DownTime
        {
            get { return _downtime; }
            set { _downtime = value; }
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
            get { return "SVM_DownloadTemplate"; }
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
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "Path":
                        return _path;
                    case "State":
                        return _state.ToString();
                    case "IsOpponent":
                        return _isopponent.ToString();
                    case "ProductGifts":
                        return _productgifts;
                    case "Testers":
                        return _testers;
                    case "Gifts":
                        return _gifts;
                    case "Remark":
                        return _remark;
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "InsertTime":
                        return _inserttime.ToString();
                    case "DownStaff":
                        return _downstaff.ToString();
                    case "DownTime":
                        return _downtime.ToString();
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
                    case "Name":
                        _name = value;
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "Path":
                        _path = value;
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "IsOpponent":
                        int.TryParse(value, out _isopponent);
                        break;
                    case "ProductGifts":
                        _productgifts = value;
                        break;
                    case "Testers":
                        _testers = value;
                        break;
                    case "Gifts":
                        _gifts = value;
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
                    case "DownStaff":
                        int.TryParse(value, out _downstaff);
                        break;
                    case "DownTime":
                        DateTime.TryParse(value, out _downtime);
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
