// ===================================================================
// 文件： CM_DIMembership.cs
// 项目名称：
// 创建时间：2013/9/24
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
	///CM_DIMembership数据实体类
	/// </summary>
	[Serializable]
	public class CM_DIMembership : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _username = string.Empty;
		private string _password = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private int _islockedout = 0;
		private int _isapproved = 0;
		private int _failedpasswordattemptcount = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_DIMembership()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_DIMembership(int id, string username, string password, DateTime inserttime, int insertstaff, int islockedout, int isapproved, int failedpasswordattemptcount)
		{
			_id                         = id;
			_username                   = username;
			_password                   = password;
			_inserttime                 = inserttime;
			_insertstaff                = insertstaff;
			_islockedout                = islockedout;
			_isapproved                 = isapproved;
			_failedpasswordattemptcount = failedpasswordattemptcount;
			
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
		///UserName
		///</summary>
		public string UserName
		{
			get	{ return _username; }
			set	{ _username = value; }
		}

		///<summary>
		///Password
		///</summary>
		public string Password
		{
			get	{ return _password; }
			set	{ _password = value; }
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

		///<summary>
		///IsLockedOut
		///</summary>
		public int IsLockedOut
		{
			get	{ return _islockedout; }
			set	{ _islockedout = value; }
		}

		///<summary>
		///IsApproved
		///</summary>
		public int IsApproved
		{
			get	{ return _isapproved; }
			set	{ _isapproved = value; }
		}

		///<summary>
		///FailedPasswordAttemptCount
		///</summary>
		public int FailedPasswordAttemptCount
		{
			get	{ return _failedpasswordattemptcount; }
			set	{ _failedpasswordattemptcount = value; }
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
            get { return "CM_DIMembership"; }
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
					case "UserName":
						return _username;
					case "Password":
						return _password;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "IsLockedOut":
						return _islockedout.ToString();
					case "IsApproved":
						return _isapproved.ToString();
					case "FailedPasswordAttemptCount":
						return _failedpasswordattemptcount.ToString();
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
					case "UserName":
						_username = value ;
						break;
					case "Password":
						_password = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "IsLockedOut":
						int.TryParse(value, out _islockedout);
						break;
					case "IsApproved":
						int.TryParse(value, out _isapproved);
						break;
					case "FailedPasswordAttemptCount":
						int.TryParse(value, out _failedpasswordattemptcount);
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
