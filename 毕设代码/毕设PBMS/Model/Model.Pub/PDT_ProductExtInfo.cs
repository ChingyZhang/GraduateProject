// ===================================================================
// 文件： PDT_ProductExtInfo.cs
// 项目名称：
// 创建时间：2015-02-02
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_ProductExtInfo数据实体类
	/// </summary>
	[Serializable]
	public class PDT_ProductExtInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _supplier = 0;
		private int _product = 0;
		private string _code = string.Empty;
		private int _category = 0;
		private decimal _buyprice = 0;
		private decimal _salesprice = 0;
		private decimal _maxsalesprice = 0;
		private decimal _minsalesprice = 0;
		private int _maxinventory = 0;
		private int _mininventory = 0;
		private int _salesstate = 0;
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
		public PDT_ProductExtInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_ProductExtInfo(int id, int supplier, int product, string code, int category, decimal buyprice, decimal salesprice, decimal maxsalesprice, decimal minsalesprice, int maxinventory, int mininventory, int salesstate, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_supplier      = supplier;
			_product       = product;
			_code          = code;
			_category      = category;
			_buyprice      = buyprice;
			_salesprice    = salesprice;
			_maxsalesprice = maxsalesprice;
			_minsalesprice = minsalesprice;
			_maxinventory  = maxinventory;
			_mininventory  = mininventory;
			_salesstate    = salesstate;
			_remark        = remark;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			_updatetime    = updatetime;
			_updatestaff   = updatestaff;
			
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
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
		}

		///<summary>
		///商品
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///产品自编码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///商品种类
		///</summary>
		public int Category
		{
			get	{ return _category; }
			set	{ _category = value; }
		}

		///<summary>
		///默认采购价
		///</summary>
		public decimal BuyPrice
		{
			get	{ return _buyprice; }
			set	{ _buyprice = value; }
		}

		///<summary>
		///默认销售价
		///</summary>
		public decimal SalesPrice
		{
			get	{ return _salesprice; }
			set	{ _salesprice = value; }
		}

		///<summary>
		///最高销售价
		///</summary>
		public decimal MaxSalesPrice
		{
			get	{ return _maxsalesprice; }
			set	{ _maxsalesprice = value; }
		}

		///<summary>
		///最低销售价
		///</summary>
		public decimal MinSalesPrice
		{
			get	{ return _minsalesprice; }
			set	{ _minsalesprice = value; }
		}

		///<summary>
		///库存上限
		///</summary>
		public int MaxInventory
		{
			get	{ return _maxinventory; }
			set	{ _maxinventory = value; }
		}

		///<summary>
		///库存下限
		///</summary>
		public int MinInventory
		{
			get	{ return _mininventory; }
			set	{ _mininventory = value; }
		}

		///<summary>
		///经营状态
		///</summary>
		public int SalesState
		{
			get	{ return _salesstate; }
			set	{ _salesstate = value; }
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
            get { return "PDT_ProductExtInfo"; }
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
					case "Supplier":
						return _supplier.ToString();
					case "Product":
						return _product.ToString();
					case "Code":
						return _code;
					case "Category":
						return _category.ToString();
					case "BuyPrice":
						return _buyprice.ToString();
					case "SalesPrice":
						return _salesprice.ToString();
					case "MaxSalesPrice":
						return _maxsalesprice.ToString();
					case "MinSalesPrice":
						return _minsalesprice.ToString();
					case "MaxInventory":
						return _maxinventory.ToString();
					case "MinInventory":
						return _mininventory.ToString();
					case "SalesState":
						return _salesstate.ToString();
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
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Code":
						_code = value;
						break;
					case "Category":
						int.TryParse(value, out _category);
						break;
					case "BuyPrice":
						decimal.TryParse(value, out _buyprice);
						break;
					case "SalesPrice":
						decimal.TryParse(value, out _salesprice);
						break;
					case "MaxSalesPrice":
						decimal.TryParse(value, out _maxsalesprice);
						break;
					case "MinSalesPrice":
						decimal.TryParse(value, out _minsalesprice);
						break;
					case "MaxInventory":
						int.TryParse(value, out _maxinventory);
						break;
					case "MinInventory":
						int.TryParse(value, out _mininventory);
						break;
					case "SalesState":
						int.TryParse(value, out _salesstate);
						break;
					case "Remark":
						_remark = value;
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
