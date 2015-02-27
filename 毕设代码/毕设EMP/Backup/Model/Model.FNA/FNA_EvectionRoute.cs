// ===================================================================
// 文件： FNA_EvectionRoute.cs
// 项目名称：
// 创建时间：2010/8/3
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
	/// <summary>
	///FNA_EvectionRoute数据实体类
	/// </summary>
	[Serializable]
	public class FNA_EvectionRoute : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _writeoffid = 0;
		private int _relatestaff = 0;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private string _evectionline = string.Empty;
		private string _transport = string.Empty;
		private decimal _cost1 = 0;
		private decimal _cost2 = 0;
		private decimal _cost3 = 0;
		private decimal _cost4 = 0;
		private decimal _cost5 = 0;
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
		public FNA_EvectionRoute()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_EvectionRoute(int id, int writeoffid, int relatestaff, DateTime begindate, DateTime enddate, string evectionline, string transport, decimal cost1, decimal cost2, decimal cost3, decimal cost4, decimal cost5, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_writeoffid   = writeoffid;
			_relatestaff  = relatestaff;
			_begindate    = begindate;
			_enddate      = enddate;
			_evectionline = evectionline;
			_transport    = transport;
			_cost1        = cost1;
			_cost2        = cost2;
			_cost3        = cost3;
			_cost4        = cost4;
			_cost5        = cost5;
			_remark       = remark;
			_approveflag  = approveflag;
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
		///WriteOffID
		///</summary>
		public int WriteOffID
		{
			get	{ return _writeoffid; }
			set	{ _writeoffid = value; }
		}

		///<summary>
		///RelateStaff
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///BeginDate
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///EndDate
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
		}

		///<summary>
		///EvectionLine
		///</summary>
		public string EvectionLine
		{
			get	{ return _evectionline; }
			set	{ _evectionline = value; }
		}

		///<summary>
		///Transport
		///</summary>
		public string Transport
		{
			get	{ return _transport; }
			set	{ _transport = value; }
		}

		///<summary>
		///Cost1
		///</summary>
		public decimal Cost1
		{
			get	{ return _cost1; }
			set	{ _cost1 = value; }
		}

		///<summary>
		///Cost2
		///</summary>
		public decimal Cost2
		{
			get	{ return _cost2; }
			set	{ _cost2 = value; }
		}

		///<summary>
		///Cost3
		///</summary>
		public decimal Cost3
		{
			get	{ return _cost3; }
			set	{ _cost3 = value; }
		}

		///<summary>
		///Cost4
		///</summary>
		public decimal Cost4
		{
			get	{ return _cost4; }
			set	{ _cost4 = value; }
		}

		///<summary>
		///Cost5
		///</summary>
		public decimal Cost5
		{
			get	{ return _cost5; }
			set	{ _cost5 = value; }
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
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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
            get { return "FNA_EvectionRoute"; }
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
					case "WriteOffID":
						return _writeoffid.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "BeginDate":
						return _begindate.ToString();
					case "EndDate":
						return _enddate.ToString();
					case "EvectionLine":
						return _evectionline;
					case "Transport":
						return _transport;
					case "Cost1":
						return _cost1.ToString();
					case "Cost2":
						return _cost2.ToString();
					case "Cost3":
						return _cost3.ToString();
					case "Cost4":
						return _cost4.ToString();
					case "Cost5":
						return _cost5.ToString();
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
					case "WriteOffID":
						int.TryParse(value, out _writeoffid);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
						break;
					case "EvectionLine":
						_evectionline = value ;
						break;
					case "Transport":
						_transport = value ;
						break;
					case "Cost1":
						decimal.TryParse(value, out _cost1);
						break;
					case "Cost2":
						decimal.TryParse(value, out _cost2);
						break;
					case "Cost3":
						decimal.TryParse(value, out _cost3);
						break;
					case "Cost4":
						decimal.TryParse(value, out _cost4);
						break;
					case "Cost5":
						decimal.TryParse(value, out _cost5);
						break;
					case "Remark":
						_remark = value ;
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
