using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.IFStrategy;
using MCSFramework.Model;
using MCSFramework.Model.EWF;
using MCSFramework.BLL.EWF;
using System.Collections.Specialized;

namespace MCSFramework.UD_Control
{
    [ToolboxData("<{0}:UC_EWFPanel runat=server></{0}:UC_EWFPanel>")]
    public class UC_EWFPanel : Panel
    {
        public static readonly string sessionname = "UC_EWFPanel_App";
        #region Property

        private Guid _app = Guid.Empty;
        [Browsable(true),
        Description("The id of the app contains the content of the panel"),
        Category("Extends")]
        public Guid App
        {
            get
            {
                try
                {
                    if (_app != Guid.Empty) return _app;

                    if (this.Page.Request.QueryString["AppID"] != null)
                    {
                        _app = new Guid(this.Page.Request.QueryString["AppID"]);
                        return _app;
                    }

                    if (this.Page.Request.QueryString["TaskID"] != null)
                    {

                        int taskid = int.Parse(this.Page.Request.QueryString["TaskID"]);
                        _app = new EWF_TaskBLL(taskid).Model.App;

                        return _app;
                    }
                }
                catch { }
                //if (HttpContext.Current.Session[sessionname] != null)
                //{
                //    _app = (int)HttpContext.Current.Session[sessionname];
                //    return _app;
                //}
                return Guid.Empty;

            }
            set
            {
                _app = value;
                //HttpContext.Current.Session[sessionname] = value;
            }
        }

        private string _validationgroup = "";
        [Browsable(true),
        Description("验证控件所属验证组"),
        Category("Extends")]
        public string ValidationGroup
        {
            get
            { return _validationgroup; }
            set
            { _validationgroup = value; }
        }

        /// <summary>
        ///  The struct contains the info of the special control
        ///  Key：ModelName|FieldName=ControlID   Value：FieldControlsInfo(a struct object)
        /// </summary>
        public Hashtable HTDataObjectControlInfo
        {
            get
            {
                if (ViewState["HTDataObjectControlInfo"] == null) return null;
                return (Hashtable)ViewState["HTDataObjectControlInfo"];
            }
        }

        private int _fieldcount = 3;
        [Browsable(true),
        Description("每行显示列数"),
        Category("Extends")]
        public int FieldCount
        {
            get
            { return _fieldcount; }
            set
            { _fieldcount = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            try
            {
                InitComponents();
            }
            catch
            {
            }
            base.OnInit(e);
        }

        #region InitComponents
        /// <summary>
        /// Init the components of the panel through the special pageid
        /// </summary>
        private void InitComponents()
        {
            Hashtable _htDataObjectdControlsInfo = new Hashtable();
            HtmlTable T_Content = new HtmlTable();
            T_Content.CellPadding = 0;
            T_Content.CellSpacing = 0;
            T_Content.Width = "100%";
            T_Content.Border = 0;
            T_Content.ID = "T_Content_" + ID;
            this.Controls.Add(T_Content);

            HtmlTableRow T_tr_title = new HtmlTableRow();
            HtmlTableCell T_tc_title = new HtmlTableCell();
            T_tr_title.Cells.Add(T_tc_title);
            T_Content.Rows.Add(T_tr_title);

            #region The title of the panel
            HtmlTable tb_title = new HtmlTable();
            T_tc_title.Controls.Add(tb_title);
            tb_title.CellPadding = 0;
            tb_title.CellSpacing = 0;
            tb_title.Width = "100%";
            tb_title.Height = "30px";
            tb_title.Border = 0;
            tb_title.Attributes["class"] = "h3Row";

            HtmlTableRow tr_title = new HtmlTableRow();
            HtmlTableCell tc_title = new HtmlTableCell();
            tr_title.Cells.Add(tc_title);
            tb_title.Rows.Add(tr_title);
            tc_title.InnerHtml = "<h3>工作流提交的数据字段内容</h3>";
            #endregion

            #region The content of the panel
            HtmlTableRow T_tr_panelcontent = new HtmlTableRow();
            T_tr_panelcontent.ID = "T_tr_panelcontent";
            HtmlTableCell T_tc_panelcontent = new HtmlTableCell();
            T_tr_panelcontent.Cells.Add(T_tc_panelcontent);
            T_Content.Rows.Add(T_tr_panelcontent);


            HtmlTable tb_panel_content = new HtmlTable();
            tb_panel_content.Width = "100%";
            tb_panel_content.Attributes["class"] = "tabForm";
            T_tc_panelcontent.Controls.Add(tb_panel_content);
            IList<EWF_Flow_DataObject> _dataobjects = new EWF_Flow_AppBLL(App).GetDataObjectList();
            int i = 0;
            foreach (EWF_Flow_DataObject _dataobject in _dataobjects)
            {
                if (_dataobject.Visible == "N") continue;

                #region 判断该控件是否已存在
                if (_htDataObjectdControlsInfo.Contains(_dataobject.Name)) continue;
                #endregion

                #region 判断是否要增加新行
                HtmlTableRow tr_panel_detail;
                if (i >= FieldCount || i == 0)
                {
                    tr_panel_detail = new HtmlTableRow();
                    tb_panel_content.Rows.Add(tr_panel_detail);
                    i = 0;
                }
                else
                {
                    tr_panel_detail = tb_panel_content.Rows[tb_panel_content.Rows.Count - 1];
                }
                #endregion

                #region 增加Label Cell
                HtmlTableCell tc_displayname = new HtmlTableCell();
                tc_displayname.Attributes["Class"] = "dataLabel";
                tc_displayname.Attributes["Style"] = "width: 80px; height: 18px;";
                tc_displayname.Attributes["nowrap"] = "nowrap";
                tc_displayname.InnerText = _dataobject.DisplayName;
                tr_panel_detail.Cells.Add(tc_displayname);
                #endregion

                #region 增加Field Cell
                HtmlTableCell tc_control = new HtmlTableCell();
                tc_control.Attributes["Class"] = "dataField";

                if (_dataobject.ColSpan > 0)
                {
                    if (i + _dataobject.ColSpan <= FieldCount)
                    {
                        tc_control.ColSpan = 2 * _dataobject.ColSpan - 1;
                        i = i + _dataobject.ColSpan;
                    }
                    else
                    {
                        tc_control.ColSpan = 2 * (FieldCount - i) - 1;
                        i = 0;
                    }
                }
                else
                {
                    i++;
                }

                WebControl control = null;

                int RelationType = _dataobject.RelationType;
                string RelationTableName = _dataobject.RelationTableName;
                string RelationValueField = _dataobject.RelationValueField;
                string RelationTextField = _dataobject.RelationTextField;

                #region 根据控件类型生成相应的控件
                switch (_dataobject.ControlType)
                {
                    case 1://Label
                        control = new Label();
                        break;
                    case 2://TextBox
                        control = new TextBox();
                        if (_dataobject.DataType == 4)
                        {
                            control.Attributes["onfocus"] = "setday(this);";
                        }
                        break;
                    case 3://DropDownList
                        control = new DropDownList();
                        if (RelationType == 1)//Relation to the dictionary
                        {
                            ((DropDownList)control).DataSource = DictionaryBLL.GetDicCollections(RelationTableName, true);
                        }
                        else if (RelationType == 2)//Relation to the model table
                        {
                            ((DropDownList)control).DataSource = TreeTableBLL.GetRelationTableSourceData(RelationTableName, RelationValueField, RelationTextField);
                        }
                        else
                            break;

                        ((DropDownList)control).DataTextField = "Value";
                        ((DropDownList)control).DataValueField = "Key";
                        ((DropDownList)control).DataBind();
                        ((DropDownList)control).Items.Insert(0, new ListItem("请选择...", "0"));
                        break;
                    case 4://RadioButtonList
                        control = new RadioButtonList();
                        if (RelationType == 1)//Relation to the dictionary
                        {
                            ((RadioButtonList)control).DataSource = DictionaryBLL.GetDicCollections(RelationTableName, true);
                        }
                        else if (RelationType == 2)//Relation to the model table
                        {
                            ((RadioButtonList)control).DataSource = TreeTableBLL.GetRelationTableSourceData(RelationTableName, RelationValueField, RelationTextField);
                        }
                        else
                            break;

                        ((RadioButtonList)control).RepeatColumns = 6;
                        ((RadioButtonList)control).RepeatDirection = RepeatDirection.Horizontal;
                        ((RadioButtonList)control).DataTextField = "Value";
                        ((RadioButtonList)control).DataValueField = "Key";
                        ((RadioButtonList)control).DataBind();
                        if (((RadioButtonList)control).Items.Count != 0) ((RadioButtonList)control).SelectedIndex = 0;
                        break;
                    case 5://MutiLinesTextBox
                        control = new TextBox();
                        ((TextBox)control).TextMode = TextBoxMode.MultiLine;
                        if (_dataobject.ControlHeight > 0) ((TextBox)control).Height = new Unit(_dataobject.ControlHeight);
                        break;
                    case 6://TextBox supports search
                        control = new MCSSelectControl();
                        control.ID = "C_" + _dataobject.Name.ToString();
                        if (RelationType == 2)//Relation to the model table
                        {
                            ((MCSSelectControl)control).PageUrl = _dataobject.SearchPageURL;
                        }
                        break;
                    case 7://MCSTreeControl

                        control = new MCSTreeControl();


                        if (RelationType == 2)//Relation to the model table
                        {
                            control.ID = "C_" + _dataobject.Name.ToString();    //在设置控件DataSource之前，必须要有ID属性 Shen Gang 20090110
                            if (_dataobject.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                            {
                                #region 如果为管理片区字段，则取员工所能管辖的片区
                                Org_StaffBLL staff = new Org_StaffBLL((int)System.Web.HttpContext.Current.Session["UserID"]);
                                ((MCSTreeControl)control).DataSource = staff.GetStaffOrganizeCity();
                                ((MCSTreeControl)control).IDColumnName = "ID";
                                ((MCSTreeControl)control).NameColumnName = "Name";
                                ((MCSTreeControl)control).ParentColumnName = "SuperID";

                                if (((MCSTreeControl)control).DataSource.Select("ID = 1").Length > 0 || staff.Model.OrganizeCity == 0)
                                {
                                    ((MCSTreeControl)control).RootValue = "0";
                                    if (!Page.IsPostBack) ((MCSTreeControl)control).SelectValue = "0";
                                }
                                else
                                {
                                    ((MCSTreeControl)control).RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
                                    if (!Page.IsPostBack) ((MCSTreeControl)control).SelectValue = staff.Model.OrganizeCity.ToString();
                                }

                                #endregion
                            }
                            else
                            {
                                ((MCSTreeControl)control).TableName = RelationTableName;
                                ((MCSTreeControl)control).IDColumnName = RelationValueField;
                                ((MCSTreeControl)control).NameColumnName = RelationTextField;
                                ((MCSTreeControl)control).ParentColumnName = "SuperID";
                            }
                        }
                        break;
                }
                #endregion

                control.ID = "C_" + _dataobject.Name.ToString();
                control.Enabled = _dataobject.Enable.ToUpper() == "Y";
                control.ToolTip = _dataobject.Description;
                if (_dataobject.ControlWidth > 0) control.Width = _dataobject.ControlWidth;

                tc_control.Controls.Add(control);

                #region 如果是文本框时，加上输入验证控件
                if (_dataobject.IsRequireField == "Y")
                {
                    Label lbl_reqinfo = new Label();
                    lbl_reqinfo.Text = "*";
                    lbl_reqinfo.ForeColor = System.Drawing.Color.Red;
                    tc_control.Controls.Add(lbl_reqinfo);
                }
                //add validate control for the textbox

                if (_dataobject.ControlType == 2 || _dataobject.ControlType == 5)
                {
                    RequiredFieldValidator _requiredfieldvalidator = null;
                    CompareValidator _comparevalidator = null;
                    RegularExpressionValidator _regularexpressionvalidator = null;
                    if (_dataobject.IsRequireField == "Y")
                    {
                        _requiredfieldvalidator = new RequiredFieldValidator();
                        _requiredfieldvalidator.ControlToValidate = control.ID;
                        _requiredfieldvalidator.Display = ValidatorDisplay.Dynamic;
                        _requiredfieldvalidator.ErrorMessage = "必填";
                        _requiredfieldvalidator.ForeColor = System.Drawing.Color.Red;
                        _requiredfieldvalidator.ValidationGroup = _validationgroup;

                        tc_control.Controls.Add(_requiredfieldvalidator);
                    }

                    if (_dataobject.DataType == 1 || _dataobject.DataType == 2 || _dataobject.DataType == 4)        //非varchar 字符串
                    {
                        _comparevalidator = new CompareValidator();
                        _comparevalidator.ControlToValidate = control.ID;
                        _comparevalidator.Operator = ValidationCompareOperator.DataTypeCheck;
                        _comparevalidator.Display = ValidatorDisplay.Dynamic;
                        _comparevalidator.ForeColor = System.Drawing.Color.Red;
                        _comparevalidator.ValidationGroup = _validationgroup;

                        if (_dataobject.DataType == 1)//int
                        {
                            _comparevalidator.Type = ValidationDataType.Integer;
                            _comparevalidator.ErrorMessage = "应为整数";

                        }
                        if (_dataobject.DataType == 2)//decimal
                        {
                            _comparevalidator.Type = ValidationDataType.Double;
                            _comparevalidator.ErrorMessage = "应为数字";
                        }
                        if (_dataobject.DataType == 4)//datetime
                        {
                            _comparevalidator.Type = ValidationDataType.Date;
                            _comparevalidator.ErrorMessage = "日期格式不正确";
                        }
                        tc_control.Controls.Add(_comparevalidator);
                    }
                    else
                    {
                        if (_dataobject.RegularExpression != "")
                        {
                            _regularexpressionvalidator = new RegularExpressionValidator();
                            _regularexpressionvalidator.ControlToValidate = control.ID;
                            _regularexpressionvalidator.ErrorMessage = "数据格式不正确";
                            _regularexpressionvalidator.ForeColor = System.Drawing.Color.Red;
                            _regularexpressionvalidator.ValidationExpression = _dataobject.RegularExpression;
                            _regularexpressionvalidator.ValidationGroup = ValidationGroup;
                            _regularexpressionvalidator.Display = ValidatorDisplay.Dynamic;
                            tc_control.Controls.Add(_regularexpressionvalidator);
                        }
                    }
                }
                #endregion

                tr_panel_detail.Cells.Add(tc_control);
                #endregion

                #region Record the info of the control created
                DataObjectControlInfo dataobjectcontrolinfo = new DataObjectControlInfo();

                dataobjectcontrolinfo.ControlName = control.ID;
                dataobjectcontrolinfo.ControlType = _dataobject.ControlType;
                dataobjectcontrolinfo.DataObjectID = _dataobject.ID;
                dataobjectcontrolinfo.DataObjectName = _dataobject.Name;
                _htDataObjectdControlsInfo.Add(dataobjectcontrolinfo.DataObjectName, dataobjectcontrolinfo);
                #endregion
            }
            #endregion

            ViewState["HTDataObjectControlInfo"] = _htDataObjectdControlsInfo;

            if (new EWF_Flow_AppBLL(App).Model.RelateBusiness.ToUpper() == "Y")
                SetPanelEnable(false);
        }
        #endregion

        #region GetData
        public NameValueCollection GetData()
        {
            NameValueCollection _dataobjects = new NameValueCollection(HTDataObjectControlInfo.Count);//new EWF_TaskBLL(TaskID).GetDataObjectValue();

            foreach (object key in HTDataObjectControlInfo.Keys)
            {
                DataObjectControlInfo _dataobjectcontrolinfo = (DataObjectControlInfo)HTDataObjectControlInfo[key];
                switch (_dataobjectcontrolinfo.ControlType)
                {
                    case 1:
                        //_dataobjects.Add(key.ToString(), ((Label)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text);
                        break;
                    case 2:
                    case 5:
                        _dataobjects.Add(key.ToString(), ((TextBox)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text);
                        break;
                    case 3:
                        _dataobjects.Add(key.ToString(), ((DropDownList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue);
                        break;
                    case 4:
                        _dataobjects.Add(key.ToString(), ((RadioButtonList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue);
                        break;
                    case 6:
                        _dataobjects.Add(key.ToString(), ((MCSSelectControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectValue);
                        break;
                    case 7:
                        _dataobjects.Add(key.ToString(), ((MCSTreeControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectValue);
                        break;
                }


            }
            return _dataobjects;
        }
        #endregion

        #region BindData
        /// <summary>
        /// Bind data to the control by special taskid
        /// </summary>
        public void BindData(int TaskID)
        {
            BindData(new EWF_TaskBLL(TaskID).GetDataObjectValue());

            #region MyRegion
            //NameValueCollection dataobjects = new EWF_TaskBLL(TaskID).GetDataObjectValue();
            //foreach (string key1 in dataobjects.Keys)
            //{
            //    foreach (object key2 in HTDataObjectControlInfo.Keys)
            //    {
            //        DataObjectControlInfo _dataobjectcontrolinfo = (DataObjectControlInfo)HTDataObjectControlInfo[key2];
            //        if (key1 == _dataobjectcontrolinfo.DataObjectName)
            //        {
            //            try
            //            {
            //                switch (_dataobjectcontrolinfo.ControlType)
            //                {
            //                    case 1: //Label控件
            //                    case 2:
            //                    case 5:
            //                        ((TextBox)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = dataobjects[key1];
            //                        break;
            //                    case 3:
            //                        if (dataobjects[key1] != "")
            //                            ((DropDownList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue = dataobjects[key1];
            //                        break;
            //                    case 4:
            //                        if (dataobjects[key1] != "")
            //                            ((RadioButtonList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue = dataobjects[key1];
            //                        break;
            //                    case 7:
            //                        if (dataobjects[key1] != "")
            //                            ((MCSTreeControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectValue = dataobjects[key1];
            //                        break;
            //                }
            //            }
            //            catch { }
            //        }
            //    }
            //}
            #endregion
        }

        /// <summary>
        /// Bind data to the control by special NameValueCollection
        /// </summary>
        public void BindData(NameValueCollection dataobjects)
        {
            foreach (object key in HTDataObjectControlInfo.Keys)
            {
                DataObjectControlInfo _dataobjectcontrolinfo = (DataObjectControlInfo)HTDataObjectControlInfo[key];
                EWF_Flow_DataObject _dataobjct = new EWF_Flow_DataObjectBLL(_dataobjectcontrolinfo.DataObjectID).Model;

                try
                {
                    switch (_dataobjectcontrolinfo.ControlType)
                    {
                        case 1: //Label控件
                            if (_dataobjct.RelationType == 3)
                                ((Label)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = dataobjects[key.ToString()];
                            else
                            {
                                if (_dataobjct.RelationType == 2 && new UD_TableListBLL(_dataobjct.RelationTableName).Model.TreeFlag == "Y")
                                {
                                    //关联到实体树形结构表时，显示全路径
                                    int value = 0;
                                    if (int.TryParse(dataobjects[key.ToString()], out value))
                                        ((Label)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = TreeTableBLL.GetFullPathName(_dataobjct.RelationTableName, value);
                                }
                                else
                                    ((Label)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = GetRelateTextByValue(_dataobjct, dataobjects[key.ToString()]);
                            }
                            break;
                        case 2:
                        case 5:
                            if (_dataobjct.RelationType == 3)
                                ((TextBox)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = dataobjects[key.ToString()];
                            else
                                ((TextBox)this.FindControl(_dataobjectcontrolinfo.ControlName)).Text = GetRelateTextByValue(_dataobjct, dataobjects[key.ToString()]);
                            break;
                        case 3:
                            if (dataobjects[key.ToString()] != "")
                                ((DropDownList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue = dataobjects[key.ToString()];
                            break;
                        case 4:
                            if (dataobjects[key.ToString()] != "")
                                ((RadioButtonList)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectedValue = dataobjects[key.ToString()];
                            break;
                        case 6: //带选择功能的控件
                            if (dataobjects[key.ToString()] != "")
                            {
                                ((MCSSelectControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectValue = dataobjects[key.ToString()];
                                ((MCSSelectControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectText = GetRelateTextByValue(_dataobjct, dataobjects[key.ToString()]);
                            }
                            break;
                        case 7:
                            if (dataobjects[key.ToString()] != "")
                                ((MCSTreeControl)this.FindControl(_dataobjectcontrolinfo.ControlName)).SelectValue = dataobjects[key.ToString()];
                            break;
                    }
                }
                catch { }
            }
        }
        #endregion

        private string GetRelateTextByValue(EWF_Flow_DataObject field, string value)
        {
            if (field.RelationType == 1)
            {
                //关联字典表
                return DictionaryBLL.GetDicCollections(field.RelationTableName, false)[value].Name;
            }
            else if (field.RelationType == 2)
            {
                //关联实体表
                return TreeTableBLL.GetRelationTableDataValue(field.RelationTableName, field.RelationValueField, field.RelationTextField, value);
            }
            else
                return value;
        }

        /// <summary>
        /// set the enable property for the panel
        /// </summary>
        /// <param name="PanelCode"></param>
        /// <param name="Enable"></param>
        public void SetPanelEnable(bool Enable)
        {
            HtmlTableRow tr = (HtmlTableRow)FindControl("T_tr_panelcontent");
            if (tr != null)
            {
                //tr.Disabled = !Enable;
                SetControlsEnable(tr, Enable);
            }
        }

        private void SetControlsEnable(Control C, bool Enable)
        {
            foreach (Control _c in C.Controls)
            {
                string Type = _c.GetType().ToString();
                if (Type == "System.Web.UI.WebControls.TextBox")
                    ((TextBox)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.DropDownList")
                    ((DropDownList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.RadioButtonList")
                    ((RadioButtonList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.CheckBoxList")
                    ((CheckBoxList)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.Button")
                    ((Button)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.DataControlButton")
                    ((Button)_c).Enabled = Enable;
                if (Type == "System.Web.UI.WebControls.HyperLink")
                    ((HyperLink)_c).Enabled = Enable;
                if (Type == "MCSControls.MCSWebControls.MCSSelectControl")
                    ((MCSSelectControl)_c).Enabled = Enable;
                if (Type == "MCSControls.MCSWebControls.MCSTreeControl")
                    ((MCSTreeControl)_c).Enabled = Enable;

                if (_c.HasControls())
                {
                    SetControlsEnable(_c, Enable);
                }
            }
        }
    }


    #region The struct contains the info of the special control
    /// <summary>
    /// The struct contains the info of the special control
    /// </summary>
    [Serializable]
    public struct DataObjectControlInfo
    {
        public Guid DataObjectID;
        public string DataObjectName;
        public int ControlType;
        public string ControlName;
    }
    #endregion

}
