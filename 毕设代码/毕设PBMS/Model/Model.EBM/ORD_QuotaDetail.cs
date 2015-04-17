// ===================================================================
// 文件： ORD_QuotaDetail.cs
// 项目名称：
// 创建时间：2014-01-24
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_QuotaDetail数据实体类
	/// </summary>
	[Serializable]
	public class ORD_QuotaDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _quotaid = 0;
		private int _brand = 0;
		private int _classify = 0;
		private int _product = 0;
		private int _stdquota = 0;
		private int _adjquota = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_QuotaDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_QuotaDetail(int id, int quotaid, int brand, int classify, int product, int stdquota, int adjquota, string remark)
		{
			_id           = id;
			_quotaid      = quotaid;
			_brand        = brand;
			_classify     = classify;
			_product      = product;
			_stdquota     = stdquota;
			_adjquota     = adjquota;
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
		///配额ID
		///</summary>
		public int QuotaID
		{
			get	{ return _quotaid; }
			set	{ _quotaid = value; }
		}

		///<summary>
		///产品品牌
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///产品系列
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
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
		///标准配额量
		///</summary>
		public int StdQuota
		{
			get	{ return _stdquota; }
			set	{ _stdquota = value; }
		}

		///<summary>
		///调整配额量
		///</summary>
		public int AdjQuota
		{
			get	{ return _adjquota; }
			set	{ _adjquota = value; }
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
            get { return "ORD_QuotaDetail"; }
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
					case "QuotaID":
						return _quotaid.ToString();
					case "Brand":
						return _brand.ToString();
					case "Classify":
						return _classify.ToString();
					case "Product":
						return _product.ToString();
					case "StdQuota":
						return _stdquota.ToString();
					case "AdjQuota":
						return _adjquota.ToString();
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
					case "QuotaID":
						int.TryParse(value, out _quotaid);
						break;
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "StdQuota":
						int.TryParse(value, out _stdquota);
						break;
					case "AdjQuota":
						int.TryParse(value, out _adjquota);
						break;
					case "Remark":
						_remark = value;
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
