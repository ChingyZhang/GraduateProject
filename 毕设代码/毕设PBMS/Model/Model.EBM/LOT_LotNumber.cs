// ===================================================================
// 文件： LOT_LotNumber.cs
// 项目名称：
// 创建时间：2012-11-11
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///LOT_LotNumber数据实体类
	/// </summary>
	[Serializable]
	public class LOT_LotNumber : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _lotnumber = string.Empty;
		private int _product = 0;
		private DateTime _productiondate = new DateTime(1900,1,1);
		private int _manufacturer = 0;
		private int _state = 0;
		private string _qualityreportcode = string.Empty;
		private string _remark = string.Empty;
		private int _approvetask = 0;
		private int _approveflag = 0;
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
		public LOT_LotNumber()
		{
		}
		
		///<summary>
		///
		///</summary>
		public LOT_LotNumber(int id, string lotnumber, int product, DateTime productiondate, int manufacturer, int state, string qualityreportcode, string remark, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id                = id;
			_lotnumber         = lotnumber;
			_product           = product;
			_productiondate    = productiondate;
			_manufacturer      = manufacturer;
			_state             = state;
			_qualityreportcode = qualityreportcode;
			_remark            = remark;
			_approvetask       = approvetask;
			_approveflag       = approveflag;
			_inserttime        = inserttime;
			_insertuser        = insertuser;
			_updatetime        = updatetime;
			_updateuser        = updateuser;
			
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
		///批号
		///</summary>
		public string LotNumber
		{
			get	{ return _lotnumber; }
			set	{ _lotnumber = value; }
		}

		///<summary>
		///产品
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///生产日期
		///</summary>
		public DateTime ProductionDate
		{
			get	{ return _productiondate; }
			set	{ _productiondate = value; }
		}

		///<summary>
		///生厂工厂
		///</summary>
		public int Manufacturer
		{
			get	{ return _manufacturer; }
			set	{ _manufacturer = value; }
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
		///质监报告号
		///</summary>
		public string QualityReportCode
		{
			get	{ return _qualityreportcode; }
			set	{ _qualityreportcode = value; }
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
		///申请任务
		///</summary>
		public int ApproveTask
		{
			get	{ return _approvetask; }
			set	{ _approvetask = value; }
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
            get { return "LOT_LotNumber"; }
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
					case "LotNumber":
						return _lotnumber;
					case "Product":
						return _product.ToString();
					case "ProductionDate":
						return _productiondate.ToString();
					case "Manufacturer":
						return _manufacturer.ToString();
					case "State":
						return _state.ToString();
					case "QualityReportCode":
						return _qualityreportcode;
					case "Remark":
						return _remark;
					case "ApproveTask":
						return _approvetask.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
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
					case "LotNumber":
						_lotnumber = value;
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "ProductionDate":
						DateTime.TryParse(value, out _productiondate);
						break;
					case "Manufacturer":
						int.TryParse(value, out _manufacturer);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "QualityReportCode":
						_qualityreportcode = value;
						break;
					case "Remark":
						_remark = value;
						break;
					case "ApproveTask":
						int.TryParse(value, out _approvetask);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
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
