// ===================================================================
// 文件： PM_StdInsuranceCost.cs
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
	///PM_StdInsuranceCost数据实体类
	/// </summary>
	[Serializable]
	public class PM_StdInsuranceCost : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _insurancemode = 0;
		private decimal _companycost = 0;
		private decimal _staffcost = 0;
		private decimal _servicecost = 0;
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
		public PM_StdInsuranceCost()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_StdInsuranceCost(int id, int insurancemode, decimal companycost, decimal staffcost, decimal servicecost, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_insurancemode = insurancemode;
			_companycost   = companycost;
			_staffcost     = staffcost;
			_servicecost   = servicecost;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			_updatetime    = updatetime;
			_updatestaff   = updatestaff;
			
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
		///社保模式
		///</summary>
		public int InsuranceMode
		{
			get	{ return _insurancemode; }
			set	{ _insurancemode = value; }
		}

		///<summary>
		///公司承担金额
		///</summary>
		public decimal CompanyCost
		{
			get	{ return _companycost; }
			set	{ _companycost = value; }
		}

		///<summary>
		///个人承担金额
		///</summary>
		public decimal StaffCost
		{
			get	{ return _staffcost; }
			set	{ _staffcost = value; }
		}

		///<summary>
		///派遣服务费
		///</summary>
		public decimal ServiceCost
		{
			get	{ return _servicecost; }
			set	{ _servicecost = value; }
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
            get { return "PM_StdInsuranceCost"; }
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
					case "InsuranceMode":
						return _insurancemode.ToString();
					case "CompanyCost":
						return _companycost.ToString();
					case "StaffCost":
						return _staffcost.ToString();
					case "ServiceCost":
						return _servicecost.ToString();
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
					case "InsuranceMode":
						int.TryParse(value, out _insurancemode);
						break;
					case "CompanyCost":
						decimal.TryParse(value, out _companycost);
						break;
					case "StaffCost":
						decimal.TryParse(value, out _staffcost);
						break;
					case "ServiceCost":
						decimal.TryParse(value, out _servicecost);
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
