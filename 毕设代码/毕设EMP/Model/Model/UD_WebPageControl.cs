// ===================================================================
// 文件： UD_WebPageControl.cs
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
	///UD_WebPageControl数据实体类
	/// </summary>
	[Serializable]
	public class UD_WebPageControl : IModel
	{
		#region 私有变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _webpageid;
		private string _controlname = string.Empty;
		private int _controlindex = 0;
		private string _text = string.Empty;
		private string _visibleactioncode = string.Empty;
		private string _enableactioncode = string.Empty;
		private string _description = string.Empty;
		private string _controltype = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_WebPageControl()
		{
		}
		
		///<summary>
		///
		///</summary>
        public UD_WebPageControl(Guid id, Guid webpage, string controlname, int controlindex, string text, string visibleactioncode, string enableactioncode, string description, string controltype)
		{
			_id                = id;
			_webpageid           = webpage;
			_controlname       = controlname;
			_controlindex      = controlindex;
			_text              = text;
			_visibleactioncode = visibleactioncode;
			_enableactioncode  = enableactioncode;
			_description       = description;
			_controltype       = controltype;
			
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
		///WebPageID
		///</summary>
        public Guid WebPageID
		{
			get	{ return _webpageid; }
			set	{ _webpageid = value; }
		}

		///<summary>
		///ControlName
		///</summary>
		public string ControlName
		{
			get	{ return _controlname; }
			set	{ _controlname = value; }
		}

		///<summary>
		///ControlIndex
		///</summary>
		public int ControlIndex
		{
			get	{ return _controlindex; }
			set	{ _controlindex = value; }
		}

		///<summary>
		///Text
		///</summary>
		public string Text
		{
			get	{ return _text; }
			set	{ _text = value; }
		}

		///<summary>
		///VisibleActionCode
		///</summary>
		public string VisibleActionCode
		{
			get	{ return _visibleactioncode; }
			set	{ _visibleactioncode = value; }
		}

		///<summary>
		///EnableActionCode
		///</summary>
		public string EnableActionCode
		{
			get	{ return _enableactioncode; }
			set	{ _enableactioncode = value; }
		}

		///<summary>
		///Description
		///</summary>
		public string Description
		{
			get	{ return _description; }
			set	{ _description = value; }
		}

		///<summary>
		///ControlType
		///</summary>
		public string ControlType
		{
			get	{ return _controltype; }
			set	{ _controltype = value; }
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
            get { return "UD_WebPageControl"; }
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
					case "WebPageID":
						return _webpageid.ToString();
					case "ControlName":
						return _controlname;
					case "ControlIndex":
						return _controlindex.ToString();
					case "Text":
						return _text;
					case "VisibleActionCode":
						return _visibleactioncode;
					case "EnableActionCode":
						return _enableactioncode;
					case "Description":
						return _description;
					case "ControlType":
						return _controltype;
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
					case "WebPageID":
                        _webpageid = new Guid(value);
						break;
					case "ControlName":
						_controlname = value ;
						break;
					case "ControlIndex":
						int.TryParse(value, out _controlindex);
						break;
					case "Text":
						_text = value ;
						break;
					case "VisibleActionCode":
						_visibleactioncode = value ;
						break;
					case "EnableActionCode":
						_enableactioncode = value ;
						break;
					case "Description":
						_description = value ;
						break;
					case "ControlType":
						_controltype = value ;
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
