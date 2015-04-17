// ===================================================================
// 文件： Rpt_ReportColumnGroups.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.RPT
{
	/// <summary>
	///Rpt_ReportColumnGroups数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_ReportColumnGroups : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private Guid _report = Guid.Empty;
		private Guid _datasetfield = Guid.Empty;
		private string _displayname = string.Empty;
		private int _groupsortid = 0;
		private string _addsummary = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_ReportColumnGroups()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_ReportColumnGroups(Guid id, Guid report, Guid datasetfield, string displayname, int groupsortid, string addsummary, DateTime inserttime, DateTime updatetime)
		{
			_id           = id;
			_report       = report;
			_datasetfield = datasetfield;
			_displayname  = displayname;
			_groupsortid  = groupsortid;
			_addsummary   = addsummary;
			_inserttime   = inserttime;
			_updatetime   = updatetime;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///列组ID
		///</summary>
		public Guid ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///所属报表
		///</summary>
		public Guid Report
		{
			get	{ return _report; }
			set	{ _report = value; }
		}

		///<summary>
		///数据字段
		///</summary>
		public Guid DataSetField
		{
			get	{ return _datasetfield; }
			set	{ _datasetfield = value; }
		}

		///<summary>
		///显示名称
		///</summary>
		public string DisplayName
		{
			get	{ return _displayname; }
			set	{ _displayname = value; }
		}

		///<summary>
		///组序号
		///</summary>
		public int GroupSortID
		{
			get	{ return _groupsortid; }
			set	{ _groupsortid = value; }
		}

		///<summary>
		///是否增加列小计
		///</summary>
		public string AddSummary
		{
			get	{ return _addsummary; }
			set	{ _addsummary = value; }
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
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
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
            get { return "Rpt_ReportColumnGroups"; }
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
					case "Report":
						return _report.ToString();
					case "DataSetField":
						return _datasetfield.ToString();
					case "DisplayName":
						return _displayname;
					case "GroupSortID":
						return _groupsortid.ToString();
					case "AddSummary":
						return _addsummary;
					case "InsertTime":
						return _inserttime.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
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
						_id = new Guid(value);
						break;
					case "Report":
						_report = new Guid(value);
						break;
					case "DataSetField":
						_datasetfield = new Guid(value);
						break;
					case "DisplayName":
						_displayname = value ;
						break;
					case "GroupSortID":
						int.TryParse(value, out _groupsortid);
						break;
					case "AddSummary":
						_addsummary = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
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
