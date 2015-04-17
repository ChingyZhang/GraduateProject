// ===================================================================
// 文件： Rpt_FolderRight.cs
// 项目名称：
// 创建时间：2010/10/12
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
	///Rpt_FolderRight数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_FolderRight : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _folder = 0;
		private int _action = 0;
		private int _based_on = 0;
		private int _position = 0;
		private string _rolename = string.Empty;
		private string _username = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_FolderRight()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_FolderRight(int id, int folder, int action, int based_on, int position, string rolename, string username, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_folder       = folder;
			_action       = action;
			_based_on     = based_on;
			_position     = position;
			_rolename     = rolename;
			_username     = username;
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
		///报表目录
		///</summary>
		public int Folder
		{
			get	{ return _folder; }
			set	{ _folder = value; }
		}

		///<summary>
		///权限动作 1查看 2修改所有报表 3创建及修改自己创建的报表
		///</summary>
		public int Action
		{
			get	{ return _action; }
			set	{ _action = value; }
		}

		///<summary>
		///基于 1职务 2角色 3用户
		///</summary>
		public int Based_On
		{
			get	{ return _based_on; }
			set	{ _based_on = value; }
		}

		///<summary>
		///职务
		///</summary>
		public int Position
		{
			get	{ return _position; }
			set	{ _position = value; }
		}

		///<summary>
		///角色
		///</summary>
		public string RoleName
		{
			get	{ return _rolename; }
			set	{ _rolename = value; }
		}

		///<summary>
		///用户名
		///</summary>
		public string UserName
		{
			get	{ return _username; }
			set	{ _username = value; }
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
		///录入人
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
            get { return "Rpt_FolderRight"; }
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
					case "Folder":
						return _folder.ToString();
					case "Action":
						return _action.ToString();
					case "Based_On":
						return _based_on.ToString();
					case "Position":
						return _position.ToString();
					case "RoleName":
						return _rolename;
					case "UserName":
						return _username;
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
					case "Folder":
						int.TryParse(value, out _folder);
						break;
					case "Action":
						int.TryParse(value, out _action);
						break;
					case "Based_On":
						int.TryParse(value, out _based_on);
						break;
					case "Position":
						int.TryParse(value, out _position);
						break;
					case "RoleName":
						_rolename = value ;
						break;
					case "UserName":
						_username = value ;
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
