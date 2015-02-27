// ===================================================================
// 文件： ORD_OrderDeliveryDetail.cs
// 项目名称：
// 创建时间：2010/7/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
	/// <summary>
	///ORD_OrderDeliveryDetail数据实体类
	/// </summary>
	[Serializable]
	public class ORD_OrderDeliveryDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _deliveryid = 0;
		private int _applydetailid = 0;
		private int _client = 0;
		private int _product = 0;
		private decimal _price = 0;
		private decimal _factoryprice = 0;
		private int _deliveryquantity = 0;
		private int _signinquantity = 0;
		private int _badquantity = 0;
		private int _lostquantity = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_OrderDeliveryDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_OrderDeliveryDetail(int id, int deliveryid, int applydetailid, int client, int product, decimal price, decimal factoryprice, int deliveryquantity, int signinquantity, int badquantity, int lostquantity, string remark)
		{
			_id               = id;
			_deliveryid       = deliveryid;
			_applydetailid    = applydetailid;
			_client           = client;
			_product          = product;
			_price            = price;
			_factoryprice     = factoryprice;
			_deliveryquantity = deliveryquantity;
			_signinquantity   = signinquantity;
			_badquantity      = badquantity;
			_lostquantity     = lostquantity;
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
		///DeliveryID
		///</summary>
		public int DeliveryID
		{
			get	{ return _deliveryid; }
			set	{ _deliveryid = value; }
		}

		///<summary>
		///ApplyDetailID
		///</summary>
		public int ApplyDetailID
		{
			get	{ return _applydetailid; }
			set	{ _applydetailid = value; }
		}

		///<summary>
		///Client
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
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

		///<summary>
		///FactoryPrice
		///</summary>
		public decimal FactoryPrice
		{
			get	{ return _factoryprice; }
			set	{ _factoryprice = value; }
		}

		///<summary>
		///发放数量
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
		///BadQuantity
		///</summary>
		public int BadQuantity
		{
			get	{ return _badquantity; }
			set	{ _badquantity = value; }
		}

		///<summary>
		///LostQuantity
		///</summary>
		public int LostQuantity
		{
			get	{ return _lostquantity; }
			set	{ _lostquantity = value; }
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
            get { return "ORD_OrderDeliveryDetail"; }
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
					case "ApplyDetailID":
						return _applydetailid.ToString();
					case "Client":
						return _client.ToString();
					case "Product":
						return _product.ToString();
					case "Price":
						return _price.ToString();
					case "FactoryPrice":
						return _factoryprice.ToString();
					case "DeliveryQuantity":
						return _deliveryquantity.ToString();
					case "SignInQuantity":
						return _signinquantity.ToString();
					case "BadQuantity":
						return _badquantity.ToString();
					case "LostQuantity":
						return _lostquantity.ToString();
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
					case "DeliveryID":
						int.TryParse(value, out _deliveryid);
						break;
					case "ApplyDetailID":
						int.TryParse(value, out _applydetailid);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "FactoryPrice":
						decimal.TryParse(value, out _factoryprice);
						break;
					case "DeliveryQuantity":
						int.TryParse(value, out _deliveryquantity);
						break;
					case "SignInQuantity":
						int.TryParse(value, out _signinquantity);
						break;
					case "BadQuantity":
						int.TryParse(value, out _badquantity);
						break;
					case "LostQuantity":
						int.TryParse(value, out _lostquantity);
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
