﻿// ===================================================================
// 文件： SVM_ClassifyForcast.cs
// 项目名称：
// 创建时间：2011/10/13
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_ClassifyForcast数据实体类
	/// </summary>
	[Serializable]
	public class SVM_ClassifyForcast : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _accountmonth = 0;
		private int _organizecity = 0;
		private int _client = 0;
		private int _approveflag = 0;
        private int _taskid = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_ClassifyForcast()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_ClassifyForcast(int id, int accountmonth, int organizecity, int client, int approveflag,int taskid , DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_accountmonth = accountmonth;
			_organizecity = organizecity;
			_client       = client;
            _taskid = taskid;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			_updatetime   = updatetime;
			_updatestaff  = updatestaff;
			
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
		///会计月
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///预估客户
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
		}

		///<summary>
		///审核标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

        ///<summary>
        ///流程ID   
        ///</summary>
        public int TaskID
        {
            get { return _taskid; }
            set { _taskid = value; }
        }

		///<summary>
		///录入日期
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

		///<summary>
		///更新日期
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
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
            get { return "SVM_ClassifyForcast"; }
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
					case "AccountMonth":
						return _accountmonth.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "Client":
						return _client.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
                    case "TaskID":
                        return _taskid.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
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
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
                    case "TaskID":
                        int.TryParse(value, out _taskid);
                        break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
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
