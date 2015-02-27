// ===================================================================
// 文件： CM_PropertyInTelephone.cs
// 项目名称：
// 创建时间：2012/3/6
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
	///CM_PropertyInTelephone数据实体类
	/// </summary>
	[Serializable]
	public class CM_PropertyInTelephone : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private string _telenumber = string.Empty;
		private decimal _telecost = 0;
		private decimal _netcost = 0;
		private DateTime _installdate = new DateTime(1900,1,1);
		private DateTime _uninstalldate = new DateTime(1900,1,1);
		private string _owner = string.Empty;
		private int _state = 0;
		private int _approvetask = 0;
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
		public CM_PropertyInTelephone()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_PropertyInTelephone(int id, int client, string telenumber, decimal telecost, decimal netcost, DateTime installdate, DateTime uninstalldate, string owner, int state, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_client        = client;
			_telenumber    = telenumber;
			_telecost      = telecost;
			_netcost       = netcost;
			_installdate   = installdate;
			_uninstalldate = uninstalldate;
			_owner         = owner;
			_state         = state;
			_approvetask   = approvetask;
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
		///所属物业
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///电话号码
		///</summary>
		public string TeleNumber
		{
			get	{ return _telenumber; }
			set	{ _telenumber = value; }
		}

		///<summary>
		///电话费
		///</summary>
		public decimal TeleCost
		{
			get	{ return _telecost; }
			set	{ _telecost = value; }
		}

		///<summary>
		///网络费
		///</summary>
		public decimal NetCost
		{
			get	{ return _netcost; }
			set	{ _netcost = value; }
		}

		///<summary>
		///装机日期
		///</summary>
		public DateTime InstallDate
		{
			get	{ return _installdate; }
			set	{ _installdate = value; }
		}

		///<summary>
		///拆机日期
		///</summary>
		public DateTime UninstallDate
		{
			get	{ return _uninstalldate; }
			set	{ _uninstalldate = value; }
		}

		///<summary>
		///户主姓名
		///</summary>
		public string Owner
		{
			get	{ return _owner; }
			set	{ _owner = value; }
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
		///审批工作流
		///</summary>
		public int ApproveTask
		{
			get	{ return _approvetask; }
			set	{ _approvetask = value; }
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
            get { return "CM_PropertyInTelephone"; }
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
					case "TeleNumber":
						return _telenumber;
					case "TeleCost":
						return _telecost.ToString();
					case "NetCost":
						return _netcost.ToString();
					case "InstallDate":
						return _installdate.ToString();
					case "UninstallDate":
						return _uninstalldate.ToString();
					case "Owner":
						return _owner;
					case "State":
						return _state.ToString();
					case "ApproveTask":
						return _approvetask.ToString();
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
					case "TeleNumber":
						_telenumber = value ;
						break;
					case "TeleCost":
						decimal.TryParse(value, out _telecost);
						break;
					case "NetCost":
						decimal.TryParse(value, out _netcost);
						break;
					case "InstallDate":
						DateTime.TryParse(value, out _installdate);
						break;
					case "UninstallDate":
						DateTime.TryParse(value, out _uninstalldate);
						break;
					case "Owner":
						_owner = value ;
						break;
					case "State":
						int.TryParse(value, out _state);
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
