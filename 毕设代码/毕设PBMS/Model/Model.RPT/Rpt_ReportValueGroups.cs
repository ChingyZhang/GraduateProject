// ===================================================================
// 文件： Rpt_ReportValueGroups.cs
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
	///Rpt_ReportValueGroups数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_ReportValueGroups : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private Guid _report = Guid.Empty;
		private Guid _datasetfield = Guid.Empty;
		private string _displayname = string.Empty;
		private int _valuesortid = 0;
		private int _statisticmode = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_ReportValueGroups()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_ReportValueGroups(Guid id, Guid report, Guid datasetfield, string displayname, int valuesortid, int statisticmode, DateTime inserttime, DateTime updatetime)
		{
			_id            = id;
			_report        = report;
			_datasetfield  = datasetfield;
			_displayname   = displayname;
			_valuesortid   = valuesortid;
			_statisticmode = statisticmode;
			_inserttime    = inserttime;
			_updatetime    = updatetime;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
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
		///值序号
		///</summary>
		public int ValueSortID
		{
			get	{ return _valuesortid; }
			set	{ _valuesortid = value; }
		}

		///<summary>
		///统计方式(1求和 2计数 3最大值 4最小值)
		///</summary>
		public int StatisticMode
		{
			get	{ return _statisticmode; }
			set	{ _statisticmode = value; }
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
            get { return "Rpt_ReportValueGroups"; }
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
					case "ValueSortID":
						return _valuesortid.ToString();
					case "StatisticMode":
						return _statisticmode.ToString();
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
					case "ValueSortID":
						int.TryParse(value, out _valuesortid);
						break;
					case "StatisticMode":
						int.TryParse(value, out _statisticmode);
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
