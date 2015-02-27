// ===================================================================
// 文件： PM_PromotorModel.cs
// 项目名称：
// 创建时间：2008-12-30
// 作者:	   yangwei
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
	/// <summary>
	///PM_Promotor数据实体类
	/// </summary>
	[Serializable]
	public class PM_Promotor : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _officialcity = 0;
		private int _organizecity = 0;
		private int _retailer = 0;
		private int _sex = 0;
		private int _jobtitle = 0;
		private string _journeyworker = string.Empty;
		private int _salarygrade = 0;
        private string _mobilenumber = string.Empty;
		private int _dimission = 0;
		private DateTime _beginworkdate = new DateTime(1900,1,1);
		private DateTime _endworkdate = new DateTime(1900,1,1);
		private int _approveflag = 0;
		private DateTime _inputtime = new DateTime(1900,1,1);
		private int _inputstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_Promotor()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_Promotor(int id, string code, string name, int officialcity, int organizecity, int retailer, int sex, int jobtitle, string journeyworker, int salarygrade, int dimission, DateTime beginworkdate, DateTime endworkdate, int approveflag, DateTime inputtime, int inputstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_code          = code;
			_name          = name;
			_officialcity  = officialcity;
			_organizecity  = organizecity;
			_retailer      = retailer;
			_sex           = sex;
			_jobtitle      = jobtitle;
			_journeyworker = journeyworker;
			_salarygrade   = salarygrade;
			_dimission     = dimission;
			_beginworkdate = beginworkdate;
			_endworkdate   = endworkdate;
			_approveflag   = approveflag;
			_inputtime     = inputtime;
			_inputstaff    = inputstaff;
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
		///员工代码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///姓名
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///销售城市
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}

		///<summary>
		///销售区域
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///Retailer
		///</summary>
		public int Retailer
		{
			get	{ return _retailer; }
			set	{ _retailer = value; }
		}

		///<summary>
		///性别
		///</summary>
		public int Sex
		{
			get	{ return _sex; }
			set	{ _sex = value; }
		}

		///<summary>
		///生日
		///</summary>
		public int JobTitle
		{
			get	{ return _jobtitle; }
			set	{ _jobtitle = value; }
		}

		///<summary>
		///教育学历
		///</summary>
		public string JourneyWorker
		{
			get	{ return _journeyworker; }
			set	{ _journeyworker = value; }
		}

		///<summary>
		///薪酬级别
		///</summary>
		public int SalaryGrade
		{
			get	{ return _salarygrade; }
			set	{ _salarygrade = value; }
		}

        /// <summary>
        /// 移动电话号码
        /// </summary>
        public string MobileNumber
        {
            get { return _mobilenumber; }
            set { _mobilenumber = value; }
        }

		///<summary>
		///在职标志
		///</summary>
		public int Dimission
		{
			get	{ return _dimission; }
			set	{ _dimission = value; }
		}

		///<summary>
		///开始工作日期
		///</summary>
		public DateTime BeginWorkDate
		{
			get	{ return _beginworkdate; }
			set	{ _beginworkdate = value; }
		}

		///<summary>
		///结束工作日期
		///</summary>
		public DateTime EndWorkDate
		{
			get	{ return _endworkdate; }
			set	{ _endworkdate = value; }
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
		///录入时间
		///</summary>
		public DateTime InputTime
		{
			get	{ return _inputtime; }
			set	{ _inputtime = value; }
		}

		///<summary>
		///录入人
		///</summary>
		public int InputStaff
		{
			get	{ return _inputstaff; }
			set	{ _inputstaff = value; }
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
            get { return "PM_Promotor"; }
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
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "OfficialCity":
						return _officialcity.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "Retailer":
						return _retailer.ToString();
					case "Sex":
						return _sex.ToString();
					case "JobTitle":
						return _jobtitle.ToString();
					case "JourneyWorker":
						return _journeyworker;
					case "SalaryGrade":
						return _salarygrade.ToString();
                    case "MobileNumber":
                        return _mobilenumber.ToString();
					case "Dimission":
						return _dimission.ToString();
					case "BeginWorkDate":
						return _beginworkdate.ToShortDateString();
					case "EndWorkDate":
						return _endworkdate.ToShortDateString();
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
					case "Code":
						_code = value ;
						break;
					case "Name":
						_name = value ;
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "Retailer":
						int.TryParse(value, out _retailer);
						break;
					case "Sex":
						int.TryParse(value, out _sex);
						break;
					case "JobTitle":
						int.TryParse(value, out _jobtitle);
						break;
					case "JourneyWorker":
						_journeyworker = value ;
						break;
					case "SalaryGrade":
						int.TryParse(value, out _salarygrade);
						break;
                    case "MobileNumber":
                        _mobilenumber = value;
                        break;
					case "Dimission":
						int.TryParse(value, out _dimission);
						break;
					case "BeginWorkDate":
						DateTime.TryParse(value, out _beginworkdate);
						break;
					case "EndWorkDate":
						DateTime.TryParse(value, out _endworkdate);
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
