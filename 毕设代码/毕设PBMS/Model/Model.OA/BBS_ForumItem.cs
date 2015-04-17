// ===================================================================
// 文件： BBS_ForumItem.cs
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
	///BBS_ForumItem数据实体类
	/// </summary>
	[Serializable]
	public class BBS_ForumItem : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _board = 0;
		private string _title = string.Empty;
		private string _content = string.Empty;
		private string _sender = string.Empty;
		private DateTime _sendtime = new DateTime(1900,1,1);
		private int _hittimes = 0;
		private int _replytimes = 0;
		private string _lastreplyer = string.Empty;
		private DateTime _lastreplytime = new DateTime(1900,1,1);
		private string _ipaddr = string.Empty;
		private string _isboardnotice = string.Empty;
		private string _ispublicnotice = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_ForumItem()
		{
		}
		
		///<summary>
		///
		///</summary>
		public BBS_ForumItem(int id, int board, string title, string content, string sender, DateTime sendtime, int hittimes, int replytimes, string lastreplyer, DateTime lastreplytime, string ipaddr, string isboardnotice, string ispublicnotice)
		{
			_id             = id;
			_board          = board;
			_title          = title;
			_content        = content;
			_sender         = sender;
			_sendtime       = sendtime;
			_hittimes       = hittimes;
			_replytimes     = replytimes;
			_lastreplyer    = lastreplyer;
			_lastreplytime  = lastreplytime;
			_ipaddr         = ipaddr;
			_isboardnotice  = isboardnotice;
			_ispublicnotice = ispublicnotice;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
		public int ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///所属板块
		///</summary>
		public int Board
		{
			get	{ return _board; }
			set	{ _board = value; }
		}

		///<summary>
		///标题
		///</summary>
		public string Title
		{
			get	{ return _title; }
			set	{ _title = value; }
		}

		///<summary>
		///内容正文
		///</summary>
		public string Content
		{
			get	{ return _content; }
			set	{ _content = value; }
		}

		///<summary>
		///发布者
		///</summary>
		public string Sender
		{
			get	{ return _sender; }
			set	{ _sender = value; }
		}

		///<summary>
		///发布时间
		///</summary>
		public DateTime SendTime
		{
			get	{ return _sendtime; }
			set	{ _sendtime = value; }
		}

		///<summary>
		///点击次数
		///</summary>
		public int HitTimes
		{
			get	{ return _hittimes; }
			set	{ _hittimes = value; }
		}

		///<summary>
		///回复次数
		///</summary>
		public int ReplyTimes
		{
			get	{ return _replytimes; }
			set	{ _replytimes = value; }
		}

		///<summary>
		///最后回复人
		///</summary>
		public string LastReplyer
		{
			get	{ return _lastreplyer; }
			set	{ _lastreplyer = value; }
		}

		///<summary>
		///最后回复时间
		///</summary>
		public DateTime LastReplyTime
		{
			get	{ return _lastreplytime; }
			set	{ _lastreplytime = value; }
		}

		///<summary>
		///发布者IP地址
		///</summary>
		public string IPAddr
		{
			get	{ return _ipaddr; }
			set	{ _ipaddr = value; }
		}

		///<summary>
		///是否板块公告
		///</summary>
		public string IsBoardNotice
		{
			get	{ return _isboardnotice; }
			set	{ _isboardnotice = value; }
		}

		///<summary>
		///是否公司公告
		///</summary>
		public string IsPublicNotice
		{
			get	{ return _ispublicnotice; }
			set	{ _ispublicnotice = value; }
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
            get { return "BBS_ForumItem"; }
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
					case "Board":
						return _board.ToString();
					case "Title":
						return _title;
					case "Content":
						return _content;
					case "Sender":
						return _sender;
					case "SendTime":
						return _sendtime.ToString();
					case "HitTimes":
						return _hittimes.ToString();
					case "ReplyTimes":
						return _replytimes.ToString();
					case "LastReplyer":
						return _lastreplyer;
					case "LastReplyTime":
						return _lastreplytime.ToString();
					case "IPAddr":
						return _ipaddr;
					case "IsBoardNotice":
						return _isboardnotice;
					case "IsPublicNotice":
						return _ispublicnotice;
					default:
						if (_extpropertys==null)
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
					case "Board":
						int.TryParse(value, out _board);
						break;
					case "Title":
						_title = value ;
						break;
					case "Content":
						_content = value ;
						break;
					case "Sender":
						_sender = value ;
						break;
					case "SendTime":
						DateTime.TryParse(value, out _sendtime);
						break;
					case "HitTimes":
						int.TryParse(value, out _hittimes);
						break;
					case "ReplyTimes":
						int.TryParse(value, out _replytimes);
						break;
					case "LastReplyer":
						_lastreplyer = value ;
						break;
					case "LastReplyTime":
						DateTime.TryParse(value, out _lastreplytime);
						break;
					case "IPAddr":
						_ipaddr = value ;
						break;
					case "IsBoardNotice":
						_isboardnotice = value ;
						break;
					case "IsPublicNotice":
						_ispublicnotice = value ;
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
