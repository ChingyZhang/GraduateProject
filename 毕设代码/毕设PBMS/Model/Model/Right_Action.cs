// ===================================================================
// 文件： Right_Action.cs
// 项目名称：
// 创建时间：2009/3/5
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///Right_Action数据实体类
	/// </summary>
	[Serializable]
	public class Right_Action : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _module = 0;
		private string _remark = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Right_Action()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Right_Action(int id, string code, string name, int module, string remark)
		{
			_id     = id;
			_code   = code;
			_name   = name;
			_module = module;
			_remark = remark;
			
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
		///Code
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
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
		///Module
		///</summary>
		public int Module
		{
			get	{ return _module; }
			set	{ _module = value; }
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
            get { return "Right_Action"; }
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
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "Module":
						return _module.ToString();
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
					case "Code":
						_code = value ;
						break;
					case "Name":
						_name = value ;
						break;
					case "Module":
						int.TryParse(value, out _module);
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
