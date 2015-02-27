// ===================================================================
// 文件： PM_StdBasePay.cs
// 项目名称：
// 创建时间：2011/10/21
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
	/// <summary>
	///PM_StdBasePay数据实体类
	/// </summary>
	[Serializable]
	public class PM_StdBasePay : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _city = 0;
		private int _classify = 0;
		private decimal _minbasepay = 0;
		private decimal _maxbasepay = 0;
		private decimal _minimumwage = 0;
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
		public PM_StdBasePay()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_StdBasePay(int id, int city, int classify, decimal minbasepay, decimal maxbasepay, decimal minimumwage, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_city         = city;
			_classify     = classify;
			_minbasepay   = minbasepay;
			_maxbasepay   = maxbasepay;
			_minimumwage  = minimumwage;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			_updatetime   = updatetime;
			_updatestaff  = updatestaff;
			
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
		///适用城市
		///</summary>
		public int City
		{
			get	{ return _city; }
			set	{ _city = value; }
		}

		///<summary>
		///类别
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///最低标准底薪
		///</summary>
		public decimal MinBasePay
		{
			get	{ return _minbasepay; }
			set	{ _minbasepay = value; }
		}

		///<summary>
		///最高标准底薪
		///</summary>
		public decimal MaxBasePay
		{
			get	{ return _maxbasepay; }
			set	{ _maxbasepay = value; }
		}

		///<summary>
		///最低保底底薪
		///</summary>
		public decimal MinimumWage
		{
			get	{ return _minimumwage; }
			set	{ _minimumwage = value; }
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
            get { return "PM_StdBasePay"; }
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
					case "City":
						return _city.ToString();
					case "Classify":
						return _classify.ToString();
					case "MinBasePay":
						return _minbasepay.ToString();
					case "MaxBasePay":
						return _maxbasepay.ToString();
					case "MinimumWage":
						return _minimumwage.ToString();
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
					case "City":
						int.TryParse(value, out _city);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "MinBasePay":
						decimal.TryParse(value, out _minbasepay);
						break;
					case "MaxBasePay":
						decimal.TryParse(value, out _maxbasepay);
						break;
					case "MinimumWage":
						decimal.TryParse(value, out _minimumwage);
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
