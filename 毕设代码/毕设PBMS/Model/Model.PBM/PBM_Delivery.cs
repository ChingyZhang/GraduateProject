// ===================================================================
// 文件： PBM_Delivery.cs
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
	///PBM_Delivery数据实体类
	/// </summary>
	[Serializable]
	public class PBM_Delivery : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _supplier = 0;
		private int _client = 0;
		private int _supplierwarehouse = 0;
		private int _clientwarehouse = 0;
		private int _salesman = 0;
        private int _deliveryman = 0;
		private int _classify = 0;
		private int _preparemode = 0;
		private int _state = 0;
		private int _standardprice = 0;
		private int _orderid = 0;
		private decimal _discountamount = 0;
		private decimal _wipeamount = 0;
		private decimal _actamount = 0;
		private int _deliveryvehicle = 0;
		private DateTime _prearrivaldate = new DateTime(1900,1,1);
		private DateTime _loadingtime = new DateTime(1900,1,1);
		private DateTime _departtime = new DateTime(1900,1,1);
		private DateTime _actarrivetime = new DateTime(1900,1,1);
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
		public PBM_Delivery()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PBM_Delivery(int id, string sheetcode, int supplier, int client, int supplierwarehouse, int clientwarehouse, int salesman, int classify, int preparemode, int state, int standardprice, int orderid, decimal discountamount, decimal wipeamount, decimal actamount, int deliveryvehicle, DateTime prearrivaldate, DateTime loadingtime, DateTime departtime, DateTime actarrivetime, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, int worklist)
		{
			_id                = id;
			_sheetcode         = sheetcode;
			_supplier          = supplier;
			_client            = client;
			_supplierwarehouse = supplierwarehouse;
			_clientwarehouse   = clientwarehouse;
			_salesman          = salesman;
			_classify          = classify;
			_preparemode       = preparemode;
			_state             = state;
			_standardprice     = standardprice;
			_orderid           = orderid;
			_discountamount    = discountamount;
			_wipeamount        = wipeamount;
			_actamount         = actamount;
			_deliveryvehicle   = deliveryvehicle;
			_prearrivaldate    = prearrivaldate;
			_loadingtime       = loadingtime;
			_departtime        = departtime;
			_actarrivetime     = actarrivetime;
			_remark            = remark;
			_approveflag       = approveflag;
			_inserttime        = inserttime;
			_insertstaff       = insertstaff;
			_updatetime        = updatetime;
			_updatestaff       = updatestaff;
			_worklist          = worklist;
			
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
		///单号
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
		}

		///<summary>
		///发货商
		///</summary>
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
		}

		///<summary>
		///收货商
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///发货商仓库
		///</summary>
		public int SupplierWareHouse
		{
			get	{ return _supplierwarehouse; }
			set	{ _supplierwarehouse = value; }
		}

		///<summary>
		///收货商仓库
		///</summary>
		public int ClientWareHouse
		{
			get	{ return _clientwarehouse; }
			set	{ _clientwarehouse = value; }
		}

		///<summary>
		///SalesMan
		///</summary>
		public int SalesMan
		{
			get	{ return _salesman; }
			set	{ _salesman = value; }
		}

        /// <summary>
        /// 送货人
        /// </summary>
        public int DeliveryMan
        {
            get { return _deliveryman; }
            set { _deliveryman = value; }
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
		///制单标志
		///</summary>
		public int PrepareMode
		{
			get	{ return _preparemode; }
			set	{ _preparemode = value; }
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
		///供货价盘表
		///</summary>
		public int StandardPrice
		{
			get	{ return _standardprice; }
			set	{ _standardprice = value; }
		}

		///<summary>
		///关联订货单
		///</summary>
		public int OrderId
		{
			get	{ return _orderid; }
			set	{ _orderid = value; }
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
		///实际销售金额
		///</summary>
		public decimal ActAmount
		{
			get	{ return _actamount; }
			set	{ _actamount = value; }
		}

		///<summary>
		///送货车辆
		///</summary>
		public int DeliveryVehicle
		{
			get	{ return _deliveryvehicle; }
			set	{ _deliveryvehicle = value; }
		}

		///<summary>
		///预计送达时间
		///</summary>
		public DateTime PreArrivalDate
		{
			get	{ return _prearrivaldate; }
			set	{ _prearrivaldate = value; }
		}

		///<summary>
		///装车配货时间
		///</summary>
		public DateTime LoadingTime
		{
			get	{ return _loadingtime; }
			set	{ _loadingtime = value; }
		}

		///<summary>
		///发车时间
		///</summary>
		public DateTime DepartTime
		{
			get	{ return _departtime; }
			set	{ _departtime = value; }
		}

		///<summary>
		///实际到达时间
		///</summary>
		public DateTime ActArriveTime
		{
			get	{ return _actarrivetime; }
			set	{ _actarrivetime = value; }
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
            get { return "PBM_Delivery"; }
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
					case "SupplierWareHouse":
						return _supplierwarehouse.ToString();
					case "ClientWareHouse":
						return _clientwarehouse.ToString();
					case "SalesMan":
						return _salesman.ToString();
                    case "DeliveryMan":
                        return _deliveryman.ToString();
					case "Classify":
						return _classify.ToString();
					case "PrepareMode":
						return _preparemode.ToString();
					case "State":
						return _state.ToString();
					case "StandardPrice":
						return _standardprice.ToString();
					case "OrderId":
						return _orderid.ToString();
					case "DiscountAmount":
						return _discountamount.ToString();
					case "WipeAmount":
						return _wipeamount.ToString();
					case "ActAmount":
						return _actamount.ToString();
					case "DeliveryVehicle":
						return _deliveryvehicle.ToString();
					case "PreArrivalDate":
						return _prearrivaldate.ToString();
					case "LoadingTime":
						return _loadingtime.ToString();
					case "DepartTime":
						return _departtime.ToString();
					case "ActArriveTime":
						return _actarrivetime.ToString();
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
					case "SupplierWareHouse":
						int.TryParse(value, out _supplierwarehouse);
						break;
					case "ClientWareHouse":
						int.TryParse(value, out _clientwarehouse);
						break;
					case "SalesMan":
						int.TryParse(value, out _salesman);
						break;
                    case "DeliveryMan":
                        int.TryParse(value, out _deliveryman);
                        break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "PrepareMode":
						int.TryParse(value, out _preparemode);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "StandardPrice":
						int.TryParse(value, out _standardprice);
						break;
					case "OrderId":
						int.TryParse(value, out _orderid);
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
					case "DeliveryVehicle":
						int.TryParse(value, out _deliveryvehicle);
						break;
					case "PreArrivalDate":
						DateTime.TryParse(value, out _prearrivaldate);
						break;
					case "LoadingTime":
						DateTime.TryParse(value, out _loadingtime);
						break;
					case "DepartTime":
						DateTime.TryParse(value, out _departtime);
						break;
					case "ActArriveTime":
						DateTime.TryParse(value, out _actarrivetime);
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
