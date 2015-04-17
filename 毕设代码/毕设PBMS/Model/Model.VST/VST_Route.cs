// ===================================================================
// 文件： VST_Route.cs
// 项目名称：
// 创建时间：2015-03-24
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_Route数据实体类
	/// </summary>
	[Serializable]
	public class VST_Route : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _organizecity = 0;
		private int _relatestaff = 0;
		private int _visitcycle = 0;
		private int _visitday = 0;
		private DateTime _firstvisitdate = new DateTime(1900,1,1);
		private string _ismustsequencevisit = string.Empty;
		private string _enableflag = string.Empty;
		private int _ownertype = 0;
		private int _ownerclient = 0;
		private int _approveflag = 0;
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
		public VST_Route()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_Route(int id, string code, string name, int organizecity, int relatestaff, int visitcycle, int visitday, DateTime firstvisitdate, string ismustsequencevisit, string enableflag, int ownertype, int ownerclient, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id                  = id;
			_code                = code;
			_name                = name;
			_organizecity        = organizecity;
			_relatestaff         = relatestaff;
			_visitcycle          = visitcycle;
			_visitday            = visitday;
			_firstvisitdate      = firstvisitdate;
			_ismustsequencevisit = ismustsequencevisit;
			_enableflag          = enableflag;
			_ownertype           = ownertype;
			_ownerclient         = ownerclient;
			_approveflag         = approveflag;
			_inserttime          = inserttime;
			_insertstaff         = insertstaff;
			_updatetime          = updatetime;
			_updatestaff         = updatestaff;
			
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
		///线路编号
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///线路名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///所属区域
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///关联业代
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///拜访周期
		///</summary>
		public int VisitCycle
		{
			get	{ return _visitcycle; }
			set	{ _visitcycle = value; }
		}

		///<summary>
		///拜访日
		///</summary>
		public int VisitDay
		{
			get	{ return _visitday; }
			set	{ _visitday = value; }
		}

		///<summary>
		///首次拜访日期
		///</summary>
		public DateTime FirstVisitDate
		{
			get	{ return _firstvisitdate; }
			set	{ _firstvisitdate = value; }
		}

		///<summary>
		///是否顺序拜访
		///</summary>
		public string IsMustSequenceVisit
		{
			get	{ return _ismustsequencevisit; }
			set	{ _ismustsequencevisit = value; }
		}

		///<summary>
		///是否启用
		///</summary>
		public string EnableFlag
		{
			get	{ return _enableflag; }
			set	{ _enableflag = value; }
		}

		///<summary>
		///所有权属性
		///</summary>
		public int OwnerType
		{
			get	{ return _ownertype; }
			set	{ _ownertype = value; }
		}

		///<summary>
		///所有权人
		///</summary>
		public int OwnerClient
		{
			get	{ return _ownerclient; }
			set	{ _ownerclient = value; }
		}

		///<summary>
		///审核标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///新增人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///更新日期
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
            get { return "VST_Route"; }
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
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "OrganizeCity":
						return _organizecity.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "VisitCycle":
						return _visitcycle.ToString();
					case "VisitDay":
						return _visitday.ToString();
					case "FirstVisitDate":
						return _firstvisitdate.ToString();
					case "IsMustSequenceVisit":
						return _ismustsequencevisit;
					case "EnableFlag":
						return _enableflag;
					case "OwnerType":
						return _ownertype.ToString();
					case "OwnerClient":
						return _ownerclient.ToString();
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
					case "Code":
						_code = value;
						break;
					case "Name":
						_name = value;
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "VisitCycle":
						int.TryParse(value, out _visitcycle);
						break;
					case "VisitDay":
						int.TryParse(value, out _visitday);
						break;
					case "FirstVisitDate":
						DateTime.TryParse(value, out _firstvisitdate);
						break;
					case "IsMustSequenceVisit":
						_ismustsequencevisit = value;
						break;
					case "EnableFlag":
						_enableflag = value;
						break;
					case "OwnerType":
						int.TryParse(value, out _ownertype);
						break;
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
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
