// ===================================================================
// 文件： GIS_OfficialCityGeo.cs
// 项目名称：
// 创建时间：2010/9/11
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
	///GIS_OfficialCityGeo数据实体类
	/// </summary>
	[Serializable]
	public class GIS_OfficialCityGeo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _officialcity = 0;
		private double _latitude = 0;
        private double _longitude = 0;
        private double _latlonbox_north = 0;
        private double _latlonbox_east = 0;
        private double _latlonbox_south = 0;
        private double _latlonbox_west = 0;
		private string _address = string.Empty;
		private string _accuracy = string.Empty;
		private string _countrynamecode = string.Empty;
		private string _countryname = string.Empty;
		private string _administrativeareaname = string.Empty;
		private string _localityname = string.Empty;
		private string _dependentlocalityname = string.Empty;
		private string _thoroughfarename = string.Empty;
		private string _addressline = string.Empty;
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public GIS_OfficialCityGeo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public GIS_OfficialCityGeo(int id, int officialcity, double latitude, double longitude, double latlonbox_north, double latlonbox_east, double latlonbox_south, double latlonbox_west, string address, string accuracy, string countrynamecode, string countryname, string administrativeareaname, string localityname, string dependentlocalityname, string thoroughfarename, string addressline, DateTime updatetime)
		{
			_id                     = id;
			_officialcity           = officialcity;
			_latitude               = latitude;
			_longitude              = longitude;
			_latlonbox_north        = latlonbox_north;
			_latlonbox_east         = latlonbox_east;
			_latlonbox_south        = latlonbox_south;
			_latlonbox_west         = latlonbox_west;
			_address                = address;
			_accuracy               = accuracy;
			_countrynamecode        = countrynamecode;
			_countryname            = countryname;
			_administrativeareaname = administrativeareaname;
			_localityname           = localityname;
			_dependentlocalityname  = dependentlocalityname;
			_thoroughfarename       = thoroughfarename;
			_addressline            = addressline;
			_updatetime             = updatetime;
			
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
		///行政城市ID
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}

		///<summary>
        ///坐标纬度X
		///</summary>
        public double Latitude
		{
			get	{ return _latitude; }
			set	{ _latitude = value; }
		}

		///<summary>
        ///坐标经度Y
		///</summary>
        public double Longitude
		{
			get	{ return _longitude; }
			set	{ _longitude = value; }
		}

		///<summary>
        ///范围纬度X(北)
		///</summary>
        public double LatLonBox_north
		{
			get	{ return _latlonbox_north; }
			set	{ _latlonbox_north = value; }
		}

		///<summary>
        ///范围经度Y(东)
		///</summary>
        public double LatLonBox_east
		{
			get	{ return _latlonbox_east; }
			set	{ _latlonbox_east = value; }
		}

		///<summary>
        ///范围纬度X(南)
		///</summary>
        public double LatLonBox_south
		{
			get	{ return _latlonbox_south; }
			set	{ _latlonbox_south = value; }
		}

		///<summary>
        ///范围经度Y(西)
		///</summary>
        public double LatLonBox_west
		{
			get	{ return _latlonbox_west; }
			set	{ _latlonbox_west = value; }
		}

		///<summary>
        ///全称地址
		///</summary>
		public string Address
		{
			get	{ return _address; }
			set	{ _address = value; }
		}

		///<summary>
        ///精度
		///</summary>
		public string Accuracy
		{
			get	{ return _accuracy; }
			set	{ _accuracy = value; }
		}

		///<summary>
        ///国家代码
		///</summary>
		public string CountryNameCode
		{
			get	{ return _countrynamecode; }
			set	{ _countrynamecode = value; }
		}

		///<summary>
        ///国家名称
		///</summary>
		public string CountryName
		{
			get	{ return _countryname; }
			set	{ _countryname = value; }
		}

		///<summary>
        ///省份(行政区)
		///</summary>
		public string AdministrativeAreaName
		{
			get	{ return _administrativeareaname; }
			set	{ _administrativeareaname = value; }
		}

		///<summary>
        ///地级市
		///</summary>
		public string LocalityName
		{
			get	{ return _localityname; }
			set	{ _localityname = value; }
		}

		///<summary>
        ///区/县
		///</summary>
		public string DependentLocalityName
		{
			get	{ return _dependentlocalityname; }
			set	{ _dependentlocalityname = value; }
		}

		///<summary>
        ///街道/镇/路
		///</summary>
		public string ThoroughfareName
		{
			get	{ return _thoroughfarename; }
			set	{ _thoroughfarename = value; }
		}

		///<summary>
        ///站点
		///</summary>
		public string AddressLine
		{
			get	{ return _addressline; }
			set	{ _addressline = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
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
            get { return "GIS_OfficialCityGeo"; }
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
					case "OfficialCity":
						return _officialcity.ToString();
					case "Latitude":
						return _latitude.ToString();
					case "Longitude":
						return _longitude.ToString();
					case "LatLonBox_north":
						return _latlonbox_north.ToString();
					case "LatLonBox_east":
						return _latlonbox_east.ToString();
					case "LatLonBox_south":
						return _latlonbox_south.ToString();
					case "LatLonBox_west":
						return _latlonbox_west.ToString();
					case "Address":
						return _address;
					case "Accuracy":
						return _accuracy;
					case "CountryNameCode":
						return _countrynamecode;
					case "CountryName":
						return _countryname;
					case "AdministrativeAreaName":
						return _administrativeareaname;
					case "LocalityName":
						return _localityname;
					case "DependentLocalityName":
						return _dependentlocalityname;
					case "ThoroughfareName":
						return _thoroughfarename;
					case "AddressLine":
						return _addressline;
					case "UpdateTime":
						return _updatetime.ToString();
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
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "Latitude":
						double.TryParse(value, out _latitude);
						break;
					case "Longitude":
						double.TryParse(value, out _longitude);
						break;
					case "LatLonBox_north":
						double.TryParse(value, out _latlonbox_north);
						break;
					case "LatLonBox_east":
						double.TryParse(value, out _latlonbox_east);
						break;
					case "LatLonBox_south":
						double.TryParse(value, out _latlonbox_south);
						break;
					case "LatLonBox_west":
						double.TryParse(value, out _latlonbox_west);
						break;
					case "Address":
						_address = value ;
						break;
					case "Accuracy":
						_accuracy = value ;
						break;
					case "CountryNameCode":
						_countrynamecode = value ;
						break;
					case "CountryName":
						_countryname = value ;
						break;
					case "AdministrativeAreaName":
						_administrativeareaname = value ;
						break;
					case "LocalityName":
						_localityname = value ;
						break;
					case "DependentLocalityName":
						_dependentlocalityname = value ;
						break;
					case "ThoroughfareName":
						_thoroughfarename = value ;
						break;
					case "AddressLine":
						_addressline = value ;
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
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
