// ===================================================================
// 文件： PM_PromotorInRetailer.cs
// 项目名称：
// 创建时间：2009-4-29
// 作者:	   shh
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
	/// <summary>
	///PM_PromotorInRetailer数据实体类
	/// </summary>
	[Serializable]
	public class PM_PromotorInRetailer : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _promotor = 0;
		private int _client = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_PromotorInRetailer()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_PromotorInRetailer(int id, int promotor, int client)
		{
			_id       = id;
			_promotor = promotor;
			_client   = client;
			
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
		///促销员
		///</summary>
		public int Promotor
		{
			get	{ return _promotor; }
			set	{ _promotor = value; }
		}

		///<summary>
		///终端门店
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PM_PromotorInRetailer"; }
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
					case "Promotor":
						return _promotor.ToString();
					case "Client":
						return _client.ToString();
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
					case "Promotor":
						int.TryParse(value, out _promotor);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;

				}
			}
        }
		#endregion
	}
}
