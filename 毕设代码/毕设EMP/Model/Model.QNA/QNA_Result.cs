// ===================================================================
// 文件： QNA_Result.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.QNA
{
	/// <summary>
	///QNA_Result数据实体类
	/// </summary>
	[Serializable]
	public class QNA_Result : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _project = 0;
		private int _relateclient = 0;
		private int _relatetask = 0;
		private string _iscommit = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public QNA_Result()
		{
		}
		
		///<summary>
		///
		///</summary>
		public QNA_Result(int id, int project, int relateclient, int relatetask, string iscommit, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_project      = project;
			_relateclient = relateclient;
			_relatetask   = relatetask;
			_iscommit     = iscommit;
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
		///问卷项目
		///</summary>
		public int Project
		{
			get	{ return _project; }
			set	{ _project = value; }
		}

		///<summary>
		///关联客户
		///</summary>
		public int RelateClient
		{
			get	{ return _relateclient; }
			set	{ _relateclient = value; }
		}

		///<summary>
		///关联工单
		///</summary>
		public int RelateTask
		{
			get	{ return _relatetask; }
			set	{ _relatetask = value; }
		}

		///<summary>
		///是否提交
		///</summary>
		public string IsCommit
		{
			get	{ return _iscommit; }
			set	{ _iscommit = value; }
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
            get { return "QNA_Result"; }
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
					case "Project":
						return _project.ToString();
					case "RelateClient":
						return _relateclient.ToString();
					case "RelateTask":
						return _relatetask.ToString();
					case "IsCommit":
						return _iscommit;
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
					case "Project":
						int.TryParse(value, out _project);
						break;
					case "RelateClient":
						int.TryParse(value, out _relateclient);
						break;
					case "RelateTask":
						int.TryParse(value, out _relatetask);
						break;
					case "IsCommit":
						_iscommit = value ;
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
