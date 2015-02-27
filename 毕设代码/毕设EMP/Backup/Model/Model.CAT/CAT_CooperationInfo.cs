// ===================================================================
// 文件： CAT_CooperationInfo.cs
// 项目名称：
// 创建时间：2011/1/20
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CAT
{
	/// <summary>
	///CAT_CooperationInfo数据实体类
	/// </summary>
	[Serializable]
	public class CAT_CooperationInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _activity = 0;
		private int _cooperation = 0;
		private string _remark = string.Empty;
		private string _people = string.Empty;
		private string _telenum = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_CooperationInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CAT_CooperationInfo(int id, int activity, int cooperation, string remark, string people, string telenum)
		{
			_id          = id;
			_activity    = activity;
			_cooperation = cooperation;
			_remark      = remark;
			_people      = people;
			_telenum     = telenum;
			
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
		///Activity
		///</summary>
		public int Activity
		{
			get	{ return _activity; }
			set	{ _activity = value; }
		}

		///<summary>
		///Cooperation
		///</summary>
		public int Cooperation
		{
			get	{ return _cooperation; }
			set	{ _cooperation = value; }
		}

		///<summary>
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///People
		///</summary>
		public string People
		{
			get	{ return _people; }
			set	{ _people = value; }
		}

		///<summary>
		///TeleNum
		///</summary>
		public string TeleNum
		{
			get	{ return _telenum; }
			set	{ _telenum = value; }
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
            get { return "CAT_CooperationInfo"; }
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
					case "Activity":
						return _activity.ToString();
					case "Cooperation":
						return _cooperation.ToString();
					case "Remark":
						return _remark;
					case "People":
						return _people;
					case "TeleNum":
						return _telenum;
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
					case "Activity":
						int.TryParse(value, out _activity);
						break;
					case "Cooperation":
						int.TryParse(value, out _cooperation);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "People":
						_people = value ;
						break;
					case "TeleNum":
						_telenum = value ;
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
