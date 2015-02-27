// ===================================================================
// 文件： CM_RebateRule_ApplyCity.cs
// 项目名称：
// 创建时间：2011/9/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_RebateRule_ApplyCity数据实体类
	/// </summary>
	[Serializable]
	public class CM_RebateRule_ApplyCity : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _rebaterule = 0;
		private int _organizecity = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_RebateRule_ApplyCity()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_RebateRule_ApplyCity(int id, int rebaterule, int organizecity)
		{
			_id           = id;
			_rebaterule   = rebaterule;
			_organizecity = organizecity;
			
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
		///RebateRule
		///</summary>
		public int RebateRule
		{
			get	{ return _rebaterule; }
			set	{ _rebaterule = value; }
		}

		///<summary>
		///OrganizeCity
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
            get { return "CM_RebateRule_ApplyCity"; }
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
					case "RebateRule":
						return _rebaterule.ToString();
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
					case "RebateRule":
						int.TryParse(value, out _rebaterule);
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
