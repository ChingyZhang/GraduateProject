// ===================================================================
// 文件： CM_Client.cs
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
	///CM_Client数据实体类
	/// </summary>
	[Serializable]
	public class CM_Client : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _fullname = string.Empty;
		private string _shortname = string.Empty;
		private int _organizecity = 0;
		private int _officalcity = 0;
		private string _address = string.Empty;
		private string _telenum = string.Empty;
		private string _fax = string.Empty;
		private string _email = string.Empty;
		private string _url = string.Empty;
		private string _postcode = string.Empty;
		private int _chieflinkman = 0;
		private int _activeflag = 0;
		private DateTime _opentime = new DateTime(1900,1,1);
		private DateTime _closetime = new DateTime(1900,1,1);
		private int _clienttype = 0;
		private int _supplier = 0;
		private int _clientmanager = 0;
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
		public CM_Client()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_Client(int id, string code, string fullname, string shortname, int organizecity, int officalcity, string address, string telenum, string fax, string email, string url, string postcode, int chieflinkman, int activeflag, DateTime opentime, DateTime closetime, int clienttype, int supplier, int clientmanager, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_code          = code;
			_fullname      = fullname;
			_shortname     = shortname;
			_organizecity  = organizecity;
			_officalcity   = officalcity;
			_address       = address;
			_telenum       = telenum;
			_fax           = fax;
			_email         = email;
			_url           = url;
			_postcode      = postcode;
			_chieflinkman  = chieflinkman;
			_activeflag    = activeflag;
			_opentime      = opentime;
			_closetime     = closetime;
			_clienttype    = clienttype;
			_supplier      = supplier;
			_clientmanager = clientmanager;
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
		///编号
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///客户全程
		///</summary>
		public string FullName
		{
			get	{ return _fullname; }
			set	{ _fullname = value; }
		}

		///<summary>
		///简称
		///</summary>
		public string ShortName
		{
			get	{ return _shortname; }
			set	{ _shortname = value; }
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
		///行政城市
		///</summary>
		public int OfficalCity
		{
			get	{ return _officalcity; }
			set	{ _officalcity = value; }
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
		///电话号码
		///</summary>
		public string TeleNum
		{
			get	{ return _telenum; }
			set	{ _telenum = value; }
		}

		///<summary>
		///传真
		///</summary>
		public string Fax
		{
			get	{ return _fax; }
			set	{ _fax = value; }
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
		///企业网址
		///</summary>
		public string URL
		{
			get	{ return _url; }
			set	{ _url = value; }
		}

		///<summary>
		///邮编
		///</summary>
		public string PostCode
		{
			get	{ return _postcode; }
			set	{ _postcode = value; }
		}

		///<summary>
		///首要联系人
		///</summary>
		public int ChiefLinkMan
		{
			get	{ return _chieflinkman; }
			set	{ _chieflinkman = value; }
		}

		///<summary>
		///活跃标志
		///</summary>
		public int ActiveFlag
		{
			get	{ return _activeflag; }
			set	{ _activeflag = value; }
		}

		///<summary>
		///开始合作时间
		///</summary>
		public DateTime OpenTime
		{
			get	{ return _opentime; }
			set	{ _opentime = value; }
		}

		///<summary>
		///停止合作时间
		///</summary>
		public DateTime CloseTime
		{
			get	{ return _closetime; }
			set	{ _closetime = value; }
		}

		///<summary>
		///客户类型
		///</summary>
		public int ClientType
		{
			get	{ return _clienttype; }
			set	{ _clienttype = value; }
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
		///客户经理
		///</summary>
		public int ClientManager
		{
			get	{ return _clientmanager; }
			set	{ _clientmanager = value; }
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
            get { return "CM_Client"; }
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
					case "FullName":
						return _fullname;
					case "ShortName":
						return _shortname;
					case "OrganizeCity":
						return _organizecity.ToString();
					case "OfficalCity":
						return _officalcity.ToString();
					case "Address":
						return _address;
					case "TeleNum":
						return _telenum;
					case "Fax":
						return _fax;
					case "Email":
						return _email;
					case "URL":
						return _url;
					case "PostCode":
						return _postcode;
					case "ChiefLinkMan":
						return _chieflinkman.ToString();
					case "ActiveFlag":
						return _activeflag.ToString();
					case "OpenTime":
						return _opentime.ToShortDateString();
					case "CloseTime":
						return _closetime.ToShortDateString();
					case "ClientType":
						return _clienttype.ToString();
					case "Supplier":
						return _supplier.ToString();
					case "ClientManager":
						return _clientmanager.ToString();
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
					case "Code":
						_code = value ;
						break;
					case "FullName":
						_fullname = value ;
						break;
					case "ShortName":
						_shortname = value ;
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "OfficalCity":
						int.TryParse(value, out _officalcity);
						break;
					case "Address":
						_address = value ;
						break;
					case "TeleNum":
						_telenum = value ;
						break;
					case "Fax":
						_fax = value ;
						break;
					case "Email":
						_email = value ;
						break;
					case "URL":
						_url = value ;
						break;
					case "PostCode":
						_postcode = value ;
						break;
					case "ChiefLinkMan":
						int.TryParse(value, out _chieflinkman);
						break;
					case "ActiveFlag":
						int.TryParse(value, out _activeflag);
						break;
					case "OpenTime":
						DateTime.TryParse(value, out _opentime);
						break;
					case "CloseTime":
						DateTime.TryParse(value, out _closetime);
						break;
					case "ClientType":
						int.TryParse(value, out _clienttype);
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "ClientManager":
						int.TryParse(value, out _clientmanager);
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
