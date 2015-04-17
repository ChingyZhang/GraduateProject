// ===================================================================
// 文件： CM_ClientGeoInfo.cs
// 项目名称：
// 创建时间：2014-03-09
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_ClientGeoInfo数据实体类
	/// </summary>
	[Serializable]
	public class CM_ClientGeoInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private decimal _latitude = 0;
		private decimal _longitude = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private Guid _updateuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_ClientGeoInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_ClientGeoInfo(int id, int client, decimal latitude, decimal longitude, string remark, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id           = id;
			_client       = client;
			_latitude     = latitude;
			_longitude    = longitude;
			_remark       = remark;
			_inserttime   = inserttime;
			_insertuser   = insertuser;
			_updatetime   = updatetime;
			_updateuser   = updateuser;
			
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
		///客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///纬度
		///</summary>
		public decimal Latitude
		{
			get	{ return _latitude; }
			set	{ _latitude = value; }
		}

		///<summary>
		///经度
		///</summary>
		public decimal Longitude
		{
			get	{ return _longitude; }
			set	{ _longitude = value; }
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
		///录入时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///录入人
		///</summary>
		public Guid InsertUser
		{
			get	{ return _insertuser; }
			set	{ _insertuser = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public Guid UpdateUser
		{
			get	{ return _updateuser; }
			set	{ _updateuser = value; }
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
            get { return "CM_ClientGeoInfo"; }
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
					case "Client":
						return _client.ToString();
					case "Latitude":
						return _latitude.ToString();
					case "Longitude":
						return _longitude.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateUser":
						return _updateuser.ToString();
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
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Latitude":
						decimal.TryParse(value, out _latitude);
						break;
					case "Longitude":
						decimal.TryParse(value, out _longitude);
						break;
					case "Remark":
						_remark = value;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertUser":
						_insertuser = new Guid(value);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateUser":
						_updateuser = new Guid(value);
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
