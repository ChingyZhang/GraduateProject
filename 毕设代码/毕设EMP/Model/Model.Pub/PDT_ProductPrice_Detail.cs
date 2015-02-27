// ===================================================================
// 文件： PDT_ProductPrice_Detail.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_ProductPrice_Detail数据实体类
	/// </summary>
	[Serializable]
	public class PDT_ProductPrice_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _priceid = 0;
		private int _product = 0;
		private decimal _buyingprice = 0;
		private decimal _salesprice = 0;
        private decimal _factoryprice = 0;
		private string _remark = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ProductPrice_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_ProductPrice_Detail(int id, int priceid, int product, decimal buyingprice, decimal salesprice, string remark)
		{
			_id          = id;
			_priceid     = priceid;
			_product     = product;
			_buyingprice = buyingprice;
			_salesprice  = salesprice;
			_remark      = remark;
			
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
		///PriceID
		///</summary>
		public int PriceID
		{
			get	{ return _priceid; }
			set	{ _priceid = value; }
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
		///买入价
		///</summary>
		public decimal BuyingPrice
		{
			get	{ return _buyingprice; }
			set	{ _buyingprice = value; }
		}

		///<summary>
		///SalesPrice
		///</summary>
		public decimal SalesPrice
		{
			get	{ return _salesprice; }
			set	{ _salesprice = value; }
		}

        ///<summary>
        ///FactoryPrice
        ///</summary>
        public decimal FactoryPrice
        {
            get { return _factoryprice; }
            set { _factoryprice = value; }
        }

		///<summary>
		///Remark
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
            get { return "PDT_ProductPrice_Detail"; }
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
					case "PriceID":
						return _priceid.ToString();
					case "Product":
						return _product.ToString();
					case "BuyingPrice":
						return _buyingprice.ToString();
					case "SalesPrice":
						return _salesprice.ToString();
                    case "FactoryPrice":
						return _factoryprice.ToString();
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
					case "PriceID":
						int.TryParse(value, out _priceid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "BuyingPrice":
						decimal.TryParse(value, out _buyingprice);
						break;
					case "SalesPrice":
						decimal.TryParse(value, out _salesprice);
						break;
                    case "FactoryPrice":
						decimal.TryParse(value, out _factoryprice);
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
