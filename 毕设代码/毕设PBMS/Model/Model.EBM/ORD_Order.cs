// ===================================================================
// 文件： ORD_Order.cs
// 项目名称：
// 创建时间：2014-01-28
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_Order数据实体类
	/// </summary>
	[Serializable]
	public class ORD_Order : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _accountmonth = 0;
		private int _supplier = 0;
		private int _client = 0;
		private int _publishid = 0;
		private int _type = 0;
		private int _classify = 0;
		private int _state = 0;
		private int _paymode = 0;
		private int _balanceflag = 0;
		private DateTime _reqarrivaldate = new DateTime(1900,1,1);
		private int _reqwarehouse = 0;
		private string _remark = string.Empty;
		private DateTime _submittime = new DateTime(1900,1,1);
		private DateTime _confirmtime = new DateTime(1900,1,1);
		private DateTime _balancetime = new DateTime(1900,1,1);
		private int _approvetask = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private Guid _updateuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_Order()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_Order(int id, string sheetcode, int accountmonth, int supplier, int client, int publishid, int type, int classify, int state, int paymode, int balanceflag, DateTime reqarrivaldate, int reqwarehouse, string remark, DateTime submittime, DateTime confirmtime, DateTime balancetime, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id             = id;
			_sheetcode      = sheetcode;
			_accountmonth   = accountmonth;
			_supplier       = supplier;
			_client         = client;
			_publishid      = publishid;
			_type           = type;
			_classify       = classify;
			_state          = state;
			_paymode        = paymode;
			_balanceflag    = balanceflag;
			_reqarrivaldate = reqarrivaldate;
			_reqwarehouse   = reqwarehouse;
			_remark         = remark;
			_submittime     = submittime;
			_confirmtime    = confirmtime;
			_balancetime    = balancetime;
			_approvetask    = approvetask;
			_approveflag    = approveflag;
			_inserttime     = inserttime;
			_insertuser     = insertuser;
			_updatetime     = updatetime;
			_updateuser     = updateuser;
			
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
		///请购单号
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
		}

		///<summary>
		///月份
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
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
		///请购商
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///归属发布ID
		///</summary>
		public int PublishID
		{
			get	{ return _publishid; }
			set	{ _publishid = value; }
		}

		///<summary>
		///订单类型
		///</summary>
		public int Type
		{
			get	{ return _type; }
			set	{ _type = value; }
		}

		///<summary>
		///订单类别
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///支付方式
		///</summary>
		public int PayMode
		{
			get	{ return _paymode; }
			set	{ _paymode = value; }
		}

		///<summary>
		///结算标志
		///</summary>
		public int BalanceFlag
		{
			get	{ return _balanceflag; }
			set	{ _balanceflag = value; }
		}

		///<summary>
		///要求到货日期
		///</summary>
		public DateTime ReqArrivalDate
		{
			get	{ return _reqarrivaldate; }
			set	{ _reqarrivaldate = value; }
		}

		///<summary>
		///要求到货仓库
		///</summary>
		public int ReqWarehouse
		{
			get	{ return _reqwarehouse; }
			set	{ _reqwarehouse = value; }
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
		///提交时间
		///</summary>
		public DateTime SubmitTime
		{
			get	{ return _submittime; }
			set	{ _submittime = value; }
		}

		///<summary>
		///确认时间
		///</summary>
		public DateTime ConfirmTime
		{
			get	{ return _confirmtime; }
			set	{ _confirmtime = value; }
		}

		///<summary>
		///结算时间
		///</summary>
		public DateTime BalanceTime
		{
			get	{ return _balancetime; }
			set	{ _balancetime = value; }
		}

		///<summary>
		///审批任务
		///</summary>
		public int ApproveTask
		{
			get	{ return _approvetask; }
			set	{ _approvetask = value; }
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
		public Guid InsertUser
		{
			get	{ return _insertuser; }
			set	{ _insertuser = value; }
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
		public Guid UpdateUser
		{
			get	{ return _updateuser; }
			set	{ _updateuser = value; }
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
            get { return "ORD_Order"; }
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
					case "SheetCode":
						return _sheetcode;
					case "AccountMonth":
						return _accountmonth.ToString();
					case "Supplier":
						return _supplier.ToString();
					case "Client":
						return _client.ToString();
					case "PublishID":
						return _publishid.ToString();
					case "Type":
						return _type.ToString();
					case "Classify":
						return _classify.ToString();
					case "State":
						return _state.ToString();
					case "PayMode":
						return _paymode.ToString();
					case "BalanceFlag":
						return _balanceflag.ToString();
					case "ReqArrivalDate":
						return _reqarrivaldate.ToString();
					case "ReqWarehouse":
						return _reqwarehouse.ToString();
					case "Remark":
						return _remark;
					case "SubmitTime":
						return _submittime.ToString();
					case "ConfirmTime":
						return _confirmtime.ToString();
					case "BalanceTime":
						return _balancetime.ToString();
					case "ApproveTask":
						return _approvetask.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateUser":
						return _updateuser.ToString();
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
					case "SheetCode":
						_sheetcode = value;
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "PublishID":
						int.TryParse(value, out _publishid);
						break;
					case "Type":
						int.TryParse(value, out _type);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "PayMode":
						int.TryParse(value, out _paymode);
						break;
					case "BalanceFlag":
						int.TryParse(value, out _balanceflag);
						break;
					case "ReqArrivalDate":
						DateTime.TryParse(value, out _reqarrivaldate);
						break;
					case "ReqWarehouse":
						int.TryParse(value, out _reqwarehouse);
						break;
					case "Remark":
						_remark = value;
						break;
					case "SubmitTime":
						DateTime.TryParse(value, out _submittime);
						break;
					case "ConfirmTime":
						DateTime.TryParse(value, out _confirmtime);
						break;
					case "BalanceTime":
						DateTime.TryParse(value, out _balancetime);
						break;
					case "ApproveTask":
						int.TryParse(value, out _approvetask);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertUser":
						_insertuser = new Guid(value);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateUser":
						_updateuser = new Guid(value);
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
