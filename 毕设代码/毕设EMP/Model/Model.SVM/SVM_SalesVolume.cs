// ===================================================================
// 文件： SVM_SalesVolume_Head.cs
// 项目名称：
// 创建时间：2009-2-19
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_SalesVolume数据实体类
	/// </summary>
	[Serializable]
	public class SVM_SalesVolume : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _type = 0;
		private int _organizecity = 0;
		private DateTime _salesdate = new DateTime(1900,1,1);
		private int _accountmonth = 0;
		private int _client = 0;
        private int _flag = 0; 
        private int _supplier = 0;
		private int _salesstaff = 0;
		private int _promotor = 0;
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
		public SVM_SalesVolume()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_SalesVolume(int id, string sheetcode, int type, int organizecity, DateTime salesdate, int accountmonth, int client, int supplier, int salesstaff, int promotor, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_sheetcode    = sheetcode;
			_type         = type;
			_organizecity = organizecity;
			_salesdate    = salesdate;
			_accountmonth = accountmonth;
			_client       = client;
			_supplier     = supplier;
			_salesstaff   = salesstaff;
			_promotor     = promotor;
			_remark       = remark;
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
		///销售单号
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
		}

		///<summary>
		///销售类型
		///</summary>
		public int Type
		{
			get	{ return _type; }
			set	{ _type = value; }
		}

		///<summary>
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///销售日期
		///</summary>
		public DateTime SalesDate
		{
			get	{ return _salesdate; }
			set	{ _salesdate = value; }
		}

		///<summary>
		///结算日
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///买入客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///供货商
		///</summary>
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
		}

        ///<summary>
        ///进退货标志
        ///</summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

		///<summary>
		///销售人员
		///</summary>
		public int SalesStaff
		{
			get	{ return _salesstaff; }
			set	{ _salesstaff = value; }
		}

		///<summary>
		///促销员
		///</summary>
		public int Promotor
		{
			get	{ return _promotor; }
			set	{ _promotor = value; }
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
		///审批标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///录入日期
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
            get { return "SVM_SalesVolume"; }
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
					case "SheetCode":
						return _sheetcode;
					case "Type":
						return _type.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "SalesDate":
						return _salesdate.ToShortDateString();
					case "AccountMonth":
						return _accountmonth.ToString();
					case "Client":
						return _client.ToString();
					case "Supplier":
						return _supplier.ToString();
                    case "Flag":
                        return _flag.ToString();
					case "SalesStaff":
						return _salesstaff.ToString();
					case "Promotor":
						return _promotor.ToString();
					case "Remark":
						return _remark;
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
					default:
						if (_extpropertys==null)
							return "";
						else
							return _extpropertys[FieldName];						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "SheetCode":
						_sheetcode = value ;
						break;
					case "Type":
						int.TryParse(value, out _type);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "SalesDate":
						DateTime.TryParse(value, out _salesdate);
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
                    case "Flag":
                        int.TryParse(value, out _flag);
                        break;
					case "SalesStaff":
						int.TryParse(value, out _salesstaff);
						break;
					case "Promotor":
						int.TryParse(value, out _promotor);
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
