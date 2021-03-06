﻿// ===================================================================
// 文件： ORD_Delivery.cs
// 项目名称：
// 创建时间：2012-7-22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_Delivery数据实体类
	/// </summary>
	[Serializable]
	public class ORD_Delivery : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _supplier = 0;
		private int _client = 0;
		private int _supplierwarehouse = 0;
		private int _clientwarehouse = 0;
		private int _classify = 0;
		private int _preparemode = 0;
		private int _state = 0;
		private int _orderid = 0;
		private int _truckingid = 0;
		private DateTime _prearrivaldate = new DateTime(1900,1,1);
		private DateTime _loadingtime = new DateTime(1900,1,1);
		private DateTime _departtime = new DateTime(1900,1,1);
		private DateTime _actarrivetime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
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
		public ORD_Delivery()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_Delivery(int id, string sheetcode, int supplier, int client, int supplierwarehouse, int clientwarehouse, int classify, int prepairmode, int state, int orderid, int truckingid, DateTime prearrivaldate, DateTime loadingtime, DateTime departtime, DateTime actarrivetime, string remark, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id                = id;
			_sheetcode         = sheetcode;
			_supplier          = supplier;
			_client            = client;
			_supplierwarehouse = supplierwarehouse;
			_clientwarehouse   = clientwarehouse;
			_classify          = classify;
			_preparemode       = prepairmode;
			_state             = state;
			_orderid           = orderid;
			_truckingid        = truckingid;
			_prearrivaldate    = prearrivaldate;
			_loadingtime       = loadingtime;
			_departtime        = departtime;
			_actarrivetime     = actarrivetime;
			_remark            = remark;
			_approvetask       = approvetask;
			_approveflag       = approveflag;
			_inserttime        = inserttime;
			_insertuser        = insertuser;
			_updatetime        = updatetime;
			_updateuser        = updateuser;
			
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
		///发放单号
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
		///请购商
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///供货商仓库
		///</summary>
		public int SupplierWareHouse
		{
			get	{ return _supplierwarehouse; }
			set	{ _supplierwarehouse = value; }
		}

		///<summary>
		///请购商仓库
		///</summary>
		public int ClientWareHouse
		{
			get	{ return _clientwarehouse; }
			set	{ _clientwarehouse = value; }
		}

		///<summary>
		///单据类别
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///备单标志
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
		///OrderID
		///</summary>
		public int OrderID
		{
			get	{ return _orderid; }
			set	{ _orderid = value; }
		}

		///<summary>
		///装车单ID
		///</summary>
		public int TruckingID
		{
			get	{ return _truckingid; }
			set	{ _truckingid = value; }
		}

		///<summary>
		///预计到货日期
		///</summary>
		public DateTime PreArrivalDate
		{
			get	{ return _prearrivaldate; }
			set	{ _prearrivaldate = value; }
		}

		///<summary>
		///装车时间
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
		///实际到货时间
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
            get { return "ORD_Delivery"; }
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
					case "Classify":
						return _classify.ToString();
					case "PrepareMode":
						return _preparemode.ToString();
					case "State":
						return _state.ToString();
					case "OrderID":
						return _orderid.ToString();
					case "TruckingID":
						return _truckingid.ToString();
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
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "PrepareMode":
						int.TryParse(value, out _preparemode);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "OrderID":
						int.TryParse(value, out _orderid);
						break;
					case "TruckingID":
						int.TryParse(value, out _truckingid);
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
