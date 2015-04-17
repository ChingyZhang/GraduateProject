// ===================================================================
// 文件： BBS_ForumReply.cs
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
	///BBS_ForumReply数据实体类
	/// </summary>
	[Serializable]
	public class BBS_ForumReply : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _item = 0;
		private string _title = string.Empty;
		private string _content = string.Empty;
		private string _replyer = string.Empty;
		private DateTime _replytime = new DateTime(1900,1,1);
		private string _ipaddress = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_ForumReply()
		{
		}
		
		///<summary>
		///
		///</summary>
		public BBS_ForumReply(int id, int item, string title, string content, string replyer, DateTime replytime, string ipaddress)
		{
			_id           = id;
			_item         = item;
			_title        = title;
			_content      = content;
			_replyer      = replyer;
			_replytime    = replytime;
			_ipaddress    = ipaddress;
			
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
		///论坛帖子
		///</summary>
		public int ItemID
		{
			get	{ return _item; }
			set	{ _item = value; }
		}

		///<summary>
		///回复标题
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
		///回复人
		///</summary>
		public string Replyer
		{
			get	{ return _replyer; }
			set	{ _replyer = value; }
		}

		///<summary>
		///回复时间
		///</summary>
		public DateTime ReplyTime
		{
			get	{ return _replytime; }
			set	{ _replytime = value; }
		}

		///<summary>
		///IP地址
		///</summary>
		public string IPAddress
		{
			get	{ return _ipaddress; }
			set	{ _ipaddress = value; }
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
            get { return "BBS_ForumReply"; }
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
					case "Item":
						return _item.ToString();
					case "Title":
						return _title;
					case "Content":
						return _content;
					case "Replyer":
						return _replyer;
					case "ReplyTime":
						return _replytime.ToString();
					case "IPAddress":
						return _ipaddress;
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
					case "Item":
						int.TryParse(value, out _item);
						break;
					case "Title":
						_title = value ;
						break;
					case "Content":
						_content = value ;
						break;
					case "Replyer":
						_replyer = value ;
						break;
					case "ReplyTime":
						DateTime.TryParse(value, out _replytime);
						break;
					case "IPAddress":
						_ipaddress = value ;
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
