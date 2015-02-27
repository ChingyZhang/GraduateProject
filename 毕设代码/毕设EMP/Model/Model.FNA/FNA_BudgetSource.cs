// ===================================================================
// 文件： FNA_BudgetSource.cs
// 项目名称：
// 创建时间：2010/7/25
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
	///FNA_BudgetSource数据实体类
	/// </summary>
	[Serializable]
	public class FNA_BudgetSource : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _accountmonth = 0;
		private int _organizecity = 0;
		private decimal _basevolume = 0;
		private decimal _planvolume = 0;
		private decimal _basebudget = 0;
		private decimal _overfullbudget = 0;
		private decimal _retentionbudget = 0;
		private string _remark = string.Empty;
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
		public FNA_BudgetSource()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_BudgetSource(int id, int accountmonth, int organizecity, decimal basevolume, decimal planvolume, decimal basebudget, decimal overfullbudget, decimal retentionbudget, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id              = id;
			_accountmonth    = accountmonth;
			_organizecity    = organizecity;
			_basevolume      = basevolume;
			_planvolume      = planvolume;
			_basebudget      = basebudget;
			_overfullbudget  = overfullbudget;
			_retentionbudget = retentionbudget;
			_remark          = remark;
			_approveflag     = approveflag;
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
		///会计月
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
		///基础销量
		///</summary>
		public decimal BaseVolume
		{
			get	{ return _basevolume; }
			set	{ _basevolume = value; }
		}

		///<summary>
		///计划销量
		///</summary>
		public decimal PlanVolume
		{
			get	{ return _planvolume; }
			set	{ _planvolume = value; }
		}

		///<summary>
		///费用预算额度
		///</summary>
		public decimal BaseBudget
		{
			get	{ return _basebudget; }
			set	{ _basebudget = value; }
		}

		///<summary>
		///增量费用
		///</summary>
		public decimal OverFullBudget
		{
			get	{ return _overfullbudget; }
			set	{ _overfullbudget = value; }
		}

		///<summary>
		///自留费用
		///</summary>
		public decimal RetentionBudget
		{
			get	{ return _retentionbudget; }
			set	{ _retentionbudget = value; }
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
            get { return "FNA_BudgetSource"; }
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
					case "BaseVolume":
						return _basevolume.ToString();
					case "PlanVolume":
						return _planvolume.ToString();
					case "BaseBudget":
						return _basebudget.ToString();
					case "OverFullBudget":
						return _overfullbudget.ToString();
					case "RetentionBudget":
						return _retentionbudget.ToString();
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "BaseVolume":
						decimal.TryParse(value, out _basevolume);
						break;
					case "PlanVolume":
						decimal.TryParse(value, out _planvolume);
						break;
					case "BaseBudget":
						decimal.TryParse(value, out _basebudget);
						break;
					case "OverFullBudget":
						decimal.TryParse(value, out _overfullbudget);
						break;
					case "RetentionBudget":
						decimal.TryParse(value, out _retentionbudget);
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
