// ===================================================================
// 文件： CM_Contract.cs
// 项目名称：
// 创建时间：2011/9/7
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
	///CM_Contract数据实体类
	/// </summary>
	[Serializable]
	public class CM_Contract : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _classify = 0;
        private string _contractcode = string.Empty;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private string _signman = string.Empty;
		private DateTime _signdate = new DateTime(1900,1,1);
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
		public CM_Contract()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_Contract(int id, int client, int classify, DateTime begindate, DateTime enddate, string signman, DateTime signdate, int state, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_client       = client;
			_classify     = classify;
			_begindate    = begindate;
			_enddate      = enddate;
			_signman      = signman;
			_signdate     = signdate;
			_state        = state;
			_approvetask  = approvetask;
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
		///合同类别
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}
        ///<summary>
        ///合同编码
        ///</summary>
        public string ContractCode
        {
            get { return _contractcode; }
            set { _contractcode = value; }
        }
		///<summary>
		///开始日期
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///截止日期
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
		}

		///<summary>
		///签定人
		///</summary>
		public string SignMan
		{
			get	{ return _signman; }
			set	{ _signman = value; }
		}

		///<summary>
		///签定日期
		///</summary>
		public DateTime SignDate
		{
			get	{ return _signdate; }
			set	{ _signdate = value; }
		}

		///<summary>
		///合同审批状态
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
            get { return "CM_Contract"; }
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
					case "Classify":
						return _classify.ToString();
                    case "ContractCode":
                        return _contractcode.ToString();
					case "BeginDate":
						return _begindate.ToString();
					case "EndDate":
						return _enddate.ToString();
					case "SignMan":
						return _signman;
					case "SignDate":
						return _signdate.ToString();
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
					case "Classify":
						int.TryParse(value, out _classify);
						break;
                    case "ContractCode":
                        _contractcode = value;
                        break;
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
						break;
					case "SignMan":
						_signman = value ;
						break;
					case "SignDate":
						DateTime.TryParse(value, out _signdate);
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
