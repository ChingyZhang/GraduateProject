// ===================================================================
// 文件： PDT_StandardPrice.cs
// 项目名称：
// 创建时间：2011/8/23
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_StandardPrice数据实体类
	/// </summary>
	[Serializable]
	public class PDT_StandardPrice : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _fullname = string.Empty;
		private int _organizecity = 0;
		private int _activeflag = 0;
		private int _approveflag = 0;
		private int _taskid = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice(int id, string fullname, int organizecity, int activeflag, int approveflag, int taskid, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_fullname     = fullname;
			_organizecity = organizecity;
			_activeflag   = activeflag;
			_approveflag  = approveflag;
			_taskid       = taskid;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			_updatetime   = updatetime;
			_updatestaff  = updatestaff;
			
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
		///FullName
		///</summary>
		public string FullName
		{
			get	{ return _fullname; }
			set	{ _fullname = value; }
		}

		///<summary>
		///OrganizeCity
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///ActiveFlag
		///</summary>
		public int ActiveFlag
		{
			get	{ return _activeflag; }
			set	{ _activeflag = value; }
		}

		///<summary>
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///TaskID
		///</summary>
		public int TaskID
		{
			get	{ return _taskid; }
			set	{ _taskid = value; }
		}

		///<summary>
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
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
		///UpdateTime
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///UpdateStaff
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
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
            get { return "PDT_StandardPrice"; }
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
					case "FullName":
						return _fullname;
					case "OrganizeCity":
						return _organizecity.ToString();
					case "ActiveFlag":
						return _activeflag.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "TaskID":
						return _taskid.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
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
					case "FullName":
						_fullname = value ;
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "ActiveFlag":
						int.TryParse(value, out _activeflag);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "TaskID":
						int.TryParse(value, out _taskid);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
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
