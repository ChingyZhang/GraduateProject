using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;


namespace MCSFramework.Model.OA
{
    /// <summary>
    ///SM_Receiver数据实体类
    /// </summary>
    [Serializable]
    public class SM_Receiver : IModel
    {
        #region 私有变量
        private int _id = 0;
        private int _msgid = 0;
        private string _receiver = string.Empty;
        private string _mobileno = string.Empty;
        private string _isread = string.Empty;
        private string _isdelete = string.Empty;

        private NameValueCollection _extpropertys;

        #endregion

        public SM_Receiver()
        {
        }

        public SM_Receiver(int id,int msgid, string receiver, string mobileno, string isread, string isdelete)
        {
            _id = id;
            _msgid = msgid;
            _receiver = receiver;
            _mobileno = mobileno;
            _isread = isread;
            _isdelete = isdelete;
        }
        #region 公共属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 短讯ID
        /// </summary>
        public int MsgID
        {
            get { return _msgid; }
            set { _msgid = value; }
        }

        /// <summary>
        /// 接受者
        /// </summary>
        public string Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo
        {
            get { return _mobileno; }
            set { _mobileno = value; }
        }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public string IsRead
        {
            get { return _isread; }
            set { _isread = value; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
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
            get { return "SM_Receiver"; }
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
                    case "MsgID":
                        return _msgid.ToString();
                    case "Receiver":
                        return _receiver;
                    case "MobileNo":
                        return _mobileno;
                    case "IsRead":
                        return _isread.ToString();
                    case "IsDelete":
                        return _isdelete;
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
                    case "MsgID":
                        int.TryParse(value, out _msgid);
                        break;
                    case "Receiver":
                        _receiver = value;
                        break;
                    case "MobileNo":
                        _mobileno = value;
                        break;
                    case "IsRead":
                        _isread = value;
                        break;
                    case "IsDelete":
                        _isdelete = value;
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
