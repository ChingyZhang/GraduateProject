// ===================================================================
// 文件： ORD_OrderLimitFactor.cs
// 项目名称：
// 创建时间：2010/12/8
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
	/// <summary>
	///ORD_OrderLimitFactor数据实体类
	/// </summary>
	[Serializable]
	public class ORD_OrderLimitFactor : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _accountmonth = 0;
		private int _product = 0;
		private int _upperlimit = 0;
		private int _lowerlimit = 0;
        private int _theoryquantity = 0;
		private decimal _factor = 0;
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
		public ORD_OrderLimitFactor()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_OrderLimitFactor(int id, int client, int accountmonth, int product, int upperlimit, int lowerlimit,int theoryquantity,decimal factor, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_client       = client;
			_accountmonth = accountmonth;
			_product      = product;
			_upperlimit   = upperlimit;
			_lowerlimit   = lowerlimit;
            _theoryquantity = theoryquantity;
			_factor       = factor;
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
		///客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///AccountMonth
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
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
		///上限
		///</summary>
		public int UpperLimit
		{
			get	{ return _upperlimit; }
			set	{ _upperlimit = value; }
		}

		///<summary>
		///下限
		///</summary>
		public int LowerLimit
		{
			get	{ return _lowerlimit; }
			set	{ _lowerlimit = value; }
		}
        /// <summary>
        /// 理论订货量
        /// </summary>
        public int TheoryQuantity
        {
            get { return _theoryquantity; }
            set { _theoryquantity = value; }
        }

		///<summary>
		///系数
		///</summary>
		public decimal Factor
		{
			get	{ return _factor; }
			set	{ _factor = value; }
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
            get { return "ORD_OrderLimitFactor"; }
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
					case "AccountMonth":
						return _accountmonth.ToString();
					case "Product":
						return _product.ToString();
					case "UpperLimit":
						return _upperlimit.ToString();
					case "LowerLimit":
						return _lowerlimit.ToString();
                    case "TheoryQuantity":
                        return _theoryquantity.ToString();
					case "Factor":
						return _factor.ToString();
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
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "UpperLimit":
						int.TryParse(value, out _upperlimit);
						break;
					case "LowerLimit":
						int.TryParse(value, out _lowerlimit);
						break;
                    case "TheoryQuantity":
                        int.TryParse(value, out _theoryquantity);
                        break;
					case "Factor":
						decimal.TryParse(value, out _factor);
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
