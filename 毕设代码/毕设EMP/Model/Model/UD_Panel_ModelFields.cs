// ===================================================================
// 文件： UD_Panel_ModelFieldsModel.cs
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
    ///UD_Panel_ModelFields数据实体类
    /// </summary>
    [Serializable]
    public class UD_Panel_ModelFields : IModel
    {
        #region 变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _panelid;
        private Guid _fieldid;
        private string _labeltext = String.Empty;
        private string _readonly = String.Empty;
        private int _controltype = 0;
        private int _controlwidth = 0;
        private int _controlheight = 0;
        private string _controlstyle = String.Empty;
        private string _description = String.Empty;
        private string _enable = String.Empty;
        private string _visible = String.Empty;
        private string _isrequirefield = String.Empty;
        private int _sortid = 0;
        private string _visibleactioncode = String.Empty;
        private string _enableactioncode = string.Empty;
        private int _colspan = 0;
        private int _displaymode = 1;
        private string _regularexpression = String.Empty;
        private string _formatstring = String.Empty;
        private string _searchpageurl = string.Empty;
        private int _treelevel = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_Panel_ModelFields()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UD_Panel_ModelFields(Guid id, Guid panelid, Guid fieldid, string isreadonly, int controltype, int controlwidth, int controlheight, string controlstyle, string description, string enable, string visible, string isrequirefield, int sortid, string visibleactioncode, string enableactioncode)
        {
            _id = id;
            _panelid = panelid;
            _fieldid = fieldid;
            _readonly = isreadonly;
            _controltype = controltype;
            _controlwidth = controlwidth;
            _controlheight = controlheight;
            _controlstyle = controlstyle;
            _description = description;
            _enable = enable;
            _visible = visible;
            _isrequirefield = isrequirefield;
            _sortid = sortid;
            _visibleactioncode = visibleactioncode;
            _enableactioncode = enableactioncode;

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

        ///<summary>
        ///PanelID
        ///</summary>
        public Guid PanelID
        {
            get
            { return _panelid; }
            set
            { _panelid = value; }
        }

        ///<summary>
        ///FieldID
        ///</summary>
        public Guid FieldID
        {
            get
            { return _fieldid; }
            set
            { _fieldid = value; }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string LabelText
        {
            get
            { return _labeltext; }
            set
            { _labeltext = value; }
        }

        ///<summary>
        ///是否只读
        ///</summary>
        public string ReadOnly
        {
            get
            { return _readonly; }
            set
            { _readonly = value; }
        }

        ///<summary>
        ///ControlType
        ///</summary>
        public int ControlType
        {
            get
            { return _controltype; }
            set
            { _controltype = value; }
        }

        ///<summary>
        ///ControlWidth
        ///</summary>
        public int ControlWidth
        {
            get
            { return _controlwidth; }
            set
            { _controlwidth = value; }
        }

        ///<summary>
        ///ControlHeight
        ///</summary>
        public int ControlHeight
        {
            get
            { return _controlheight; }
            set
            { _controlheight = value; }
        }

        ///<summary>
        ///ControlStyle
        ///</summary>
        public string ControlStyle
        {
            get
            { return _controlstyle; }
            set
            { _controlstyle = value; }
        }

        public int ColSpan
        {
            get { return _colspan; }
            set { _colspan = value; }
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
        ///Visible
        ///</summary>
        public string Visible
        {
            get
            { return _visible; }
            set
            { _visible = value; }
        }

        ///<summary>
        ///IsRequireField
        ///</summary>
        public string IsRequireField
        {
            get
            { return _isrequirefield; }
            set
            { _isrequirefield = value; }
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
        ///查看权限代码
        ///</summary>
        public string VisibleActionCode
        {
            get
            { return _visibleactioncode; }
            set
            { _visibleactioncode = value; }
        }
        /// <summary>
        /// 编辑权限代表
        /// </summary>
        public string EnableActionCode
        {
            get
            { return _enableactioncode; }
            set
            { _enableactioncode = value; }
        }

        //显示方式(1显示值,2显示关联文本)
        public int DisplayMode
        {
            get { return _displaymode; }
            set { _displaymode = value; }
        }

        public string RegularExpression
        {
            get { return _regularexpression; }
            set { _regularexpression = value; }
        }

        public string FormatString
        {
            get { return _formatstring; }
            set { _formatstring = value; }
        }

        ///<summary>
        ///SearchPageURL
        ///</summary>
        public string SearchPageURL
        {
            get { return _searchpageurl; }
            set { _searchpageurl = value; }
        }

        /// <summary>
        /// 树结构层级
        /// </summary>
        public int TreeLevel
        {
            get
            { return _treelevel; }
            set
            { _treelevel = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "UD_Panel_ModelFields"; }
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
                    case "PanelID":
                        return _panelid.ToString();
                    case "FieldID":
                        return _fieldid.ToString();
                    case "ReadOnly":
                        return _readonly;
                    case "ControlType":
                        return _controltype.ToString();
                    case "ControlWidth":
                        return _controlwidth.ToString();
                    case "ControlHeight":
                        return _controlheight.ToString();
                    case "ControlStyle":
                        return _controlstyle;
                    case "ColSpan":
                        return _colspan.ToString();
                    case "Description":
                        return _description;
                    case "Enable":
                        return _enable;
                    case "Visible":
                        return _visible;
                    case "IsRequireField":
                        return _isrequirefield;
                    case "SortID":
                        return _sortid.ToString();
                    case "VisibleActionCode":
                        return _visibleactioncode;
                    case "EnableActionCode":
                        return _enableactioncode;
                    case "DisplayMode":
                        return _displaymode.ToString();
                    case "RegularExpression":
                        return _regularexpression;
                    case "FormatString":
                        return _formatstring;
                    case "SearchPageURL":
                        return _searchpageurl;
                    case "TreeLevel":
                        return _treelevel.ToString();
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
                    case "PanelID":
                        _panelid = new Guid(value);
                        break;
                    case "FieldID":
                        _fieldid = new Guid(value);
                        break;
                    case "ReadOnly":
                        _readonly = value;
                        break;
                    case "ControlType":
                        int.TryParse(value, out _controltype);
                        break;
                    case "ControlWidth":
                        int.TryParse(value, out _controlwidth);
                        break;
                    case "ControlHeight":
                        int.TryParse(value, out _controlheight);
                        break;
                    case "ControlStyle":
                        _controlstyle = value;
                        break;
                    case "ColSpan":
                        int.TryParse(value, out _colspan);
                        break;
                    case "Description":
                        _description = value;
                        break;
                    case "Enable":
                        _enable = value;
                        break;
                    case "Visible":
                        _visible = value;
                        break;
                    case "IsRequireField":
                        _isrequirefield = value;
                        break;
                    case "SortID":
                        int.TryParse(value, out _sortid);
                        break;
                    case "VisibleActionCode":
                        _visibleactioncode = value;
                        break;
                    case "EnableActionCode":
                        _enableactioncode = value;
                        break;
                    case "DisplayMode":
                        int.TryParse(value, out _displaymode);
                        break;
                    case "RegularExpression":
                        _regularexpression = value;
                        break;
                    case "FormatString":
                        _formatstring = value;
                        break;
                    case "SearchPageURL":
                        _searchpageurl = value;
                        break;
                    case "TreeLevel":
                        int.TryParse(value, out _treelevel);
                        break;
                }
            }
        }
        #endregion
    }
}
