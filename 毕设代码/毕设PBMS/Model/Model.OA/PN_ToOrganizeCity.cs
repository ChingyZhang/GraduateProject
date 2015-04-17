// ===================================================================
// 文件： PN_ToOrganizeCity.cs
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
	///PN_ToOrganizeCity数据实体类
	/// </summary>
	[Serializable]
	public class PN_ToOrganizeCity : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _noticeid = 0;
		private int _organizecity = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_ToOrganizeCity()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PN_ToOrganizeCity(int id, int noticeid, int organizecity)
		{
			_id           = id;
			_noticeid     = noticeid;
			_organizecity = organizecity;
			
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
		public int NoticeID
		{
			get	{ return _noticeid; }
			set	{ _noticeid = value; }
		}

		///<summary>
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PN_ToOrganizeCity"; }
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
					case "NoticeID":
						return _noticeid.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
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
					case "NoticeID":
						int.TryParse(value, out _noticeid);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;

				}
			}
        }
		#endregion
	}
}
