// ===================================================================
// 文件： SVM_InventoryDifferences_Detail.cs
// 项目名称：
// 创建时间：2011/12/14
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_InventoryDifferences_Detail数据实体类
	/// </summary>
	[Serializable]
	public class SVM_InventoryDifferences_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _inventoryid = 0;
		private int _product = 0;
		private string _lotnumber = string.Empty;
		private int _quantity = 0;
		private decimal _factoryprice = 0;
		private string _remark = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_InventoryDifferences_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_InventoryDifferences_Detail(int id, int inventoryid, int product, string lotnumber, int quantity, decimal factoryprice, string remark)
		{
			_id           = id;
			_inventoryid  = inventoryid;
			_product      = product;
			_lotnumber    = lotnumber;
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
		///InventoryID
		///</summary>
		public int InventoryID
		{
			get	{ return _inventoryid; }
			set	{ _inventoryid = value; }
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
		///LotNumber
		///</summary>
		public string LotNumber
		{
			get	{ return _lotnumber; }
			set	{ _lotnumber = value; }
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
            get { return "SVM_InventoryDifferences_Detail"; }
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
					case "InventoryID":
						return _inventoryid.ToString();
					case "Product":
						return _product.ToString();
					case "LotNumber":
						return _lotnumber;
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
					case "InventoryID":
						int.TryParse(value, out _inventoryid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "LotNumber":
						_lotnumber = value ;
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
