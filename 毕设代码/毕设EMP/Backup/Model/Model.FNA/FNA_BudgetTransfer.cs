// ===================================================================
// 文件： FNA_BudgetTransfer.cs
// 项目名称：
// 创建时间：2010/5/15
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
	///FNA_BudgetTransfer数据实体类
	/// </summary>
	[Serializable]
	public class FNA_BudgetTransfer : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _fromorganizecity = 0;
		private int _toorganizecity = 0;
		private int _fromaccountmonth = 0;
		private int _toaccountmonth = 0;
		private int _fromfeetype = 0;
		private int _tofeetype = 0;
		private int _budgettype = 0;
		private decimal _amount = 0;
		private int _approveflag = 0;
		private int _insertstaff = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_BudgetTransfer()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_BudgetTransfer(int id, int fromorganizecity, int toorganizecity, int fromaccountmonth, int toaccountmonth, int fromfeetype, int tofeetype, int budgettype, decimal amount, int approveflag, int insertstaff, DateTime inserttime, int updatestaff, DateTime updatetime)
		{
			_id               = id;
			_fromorganizecity = fromorganizecity;
			_toorganizecity   = toorganizecity;
			_fromaccountmonth = fromaccountmonth;
			_toaccountmonth   = toaccountmonth;
			_fromfeetype      = fromfeetype;
			_tofeetype        = tofeetype;
			_budgettype       = budgettype;
			_amount           = amount;
			_approveflag      = approveflag;
			_insertstaff      = insertstaff;
			_inserttime       = inserttime;
			_updatestaff      = updatestaff;
			_updatetime       = updatetime;
			
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
		///源管理片区
		///</summary>
		public int FromOrganizeCity
		{
			get	{ return _fromorganizecity; }
			set	{ _fromorganizecity = value; }
		}

		///<summary>
		///目的管理片区
		///</summary>
		public int ToOrganizeCity
		{
			get	{ return _toorganizecity; }
			set	{ _toorganizecity = value; }
		}

		///<summary>
		///源会计月
		///</summary>
		public int FromAccountMonth
		{
			get	{ return _fromaccountmonth; }
			set	{ _fromaccountmonth = value; }
		}

		///<summary>
		///目的会计月
		///</summary>
		public int ToAccountMonth
		{
			get	{ return _toaccountmonth; }
			set	{ _toaccountmonth = value; }
		}

		///<summary>
		///费用类型
		///</summary>
		public int FromFeeType
		{
			get	{ return _fromfeetype; }
			set	{ _fromfeetype = value; }
		}

		///<summary>
		///ToFeeType
		///</summary>
		public int ToFeeType
		{
			get	{ return _tofeetype; }
			set	{ _tofeetype = value; }
		}

		///<summary>
		///预算类型
		///</summary>
		public int BudgetType
		{
			get	{ return _budgettype; }
			set	{ _budgettype = value; }
		}

		///<summary>
		///调拔金额
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
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
		///录入人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
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
		///更新人
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
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
            get { return "FNA_BudgetTransfer"; }
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
					case "FromOrganizeCity":
						return _fromorganizecity.ToString();
					case "ToOrganizeCity":
						return _toorganizecity.ToString();
					case "FromAccountMonth":
						return _fromaccountmonth.ToString();
					case "ToAccountMonth":
						return _toaccountmonth.ToString();
					case "FromFeeType":
						return _fromfeetype.ToString();
					case "ToFeeType":
						return _tofeetype.ToString();
					case "BudgetType":
						return _budgettype.ToString();
					case "Amount":
						return _amount.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
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
					case "FromOrganizeCity":
						int.TryParse(value, out _fromorganizecity);
						break;
					case "ToOrganizeCity":
						int.TryParse(value, out _toorganizecity);
						break;
					case "FromAccountMonth":
						int.TryParse(value, out _fromaccountmonth);
						break;
					case "ToAccountMonth":
						int.TryParse(value, out _toaccountmonth);
						break;
					case "FromFeeType":
						int.TryParse(value, out _fromfeetype);
						break;
					case "ToFeeType":
						int.TryParse(value, out _tofeetype);
						break;
					case "BudgetType":
						int.TryParse(value, out _budgettype);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
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
