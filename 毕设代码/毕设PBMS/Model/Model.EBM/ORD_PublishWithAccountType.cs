// ===================================================================
// 文件： ORD_PublishWithAccountType.cs
// 项目名称：
// 创建时间：2013-12-20
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_PublishWithAccountType数据实体类
	/// </summary>
	[Serializable]
	public class ORD_PublishWithAccountType : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _publishid = 0;
		private int _accounttype = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_PublishWithAccountType()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_PublishWithAccountType(int id, int publishid, int accounttype)
		{
			_id           = id;
			_publishid    = publishid;
			_accounttype  = accounttype;
			
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
		///PublishID
		///</summary>
		public int PublishID
		{
			get	{ return _publishid; }
			set	{ _publishid = value; }
		}

		///<summary>
		///AccountType
		///</summary>
		public int AccountType
		{
			get	{ return _accounttype; }
			set	{ _accounttype = value; }
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
            get { return "ORD_PublishWithAccountType"; }
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
					case "AccountType":
						return _accounttype.ToString();
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
					case "AccountType":
						int.TryParse(value, out _accounttype);
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
