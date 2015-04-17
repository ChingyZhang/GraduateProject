// ===================================================================
// 文件： AC_BalanceUsageList.cs
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
	///AC_BalanceUsageList数据实体类
	/// </summary>
	[Serializable]
	public class AC_BalanceUsageList : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _ownerclient = 0;
		private int _tradeclient = 0;
		private decimal _amount = 0;
		private decimal _balance = 0;
		private int _cashflowid = 0;
		private int _deliveryid = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_BalanceUsageList()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_BalanceUsageList(int id, int ownerclient, int tradeclient, decimal amount, decimal balance, int cashflowid, int deliveryid, string remark, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_ownerclient  = ownerclient;
			_tradeclient  = tradeclient;
			_amount       = amount;
			_balance      = balance;
			_cashflowid   = cashflowid;
			_deliveryid   = deliveryid;
			_remark       = remark;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			
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
		///经销商
		///</summary>
		public int OwnerClient
		{
			get	{ return _ownerclient; }
			set	{ _ownerclient = value; }
		}

		///<summary>
		///往来客户
		///</summary>
		public int TradeClient
		{
			get	{ return _tradeclient; }
			set	{ _tradeclient = value; }
		}

		///<summary>
		///变动金额
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}

		///<summary>
		///变动后余额
		///</summary>
		public decimal Balance
		{
			get	{ return _balance; }
			set	{ _balance = value; }
		}

		///<summary>
		///收付款单ID
		///</summary>
		public int CashFlowId
		{
			get	{ return _cashflowid; }
			set	{ _cashflowid = value; }
		}

		///<summary>
		///关联发货单
		///</summary>
		public int DeliveryId
		{
			get	{ return _deliveryid; }
			set	{ _deliveryid = value; }
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
            get { return "AC_BalanceUsageList"; }
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
					case "OwnerClient":
						return _ownerclient.ToString();
					case "TradeClient":
						return _tradeclient.ToString();
					case "Amount":
						return _amount.ToString();
					case "Balance":
						return _balance.ToString();
					case "CashFlowId":
						return _cashflowid.ToString();
					case "DeliveryId":
						return _deliveryid.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
						break;
					case "TradeClient":
						int.TryParse(value, out _tradeclient);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
					case "Balance":
						decimal.TryParse(value, out _balance);
						break;
					case "CashFlowId":
						int.TryParse(value, out _cashflowid);
						break;
					case "DeliveryId":
						int.TryParse(value, out _deliveryid);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
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
