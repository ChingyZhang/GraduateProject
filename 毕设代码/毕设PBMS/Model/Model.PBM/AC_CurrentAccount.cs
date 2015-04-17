// ===================================================================
// 文件： AC_CurrentAccount.cs
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
	///AC_CurrentAccount数据实体类
	/// </summary>
	[Serializable]
	public class AC_CurrentAccount : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _ownerclient = 0;
		private int _tradeclient = 0;
		private decimal _prepaymentamount = 0;
		private decimal _ap = 0;
		private decimal _prepaymentbalance = 0;
		private decimal _prereceivedamount = 0;
		private decimal _ar = 0;
		private decimal _prereceivedbalance = 0;
		private DateTime _lastupdatetime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_CurrentAccount()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_CurrentAccount(int id, int ownerclient, int tradeclient, decimal prepaymentamount, decimal ap, decimal prepaymentbalance, decimal prereceivedamount, decimal ar, decimal prereceivedbalance, DateTime lastupdatetime, string remark)
		{
			_id                 = id;
			_ownerclient        = ownerclient;
			_tradeclient        = tradeclient;
			_prepaymentamount   = prepaymentamount;
			_ap                 = ap;
			_prepaymentbalance  = prepaymentbalance;
			_prereceivedamount  = prereceivedamount;
			_ar                 = ar;
			_prereceivedbalance = prereceivedbalance;
			_lastupdatetime     = lastupdatetime;
			_remark             = remark;
			
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
		///预付款
		///</summary>
		public decimal PrePaymentAmount
		{
			get	{ return _prepaymentamount; }
			set	{ _prepaymentamount = value; }
		}

		///<summary>
		///应付款
		///</summary>
		public decimal AP
		{
			get	{ return _ap; }
			set	{ _ap = value; }
		}

		///<summary>
		///预付余额
		///</summary>
		public decimal PrePaymentBalance
		{
			get	{ return _prepaymentbalance; }
			set	{ _prepaymentbalance = value; }
		}

		///<summary>
		///预收款
		///</summary>
		public decimal PreReceivedAmount
		{
			get	{ return _prereceivedamount; }
			set	{ _prereceivedamount = value; }
		}

		///<summary>
		///应收款
		///</summary>
		public decimal AR
		{
			get	{ return _ar; }
			set	{ _ar = value; }
		}

		///<summary>
		///预收余额
		///</summary>
		public decimal PreReceivedBalance
		{
			get	{ return _prereceivedbalance; }
			set	{ _prereceivedbalance = value; }
		}

		///<summary>
		///最后更新时间
		///</summary>
		public DateTime LastUpdateTime
		{
			get	{ return _lastupdatetime; }
			set	{ _lastupdatetime = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "AC_CurrentAccount"; }
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
					case "PrePaymentAmount":
						return _prepaymentamount.ToString();
					case "AP":
						return _ap.ToString();
					case "PrePaymentBalance":
						return _prepaymentbalance.ToString();
					case "PreReceivedAmount":
						return _prereceivedamount.ToString();
					case "AR":
						return _ar.ToString();
					case "PreReceivedBalance":
						return _prereceivedbalance.ToString();
					case "LastUpdateTime":
						return _lastupdatetime.ToString();
					case "Remark":
						return _remark;
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
					case "PrePaymentAmount":
						decimal.TryParse(value, out _prepaymentamount);
						break;
					case "AP":
						decimal.TryParse(value, out _ap);
						break;
					case "PrePaymentBalance":
						decimal.TryParse(value, out _prepaymentbalance);
						break;
					case "PreReceivedAmount":
						decimal.TryParse(value, out _prereceivedamount);
						break;
					case "AR":
						decimal.TryParse(value, out _ar);
						break;
					case "PreReceivedBalance":
						decimal.TryParse(value, out _prereceivedbalance);
						break;
					case "LastUpdateTime":
						DateTime.TryParse(value, out _lastupdatetime);
						break;
					case "Remark":
						_remark = value ;
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
