// ===================================================================
// 文件： AC_CashFlowList.cs
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
	///AC_CashFlowList数据实体类
	/// </summary>
	[Serializable]
	public class AC_CashFlowList : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _ownerclient = 0;
		private int _tradeclient = 0;
		private DateTime _paydate = new DateTime(1900,1,1);
		private int _agentstaff = 0;
		private int _paymode = 0;
		private int _payclassify = 0;
		private int _accounttitle = 0;
		private decimal _amount = 0;
		private int _cashaccount = 0;
		private int _confirmstate = 0;
		private int _confirmstaff = 0;
		private DateTime _confirmdate = new DateTime(1900,1,1);
		private int _relateorderid = 0;
		private int _relatedeliveryid = 0;
		private string _remark = string.Empty;
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
		public AC_CashFlowList()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_CashFlowList(int id, int ownerclient, int tradeclient, DateTime paydate, int agentstaff, int paymode, int payclassify, int accounttitle, decimal amount, int cashaccount, int confirmstate, int confirmstaff, DateTime confirmdate, int relateorderid, int relatedeliveryid, string remark, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, int worklist)
		{
			_id               = id;
			_ownerclient      = ownerclient;
			_tradeclient      = tradeclient;
			_paydate          = paydate;
			_agentstaff       = agentstaff;
			_paymode          = paymode;
			_payclassify      = payclassify;
			_accounttitle     = accounttitle;
			_amount           = amount;
			_cashaccount      = cashaccount;
			_confirmstate     = confirmstate;
			_confirmstaff     = confirmstaff;
			_confirmdate      = confirmdate;
			_relateorderid    = relateorderid;
			_relatedeliveryid = relatedeliveryid;
			_remark           = remark;
			_inserttime       = inserttime;
			_insertstaff      = insertstaff;
			_updatetime       = updatetime;
			_updatestaff      = updatestaff;
			_worklist         = worklist;
			
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
		///收款日期
		///</summary>
		public DateTime PayDate
		{
			get	{ return _paydate; }
			set	{ _paydate = value; }
		}

		///<summary>
		///经办人
		///</summary>
		public int AgentStaff
		{
			get	{ return _agentstaff; }
			set	{ _agentstaff = value; }
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
		///类别
		///</summary>
		public int PayClassify
		{
			get	{ return _payclassify; }
			set	{ _payclassify = value; }
		}

		///<summary>
		///会计科目
		///</summary>
		public int AccountTitle
		{
			get	{ return _accounttitle; }
			set	{ _accounttitle = value; }
		}

		///<summary>
		///收款金额
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}

		///<summary>
		///收款财务账户
		///</summary>
		public int CashAccount
		{
			get	{ return _cashaccount; }
			set	{ _cashaccount = value; }
		}

		///<summary>
		///交款标志
		///</summary>
		public int ConfirmState
		{
			get	{ return _confirmstate; }
			set	{ _confirmstate = value; }
		}

		///<summary>
		///交款人
		///</summary>
		public int ConfirmStaff
		{
			get	{ return _confirmstaff; }
			set	{ _confirmstaff = value; }
		}

		///<summary>
		///交款日期
		///</summary>
		public DateTime ConfirmDate
		{
			get	{ return _confirmdate; }
			set	{ _confirmdate = value; }
		}

		///<summary>
		///关联订货单
		///</summary>
		public int RelateOrderId
		{
			get	{ return _relateorderid; }
			set	{ _relateorderid = value; }
		}

		///<summary>
		///关联发货单
		///</summary>
		public int RelateDeliveryId
		{
			get	{ return _relatedeliveryid; }
			set	{ _relatedeliveryid = value; }
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
            get { return "AC_CashFlowList"; }
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
					case "PayDate":
						return _paydate.ToString();
					case "AgentStaff":
						return _agentstaff.ToString();
					case "PayMode":
						return _paymode.ToString();
					case "PayClassify":
						return _payclassify.ToString();
					case "AccountTitle":
						return _accounttitle.ToString();
					case "Amount":
						return _amount.ToString();
					case "CashAccount":
						return _cashaccount.ToString();
					case "ConfirmState":
						return _confirmstate.ToString();
					case "ConfirmStaff":
						return _confirmstaff.ToString();
					case "ConfirmDate":
						return _confirmdate.ToString();
					case "RelateOrderId":
						return _relateorderid.ToString();
					case "RelateDeliveryId":
						return _relatedeliveryid.ToString();
					case "Remark":
						return _remark;
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
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
						break;
					case "TradeClient":
						int.TryParse(value, out _tradeclient);
						break;
					case "PayDate":
						DateTime.TryParse(value, out _paydate);
						break;
					case "AgentStaff":
						int.TryParse(value, out _agentstaff);
						break;
					case "PayMode":
						int.TryParse(value, out _paymode);
						break;
					case "PayClassify":
						int.TryParse(value, out _payclassify);
						break;
					case "AccountTitle":
						int.TryParse(value, out _accounttitle);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
					case "CashAccount":
						int.TryParse(value, out _cashaccount);
						break;
					case "ConfirmState":
						int.TryParse(value, out _confirmstate);
						break;
					case "ConfirmStaff":
						int.TryParse(value, out _confirmstaff);
						break;
					case "ConfirmDate":
						DateTime.TryParse(value, out _confirmdate);
						break;
					case "RelateOrderId":
						int.TryParse(value, out _relateorderid);
						break;
					case "RelateDeliveryId":
						int.TryParse(value, out _relatedeliveryid);
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
