// ===================================================================
// 文件： PDT_ProductInSeries.cs
// 项目名称：
// 创建时间：2009-4-27
// 作者:	   chenli
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_ProductInSeries数据实体类
	/// </summary>
	[Serializable]
	public class PDT_ProductInSeries : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _product = 0;
		private int _series = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ProductInSeries()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_ProductInSeries(int id, int product, int series)
		{
			_id      = id;
			_product = product;
			_series  = series;
			
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
		///Product
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///Series
		///</summary>
		public int Series
		{
			get	{ return _series; }
			set	{ _series = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PDT_ProductInSeries"; }
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
					case "Product":
						return _product.ToString();
					case "Series":
						return _series.ToString();
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
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Series":
						int.TryParse(value, out _series);
						break;

				}
			}
        }
		#endregion
	}
}
