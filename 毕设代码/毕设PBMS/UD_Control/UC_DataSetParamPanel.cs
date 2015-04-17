using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.RPT;
using MCSFramework.Model;
using MCSFramework.Model.RPT;
using MCSControls.MCSWebControls;
using MCSFramework.BLL.Pub;


namespace MCSFramework.UD_Control
{
    [ToolboxData("<{0}:UC_DataSetParamPanel runat=server></{0}:UC_DataSetParamPanel>")]
    public class UC_DataSetParamPanel : Panel
    {
        private Guid _dataset = Guid.Empty;
        #region Property
        [Browsable(true)]
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public Guid DataSet
        {
            get
            {
                try
                {
                    if (_dataset != Guid.Empty) return _dataset;

                    if (this.Page.Request.QueryString["Report"] != null)
                    {
                        Rpt_Report report = new Rpt_ReportBLL(new Guid(this.Page.Request.QueryString["Report"])).Model;
                        _dataset = report.DataSet;
                        return _dataset;
                    }
                }
                catch { }
                return Guid.Empty;
            }

            set { _dataset = value; }
        }

        [Browsable(false)]
        [Bindable(false)]
        private Dictionary<string, Rpt_DataSetParams> ParamControlsInfo
        {
            get
            {
                if (ViewState["ParamControlsInfo"] == null) ViewState["ParamControlsInfo"] = new Dictionary<string, Rpt_DataSetParams>();
                return (Dictionary<string, Rpt_DataSetParams>)ViewState["ParamControlsInfo"];
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
            if (DataSet == Guid.Empty) return;

            IList<Rpt_DataSetParams> paramfields = new Rpt_DataSetBLL(DataSet, true).GetParams();
            if (paramfields.Count == 0) return;    //无参数

            HtmlTable T_Content = new HtmlTable();
            string[] tablestyles = new string[] { "tabForm", "dataLabel", "dataField" };

            T_Content.CellPadding = 0;
            T_Content.CellSpacing = 0;
            T_Content.Width = "100%";
            T_Content.Border = 0;
            T_Content.ID = "T_Content_" + ID;
            this.Controls.Add(T_Content);

            HtmlTableRow tr_panel = new HtmlTableRow();//Create one TableRow for a panel 
            tr_panel.ID = "DataSetParamPanel";

            HtmlTableCell tc_panel = new HtmlTableCell();

            #region The title of the panel
            HtmlTable tb_panel_title = new HtmlTable();
            tb_panel_title.CellPadding = 0;
            tb_panel_title.CellSpacing = 0;
            tb_panel_title.Width = "100%";
            tb_panel_title.Height = "28px";
            tb_panel_title.Border = 0;
            tb_panel_title.Attributes["class"] = "h3Row";
            HtmlTableRow tr_panel_title = new HtmlTableRow();
            HtmlTableCell tc_panel_title = new HtmlTableCell();
            tc_panel_title.InnerHtml = "<h3>请设置统计报表参数信息</h3>";
            tr_panel_title.Cells.Add(tc_panel_title);
            tb_panel_title.Rows.Add(tr_panel_title);
            tc_panel.Controls.Add(tb_panel_title);
            #endregion

            #region The content of the panel

            int FieldCount = 3;

            HtmlTable tb_panel_content = new HtmlTable();
            tb_panel_content.Width = "100%";
            tb_panel_content.Attributes["class"] = tablestyles[0];
            int i = 0;
            foreach (Rpt_DataSetParams param in paramfields)
            {
                string ControlID = "Param_" + param.ParamName.Replace("@", "");

                //判断该控件是否已存在
                if (ParamControlsInfo.ContainsKey(ControlID)) continue;
                ParamControlsInfo.Add(ControlID, param);

                if (param.Visible == "Y")
                {
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
                    tc_displayname.Attributes["Class"] = tablestyles[1];
                    tc_displayname.InnerText = param.DisplayName;

                    if (tc_displayname.InnerText.Length <= 6)
                        tc_displayname.Attributes["Style"] = "width: 80px; height: 18px;";
                    else
                        tc_displayname.Attributes["Style"] = "width: 100px; height: 18px;";
                    tc_displayname.Attributes["nowrap"] = "nowrap";
                    tr_panel_detail.Cells.Add(tc_displayname);
                    #endregion

                    #region 增加Field Cell
                    HtmlTableCell tc_control = new HtmlTableCell();
                    tc_control.Attributes["Class"] = tablestyles[2];
                    i++;

                    WebControl control = null;


                    int RelationType = param.RelationType;
                    string RelationTableName = param.RelationTableName;
                    string RelationValueField = param.RelationValueField;
                    string RelationTextField = param.RelationTextField;

                    #region 根据控件类型生成相应的控件
                    switch (param.ControlType)
                    {
                        case 1://Label
                            control = new Label();
                            ((Label)control).Text = GetDefaule(param.DefaultValue);
                            break;
                        case 2://TextBox
                            control = new TextBox();
                            if (param.DataType == 4)
                            {
                                control.Attributes["onfocus"] = "WdatePicker();";
                            }
                            ((TextBox)control).Text = GetDefaule(param.DefaultValue);
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
                            if (param.DataType != 5)
                                ((DropDownList)control).Items.Insert(0, new ListItem("请选择...", "0"));
                            else
                                ((DropDownList)control).Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));

                            if (param.DefaultValue != "")
                            {
                                if (((DropDownList)control).Items.FindByValue(GetDefaule(param.DefaultValue)) != null)
                                    ((DropDownList)control).SelectedValue = GetDefaule(param.DefaultValue);
                            }
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

                            if (param.DefaultValue != "")
                            {
                                if (((RadioButtonList)control).Items.FindByValue(GetDefaule(param.DefaultValue)) != null)
                                    ((RadioButtonList)control).SelectedValue = GetDefaule(param.DefaultValue);
                            }
                            break;
                        case 5://MutiLinesTextBox
                            control = new TextBox();
                            ((TextBox)control).TextMode = TextBoxMode.MultiLine;
                            ((TextBox)control).Text = GetDefaule(param.DefaultValue);
                            break;
                        case 6://TextBox supports search
                            control = new MCSSelectControl();
                            control.ID = ControlID;
                            if (param.SearchPageURL != "")
                                ((MCSSelectControl)control).PageUrl = param.SearchPageURL;

                            if (param.DefaultValue != "")
                            {
                                control.Init += new EventHandler(control_Init);

                            }
                            break;
                        case 7://MCSTreeControl
                            control = new MCSTreeControl();

                            control.ID = ControlID;

                            if (param.RelationTableName == "MCS_SYS.dbo.Addr_OrganizeCity")
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
                                ((MCSTreeControl)control).RootValue = "0";
                                if (!Page.IsPostBack) ((MCSTreeControl)control).SelectValue = "0";
                            }

                            if (param.DefaultValue != "")
                            {
                                if (((MCSTreeControl)control).SelectValue == "0" || ((MCSTreeControl)control).SelectValue == "")
                                    ((MCSTreeControl)control).SelectValue = GetDefaule(param.DefaultValue);
                            }
                            break;
                    }
                    #endregion

                    control.ID = ControlID;
                    control.Enabled = param["Enable"] != "N";

                    int controlwidth = 0;
                    if (int.TryParse(param["ControlWidth"], out controlwidth)) control.Width = new Unit(controlwidth);
                    tc_control.Controls.Add(control);

                    #region 如果是文本框时，加上输入验证控件
                    if (param["IsRequireField"] == "Y")
                    {
                        Label lbl_reqinfo = new Label();
                        lbl_reqinfo.Text = "&nbsp;&nbsp;*";
                        lbl_reqinfo.ForeColor = System.Drawing.Color.Red;
                        tc_control.Controls.Add(lbl_reqinfo);
                    }

                    if (param.ControlType == 2 || param.ControlType == 5)
                    {
                        if (param["IsRequireField"] == "Y")
                        {
                            RequiredFieldValidator _requiredfieldvalidator = new RequiredFieldValidator();
                            _requiredfieldvalidator.ControlToValidate = control.ID;
                            _requiredfieldvalidator.Display = ValidatorDisplay.Dynamic;
                            _requiredfieldvalidator.ErrorMessage = "必填";
                            _requiredfieldvalidator.ForeColor = System.Drawing.Color.Red;

                            tc_control.Controls.Add(_requiredfieldvalidator);
                        }

                        if (param.DataType == 1 || param.DataType == 2 || param.DataType == 4)        //非varchar 字符串
                        {
                            CompareValidator _comparevalidator = new CompareValidator();
                            _comparevalidator.ControlToValidate = control.ID;
                            _comparevalidator.Operator = ValidationCompareOperator.DataTypeCheck;
                            _comparevalidator.Display = ValidatorDisplay.Dynamic;
                            _comparevalidator.ForeColor = System.Drawing.Color.Red;

                            if (param.DataType == 1)//int
                            {
                                _comparevalidator.Type = ValidationDataType.Integer;
                                _comparevalidator.ErrorMessage = "应为整数";

                            }
                            if (param.DataType == 2)//decimal
                            {
                                _comparevalidator.Type = ValidationDataType.Double;
                                _comparevalidator.ErrorMessage = "应为数字";
                            }
                            if (param.DataType == 4)//datetime
                            {
                                _comparevalidator.Type = ValidationDataType.Date;
                                _comparevalidator.ErrorMessage = "日期格式不正确";
                            }
                            tc_control.Controls.Add(_comparevalidator);
                        }
                        else
                        {
                            if (param.RegularExpression != "")
                            {
                                RegularExpressionValidator _regularexpressionvalidator = new RegularExpressionValidator();
                                _regularexpressionvalidator.ControlToValidate = control.ID;
                                _regularexpressionvalidator.ErrorMessage = "数据格式不正确";
                                _regularexpressionvalidator.ForeColor = System.Drawing.Color.Red;
                                _regularexpressionvalidator.ValidationExpression = param.RegularExpression;
                                _regularexpressionvalidator.Display = ValidatorDisplay.Dynamic;
                                tc_control.Controls.Add(_regularexpressionvalidator);
                            }
                        }
                    }
                    #endregion

                    tr_panel_detail.Cells.Add(tc_control);
                    #endregion
                }

            }
            #endregion

            tc_panel.Controls.Add(tb_panel_content);
            tr_panel.Cells.Add(tc_panel);
            T_Content.Rows.Add(tr_panel);

        }

        #region MCSSelectControl控件的初始默认值在控件初始化后赋值
        void control_Init(object sender, EventArgs e)
        {
            base.OnInit(e);

            MCSSelectControl control = (MCSSelectControl)sender;
            Rpt_DataSetParams param = ParamControlsInfo[control.ID];
            if (param.DefaultValue != "")
            {
                control.SelectValue = GetDefaule(param.DefaultValue);
                control.SelectText = GetRelateTextByValue(param, GetDefaule(param.DefaultValue));

                if (param.DefaultValue == "$TDPClient$" && control.SelectValue != "0" && control.SelectValue != "")
                {
                    control.Enabled = false;
                }
            }
        }
        #endregion

        #endregion

        private string GetDefaule(string defaulevalue)
        {
            if (defaulevalue.StartsWith("$") && defaulevalue.EndsWith("$"))
            {
                switch (defaulevalue)
                {
                    case "$StaffID$":
                        if (System.Web.HttpContext.Current.Session["AccountType"] == null ||
                            (int)System.Web.HttpContext.Current.Session["AccountType"] == 1)
                            defaulevalue = System.Web.HttpContext.Current.Session["UserID"].ToString();
                        else
                            defaulevalue = "0";
                        break;
                    case "$Today$":
                        defaulevalue = DateTime.Today.ToString("yyyy-MM-dd");
                        break;
                    case "$Now$":
                        defaulevalue = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        break;
                    case "$ThisMonthFirstDay$":
                        defaulevalue = DateTime.Today.ToString("yyyy-MM-01");
                        break;
                    case "$ThisYearFirstDay$":
                        defaulevalue = DateTime.Today.ToString("yyyy-01-01");
                        break;
                    case "$CurrentAccountMonth$":
                        defaulevalue = AC_AccountMonthBLL.GetCurrentMonth().ToString();
                        break;
                    case "$StaffOrganizeCity$":
                        {
                            if (System.Web.HttpContext.Current.Session["AccountType"] == null ||
                                (int)System.Web.HttpContext.Current.Session["AccountType"] == 1)
                            {
                                Org_StaffBLL staff = new Org_StaffBLL((int)System.Web.HttpContext.Current.Session["UserID"]);
                                if (staff.Model != null)
                                    defaulevalue = staff.Model.OrganizeCity.ToString();
                                else
                                    defaulevalue = "1";
                            }
                            else
                                defaulevalue = "1";
                        }
                        break;
                    case "$StaffOfficialCity$":
                        {
                            if (System.Web.HttpContext.Current.Session["AccountType"] == null ||
                                   (int)System.Web.HttpContext.Current.Session["AccountType"] == 1)
                            {
                                Org_StaffBLL staff = new Org_StaffBLL((int)System.Web.HttpContext.Current.Session["UserID"]);
                                if (staff.Model != null) defaulevalue = staff.Model.OfficialCity.ToString();
                            }
                            else
                                defaulevalue = "1";
                        }
                        break;
                    case "$TDPClient$":
                        {
                            Org_StaffBLL staff = new Org_StaffBLL((int)System.Web.HttpContext.Current.Session["UserID"]);
                            if (System.Web.HttpContext.Current.Session["OwnerType"] != null &&
                                    (int)System.Web.HttpContext.Current.Session["OwnerType"] == 3)
                                defaulevalue = staff.Model["OwnerClient"];
                            else
                                defaulevalue = "0";

                            break;
                        }
                    default:
                        break;
                }
            }

            return defaulevalue;
        }

        private string GetRelateTextByValue(Rpt_DataSetParams param, string value)
        {
            if (param.RelationType == 1)
            {
                //关联字典表
                return DictionaryBLL.GetDicCollections(param.RelationTableName, false)[value].Name;
            }
            else if (param.RelationType == 2)
            {
                //关联实体表
                return TreeTableBLL.GetRelationTableDataValue(param.RelationTableName, param.RelationValueField, param.RelationTextField, value);
            }
            else
                return value;
        }

        #region Get Params Value
        public bool GetParamsValue(out Dictionary<string, object> ParamValues)
        {
            ParamValues = new Dictionary<string, object>();

            foreach (string p_controlid in ParamControlsInfo.Keys)
            {
                Rpt_DataSetParams param = ParamControlsInfo[p_controlid];
                string paramvalue = "";

                if (param.Visible == "N")
                {
                    paramvalue = GetDefaule(param.DefaultValue);
                }
                else
                {
                    switch (param.ControlType)
                    {
                        case 1:
                            paramvalue = ((Label)this.FindControl((string)p_controlid)).Text;
                            break;
                        case 2:
                        case 5:
                            paramvalue = ((TextBox)this.FindControl((string)p_controlid)).Text;
                            if (paramvalue.Trim() == string.Empty && param["IsRequireField"] == "Y") return false;    //必填未填                           
                            break;
                        case 3:
                            paramvalue = ((DropDownList)this.FindControl((string)p_controlid)).SelectedValue;
                            if ((paramvalue.Trim() == string.Empty || paramvalue == "0") && param["IsRequireField"] == "Y") return false;    //必填未填   
                            break;
                        case 4:
                            paramvalue = ((RadioButtonList)this.FindControl((string)p_controlid)).SelectedValue;
                            if ((paramvalue.Trim() == string.Empty || paramvalue == "0") && param["IsRequireField"] == "Y") return false;    //必填未填   
                            break;
                        case 6:
                            paramvalue = ((MCSSelectControl)this.FindControl((string)p_controlid)).SelectValue;
                            if ((paramvalue.Trim() == string.Empty || paramvalue == "0") && param["IsRequireField"] == "Y") return false;    //必填未填   
                            break;
                        case 7:
                            paramvalue = ((MCSTreeControl)this.FindControl((string)p_controlid)).SelectValue;
                            if ((paramvalue.Trim() == string.Empty || paramvalue == "0") && param["IsRequireField"] == "Y") return false;    //必填未填   
                            break;
                    }
                }

                ParamValues.Add(param.ParamName, paramvalue);
            }

            return true;
        }
        #endregion
    }
}
