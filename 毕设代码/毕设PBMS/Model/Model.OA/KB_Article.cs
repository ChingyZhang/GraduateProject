// ===================================================================
// 文件： KB_Article.cs
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
	///KB_Article数据实体类
	/// </summary>
	[Serializable]
	public class KB_Article : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _title = string.Empty;
		private int _catalog = 0;
		private string _keyword = string.Empty;
		private string _source = string.Empty;
		private string _content = string.Empty;
		private string _author = string.Empty;
		private int _uploadstaff = 0;
		private DateTime _uploadtime = new DateTime(1900,1,1);
		private string _hasapproved = string.Empty;
		private DateTime _approvetime = new DateTime(1900,1,1);
		private string _approvestaffideas = string.Empty;
		private int _approvestaff = 0;
		private int _readcount = 0;
		private int _usefullcount = 0;
		private string _remark = string.Empty;
		private string _isdelete = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public KB_Article()
		{
		}
		
		///<summary>
		///
		///</summary>
		public KB_Article(int id, string title, int catalog, string keyword, string source, string content, string author, int uploadstaff, DateTime uploadtime, string hasapproved, DateTime approvetime, string approvestaffideas, int approvestaff, int readcount, int usefullcount, string remark, string isdelete)
		{
			_id                = id;
			_title             = title;
			_catalog           = catalog;
			_keyword           = keyword;
			_source            = source;
			_content           = content;
			_author            = author;
			_uploadstaff       = uploadstaff;
			_uploadtime        = uploadtime;
			_hasapproved       = hasapproved;
			_approvetime       = approvetime;
			_approvestaffideas = approvestaffideas;
			_approvestaff      = approvestaff;
			_readcount         = readcount;
			_usefullcount      = usefullcount;
			_remark            = remark;
			_isdelete          = isdelete;
			
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
		///Title
		///</summary>
		public string Title
		{
			get	{ return _title; }
			set	{ _title = value; }
		}

		///<summary>
		///Catalog
		///</summary>
		public int Catalog
		{
			get	{ return _catalog; }
			set	{ _catalog = value; }
		}

		///<summary>
		///KeyWord
		///</summary>
		public string KeyWord
		{
			get	{ return _keyword; }
			set	{ _keyword = value; }
		}

		///<summary>
		///Source
		///</summary>
		public string Source
		{
			get	{ return _source; }
			set	{ _source = value; }
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
		///Author
		///</summary>
		public string Author
		{
			get	{ return _author; }
			set	{ _author = value; }
		}

		///<summary>
		///UploadStaff
		///</summary>
		public int UploadStaff
		{
			get	{ return _uploadstaff; }
			set	{ _uploadstaff = value; }
		}

		///<summary>
		///UploadTime
		///</summary>
		public DateTime UploadTime
		{
			get	{ return _uploadtime; }
			set	{ _uploadtime = value; }
		}

		///<summary>
		///HasApproved
		///</summary>
		public string HasApproved
		{
			get	{ return _hasapproved; }
			set	{ _hasapproved = value; }
		}

		///<summary>
		///ApproveTime
		///</summary>
		public DateTime ApproveTime
		{
			get	{ return _approvetime; }
			set	{ _approvetime = value; }
		}

		///<summary>
		///ApproveStaffIdeas
		///</summary>
		public string ApproveStaffIdeas
		{
			get	{ return _approvestaffideas; }
			set	{ _approvestaffideas = value; }
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
		///ReadCount
		///</summary>
		public int ReadCount
		{
			get	{ return _readcount; }
			set	{ _readcount = value; }
		}

		///<summary>
		///UsefullCount
		///</summary>
		public int UsefullCount
		{
			get	{ return _usefullcount; }
			set	{ _usefullcount = value; }
		}

		///<summary>
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "KB_Article"; }
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
					case "Title":
						return _title;
					case "Catalog":
						return _catalog.ToString();
					case "KeyWord":
						return _keyword;
					case "Source":
						return _source;
					case "Content":
						return _content;
					case "Author":
						return _author;
					case "UploadStaff":
						return _uploadstaff.ToString();
					case "UploadTime":
						return _uploadtime.ToString();
					case "HasApproved":
						return _hasapproved;
					case "ApproveTime":
						return _approvetime.ToString();
					case "ApproveStaffIdeas":
						return _approvestaffideas;
					case "ApproveStaff":
						return _approvestaff.ToString();
					case "ReadCount":
						return _readcount.ToString();
					case "UsefullCount":
						return _usefullcount.ToString();
					case "Remark":
						return _remark;
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
					case "Title":
						_title = value ;
						break;
					case "Catalog":
						int.TryParse(value, out _catalog);
						break;
					case "KeyWord":
						_keyword = value ;
						break;
					case "Source":
						_source = value ;
						break;
					case "Content":
						_content = value ;
						break;
					case "Author":
						_author = value ;
						break;
					case "UploadStaff":
						int.TryParse(value, out _uploadstaff);
						break;
					case "UploadTime":
						DateTime.TryParse(value, out _uploadtime);
						break;
					case "HasApproved":
						_hasapproved = value ;
						break;
					case "ApproveTime":
						DateTime.TryParse(value, out _approvetime);
						break;
					case "ApproveStaffIdeas":
						_approvestaffideas = value ;
						break;
					case "ApproveStaff":
						int.TryParse(value, out _approvestaff);
						break;
					case "ReadCount":
						int.TryParse(value, out _readcount);
						break;
					case "UsefullCount":
						int.TryParse(value, out _usefullcount);
						break;
					case "Remark":
						_remark = value ;
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
