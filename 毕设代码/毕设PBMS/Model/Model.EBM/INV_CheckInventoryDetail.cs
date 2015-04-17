// ===================================================================
// 文件： INV_CheckInventoryDetail.cs
// 项目名称：
// 创建时间：2012-8-8
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
	///INV_CheckInventoryDetail数据实体类
	/// </summary>
	[Serializable]
	public class INV_CheckInventoryDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _checkid = 0;
		private int _product = 0;
		private string _lotnumber = string.Empty;
		private int _bookquantity = 0;
		private int _actcheckquantity = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_CheckInventoryDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_CheckInventoryDetail(int id, int checkid, int product, string lotnumber, int bookquantity, int actcheckquantity, string remark)
		{
			_id               = id;
			_checkid          = checkid;
			_product          = product;
			_lotnumber        = lotnumber;
			_bookquantity     = bookquantity;
			_actcheckquantity = actcheckquantity;
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
		///盘点ID
		///</summary>
		public int CheckID
		{
			get	{ return _checkid; }
			set	{ _checkid = value; }
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
		///批号
		///</summary>
		public string LotNumber
		{
			get	{ return _lotnumber; }
			set	{ _lotnumber = value; }
		}

		///<summary>
		///账面数量
		///</summary>
		public int BookQuantity
		{
			get	{ return _bookquantity; }
			set	{ _bookquantity = value; }
		}

		///<summary>
		///实盘数量
		///</summary>
		public int ActCheckQuantity
		{
			get	{ return _actcheckquantity; }
			set	{ _actcheckquantity = value; }
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
            get { return "INV_CheckInventoryDetail"; }
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
					case "CheckID":
						return _checkid.ToString();
					case "Product":
						return _product.ToString();
					case "LotNumber":
						return _lotnumber;
					case "BookQuantity":
						return _bookquantity.ToString();
					case "ActCheckQuantity":
						return _actcheckquantity.ToString();
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
					case "CheckID":
						int.TryParse(value, out _checkid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "LotNumber":
						_lotnumber = value ;
						break;
					case "BookQuantity":
						int.TryParse(value, out _bookquantity);
						break;
					case "ActCheckQuantity":
						int.TryParse(value, out _actcheckquantity);
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
