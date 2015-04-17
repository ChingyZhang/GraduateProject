// ===================================================================
// 文件： JN_Journal.cs
// 项目名称：
// 创建时间：2010/6/19
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///JN_Journal数据实体类
	/// </summary>
	[Serializable]
	public class JN_Journal : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _title = string.Empty;
		private int _organizecity = 0;
		private int _journaltype = 0;
		private int _staff = 0;
		private DateTime _begintime = new DateTime(1900,1,1);
		private DateTime _endtime = new DateTime(1900,1,1);
		private int _workingclassify = 0;
		private int _relateclient = 0;
		private int _relatelinkman = 0;
		private int _officialcity = 0;
		private int _relatestaff = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public JN_Journal()
		{
		}
		
		///<summary>
		///
		///</summary>
		public JN_Journal(int id, string title, int organizecity, int journaltype, int staff, DateTime begintime, DateTime endtime, int workingclassify, int relateclient, int relatelinkman, int officialcity, int relatestaff, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, string remark)
		{
			_id              = id;
			_title           = title;
			_organizecity    = organizecity;
			_journaltype     = journaltype;
			_staff           = staff;
			_begintime       = begintime;
			_endtime         = endtime;
			_workingclassify = workingclassify;
			_relateclient    = relateclient;
			_relatelinkman   = relatelinkman;
			_officialcity    = officialcity;
			_relatestaff     = relatestaff;
			_approveflag     = approveflag;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
			_remark          = remark;
			
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
		///标题
		///</summary>
		public string Title
		{
			get	{ return _title; }
			set	{ _title = value; }
		}

		///<summary>
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///日志类型
		///</summary>
		public int JournalType
		{
			get	{ return _journaltype; }
			set	{ _journaltype = value; }
		}

		///<summary>
		///员工
		///</summary>
		public int Staff
		{
			get	{ return _staff; }
			set	{ _staff = value; }
		}

		///<summary>
		///开始时间
		///</summary>
		public DateTime BeginTime
		{
			get	{ return _begintime; }
			set	{ _begintime = value; }
		}

		///<summary>
		///截止时间
		///</summary>
		public DateTime EndTime
		{
			get	{ return _endtime; }
			set	{ _endtime = value; }
		}

		///<summary>
		///工作类别
		///</summary>
		public int WorkingClassify
		{
			get	{ return _workingclassify; }
			set	{ _workingclassify = value; }
		}

		///<summary>
		///相关客户
		///</summary>
		public int RelateClient
		{
			get	{ return _relateclient; }
			set	{ _relateclient = value; }
		}

		///<summary>
		///RelateLinkMan
		///</summary>
		public int RelateLinkMan
		{
			get	{ return _relatelinkman; }
			set	{ _relatelinkman = value; }
		}

		///<summary>
		///城市
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}

		///<summary>
		///协同员工
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
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
		///录入时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///录入人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "JN_Journal"; }
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
					case "OrganizeCity":
						return _organizecity.ToString();
					case "JournalType":
						return _journaltype.ToString();
					case "Staff":
						return _staff.ToString();
					case "BeginTime":
						return _begintime.ToString();
					case "EndTime":
						return _endtime.ToString();
					case "WorkingClassify":
						return _workingclassify.ToString();
					case "RelateClient":
						return _relateclient.ToString();
					case "RelateLinkMan":
						return _relatelinkman.ToString();
					case "OfficialCity":
						return _officialcity.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
					case "Remark":
						return _remark;
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "JournalType":
						int.TryParse(value, out _journaltype);
						break;
					case "Staff":
						int.TryParse(value, out _staff);
						break;
					case "BeginTime":
						DateTime.TryParse(value, out _begintime);
						break;
					case "EndTime":
						DateTime.TryParse(value, out _endtime);
						break;
					case "WorkingClassify":
						int.TryParse(value, out _workingclassify);
						break;
					case "RelateClient":
						int.TryParse(value, out _relateclient);
						break;
					case "RelateLinkMan":
						int.TryParse(value, out _relatelinkman);
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
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
					case "Remark":
						_remark = value ;
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
