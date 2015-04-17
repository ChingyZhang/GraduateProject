// ===================================================================
// 文件： ORD_Publish.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_Publish数据实体类
	/// </summary>
	[Serializable]
	public class ORD_Publish : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _supplier = 0;
		private string _topic = string.Empty;
		private DateTime _begintime = new DateTime(1900,1,1);
		private DateTime _endtime = new DateTime(1900,1,1);
		private int _type = 0;
		private int _state = 0;
		private int _facemode = 0;
		private string _description = string.Empty;
		private string _remark = string.Empty;
		private int _approvetask = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private Guid _updateuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_Publish()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_Publish(int id, int supplier, string topic, DateTime begintime, DateTime endtime, int type, int state, int facemode, string description, string remark, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id           = id;
			_supplier     = supplier;
			_topic        = topic;
			_begintime    = begintime;
			_endtime      = endtime;
			_type         = type;
			_state        = state;
			_facemode     = facemode;
			_description  = description;
			_remark       = remark;
			_approvetask  = approvetask;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertuser   = insertuser;
			_updatetime   = updatetime;
			_updateuser   = updateuser;
			
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
		///发布供货商
		///</summary>
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
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
		///开始请购日期
		///</summary>
		public DateTime BeginTime
		{
			get	{ return _begintime; }
			set	{ _begintime = value; }
		}

		///<summary>
		///截止请购日期
		///</summary>
		public DateTime EndTime
		{
			get	{ return _endtime; }
			set	{ _endtime = value; }
		}

		///<summary>
		///类别 1成品 2赠品
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
		///适用客户模式
		///</summary>
		public int FaceMode
		{
			get	{ return _facemode; }
			set	{ _facemode = value; }
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
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///申请任务
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
		public Guid InsertUser
		{
			get	{ return _insertuser; }
			set	{ _insertuser = value; }
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
		public Guid UpdateUser
		{
			get	{ return _updateuser; }
			set	{ _updateuser = value; }
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
            get { return "ORD_Publish"; }
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
					case "Supplier":
						return _supplier.ToString();
					case "Topic":
						return _topic;
					case "BeginTime":
						return _begintime.ToString();
					case "EndTime":
						return _endtime.ToString();
					case "Type":
						return _type.ToString();
					case "State":
						return _state.ToString();
					case "FaceMode":
						return _facemode.ToString();
					case "Description":
						return _description;
					case "Remark":
						return _remark;
					case "ApproveTask":
						return _approvetask.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateUser":
						return _updateuser.ToString();
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
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "Topic":
						_topic = value ;
						break;
					case "BeginTime":
						DateTime.TryParse(value, out _begintime);
						break;
					case "EndTime":
						DateTime.TryParse(value, out _endtime);
						break;
					case "Type":
						int.TryParse(value, out _type);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "FaceMode":
						int.TryParse(value, out _facemode);
						break;
					case "Description":
						_description = value ;
						break;
					case "Remark":
						_remark = value ;
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
					case "InsertUser":
						_insertuser = new Guid(value);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateUser":
                        _updateuser = new Guid(value);
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
