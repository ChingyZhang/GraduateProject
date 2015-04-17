// ===================================================================
// 文件： CM_GeoCode.cs
// 项目名称：
// 创建时间：2015-03-24
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
	///CM_GeoCode数据实体类
	/// </summary>
	[Serializable]
	public class CM_GeoCode : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private string _code = string.Empty;
		private string _citycode = string.Empty;
		private int _officialcity = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_GeoCode()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_GeoCode(int id, string name, string code, string citycode, int officialcity, string remark, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_name         = name;
			_code         = code;
			_citycode     = citycode;
			_officialcity = officialcity;
			_remark       = remark;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			
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
		///Code
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///CityCode
		///</summary>
		public string CityCode
		{
			get	{ return _citycode; }
			set	{ _citycode = value; }
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
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
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
            get { return "CM_GeoCode"; }
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
					case "Code":
						return _code;
					case "CityCode":
						return _citycode;
					case "OfficialCity":
						return _officialcity.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "Name":
						_name = value;
						break;
					case "Code":
						_code = value;
						break;
					case "CityCode":
						_citycode = value;
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "Remark":
						_remark = value;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
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
