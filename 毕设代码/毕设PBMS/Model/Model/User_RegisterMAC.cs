// ===================================================================
// 文件： User_RegisterMAC.cs
// 项目名称：
// 创建时间：2011/11/18
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
	///User_RegisterMAC数据实体类
	/// </summary>
	[Serializable]
	public class User_RegisterMAC : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _macaddr = string.Empty;
		private string _username = string.Empty;
		private string _enabled = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public User_RegisterMAC()
		{
		}
		
		///<summary>
		///
		///</summary>
		public User_RegisterMAC(int id, string macaddr, string username, string enabled, int approveflag, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_macaddr      = macaddr;
			_username     = username;
			_enabled      = enabled;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			
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
		///MacAddr
		///</summary>
		public string MacAddr
		{
			get	{ return _macaddr; }
			set	{ _macaddr = value; }
		}

		///<summary>
		///UserName
		///</summary>
		public string UserName
		{
			get	{ return _username; }
			set	{ _username = value; }
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
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
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
            get { return "User_RegisterMAC"; }
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
					case "MacAddr":
						return _macaddr;
					case "UserName":
						return _username;
					case "Enabled":
						return _enabled;
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "MacAddr":
						_macaddr = value ;
						break;
					case "UserName":
						_username = value ;
						break;
					case "Enabled":
						_enabled = value ;
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
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
