// ===================================================================
// 文件： KB_Comment.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   WJX
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///KB_Comment数据实体类
	/// </summary>
	[Serializable]
	public class KB_Comment : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _article = 0;
		private int _commentstaff = 0;
		private DateTime _commenttime = new DateTime(1900,1,1);
		private string _content = string.Empty;
		private string _isdelete = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_Comment()
		{
		}
		
		///<summary>
		///
		///</summary>
		public KB_Comment(int id, int article, int commentstaff, DateTime commenttime, string content, string isdelete)
		{
			_id           = id;
			_article      = article;
			_commentstaff = commentstaff;
			_commenttime  = commenttime;
			_content      = content;
			_isdelete     = isdelete;
			
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
		///Article
		///</summary>
		public int Article
		{
			get	{ return _article; }
			set	{ _article = value; }
		}

		///<summary>
		///CommentStaff
		///</summary>
		public int CommentStaff
		{
			get	{ return _commentstaff; }
			set	{ _commentstaff = value; }
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
		///Content
		///</summary>
		public string Content
		{
			get	{ return _content; }
			set	{ _content = value; }
		}

		///<summary>
		///IsDelete
		///</summary>
		public string IsDelete
		{
			get	{ return _isdelete; }
			set	{ _isdelete = value; }
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
            get { return "KB_Comment"; }
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
					case "Article":
						return _article.ToString();
					case "CommentStaff":
						return _commentstaff.ToString();
					case "CommentTime":
						return _commenttime.ToString();
					case "Content":
						return _content;
					case "IsDelete":
						return _isdelete;
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
					case "Article":
						int.TryParse(value, out _article);
						break;
					case "CommentStaff":
						int.TryParse(value, out _commentstaff);
						break;
					case "CommentTime":
						DateTime.TryParse(value, out _commenttime);
						break;
					case "Content":
						_content = value ;
						break;
					case "IsDelete":
						_isdelete = value ;
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
