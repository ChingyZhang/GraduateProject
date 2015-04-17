// ===================================================================
// 文件： CM_VehicleInStaff.cs
// 项目名称：
// 创建时间：2015-02-01
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
	///CM_VehicleInStaff数据实体类
	/// </summary>
	[Serializable]
	public class CM_VehicleInStaff : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _staff = 0;
		private int _vehicle = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_VehicleInStaff()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_VehicleInStaff(int id, int staff, int vehicle, string remark, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_staff        = staff;
			_vehicle      = vehicle;
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
		///员工
		///</summary>
		public int Staff
		{
			get	{ return _staff; }
			set	{ _staff = value; }
		}

		///<summary>
		///车辆
		///</summary>
		public int Vehicle
		{
			get	{ return _vehicle; }
			set	{ _vehicle = value; }
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
            get { return "CM_VehicleInStaff"; }
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
					case "Staff":
						return _staff.ToString();
					case "Vehicle":
						return _vehicle.ToString();
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
					case "Staff":
						int.TryParse(value, out _staff);
						break;
					case "Vehicle":
						int.TryParse(value, out _vehicle);
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
