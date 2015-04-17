using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.IFStrategy;
using MCSFramework.Model;
using System.Data;

namespace MCSFramework.UD_Control
{
    [ToolboxData("<{0}:UC_DetailView runat=server></{0}:UC_DetailView>")]
    public class UC_DetailView : Panel
    {
        #region Property

        [Browsable(true),
        Description("The code of the page contains the content of the panel"),
        Category("Extends")]
        public string DetailViewCode
        {
            get
            {
                if (ViewState["DetailViewCode"] == null)
                    return "";
                return ViewState["DetailViewCode"].ToString();
            }
            set
            {
                ViewState["DetailViewCode"] = value;
            }
        }

        private string _validationgroup = "";
        [Browsable(true),
        Description("验证控件所属验证组"),
        Category("Extends")]
        public string ValidationGroup
        {
            get
            {
                return _validationgroup;
            }
            set
            {
                _validationgroup = value;
            }
        }

        /// <summary>
        ///  The struct contains the info of the special control
        ///  Key：ModelName|FieldName=ControlID   Value：FieldControlsInfo(a struct object)
        /// </summary>
        public Hashtable FieldControlsInfo
        {
            get
            {
                if (ViewState["FieldControlsInfo"] == null) return null;
                return (Hashtable)ViewState["FieldControlsInfo"];
            }
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
            HtmlTable T_Content = new HtmlTable();
            T_Content.CellPadding = 0;
            T_Content.CellSpacing = 0;
            T_Content.Width = "100%";
            T_Content.Border = 0;
            T_Content.ID = "T_Content_" + ID;
            this.Controls.Add(T_Content);


            UD_DetailViewBLL _DetailViewBll = new UD_DetailViewBLL(DetailViewCode, true);

            IList<UD_Panel> _panellist = _DetailViewBll.GetDetailPanels();
            Hashtable _htFieldControlsInfo = new Hashtable();

            foreach (UD_Panel _panelmodel in _panellist)
            {
                HtmlTableRow tr_panel = new HtmlTableRow();//Create one TableRow for a panel 
                tr_panel.ID = _panelmodel.Code;
                if (_panelmodel.Enable.ToUpper() == "N")
                    tr_panel.Visible = false;
                HtmlTableCell tc_panel = new HtmlTableCell();
                string _tablestytle = _panelmodel.TableStyle;
                string[] _tablestyles = _panelmodel.TableStyle.Split(new char[] { ',' });

                if (_tablestyles.Length < 3)
                    _tablestyles = ("tabForm,dataLabel,dataField").Split(new char[] { ',' });

                #region The title of the panel
                if (_panelmodel.Name != "")
                {
                    HtmlTable tb_panel_title = new HtmlTable();
                    tb_panel_title.CellPadding = 0;
                    tb_panel_title.CellSpacing = 0;
                    tb_panel_title.Width = "100%";
                    tb_panel_title.Height = "28px";
                    tb_panel_title.Border = 0;
                    tb_panel_title.Attributes["class"] = "h3Row";
                    HtmlTableRow tr_panel_title = new HtmlTableRow();
                    HtmlTableCell tc_panel_title = new HtmlTableCell();
                    tc_panel_title.InnerHtml = "<h3>" + _panelmodel.Name + "</h3>";
                    tr_panel_title.Cells.Add(tc_panel_title);
                    tb_panel_title.Rows.Add(tr_panel_title);
                    tc_panel.Controls.Add(tb_panel_title);
                }
                #endregion

                #region The content of the panel
                IList<UD_Panel_ModelFields> fields = new UD_PanelBLL(_panelmodel.ID, true).GetModelFields();

                int FieldCount = _panelmodel.FieldCount;

                HtmlTable tb_panel_content = new HtmlTable();
                tb_panel_content.Width = "100%";
                tb_panel_content.Attributes["class"] = _tablestyles[0];
                int i = 0;
                foreach (UD_Panel_ModelFields _panel_modelfields in fields)
                {
                    if (_panel_modelfields.Visible == "N") continue;

                    UD_ModelFields _modelfieldsmodel = new UD_ModelFieldsBLL(_panel_modelfields.FieldID, true).Model;
                    UD_TableList _tablemodel = new UD_TableListBLL(_modelfieldsmodel.TableID, true).Model;

                    #region 判断该控件是否已存在
                    if (_htFieldControlsInfo.Contains(_tablemodel.ModelClassName + "_" + _modelfieldsmodel.FieldName)) continue;
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
                    tc_displayname.Attributes["Class"] = _tablestyles[1];
                    tc_displayname.InnerText = string.IsNullOrEmpty(_panel_modelfields.LabelText) ?
                        _modelfieldsmodel.DisplayName : _panel_modelfields.LabelText;
                    if (tc_displayname.InnerText.Length <= 6)
                        tc_displayname.Attributes["Style"] = "width: 80px; height: 18px;";
                    else
                        tc_displayname.Attributes["Style"] = "width: 100px; height: 18px;";
                    tc_displayname.Attributes["nowrap"] = "nowrap";
                    tr_panel_detail.Cells.Add(tc_displayname);
                    #endregion

                    #region 增加Field Cell
                    HtmlTableCell tc_control = new HtmlTableCell();
                    tc_control.Attributes["Class"] = _tablestyles[2];
                    if (_panel_modelfields.ColSpan > 0)
                    {
                        if (i + _panel_modelfields.ColSpan <= FieldCount)
                        {
                            tc_control.ColSpan = 2 * _panel_modelfields.ColSpan - 1;
                            i = i + _panel_modelfields.ColSpan;
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

                    int RelationType = _modelfieldsmodel.RelationType;
                    string RelationTableName = _modelfieldsmodel.RelationTableName;
                    string RelationValueField = _modelfieldsmodel.RelationValueField;
                    string RelationTextField = _modelfieldsmodel.RelationTextField;

                    #region 根据控件类型生成相应的控件
                    switch (_panel_modelfields.ControlType)
                    {
                        case 1://Label
                            control = new Label();
                            break;
                        case 2://TextBox
                            control = new TextBox();
                            if (_modelfieldsmodel.DataType == 4)
                            {
                                control.Attributes["onfocus"] = "WdatePicker();";
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
                            if (_modelfieldsmodel.DataType != 5)
                                ((DropDownList)control).Items.Insert(0, new ListItem("请选择...", "0"));
                            else
                                ((DropDownList)control).Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
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
                            if (_panel_modelfields.ControlHeight > 0) ((TextBox)control).Height = new Unit(_panel_modelfields.ControlHeight);
                            break;
                        case 6://TextBox supports search
                            control = new MCSSelectControl();
                            if (RelationType == 2)//Relation to the model table
                            {
                                control.ID = _tablemodel.ModelClassName + "_" + _modelfieldsmodel.FieldName;

                                if (_panel_modelfields.SearchPageURL != "")
                                    ((MCSSelectControl)control).PageUrl = _panel_modelfields.SearchPageURL;
                                else if (_modelfieldsmodel.SearchPageURL != "")
                                    ((MCSSelectControl)control).PageUrl = _modelfieldsmodel.SearchPageURL;
                            }
                            break;
                        case 7://MCSTreeControl

                            control = new MCSTreeControl();


                            if (RelationType == 2)//Relation to the model table
                            {
                                control.ID = _tablemodel.ModelClassName + "_" + _modelfieldsmodel.FieldName;

                                if (_modelfieldsmodel.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
                                {
                                    #region 如果为管理片区字段，则取所能管辖的片区
                                    if (System.Web.HttpContext.Current.Session["AccountType"] == null ||
                                    (int)System.Web.HttpContext.Current.Session["AccountType"] == 1)
                                    {
                                        //员工
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
                                    }
                                    else if ((int)System.Web.HttpContext.Current.Session["AccountType"] == 2 &&
                                     System.Web.HttpContext.Current.Session["OrganizeCity"] != null)
                                    {
                                        //商业客户
                                        
                                        int city = (int)System.Web.HttpContext.Current.Session["OrganizeCity"];
                                        Addr_OrganizeCityBLL citybll = new Addr_OrganizeCityBLL(city);
                                        ((MCSTreeControl)control).DataSource = citybll.GetAllChildNodeIncludeSelf();
                                        ((MCSTreeControl)control).RootValue = citybll.Model.SuperID.ToString();
                                        ((MCSTreeControl)control).IDColumnName = "ID";
                                        ((MCSTreeControl)control).NameColumnName = "Name";
                                        ((MCSTreeControl)control).ParentColumnName = "SuperID";

                                        if (!Page.IsPostBack) ((MCSTreeControl)control).SelectValue = city.ToString();
                                    }
                                    #endregion
                                }
                                else if (_modelfieldsmodel.RelationTableName == "MCS_SYS.dbo.Addr_OfficialCity")
                                {
                                    ((MCSTreeControl)control).TableName = "MCS_SYS.dbo.Addr_OfficialCity";
                                    ((MCSTreeControl)control).IDColumnName = "ID";
                                    ((MCSTreeControl)control).NameColumnName = "Name";
                                    ((MCSTreeControl)control).ParentColumnName = "SuperID";
                                    ((MCSTreeControl)control).RootValue = "0";
                                    if (!Page.IsPostBack) ((MCSTreeControl)control).SelectValue = "0";
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

                    control.ID = _tablemodel.ModelClassName + "_" + _modelfieldsmodel.FieldName;
                    control.Enabled = _panel_modelfields.Enable.ToUpper() == "Y";

                    if (_panel_modelfields.ControlWidth > 0) control.Width = _panel_modelfields.ControlWidth;

                    tc_control.Controls.Add(control);

                    #region 如果是文本框时，加上输入验证控件
                    if (_panel_modelfields.IsRequireField == "Y")
                    {
                        Label lbl_reqinfo = new Label();
                        lbl_reqinfo.Text = "&nbsp;&nbsp;*";
                        lbl_reqinfo.ForeColor = System.Drawing.Color.Red;
                        tc_control.Controls.Add(lbl_reqinfo);
                    }

                    if (_panel_modelfields.ControlType == 2 || _panel_modelfields.ControlType == 5)
                    {
                        if (_panel_modelfields.IsRequireField == "Y")
                        {
                            RequiredFieldValidator _requiredfieldvalidator = new RequiredFieldValidator();
                            _requiredfieldvalidator.ControlToValidate = control.ID;
                            _requiredfieldvalidator.Display = ValidatorDisplay.Dynamic;
                            _requiredfieldvalidator.ErrorMessage = "必填";
                            _requiredfieldvalidator.ForeColor = System.Drawing.Color.Red;
                            _requiredfieldvalidator.ValidationGroup = _validationgroup;

                            tc_control.Controls.Add(_requiredfieldvalidator);
                        }

                        if (_panel_modelfields.RegularExpression != "")
                        {
                            RegularExpressionValidator _regularexpressionvalidator = new RegularExpressionValidator();
                            _regularexpressionvalidator.ControlToValidate = control.ID;
                            _regularexpressionvalidator.ErrorMessage = "数据格式不正确";
                            _regularexpressionvalidator.ForeColor = System.Drawing.Color.Red;
                            _regularexpressionvalidator.ValidationExpression = _panel_modelfields.RegularExpression;
                            _regularexpressionvalidator.ValidationGroup = ValidationGroup;
                            _regularexpressionvalidator.Display = ValidatorDisplay.Dynamic;
                            tc_control.Controls.Add(_regularexpressionvalidator);
                        }
                        else
                        {
                            if (_modelfieldsmodel.DataType == 1 || _modelfieldsmodel.DataType == 2 || _modelfieldsmodel.DataType == 4)        //非varchar 字符串
                            {
                                CompareValidator _comparevalidator = new CompareValidator();
                                _comparevalidator.ControlToValidate = control.ID;
                                _comparevalidator.Operator = ValidationCompareOperator.DataTypeCheck;
                                _comparevalidator.Display = ValidatorDisplay.Dynamic;
                                _comparevalidator.ForeColor = System.Drawing.Color.Red;
                                _comparevalidator.ValidationGroup = _validationgroup;

                                if (_modelfieldsmodel.DataType == 1)//int
                                {
                                    _comparevalidator.Type = ValidationDataType.Integer;
                                    _comparevalidator.ErrorMessage = "应为整数";

                                }
                                if (_modelfieldsmodel.DataType == 2)//decimal
                                {
                                    _comparevalidator.Type = ValidationDataType.Double;
                                    _comparevalidator.ErrorMessage = "应为数字";
                                }
                                if (_modelfieldsmodel.DataType == 4)//datetime
                                {
                                    _comparevalidator.Type = ValidationDataType.Date;
                                    _comparevalidator.ErrorMessage = "日期格式不正确";
                                }
                                tc_control.Controls.Add(_comparevalidator);
                            }
                        }
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(_panel_modelfields.Description))
                    {
                        Label lb = new Label();
                        lb.Text = "  " + _panel_modelfields.Description;
                        tc_control.Controls.Add(lb);
                    }

                    tr_panel_detail.Cells.Add(tc_control);
                    #endregion

                    #region 将控件记录到字段控件HashTable中
                    FieldControlInfo fieldcontrolinfo = new FieldControlInfo();

                    fieldcontrolinfo.FieldID = _modelfieldsmodel.ID;
                    fieldcontrolinfo.FieldName = _modelfieldsmodel.FieldName;
                    fieldcontrolinfo.ModelName = _tablemodel.ModelClassName;
                    fieldcontrolinfo.ControlType = _panel_modelfields.ControlType;
                    fieldcontrolinfo.ControlName = control.ID;
                    fieldcontrolinfo.DisplayMode = _panel_modelfields.DisplayMode;
                    fieldcontrolinfo.Panel_Field_ID = _panel_modelfields.ID;
                    _htFieldControlsInfo.Add(fieldcontrolinfo.ControlName, fieldcontrolinfo);
                    #endregion
                }
                #endregion

                tc_panel.Controls.Add(tb_panel_content);
                tr_panel.Cells.Add(tc_panel);
                T_Content.Rows.Add(tr_panel);
            }
            ViewState["FieldControlsInfo"] = _htFieldControlsInfo;
        }
        #endregion

        #region GetData
        /// <summary>
        /// Get data from the special model
        /// </summary>
        /// <param name="_model"></param>
        public void GetData(IModel _model)
        {
            foreach (object _key in FieldControlsInfo.Keys)
            {
                FieldControlInfo fieldcontrolinfo = (FieldControlInfo)FieldControlsInfo[_key];
                string fieldname = fieldcontrolinfo.FieldName;

                if (fieldcontrolinfo.ModelName == _model.ModelName)
                {
                    switch (fieldcontrolinfo.ControlType)
                    {
                        case 1:
                            //_model[fieldname] = ((Label)this.FindControl((string)_key)).Text;
                            break;
                        case 2:
                        case 5:
                            string _textboxvalue = ((TextBox)this.FindControl((string)_key)).Text;
                            _model[fieldname] = _textboxvalue;

                            if (_textboxvalue.Trim() == string.Empty)
                            {
                                //如果文本框的内容为空，则要判断是否是日期型的，如是，则设为1900-01-01
                                UD_ModelFields _modelfield = new UD_ModelFieldsBLL(fieldcontrolinfo.FieldID, true).Model;
                                if (_modelfield.DataType == 4) _model[fieldname] = "1900-01-01";
                            }
                            break;
                        case 3:
                            _model[fieldname] = ((DropDownList)this.FindControl((string)_key)).SelectedValue;
                            break;
                        case 4:
                            _model[fieldname] = ((RadioButtonList)this.FindControl((string)_key)).SelectedValue;
                            break;
                        case 6:
                            _model[fieldname] = ((MCSSelectControl)this.FindControl((string)_key)).SelectValue;
                            break;
                        case 7:
                            _model[fieldname] = ((MCSTreeControl)this.FindControl((string)_key)).SelectValue;
                            break;
                    }
                }
            }
        }
        #endregion

        #region BindData
        /// <summary>
        /// Bind data to the control from the special model
        /// </summary>
        /// <param name="_model"></param>
        public void BindData(IModel _model)
        {
            if (_model == null || _model["ID"] == string.Empty)
            {
                return;
            }

            foreach (object _key in FieldControlsInfo.Keys)
            {
                FieldControlInfo fieldcontrolinfo = (FieldControlInfo)FieldControlsInfo[_key];
                string fieldname = fieldcontrolinfo.FieldName;

                if (fieldcontrolinfo.ModelName == _model.ModelName)
                {
                    try
                    {
                        UD_Panel_ModelFields _panelfield = new UD_Panel_ModelFieldsBLL(fieldcontrolinfo.Panel_Field_ID, true).Model;
                        UD_ModelFields _modelfield = new UD_ModelFieldsBLL(fieldcontrolinfo.FieldID, true).Model;
                        string _formatstring = _panelfield.FormatString;

                        switch (fieldcontrolinfo.ControlType)
                        {
                            case 1: //Label控件
                                //判断是否显示关联表的文本值
                                if (fieldcontrolinfo.DisplayMode == 1)
                                {
                                    if (_modelfield.DataType == 1)
                                        ((Label)this.FindControl((string)_key)).Text = int.Parse(_model[fieldname]).ToString(_formatstring);
                                    else if (_modelfield.DataType == 2)
                                        ((Label)this.FindControl((string)_key)).Text = decimal.Parse(_model[fieldname]).ToString(_formatstring);
                                    else if (_modelfield.DataType == 4)
                                    {
                                        if (DateTime.Parse(_model[fieldname]) != new DateTime(1900, 1, 1))
                                            ((Label)this.FindControl((string)_key)).Text = DateTime.Parse(_model[fieldname]).ToString(_formatstring);
                                    }
                                    else
                                        ((Label)this.FindControl((string)_key)).Text = _model[fieldname];

                                }
                                else
                                {
                                    if (_panelfield.TreeLevel > 0 && new UD_TableListBLL(_modelfield.RelationTableName).Model.TreeFlag == "Y")
                                    {
                                        #region 字段关联到树形结构表，且要显示上层父结点信息
                                        int value = 0;
                                        if (int.TryParse(_model[fieldname], out value))
                                        {
                                            if (_panelfield.TreeLevel == 100)
                                            {
                                                //显示全路径
                                                ((Label)this.FindControl((string)_key)).Text = TreeTableBLL.GetFullPathName(_modelfield.RelationTableName, value);
                                            }
                                            else
                                            {
                                                //显示父级
                                                ((Label)this.FindControl((string)_key)).Text = TreeTableBLL.GetSuperNameByLevel(_modelfield.RelationTableName, value, _panelfield.TreeLevel);
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        ((Label)this.FindControl((string)_key)).Text = GetRelateTextByValue(_modelfield, _model[fieldname]);
                                    }
                                }
                                break;
                            case 2: //TextBox
                            case 5: //MultiRowTextBox
                                if (_modelfield.DataType == 1)
                                    ((TextBox)this.FindControl((string)_key)).Text = int.Parse(_model[fieldname]).ToString(_formatstring);
                                else if (_modelfield.DataType == 2)
                                    ((TextBox)this.FindControl((string)_key)).Text = decimal.Parse(_model[fieldname]).ToString(_formatstring);
                                else if (_modelfield.DataType == 4)
                                {
                                    if (DateTime.Parse(_model[fieldname]) != new DateTime(1900, 1, 1))
                                        ((TextBox)this.FindControl((string)_key)).Text = DateTime.Parse(_model[fieldname]).ToString(_formatstring);
                                }
                                else
                                    ((TextBox)this.FindControl((string)_key)).Text = _model[fieldname];
                                break;
                            case 3: //DropDownList
                                if (_model[fieldname] != "") ((DropDownList)this.FindControl((string)_key)).SelectedValue = _model[fieldname];
                                break;
                            case 4: //RadioButtonList
                                if (_model[fieldname] != "") ((RadioButtonList)this.FindControl((string)_key)).SelectedValue = _model[fieldname];
                                break;
                            case 6: //带选择功能的控件
                                if (_model[fieldname] != "")
                                {
                                    ((MCSSelectControl)this.FindControl((string)_key)).SelectValue = _model[fieldname];
                                    ((MCSSelectControl)this.FindControl((string)_key)).SelectText = GetRelateTextByValue(_modelfield, _model[fieldname]);
                                }
                                break;
                            case 7: //树形选择控件
                                if (_model[fieldname] != "") ((MCSTreeControl)this.FindControl((string)_key)).SelectValue = _model[fieldname];
                                break;
                        }
                    }
                    catch { }
                }
            }
        }
        #endregion

        private string GetRelateTextByValue(UD_ModelFields field, string value)
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

        public void SetControlsEnable(bool Enable)
        {
            SetControlsEnable(this, Enable);
        }
        private void SetControlsEnable(Control control, bool Enable)
        {
            foreach (Control _c in control.Controls)
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
        /// <summary>
        /// set the enable property for the special panel contains the specified code
        /// </summary>
        /// <param name="PanelCode"></param>
        /// <param name="Enable"></param>
        public void SetPanelEnable(string PanelCode, bool Enable)
        {
            HtmlTableRow tr_panel = (HtmlTableRow)FindControl(PanelCode);
            if (tr_panel != null)
            {
                SetControlsEnable(tr_panel, Enable);
            }
        }

        public void SetPanelVisible(string PanelCode, bool Visible)
        {
            HtmlTableRow tr_panel = (HtmlTableRow)FindControl(PanelCode);
            if (tr_panel != null)
            {
                tr_panel.Visible = Visible;
            }
        }
    }


    #region The struct contains the info of the special control
    /// <summary>
    /// The struct contains the info of the special control
    /// </summary>
    [Serializable]
    public struct FieldControlInfo
    {
        public Guid FieldID;
        public string FieldName;
        public string ModelName;
        public int ControlType;
        public string ControlName;
        public int DisplayMode; //如果是关联字段，其关联显示方式
        public Guid Panel_Field_ID;
    }
    #endregion

}
