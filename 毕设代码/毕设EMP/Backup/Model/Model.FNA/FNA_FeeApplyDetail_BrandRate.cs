// ===================================================================
// 文件： FNA_FeeApplyDetail_BrandRate.cs
// 项目名称：
// 创建时间：2011/11/10
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
	/// <summary>
	///FNA_FeeApplyDetail_BrandRate数据实体类
	/// </summary>
	[Serializable]
	public class FNA_FeeApplyDetail_BrandRate : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _detailid = 0;
		private int _brand = 0;
		private decimal _rate = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_FeeApplyDetail_BrandRate()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_FeeApplyDetail_BrandRate(int id, int detailid, int brand, decimal rate)
		{
			_id           = id;
			_detailid     = detailid;
			_brand        = brand;
			_rate         = rate;
			
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
		///申请详细单ID
		///</summary>
		public int DetailID
		{
			get	{ return _detailid; }
			set	{ _detailid = value; }
		}

		///<summary>
		///涉及品牌
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///占比
		///</summary>
		public decimal Rate
		{
			get	{ return _rate; }
			set	{ _rate = value; }
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
            get { return "FNA_FeeApplyDetail_BrandRate"; }
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
					case "DetailID":
						return _detailid.ToString();
					case "Brand":
						return _brand.ToString();
					case "Rate":
						return _rate.ToString();
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
					case "DetailID":
						int.TryParse(value, out _detailid);
						break;
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Rate":
						decimal.TryParse(value, out _rate);
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
