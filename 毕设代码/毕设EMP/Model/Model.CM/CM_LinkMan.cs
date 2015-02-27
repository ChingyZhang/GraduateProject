// ===================================================================
// 文件： CM_LinkMan.cs
// 项目名称：
// 创建时间：2009/2/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_LinkMan数据实体类
	/// </summary>
	[Serializable]
	public class CM_LinkMan : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _clientid = 0;
		private string _name = string.Empty;
		private int _sex = 0;
		private string _mobile = string.Empty;
		private string _officetelenum = string.Empty;
		private string _hometelenum = string.Empty;
		private string _email = string.Empty;
		private string _address = string.Empty;
		private DateTime _birthday = new DateTime(1900,1,1);
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
		public CM_LinkMan()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_LinkMan(int id, int clientid, string name, int sex, string mobile, string officetelenum, string hometelenum, string email, string address, DateTime birthday, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_clientid      = clientid;
			_name          = name;
			_sex           = sex;
			_mobile        = mobile;
			_officetelenum = officetelenum;
			_hometelenum   = hometelenum;
			_email         = email;
			_address       = address;
			_birthday      = birthday;
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
		///关系客户ID
		///</summary>
		public int ClientID
		{
			get	{ return _clientid; }
			set	{ _clientid = value; }
		}

		///<summary>
		///名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
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
		///手机号
		///</summary>
		public string Mobile
		{
			get	{ return _mobile; }
			set	{ _mobile = value; }
		}

		///<summary>
		///办公电话
		///</summary>
		public string OfficeTeleNum
		{
			get	{ return _officetelenum; }
			set	{ _officetelenum = value; }
		}

		///<summary>
		///家庭电话
		///</summary>
		public string HomeTeleNum
		{
			get	{ return _hometelenum; }
			set	{ _hometelenum = value; }
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
		///地址
		///</summary>
		public string Address
		{
			get	{ return _address; }
			set	{ _address = value; }
		}

		///<summary>
		///生日
		///</summary>
		public DateTime Birthday
		{
			get	{ return _birthday; }
			set	{ _birthday = value; }
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
		///录入事件
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
            get { return "CM_LinkMan"; }
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
					case "ClientID":
						return _clientid.ToString();
					case "Name":
						return _name;
					case "Sex":
						return _sex.ToString();
					case "Mobile":
						return _mobile;
					case "OfficeTeleNum":
						return _officetelenum;
					case "HomeTeleNum":
						return _hometelenum;
					case "Email":
						return _email;
					case "Address":
						return _address;
					case "Birthday":
						return _birthday.ToShortDateString();
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
					case "ClientID":
						int.TryParse(value, out _clientid);
						break;
					case "Name":
						_name = value ;
						break;
					case "Sex":
						int.TryParse(value, out _sex);
						break;
					case "Mobile":
						_mobile = value ;
						break;
					case "OfficeTeleNum":
						_officetelenum = value ;
						break;
					case "HomeTeleNum":
						_hometelenum = value ;
						break;
					case "Email":
						_email = value ;
						break;
					case "Address":
						_address = value ;
						break;
					case "Birthday":
						DateTime.TryParse(value, out _birthday);
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
