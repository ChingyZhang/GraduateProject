// ===================================================================
// 文件： Rpt_DataSetFields.cs
// 项目名称：
// 创建时间：2010/9/26
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
	///Rpt_DataSetFields数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSetFields : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
        private Guid _dataset = Guid.Empty;
        private Guid _fieldid = Guid.Empty;
        private string _fieldname = string.Empty;
        private string _displayname = string.Empty;
        private int _displaymode = 0;
        private int _treelevel = 0;
        private int _columnsortid = 0;
        private int _datatype = 0;
        private string _iscomputefield = string.Empty;
        private string _expression = string.Empty;
        private string _description = string.Empty;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private DateTime _updatetime = new DateTime(1900, 1, 1);
		
		private NameValueCollection _extpropertys;		
		#endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSetFields()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSetFields(Guid id, Guid dataset, Guid fieldid, string fieldname, string displayname, int displaymode, int treelevel, int columnsortid, int datatype, string iscomputefield, string expression, string description, DateTime inserttime, DateTime updatetime)
        {
            _id = id;
            _dataset = dataset;
            _fieldid = fieldid;
            _fieldname = fieldname;
            _displayname = displayname;
            _displaymode = displaymode;
            _treelevel = treelevel;
            _columnsortid = columnsortid;
            _datatype = datatype;
            _iscomputefield = iscomputefield;
            _expression = expression;
            _description = description;
            _inserttime = inserttime;
            _updatetime = updatetime;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///所属数据集
        ///</summary>
        public Guid DataSet
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        ///<summary>
        ///UD数据表中的字段ID
        ///</summary>
        public Guid FieldID
        {
            get { return _fieldid; }
            set { _fieldid = value; }
        }

        ///<summary>
        ///字段名称
        ///</summary>
        public string FieldName
        {
            get { return _fieldname; }
            set { _fieldname = value; }
        }

        ///<summary>
        ///显示名称
        ///</summary>
        public string DisplayName
        {
            get { return _displayname; }
            set { _displayname = value; }
        }

        ///<summary>
        ///显示方式 (1原始值 2关联值)
        ///</summary>
        public int DisplayMode
        {
            get { return _displaymode; }
            set { _displaymode = value; }
        }

        ///<summary>
        ///显示树表级别
        ///</summary>
        public int TreeLevel
        {
            get { return _treelevel; }
            set { _treelevel = value; }
        }

        ///<summary>
        ///字段排序号
        ///</summary>
        public int ColumnSortID
        {
            get { return _columnsortid; }
            set { _columnsortid = value; }
        }

        ///<summary>
        ///数据类型
        ///</summary>
        public int DataType
        {
            get { return _datatype; }
            set { _datatype = value; }
        }

        ///<summary>
        ///是否计算列
        ///</summary>
        public string IsComputeField
        {
            get { return _iscomputefield; }
            set { _iscomputefield = value; }
        }

        ///<summary>
        ///计算列表达式
        ///</summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        ///<summary>
        ///描述
        ///</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        ///<summary>
        ///录入时间
        ///</summary>
        public DateTime InsertTime
        {
            get { return _inserttime; }
            set { _inserttime = value; }
        }

        ///<summary>
        ///更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
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
            get { return "Rpt_DataSetFields"; }
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
                    case "FieldID":
                        return _fieldid.ToString();
                    case "FieldName":
                        return _fieldname;
                    case "DisplayName":
                        return _displayname;
                    case "DisplayMode":
                        return _displaymode.ToString();
                    case "TreeLevel":
                        return _treelevel.ToString();
                    case "ColumnSortID":
                        return _columnsortid.ToString();
                    case "DataType":
                        return _datatype.ToString();
                    case "IsComputeField":
                        return _iscomputefield;
                    case "Expression":
                        return _expression;
                    case "Description":
                        return _description;
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
					case "FieldID":
                        _fieldid = new Guid(value);
						break;
					case "FieldName":
						_fieldname = value ;
						break;
					case "DisplayName":
						_displayname = value ;
						break;
                    case "DisplayMode":
                        int.TryParse(value, out _displaymode);
                        break;
                    case "TreeLevel":
                        int.TryParse(value, out _treelevel);
                        break;
                    case "ColumnSortID":
						int.TryParse(value, out _columnsortid);
						break;
					case "DataType":
						int.TryParse(value, out _datatype);
						break;
					case "IsComputeField":
						_iscomputefield = value ;
						break;
					case "Expression":
						_expression = value ;
						break;
					case "Description":
						_description = value ;
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
