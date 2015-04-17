// ===================================================================
// 文件： AC_CashAccount.cs
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
	///AC_CashAccount数据实体类
	/// </summary>
	[Serializable]
	public class AC_CashAccount : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _ownerclient = 0;
		private string _name = string.Empty;
		private int _cashaccounttype = 0;
		private string _bankname = string.Empty;
		private string _bankaccount = string.Empty;
		private string _bankaccountname = string.Empty;
		private decimal _accountbalance = 0;
		private string _remark = string.Empty;
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
		public AC_CashAccount()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_CashAccount(int id, int ownerclient, string name, int cashaccounttype, string bankname, string bankaccount, string bankaccountname, decimal accountbalance, string remark, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id              = id;
			_ownerclient     = ownerclient;
			_name            = name;
			_cashaccounttype = cashaccounttype;
			_bankname        = bankname;
			_bankaccount     = bankaccount;
			_bankaccountname = bankaccountname;
			_accountbalance  = accountbalance;
			_remark          = remark;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
			
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
		///名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///账户类别
		///</summary>
		public int CashAccountType
		{
			get	{ return _cashaccounttype; }
			set	{ _cashaccounttype = value; }
		}

		///<summary>
		///开户行
		///</summary>
		public string BankName
		{
			get	{ return _bankname; }
			set	{ _bankname = value; }
		}

		///<summary>
		///账号
		///</summary>
		public string BankAccount
		{
			get	{ return _bankaccount; }
			set	{ _bankaccount = value; }
		}

		///<summary>
		///开户名
		///</summary>
		public string BankAccountName
		{
			get	{ return _bankaccountname; }
			set	{ _bankaccountname = value; }
		}

		///<summary>
		///账户余额
		///</summary>
		public decimal AccountBalance
		{
			get	{ return _accountbalance; }
			set	{ _accountbalance = value; }
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
            get { return "AC_CashAccount"; }
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
					case "Name":
						return _name;
					case "CashAccountType":
						return _cashaccounttype.ToString();
					case "BankName":
						return _bankname;
					case "BankAccount":
						return _bankaccount;
					case "BankAccountName":
						return _bankaccountname;
					case "AccountBalance":
						return _accountbalance.ToString();
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
					case "Name":
						_name = value ;
						break;
					case "CashAccountType":
						int.TryParse(value, out _cashaccounttype);
						break;
					case "BankName":
						_bankname = value ;
						break;
					case "BankAccount":
						_bankaccount = value ;
						break;
					case "BankAccountName":
						_bankaccountname = value ;
						break;
					case "AccountBalance":
						decimal.TryParse(value, out _accountbalance);
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
