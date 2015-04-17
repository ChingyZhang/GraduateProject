// ===================================================================
// 文件： VST_WorkList.cs
// 项目名称：
// 创建时间：2015-02-01
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_WorkList数据实体类
	/// </summary>
	[Serializable]
	public class VST_WorkList : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _relatestaff = 0;
		private int _route = 0;
		private int _client = 0;
		private int _template = 0;
		private int _workingclassify = 0;
		private string _iscomplete = string.Empty;
		private DateTime _begintime = new DateTime(1900,1,1);
		private DateTime _endtime = new DateTime(1900,1,1);
        private int _planid = 0;
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
		public VST_WorkList()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_WorkList(int id, int relatestaff, int route, int client, int template, int workingclassify, string iscomplete, DateTime begintime, DateTime endtime, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id              = id;
			_relatestaff     = relatestaff;
			_route           = route;
			_client          = client;
			_template        = template;
			_workingclassify = workingclassify;
			_iscomplete      = iscomplete;
			_begintime       = begintime;
			_endtime         = endtime;
			_remark          = remark;
			_approveflag     = approveflag;
			_inserttime      = inserttime;
			_insertstaff     = insertstaff;
			_updatetime      = updatetime;
			_updatestaff     = updatestaff;
			
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
		///关联业代
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///线路ID
		///</summary>
		public int Route
		{
			get	{ return _route; }
			set	{ _route = value; }
		}

		///<summary>
		///门店ID
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///模板ID
		///</summary>
		public int Template
		{
			get	{ return _template; }
			set	{ _template = value; }
		}

		///<summary>
		///工作类别
		///</summary>
		public int WorkingClassify
		{
			get	{ return _workingclassify; }
			set	{ _workingclassify = value; }
		}

		///<summary>
		///是否完成
		///</summary>
		public string IsComplete
		{
			get	{ return _iscomplete; }
			set	{ _iscomplete = value; }
		}

		///<summary>
		///开始工作时间
		///</summary>
		public DateTime BeginTime
		{
			get	{ return _begintime; }
			set	{ _begintime = value; }
		}

		///<summary>
		///结束工作时间
		///</summary>
		public DateTime EndTime
		{
			get	{ return _endtime; }
			set	{ _endtime = value; }
		}

        /// <summary>
        /// 所属拜访计划
        /// </summary>
        public int PlanID
        {
            get { return _planid; }
            set { _planid = value; }
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
            get { return "VST_WorkList"; }
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
					case "RelateStaff":
						return _relatestaff.ToString();
					case "Route":
						return _route.ToString();
					case "Client":
						return _client.ToString();
					case "Template":
						return _template.ToString();
					case "WorkingClassify":
						return _workingclassify.ToString();
					case "IsComplete":
						return _iscomplete;
					case "BeginTime":
						return _begintime.ToString();
					case "EndTime":
						return _endtime.ToString();
                    case "PlanID":
                        return _planid.ToString();
					case "Remark":
						return _remark;
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
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "Route":
						int.TryParse(value, out _route);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Template":
						int.TryParse(value, out _template);
						break;
					case "WorkingClassify":
						int.TryParse(value, out _workingclassify);
						break;
					case "IsComplete":
						_iscomplete = value;
						break;
					case "BeginTime":
						DateTime.TryParse(value, out _begintime);
						break;
					case "EndTime":
						DateTime.TryParse(value, out _endtime);
						break;
                    case "PlanID":
                        int.TryParse(value, out _planid);
                        break;
					case "Remark":
						_remark = value;
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
