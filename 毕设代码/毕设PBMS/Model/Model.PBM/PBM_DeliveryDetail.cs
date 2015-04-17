// ===================================================================
// 文件： PBM_DeliveryDetail.cs
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
	///PBM_DeliveryDetail数据实体类
	/// </summary>
	[Serializable]
	public class PBM_DeliveryDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _deliveryid = 0;
		private int _product = 0;
        private string _lotnumber = string.Empty;
		private decimal _costprice = 0;
		private decimal _price = 0;
		private decimal _discountrate = 0;
        private int _convertfactor = 0;
		private int _deliveryquantity = 0;
		private int _signinquantity = 0;
		private int _returnquantity = 0;
		private int _badquantity = 0;
		private int _lostquantity = 0;
		private int _salesmode = 0;
        private DateTime _productdate = new DateTime(1900, 1, 1);
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_DeliveryDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PBM_DeliveryDetail(int id, int deliveryid, int product, decimal costprice, decimal price, decimal discountrate, int deliveryquantity, int signinquantity, int returnquantity, int badquantity, int lostquantity, int salesmode, string remark)
		{
			_id               = id;
			_deliveryid       = deliveryid;
			_product          = product;
			_costprice        = costprice;
			_price            = price;
			_discountrate     = discountrate;
			_deliveryquantity = deliveryquantity;
			_signinquantity   = signinquantity;
			_returnquantity   = returnquantity;
			_badquantity      = badquantity;
			_lostquantity     = lostquantity;
			_salesmode        = salesmode;
			_remark           = remark;
			
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
		///发货单ID
		///</summary>
		public int DeliveryID
		{
			get	{ return _deliveryid; }
			set	{ _deliveryid = value; }
		}

		///<summary>
		///产品
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

        /// <summary>
        /// 批号
        /// </summary>
        public string LotNumber
        {
            get { return _lotnumber; }
            set { _lotnumber = value; }
        }

		///<summary>
		///成本价
		///</summary>
		public decimal CostPrice
		{
			get	{ return _costprice; }
			set	{ _costprice = value; }
		}

		///<summary>
		///销售价
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
		}

		///<summary>
		///折扣率
		///</summary>
		public decimal DiscountRate
		{
			get	{ return _discountrate; }
			set	{ _discountrate = value; }
		}

        /// <summary>
        /// 包装系数
        /// </summary>
        public int ConvertFactor
        {
            get	{ return _convertfactor; }
            set { _convertfactor = value; }
        }

		///<summary>
		///发货数量
		///</summary>
		public int DeliveryQuantity
		{
			get	{ return _deliveryquantity; }
			set	{ _deliveryquantity = value; }
		}

		///<summary>
		///签收数量
		///</summary>
		public int SignInQuantity
		{
			get	{ return _signinquantity; }
			set	{ _signinquantity = value; }
		}

		///<summary>
		///退回数量
		///</summary>
		public int ReturnQuantity
		{
			get	{ return _returnquantity; }
			set	{ _returnquantity = value; }
		}

		///<summary>
		///破损数量
		///</summary>
		public int BadQuantity
		{
			get	{ return _badquantity; }
			set	{ _badquantity = value; }
		}

		///<summary>
		///丢失数量
		///</summary>
		public int LostQuantity
		{
			get	{ return _lostquantity; }
			set	{ _lostquantity = value; }
		}

		///<summary>
		///销售方式
		///</summary>
		public int SalesMode
		{
			get	{ return _salesmode; }
			set	{ _salesmode = value; }
		}

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProductDate
        {
            get { return _productdate; }
            set { _productdate = value; }
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
            get { return "PBM_DeliveryDetail"; }
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
					case "DeliveryID":
						return _deliveryid.ToString();
					case "Product":
						return _product.ToString();
                    case "LotNumber":
                        return _lotnumber.ToString();
					case "CostPrice":
						return _costprice.ToString();
					case "Price":
						return _price.ToString();
					case "DiscountRate":
						return _discountrate.ToString();
                    case "ConvertFactor":
                        return _convertfactor.ToString();
					case "DeliveryQuantity":
						return _deliveryquantity.ToString();
					case "SignInQuantity":
						return _signinquantity.ToString();
					case "ReturnQuantity":
						return _returnquantity.ToString();
					case "BadQuantity":
						return _badquantity.ToString();
					case "LostQuantity":
						return _lostquantity.ToString();
					case "SalesMode":
						return _salesmode.ToString();
                    case "ProductDate":
                        return _productdate.ToString();
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
						int.TryParse(value, out _id);
						break;
					case "DeliveryID":
						int.TryParse(value, out _deliveryid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
                    case "LotNumber":
                        _lotnumber = value;
                        break;
					case "CostPrice":
						decimal.TryParse(value, out _costprice);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "DiscountRate":
						decimal.TryParse(value, out _discountrate);
						break;
                    case "ConvertFactor":
                        int.TryParse(value, out _convertfactor);
                        break;
					case "DeliveryQuantity":
						int.TryParse(value, out _deliveryquantity);
						break;
					case "SignInQuantity":
						int.TryParse(value, out _signinquantity);
						break;
					case "ReturnQuantity":
						int.TryParse(value, out _returnquantity);
						break;
					case "BadQuantity":
						int.TryParse(value, out _badquantity);
						break;
					case "LostQuantity":
						int.TryParse(value, out _lostquantity);
						break;
					case "SalesMode":
						int.TryParse(value, out _salesmode);
						break;
                    case "ProductDate":
                        DateTime.TryParse(value, out _productdate);
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
