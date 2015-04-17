// ===================================================================
// 文件： JN_WorkingPlanDetail.cs
// 项目名称：
// 创建时间：2010/6/19
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///JN_WorkingPlanDetail数据实体类
	/// </summary>
	[Serializable]
	public class JN_WorkingPlanDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _workingplan = 0;
		private DateTime _begintime = new DateTime(1900,1,1);
		private DateTime _endtime = new DateTime(1900,1,1);
		private int _workingclassify = 0;
		private int _relateclient = 0;
		private int _officialcity = 0;
		private int _relatestaff = 0;
		private string _description = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public JN_WorkingPlanDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public JN_WorkingPlanDetail(int id, int workingplan, DateTime begintime, DateTime endtime, int workingclassify, int relateclient, int officialcity, int relatestaff, string description)
		{
			_id              = id;
			_workingplan     = workingplan;
			_begintime       = begintime;
			_endtime         = endtime;
			_workingclassify = workingclassify;
			_relateclient    = relateclient;
			_officialcity    = officialcity;
			_relatestaff     = relatestaff;
			_description     = description;
			
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
		public int WorkingPlan
		{
			get	{ return _workingplan; }
			set	{ _workingplan = value; }
		}

		///<summary>
		///开始时间
		///</summary>
		public DateTime BeginTime
		{
			get	{ return _begintime; }
			set	{ _begintime = value; }
		}

		///<summary>
		///截止时间
		///</summary>
		public DateTime EndTime
		{
			get	{ return _endtime; }
			set	{ _endtime = value; }
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
		///相关客户
		///</summary>
		public int RelateClient
		{
			get	{ return _relateclient; }
			set	{ _relateclient = value; }
		}

		///<summary>
		///城市
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}

		///<summary>
		///协同员工
		///</summary>
		public int RelateStaff
		{
			get	{ return _relatestaff; }
			set	{ _relatestaff = value; }
		}

		///<summary>
		///工作描述
		///</summary>
		public string Description
		{
			get	{ return _description; }
			set	{ _description = value; }
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
            get { return "JN_WorkingPlanDetail"; }
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
					case "WorkingPlan":
						return _workingplan.ToString();
					case "BeginTime":
						return _begintime.ToShortDateString();
					case "EndTime":
						return _endtime.ToShortDateString();
					case "WorkingClassify":
						return _workingclassify.ToString();
					case "RelateClient":
						return _relateclient.ToString();
					case "OfficialCity":
						return _officialcity.ToString();
					case "RelateStaff":
						return _relatestaff.ToString();
					case "Description":
						return _description;
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
					case "WorkingPlan":
						int.TryParse(value, out _workingplan);
						break;
					case "BeginTime":
						DateTime.TryParse(value, out _begintime);
						break;
					case "EndTime":
						DateTime.TryParse(value, out _endtime);
						break;
					case "WorkingClassify":
						int.TryParse(value, out _workingclassify);
						break;
					case "RelateClient":
						int.TryParse(value, out _relateclient);
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;
					case "RelateStaff":
						int.TryParse(value, out _relatestaff);
						break;
					case "Description":
						_description = value ;
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
