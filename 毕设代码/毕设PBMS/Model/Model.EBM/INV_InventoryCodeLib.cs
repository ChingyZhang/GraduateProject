// ===================================================================
// 文件： INV_InventoryCodeLib.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///INV_InventoryCodeLib数据实体类
	/// </summary>
	[Serializable]
	public class INV_InventoryCodeLib : IModel
	{
		#region 私有变量定义
		private long _id = 0;
		private int _product = 0;
		private string _casecode = string.Empty;
		private string _piececode = string.Empty;
		private string _lotnumber = string.Empty;
		private int _warehouse = 0;
		private int _warehousecell = 0;
		private decimal _price = 0;
		private int _state = 0;
		private DateTime _putintime = new DateTime(1900,1,1);
		private DateTime _lastupdatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_InventoryCodeLib()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_InventoryCodeLib(long id, int product, string casecode, string piececode, string lotnumber, int warehouse, int warehousecell, decimal price, int state, DateTime putintime, DateTime lastupdatetime)
		{
			_id             = id;
			_product        = product;
			_casecode       = casecode;
			_piececode      = piececode;
			_lotnumber      = lotnumber;
			_warehouse      = warehouse;
			_warehousecell  = warehousecell;
			_price          = price;
			_state          = state;
			_putintime      = putintime;
			_lastupdatetime = lastupdatetime;
			
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
		///产品SKU
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///箱码
		///</summary>
		public string CaseCode
		{
			get	{ return _casecode; }
			set	{ _casecode = value; }
		}

		///<summary>
		///件码
		///</summary>
		public string PieceCode
		{
			get	{ return _piececode; }
			set	{ _piececode = value; }
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
		///所在仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
		}

		///<summary>
		///所在库位
		///</summary>
		public int WareHouseCell
		{
			get	{ return _warehousecell; }
			set	{ _warehousecell = value; }
		}

		///<summary>
		///单价
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
		}

		///<summary>
		///状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///入库时间
		///</summary>
		public DateTime PutInTime
		{
			get	{ return _putintime; }
			set	{ _putintime = value; }
		}

		///<summary>
		///最后更新时间
		///</summary>
		public DateTime LastUpdateTime
		{
			get	{ return _lastupdatetime; }
			set	{ _lastupdatetime = value; }
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
            get { return "INV_InventoryCodeLib"; }
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
					case "Product":
						return _product.ToString();
					case "CaseCode":
						return _casecode;
					case "PieceCode":
						return _piececode;
					case "LotNumber":
						return _lotnumber;
					case "WareHouse":
						return _warehouse.ToString();
					case "WareHouseCell":
						return _warehousecell.ToString();
					case "Price":
						return _price.ToString();
					case "State":
						return _state.ToString();
					case "PutInTime":
						return _putintime.ToString();
					case "LastUpdateTime":
						return _lastupdatetime.ToString();
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
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "CaseCode":
						_casecode = value ;
						break;
					case "PieceCode":
						_piececode = value ;
						break;
					case "LotNumber":
						_lotnumber = value ;
						break;
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "WareHouseCell":
						int.TryParse(value, out _warehousecell);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "PutInTime":
						DateTime.TryParse(value, out _putintime);
						break;
					case "LastUpdateTime":
						DateTime.TryParse(value, out _lastupdatetime);
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
