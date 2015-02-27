// ===================================================================
// 文件： PDT_Brand.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_Brand数据实体类
	/// </summary>
	[Serializable]
	public class PDT_Brand : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private string _isopponent = string.Empty;
		private string _remark = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_Brand()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_Brand(int id, string name, string isopponent, string remark)
		{
			_id         = id;
			_name       = name;
			_isopponent = isopponent;
			_remark     = remark;
			
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
		///Name
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///IsOpponent
		///</summary>
		public string IsOpponent
		{
			get	{ return _isopponent; }
			set	{ _isopponent = value; }
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
            get { return "PDT_Brand"; }
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
					case "Name":
						return _name;
					case "IsOpponent":
						return _isopponent;
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
					case "Name":
						_name = value ;
						break;
					case "IsOpponent":
						_isopponent = value ;
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
