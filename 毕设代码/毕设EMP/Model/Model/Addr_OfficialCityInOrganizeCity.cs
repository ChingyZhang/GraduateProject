// ===================================================================
// 文件： Addr_OfficialCityInOrganizeCity.cs
// 项目名称：
// 创建时间：2010/2/26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///Addr_OfficialCityInOrganizeCity数据实体类
	/// </summary>
	[Serializable]
	public class Addr_OfficialCityInOrganizeCity : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _organizecity = 0;
		private int _officialcity = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Addr_OfficialCityInOrganizeCity()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Addr_OfficialCityInOrganizeCity(int id, int organizecity, int officialcity)
		{
			_id           = id;
			_organizecity = organizecity;
			_officialcity = officialcity;
			
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
		///OrganizeCity
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///OfficialCity
		///</summary>
		public int OfficialCity
		{
			get	{ return _officialcity; }
			set	{ _officialcity = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "Addr_OfficialCityInOrganizeCity"; }
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
					case "OrganizeCity":
						return _organizecity.ToString();
					case "OfficialCity":
						return _officialcity.ToString();
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "OfficialCity":
						int.TryParse(value, out _officialcity);
						break;

				}
			}
        }
		#endregion
	}
}
