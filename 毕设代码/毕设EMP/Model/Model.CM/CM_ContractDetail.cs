// ===================================================================
// 文件： CM_ContractDetail.cs
// 项目名称：
// 创建时间：2011/3/9
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_ContractDetail数据实体类
	/// </summary>
	[Serializable]
	public class CM_ContractDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _contractid = 0;
		private int _accounttitle = 0;
		private decimal _amount = 0;
        private decimal _applylimit = 0;
		private int _feecycle = 0;
		private int _paymode = 0;
		private int _bearmode = 0;
		private decimal _bearpercent = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_ContractDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_ContractDetail(int id, int contractid, int accounttitle, decimal amount,decimal applylimit, int feecycle, int paymode, int bearmode, int bearpercent, string remark)
		{
			_id           = id;
			_contractid   = contractid;
			_accounttitle = accounttitle;
			_amount       = amount;
            _applylimit = applylimit;
			_feecycle     = feecycle;
			_paymode      = paymode;
			_bearmode     = bearmode;
			_bearpercent  = bearpercent;
			_remark       = remark;
			
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
		///ContractID
		///</summary>
		public int ContractID
		{
			get	{ return _contractid; }
			set	{ _contractid = value; }
		}

		///<summary>
		///AccountTitle
		///</summary>
		public int AccountTitle
		{
			get	{ return _accounttitle; }
			set	{ _accounttitle = value; }
		}

		///<summary>
		///Amount
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}

        public decimal ApplyLimit
        {
            get {return _applylimit;}
            set { _applylimit = value; }
        }

		///<summary>
		///FeeCycle
		///</summary>
		public int FeeCycle
		{
			get	{ return _feecycle; }
			set	{ _feecycle = value; }
		}

		///<summary>
		///PayMode
		///</summary>
		public int PayMode
		{
			get	{ return _paymode; }
			set	{ _paymode = value; }
		}

		///<summary>
		///BearMode
		///</summary>
		public int BearMode
		{
			get	{ return _bearmode; }
			set	{ _bearmode = value; }
		}

		///<summary>
		///BearPercent
		///</summary>
		public decimal BearPercent
		{
			get	{ return _bearpercent; }
			set	{ _bearpercent = value; }
		}

		///<summary>
		///Remark
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
            get { return "CM_ContractDetail"; }
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
					case "ContractID":
						return _contractid.ToString();
					case "AccountTitle":
						return _accounttitle.ToString();
					case "Amount":
						return _amount.ToString();
                    case "ApplyLimit":
                        return _applylimit.ToString();
					case "FeeCycle":
						return _feecycle.ToString();
					case "PayMode":
						return _paymode.ToString();
					case "BearMode":
						return _bearmode.ToString();
					case "BearPercent":
						return _bearpercent.ToString();
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
					case "ContractID":
						int.TryParse(value, out _contractid);
						break;
					case "AccountTitle":
						int.TryParse(value, out _accounttitle);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
                    case "ApplyLimit":
                        decimal.TryParse(value,out _applylimit);
                        break;
					case "FeeCycle":
						int.TryParse(value, out _feecycle);
						break;
					case "PayMode":
						int.TryParse(value, out _paymode);
						break;
					case "BearMode":
						int.TryParse(value, out _bearmode);
						break;
					case "BearPercent":
						decimal.TryParse(value, out _bearpercent);
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
