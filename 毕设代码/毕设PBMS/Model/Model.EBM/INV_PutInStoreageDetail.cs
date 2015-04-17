// ===================================================================
// 文件： INV_PutInStoreageDetail.cs
// 项目名称：
// 创建时间：2012-7-23
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
	///INV_PutInStoreageDetail数据实体类
	/// </summary>
	[Serializable]
	public class INV_PutInStoreageDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _putinid = 0;
		private int _product = 0;
		private decimal _price = 0;
		private int _quantity = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_PutInStoreageDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_PutInStoreageDetail(int id, int putinid, int product, decimal price, int quantity, string remark)
		{
			_id           = id;
			_putinid      = putinid;
			_product      = product;
			_price        = price;
			_quantity     = quantity;
			_remark       = remark;
			
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
		///入库单ID
		///</summary>
		public int PutInID
		{
			get	{ return _putinid; }
			set	{ _putinid = value; }
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
		///入库数量
		///</summary>
		public int Quantity
		{
			get	{ return _quantity; }
			set	{ _quantity = value; }
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
            get { return "INV_PutInStoreageDetail"; }
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
					case "PutInID":
						return _putinid.ToString();
					case "Product":
						return _product.ToString();
					case "Price":
						return _price.ToString();
					case "Quantity":
						return _quantity.ToString();
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
					case "PutInID":
						int.TryParse(value, out _putinid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "Quantity":
						int.TryParse(value, out _quantity);
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
