// ===================================================================
// 文件： PDT_Product.cs
// 项目名称：
// 创建时间：2015/1/30
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_Product数据实体类
	/// </summary>
	[Serializable]
	public class PDT_Product : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _fullname = string.Empty;
		private string _shortname = string.Empty;
		private string _code = string.Empty;
		private string _spec = string.Empty;
		private int _trafficpackaging = 0;
		private int _packaging = 0;
		private int _convertfactor = 0;
		private string _boxbarcode = string.Empty;
		private string _barcode = string.Empty;
		private int _category = 0;
		private int _brand = 0;
		private int _classify = 0;
		private int _grade = 0;
		private decimal _weight = 0;
		private decimal _volume = 0;
		private string _factoryname = string.Empty;
		private int _manufacturer = 0;
		private string _factorycode = string.Empty;
		private int _factoryerpid = 0;
		private decimal _factoryprice = 0;
		private decimal _tradeprice = 0;
		private decimal _stdprice = 0;
		private decimal _netprice = 0;
		private int _state = 0;
		private int _expiry = 0;
		private int _ownertype = 0;
		private int _ownerclient = 0;
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
		public PDT_Product()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_Product(int id, string fullname, string shortname, string code, string spec, int trafficpackaging, int packaging, int convertfactor, string boxbarcode, string barcode, int category, int brand, int classify, int grade, decimal weight, decimal volume, string factoryname, int manufacturer, string factorycode, int factoryerpid, decimal factoryprice, decimal tradeprice, decimal stdprice, decimal netprice, int state, int expiry, int ownertype, int ownerclient, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id               = id;
			_fullname         = fullname;
			_shortname        = shortname;
			_code             = code;
			_spec             = spec;
			_trafficpackaging = trafficpackaging;
			_packaging        = packaging;
			_convertfactor    = convertfactor;
			_boxbarcode       = boxbarcode;
			_barcode          = barcode;
			_category         = category;
			_brand            = brand;
			_classify         = classify;
			_grade            = grade;
			_weight           = weight;
			_volume           = volume;
			_factoryname      = factoryname;
			_manufacturer     = manufacturer;
			_factorycode      = factorycode;
			_factoryerpid     = factoryerpid;
			_factoryprice     = factoryprice;
			_tradeprice       = tradeprice;
			_stdprice         = stdprice;
			_netprice         = netprice;
			_state            = state;
			_expiry           = expiry;
			_ownertype        = ownertype;
			_ownerclient      = ownerclient;
			_remark           = remark;
			_approveflag      = approveflag;
			_inserttime       = inserttime;
			_insertstaff      = insertstaff;
			_updatetime       = updatetime;
			_updatestaff      = updatestaff;
			
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
		///产品名称
		///</summary>
		public string FullName
		{
			get	{ return _fullname; }
			set	{ _fullname = value; }
		}

		///<summary>
		///产品简称
		///</summary>
		public string ShortName
		{
			get	{ return _shortname; }
			set	{ _shortname = value; }
		}

		///<summary>
		///商品编码（平台级编码）
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///规格
		///</summary>
		public string Spec
		{
			get	{ return _spec; }
			set	{ _spec = value; }
		}

		///<summary>
		///整件单位
		///</summary>
		public int TrafficPackaging
		{
			get	{ return _trafficpackaging; }
			set	{ _trafficpackaging = value; }
		}

		///<summary>
		///零售单位
		///</summary>
		public int Packaging
		{
			get	{ return _packaging; }
			set	{ _packaging = value; }
		}

		///<summary>
		///包装系数
		///</summary>
		public int ConvertFactor
		{
			get	{ return _convertfactor; }
			set	{ _convertfactor = value; }
		}

		///<summary>
		///整件商品条码
		///</summary>
		public string BoxBarCode
		{
			get	{ return _boxbarcode; }
			set	{ _boxbarcode = value; }
		}

		///<summary>
		///零售商品条码
		///</summary>
		public string BarCode
		{
			get	{ return _barcode; }
			set	{ _barcode = value; }
		}

		///<summary>
		///Category
		///</summary>
		public int Category
		{
			get	{ return _category; }
			set	{ _category = value; }
		}

		///<summary>
		///商品品牌
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///商品系列
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///段位
		///</summary>
		public int Grade
		{
			get	{ return _grade; }
			set	{ _grade = value; }
		}

		///<summary>
		///整件重量
		///</summary>
		public decimal Weight
		{
			get	{ return _weight; }
			set	{ _weight = value; }
		}

		///<summary>
		///整件体积
		///</summary>
		public decimal Volume
		{
			get	{ return _volume; }
			set	{ _volume = value; }
		}

		///<summary>
		///厂家名称
		///</summary>
		public string FactoryName
		{
			get	{ return _factoryname; }
			set	{ _factoryname = value; }
		}

		///<summary>
		///生产厂商
		///</summary>
		public int Manufacturer
		{
			get	{ return _manufacturer; }
			set	{ _manufacturer = value; }
		}

		///<summary>
		///厂家商品码
		///</summary>
		public string FactoryCode
		{
			get	{ return _factorycode; }
			set	{ _factorycode = value; }
		}

		///<summary>
		///厂家商品ID
		///</summary>
		public int FactoryERPID
		{
			get	{ return _factoryerpid; }
			set	{ _factoryerpid = value; }
		}

		///<summary>
		///标准经销价
		///</summary>
		public decimal FactoryPrice
		{
			get	{ return _factoryprice; }
			set	{ _factoryprice = value; }
		}

		///<summary>
		///标准分销价
		///</summary>
		public decimal TradePrice
		{
			get	{ return _tradeprice; }
			set	{ _tradeprice = value; }
		}

		///<summary>
		///建议零售价
		///</summary>
		public decimal StdPrice
		{
			get	{ return _stdprice; }
			set	{ _stdprice = value; }
		}

		///<summary>
		///成本价
		///</summary>
		public decimal NetPrice
		{
			get	{ return _netprice; }
			set	{ _netprice = value; }
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
		///保质期
		///</summary>
		public int Expiry
		{
			get	{ return _expiry; }
			set	{ _expiry = value; }
		}

		///<summary>
		///所有权属性
		///</summary>
		public int OwnerType
		{
			get	{ return _ownertype; }
			set	{ _ownertype = value; }
		}

		///<summary>
		///所有权人
		///</summary>
		public int OwnerClient
		{
			get	{ return _ownerclient; }
			set	{ _ownerclient = value; }
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
            get { return "PDT_Product"; }
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
					case "FullName":
						return _fullname;
					case "ShortName":
						return _shortname;
					case "Code":
						return _code;
					case "Spec":
						return _spec;
					case "TrafficPackaging":
						return _trafficpackaging.ToString();
					case "Packaging":
						return _packaging.ToString();
					case "ConvertFactor":
						return _convertfactor.ToString();
					case "BoxBarCode":
						return _boxbarcode;
					case "BarCode":
						return _barcode;
					case "Category":
						return _category.ToString();
					case "Brand":
						return _brand.ToString();
					case "Classify":
						return _classify.ToString();
					case "Grade":
						return _grade.ToString();
					case "Weight":
						return _weight.ToString();
					case "Volume":
						return _volume.ToString();
					case "FactoryName":
						return _factoryname;
					case "Manufacturer":
						return _manufacturer.ToString();
					case "FactoryCode":
						return _factorycode;
					case "FactoryERPID":
						return _factoryerpid.ToString();
					case "FactoryPrice":
						return _factoryprice.ToString();
					case "TradePrice":
						return _tradeprice.ToString();
					case "StdPrice":
						return _stdprice.ToString();
					case "NetPrice":
						return _netprice.ToString();
					case "State":
						return _state.ToString();
					case "Expiry":
						return _expiry.ToString();
					case "OwnerType":
						return _ownertype.ToString();
					case "OwnerClient":
						return _ownerclient.ToString();
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
					case "FullName":
						_fullname = value ;
						break;
					case "ShortName":
						_shortname = value ;
						break;
					case "Code":
						_code = value ;
						break;
					case "Spec":
						_spec = value ;
						break;
					case "TrafficPackaging":
						int.TryParse(value, out _trafficpackaging);
						break;
					case "Packaging":
						int.TryParse(value, out _packaging);
						break;
					case "ConvertFactor":
						int.TryParse(value, out _convertfactor);
						break;
					case "BoxBarCode":
						_boxbarcode = value ;
						break;
					case "BarCode":
						_barcode = value ;
						break;
					case "Category":
						int.TryParse(value, out _category);
						break;
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "Grade":
						int.TryParse(value, out _grade);
						break;
					case "Weight":
						decimal.TryParse(value, out _weight);
						break;
					case "Volume":
						decimal.TryParse(value, out _volume);
						break;
					case "FactoryName":
						_factoryname = value ;
						break;
					case "Manufacturer":
						int.TryParse(value, out _manufacturer);
						break;
					case "FactoryCode":
						_factorycode = value ;
						break;
					case "FactoryERPID":
						int.TryParse(value, out _factoryerpid);
						break;
					case "FactoryPrice":
						decimal.TryParse(value, out _factoryprice);
						break;
					case "TradePrice":
						decimal.TryParse(value, out _tradeprice);
						break;
					case "StdPrice":
						decimal.TryParse(value, out _stdprice);
						break;
					case "NetPrice":
						decimal.TryParse(value, out _netprice);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "Expiry":
						int.TryParse(value, out _expiry);
						break;
					case "OwnerType":
						int.TryParse(value, out _ownertype);
						break;
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
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
