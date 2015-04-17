// ===================================================================
// 文件： UD_PanelModel.cs
// 项目名称：
// 创建时间：2008-11-27
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
//using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///UD_Panel数据实体类
    /// </summary>
    [Serializable]
    public class UD_Panel : IModel
    {
        #region 变量定义
        private Guid _id = Guid.NewGuid();
        private string _code = String.Empty;
        private string _name = String.Empty;
        private int _sortid = 0;
        private string _enable = String.Empty;
        private string _description = String.Empty;
        private int _fieldcount = 0;
        private int _displaytype = 0;
        private Guid _detailviewid ;
        private string _advancefind = "N";
        private string _defaultsortfields = String.Empty;
        private string _tablestyle = String.Empty;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_Panel()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UD_Panel(Guid id, string code, string name, int sortid, string enable, string description, int fieldcount, int displaytype, Guid detailviewid, string advancefind, string defaultSortFields, string tableStyle)
        {
            _id = id;
            _code = code;
            _name = name;
            _sortid = sortid;
            _enable = enable;
            _description = description;
            _fieldcount = fieldcount;
            _displaytype = displaytype;
            _detailviewid = detailviewid;
            _advancefind = advancefind;
            _defaultsortfields = defaultSortFields;
            _tablestyle = tableStyle;

        }
        #endregion

        #region 公共属性

        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }

        public string Code
        {
            get
            { return _code; }
            set
            { _code = value; }
        }

        ///<summary>
        ///Name
        ///</summary>
        public string Name
        {
            get
            { return _name; }
            set
            { _name = value; }
        }

        ///<summary>
        ///SortID
        ///</summary>
        public int SortID
        {
            get
            { return _sortid; }
            set
            { _sortid = value; }
        }

        ///<summary>
        ///Enable
        ///</summary>
        public string Enable
        {
            get
            { return _enable; }
            set
            { _enable = value; }
        }

        ///<summary>
        ///Description
        ///</summary>
        public string Description
        {
            get
            { return _description; }
            set
            { _description = value; }
        }

        ///<summary>
        ///FieldCount
        ///</summary>
        public int FieldCount
        {
            get
            { return _fieldcount; }
            set
            { _fieldcount = value; }
        }

        ///<summary>
        ///DisplayType
        ///</summary>
        public int DisplayType
        {
            get
            { return _displaytype; }
            set
            { _displaytype = value; }
        }

        ///<summary>
        ///DetailViewID
        ///</summary>
        public Guid DetailViewID
        {
            get
            { return _detailviewid; }
            set
            { _detailviewid = value; }
        }

        /// <summary>
        /// 是否是高级查询列表
        /// </summary>
        public string AdvanceFind
        {
            get { return _advancefind; }
            set { _advancefind = value; }
        }

        /// <summary>
        /// 默认排序字段
        /// </summary>
        public string DefaultSortFields
        {
            get { return _defaultsortfields; }
            set { _defaultsortfields = value; }
        }

        /// <summary>
        /// 表格样式
        /// </summary>
        public string TableStyle
        {
            get { return _tablestyle; }
            set { _tablestyle = value; }
        }
        #endregion


        #region IModel 成员

        public string ModelName
        {
            get { return "UD_Panel"; }
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
                    case "Code":
                        return _code;
                    case "Name":
                        return _name;
                    case "SortID":
                        return _sortid.ToString();
                    case "Enable":
                        return _enable;
                    case "Description":
                        return _description;
                    case "FieldCount":
                        return _fieldcount.ToString();
                    case "DisplayType":
                        return _displaytype.ToString();
                    case "DetailViewID":
                        return _detailviewid.ToString();
                    case "AdvanceFind":
                        return _advancefind;
                    case "DefaultSortFields":
                        return _defaultsortfields;
                    case "TableStyle":
                        return _tablestyle;
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        _id = new Guid(value);
                        break;
                    case "Code":
                        _code = value;
                        break;
                    case "Name":
                        _name = value;
                        break;
                    case "SortID":
                        int.TryParse(value, out _sortid);
                        break;
                    case "Enable":
                        _enable = value;
                        break;
                    case "Description":
                        _description = value;
                        break;
                    case "FieldCount":
                        int.TryParse(value, out _fieldcount);
                        break;
                    case "DisplayType":
                        int.TryParse(value, out _displaytype);
                        break;
                    case "DetailViewID":
                        _detailviewid = new Guid(value);
                        break;
                    case "AdvanceFind":
                        _advancefind = value;
                        break;
                    case "DefaultSortFields":
                        _defaultsortfields = value;
                        break;
                    case "TableStyle":
                        _tablestyle = value;
                        break;
                }
            }
        }
        #endregion

        #endregion
    }
}
