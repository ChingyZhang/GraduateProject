// ===================================================================
// 文件： AC_AccountQuarter.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///AC_AccountQuarter数据实体类
    /// </summary>
    [Serializable]
    public class AC_AccountQuarter : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _beginmonth = 0;
        private int _endmonth = 0;
        private int _year = 0;
        private int _quarter = 0;
        private DateTime _begindate = new DateTime(1900, 1, 1);
        private DateTime _enddate = new DateTime(1900, 1, 1);
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public AC_AccountQuarter()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public AC_AccountQuarter(int id, string name, int beginmonth, int endmonth, int year, int quarter, DateTime begindate, DateTime enddate)
        {
            _id = id;
            _name = name;
            _beginmonth = beginmonth;
            _endmonth = endmonth;
            _year = year;
            _quarter = quarter;
            _begindate = begindate;
            _enddate = enddate;

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
        ///BeginMonth
        ///</summary>
        public int BeginMonth
        {
            get { return _beginmonth; }
            set { _beginmonth = value; }
        }

        ///<summary>
        ///EndMonth
        ///</summary>
        public int EndMonth
        {
            get { return _endmonth; }
            set { _endmonth = value; }
        }

        ///<summary>
        ///Year
        ///</summary>
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        ///<summary>
        ///Quarter
        ///</summary>
        public int Quarter
        {
            get { return _quarter; }
            set { _quarter = value; }
        }

        ///<summary>
        ///BeginDate
        ///</summary>
        public DateTime BeginDate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        ///<summary>
        ///EndDate
        ///</summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
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
            get { return "AC_AccountQuarter"; }
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
                    case "BeginMonth":
                        return _beginmonth.ToString();
                    case "EndMonth":
                        return _endmonth.ToString();
                    case "Year":
                        return _year.ToString();
                    case "Quarter":
                        return _quarter.ToString();
                    case "BeginDate":
                        return _begindate.ToString();
                    case "EndDate":
                        return _enddate.ToString();
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
                    case "BeginMonth":
                        int.TryParse(value, out _beginmonth);
                        break;
                    case "EndMonth":
                        int.TryParse(value, out _endmonth);
                        break;
                    case "Year":
                        int.TryParse(value, out _year);
                        break;
                    case "Quarter":
                        int.TryParse(value, out _quarter);
                        break;
                    case "BeginDate":
                        DateTime.TryParse(value, out _begindate);
                        break;
                    case "EndDate":
                        DateTime.TryParse(value, out _enddate);
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
