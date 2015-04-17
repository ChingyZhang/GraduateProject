// ===================================================================
// 文件： Rpt_DataSetTableRelations.cs
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
	///Rpt_DataSetTableRelations数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSetTableRelations : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private Guid _dataset = Guid.Empty;
		private Guid _parenttableid = Guid.Empty;
		private Guid _parentfieldid = Guid.Empty;
		private Guid _childtableid = Guid.Empty;
		private Guid _childfieldid = Guid.Empty;
		private string _joinmode = string.Empty;
		private string _relationcondition = string.Empty;
		private int _sortid = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSetTableRelations()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_DataSetTableRelations(Guid id, Guid dataset, Guid parenttableid, Guid parentfieldid, Guid childtableid, Guid childfieldid, string joinmode, string relationcondition, int sortid, DateTime inserttime, DateTime updatetime)
		{
			_id                = id;
			_dataset           = dataset;
			_parenttableid     = parenttableid;
			_parentfieldid     = parentfieldid;
			_childtableid      = childtableid;
			_childfieldid      = childfieldid;
			_joinmode          = joinmode;
			_relationcondition = relationcondition;
			_sortid            = sortid;
			_inserttime        = inserttime;
			_updatetime        = updatetime;
			
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
		///所属数据集
		///</summary>
		public Guid DataSet
		{
			get	{ return _dataset; }
			set	{ _dataset = value; }
		}

		///<summary>
		///父表
		///</summary>
		public Guid ParentTableID
		{
			get	{ return _parenttableid; }
			set	{ _parenttableid = value; }
		}

		///<summary>
		///父字段
		///</summary>
		public Guid ParentFieldID
		{
			get	{ return _parentfieldid; }
			set	{ _parentfieldid = value; }
		}

		///<summary>
		///子表
		///</summary>
		public Guid ChildTableID
		{
			get	{ return _childtableid; }
			set	{ _childtableid = value; }
		}

		///<summary>
		///子表字段
		///</summary>
		public Guid ChildFieldID
		{
			get	{ return _childfieldid; }
			set	{ _childfieldid = value; }
		}

		///<summary>
		///关联方式
		///</summary>
		public string JoinMode
		{
			get	{ return _joinmode; }
			set	{ _joinmode = value; }
		}

		///<summary>
		///关联条件
		///</summary>
		public string RelationCondition
		{
			get	{ return _relationcondition; }
			set	{ _relationcondition = value; }
		}

		///<summary>
		///关系排序号
		///</summary>
		public int SortID
		{
			get	{ return _sortid; }
			set	{ _sortid = value; }
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
            get { return "Rpt_DataSetTableRelations"; }
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
					case "DataSet":
						return _dataset.ToString();
					case "ParentTableID":
						return _parenttableid.ToString();
					case "ParentFieldID":
						return _parentfieldid.ToString();
					case "ChildTableID":
						return _childtableid.ToString();
					case "ChildFieldID":
						return _childfieldid.ToString();
					case "JoinMode":
						return _joinmode;
					case "RelationCondition":
						return _relationcondition;
					case "SortID":
						return _sortid.ToString();
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
					case "DataSet":
						_dataset = new Guid(value);
						break;
					case "ParentTableID":
                        _parenttableid = new Guid(value);
						break;
					case "ParentFieldID":
                        _parentfieldid = new Guid(value);
						break;
					case "ChildTableID":
                        _childtableid = new Guid(value);
						break;
					case "ChildFieldID":
                        _childfieldid = new Guid(value);
						break;
					case "JoinMode":
						_joinmode = value ;
						break;
					case "RelationCondition":
						_relationcondition = value ;
						break;
					case "SortID":
						int.TryParse(value, out _sortid);
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
