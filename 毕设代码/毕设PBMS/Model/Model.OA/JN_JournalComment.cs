// ===================================================================
// 文件： JN_JournalComment.cs
// 项目名称：
// 创建时间：2009-4-25
// 作者:	   shh
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///JN_JournalComment数据实体类
	/// </summary>
	[Serializable]
	public class JN_JournalComment : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _journalid = 0;
		private int _staff = 0;
		private string _content = string.Empty;
		private DateTime _commenttime = new DateTime(1900,1,1);
		private string _extproperty = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public JN_JournalComment()
		{
		}
		
		///<summary>
		///
		///</summary>
		public JN_JournalComment(int id, int journalid, int staff, string content, DateTime commenttime, string extproperty)
		{
			_id          = id;
			_journalid   = journalid;
			_staff       = staff;
			_content     = content;
			_commenttime = commenttime;
			_extproperty = extproperty;
			
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
		///JournalID
		///</summary>
		public int JournalID
		{
			get	{ return _journalid; }
			set	{ _journalid = value; }
		}

		///<summary>
		///Staff
		///</summary>
		public int Staff
		{
			get	{ return _staff; }
			set	{ _staff = value; }
		}

		///<summary>
		///Content
		///</summary>
		public string Content
		{
			get	{ return _content; }
			set	{ _content = value; }
		}

		///<summary>
		///CommentTime
		///</summary>
		public DateTime CommentTime
		{
			get	{ return _commenttime; }
			set	{ _commenttime = value; }
		}

		///<summary>
		///ExtProperty
		///</summary>
		public string ExtProperty
		{
			get	{ return _extproperty; }
			set	{ _extproperty = value; }
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
            get { return "JN_JournalComment"; }
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
					case "JournalID":
						return _journalid.ToString();
					case "Staff":
						return _staff.ToString();
					case "Content":
						return _content;
					case "CommentTime":
						return _commenttime.ToString();
					case "ExtProperty":
						return _extproperty;
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
					case "JournalID":
						int.TryParse(value, out _journalid);
						break;
					case "Staff":
						int.TryParse(value, out _staff);
						break;
					case "Content":
						_content = value ;
						break;
					case "CommentTime":
						DateTime.TryParse(value, out _commenttime);
						break;
					case "ExtProperty":
						_extproperty = value ;
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
