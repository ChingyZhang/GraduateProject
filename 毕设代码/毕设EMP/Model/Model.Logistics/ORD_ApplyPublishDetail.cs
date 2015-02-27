// ===================================================================
// 文件： ORD_ApplyPublishDetail.cs
// 项目名称：
// 创建时间：2009/9/5
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
    ///ORD_ApplyPublishDetail数据实体类
	/// </summary>
	[Serializable]
	public class ORD_ApplyPublishDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _publishid = 0;
		private int _product = 0;
		private int _minquantity = 0;
		private int _maxquantity = 0;
		private int _availablequantity = 0;
		private decimal _price = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_ApplyPublishDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_ApplyPublishDetail(int id, int publishid, int product, int minquantity, int maxquantity, int availablequantity, decimal price, string remark)
		{
			_id                = id;
			_publishid         = publishid;
			_product           = product;
			_minquantity       = minquantity;
			_maxquantity       = maxquantity;
			_availablequantity = availablequantity;
			_price             = price;
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
		///发布ID
		///</summary>
		public int PublishID
		{
			get	{ return _publishid; }
			set	{ _publishid = value; }
		}

		///<summary>
        ///品项
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///请购最小量
		///</summary>
		public int MinQuantity
		{
			get	{ return _minquantity; }
			set	{ _minquantity = value; }
		}

		///<summary>
		///请购最大量
		///</summary>
		public int MaxQuantity
		{
			get	{ return _maxquantity; }
			set	{ _maxquantity = value; }
		}

		///<summary>
		///可供采购量
		///</summary>
		public int AvailableQuantity
		{
			get	{ return _availablequantity; }
			set	{ _availablequantity = value; }
		}

		///<summary>
		///请购单价
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
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
            get { return "ORD_ApplyPublishDetail"; }
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
					case "PublishID":
						return _publishid.ToString();
					case "Product":
						return _product.ToString();
					case "MinQuantity":
						return _minquantity.ToString();
					case "MaxQuantity":
						return _maxquantity.ToString();
					case "AvailableQuantity":
						return _availablequantity.ToString();
					case "Price":
						return _price.ToString();
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
					case "PublishID":
						int.TryParse(value, out _publishid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "MinQuantity":
						int.TryParse(value, out _minquantity);
						break;
					case "MaxQuantity":
						int.TryParse(value, out _maxquantity);
						break;
					case "AvailableQuantity":
						int.TryParse(value, out _availablequantity);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
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
