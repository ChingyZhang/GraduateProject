using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    /// SM_Msg数据实体类
    /// </summary>
    [Serializable]
    public class SM_Msg : IModel
    {
        #region 私有变量
        private int _id = 0;
        private string _sender = string.Empty;
        private string _content =string.Empty;
        private DateTime _sendtime;
        private int _type = 0;
        private string _isdelete = string.Empty;

        private NameValueCollection _extpropertys;
        #endregion

        public SM_Msg()
        {
        }

        public SM_Msg(int id, string sender, string content, DateTime sendtime, int type, string isdelete)
        {
            _id = id;
            _sender = sender;
            _content = content;
            _sendtime = sendtime;
            _type = type;
            _isdelete = isdelete;
        }

        #region 公共属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }

        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return _sendtime; }
            set { _sendtime = value; }
        }

        /// <summary>
        /// 短讯类别
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
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
            get { return "SM_Msg"; }
        }

        #region 索引器
        public string this[string FieldName]
        {
            get
            {
                switch (FieldName)
                {
                    case "ID":
                        return _id.ToString();
                    case "Sender":
                        return _sender;
                    case "Content":
                        return _content;
                    case "SendTime":
                        return _sendtime.ToShortDateString();
                    case "Type":
                        return _type.ToString();
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
                    case "Sender":
                        _sender = value;
                        break;
                    case "Content":
                        _content = value;
                        break;


                    case "SendTime":
                        DateTime.TryParse(value, out _sendtime);
                        break;

                    case "Type":
                        int.TryParse(value, out _type);
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
