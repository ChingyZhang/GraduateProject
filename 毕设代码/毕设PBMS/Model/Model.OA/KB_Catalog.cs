// ===================================================================
// 文件： KB_Catalog.cs
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
	///KB_Catalog数据实体类
	/// </summary>
	[Serializable]
	public class KB_Catalog : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _superid = 0;
		private int _approvestaff = 0;
		private string _commentflag = string.Empty;
		private string _approveflag = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_Catalog()
		{
		}
		
		///<summary>
		///
		///</summary>
		public KB_Catalog(int id, string name, int superid, int approvestaff, string commentflag, string approveflag)
		{
			_id           = id;
			_name         = name;
			_superid      = superid;
			_approvestaff = approvestaff;
			_commentflag  = commentflag;
			_approveflag  = approveflag;
			
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
		///Name
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///SuperID
		///</summary>
		public int SuperID
		{
			get	{ return _superid; }
			set	{ _superid = value; }
		}

		///<summary>
		///ApproveStaff
		///</summary>
		public int ApproveStaff
		{
			get	{ return _approvestaff; }
			set	{ _approvestaff = value; }
		}

		///<summary>
		///CommentFlag
		///</summary>
		public string CommentFlag
		{
			get	{ return _commentflag; }
			set	{ _commentflag = value; }
		}

		///<summary>
		///ApproveFlag
		///</summary>
		public string ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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
            get { return "KB_Catalog"; }
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
					case "Name":
						return _name;
					case "SuperID":
						return _superid.ToString();
					case "ApproveStaff":
						return _approvestaff.ToString();
					case "CommentFlag":
						return _commentflag;
					case "ApproveFlag":
						return _approveflag;
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
					case "Name":
						_name = value ;
						break;
					case "SuperID":
						int.TryParse(value, out _superid);
						break;
					case "ApproveStaff":
						int.TryParse(value, out _approvestaff);
						break;
					case "CommentFlag":
						_commentflag = value ;
						break;
					case "ApproveFlag":
						_approveflag = value ;
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
