// ===================================================================
// 文件： Rpt_Report.cs
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
	///Rpt_Report数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_Report : IModel
	{
		#region 私有变量定义
		private Guid _id = Guid.NewGuid();
		private string _name = string.Empty;
		private int _folder = 0;
        private Guid _dataset = Guid.Empty;
		private string _title = string.Empty;
		private int _reporttype = 0;
        private string _addrowtotal = string.Empty;
        private string _addcolumntotal = string.Empty;
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
        public Rpt_Report()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Rpt_Report(Guid id, string name, int folder, Guid dataset, string title, int reporttype, string addrowtotal, string addcolumntotal, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id             = id;
			_name           = name;
			_folder         = folder;
			_dataset        = dataset;
			_title          = title;
			_reporttype     = reporttype;
			_addrowtotal    = addrowtotal;
			_addcolumntotal = addcolumntotal;
			_approveflag    = approveflag;
			_inserttime     = inserttime;
			_insertstaff    = insertstaff;
			_updatetime     = updatetime;
			_updatestaff    = updatestaff;
			
		}
        #endregion

        #region 公共属性
        ///<summary>
        ///报表ID
        ///</summary>
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///报表名称
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///所属目录
        ///</summary>
        public int Folder
        {
            get { return _folder; }
            set { _folder = value; }
        }

        ///<summary>
        ///数据集
        ///</summary>
        public Guid DataSet
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        ///<summary>
        ///报表标题
        ///</summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<summary>
        ///报表类型(1普通表 2透视表)
        ///</summary>
        public int ReportType
        {
            get { return _reporttype; }
            set { _reporttype = value; }
        }

        ///<summary>
        ///是否加入行总计
        ///</summary>
        public string AddRowTotal
        {
            get { return _addrowtotal; }
            set { _addrowtotal = value; }
        }

        ///<summary>
        ///是否加入列总计
        ///</summary>
        public string AddColumnTotal
        {
            get { return _addcolumntotal; }
            set { _addcolumntotal = value; }
        }

        ///<summary>
        ///审核标志
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
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
        ///录入人
        ///</summary>
        public int InsertStaff
        {
            get { return _insertstaff; }
            set { _insertstaff = value; }
        }

        ///<summary>
        ///更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        ///<summary>
        ///更新人
        ///</summary>
        public int UpdateStaff
        {
            get { return _updatestaff; }
            set { _updatestaff = value; }
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
            get { return "Rpt_Report"; }
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
					case "Folder":
						return _folder.ToString();
                    case "DataSet":
                        return _dataset.ToString();
                    case "Title":
						return _title;
					case "ReportType":
						return _reporttype.ToString();
                    case "AddRowTotal":
                        return _addrowtotal;
                    case "AddColumnTotal":
                        return _addcolumntotal;
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
					case "Folder":
						int.TryParse(value, out _folder);
						break;
                    case "DataSet":
                        _dataset = new Guid(value);
                        break;
					case "Title":
						_title = value ;
						break;
					case "ReportType":
						int.TryParse(value, out _reporttype);
						break;
                    case "AddRowTotal":
                        _addrowtotal = value;
                        break;
                    case "AddColumnTotal":
                        _addcolumntotal = value;
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
