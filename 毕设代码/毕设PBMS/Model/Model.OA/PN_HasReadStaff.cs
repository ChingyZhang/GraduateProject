// ===================================================================
// 文件： PN_HasReadStaff.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
	/// <summary>
	///PN_HasReadStaff数据实体类
	/// </summary>
	[Serializable]
	public class PN_HasReadStaff : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _notice = 0;
		private int _staff = 0;
		private DateTime _readtime = new DateTime(1900,1,1);
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_HasReadStaff()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PN_HasReadStaff(int id, int notice, int staff, DateTime readtime)
		{
			_id       = id;
			_notice   = notice;
			_staff    = staff;
			_readtime = readtime;
			
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
		///公告ID
		///</summary>
		public int Notice
		{
			get	{ return _notice; }
			set	{ _notice = value; }
		}

		///<summary>
		///已阅读员工
		///</summary>
		public int Staff
		{
			get	{ return _staff; }
			set	{ _staff = value; }
		}

		///<summary>
		///阅读时间
		///</summary>
		public DateTime ReadTime
		{
			get	{ return _readtime; }
			set	{ _readtime = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PN_HasReadStaff"; }
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
					case "Notice":
						return _notice.ToString();
					case "Staff":
						return _staff.ToString();
					case "ReadTime":
						return _readtime.ToString();
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
					case "Notice":
						int.TryParse(value, out _notice);
						break;
					case "Staff":
						int.TryParse(value, out _staff);
						break;
					case "ReadTime":
						DateTime.TryParse(value, out _readtime);
						break;

				}
			}
        }
		#endregion
	}
}
