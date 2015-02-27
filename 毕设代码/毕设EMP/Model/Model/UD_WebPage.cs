// ===================================================================
// 文件： UD_WebPage.cs
// 项目名称：
// 创建时间：2009/3/7
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
	///UD_WebPage数据实体类
	/// </summary>
	[Serializable]
	public class UD_WebPage : IModel
	{
		#region 私有变量定义
        private Guid _id = Guid.NewGuid();
		private string _path = string.Empty;
        private string _subcode = string.Empty;
		private string _title = string.Empty;
		private int _module = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_WebPage()
		{
		}
		
		///<summary>
		///
		///</summary>
        public UD_WebPage(Guid id, string path, string subcode, string title, int module)
		{
			_id           = id;
			_path         = path;
            _subcode = subcode;
			_title        = title;
			_module       = module;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
        public Guid ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///Path
		///</summary>
		public string Path
		{
			get	{ return _path; }
			set	{ _path = value; }
		}

        ///<summary>
        ///SubCode
        ///</summary>
        public string SubCode
        {
            get { return _subcode; }
            set { _subcode = value; }
        }

		///<summary>
		///Title
		///</summary>
		public string Title
		{
			get	{ return _title; }
			set	{ _title = value; }
		}

		///<summary>
		///Module
		///</summary>
		public int Module
		{
			get	{ return _module; }
			set	{ _module = value; }
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
            get { return "UD_WebPage"; }
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
					case "Path":
						return _path;
                    case "SubCode":
                        return _subcode;
					case "Title":
						return _title;
					case "Module":
						return _module.ToString();
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
                        _id = new Guid(value);
						break;
					case "Path":
						_path = value ;
						break;
                    case "SubCode":
                        _subcode = value;
                        break;
					case "Title":
						_title = value ;
						break;
					case "Module":
						int.TryParse(value, out _module);
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
