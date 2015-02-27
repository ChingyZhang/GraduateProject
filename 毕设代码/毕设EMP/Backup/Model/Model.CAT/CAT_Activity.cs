// ===================================================================
// 文件： CAT_Activity.cs
// 项目名称：
// 创建时间：2009/12/24
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
	///CAT_Activity数据实体类
	/// </summary>
	[Serializable]
	public class CAT_Activity : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _topic = string.Empty;
		private int _organizecity = 0;
		private int _classify = 0;
		private DateTime _planbegindate = new DateTime(1900,1,1);
		private DateTime _planenddate = new DateTime(1900,1,1);
		private int _stageclient = 0;
		private string _address = string.Empty;
		private string _telenum = string.Empty;
		private string _anchorperson = string.Empty;
		private string _prinipal = string.Empty;
		private string _faceto = string.Empty;
		private string _detail = string.Empty;
		private decimal _joinfee = 0;
		private int _state = 0;
		private int _relatequestionnaire = 0;
		private string _remark = string.Empty;
		private int _feeapply = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
        private int _officialcity = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_Activity()
		{
		}
		
		///<summary>
		///
		///</summary>
        public CAT_Activity(int id, string topic, int organizecity, int classify, DateTime planbegindate, DateTime planenddate, int stageclient, string address, string telenum, string anchorperson, string prinipal, string faceto, string detail, decimal joinfee, int state, int relatequestionnaire, string remark, int feeapply, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, int officialcity)
		{
			_id                  = id;
			_topic               = topic;
			_organizecity        = organizecity;
			_classify            = classify;
			_planbegindate       = planbegindate;
			_planenddate         = planenddate;
			_stageclient         = stageclient;
			_address             = address;
			_telenum             = telenum;
			_anchorperson        = anchorperson;
			_prinipal            = prinipal;
			_faceto              = faceto;
			_detail              = detail;
			_joinfee             = joinfee;
			_state               = state;
			_relatequestionnaire = relatequestionnaire;
			_remark              = remark;
			_feeapply            = feeapply;
			_approveflag         = approveflag;
			_inserttime          = inserttime;
			_insertstaff         = insertstaff;
			_updatetime          = updatetime;
			_updatestaff         = updatestaff;
            _officialcity        = officialcity;
			
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
		/// 活动主题
		///</summary>
		public string Topic
		{
			get	{ return _topic; }
			set	{ _topic = value; }
		}

		///<summary>
		///活动举办管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///活动分类
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///计划开始举办时间
		///</summary>
		public DateTime PlanBeginDate
		{
			get	{ return _planbegindate; }
			set	{ _planbegindate = value; }
		}

		///<summary>
		///计划举办截止时间
		///</summary>
		public DateTime PlanEndDate
		{
			get	{ return _planenddate; }
			set	{ _planenddate = value; }
		}

		///<summary>
		///举办客户
		///</summary>
		public int StageClient
		{
			get	{ return _stageclient; }
			set	{ _stageclient = value; }
		}

		///<summary>
		///举办地址
		///</summary>
		public string Address
		{
			get	{ return _address; }
			set	{ _address = value; }
		}
 

		///<summary>
		///活动联系电话
		///</summary>
		public string TeleNum
		{
			get	{ return _telenum; }
			set	{ _telenum = value; }
		}

		///<summary>
		///主持人
		///</summary>
		public string AnchorPerson
		{
			get	{ return _anchorperson; }
			set	{ _anchorperson = value; }
		}

		///<summary>
		///活动负责人
		///</summary>
		public string Prinipal
		{
			get	{ return _prinipal; }
			set	{ _prinipal = value; }
		}

		///<summary>
		///面向群体
		///</summary>
		public string FaceTo
		{
			get	{ return _faceto; }
			set	{ _faceto = value; }
		}

		///<summary>
		///活动详细信息
		///</summary>
		public string Detail
		{
			get	{ return _detail; }
			set	{ _detail = value; }
		}

		///<summary>
		///参与费用
		///</summary>
		public decimal JoinFee
		{
			get	{ return _joinfee; }
			set	{ _joinfee = value; }
		}

		///<summary>
		///活动状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///关联问卷
		///</summary>
		public int RelateQuestionnaire
		{
			get	{ return _relatequestionnaire; }
			set	{ _relatequestionnaire = value; }
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
		///费用申请单
		///</summary>
		public int FeeApply
		{
			get	{ return _feeapply; }
			set	{ _feeapply = value; }
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

        public int Officialcity
        {
            get { return _officialcity; }
            set { _officialcity = value; }
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
            get { return "CAT_Activity"; }
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
					case "OrganizeCity":
						return _organizecity.ToString();
					case "Classify":
						return _classify.ToString();
					case "PlanBeginDate":
						return _planbegindate.ToString();
					case "PlanEndDate":
						return _planenddate.ToString();
					case "StageClient":
						return _stageclient.ToString();
					case "Address":
						return _address;
					case "TeleNum":
						return _telenum;
					case "AnchorPerson":
						return _anchorperson;
					case "Prinipal":
						return _prinipal;
					case "FaceTo":
						return _faceto;
					case "Detail":
						return _detail;
					case "JoinFee":
						return _joinfee.ToString();
					case "State":
						return _state.ToString();
					case "RelateQuestionnaire":
						return _relatequestionnaire.ToString();
					case "Remark":
						return _remark;
					case "FeeApply":
						return _feeapply.ToString();
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
                    case "Officialcity":
                        return _officialcity.ToString();
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "PlanBeginDate":
						DateTime.TryParse(value, out _planbegindate);
						break;
					case "PlanEndDate":
						DateTime.TryParse(value, out _planenddate);
						break;
					case "StageClient":
						int.TryParse(value, out _stageclient);
						break;
					case "Address":
						_address = value ;
						break;
					case "TeleNum":
						_telenum = value ;
						break;
					case "AnchorPerson":
						_anchorperson = value ;
						break;
					case "Prinipal":
						_prinipal = value ;
						break;
					case "FaceTo":
						_faceto = value ;
						break;
					case "Detail":
						_detail = value ;
						break;
					case "JoinFee":
						decimal.TryParse(value, out _joinfee);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "RelateQuestionnaire":
						int.TryParse(value, out _relatequestionnaire);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "FeeApply":
						int.TryParse(value, out _feeapply);
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
                    case "Officialcity":
                        int.TryParse(value, out _officialcity);
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
