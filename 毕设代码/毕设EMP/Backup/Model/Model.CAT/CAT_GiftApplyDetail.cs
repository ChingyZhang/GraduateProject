// ===================================================================
// 文件： CAT_GiftApplyDetail.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CAT
{
	/// <summary>
	///CAT_GiftApplyDetail数据实体类
	/// </summary>
	[Serializable]
	public class CAT_GiftApplyDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _activity = 0;
		private int _product = 0;
		private int _applyquantity = 0;
		private int _adjustquantity = 0;
		private int _usedquantity = 0;
		private int _balancequantity = 0;
		private decimal _price = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_GiftApplyDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CAT_GiftApplyDetail(int id, int activity, int product, int applyquantity, int adjustquantity, int usedquantity, int balancequantity, decimal price, string remark)
		{
			_id              = id;
			_activity        = activity;
			_product         = product;
			_applyquantity   = applyquantity;
			_adjustquantity  = adjustquantity;
			_usedquantity    = usedquantity;
			_balancequantity = balancequantity;
			_price           = price;
			_remark          = remark;
			
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
		///活动ID
		///</summary>
		public int Activity
		{
			get	{ return _activity; }
			set	{ _activity = value; }
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
		///申请数量
		///</summary>
		public int ApplyQuantity
		{
			get	{ return _applyquantity; }
			set	{ _applyquantity = value; }
		}

		///<summary>
		///调整数量
		///</summary>
		public int AdjustQuantity
		{
			get	{ return _adjustquantity; }
			set	{ _adjustquantity = value; }
		}

		///<summary>
		///使用数量
		///</summary>
		public int UsedQuantity
		{
			get	{ return _usedquantity; }
			set	{ _usedquantity = value; }
		}

		///<summary>
		///剩余数量
		///</summary>
		public int BalanceQuantity
		{
			get	{ return _balancequantity; }
			set	{ _balancequantity = value; }
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
            get { return "CAT_GiftApplyDetail"; }
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
					case "Activity":
						return _activity.ToString();
					case "Product":
						return _product.ToString();
					case "ApplyQuantity":
						return _applyquantity.ToString();
					case "AdjustQuantity":
						return _adjustquantity.ToString();
					case "UsedQuantity":
						return _usedquantity.ToString();
					case "BalanceQuantity":
						return _balancequantity.ToString();
					case "Price":
						return _price.ToString();
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
					case "Activity":
						int.TryParse(value, out _activity);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "ApplyQuantity":
						int.TryParse(value, out _applyquantity);
						break;
					case "AdjustQuantity":
						int.TryParse(value, out _adjustquantity);
						break;
					case "UsedQuantity":
						int.TryParse(value, out _usedquantity);
						break;
					case "BalanceQuantity":
						int.TryParse(value, out _balancequantity);
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
