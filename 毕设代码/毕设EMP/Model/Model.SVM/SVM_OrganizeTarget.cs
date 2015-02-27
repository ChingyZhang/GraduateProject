// ===================================================================
// 文件： SVM_OrganizeTarget.cs
// 项目名称：
// 创建时间：2013/4/7
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_OrganizeTarget数据实体类
	/// </summary>
	[Serializable]
	public class SVM_OrganizeTarget : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _accountmonth = 0;
		private int _organizecity = 0;
		private decimal _salestarget = 0;
        private decimal _salestargetadjust = 0;
		private decimal _feeratetarget = 0;
        private decimal _feeyieldrate = 0;
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
		public SVM_OrganizeTarget()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_OrganizeTarget(int id, int accountmonth, int organizecity, decimal salestarget,decimal salestargetadjust, decimal feeratetarget,decimal feeyieldrate, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_accountmonth  = accountmonth;
			_organizecity  = organizecity;
			_salestarget   = salestarget;
            _salestargetadjust = salestargetadjust;
			_feeratetarget = feeratetarget;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			_updatetime    = updatetime;
			_updatestaff   = updatestaff;
            _feeyieldrate = feeyieldrate;
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
		///AccountMonth
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///OrganizeCity
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

        public decimal FeeYieldRate
        {
            get { return _feeyieldrate; }
            set { _feeyieldrate = value; }
        }
		///<summary>
		///SalesTarget
		///</summary>
		public decimal SalesTarget
		{
			get	{ return _salestarget; }
			set	{ _salestarget = value; }
		}

        public decimal SalesTargetAdjust
        {
            get { return _salestargetadjust; }
            set { _salestargetadjust = value; }
        }
		///<summary>
		///FeeRateTarget
		///</summary>
		public decimal FeeRateTarget
		{
			get	{ return _feeratetarget; }
			set	{ _feeratetarget = value; }
		}

		///<summary>
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///UpdateTime
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///UpdateStaff
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
            get { return "SVM_OrganizeTarget"; }
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
					case "OrganizeCity":
						return _organizecity.ToString();
					case "SalesTarget":
						return _salestarget.ToString();
                    case "SalesTargetAdjust":
                        return _salestargetadjust.ToString();
					case "FeeRateTarget":
						return _feeratetarget.ToString();
                    case "FeeYieldRate":
                        return _feeyieldrate.ToString();
                        break;
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "SalesTarget":
						decimal.TryParse(value, out _salestarget);
						break;
                    case "SalesTargetAdjust":
                        decimal.TryParse(value, out _salestargetadjust);
                        break;
					case "FeeRateTarget":
						decimal.TryParse(value, out _feeratetarget);
						break;
                    case "FeeYieldRate":
						decimal.TryParse(value, out _feeyieldrate);
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
