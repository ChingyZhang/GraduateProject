// ===================================================================
// 文件： BBS_BoardUserMember.cs
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
	///BBS_BoardUserMember数据实体类
	/// </summary>
	[Serializable]
	public class BBS_BoardUserMember : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _board = 0;
		private string _username = string.Empty;
		private int _role = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_BoardUserMember()
		{
		}
		
		///<summary>
		///
		///</summary>
		public BBS_BoardUserMember(int id, int board, string username, int role)
		{
			_id       = id;
			_board    = board;
			_username = username;
			_role     = role;
			
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
		///板块
		///</summary>
		public int Board
		{
			get	{ return _board; }
			set	{ _board = value; }
		}

		///<summary>
		///成员用户名
		///</summary>
		public string UserName
		{
			get	{ return _username; }
			set	{ _username = value; }
		}

		///<summary>
		///角色
		///</summary>
		public int Role
		{
			get	{ return _role; }
			set	{ _role = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "BBS_BoardUserMember"; }
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
					case "UserName":
						return _username;
					case "Role":
						return _role.ToString();
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
					case "Board":
						int.TryParse(value, out _board);
						break;
					case "UserName":
						_username = value ;
						break;
					case "Role":
						int.TryParse(value, out _role);
						break;

				}
			}
        }
		#endregion
	}
}
