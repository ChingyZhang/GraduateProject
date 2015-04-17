// ===================================================================
// 文件： VST_ReportLocation.cs
// 项目名称：
// 创建时间：2015-04-12
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_ReportLocation数据实体类
	/// </summary>
	[Serializable]
	public class VST_ReportLocation : IModel
	{
		#region 私有变量定义
		private long _id = 0;
		private int _relatestaff = 0;
		private int _locatetype = 0;
		private double _longitude = 0;
		private double _latitude = 0;
		private string _devicecode = string.Empty;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_ReportLocation()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_ReportLocation(long id, int relatestaff, int locatetype, double longitude, double latitude, string devicecode, string remark, DateTime inserttime)
		{
			_id           = id;
			_relatestaff  = relatestaff;
			_locatetype   = locatetype;
			_longitude    = longitude;
			_latitude     = latitude;
			_devicecode   = devicecode;
			_remark       = remark;
			_inserttime   = inserttime;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
		public long ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///员工
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///定位类型
		///</summary>
		public int LocateType
		{
			get	{ return _locatetype; }
			set	{ _locatetype = value; }
		}

		///<summary>
		///经度
		///</summary>
		public double Longitude
		{
			get	{ return _longitude; }
			set	{ _longitude = value; }
		}

		///<summary>
		///纬度
		///</summary>
		public double Latitude
		{
			get	{ return _latitude; }
			set	{ _latitude = value; }
		}

		///<summary>
		///设备号
		///</summary>
		public string DeviceCode
		{
			get	{ return _devicecode; }
			set	{ _devicecode = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
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
            get { return "VST_ReportLocation"; }
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
					case "RelateStaff":
						return _relatestaff.ToString();
					case "LocateType":
						return _locatetype.ToString();
					case "Longitude":
						return _longitude.ToString();
					case "Latitude":
						return _latitude.ToString();
					case "DeviceCode":
						return _devicecode;
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
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
						long.TryParse(value, out _id);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "LocateType":
						int.TryParse(value, out _locatetype);
						break;
					case "Longitude":
						double.TryParse(value, out _longitude);
						break;
					case "Latitude":
						double.TryParse(value, out _latitude);
						break;
					case "DeviceCode":
						_devicecode = value;
						break;
					case "Remark":
						_remark = value;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
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
