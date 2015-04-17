// ===================================================================
// 文件： ML_Mail.cs
// 项目名称：
// 创建时间：2009/3/2
// 作者:	  chen li
// ===================================================================
using System;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    ///ML_Mail数据实体类
    /// </summary>
    [Serializable]
    public class ML_Mail : IModel
    {

        #region 私有变量定义
        private int _id = 0;
        private int _mailType = 0;
        private string _receiverStr = "";
        private DateTime _sendTime = new DateTime(1900, 1, 1);
        private int _sendTags = 0;
        private string _receiver = "";
        private string _sender = "";
        private string _subject = "";
        private string _content = "";
        private string _ccToAddr = "";
        private string _bccToAddr = "";
        private string _isRead = "N";
        private string _isDelete = "N";
        private int _type = 0;
        private int _size = 0;
        private int _folder = 0;
        private int _importance = 0;
        private string _extPropertys = "";
        #endregion

        #region  构造函数
        /// <summary>
        /// 
        /// </summary>
        public ML_Mail()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public ML_Mail(int id, int mailType, string receiverStr, string sender, DateTime sendTime, int sendTags, string receiver, string subject,
          string content, string ccToAddr, string bccToAddr, string isRead, string isDelete, int type, int size, int importance, int folder, string extPropertys)
        {
            _id = id;
            _mailType = mailType;
            _receiverStr = receiverStr;
            _sender = sender;
            _sendTime = sendTime;
            _sendTags = sendTags;
            _receiver = receiver;
            _subject = subject;
            _content = content;
            _ccToAddr = ccToAddr;
            _bccToAddr = bccToAddr;
            _isRead = isRead;
            _isDelete = isDelete;
            _type = type;
            _size = size;
            _importance = importance;
            _folder = folder;
            _extPropertys = extPropertys;

        }
        #endregion

        #region  公共属性
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int MailType
        {
            //邮箱类型
            get { return _mailType; }
            set { _mailType = value; }
        }

        public string ReceiverStr
        {
            //收件人群
            get { return _receiverStr; }
            set { _receiverStr = value; }
        }

        public string Sender
        {
            //发件人
            get { return _sender; }
            set { _sender = value; }
        }


        public DateTime SendTime
        {
            //发送时间
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        public int SendTags
        {
            //发送标签
            get { return _sendTags; }
            set { _sendTags = value; }
        }


        public string Receiver
        {
            //收件人
            get { return _receiver; }
            set { _receiver = value; }
        }

        public string Subject
        {
            //主题
            get { return _subject; }
            set { _subject = value; }
        }

        public string Content
        {
            //内容
            get { return _content; }
            set { _content = value; }
        }

        public string CcToAddr
        {
            //抄送地址
            get { return _ccToAddr; }
            set { _ccToAddr = value; }
        }

        public string BccToAddr
        {
            //秘送地址
            get { return _bccToAddr; }
            set { _bccToAddr = value; }
        }

        public string IsRead
        {
            //是否已读 N未读 Y已读
            get { return _isRead; }
            set { _isRead = value; }
        }

        public string IsDelete
        {
            //是否删除 N未读 Y已读
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        public int Type
        {
            //邮件类型
            get { return _type; }
            set { _type = value; }
        }

        public int Size
        {
            //邮件大小
            get { return _size; }
            set { _size = value; }
        }

        public int Importance
        {
            //重要等级
            get { return _importance; }
            set { _importance = value; }
        }

        public int Folder
        {
            //文件夹
            get { return _folder; }
            set { _folder = value; }
        }

        public string ExtPropertys
        {
            //扩展属性
            get { return _extPropertys; }
            set { _extPropertys = value; }
        }
        #endregion

        #region IModel 成员

        public string ModelName
        {
            get { return "ML_Mail"; }
        }

        /// <summary>
        /// 索引器访问
        /// </summary>
        /// <param name="FieldName"></param>
        /// <returns></returns>

        public string this[string FieldName]
        {
            get
            {
                switch (FieldName)
                {
                    case "ID":
                        return _id.ToString();
                    case "MailType":
                        return _mailType.ToString();
                    case "ReceiverStr":
                        return _receiverStr;
                    case "Sender":
                        return _sender;
                    case "SendTime":
                        return _sendTime.ToString();
                    case "SendTags":
                        return _sendTags.ToString();
                    case "Receiver":
                        return _receiver;
                    case "Subject":
                        return _subject;
                    case "Content":
                        return _content;
                    case "CcToAddr":
                        return _ccToAddr;
                    case "BccToAddr":
                        return _bccToAddr;
                    case "IsRead":
                        return _isRead;
                    case "IsDelete":
                        return _isDelete;
                    case "Type":
                        return _type.ToString();
                    case "Size":
                        return _size.ToString();
                    case "Importance":
                        return _importance.ToString();
                    case "Folder":
                        return _folder.ToString();
                    case "ExtPropertys":
                        return _extPropertys;
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
                    case "MailType":
                        int.TryParse(value, out _mailType);
                        break;
                    case "ReceiverStr":
                        _receiverStr = value;
                        break;
                    case "Sender":
                        _sender = value;
                        break;
                    case "SendTime":
                        DateTime.TryParse(value, out _sendTime);
                        break;
                    case "SendTags":
                        int.TryParse(value, out _sendTags);
                        break;
                    case "Receiver":
                        _receiver = value;
                        break;
                    case "Subject":
                        _subject = value;
                        break;
                    case "Content":
                        _content = value;
                        break;
                    case "CcToAddr":
                        _ccToAddr = value;
                        break;
                    case "BccToAddr":
                        _bccToAddr = value;
                        break;
                    case "IsRead":
                        _isRead = value;
                        break;
                    case "IsDelete":
                        _isDelete = value;
                        break;
                    case "MailSize":
                        int.TryParse(value, out _size);
                        break;
                    case "type":
                        int.TryParse(value, out _type);
                        break;
                    case "Importance":
                        int.TryParse(value, out _importance);
                        break;
                    case "Folder":
                        int.TryParse(value, out _folder);
                        break;
                    case "ExtPropertys":
                        _extPropertys = value;
                        break;
                }
            }

        }
        #endregion
    }
}
