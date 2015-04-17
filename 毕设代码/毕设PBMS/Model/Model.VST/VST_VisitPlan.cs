// ===================================================================
// 文件： VST_VisitPlan.cs
// 项目名称：
// 创建时间：2015-03-13
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
	///VST_VisitPlan数据实体类
	/// </summary>
	[Serializable]
	public class VST_VisitPlan : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _route = 0;
		private int _relatestaff = 0;
		private DateTime _planvisitdate = new DateTime(1900,1,1);
		private string _ismustsequencevisit = string.Empty;
		private string _remark = string.Empty;
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
		public VST_VisitPlan()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_VisitPlan(int id, int route, int relatestaff, DateTime planvisitdate, string ismustsequencevisit, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id                  = id;
			_route               = route;
			_relatestaff         = relatestaff;
			_planvisitdate       = planvisitdate;
			_ismustsequencevisit = ismustsequencevisit;
			_remark              = remark;
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
		///线路ID
		///</summary>
		public int Route
		{
			get	{ return _route; }
			set	{ _route = value; }
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
		///计划拜访日期
		///</summary>
		public DateTime PlanVisitDate
		{
			get	{ return _planvisitdate; }
			set	{ _planvisitdate = value; }
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
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "VST_VisitPlan"; }
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
					case "Route":
						return _route.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "PlanVisitDate":
						return _planvisitdate.ToString();
					case "IsMustSequenceVisit":
						return _ismustsequencevisit;
					case "Remark":
						return _remark;
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
					case "Route":
						int.TryParse(value, out _route);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "PlanVisitDate":
						DateTime.TryParse(value, out _planvisitdate);
						break;
					case "IsMustSequenceVisit":
						_ismustsequencevisit = value;
						break;
					case "Remark":
						_remark = value;
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
