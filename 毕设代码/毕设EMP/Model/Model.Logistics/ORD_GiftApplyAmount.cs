// ===================================================================
// 文件： ORD_GiftApplyAmount.cs
// 项目名称：
// 创建时间：2011/12/12
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
	///ORD_GiftApplyAmount数据实体类
	/// </summary>
	[Serializable]
	public class ORD_GiftApplyAmount : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _accountmonth = 0;
		private int _client = 0;
		private int _brand = 0;
		private int _classify = 0;
		private decimal _feerate = 0;
        private decimal _salesvolume = 0;
		private decimal _availableamount = 0;
		private decimal _appliedamount = 0;
		private decimal _balanceamount = 0;
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
        private decimal _prebalance = 0;
        private decimal _deductibledmount = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_GiftApplyAmount()
		{
		}
		
		///<summary>
		///
		///</summary>
        public ORD_GiftApplyAmount(int id, int accountmonth, int client, int brand, int classify, decimal feerate, decimal availableamount, decimal appliedamount, decimal balanceamount, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, decimal prebalance, decimal deductibleamount)
		{
			_id              = id;
			_accountmonth    = accountmonth;
			_client          = client;
			_brand           = brand;
			_classify        = classify;
			_feerate         = feerate;
			_availableamount = availableamount;
			_appliedamount   = appliedamount;
			_balanceamount   = balanceamount;
			_remark          = remark;
			_approveflag     = approveflag;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
            _prebalance = prebalance;
            _deductibledmount = deductibleamount;
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
		///会计月
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///经销商
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///品牌
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///有导无导标志
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///FeeRate
		///</summary>
		public decimal FeeRate
		{
			get	{ return _feerate; }
			set	{ _feerate = value; }
		}

        /// <summary>
        /// SalesVolume
        /// </summary>
        public decimal SalesVolume
        {
            get { return _salesvolume; }
            set { _salesvolume = value; }
        }
        /// <summary>
        /// 赠品抵扣额
        /// </summary>
        public decimal DeductibleAmount
        {
            get { return _deductibledmount; }
            set { _deductibledmount = value; }
        }
        /// <summary>
        /// 上月余额
        /// </summary>
        public decimal PreBalance
        {
            get { return _prebalance; }
            set { _prebalance = value; }
        }
		///<summary>
		///AvailableAmount
		///</summary>
		public decimal AvailableAmount
		{
			get	{ return _availableamount; }
			set	{ _availableamount = value; }
		}

		///<summary>
		///AppliedAmount
		///</summary>
		public decimal AppliedAmount
		{
			get	{ return _appliedamount; }
			set	{ _appliedamount = value; }
		}

		///<summary>
		///BalanceAmount
		///</summary>
		public decimal BalanceAmount
		{
			get	{ return _balanceamount; }
			set	{ _balanceamount = value; }
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
            get { return "ORD_GiftApplyAmount"; }
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
					case "AccountMonth":
						return _accountmonth.ToString();
					case "Client":
						return _client.ToString();
					case "Brand":
						return _brand.ToString();
					case "Classify":
						return _classify.ToString();
					case "FeeRate":
						return _feerate.ToString();
                    case "SalesVolume":
						return _salesvolume.ToString();
					case "AvailableAmount":
						return _availableamount.ToString();
					case "AppliedAmount":
						return _appliedamount.ToString();
					case "BalanceAmount":
						return _balanceamount.ToString();
                    case "PreBalance":
                        return _prebalance.ToString();
                    case "DeductibleAmount":
                        return _deductibledmount.ToString();
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
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "FeeRate":
						decimal.TryParse(value, out _feerate);
						break;
                    case "SalesVolume":
						decimal.TryParse(value, out _salesvolume);
						break;
					case "AvailableAmount":
						decimal.TryParse(value, out _availableamount);
						break;
					case "AppliedAmount":
						decimal.TryParse(value, out _appliedamount);
						break;
					case "BalanceAmount":
						decimal.TryParse(value, out _balanceamount);
						break;
                    case "PreBalance":
                        decimal.TryParse(value, out _prebalance);
                        break;
                    case "DeductibleAmount":
                        decimal.TryParse(value, out _deductibledmount);
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
