// ===================================================================
// 文件： ORD_OrderDelivery.cs
// 项目名称：
// 创建时间：2010/7/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
	/// <summary>
	///ORD_OrderDelivery数据实体类
	/// </summary>
	[Serializable]
	public class ORD_OrderDelivery : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _accountmonth = 0;
		private int _organizecity = 0;
		private int _state = 0;
		private int _client = 0;
		private int _store = 0;
		private DateTime _prearrivaldate = new DateTime(1900,1,1);
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
		public ORD_OrderDelivery()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_OrderDelivery(int id, string sheetcode, int accountmonth, int organizecity, int state, int client, int store, DateTime prearrivaldate, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id             = id;
			_sheetcode      = sheetcode;
			_accountmonth   = accountmonth;
			_organizecity   = organizecity;
			_state          = state;
			_client         = client;
			_store          = store;
			_prearrivaldate = prearrivaldate;
			_approveflag    = approveflag;
			_inserttime     = inserttime;
			_insertstaff    = insertstaff;
			_updatetime     = updatetime;
			_updatestaff    = updatestaff;
			
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
		///结算月
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
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
		///收货客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///发货仓库
		///</summary>
		public int Store
		{
			get	{ return _store; }
			set	{ _store = value; }
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
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
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
            get { return "ORD_OrderDelivery"; }
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
					case "OrganizeCity":
						return _organizecity.ToString();
					case "State":
						return _state.ToString();
					case "Client":
						return _client.ToString();
					case "Store":
						return _store.ToString();
					case "PreArrivalDate":
						return _prearrivaldate.ToString();
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
					case "SheetCode":
						_sheetcode = value ;
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Store":
						int.TryParse(value, out _store);
						break;
					case "PreArrivalDate":
						DateTime.TryParse(value, out _prearrivaldate);
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
