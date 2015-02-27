// ===================================================================
// 文件： FNA_AmountReceivableChangeHistory.cs
// 项目名称：
// 创建时间：2009/5/16
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
	/// <summary>
	///FNA_AmountReceivableChangeHistory数据实体类
	/// </summary>
	[Serializable]
	public class FNA_AmountReceivableChangeHistory : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _accountmonth = 0;
		private decimal _changeamount = 0;
		private decimal _balanceamount = 0;
		private int _changetype = 0;
		private int _debitcreditflag = 0;
		private int _changestaff = 0;
		private DateTime _changetime = new DateTime(1900,1,1);
		private string _relatedinfo = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_AmountReceivableChangeHistory()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_AmountReceivableChangeHistory(int id, int client, int accountmonth, decimal changeamount, decimal balanceamount, int changetype, int debitcreditflag, int changestaff, DateTime changetime, string relatedinfo)
		{
			_id              = id;
			_client          = client;
			_accountmonth    = accountmonth;
			_changeamount    = changeamount;
			_balanceamount   = balanceamount;
			_changetype      = changetype;
			_debitcreditflag = debitcreditflag;
			_changestaff     = changestaff;
			_changetime      = changetime;
			_relatedinfo     = relatedinfo;
			
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
		///客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///会计月
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///发生金额
		///</summary>
		public decimal ChangeAmount
		{
			get	{ return _changeamount; }
			set	{ _changeamount = value; }
		}

		///<summary>
		///应收账款余额
		///</summary>
		public decimal BalanceAmount
		{
			get	{ return _balanceamount; }
			set	{ _balanceamount = value; }
		}

		///<summary>
		///变动类型
		///</summary>
		public int ChangeType
		{
			get	{ return _changetype; }
			set	{ _changetype = value; }
		}

		///<summary>
		///借贷标志
		///</summary>
		public int DebitCreditFlag
		{
			get	{ return _debitcreditflag; }
			set	{ _debitcreditflag = value; }
		}

		///<summary>
		///变更人
		///</summary>
		public int ChangeStaff
		{
			get	{ return _changestaff; }
			set	{ _changestaff = value; }
		}

		///<summary>
		///变更时间
		///</summary>
		public DateTime ChangeTime
		{
			get	{ return _changetime; }
			set	{ _changetime = value; }
		}

		///<summary>
		///关联信息
		///</summary>
		public string RelatedInfo
		{
			get	{ return _relatedinfo; }
			set	{ _relatedinfo = value; }
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
            get { return "FNA_AmountReceivableChangeHistory"; }
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
					case "AccountMonth":
						return _accountmonth.ToString();
					case "ChangeAmount":
						return _changeamount.ToString();
					case "BalanceAmount":
						return _balanceamount.ToString();
					case "ChangeType":
						return _changetype.ToString();
					case "DebitCreditFlag":
						return _debitcreditflag.ToString();
					case "ChangeStaff":
						return _changestaff.ToString();
					case "ChangeTime":
						return _changetime.ToString();
					case "RelatedInfo":
						return _relatedinfo;
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
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "ChangeAmount":
						decimal.TryParse(value, out _changeamount);
						break;
					case "BalanceAmount":
						decimal.TryParse(value, out _balanceamount);
						break;
					case "ChangeType":
						int.TryParse(value, out _changetype);
						break;
					case "DebitCreditFlag":
						int.TryParse(value, out _debitcreditflag);
						break;
					case "ChangeStaff":
						int.TryParse(value, out _changestaff);
						break;
					case "ChangeTime":
						DateTime.TryParse(value, out _changetime);
						break;
					case "RelatedInfo":
						_relatedinfo = value ;
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
