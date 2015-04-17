// ===================================================================
// 文件： INV_Inventory_CarryDown.cs
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
	///INV_Inventory_CarryDown数据实体类
	/// </summary>
	[Serializable]
	public class INV_Inventory_CarryDown : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _warehouse = 0;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private int _product = 0;
		private string _lotnumber = string.Empty;
		private DateTime _productdate = new DateTime(1900,1,1);
		private decimal _price = 0;
		private int _beginquanitity = 0;
		private int _inwarehouse = 0;
		private int _outwarehouse = 0;
		private int _losswarehouse = 0;
		private int _endquantitty = 0;
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_Inventory_CarryDown()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_Inventory_CarryDown(int id, int warehouse, DateTime begindate, DateTime enddate, int product, string lotnumber, DateTime productdate, decimal price, int beginquanitity, int inwarehouse, int outwarehouse, int losswarehouse, int endquantitty, string remark, int approveflag, DateTime inserttime, int insertstaff)
		{
			_id             = id;
			_warehouse      = warehouse;
			_begindate      = begindate;
			_enddate        = enddate;
			_product        = product;
			_lotnumber      = lotnumber;
			_productdate    = productdate;
			_price          = price;
			_beginquanitity = beginquanitity;
			_inwarehouse    = inwarehouse;
			_outwarehouse   = outwarehouse;
			_losswarehouse  = losswarehouse;
			_endquantitty   = endquantitty;
			_remark         = remark;
			_approveflag    = approveflag;
			_inserttime     = inserttime;
			_insertstaff    = insertstaff;
			
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
		///仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
		}

		///<summary>
		///期初日期
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///期末日期
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
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
		///成本价
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
		}

		///<summary>
		///期初数量
		///</summary>
		public int BeginQuanitity
		{
			get	{ return _beginquanitity; }
			set	{ _beginquanitity = value; }
		}

		///<summary>
		///本期入库
		///</summary>
		public int InWareHouse
		{
			get	{ return _inwarehouse; }
			set	{ _inwarehouse = value; }
		}

		///<summary>
		///本期出库
		///</summary>
		public int OutWarehouse
		{
			get	{ return _outwarehouse; }
			set	{ _outwarehouse = value; }
		}

		///<summary>
		///本期损溢
		///</summary>
		public int LossWarehouse
		{
			get	{ return _losswarehouse; }
			set	{ _losswarehouse = value; }
		}

		///<summary>
		///期末数量
		///</summary>
		public int EndQuantitty
		{
			get	{ return _endquantitty; }
			set	{ _endquantitty = value; }
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
            get { return "INV_Inventory_CarryDown"; }
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
					case "BeginDate":
						return _begindate.ToString();
					case "EndDate":
						return _enddate.ToString();
					case "Product":
						return _product.ToString();
					case "LotNumber":
						return _lotnumber;
					case "ProductDate":
						return _productdate.ToString();
					case "Price":
						return _price.ToString();
					case "BeginQuanitity":
						return _beginquanitity.ToString();
					case "InWareHouse":
						return _inwarehouse.ToString();
					case "OutWarehouse":
						return _outwarehouse.ToString();
					case "LossWarehouse":
						return _losswarehouse.ToString();
					case "EndQuantitty":
						return _endquantitty.ToString();
					case "Remark":
						return _remark;
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
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
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "BeginQuanitity":
						int.TryParse(value, out _beginquanitity);
						break;
					case "InWareHouse":
						int.TryParse(value, out _inwarehouse);
						break;
					case "OutWarehouse":
						int.TryParse(value, out _outwarehouse);
						break;
					case "LossWarehouse":
						int.TryParse(value, out _losswarehouse);
						break;
					case "EndQuantitty":
						int.TryParse(value, out _endquantitty);
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
