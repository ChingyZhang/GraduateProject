// ===================================================================
// 文件： ADFind_FindCondition.cs
// 项目名称：
// 创建时间：2008-12-23
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///ADFind_FindCondition数据实体类
	/// </summary>
	[Serializable]
	public class ADFind_FindCondition : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private string _conditiontext = string.Empty;
		private string _conditionvalue = string.Empty;
		private string _conditionsql = string.Empty;
		private DateTime _createdate = new DateTime(1900,1,1);
		private int _opstaff = 0;
		private Guid _panel = new Guid();
		private string _ispublic = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ADFind_FindCondition()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ADFind_FindCondition(int id, string name, string conditiontext, string conditionvalue, string conditionsql, DateTime createdate, int opstaff, Guid panel, string ispublic)
		{
			_id             = id;
			_name           = name;
			_conditiontext  = conditiontext;
			_conditionvalue = conditionvalue;
			_conditionsql   = conditionsql;
			_createdate     = createdate;
			_opstaff        = opstaff;
			_panel          = panel;
			_ispublic       = ispublic;
			
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
		///Name
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///ConditionText
		///</summary>
		public string ConditionText
		{
			get	{ return _conditiontext; }
			set	{ _conditiontext = value; }
		}

		///<summary>
		///ConditionValue
		///</summary>
		public string ConditionValue
		{
			get	{ return _conditionvalue; }
			set	{ _conditionvalue = value; }
		}

		///<summary>
		///ConditionSQL
		///</summary>
		public string ConditionSQL
		{
			get	{ return _conditionsql; }
			set	{ _conditionsql = value; }
		}

		///<summary>
		///CreateDate
		///</summary>
		public DateTime CreateDate
		{
			get	{ return _createdate; }
			set	{ _createdate = value; }
		}

		///<summary>
		///OpStaff
		///</summary>
		public int OpStaff
		{
			get	{ return _opstaff; }
			set	{ _opstaff = value; }
		}

		///<summary>
		///Panel
		///</summary>
        public Guid Panel
		{
			get	{ return _panel; }
			set	{ _panel = value; }
		}

		///<summary>
		///IsPublic
		///</summary>
		public string IsPublic
		{
			get	{ return _ispublic; }
			set	{ _ispublic = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "ADFind_FindCondition"; }
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
					case "Name":
						return _name;
					case "ConditionText":
						return _conditiontext;
					case "ConditionValue":
						return _conditionvalue;
					case "ConditionSQL":
						return _conditionsql;
					case "CreateDate":
						return _createdate.ToShortDateString();
					case "OpStaff":
						return _opstaff.ToString();
					case "Panel":
						return _panel.ToString();
					case "IsPublic":
						return _ispublic;
                    default:
                        return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "Name":
						_name = value ;
						break;
					case "ConditionText":
						_conditiontext = value ;
						break;
					case "ConditionValue":
						_conditionvalue = value ;
						break;
					case "ConditionSQL":
						_conditionsql = value ;
						break;
					case "CreateDate":
						DateTime.TryParse(value, out _createdate);
						break;
					case "OpStaff":
						int.TryParse(value, out _opstaff);
						break;
					case "Panel":
                        _panel = new Guid(value);
						break;
					case "IsPublic":
						_ispublic = value ;
						break;

				}
			}
        }
		#endregion
	}
}
