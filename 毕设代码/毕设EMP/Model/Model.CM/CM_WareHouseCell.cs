// ===================================================================
// 文件： CM_WareHouseCell.cs
// 项目名称：
// 创建时间：2012-7-21
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
	///CM_WareHouseCell数据实体类
	/// </summary>
	[Serializable]
	public class CM_WareHouseCell : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _warehouse = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _area = 0;
		private int _capability = 0;
		private int _activestate = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_WareHouseCell()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_WareHouseCell(int id, int warehouse, string code, string name, int area, int capability, int activestate, string remark, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_warehouse    = warehouse;
			_code         = code;
			_name         = name;
			_area         = area;
			_capability   = capability;
			_activestate  = activestate;
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
		///所属仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
		}

		///<summary>
		///库位代码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///库位名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///面积
		///</summary>
		public int Area
		{
			get	{ return _area; }
			set	{ _area = value; }
		}

		///<summary>
		///容量
		///</summary>
		public int Capability
		{
			get	{ return _capability; }
			set	{ _capability = value; }
		}

		///<summary>
		///活跃状态
		///</summary>
		public int ActiveState
		{
			get	{ return _activestate; }
			set	{ _activestate = value; }
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
            get { return "CM_WareHouseCell"; }
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
					case "WareHouse":
						return _warehouse.ToString();
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "Area":
						return _area.ToString();
					case "Capability":
						return _capability.ToString();
					case "ActiveState":
						return _activestate.ToString();
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
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "Code":
						_code = value ;
						break;
					case "Name":
						_name = value ;
						break;
					case "Area":
						int.TryParse(value, out _area);
						break;
					case "Capability":
						int.TryParse(value, out _capability);
						break;
					case "ActiveState":
						int.TryParse(value, out _activestate);
						break;
					case "Remark":
						_remark = value ;
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
