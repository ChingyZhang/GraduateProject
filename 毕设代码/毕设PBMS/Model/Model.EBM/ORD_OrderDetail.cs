// ===================================================================
// 文件： ORD_OrderDetail.cs
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
	///ORD_OrderDetail数据实体类
	/// </summary>
	[Serializable]
	public class ORD_OrderDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _orderid = 0;
		private int _product = 0;
		private decimal _price = 0;
        private decimal _points = 0;
		private int _bookquantity = 0;
		private int _confirmquantity = 0;
		private string _adjustreason = string.Empty;
		private int _deliveredquantity = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_OrderDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_OrderDetail(int id, int orderid, int product, decimal price, int bookquantity, int confirmquantity, string adjustreason, int deliveredquantity, string remark)
		{
			_id                = id;
			_orderid           = orderid;
			_product           = product;
			_price             = price;
			_bookquantity      = bookquantity;
			_confirmquantity   = confirmquantity;
			_adjustreason      = adjustreason;
			_deliveredquantity = deliveredquantity;
			_remark            = remark;
			
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
		///OrderID
		///</summary>
		public int OrderID
		{
			get	{ return _orderid; }
			set	{ _orderid = value; }
		}

		///<summary>
		///产品
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///价格
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
		}

        /// <summary>
        /// 积分值
        /// </summary>
        public decimal Points
        {
            get { return _points; }
            set { _points = value; }
        }
		///<summary>
		///请购数量
		///</summary>
		public int BookQuantity
		{
			get	{ return _bookquantity; }
			set	{ _bookquantity = value; }
		}

		///<summary>
		///调整数量
		///</summary>
		public int ConfirmQuantity
		{
			get	{ return _confirmquantity; }
			set	{ _confirmquantity = value; }
		}

		///<summary>
		///调整原因
		///</summary>
		public string AdjustReason
		{
			get	{ return _adjustreason; }
			set	{ _adjustreason = value; }
		}

		///<summary>
		///已发放数量
		///</summary>
		public int DeliveredQuantity
		{
			get	{ return _deliveredquantity; }
			set	{ _deliveredquantity = value; }
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
            get { return "ORD_OrderDetail"; }
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
					case "OrderID":
						return _orderid.ToString();
					case "Product":
						return _product.ToString();
					case "Price":
						return _price.ToString();
                    case "Points":
						return _points.ToString();
					case "BookQuantity":
						return _bookquantity.ToString();
					case "ConfirmQuantity":
						return _confirmquantity.ToString();
					case "AdjustReason":
						return _adjustreason;
					case "DeliveredQuantity":
						return _deliveredquantity.ToString();
					case "Remark":
						return _remark;
					default:
						if (_extpropertys==null)
							return "";
						else
							return _extpropertys[FieldName];						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "OrderID":
						int.TryParse(value, out _orderid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
                    case "Points":
						decimal.TryParse(value, out _points);
						break;
					case "BookQuantity":
						int.TryParse(value, out _bookquantity);
						break;
					case "ConfirmQuantity":
						int.TryParse(value, out _confirmquantity);
						break;
					case "AdjustReason":
						_adjustreason = value ;
						break;
					case "DeliveredQuantity":
						int.TryParse(value, out _deliveredquantity);
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
