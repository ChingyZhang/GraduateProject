// ===================================================================
// 文件： ORD_DeliveryCodeLib.cs
// 项目名称：
// 创建时间：2012-7-22
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
	///ORD_DeliveryCodeLib数据实体类
	/// </summary>
	[Serializable]
	public class ORD_DeliveryCodeLib : IModel
	{
		#region 私有变量定义
		private long _id = 0;
		private int _deliveryid = 0;
		private int _product = 0;
		private string _casecode = string.Empty;
		private string _piececode = string.Empty;
		private string _lotnumber = string.Empty;
		private int _state = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_DeliveryCodeLib()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_DeliveryCodeLib(long id, int deliveryid, int product, string casecode, string piececode, string lotnumber, int state)
		{
			_id           = id;
			_deliveryid   = deliveryid;
			_product      = product;
			_casecode     = casecode;
			_piececode    = piececode;
			_lotnumber    = lotnumber;
			_state        = state;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
		public long ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///发货单
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

		///<summary>
		///整箱码
		///</summary>
		public string CaseCode
		{
			get	{ return _casecode; }
			set	{ _casecode = value; }
		}

		///<summary>
		///单件码
		///</summary>
		public string PieceCode
		{
			get	{ return _piececode; }
			set	{ _piececode = value; }
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
		///状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
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
            get { return "ORD_DeliveryCodeLib"; }
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
					case "CaseCode":
						return _casecode;
					case "PieceCode":
						return _piececode;
					case "LotNumber":
						return _lotnumber;
					case "State":
						return _state.ToString();
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
						long.TryParse(value, out _id);
						break;
					case "DeliveryID":
						int.TryParse(value, out _deliveryid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "CaseCode":
						_casecode = value ;
						break;
					case "PieceCode":
						_piececode = value ;
						break;
					case "LotNumber":
						_lotnumber = value ;
						break;
					case "State":
						int.TryParse(value, out _state);
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
