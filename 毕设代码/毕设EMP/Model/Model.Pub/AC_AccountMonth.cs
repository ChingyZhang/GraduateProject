// ===================================================================
// 文件： AC_AccountMonth.cs
// 项目名称：
// 创建时间：2008-12-22
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
	///AC_AccountMonth数据实体类
	/// </summary>
	[Serializable]
	public class AC_AccountMonth : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private DateTime _begindate = new DateTime(1900,1,1);
		private DateTime _enddate = new DateTime(1900,1,1);
		private int _year = 0;
		private int _month = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_AccountMonth()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_AccountMonth(int id, string name, DateTime begindate, DateTime enddate, int year, int month)
		{
			_id        = id;
			_name      = name;
			_begindate = begindate;
			_enddate   = enddate;
			_year      = year;
			_month     = month;
			
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
		///BeginDate
		///</summary>
		public DateTime BeginDate
		{
			get	{ return _begindate; }
			set	{ _begindate = value; }
		}

		///<summary>
		///EndDate
		///</summary>
		public DateTime EndDate
		{
			get	{ return _enddate; }
			set	{ _enddate = value; }
		}

		///<summary>
		///Year
		///</summary>
		public int Year
		{
			get	{ return _year; }
			set	{ _year = value; }
		}

		///<summary>
		///Month
		///</summary>
		public int Month
		{
			get	{ return _month; }
			set	{ _month = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "AC_AccountMonth"; }
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
					case "BeginDate":
						return _begindate.ToShortDateString();
					case "EndDate":
						return _enddate.ToShortDateString();
					case "Year":
						return _year.ToString();
					case "Month":
						return _month.ToString();
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
					case "BeginDate":
						DateTime.TryParse(value, out _begindate);
						break;
					case "EndDate":
						DateTime.TryParse(value, out _enddate);
						break;
					case "Year":
						int.TryParse(value, out _year);
						break;
					case "Month":
						int.TryParse(value, out _month);
						break;

				}
			}
        }
		#endregion
	}
}
