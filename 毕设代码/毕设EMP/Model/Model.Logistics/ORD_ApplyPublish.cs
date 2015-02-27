// ===================================================================
// 文件： ORD_ApplyPublish.cs
// 项目名称：
// 创建时间：2010/7/26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
	/// <summary>
	///ORD_ApplyPublish数据实体类
	/// </summary>
	[Serializable]
	public class ORD_ApplyPublish : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _topic = string.Empty;
		private int _accountmonth = 0;
		private DateTime _begintime = new DateTime(1900,1,1);
		private DateTime _endtime = new DateTime(1900,1,1);
		private int _toorganizecity = 0;
		private int _type = 0;
		private int _state = 0;
		private string _description = string.Empty;
		private int _feetype = 0;
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
		public ORD_ApplyPublish()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_ApplyPublish(int id, string topic, int accountmonth, DateTime begintime, DateTime endtime, int toorganizecity, int type, int state, string description, int feetype, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id             = id;
			_topic          = topic;
			_accountmonth   = accountmonth;
			_begintime      = begintime;
			_endtime        = endtime;
			_toorganizecity = toorganizecity;
			_type           = type;
			_state          = state;
			_description    = description;
			_feetype        = feetype;
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
		///主题
		///</summary>
		public string Topic
		{
			get	{ return _topic; }
			set	{ _topic = value; }
		}

		///<summary>
		///会计月
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///开始请购时间
		///</summary>
		public DateTime BeginTime
		{
			get	{ return _begintime; }
			set	{ _begintime = value; }
		}

		///<summary>
		///截止请购时间
		///</summary>
		public DateTime EndTime
		{
			get	{ return _endtime; }
			set	{ _endtime = value; }
		}

		///<summary>
		///面向管理片区
		///</summary>
		public int ToOrganizeCity
		{
			get	{ return _toorganizecity; }
			set	{ _toorganizecity = value; }
		}

		///<summary>
		///类别 1成品 2促销品
		///</summary>
		public int Type
		{
			get	{ return _type; }
			set	{ _type = value; }
		}

		///<summary>
		///发布状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///描述
		///</summary>
		public string Description
		{
			get	{ return _description; }
			set	{ _description = value; }
		}

		///<summary>
		///费用类型
		///</summary>
		public int FeeType
		{
			get	{ return _feetype; }
			set	{ _feetype = value; }
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
            get { return "ORD_ApplyPublish"; }
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
					case "Topic":
						return _topic;
					case "AccountMonth":
						return _accountmonth.ToString();
					case "BeginTime":
						return _begintime.ToString();
					case "EndTime":
						return _endtime.ToString();
					case "ToOrganizeCity":
						return _toorganizecity.ToString();
					case "Type":
						return _type.ToString();
					case "State":
						return _state.ToString();
					case "Description":
						return _description;
					case "FeeType":
						return _feetype.ToString();
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
					case "Topic":
						_topic = value ;
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "BeginTime":
						DateTime.TryParse(value, out _begintime);
						break;
					case "EndTime":
						DateTime.TryParse(value, out _endtime);
						break;
					case "ToOrganizeCity":
						int.TryParse(value, out _toorganizecity);
						break;
					case "Type":
						int.TryParse(value, out _type);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "Description":
						_description = value ;
						break;
					case "FeeType":
						int.TryParse(value, out _feetype);
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
