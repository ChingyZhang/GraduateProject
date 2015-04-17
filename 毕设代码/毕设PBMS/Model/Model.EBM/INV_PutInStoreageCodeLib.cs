// ===================================================================
// 文件： INV_PutInStoreageCodeLib.cs
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
	///INV_PutInStoreageCodeLib数据实体类
	/// </summary>
	[Serializable]
	public class INV_PutInStoreageCodeLib : IModel
	{
		#region 私有变量定义
		private long _id = 0;
		private int _putinid = 0;
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
		public INV_PutInStoreageCodeLib()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_PutInStoreageCodeLib(long id, int putinid, int product, string casecode, string piececode, string lotnumber, int state)
		{
			_id           = id;
			_putinid      = putinid;
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
		///入库单
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
		///箱码
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
            get { return "INV_PutInStoreageCodeLib"; }
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
					case "PutInID":
						int.TryParse(value, out _putinid);
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
