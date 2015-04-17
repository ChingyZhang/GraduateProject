// ===================================================================
// 文件： Rpt_DataSet.cs
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
	///Rpt_DataSet数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_DataSet : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private string _name = string.Empty;
		private Guid _datasource = Guid.Empty;
        private int _folder = 0;
		private string _enabled = string.Empty;
		private int _commandtype = 0;
		private string _commandtext = string.Empty;
		private string _conditiontext = string.Empty;
		private string _conditionvalue = string.Empty;
		private string _conditionsql = string.Empty;
		private string _orderstring = string.Empty;
		private string _isparamdataset = string.Empty;
		private int _approveflag = 0;
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
        public Rpt_DataSet()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSet(Guid id, string name, Guid datasource, int folder, string enabled, int commandtype, string commandtext, string conditiontext, string conditionvalue, string conditionsql, string orderstring, string isparamdataset, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _name = name;
            _datasource = datasource;
            _folder = folder;
            _enabled = enabled;
            _commandtype = commandtype;
            _commandtext = commandtext;
            _conditiontext = conditiontext;
            _conditionvalue = conditionvalue;
            _conditionsql = conditionsql;
            _orderstring = orderstring;
            _isparamdataset = isparamdataset;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;

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
        ///数据集名称
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///数据源
        ///</summary>
        public Guid DataSource
        {
            get { return _datasource; }
            set { _datasource = value; }
        }

        ///<summary>
        ///目录
        ///</summary>
        public int Folder
        {
            get { return _folder; }
            set { _folder = value; }
        }

		///<summary>
		///启用标志
		///</summary>
		public string Enabled
		{
			get	{ return _enabled; }
			set	{ _enabled = value; }
		}

		///<summary>
		///语句类型
		///</summary>
		public int CommandType
		{
			get	{ return _commandtype; }
			set	{ _commandtype = value; }
		}

		///<summary>
		///语句文本
		///</summary>
		public string CommandText
		{
			get	{ return _commandtext; }
			set	{ _commandtext = value; }
		}

		///<summary>
		///筛选条件文本
		///</summary>
		public string ConditionText
		{
			get	{ return _conditiontext; }
			set	{ _conditiontext = value; }
		}

		///<summary>
		///筛选条件值
		///</summary>
		public string ConditionValue
		{
			get	{ return _conditionvalue; }
			set	{ _conditionvalue = value; }
		}

		///<summary>
		///筛选条件SQL
		///</summary>
		public string ConditionSQL
		{
			get	{ return _conditionsql; }
			set	{ _conditionsql = value; }
		}

		///<summary>
		///排序语句
		///</summary>
		public string OrderString
		{
			get	{ return _orderstring; }
			set	{ _orderstring = value; }
		}

		///<summary>
		///是否针对参数使用的数据集
		///</summary>
		public string IsParamDataSet
		{
			get	{ return _isparamdataset; }
			set	{ _isparamdataset = value; }
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

		///<summary>
		///更新时间
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
            get { return "Rpt_DataSet"; }
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
					case "Name":
						return _name;
					case "DataSource":
						return _datasource.ToString();
                    case "Folder":
                        return _folder.ToString();
					case "Enabled":
						return _enabled;
					case "CommandType":
						return _commandtype.ToString();
					case "CommandText":
						return _commandtext;
					case "ConditionText":
						return _conditiontext;
					case "ConditionValue":
						return _conditionvalue;
					case "ConditionSQL":
						return _conditionsql;
					case "OrderString":
						return _orderstring;
					case "IsParamDataSet":
						return _isparamdataset;
					case "ApproveFlag":
						return _approveflag.ToString();
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
                        _id = new Guid(value);
						break;
					case "Name":
						_name = value ;
						break;
					case "DataSource":
                        _datasource = new Guid(value);
						break;
                    case "Folder":
                        int.TryParse(value, out _folder);
                        break;
					case "Enabled":
						_enabled = value ;
						break;
					case "CommandType":
						int.TryParse(value, out _commandtype);
						break;
					case "CommandText":
						_commandtext = value ;
						break;
					case "ConditionText":
						_conditiontext = value ;
						break;
					case "ConditionValue":
						_conditionvalue = value ;
						break;
					case "ConditionSQL":
						_conditionsql = value ;
						break;
					case "OrderString":
						_orderstring = value ;
						break;
					case "IsParamDataSet":
						_isparamdataset = value ;
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
