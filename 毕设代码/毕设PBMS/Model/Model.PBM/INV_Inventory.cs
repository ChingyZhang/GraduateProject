// ===================================================================
// 文件： INV_Inventory.cs
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
	///INV_Inventory数据实体类
	/// </summary>
	[Serializable]
	public class INV_Inventory : IModel
	{
		#region 私有变量定义
        private long _id = 0;
		private int _warehouse = 0;
		private int _product = 0;
		private string _lotnumber = string.Empty;
		private DateTime _productdate = new DateTime(1900,1,1);
		private int _quantity = 0;
		private decimal _price = 0;
		private DateTime _lastupdatetime = new DateTime(1900,1,1);
		private DateTime _inserttime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_Inventory()
		{
		}
		
		///<summary>
		///
		///</summary>
        public INV_Inventory(long id, int warehouse, int product, string lotnumber, DateTime productdate, int quantity, decimal price, DateTime lastupdatetime, DateTime inserttime, string remark)
		{
			_id             = id;
			_warehouse      = warehouse;
			_product        = product;
			_lotnumber      = lotnumber;
			_productdate    = productdate;
			_quantity       = quantity;
			_price          = price;
			_lastupdatetime = lastupdatetime;
			_inserttime     = inserttime;
			_remark         = remark;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
        public long ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
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
		///批号
		///</summary>
		public string LotNumber
		{
			get	{ return _lotnumber; }
			set	{ _lotnumber = value; }
		}

		///<summary>
		///生产日期
		///</summary>
		public DateTime ProductDate
		{
			get	{ return _productdate; }
			set	{ _productdate = value; }
		}

		///<summary>
		///数量
		///</summary>
		public int Quantity
		{
			get	{ return _quantity; }
			set	{ _quantity = value; }
		}

		///<summary>
		///价格
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
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
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
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
            get { return "INV_Inventory"; }
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
					case "WareHouse":
						return _warehouse.ToString();
					case "Product":
						return _product.ToString();
					case "LotNumber":
						return _lotnumber;
					case "ProductDate":
						return _productdate.ToString();
					case "Quantity":
						return _quantity.ToString();
					case "Price":
						return _price.ToString();
					case "LastUpdateTime":
						return _lastupdatetime.ToString();
					case "InsertTime":
						return _inserttime.ToString();
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
                        long.TryParse(value, out _id);
						break;
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "LotNumber":
						_lotnumber = value ;
						break;
					case "ProductDate":
						DateTime.TryParse(value, out _productdate);
						break;
					case "Quantity":
						int.TryParse(value, out _quantity);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "LastUpdateTime":
						DateTime.TryParse(value, out _lastupdatetime);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
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
