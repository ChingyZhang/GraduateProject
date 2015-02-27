// ===================================================================
// 文件： SVM_UploadTemplate.cs
// 项目名称：
// 创建时间：2012/6/19
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_UploadTemplate数据实体类
	/// </summary>
	[Serializable]
	public class SVM_UploadTemplate : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _accountmonth = 0;
		private string _path = string.Empty;
		private int _state = 0;
		private string _remark = string.Empty;
		private int _isopponent = 0;
		private int _uploadstaff = 0;
		private DateTime _uploadtime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _improttime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_UploadTemplate()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_UploadTemplate(int id, string name, int accountmonth, string path, int state, string remark, int isopponent, int uploadstaff, DateTime uploadtime, int insertstaff, DateTime improttime)
		{
			_id           = id;
			_name         = name;
			_accountmonth = accountmonth;
			_path         = path;
			_state        = state;
			_remark       = remark;
			_isopponent   = isopponent;
			_uploadstaff  = uploadstaff;
			_uploadtime   = uploadtime;
			_insertstaff  = insertstaff;
			_improttime   = improttime;
			
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
		///AccountMonth
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///Path
		///</summary>
		public string Path
		{
			get	{ return _path; }
			set	{ _path = value; }
		}

		///<summary>
		///State
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
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
		///IsOpponent
		///</summary>
		public int IsOpponent
		{
			get	{ return _isopponent; }
			set	{ _isopponent = value; }
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
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///ImprotTime
		///</summary>
		public DateTime ImprotTime
		{
			get	{ return _improttime; }
			set	{ _improttime = value; }
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
            get { return "SVM_UploadTemplate"; }
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
					case "AccountMonth":
						return _accountmonth.ToString();
					case "Path":
						return _path;
					case "State":
						return _state.ToString();
					case "Remark":
						return _remark;
					case "IsOpponent":
						return _isopponent.ToString();
					case "UploadStaff":
						return _uploadstaff.ToString();
					case "UploadTime":
						return _uploadtime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "ImprotTime":
						return _improttime.ToString();
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
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "Path":
						_path = value ;
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "IsOpponent":
						int.TryParse(value, out _isopponent);
						break;
					case "UploadStaff":
						int.TryParse(value, out _uploadstaff);
						break;
					case "UploadTime":
						DateTime.TryParse(value, out _uploadtime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "ImprotTime":
						DateTime.TryParse(value, out _improttime);
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
