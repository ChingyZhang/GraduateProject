// ===================================================================
// 文件： Rpt_ReportCharts.cs
// 项目名称：
// 创建时间：2010/9/29
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
	///Rpt_ReportCharts数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_ReportCharts : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private Guid _report = Guid.Empty;
		private int _charttype = 0;
		private int _chartsortid = 0;
		private string _axiscolumns = string.Empty;
		private string _seriescolumns = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_ReportCharts()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_ReportCharts(Guid id, Guid report, int charttype, int chartsortid, string axiscolumns, string seriescolumns, DateTime inserttime, DateTime updatetime)
		{
			_id            = id;
			_report        = report;
			_charttype     = charttype;
			_chartsortid   = chartsortid;
			_axiscolumns   = axiscolumns;
			_seriescolumns = seriescolumns;
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
		///图表类型
		///</summary>
		public int ChartType
		{
			get	{ return _charttype; }
			set	{ _charttype = value; }
		}

		///<summary>
		///图表序号
		///</summary>
		public int ChartSortID
		{
			get	{ return _chartsortid; }
			set	{ _chartsortid = value; }
		}

		///<summary>
		///轴字段列
		///</summary>
		public string AxisColumns
		{
			get	{ return _axiscolumns; }
			set	{ _axiscolumns = value; }
		}

		///<summary>
		///系列字段列
		///</summary>
		public string SeriesColumns
		{
			get	{ return _seriescolumns; }
			set	{ _seriescolumns = value; }
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
            get { return "Rpt_ReportCharts"; }
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
					case "ChartType":
						return _charttype.ToString();
					case "ChartSortID":
						return _chartsortid.ToString();
					case "AxisColumns":
						return _axiscolumns;
					case "SeriesColumns":
						return _seriescolumns;
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
					case "ChartType":
						int.TryParse(value, out _charttype);
						break;
					case "ChartSortID":
						int.TryParse(value, out _chartsortid);
						break;
					case "AxisColumns":
						_axiscolumns = value ;
						break;
					case "SeriesColumns":
						_seriescolumns = value ;
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
