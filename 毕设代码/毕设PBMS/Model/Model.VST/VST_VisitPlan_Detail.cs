// ===================================================================
// 文件： VST_VisitPlan_Detail.cs
// 项目名称：
// 创建时间：2015-03-13
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
	///VST_VisitPlan_Detail数据实体类
	/// </summary>
	[Serializable]
	public class VST_VisitPlan_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _planid = 0;
		private int _client = 0;
		private int _visitsequence = 0;
		private int _visitedflag = 0;
		private DateTime _visitedtime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_VisitPlan_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_VisitPlan_Detail(int id, int planid, int client, int visitsequence, int visitedflag, DateTime visitedtime, string remark, int approveflag, DateTime inserttime, int insertstaff)
		{
			_id            = id;
			_planid        = planid;
			_client        = client;
			_visitsequence = visitsequence;
			_visitedflag   = visitedflag;
			_visitedtime   = visitedtime;
			_remark        = remark;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			
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
		///计划ID
		///</summary>
		public int PlanID
		{
			get	{ return _planid; }
			set	{ _planid = value; }
		}

		///<summary>
		///拜访客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
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
		///是否拜访
		///</summary>
		public int VisitedFlag
		{
			get	{ return _visitedflag; }
			set	{ _visitedflag = value; }
		}

		///<summary>
		///实际拜访时间
		///</summary>
		public DateTime VisitedTime
		{
			get	{ return _visitedtime; }
			set	{ _visitedtime = value; }
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
            get { return "VST_VisitPlan_Detail"; }
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
					case "PlanID":
						return _planid.ToString();
					case "Client":
						return _client.ToString();
					case "VisitSequence":
						return _visitsequence.ToString();
					case "VisitedFlag":
						return _visitedflag.ToString();
					case "VisitedTime":
						return _visitedtime.ToString();
					case "Remark":
						return _remark;
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "PlanID":
						int.TryParse(value, out _planid);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "VisitSequence":
						int.TryParse(value, out _visitsequence);
						break;
					case "VisitedFlag":
						int.TryParse(value, out _visitedflag);
						break;
					case "VisitedTime":
						DateTime.TryParse(value, out _visitedtime);
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
