// ===================================================================
// 文件： VST_WorkItem_JD.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_WorkItem_JD数据实体类
	/// </summary>
	[Serializable]
	public class VST_WorkItem_JD : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _job = 0;
		private int _jobtype = 0;
		private int _judgemode = 0;
		private double _longitude = 0;
		private double _latitude = 0;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_WorkItem_JD()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_WorkItem_JD(int id, int job, int jobtype, int judgemode, double longitude, double latitude, string remark)
		{
			_id           = id;
			_job          = job;
			_jobtype      = jobtype;
			_judgemode    = judgemode;
			_longitude    = longitude;
			_latitude     = latitude;
			_remark       = remark;
			
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
		///拜访表明细ID
		///</summary>
		public int Job
		{
			get	{ return _job; }
			set	{ _job = value; }
		}

		///<summary>
		///进出店类型
		///</summary>
		public int JobType
		{
			get	{ return _jobtype; }
			set	{ _jobtype = value; }
		}

		///<summary>
		///进出店方式
		///</summary>
		public int JudgeMode
		{
			get	{ return _judgemode; }
			set	{ _judgemode = value; }
		}

		///<summary>
		///经度
		///</summary>
		public double Longitude
		{
			get	{ return _longitude; }
			set	{ _longitude = value; }
		}

		///<summary>
		///纬度
		///</summary>
		public double Latitude
		{
			get	{ return _latitude; }
			set	{ _latitude = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "VST_WorkItem_JD"; }
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
					case "Job":
						return _job.ToString();
					case "JobType":
						return _jobtype.ToString();
					case "JudgeMode":
						return _judgemode.ToString();
					case "Longitude":
						return _longitude.ToString();
					case "Latitude":
						return _latitude.ToString();
					case "Remark":
						return _remark;
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
					case "Job":
						int.TryParse(value, out _job);
						break;
					case "JobType":
						int.TryParse(value, out _jobtype);
						break;
					case "JudgeMode":
						int.TryParse(value, out _judgemode);
						break;
					case "Longitude":
						double.TryParse(value, out _longitude);
						break;
					case "Latitude":
						double.TryParse(value, out _latitude);
						break;
					case "Remark":
						_remark = value ;
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
