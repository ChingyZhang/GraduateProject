// ===================================================================
// 文件： CM_ClientManufactInfo.cs
// 项目名称：
// 创建时间：2015-03-25
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
	///CM_ClientManufactInfo数据实体类
	/// </summary>
	[Serializable]
	public class CM_ClientManufactInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _manufacturer = 0;
		private string _code = string.Empty;
		private int _erpid = 0;
		private int _channel = 0;
		private int _markettype = 0;
		private int _iskeyaccount = 0;
		private int _vestkeyaccount = 0;
		private int _clientlevel = 0;
		private int _balancemode = 0;
		private int _clientclassfiy = 0;
		private int _organizecity = 0;
		private int _clientmanager = 0;
		private int _state = 0;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private string _geocode = string.Empty;
		private int _visitroute = 0;
		private int _visitsequence = 0;
		private int _visittemplate = 0;
		private int _visitcycle = 0;
		private int _visitday = 0;
		private int _syncstate = 0;
		private string _remark = string.Empty;
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
		public CM_ClientManufactInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_ClientManufactInfo(int id, int client, int manufacturer, string code, int erpid, int channel, int markettype, int iskeyaccount, int vestkeyaccount, int clientlevel, int balancemode, int clientclassfiy, int organizecity, int clientmanager, int state, DateTime begindate, DateTime enddate, string geocode, int visitroute, int visitsequence, int visittemplate, int visitcycle, int visitday, int syncstate, string remark, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id             = id;
			_client         = client;
			_manufacturer   = manufacturer;
			_code           = code;
			_erpid          = erpid;
			_channel        = channel;
			_markettype     = markettype;
			_iskeyaccount   = iskeyaccount;
			_vestkeyaccount = vestkeyaccount;
			_clientlevel    = clientlevel;
			_balancemode    = balancemode;
			_clientclassfiy = clientclassfiy;
			_organizecity   = organizecity;
			_clientmanager  = clientmanager;
			_state          = state;
			_begindate      = begindate;
			_enddate        = enddate;
			_geocode        = geocode;
			_visitroute     = visitroute;
			_visitsequence  = visitsequence;
			_visittemplate  = visittemplate;
			_visitcycle     = visitcycle;
			_visitday       = visitday;
			_syncstate      = syncstate;
			_remark         = remark;
			_approvetask    = approvetask;
			_approveflag    = approveflag;
			_inserttime     = inserttime;
			_insertstaff    = insertstaff;
			_updatetime     = updatetime;
			_updatestaff    = updatestaff;
			
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
		///客户ID
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///生厂商
		///</summary>
		public int Manufacturer
		{
			get	{ return _manufacturer; }
			set	{ _manufacturer = value; }
		}

		///<summary>
		///厂商客户编码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///厂商ERP_ID
		///</summary>
		public int ERPID
		{
			get	{ return _erpid; }
			set	{ _erpid = value; }
		}

		///<summary>
		///渠道
		///</summary>
		public int Channel
		{
			get	{ return _channel; }
			set	{ _channel = value; }
		}

		///<summary>
		///市场类型

		///</summary>
		public int MarketType
		{
			get	{ return _markettype; }
			set	{ _markettype = value; }
		}

		///<summary>
		///是否重点客户
		///</summary>
		public int IsKeyAccount
		{
			get	{ return _iskeyaccount; }
			set	{ _iskeyaccount = value; }
		}

		///<summary>
		///所属重点客户
		///</summary>
		public int VestKeyAccount
		{
			get	{ return _vestkeyaccount; }
			set	{ _vestkeyaccount = value; }
		}

		///<summary>
		///客户等级
		///</summary>
		public int ClientLevel
		{
			get	{ return _clientlevel; }
			set	{ _clientlevel = value; }
		}

		///<summary>
		///结算方式
		///</summary>
		public int BalanceMode
		{
			get	{ return _balancemode; }
			set	{ _balancemode = value; }
		}

		///<summary>
		///客户类别
		///</summary>
		public int ClientClassfiy
		{
			get	{ return _clientclassfiy; }
			set	{ _clientclassfiy = value; }
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
		///厂家客户经理
		///</summary>
		public int ClientManager
		{
			get	{ return _clientmanager; }
			set	{ _clientmanager = value; }
		}

		///<summary>
		///客户合作状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///开始合作日期
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///截止合作日期
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
		}

		///<summary>
		///地理编码
		///</summary>
		public string GeoCode
		{
			get	{ return _geocode; }
			set	{ _geocode = value; }
		}

		///<summary>
		///销售路线
		///</summary>
		public int VisitRoute
		{
			get	{ return _visitroute; }
			set	{ _visitroute = value; }
		}

		///<summary>
		///拜访顺序
		///</summary>
		public int VisitSequence
		{
			get	{ return _visitsequence; }
			set	{ _visitsequence = value; }
		}

		///<summary>
		///拜访模板
		///</summary>
		public int VisitTemplate
		{
			get	{ return _visittemplate; }
			set	{ _visittemplate = value; }
		}

		///<summary>
		///拜访周期
		///</summary>
		public int VisitCycle
		{
			get	{ return _visitcycle; }
			set	{ _visitcycle = value; }
		}

		///<summary>
		///拜访日
		///</summary>
		public int VisitDay
		{
			get	{ return _visitday; }
			set	{ _visitday = value; }
		}

		///<summary>
		///同步状态
		///</summary>
		public int SyncState
		{
			get	{ return _syncstate; }
			set	{ _syncstate = value; }
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
		///审核流程ID
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
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///新增人
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
            get { return "CM_ClientManufactInfo"; }
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
					case "Manufacturer":
						return _manufacturer.ToString();
					case "Code":
						return _code;
					case "ERPID":
						return _erpid.ToString();
					case "Channel":
						return _channel.ToString();
					case "MarketType":
						return _markettype.ToString();
					case "IsKeyAccount":
						return _iskeyaccount.ToString();
					case "VestKeyAccount":
						return _vestkeyaccount.ToString();
					case "ClientLevel":
						return _clientlevel.ToString();
					case "BalanceMode":
						return _balancemode.ToString();
					case "ClientClassfiy":
						return _clientclassfiy.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "ClientManager":
						return _clientmanager.ToString();
					case "State":
						return _state.ToString();
					case "BeginDate":
						return _begindate.ToString();
					case "EndDate":
						return _enddate.ToString();
					case "GeoCode":
						return _geocode;
					case "VisitRoute":
						return _visitroute.ToString();
					case "VisitSequence":
						return _visitsequence.ToString();
					case "VisitTemplate":
						return _visittemplate.ToString();
					case "VisitCycle":
						return _visitcycle.ToString();
					case "VisitDay":
						return _visitday.ToString();
					case "SyncState":
						return _syncstate.ToString();
					case "Remark":
						return _remark;
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
					case "Manufacturer":
						int.TryParse(value, out _manufacturer);
						break;
					case "Code":
						_code = value;
						break;
					case "ERPID":
						int.TryParse(value, out _erpid);
						break;
					case "Channel":
						int.TryParse(value, out _channel);
						break;
					case "MarketType":
						int.TryParse(value, out _markettype);
						break;
					case "IsKeyAccount":
						int.TryParse(value, out _iskeyaccount);
						break;
					case "VestKeyAccount":
						int.TryParse(value, out _vestkeyaccount);
						break;
					case "ClientLevel":
						int.TryParse(value, out _clientlevel);
						break;
					case "BalanceMode":
						int.TryParse(value, out _balancemode);
						break;
					case "ClientClassfiy":
						int.TryParse(value, out _clientclassfiy);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "ClientManager":
						int.TryParse(value, out _clientmanager);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
						break;
					case "GeoCode":
						_geocode = value;
						break;
					case "VisitRoute":
						int.TryParse(value, out _visitroute);
						break;
					case "VisitSequence":
						int.TryParse(value, out _visitsequence);
						break;
					case "VisitTemplate":
						int.TryParse(value, out _visittemplate);
						break;
					case "VisitCycle":
						int.TryParse(value, out _visitcycle);
						break;
					case "VisitDay":
						int.TryParse(value, out _visitday);
						break;
					case "SyncState":
						int.TryParse(value, out _syncstate);
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
