// ===================================================================
// 文件： BBS_Board.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///BBS_Board数据实体类
	/// </summary>
	[Serializable]
	public class BBS_Board : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _catalog = 0;
		private string _name = string.Empty;
		private string _description = string.Empty;
		private string _ispublic = string.Empty;
		private string _canuplodatt = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_Board()
		{
		}
		
		///<summary>
		///
		///</summary>
		public BBS_Board(int id, int catalog, string name, string description, string ispublic, string canuplodatt)
		{
			_id           = id;
			_catalog      = catalog;
			_name         = name;
			_description  = description;
			_ispublic     = ispublic;
			_canuplodatt  = canuplodatt;
			
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
		///所属分类
		///</summary>
		public int Catalog
		{
			get	{ return _catalog; }
			set	{ _catalog = value; }
		}

		///<summary>
		///板块名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///描述
		///</summary>
		public string Description
		{
			get	{ return _description; }
			set	{ _description = value; }
		}

		///<summary>
		///是否公共
		///</summary>
		public string IsPublic
		{
			get	{ return _ispublic;}
            set { _ispublic=value;}
		}

		///<summary>
		///可否上传附件
		///</summary>
		public string CanUplodAtt
		{
			get	{ return _canuplodatt; }
			set	{ _canuplodatt = value; }
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
            get { return "BBS_Board"; }
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
					case "Catalog":
						return _catalog.ToString();
					case "Name":
						return _name;
					case "Description":
						return _description;
					case "IsPublic":
                        return _ispublic;
					case "CanUplodAtt":
						return _canuplodatt;
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
					case "Catalog":
						int.TryParse(value, out _catalog);
						break;
					case "Name":
						_name = value ;
						break;
					case "Description":
						_description = value ;
						break;
					case "IsPublic":
                        _ispublic = value;
						break;
					case "CanUplodAtt":
						_canuplodatt = value ;
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
