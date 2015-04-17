// ===================================================================
// 文件： Rpt_DataSetParams.cs
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
	///Rpt_DataSetParams数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSetParams : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
        private Guid _dataset = Guid.Empty;
        private string _paramname = string.Empty;
        private string _displayname = string.Empty;
        private int _datatype = 0;
        private int _paramsortid = 0;
        private string _description = string.Empty;
        private string _defaultvalue = string.Empty;
        private string _visible = string.Empty;
        private int _controltype = 0;
        private Guid _datasetsource = Guid.Empty;
        private string _regularexpression = string.Empty;
        private int _relationtype = 0;
        private string _relationtablename = string.Empty;
        private string _relationvaluefield = string.Empty;
        private string _relationtextfield = string.Empty;
        private string _searchpageurl = string.Empty;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private DateTime _updatetime = new DateTime(1900, 1, 1);
		
		private NameValueCollection _extpropertys;		
		#endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSetParams()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSetParams(Guid id, Guid dataset, string paramname, string displayname, int datatype, int paramsortid, string description, string defaultvalue, string visible, int controltype, Guid datasetsource, string regularexpression, int relationtype, string relationtablename, string relationvaluefield, string relationtextfield, string searchpageurl, DateTime inserttime, DateTime updatetime)
        {
            _id = id;
            _dataset = dataset;
            _paramname = paramname;
            _displayname = displayname;
            _datatype = datatype;
            _paramsortid = paramsortid;
            _description = description;
            _defaultvalue = defaultvalue;
            _visible = visible;
            _controltype = controltype;
            _datasetsource = datasetsource;
            _regularexpression = regularexpression;
            _relationtype = relationtype;
            _relationtablename = relationtablename;
            _relationvaluefield = relationvaluefield;
            _relationtextfield = relationtextfield;
            _searchpageurl = searchpageurl;
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
        ///参数名
        ///</summary>
        public string ParamName
        {
            get { return _paramname; }
            set { _paramname = value; }
        }

        ///<summary>
        ///显示名
        ///</summary>
        public string DisplayName
        {
            get { return _displayname; }
            set { _displayname = value; }
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
        ///参数排序
        ///</summary>
        public int ParamSortID
        {
            get { return _paramsortid; }
            set { _paramsortid = value; }
        }

        ///<summary>
        ///参数描述
        ///</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        ///<summary>
        ///默认值
        ///</summary>
        public string DefaultValue
        {
            get { return _defaultvalue; }
            set { _defaultvalue = value; }
        }

        ///<summary>
        ///是否可见
        ///</summary>
        public string Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        ///<summary>
        ///控件类型
        ///</summary>
        public int ControlType
        {
            get { return _controltype; }
            set { _controltype = value; }
        }

        ///<summary>
        ///数据源
        ///</summary>
        public Guid DataSetSource
        {
            get { return _datasetsource; }
            set { _datasetsource = value; }
        }

        ///<summary>
        ///正则表达式
        ///</summary>
        public string RegularExpression
        {
            get { return _regularexpression; }
            set { _regularexpression = value; }
        }

        ///<summary>
        ///关联类型，1 关联字典 2,关联实体表,3 不关联
        ///</summary>
        public int RelationType
        {
            get { return _relationtype; }
            set { _relationtype = value; }
        }

        ///<summary>
        ///关联表名
        ///</summary>
        public string RelationTableName
        {
            get { return _relationtablename; }
            set { _relationtablename = value; }
        }

        ///<summary>
        ///关联数值字段
        ///</summary>
        public string RelationValueField
        {
            get { return _relationvaluefield; }
            set { _relationvaluefield = value; }
        }

        ///<summary>
        ///关联文本字段
        ///</summary>
        public string RelationTextField
        {
            get { return _relationtextfield; }
            set { _relationtextfield = value; }
        }

        ///<summary>
        ///弹出窗口查询页面URL
        ///</summary>
        public string SearchPageURL
        {
            get { return _searchpageurl; }
            set { _searchpageurl = value; }
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
            get { return "Rpt_DataSetParams"; }
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
					case "ParamName":
						return _paramname;
					case "DisplayName":
						return _displayname;
					case "DataType":
						return _datatype.ToString();
					case "ParamSortID":
						return _paramsortid.ToString();
					case "Description":
						return _description;
					case "DefaultValue":
						return _defaultvalue;
					case "Visible":
						return _visible;
					case "ControlType":
						return _controltype.ToString();
					case "DataSetSource":
						return _datasetsource.ToString();
					case "RegularExpression":
						return _regularexpression;
                    case "RelationType":
                        return _relationtype.ToString();
                    case "RelationTableName":
                        return _relationtablename;
                    case "RelationValueField":
                        return _relationvaluefield;
                    case "RelationTextField":
                        return _relationtextfield;
					case "SearchPageURL":
						return _searchpageurl;
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
					case "ParamName":
						_paramname = value ;
						break;
					case "DisplayName":
						_displayname = value ;
						break;
					case "DataType":
						int.TryParse(value, out _datatype);
						break;
					case "ParamSortID":
						int.TryParse(value, out _paramsortid);
						break;
					case "Description":
						_description = value ;
						break;
					case "DefaultValue":
						_defaultvalue = value ;
						break;
					case "Visible":
						_visible = value ;
						break;
					case "ControlType":
						int.TryParse(value, out _controltype);
						break;
					case "DataSetSource":
                        _datasetsource = new Guid(value);
						break;
					case "RegularExpression":
						_regularexpression = value ;
						break;
                    case "RelationType":
                        int.TryParse(value, out _relationtype);
                        break;
                    case "RelationTableName":
                        _relationtablename = value;
                        break;
                    case "RelationValueField":
                        _relationvaluefield = value;
                        break;
                    case "RelationTextField":
                        _relationtextfield = value;
                        break;
					case "SearchPageURL":
						_searchpageurl = value ;
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
