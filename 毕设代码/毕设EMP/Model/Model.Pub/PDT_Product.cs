// ===================================================================
// 文件： PDT_ProductModel.cs
// 项目名称：
// 创建时间：2008-12-21
// 作者:	   
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
		private int _brand = 0;
		private int _classify = 0;
		private int _grade = 0;
		private int _trafficpackaging = 0;
		private int _packaging = 0;
		private int _convertfactor = 0;
		private int _subunit = 0;
		private decimal _weight = 0;
		private decimal _factoryprice = 0;
		private decimal _tradeprice = 0;
		private decimal _stdprice = 0;
		private decimal _netprice = 0;
		private int _state = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		private string _extproperty = string.Empty;
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
        public PDT_Product(int id, string fullname, string shortname, string code, int brand, int classify, int grade, int trafficpackaging, int packaging, int convertfactor, int subunit, decimal weight, decimal factoryprice, decimal tradeprice, decimal stdprice, decimal netprice, int state, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, string extproperty)
		{
			_id               = id;
			_fullname         = fullname;
			_shortname        = shortname;
			_code             = code;
			_brand            = brand;
			_classify         = classify;
			_grade            = grade;
			_trafficpackaging = trafficpackaging;
			_packaging        = packaging;
			_convertfactor    = convertfactor;
			_subunit          = subunit;
			_weight           = weight;
			_factoryprice     = factoryprice;
			_tradeprice       = tradeprice;
			_stdprice         = stdprice;
			_netprice         = netprice;
			_state            = state;
			_approveflag      = approveflag;
			_inserttime       = inserttime;
			_insertstaff      = insertstaff;
			_updatetime       = updatetime;
			_updatestaff      = updatestaff;
			_extproperty      = extproperty;
			
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
		///FullName
		///</summary>
		public string FullName
		{
			get	{ return _fullname; }
			set	{ _fullname = value; }
		}

		///<summary>
		///ShortName
		///</summary>
		public string ShortName
		{
			get	{ return _shortname; }
			set	{ _shortname = value; }
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
		///Brand
		///</summary>
		public int Brand
		{
			get	{ return _brand; }
			set	{ _brand = value; }
		}

		///<summary>
		///Classify
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///Grade
		///</summary>
		public int Grade
		{
			get	{ return _grade; }
			set	{ _grade = value; }
		}

		///<summary>
		///TrafficPackaging
		///</summary>
		public int TrafficPackaging
		{
			get	{ return _trafficpackaging; }
			set	{ _trafficpackaging = value; }
		}

		///<summary>
		///Packaging
		///</summary>
		public int Packaging
		{
			get	{ return _packaging; }
			set	{ _packaging = value; }
		}

		///<summary>
		///ConvertFactor
		///</summary>
		public int ConvertFactor
		{
			get	{ return _convertfactor; }
			set	{ _convertfactor = value; }
		}

		///<summary>
		///SubUnit
		///</summary>
		public int SubUnit
		{
			get	{ return _subunit; }
			set	{ _subunit = value; }
		}

		///<summary>
		///Weight
		///</summary>
		public decimal Weight
		{
			get	{ return _weight; }
			set	{ _weight = value; }
		}

		///<summary>
		///FactoryPrice
		///</summary>
		public decimal FactoryPrice
		{
			get	{ return _factoryprice; }
			set	{ _factoryprice = value; }
		}

		///<summary>
		///TradePrice
		///</summary>
		public decimal TradePrice
		{
			get	{ return _tradeprice; }
			set	{ _tradeprice = value; }
		}

		///<summary>
		///StdPrice
		///</summary>
		public decimal StdPrice
		{
			get	{ return _stdprice; }
			set	{ _stdprice = value; }
		}

		///<summary>
		///NetPrice
		///</summary>
		public decimal NetPrice
		{
			get	{ return _netprice; }
			set	{ _netprice = value; }
		}

		///<summary>
		///State
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///ApproveFlag
		///</summary>
        public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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

		///<summary>
		///UpdateTime
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///UpdateStaff
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
		}

		///<summary>
		///ExtProperty
		///</summary>
		public string ExtProperty
		{
			get	{ return _extproperty; }
			set	{ _extproperty = value; }
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
					case "Brand":
						return _brand.ToString();
					case "Classify":
						return _classify.ToString();
					case "Grade":
						return _grade.ToString();
					case "TrafficPackaging":
						return _trafficpackaging.ToString();
					case "Packaging":
						return _packaging.ToString();
					case "ConvertFactor":
						return _convertfactor.ToString();
					case "SubUnit":
						return _subunit.ToString();
					case "Weight":
						return _weight.ToString();
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
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToShortDateString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "UpdateTime":
						return _updatetime.ToShortDateString();
					case "UpdateStaff":
						return _updatestaff.ToString();
					case "ExtProperty":
						return _extproperty;
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
					case "Brand":
						int.TryParse(value, out _brand);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "Grade":
						int.TryParse(value, out _grade);
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
					case "SubUnit":
						int.TryParse(value, out _subunit);
						break;
					case "Weight":
						decimal.TryParse(value, out _weight);
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
					case "ExtProperty":
						_extproperty = value ;
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
