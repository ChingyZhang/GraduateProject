// ===================================================================
// 文件： CM_Vehicle.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_Vehicle数据实体类
	/// </summary>
	[Serializable]
	public class CM_Vehicle : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _vehicleno = string.Empty;
		private string _vin = string.Empty;
		private int _vehicleclassify = 0;
		private string _nameplate = string.Empty;
		private decimal _tonnage = 0;
		private int _seatnum = 0;
		private DateTime _purchasedate = new DateTime(1900,1,1);
		private int _state = 0;
		private int _kilometres = 0;
		private int _avgoilwear = 0;
		private int _client = 0;
		private int _relatestaff = 0;
		private int _relatewarehouse = 0;
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_Vehicle()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_Vehicle(int id, string vehicleno, string vin, int vehicleclassify, string nameplate, decimal tonnage, int seatnum, DateTime purchasedate, int state, int kilometres, int avgoilwear, int client, int relatestaff, int relatewarehouse, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id              = id;
			_vehicleno       = vehicleno;
			_vin             = vin;
			_vehicleclassify = vehicleclassify;
			_nameplate       = nameplate;
			_tonnage         = tonnage;
			_seatnum         = seatnum;
			_purchasedate    = purchasedate;
			_state           = state;
			_kilometres      = kilometres;
			_avgoilwear      = avgoilwear;
			_client          = client;
			_relatestaff     = relatestaff;
			_relatewarehouse = relatewarehouse;
			_remark          = remark;
			_approveflag     = approveflag;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
			
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
		///车号
		///</summary>
		public string VehicleNo
		{
			get	{ return _vehicleno; }
			set	{ _vehicleno = value; }
		}

		///<summary>
		///车架号
		///</summary>
		public string VIN
		{
			get	{ return _vin; }
			set	{ _vin = value; }
		}

		///<summary>
		///车辆类别
		///</summary>
		public int VehicleClassify
		{
			get	{ return _vehicleclassify; }
			set	{ _vehicleclassify = value; }
		}

		///<summary>
		///品牌型号
		///</summary>
		public string Nameplate
		{
			get	{ return _nameplate; }
			set	{ _nameplate = value; }
		}

		///<summary>
		///吨位
		///</summary>
		public decimal Tonnage
		{
			get	{ return _tonnage; }
			set	{ _tonnage = value; }
		}

		///<summary>
		///坐位数
		///</summary>
		public int SeatNum
		{
			get	{ return _seatnum; }
			set	{ _seatnum = value; }
		}

		///<summary>
		///购买日期
		///</summary>
		public DateTime PurchaseDate
		{
			get	{ return _purchasedate; }
			set	{ _purchasedate = value; }
		}

		///<summary>
		///状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///公里数
		///</summary>
		public int Kilometres
		{
			get	{ return _kilometres; }
			set	{ _kilometres = value; }
		}

		///<summary>
		///平均百公里油耗
		///</summary>
		public int AvgOilWear
		{
			get	{ return _avgoilwear; }
			set	{ _avgoilwear = value; }
		}

		///<summary>
		///客户ID
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///关联员工
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///关联车仓库
		///</summary>
		public int RelateWareHouse
		{
			get	{ return _relatewarehouse; }
			set	{ _relatewarehouse = value; }
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
		///审核标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///新增人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///更新日期
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
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
            get { return "CM_Vehicle"; }
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
					case "VehicleNo":
						return _vehicleno;
					case "VIN":
						return _vin;
					case "VehicleClassify":
						return _vehicleclassify.ToString();
					case "Nameplate":
						return _nameplate;
					case "Tonnage":
						return _tonnage.ToString();
					case "SeatNum":
						return _seatnum.ToString();
					case "PurchaseDate":
						return _purchasedate.ToString();
					case "State":
						return _state.ToString();
					case "Kilometres":
						return _kilometres.ToString();
					case "AvgOilWear":
						return _avgoilwear.ToString();
					case "Client":
						return _client.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "RelateWareHouse":
						return _relatewarehouse.ToString();
					case "Remark":
						return _remark;
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
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
					case "VehicleNo":
						_vehicleno = value ;
						break;
					case "VIN":
						_vin = value ;
						break;
					case "VehicleClassify":
						int.TryParse(value, out _vehicleclassify);
						break;
					case "Nameplate":
						_nameplate = value ;
						break;
					case "Tonnage":
						decimal.TryParse(value, out _tonnage);
						break;
					case "SeatNum":
						int.TryParse(value, out _seatnum);
						break;
					case "PurchaseDate":
						DateTime.TryParse(value, out _purchasedate);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "Kilometres":
						int.TryParse(value, out _kilometres);
						break;
					case "AvgOilWear":
						int.TryParse(value, out _avgoilwear);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "RelateWareHouse":
						int.TryParse(value, out _relatewarehouse);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
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
