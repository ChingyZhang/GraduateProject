// ===================================================================
// 文件： VST_WorkItem.cs
// 项目名称：
// 创建时间：2015-02-01
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_WorkItem数据实体类
	/// </summary>
	[Serializable]
	public class VST_WorkItem : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _worklist = 0;
		private int _process = 0;
		private DateTime _worktime = new DateTime(1900,1,1);
		private DateTime _inserttime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_WorkItem()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_WorkItem(int id, int worklist, int process, DateTime worktime, DateTime inserttime, string remark)
		{
			_id           = id;
			_worklist     = worklist;
			_process      = process;
			_worktime     = worktime;
			_inserttime   = inserttime;
			_remark       = remark;
			
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
		///拜访表ID
		///</summary>
		public int WorkList
		{
			get	{ return _worklist; }
			set	{ _worklist = value; }
		}

		///<summary>
		///步骤ID
		///</summary>
		public int Process
		{
			get	{ return _process; }
			set	{ _process = value; }
		}

		///<summary>
		///工作时间
		///</summary>
		public DateTime WorkTime
		{
			get	{ return _worktime; }
			set	{ _worktime = value; }
		}

		///<summary>
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "VST_WorkItem"; }
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
					case "WorkList":
						return _worklist.ToString();
					case "Process":
						return _process.ToString();
					case "WorkTime":
						return _worktime.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "Remark":
						return _remark;
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
					case "WorkList":
						int.TryParse(value, out _worklist);
						break;
					case "Process":
						int.TryParse(value, out _process);
						break;
					case "WorkTime":
						DateTime.TryParse(value, out _worktime);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "Remark":
						_remark = value;
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
