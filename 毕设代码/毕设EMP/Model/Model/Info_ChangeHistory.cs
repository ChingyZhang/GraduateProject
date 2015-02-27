// ===================================================================
// 文件： Info_ChangeHistory.cs
// 项目名称：
// 创建时间：2012/2/23
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///Info_ChangeHistory数据实体类
    /// </summary>
    [Serializable]
    public class Info_ChangeHistory : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private Guid _tableid = Guid.Empty;
        private Guid _fieldid = Guid.Empty;
        private int _infoid = 0;
        private string _oldvalue = string.Empty;
        private string _newvalue = string.Empty;
        private int _infotype = 0;
        private int _changestaff = 0;
        private DateTime _changetime = new DateTime(1900, 1, 1);
        private string _remark = string.Empty;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Info_ChangeHistory()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Info_ChangeHistory(int id, Guid tableid, Guid fieldid, int infoid, string oldvalue, string newvalue, int infotype, int changestaff, DateTime changetime, string remark)
        {
            _id = id;
            _tableid = tableid;
            _fieldid = fieldid;
            _infoid = infoid;
            _oldvalue = oldvalue;
            _newvalue = newvalue;
            _infotype = infotype;
            _changestaff = changestaff;
            _changetime = changetime;
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
        ///TableID
        ///</summary>
        public Guid TableID
        {
            get { return _tableid; }
            set { _tableid = value; }
        }

        ///<summary>
        ///FieldID
        ///</summary>
        public Guid FieldID
        {
            get { return _fieldid; }
            set { _fieldid = value; }
        }

        ///<summary>
        ///InfoID
        ///</summary>
        public int InfoID
        {
            get { return _infoid; }
            set { _infoid = value; }
        }

        ///<summary>
        ///OldValue
        ///</summary>
        public string OldValue
        {
            get { return _oldvalue; }
            set { _oldvalue = value; }
        }

        ///<summary>
        ///NewValue
        ///</summary>
        public string NewValue
        {
            get { return _newvalue; }
            set { _newvalue = value; }
        }

        ///<summary>
        ///InfoType
        ///</summary>
        public int InfoType
        {
            get { return _infotype; }
            set { _infotype = value; }
        }

        ///<summary>
        ///ChangeStaff
        ///</summary>
        public int ChangeStaff
        {
            get { return _changestaff; }
            set { _changestaff = value; }
        }

        ///<summary>
        ///ChangeTime
        ///</summary>
        public DateTime ChangeTime
        {
            get { return _changetime; }
            set { _changetime = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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
            get { return "Info_ChangeHistory"; }
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
                    case "TableID":
                        return _tableid.ToString();
                    case "FieldID":
                        return _fieldid.ToString();
                    case "InfoID":
                        return _infoid.ToString();
                    case "OldValue":
                        return _oldvalue;
                    case "NewValue":
                        return _newvalue;
                    case "InfoType":
                        return _infotype.ToString();
                    case "ChangeStaff":
                        return _changestaff.ToString();
                    case "ChangeTime":
                        return _changetime.ToString();
                    case "Remark":
                        return _remark;
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
                    case "TableID":
                        _tableid=new Guid(value);
                        break;
                    case "FieldID":
                        _fieldid = new Guid(value);
                        break;
                    case "InfoID":
                        int.TryParse(value, out _infoid);
                        break;
                    case "OldValue":
                        _oldvalue = value;
                        break;
                    case "NewValue":
                        _newvalue = value;
                        break;
                    case "InfoType":
                        int.TryParse(value, out _infotype);
                        break;
                    case "ChangeStaff":
                        int.TryParse(value, out _changestaff);
                        break;
                    case "ChangeTime":
                        DateTime.TryParse(value, out _changetime);
                        break;
                    case "Remark":
                        _remark = value;
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
