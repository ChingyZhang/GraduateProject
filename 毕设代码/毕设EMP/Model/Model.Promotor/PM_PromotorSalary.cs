// ===================================================================
// 文件： PM_PromotorSalary.cs
// 项目名称：
// 创建时间：2011/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
	/// <summary>
	///PM_PromotorSalary数据实体类
	/// </summary>
	[Serializable]
	public class PM_PromotorSalary : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _promotor = 0;
		private int _basepaymode = 0;
		private decimal _basepay = 0;
		private int _basepaysubsidymode = 0;
		private decimal _basepaysubsidy = 0;
		private DateTime _basepaysubsidybegindate = new DateTime(1900,1,1);
		private DateTime _basepaysubsidyenddate = new DateTime(1900,1,1);
		private int _senioritypaymode = 0;
		private int _minimumwagemode = 0;
		private decimal _minimumwage = 0;
		private DateTime _minimumwagebegindate = new DateTime(1900,1,1);
		private DateTime _minimumwageenddate = new DateTime(1900,1,1);
		private int _insurancemode = 0;
		private int _insurancesubsidy = 0;
		private decimal _dibasepaysubsidy = 0;
		private decimal _difeesubsidy = 0;
        private decimal _rtmanagecost = 0;
		private int _state = 0;
		private string _remark = string.Empty;
		private int _approvetask = 0;
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
		public PM_PromotorSalary()
		{
		}
		
		///<summary>
		///
		///</summary>
        public PM_PromotorSalary(int id, int promotor, int basepaymode, decimal basepay, int basepaysubsidymode, decimal basepaysubsidy, DateTime basepaysubsidybegindate, DateTime basepaysubsidyenddate, int senioritypaymode, int minimumwagemode, decimal minimumwage, DateTime minimumwagebegindate, DateTime minimumwageenddate, int insurancemode, int insurancesubsidy, decimal dibasepaysubsidy, decimal difeesubsidy, int state, string remark, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id                      = id;
			_promotor                = promotor;
			_basepaymode             = basepaymode;
			_basepay                 = basepay;
			_basepaysubsidymode      = basepaysubsidymode;
			_basepaysubsidy          = basepaysubsidy;
			_basepaysubsidybegindate = basepaysubsidybegindate;
			_basepaysubsidyenddate   = basepaysubsidyenddate;
			_senioritypaymode        = senioritypaymode;
			_minimumwagemode         = minimumwagemode;
			_minimumwage             = minimumwage;
			_minimumwagebegindate    = minimumwagebegindate;
			_minimumwageenddate      = minimumwageenddate;
			_insurancemode           = insurancemode;
			_insurancesubsidy        = insurancesubsidy;
			_dibasepaysubsidy        = dibasepaysubsidy;
			_difeesubsidy            = difeesubsidy;
			_state                   = state;
			_remark                  = remark;
			_approvetask             = approvetask;
			_approveflag             = approveflag;
			_inserttime              = inserttime;
			_insertstaff             = insertstaff;
			_updatetime              = updatetime;
			_updatestaff             = updatestaff;
			
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
		///导购
		///</summary>
		public int Promotor
		{
			get	{ return _promotor; }
			set	{ _promotor = value; }
		}

		///<summary>
		///底薪模式
		///</summary>
		public int BasePayMode
		{
			get	{ return _basepaymode; }
			set	{ _basepaymode = value; }
		}

		///<summary>
		///标准底薪
		///</summary>
		public decimal BasePay
		{
			get	{ return _basepay; }
			set	{ _basepay = value; }
		}

		///<summary>
		///底薪补贴类型
		///</summary>
		public int BasePaySubsidyMode
		{
			get	{ return _basepaysubsidymode; }
			set	{ _basepaysubsidymode = value; }
		}

		///<summary>
		///底薪补贴金额
		///</summary>
		public decimal BasePaySubsidy
		{
			get	{ return _basepaysubsidy; }
			set	{ _basepaysubsidy = value; }
		}

		///<summary>
		///底薪补贴开始日期
		///</summary>
		public DateTime BasePaySubsidyBeginDate
		{
			get	{ return _basepaysubsidybegindate; }
			set	{ _basepaysubsidybegindate = value; }
		}

		///<summary>
		///底薪补贴截止日期
		///</summary>
		public DateTime BasePaySubsidyEndDate
		{
			get	{ return _basepaysubsidyenddate; }
			set	{ _basepaysubsidyenddate = value; }
		}

		///<summary>
		///工龄工资模式
		///</summary>
		public int SeniorityPayMode
		{
			get	{ return _senioritypaymode; }
			set	{ _senioritypaymode = value; }
		}

		///<summary>
		///保底工资类型
		///</summary>
		public int MinimumWageMode
		{
			get	{ return _minimumwagemode; }
			set	{ _minimumwagemode = value; }
		}

		///<summary>
		///保底工资金额
		///</summary>
		public decimal MinimumWage
		{
			get	{ return _minimumwage; }
			set	{ _minimumwage = value; }
		}

		///<summary>
		///保底工资开始日期
		///</summary>
		public DateTime MinimumWageBeginDate
		{
			get	{ return _minimumwagebegindate; }
			set	{ _minimumwagebegindate = value; }
		}

		///<summary>
		///保底工资截止日期
		///</summary>
		public DateTime MinimumWageEndDate
		{
			get	{ return _minimumwageenddate; }
			set	{ _minimumwageenddate = value; }
		}

		///<summary>
		///社保模式
		///</summary>
		public int InsuranceMode
		{
			get	{ return _insurancemode; }
			set	{ _insurancemode = value; }
		}

		///<summary>
		///社保补贴
		///</summary>
        public int InsuranceSubsidy
		{
			get	{ return _insurancesubsidy; }
			set	{ _insurancesubsidy = value; }
		}

		///<summary>
		///经销商底薪补贴
		///</summary>
		public decimal DIBasePaySubsidy
		{
			get	{ return _dibasepaysubsidy; }
			set	{ _dibasepaysubsidy = value; }
		}

		///<summary>
		///经销商费用补贴
		///</summary>
		public decimal DIFeeSubsidy
		{
			get	{ return _difeesubsidy; }
			set	{ _difeesubsidy = value; }
		}

        /// <summary>
        /// 门店导购管理费
        /// </summary>
        public decimal RTManageCost
        {
            get { return _rtmanagecost; }
            set { _rtmanagecost = value; }
        }
		///<summary>
		///薪资状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
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
		///批复工作流
		///</summary>
		public int ApproveTask
		{
			get	{ return _approvetask; }
			set	{ _approvetask = value; }
		}

		///<summary>
		///审批标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///录入日期
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
            get { return "PM_PromotorSalary"; }
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
					case "Promotor":
						return _promotor.ToString();
					case "BasePayMode":
						return _basepaymode.ToString();
					case "BasePay":
						return _basepay.ToString();
					case "BasePaySubsidyMode":
						return _basepaysubsidymode.ToString();
					case "BasePaySubsidy":
						return _basepaysubsidy.ToString();
					case "BasePaySubsidyBeginDate":
						return _basepaysubsidybegindate.ToString();
					case "BasePaySubsidyEndDate":
						return _basepaysubsidyenddate.ToString();
					case "SeniorityPayMode":
						return _senioritypaymode.ToString();
					case "MinimumWageMode":
						return _minimumwagemode.ToString();
					case "MinimumWage":
						return _minimumwage.ToString();
					case "MinimumWageBeginDate":
						return _minimumwagebegindate.ToString();
					case "MinimumWageEndDate":
						return _minimumwageenddate.ToString();
					case "InsuranceMode":
						return _insurancemode.ToString();
					case "InsuranceSubsidy":
						return _insurancesubsidy.ToString();
					case "DIBasePaySubsidy":
						return _dibasepaysubsidy.ToString();
					case "DIFeeSubsidy":
						return _difeesubsidy.ToString();
                    case "RTManageCost":
                        return _rtmanagecost.ToString();
					case "State":
						return _state.ToString();
					case "Remark":
						return _remark;
					case "ApproveTask":
						return _approvetask.ToString();
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
					case "Promotor":
						int.TryParse(value, out _promotor);
						break;
					case "BasePayMode":
						int.TryParse(value, out _basepaymode);
						break;
					case "BasePay":
						decimal.TryParse(value, out _basepay);
						break;
					case "BasePaySubsidyMode":
						int.TryParse(value, out _basepaysubsidymode);
						break;
					case "BasePaySubsidy":
						decimal.TryParse(value, out _basepaysubsidy);
						break;
					case "BasePaySubsidyBeginDate":
						DateTime.TryParse(value, out _basepaysubsidybegindate);
						break;
					case "BasePaySubsidyEndDate":
						DateTime.TryParse(value, out _basepaysubsidyenddate);
						break;
					case "SeniorityPayMode":
						int.TryParse(value, out _senioritypaymode);
						break;
					case "MinimumWageMode":
						int.TryParse(value, out _minimumwagemode);
						break;
					case "MinimumWage":
						decimal.TryParse(value, out _minimumwage);
						break;
					case "MinimumWageBeginDate":
						DateTime.TryParse(value, out _minimumwagebegindate);
						break;
					case "MinimumWageEndDate":
						DateTime.TryParse(value, out _minimumwageenddate);
						break;
					case "InsuranceMode":
						int.TryParse(value, out _insurancemode);
						break;
					case "InsuranceSubsidy":
                        int.TryParse(value, out _insurancesubsidy);
						break;
					case "DIBasePaySubsidy":
						decimal.TryParse(value, out _dibasepaysubsidy);
						break;
					case "DIFeeSubsidy":
						decimal.TryParse(value, out _difeesubsidy);
						break;
                    case "RTManageCost":
                        decimal.TryParse(value, out _rtmanagecost);
                        break;
					case "State":
						int.TryParse(value, out _state);
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
