// ===================================================================
// 文件： PBM_Order.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.PBM
{
	/// <summary>
	///PBM_Order数据实体类
	/// </summary>
	[Serializable]
	public class PBM_Order : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _supplier = 0;
		private int _client = 0;
		private int _salesman = 0;
		private int _standardprice = 0;
		private int _classify = 0;
		private int _state = 0;
		private decimal _discountamount = 0;
		private decimal _wipeamount = 0;
		private decimal _actamount = 0;
		private DateTime _arrivetime = new DateTime(1900,1,1);
		private int _arrivewarehouse = 0;
		private DateTime _submittime = new DateTime(1900,1,1);
		private DateTime _confirmtime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private int _worklist = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_Order()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PBM_Order(int id, string sheetcode, int supplier, int client, int salesman, int standardprice, int classify, int state, decimal discountamount, decimal wipeamount, decimal actamount, DateTime arrivetime, int arrivewarehouse, DateTime submittime, DateTime confirmtime, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, int worklist)
		{
			_id              = id;
			_sheetcode       = sheetcode;
			_supplier        = supplier;
			_client          = client;
			_salesman        = salesman;
			_standardprice   = standardprice;
			_classify        = classify;
			_state           = state;
			_discountamount  = discountamount;
			_wipeamount      = wipeamount;
			_actamount       = actamount;
			_arrivetime      = arrivetime;
			_arrivewarehouse = arrivewarehouse;
			_submittime      = submittime;
			_confirmtime     = confirmtime;
			_remark          = remark;
			_approveflag     = approveflag;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
			_worklist        = worklist;
			
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
		///订单号
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
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
		///订购商
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///SalesMan
		///</summary>
		public int SalesMan
		{
			get	{ return _salesman; }
			set	{ _salesman = value; }
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
		///类别
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
		///折扣金额
		///</summary>
		public decimal DiscountAmount
		{
			get	{ return _discountamount; }
			set	{ _discountamount = value; }
		}

		///<summary>
		///优惠金额
		///</summary>
		public decimal WipeAmount
		{
			get	{ return _wipeamount; }
			set	{ _wipeamount = value; }
		}

		///<summary>
		///实际订货金额
		///</summary>
		public decimal ActAmount
		{
			get	{ return _actamount; }
			set	{ _actamount = value; }
		}

		///<summary>
		///要求送达时间
		///</summary>
		public DateTime ArriveTime
		{
			get	{ return _arrivetime; }
			set	{ _arrivetime = value; }
		}

		///<summary>
		///要求送达仓库
		///</summary>
		public int ArriveWarehouse
		{
			get	{ return _arrivewarehouse; }
			set	{ _arrivewarehouse = value; }
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

		///<summary>
		///关联拜访记录
		///</summary>
		public int WorkList
		{
			get	{ return _worklist; }
			set	{ _worklist = value; }
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
            get { return "PBM_Order"; }
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
					case "Supplier":
						return _supplier.ToString();
					case "Client":
						return _client.ToString();
					case "SalesMan":
						return _salesman.ToString();
					case "StandardPrice":
						return _standardprice.ToString();
					case "Classify":
						return _classify.ToString();
					case "State":
						return _state.ToString();
					case "DiscountAmount":
						return _discountamount.ToString();
					case "WipeAmount":
						return _wipeamount.ToString();
					case "ActAmount":
						return _actamount.ToString();
					case "ArriveTime":
						return _arrivetime.ToString();
					case "ArriveWarehouse":
						return _arrivewarehouse.ToString();
					case "SubmitTime":
						return _submittime.ToString();
					case "ConfirmTime":
						return _confirmtime.ToString();
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
					case "WorkList":
						return _worklist.ToString();
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
						_sheetcode = value ;
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "SalesMan":
						int.TryParse(value, out _salesman);
						break;
					case "StandardPrice":
						int.TryParse(value, out _standardprice);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "DiscountAmount":
						decimal.TryParse(value, out _discountamount);
						break;
					case "WipeAmount":
						decimal.TryParse(value, out _wipeamount);
						break;
					case "ActAmount":
						decimal.TryParse(value, out _actamount);
						break;
					case "ArriveTime":
						DateTime.TryParse(value, out _arrivetime);
						break;
					case "ArriveWarehouse":
						int.TryParse(value, out _arrivewarehouse);
						break;
					case "SubmitTime":
						DateTime.TryParse(value, out _submittime);
						break;
					case "ConfirmTime":
						DateTime.TryParse(value, out _confirmtime);
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
					case "WorkList":
						int.TryParse(value, out _worklist);
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
