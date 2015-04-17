// ===================================================================
// 文件： Rpt_DataSetTables.cs
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
	///Rpt_DataSetTables数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSetTables : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private Guid _dataset = Guid.Empty;
		private Guid _tableid = Guid.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///数据集包含的数据表
		///</summary>
		public Rpt_DataSetTables()
		{
		}
		
		///<summary>
		///数据集包含的数据表
		///</summary>
		public Rpt_DataSetTables(Guid id, Guid dataset, Guid tableid, DateTime inserttime, DateTime updatetime)
		{
			_id           = id;
			_dataset      = dataset;
			_tableid      = tableid;
			_inserttime   = inserttime;
			_updatetime   = updatetime;
			
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
		///包含数据表
		///</summary>
		public Guid TableID
		{
			get	{ return _tableid; }
			set	{ _tableid = value; }
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
            get { return "Rpt_DataSetTables"; }
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
					case "TableID":
						return _tableid.ToString();
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
					case "TableID":
                        _tableid = new Guid(value);
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
