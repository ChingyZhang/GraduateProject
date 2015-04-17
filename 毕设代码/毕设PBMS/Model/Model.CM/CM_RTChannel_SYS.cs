// ===================================================================
// 文件： CM_RTChannel_SYS.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_RTChannel_SYS数据实体类
	/// </summary>
	[Serializable]
	public class CM_RTChannel_SYS : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _superid = 0;
		private string _enabled = string.Empty;
		private int _manufacturer = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_RTChannel_SYS()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_RTChannel_SYS(int id, string code, string name, int superid, string enabled, int manufacturer, string remark, DateTime inserttime, Guid insertuser)
		{
			_id           = id;
			_code         = code;
			_name         = name;
			_superid      = superid;
			_enabled      = enabled;
			_manufacturer = manufacturer;
			_remark       = remark;
			_inserttime   = inserttime;
			_insertuser   = insertuser;
			
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
		///Code
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
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
		///SuperID
		///</summary>
		public int SuperID
		{
			get	{ return _superid; }
			set	{ _superid = value; }
		}

		///<summary>
		///Enabled
		///</summary>
		public string Enabled
		{
			get	{ return _enabled; }
			set	{ _enabled = value; }
		}

		///<summary>
		///Manufacturer
		///</summary>
		public int Manufacturer
		{
			get	{ return _manufacturer; }
			set	{ _manufacturer = value; }
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
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///InsertUser
		///</summary>
		public Guid InsertUser
		{
			get	{ return _insertuser; }
			set	{ _insertuser = value; }
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
            get { return "CM_RTChannel_SYS"; }
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
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "SuperID":
						return _superid.ToString();
					case "Enabled":
						return _enabled;
					case "Manufacturer":
						return _manufacturer.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
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
					case "Code":
						_code = value ;
						break;
					case "Name":
						_name = value ;
						break;
					case "SuperID":
						int.TryParse(value, out _superid);
						break;
					case "Enabled":
						_enabled = value ;
						break;
					case "Manufacturer":
						int.TryParse(value, out _manufacturer);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertUser":
                        try
                        {
                            _insertuser = new Guid(value);
                        }
                        catch { }
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
