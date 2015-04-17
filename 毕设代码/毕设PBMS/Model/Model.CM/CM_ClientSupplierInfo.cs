// ===================================================================
// 文件： CM_ClientSupplierInfo.cs
// 项目名称：
// 创建时间：2015-03-25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_ClientSupplierInfo数据实体类
	/// </summary>
	[Serializable]
	public class CM_ClientSupplierInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _supplier = 0;
		private int _salesman = 0;
		private string _code = string.Empty;
		private int _state = 0;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private int _standardprice = 0;
		private int _tdpchannel = 0;
		private int _tdpsalesarea = 0;
		private int _visitroute = 0;
		private int _visitsequence = 0;
		private int _visittemplate = 0;
		private int _visitcycle = 0;
		private int _visitday = 0;
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
		public CM_ClientSupplierInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_ClientSupplierInfo(int id, int client, int supplier, int salesman, string code, int state, DateTime begindate, DateTime enddate, int standardprice, int tdpchannel, int tdpsalesarea, int visitroute, int visitsequence, int visittemplate, int visitcycle, int visitday, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_client        = client;
			_supplier      = supplier;
			_salesman      = salesman;
			_code          = code;
			_state         = state;
			_begindate     = begindate;
			_enddate       = enddate;
			_standardprice = standardprice;
			_tdpchannel    = tdpchannel;
			_tdpsalesarea  = tdpsalesarea;
			_visitroute    = visitroute;
			_visitsequence = visitsequence;
			_visittemplate = visittemplate;
			_visitcycle    = visitcycle;
			_visitday      = visitday;
			_remark        = remark;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			_updatetime    = updatetime;
			_updatestaff   = updatestaff;
			
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
		///客户ID
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///供货商
		///</summary>
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
		}

		///<summary>
		///业务人员
		///</summary>
		public int Salesman
		{
			get	{ return _salesman; }
			set	{ _salesman = value; }
		}

		///<summary>
		///客户自编码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///客户合作状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///开始合作日期
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///截止合作日期
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
		}

		///<summary>
		///供货价盘表
		///</summary>
		public int StandardPrice
		{
			get	{ return _standardprice; }
			set	{ _standardprice = value; }
		}

		///<summary>
		///渠道
		///</summary>
		public int TDPChannel
		{
			get	{ return _tdpchannel; }
			set	{ _tdpchannel = value; }
		}

		///<summary>
		///区域
		///</summary>
		public int TDPSalesArea
		{
			get	{ return _tdpsalesarea; }
			set	{ _tdpsalesarea = value; }
		}

		///<summary>
		///销售路线
		///</summary>
		public int VisitRoute
		{
			get	{ return _visitroute; }
			set	{ _visitroute = value; }
		}

		///<summary>
		///拜访顺序
		///</summary>
		public int VisitSequence
		{
			get	{ return _visitsequence; }
			set	{ _visitsequence = value; }
		}

		///<summary>
		///拜访模板
		///</summary>
		public int VisitTemplate
		{
			get	{ return _visittemplate; }
			set	{ _visittemplate = value; }
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
            get { return "CM_ClientSupplierInfo"; }
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
					case "Client":
						return _client.ToString();
					case "Supplier":
						return _supplier.ToString();
					case "Salesman":
						return _salesman.ToString();
					case "Code":
						return _code;
					case "State":
						return _state.ToString();
					case "BeginDate":
						return _begindate.ToString();
					case "EndDate":
						return _enddate.ToString();
					case "StandardPrice":
						return _standardprice.ToString();
					case "TDPChannel":
						return _tdpchannel.ToString();
					case "TDPSalesArea":
						return _tdpsalesarea.ToString();
					case "VisitRoute":
						return _visitroute.ToString();
					case "VisitSequence":
						return _visitsequence.ToString();
					case "VisitTemplate":
						return _visittemplate.ToString();
					case "VisitCycle":
						return _visitcycle.ToString();
					case "VisitDay":
						return _visitday.ToString();
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
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "Salesman":
						int.TryParse(value, out _salesman);
						break;
					case "Code":
						_code = value;
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
						break;
					case "StandardPrice":
						int.TryParse(value, out _standardprice);
						break;
					case "TDPChannel":
						int.TryParse(value, out _tdpchannel);
						break;
					case "TDPSalesArea":
						int.TryParse(value, out _tdpsalesarea);
						break;
					case "VisitRoute":
						int.TryParse(value, out _visitroute);
						break;
					case "VisitSequence":
						int.TryParse(value, out _visitsequence);
						break;
					case "VisitTemplate":
						int.TryParse(value, out _visittemplate);
						break;
					case "VisitCycle":
						int.TryParse(value, out _visitcycle);
						break;
					case "VisitDay":
						int.TryParse(value, out _visitday);
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
