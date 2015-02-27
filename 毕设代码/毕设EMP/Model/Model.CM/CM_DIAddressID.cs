// ===================================================================
// 文件： CM_DIAddressID.cs
// 项目名称：
// 创建时间：2012/11/30
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_DIAddressID数据实体类
	/// </summary>
	[Serializable]
	public class CM_DIAddressID : IModel
	{
		#region 私有变量定义
		private int _client = 0;
		private int _addressid = 0;
		private string _address = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_DIAddressID()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_DIAddressID(int client, int addressid, string address)
		{
			_client    = client;
			_addressid = addressid;
			_address   = address;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///Client
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///AddressID
		///</summary>
		public int AddressID
		{
			get	{ return _addressid; }
			set	{ _addressid = value; }
		}

		///<summary>
		///Address
		///</summary>
		public string Address
		{
			get	{ return _address; }
			set	{ _address = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "CM_DIAddressID"; }
        }
		#region 索引器访问
		public string this[string FieldName]
        {
            get 
			{
				switch (FieldName)
                {
					case "Client":
						return _client.ToString();
					case "AddressID":
						return _addressid.ToString();
					case "Address":
						return _address;
					default:
						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "AddressID":
						int.TryParse(value, out _addressid);
						break;
					case "Address":
						_address = value ;
						break;

				}
			}
        }
		#endregion
	}
}
