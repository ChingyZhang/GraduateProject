using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using System.Data;
using MCSFramework.SQLDAL;

public partial class SubModule_UDM_FieldsInPanelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["ID"] != null)
                ViewState["ID"] = new Guid(Request.QueryString["ID"]);

            if (Request.QueryString["PanelID"] != null)
                ViewState["PanelID"] = new Guid(Request.QueryString["PanelID"]);
            else
                Response.Redirect("PanelList.aspx");

            UD_PanelBLL bll = new UD_PanelBLL((Guid)ViewState["PanelID"]);
            if (bll.Model.DisplayType == 2) tr_detail.Visible = false;

            BindDropDownList();

            if (ViewState["ID"] != null)
            {
                BindData();
            }
            else
            {
                #region 设置默认排序号
                tbx_SortID.Text = (bll.GetFieldMaxSort() + 1).ToString();
                #endregion
            }

        }
    }

    private void BindDropDownList()
    {
        ddl_TableName.DataSource = UD_Panel_TableBLL.GetTableListByPanelID(new Guid(ViewState["PanelID"].ToString()));
        ddl_TableName.DataBind();

        ddl_ControlType.DataSource = DictionaryBLL.GetDicCollections("UD_ControlType");
        ddl_ControlType.DataBind();

        ddl_DisplayMode.DataSource = DictionaryBLL.GetDicCollections("UD_DisplayMode");
        ddl_DisplayMode.DataBind();
        ddl_TableName_SelectedIndexChanged(null, null);
    }

    private void BindData()
    {
        Guid _id = (Guid)ViewState["ID"];
        UD_Panel_ModelFieldsBLL _modelfieldsbll = new UD_Panel_ModelFieldsBLL(_id);
        lbl_ID.Text = _modelfieldsbll.Model.ID.ToString();
        ddl_TableName.SelectedValue = new UD_ModelFieldsBLL(_modelfieldsbll.Model.FieldID).Model.TableID.ToString();
        ddl_TableName_SelectedIndexChanged(null, null);

        ddl_FieldID.SelectedValue = _modelfieldsbll.Model.FieldID.ToString();
        ddl_FieldID_SelectedIndexChanged(null, null);

        tbx_LabelText.Text = _modelfieldsbll.Model.LabelText;
        rbl_ReadOnly.SelectedValue = _modelfieldsbll.Model.ReadOnly;
        ddl_ControlType.SelectedValue = _modelfieldsbll.Model.ControlType.ToString();
        ddl_ControlType_SelectedIndexChanged(null, null);
        tbx_ControlWidth.Text = _modelfieldsbll.Model.ControlWidth.ToString();
        tbx_ControlHeight.Text = _modelfieldsbll.Model.ControlHeight.ToString();
        tbx_ControlStyle.Text = _modelfieldsbll.Model.ControlStyle;
        tbx_Description.Text = _modelfieldsbll.Model.Description;
        rbl_Enable.SelectedValue = _modelfieldsbll.Model.Enable;
        rbl_Visible.SelectedValue = _modelfieldsbll.Model.Visible;
        rbl_IsRequireField.SelectedValue = _modelfieldsbll.Model.IsRequireField;
        tbx_SortID.Text = _modelfieldsbll.Model.SortID.ToString();
        tbx_VisibleActionCode.Text = _modelfieldsbll.Model.VisibleActionCode;
        tbx_EnableActionCode.Text = _modelfieldsbll.Model.EnableActionCode;
        ddl_DisplayMode.SelectedValue = _modelfieldsbll.Model.DisplayMode.ToString();
        tbx_ColSpan.Text = _modelfieldsbll.Model.ColSpan.ToString();
        tbx_RegularExpression.Text = _modelfieldsbll.Model.RegularExpression;
        tbx_FormatString.Text = _modelfieldsbll.Model.FormatString;
        tbx_SearchPageURL.Text = _modelfieldsbll.Model.SearchPageURL;
        if (_modelfieldsbll.Model.TreeLevel > 0) tbx_TreeLevel.Text = _modelfieldsbll.Model.TreeLevel.ToString();

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        UD_Panel_ModelFieldsBLL _modelfieldsbll;
        if (ViewState["ID"] != null)
            _modelfieldsbll = new UD_Panel_ModelFieldsBLL((Guid)ViewState["ID"]);
        else
            _modelfieldsbll = new UD_Panel_ModelFieldsBLL();

        _modelfieldsbll.Model.PanelID = (Guid)ViewState["PanelID"];
        _modelfieldsbll.Model.FieldID = new Guid(ddl_FieldID.SelectedValue);
        _modelfieldsbll.Model.LabelText = tbx_LabelText.Text;
        _modelfieldsbll.Model.ReadOnly = rbl_ReadOnly.SelectedValue;
        _modelfieldsbll.Model.ControlType = int.Parse(ddl_ControlType.SelectedValue);
        if (tbx_ControlWidth.Text != "") _modelfieldsbll.Model.ControlWidth = int.Parse(tbx_ControlWidth.Text);
        if (tbx_ControlHeight.Text != "") _modelfieldsbll.Model.ControlHeight = int.Parse(tbx_ControlHeight.Text);
        _modelfieldsbll.Model.ControlStyle = tbx_ControlStyle.Text;
        _modelfieldsbll.Model.Description = tbx_Description.Text;
        _modelfieldsbll.Model.Enable = rbl_Enable.SelectedValue;
        _modelfieldsbll.Model.Visible = rbl_Visible.SelectedValue;
        _modelfieldsbll.Model.IsRequireField = rbl_IsRequireField.SelectedValue;
        _modelfieldsbll.Model.SortID = int.Parse(tbx_SortID.Text);
        _modelfieldsbll.Model.DisplayMode = int.Parse(ddl_DisplayMode.SelectedValue);
        if (tbx_ColSpan.Text != "") _modelfieldsbll.Model.ColSpan = int.Parse(tbx_ColSpan.Text);
        _modelfieldsbll.Model.RegularExpression = tbx_RegularExpression.Text.Trim();
        _modelfieldsbll.Model.FormatString = tbx_FormatString.Text.Trim();
        if (tbx_VisibleActionCode.Text != "") _modelfieldsbll.Model.VisibleActionCode = tbx_VisibleActionCode.Text;
        if (tbx_EnableActionCode.Text != "") _modelfieldsbll.Model.EnableActionCode = tbx_EnableActionCode.Text;
        _modelfieldsbll.Model.SearchPageURL = tbx_SearchPageURL.Text;
        if (tbx_TreeLevel.Text != "") _modelfieldsbll.Model.TreeLevel = int.Parse(tbx_TreeLevel.Text);

        if (ViewState["ID"] != null)
            _modelfieldsbll.Update();
        else
            _modelfieldsbll.Add();

        Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString());
    }
    protected void ddl_TableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        UD_ModelFieldsBLL mf = new UD_ModelFieldsBLL();

        ddl_FieldID.DataSource = mf._GetModelList("TableID='" + ddl_TableName.SelectedValue + "'");
        ddl_FieldID.DataBind();
        ddl_FieldID_SelectedIndexChanged(null, null);
    }

    protected void ddl_FieldID_SelectedIndexChanged(object sender, EventArgs e)
    {
        UD_ModelFieldsBLL bll = new UD_ModelFieldsBLL(new Guid(ddl_FieldID.SelectedValue));
        if (bll.Model.RelationType == 1 || bll.Model.RelationType == 2)
        {
            ddl_DisplayMode.Enabled = true;

            //如果关联表是树形结构，则允许设定树表层次
            if (bll.Model.RelationType == 2 && new UD_TableListBLL(bll.Model.RelationTableName).Model.TreeFlag == "Y")
                tbx_TreeLevel.Enabled = true;
            else
            {
                tbx_TreeLevel.Text = "";
                tbx_TreeLevel.Enabled = false;
            }
        }
        else
        {
            ddl_DisplayMode.Enabled = false;
            ddl_DisplayMode.SelectedValue = "1";
            tbx_TreeLevel.Text = "";
            tbx_TreeLevel.Enabled = false;
        }

        tbx_LabelText.Text = ddl_FieldID.SelectedItem.Text;
    }
    protected void ddl_ControlType_SelectedIndexChanged(object sender, EventArgs e)
    {

        switch (ddl_ControlType.SelectedValue)
        {
            case "1":   //label
                tbx_ControlWidth.Enabled = false;
                tbx_ControlHeight.Enabled = false;
                tbx_ControlWidth.Text = "";
                tbx_ControlHeight.Text = "";
                break;
            case "2":   //textbox
            case "3":   //Dropdown
            case "4":   //radiobutton
            case "6":   //select find control
            case "7":   //select Tree control
                tbx_ControlWidth.Enabled = true;
                tbx_ControlHeight.Enabled = false;
                tbx_ControlWidth.Text = "100";
                tbx_ControlHeight.Text = "0";
                break;
            case "5":   //multitext
                tbx_ControlWidth.Enabled = true;
                tbx_ControlHeight.Enabled = true;
                tbx_ControlWidth.Text = "400";
                tbx_ControlHeight.Text = "50";
                break;
        }

        if (ddl_ControlType.SelectedValue == "2" || ddl_ControlType.SelectedValue == "5")
            tbx_RegularExpression.Enabled = true;
        else
            tbx_RegularExpression.Enabled = false;

        if (ddl_ControlType.SelectedValue == "1" || ddl_ControlType.SelectedValue == "2" || ddl_ControlType.SelectedValue == "5")
            tbx_FormatString.Enabled = true;
        else
            tbx_FormatString.Enabled = false;

        if (ddl_ControlType.SelectedValue == "6")
            tbx_SearchPageURL.Enabled = true;
        else
            tbx_SearchPageURL.Enabled = false;
    }
}
