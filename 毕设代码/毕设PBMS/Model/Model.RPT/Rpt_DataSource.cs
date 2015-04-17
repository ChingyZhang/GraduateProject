// ===================================================================
// 文件： Rpt_DataSource.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.RPT
{
	/// <summary>
	///Rpt_DataSource数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSource : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private string _name = string.Empty;
		private string _providername = string.Empty;
		private string _connectionstring = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSource()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_DataSource(Guid id, string name, string providername, string connectionstring, DateTime inserttime, DateTime updatetime)
		{
			_id               = id;
			_name             = name;
			_providername     = providername;
			_connectionstring = connectionstring;
			_inserttime       = inserttime;
			_updatetime       = updatetime;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
		public Guid ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///数据源名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///数据源类型
		///</summary>
		public string ProviderName
		{
			get	{ return _providername; }
			set	{ _providername = value; }
		}

		///<summary>
		///连接字符串
		///</summary>
		public string ConnectionString
		{
			get	{ return _connectionstring; }
			set	{ _connectionstring = value; }
		}

		///<summary>
		///录入时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
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
            get { return "Rpt_DataSource"; }
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
					case "ProviderName":
						return _providername;
					case "ConnectionString":
						return _connectionstring;
					case "InsertTime":
						return _inserttime.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
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
						_id = new Guid(value);
						break;
					case "Name":
						_name = value ;
						break;
					case "ProviderName":
						_providername = value ;
						break;
					case "ConnectionString":
						_connectionstring = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
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
