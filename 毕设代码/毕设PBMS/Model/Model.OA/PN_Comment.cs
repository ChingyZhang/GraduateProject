// ===================================================================
// 文件： PN_Comment.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///PN_Comment数据实体类
	/// </summary>
	[Serializable]
	public class PN_Comment : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _notice = 0;
		private int _staff = 0;
		private string _content = string.Empty;
		private DateTime _commenttime = new DateTime(1900,1,1);
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_Comment()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PN_Comment(int id, int notice, int staff, string content, DateTime commenttime)
		{
			_id          = id;
			_notice      = notice;
			_staff       = staff;
			_content     = content;
			_commenttime = commenttime;
			
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
		///公告ID
		///</summary>
		public int Notice
		{
			get	{ return _notice; }
			set	{ _notice = value; }
		}

		///<summary>
		///评论员工
		///</summary>
		public int Staff
		{
			get	{ return _staff; }
			set	{ _staff = value; }
		}

		///<summary>
		///评论内容
		///</summary>
		public string Content
		{
			get	{ return _content; }
			set	{ _content = value; }
		}

		///<summary>
		///评论时间
		///</summary>
		public DateTime CommentTime
		{
			get	{ return _commenttime; }
			set	{ _commenttime = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PN_Comment"; }
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
					case "Notice":
						return _notice.ToString();
					case "Staff":
						return _staff.ToString();
					case "Content":
						return _content;
					case "CommentTime":
						return _commenttime.ToString();
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
					case "Notice":
						int.TryParse(value, out _notice);
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

				}
			}
        }
		#endregion
	}
}
