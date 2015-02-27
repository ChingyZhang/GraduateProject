// ===================================================================
// 文件： CAT_SalesVolumeDetail.cs
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
	///CAT_SalesVolumeDetail数据实体类
	/// </summary>
	[Serializable]
	public class CAT_SalesVolumeDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _activity = 0;
		private int _brand = 0;
		private decimal _amount = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_SalesVolumeDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CAT_SalesVolumeDetail(int id, int activity, int brand, decimal amount, string remark)
		{
			_id           = id;
			_activity     = activity;
			_brand        = brand;
			_amount       = amount;
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
		///活动ID
		///</summary>
		public int Activity
		{
			get	{ return _activity; }
			set	{ _activity = value; }
		}

		///<summary>
		///品牌
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///金额
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
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
            get { return "CAT_SalesVolumeDetail"; }
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
					case "Brand":
						return _brand.ToString();
					case "Amount":
						return _amount.ToString();
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
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
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
