// ===================================================================
// 文件： Const_IPLocation.cs
// 项目名称：
// 创建时间：2009/6/21
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
	///Const_IPLocation数据实体类
	/// </summary>
	[Serializable]
	public class Const_IPLocation : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _ip = string.Empty;
		private string _location = string.Empty;
		private int _officialcity = 0;
		private int _isp = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Const_IPLocation()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Const_IPLocation(int id, string ip, string location, int officialcity, int isp)
		{
			_id           = id;
			_ip           = ip;
			_location     = location;
			_officialcity = officialcity;
			_isp          = isp;
			
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
		///IP
		///</summary>
		public string IP
		{
			get	{ return _ip; }
			set	{ _ip = value; }
		}

		///<summary>
		///Location
		///</summary>
		public string Location
		{
			get	{ return _location; }
			set	{ _location = value; }
		}

		///<summary>
		///OfficialCity
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}

		///<summary>
		///ISP
		///</summary>
		public int ISP
		{
			get	{ return _isp; }
			set	{ _isp = value; }
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
            get { return "Const_IPLocation"; }
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
					case "IP":
						return _ip;
					case "Location":
						return _location;
					case "OfficialCity":
						return _officialcity.ToString();
					case "ISP":
						return _isp.ToString();
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
					case "IP":
						_ip = value ;
						break;
					case "Location":
						_location = value ;
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "ISP":
						int.TryParse(value, out _isp);
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
