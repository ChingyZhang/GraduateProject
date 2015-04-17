// ===================================================================
// 文件： ORD_PublishFaceToClient.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_PublishFaceToClient数据实体类
	/// </summary>
	[Serializable]
	public class ORD_PublishFaceToClient : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _publishid = 0;
		private int _facetoclient = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_PublishFaceToClient()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_PublishFaceToClient(int id, int publishid, int facetoclient)
		{
			_id           = id;
			_publishid    = publishid;
			_facetoclient = facetoclient;
			
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
		///所属发布
		///</summary>
		public int PublishID
		{
			get	{ return _publishid; }
			set	{ _publishid = value; }
		}

		///<summary>
		///面向客户
		///</summary>
		public int FaceToClient
		{
			get	{ return _facetoclient; }
			set	{ _facetoclient = value; }
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
            get { return "ORD_PublishFaceToClient"; }
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
					case "PublishID":
						return _publishid.ToString();
					case "FaceToClient":
						return _facetoclient.ToString();
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
					case "PublishID":
						int.TryParse(value, out _publishid);
						break;
					case "FaceToClient":
						int.TryParse(value, out _facetoclient);
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
