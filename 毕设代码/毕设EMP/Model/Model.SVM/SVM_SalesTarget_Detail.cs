// ===================================================================
// 文件： SVM_SalesTarget_Detail.cs
// 项目名称：
// 创建时间：2009/3/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_SalesTarget_Detail数据实体类
	/// </summary>
	[Serializable]
	public class SVM_SalesTarget_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _targetid = 0;
		private int _product = 0;
		private int _quantity = 0;
		private decimal _factoryprice = 0;
		private string _remark = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_SalesTarget_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_SalesTarget_Detail(int id, int targetid, int product, int quantity, decimal factoryprice, string remark)
		{
			_id           = id;
			_targetid     = targetid;
			_product      = product;
			_quantity     = quantity;
			_factoryprice = factoryprice;
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
		///TargetID
		///</summary>
		public int TargetID
		{
			get	{ return _targetid; }
			set	{ _targetid = value; }
		}

		///<summary>
		///Product
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///Quantity
		///</summary>
		public int Quantity
		{
			get	{ return _quantity; }
			set	{ _quantity = value; }
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
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "SVM_SalesTarget_Detail"; }
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
					case "TargetID":
						return _targetid.ToString();
					case "Product":
						return _product.ToString();
					case "Quantity":
						return _quantity.ToString();
					case "FactoryPrice":
						return _factoryprice.ToString();
					case "Remark":
						return _remark;
					default:
						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "TargetID":
						int.TryParse(value, out _targetid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Quantity":
						int.TryParse(value, out _quantity);
						break;
					case "FactoryPrice":
						decimal.TryParse(value, out _factoryprice);
						break;
					case "Remark":
						_remark = value ;
						break;

				}
			}
        }
		#endregion
	}
}
