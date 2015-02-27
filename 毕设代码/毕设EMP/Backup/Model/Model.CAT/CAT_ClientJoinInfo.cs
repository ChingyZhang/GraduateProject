// ===================================================================
// 文件： CAT_ClientJoinInfo.cs
// 项目名称：
// 创建时间：2009/12/27
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CAT
{
	/// <summary>
	///CAT_ClientJoinInfo数据实体类
	/// </summary>
	[Serializable]
	public class CAT_ClientJoinInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _activity = 0;
		private int _invitestate = 0;
		private DateTime _confirmdate = new DateTime(1900,1,1);
		private int _confirmstaff = 0;
		private int _joinstate = 0;
		private string _joinman = string.Empty;
		private DateTime _joindate = new DateTime(1900,1,1);
		private string _feedback = string.Empty;
		private int _questionnairresult = 0;
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
		public CAT_ClientJoinInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CAT_ClientJoinInfo(int id, int client, int activity, int invitestate, DateTime confirmdate, int confirmstaff, int joinstate, string joinman, DateTime joindate, string feedback, int questionnairresult, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id                 = id;
			_client             = client;
			_activity           = activity;
			_invitestate        = invitestate;
			_confirmdate        = confirmdate;
			_confirmstaff       = confirmstaff;
			_joinstate          = joinstate;
			_joinman            = joinman;
			_joindate           = joindate;
			_feedback           = feedback;
			_questionnairresult = questionnairresult;
			_remark             = remark;
			_approveflag        = approveflag;
			_inserttime         = inserttime;
			_insertstaff        = insertstaff;
			_updatetime         = updatetime;
			_updatestaff        = updatestaff;
			
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
		///参与客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///参与活动
		///</summary>
		public int Activity
		{
			get	{ return _activity; }
			set	{ _activity = value; }
		}

		///<summary>
		///邀请状态
		///</summary>
		public int InviteState
		{
			get	{ return _invitestate; }
			set	{ _invitestate = value; }
		}

		///<summary>
		///确认时间
		///</summary>
		public DateTime ConfirmDate
		{
			get	{ return _confirmdate; }
			set	{ _confirmdate = value; }
		}

		///<summary>
		///确认人
		///</summary>
		public int ConfirmStaff
		{
			get	{ return _confirmstaff; }
			set	{ _confirmstaff = value; }
		}

		///<summary>
		///参与状态
		///</summary>
		public int JoinState
		{
			get	{ return _joinstate; }
			set	{ _joinstate = value; }
		}

		///<summary>
		///参与活动人
		///</summary>
		public string JoinMan
		{
			get	{ return _joinman; }
			set	{ _joinman = value; }
		}

		///<summary>
		///参与日期
		///</summary>
		public DateTime JoinDate
		{
			get	{ return _joindate; }
			set	{ _joindate = value; }
		}

		///<summary>
		///活动反馈
		///</summary>
		public string Feedback
		{
			get	{ return _feedback; }
			set	{ _feedback = value; }
		}

		///<summary>
		///问卷结果
		///</summary>
		public int QuestionnairResult
		{
			get	{ return _questionnairresult; }
			set	{ _questionnairresult = value; }
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
		///审批标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///申请时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///申请人
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
            get { return "CAT_ClientJoinInfo"; }
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
					case "Activity":
						return _activity.ToString();
					case "InviteState":
						return _invitestate.ToString();
					case "ConfirmDate":
						return _confirmdate.ToString();
					case "ConfirmStaff":
						return _confirmstaff.ToString();
					case "JoinState":
						return _joinstate.ToString();
					case "JoinMan":
						return _joinman;
					case "JoinDate":
						return _joindate.ToString();
					case "Feedback":
						return _feedback;
					case "QuestionnairResult":
						return _questionnairresult.ToString();
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
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "Activity":
						int.TryParse(value, out _activity);
						break;
					case "InviteState":
						int.TryParse(value, out _invitestate);
						break;
					case "ConfirmDate":
						DateTime.TryParse(value, out _confirmdate);
						break;
					case "ConfirmStaff":
						int.TryParse(value, out _confirmstaff);
						break;
					case "JoinState":
						int.TryParse(value, out _joinstate);
						break;
					case "JoinMan":
						_joinman = value ;
						break;
					case "JoinDate":
						DateTime.TryParse(value, out _joindate);
						break;
					case "Feedback":
						_feedback = value ;
						break;
					case "QuestionnairResult":
						int.TryParse(value, out _questionnairresult);
						break;
					case "Remark":
						_remark = value ;
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
