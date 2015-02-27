// ===================================================================
// 文件： PDT_StandardPrice_ApplyOrganizeCity.cs
// 项目名称：
// 创建时间：2011/9/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_StandardPrice_ApplyCity数据实体类
	/// </summary>
	[Serializable]
	public class PDT_StandardPrice_ApplyCity : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _standardprice = 0;
		private int _organizecity = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice_ApplyCity()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice_ApplyCity(int id, int standardprice, int organizecity)
		{
			_id            = id;
			_standardprice = standardprice;
			_organizecity  = organizecity;
			
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
		///价表
		///</summary>
		public int StandardPrice
		{
			get	{ return _standardprice; }
			set	{ _standardprice = value; }
		}

		///<summary>
		///适用管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
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
            get { return "PDT_StandardPrice_ApplyCity"; }
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
					case "StandardPrice":
						return _standardprice.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
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
					case "StandardPrice":
						int.TryParse(value, out _standardprice);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
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
