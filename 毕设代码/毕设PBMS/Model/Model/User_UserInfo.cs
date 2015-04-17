// ===================================================================
// 文件： User_UserInfo.cs
// 项目名称：
// 创建时间：2013-08-31
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///User_UserInfo数据实体类
	/// </summary>
	[Serializable]
	public class User_UserInfo : IModel
	{
		#region 私有变量定义
		private Guid _userid = Guid.Empty;
		private int _accounttype = 0;
		private int _relatestaff = 0;
		private int _relateclient = 0;
		private int _relateclientlinkman = 0;
		private int _relatepromotor = 0;
		private int _relatecustomer = 0;
		private string _realname = string.Empty;
		private string _mobile = string.Empty;
		private string _email = string.Empty;
		private DateTime _createtime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public User_UserInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public User_UserInfo(Guid userid, int accounttype, int relatestaff, int relateclient, int relateclientlinkman, int relatepromotor, int relatecustomer, string realname, string mobile, string email, DateTime createtime)
		{
			_userid              = userid;
			_accounttype         = accounttype;
			_relatestaff         = relatestaff;
			_relateclient        = relateclient;
			_relateclientlinkman = relateclientlinkman;
			_relatepromotor      = relatepromotor;
			_relatecustomer      = relatecustomer;
			_realname            = realname;
			_mobile              = mobile;
			_email               = email;
			_createtime          = createtime;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///用户ID
		///</summary>
		public Guid UserId
		{
			get	{ return _userid; }
			set	{ _userid = value; }
		}

		///<summary>
		///账户类型
		///</summary>
		public int AccountType
		{
			get	{ return _accounttype; }
			set	{ _accounttype = value; }
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
		///关联商业客户
		///</summary>
		public int RelateClient
		{
			get	{ return _relateclient; }
			set	{ _relateclient = value; }
		}

		///<summary>
		///关联客户联系人(医生)
		///</summary>
		public int RelateClientLinkMan
		{
			get	{ return _relateclientlinkman; }
			set	{ _relateclientlinkman = value; }
		}

		///<summary>
		///关联导购
		///</summary>
		public int RelatePromotor
		{
			get	{ return _relatepromotor; }
			set	{ _relatepromotor = value; }
		}

		///<summary>
		///关联顾客
		///</summary>
		public int RelateCustomer
		{
			get	{ return _relatecustomer; }
			set	{ _relatecustomer = value; }
		}

		///<summary>
		///用户姓名
		///</summary>
		public string RealName
		{
			get	{ return _realname; }
			set	{ _realname = value; }
		}

		///<summary>
		///手机号码
		///</summary>
		public string Mobile
		{
			get	{ return _mobile; }
			set	{ _mobile = value; }
		}

		///<summary>
		///Email
		///</summary>
		public string Email
		{
			get	{ return _email; }
			set	{ _email = value; }
		}

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateTime
		{
			get	{ return _createtime; }
			set	{ _createtime = value; }
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
            get { return "User_UserInfo"; }
        }
		#region 索引器访问
		public string this[string FieldName]
        {
            get 
			{
				switch (FieldName)
                {
					case "UserId":
						return _userid.ToString();
					case "AccountType":
						return _accounttype.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "RelateClient":
						return _relateclient.ToString();
					case "RelateClientLinkMan":
						return _relateclientlinkman.ToString();
					case "RelatePromotor":
						return _relatepromotor.ToString();
					case "RelateCustomer":
						return _relatecustomer.ToString();
					case "RealName":
						return _realname;
					case "Mobile":
						return _mobile;
					case "Email":
						return _email;
					case "CreateTime":
						return _createtime.ToString();
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
					case "UserId":
						_userid = new Guid(value);
						break;
					case "AccountType":
						int.TryParse(value, out _accounttype);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "RelateClient":
						int.TryParse(value, out _relateclient);
						break;
					case "RelateClientLinkMan":
						int.TryParse(value, out _relateclientlinkman);
						break;
					case "RelatePromotor":
						int.TryParse(value, out _relatepromotor);
						break;
					case "RelateCustomer":
						int.TryParse(value, out _relatecustomer);
						break;
					case "RealName":
						_realname = value;
						break;
					case "Mobile":
						_mobile = value;
						break;
					case "Email":
						_email = value;
						break;
					case "CreateTime":
						DateTime.TryParse(value, out _createtime);
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
