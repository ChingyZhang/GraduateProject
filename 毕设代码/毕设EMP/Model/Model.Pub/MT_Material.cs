// ===================================================================
// 文件： MaterialModel.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   yangwei
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///MT_Material数据实体类
	/// </summary>
	[Serializable]
	public class MT_Material : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _classify = 0;
		private string _code = string.Empty;
		private int _trafficpackaging = 0;
		private int _packaging = 0;
		private int _convertfactor = 0;
		private decimal _weight = 0;
		private decimal _price = 0;
		private int _state = 0;
		private int _approveflag = 0;
		private DateTime _inputtime = new DateTime(1900,1,1);
		private int _inputstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
        private string _extproperty = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public MT_Material()
		{
		}
		
		///<summary>
		///
		///</summary>
        public MT_Material(int id, string name, int classify, string code, int trafficpackaging, int packaging, int convertfactor, decimal weight, decimal price, int state, int approveflag, DateTime inputtime, int inputstaff, DateTime updatetime, int updatestaff, string extproperty)
		{
			_id               = id;
			_name             = name;
			_classify         = classify;
			_code             = code;
			_trafficpackaging = trafficpackaging;
			_packaging        = packaging;
			_convertfactor    = convertfactor;
			_weight           = weight;
			_price            = price;
			_state            = state;
			_approveflag      = approveflag;
			_inputtime        = inputtime;
			_inputstaff       = inputstaff;
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
		///Name
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
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
		///Code
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
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
		///Weight
		///</summary>
		public decimal Weight
		{
			get	{ return _weight; }
			set	{ _weight = value; }
		}

		///<summary>
		///Price
		///</summary>
		public decimal Price
		{
			get	{ return _price; }
			set	{ _price = value; }
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
		///InputTime
		///</summary>
		public DateTime InputTime
		{
			get	{ return _inputtime; }
			set	{ _inputtime = value; }
		}

		///<summary>
		///InputStaff
		///</summary>
		public int InputStaff
		{
			get	{ return _inputstaff; }
			set	{ _inputstaff = value; }
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
            get { return _extproperty; }
            set { _extproperty = value; }
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
            get { return "MT_Material"; }
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
					case "Classify":
						return _classify.ToString();
					case "Code":
						return _code;
					case "TrafficPackaging":
						return _trafficpackaging.ToString();
					case "Packaging":
						return _packaging.ToString();
					case "ConvertFactor":
						return _convertfactor.ToString();
					case "Weight":
						return _weight.ToString();
					case "Price":
						return _price.ToString();
					case "State":
						return _state.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InputTime":
						return _inputtime.ToShortDateString();
					case "InputStaff":
						return _inputstaff.ToString();
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
					case "Name":
						_name = value ;
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "Code":
						_code = value ;
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
					case "Weight":
						decimal.TryParse(value, out _weight);
						break;
					case "Price":
						decimal.TryParse(value, out _price);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
						break;
					case "InputTime":
						DateTime.TryParse(value, out _inputtime);
						break;
					case "InputStaff":
						int.TryParse(value, out _inputstaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
						break;
                    case "ExtProperty":
                        _extproperty = value;
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
