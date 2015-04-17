using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_UDM_ModelFieldDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["TableID"] != null)
                ViewState["TableID"] = new Guid(Request.QueryString["TableID"]);
            else
                Response.Redirect("TableList.aspx");

            if (Request.QueryString["ID"] != null)
                ViewState["ID"] = new Guid(Request.QueryString["ID"]);

            BindDropDownList();

            if (ViewState["ID"] != null)
                BindData();
            lbl_TableName.Text = new UD_TableListBLL((Guid)ViewState["TableID"]).Model.Name;
        }
    }

    protected void ddl_DataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddl_DataType.SelectedValue)
        {
            case "1":
                tbx_Length.Enabled = false;
                tbx_Length.Text = "4";
                tbx_Precision.Text = "";
                break;
            case "2":
                tbx_Length.Enabled = false;
                tbx_Length.Text = "10";
                tbx_Precision.Text = "2";
                break;
            case "3":
                tbx_Length.Enabled = true;
                tbx_Precision.Text = "";
                break;
            case "4":
                tbx_Length.Enabled = false;
                tbx_Length.Text = "8";
                tbx_Precision.Text = "";
                break;
        }
    }
    private void BindDropDownList()
    {
        ddl_DataType.DataSource = DictionaryBLL.GetDicCollections("UD_DataType");
        ddl_DataType.DataBind();
        ddl_DataType_SelectedIndexChanged(null, null);
    }
    private void BindData()
    {
        Guid _id = (Guid)ViewState["ID"];
        UD_ModelFieldsBLL _modelfieldsbll = new UD_ModelFieldsBLL(_id);
        lbl_ID.Text = _modelfieldsbll.Model.ID.ToString();
        tbx_FieldName.Text = _modelfieldsbll.Model.FieldName;
        tbx_DisplayName.Text = _modelfieldsbll.Model.DisplayName;
        ddl_DataType.SelectedValue = _modelfieldsbll.Model.DataType.ToString();
        tbx_Length.Text = _modelfieldsbll.Model.DataLength.ToString();
        if (_modelfieldsbll.Model.Precision != 0) tbx_Precision.Text = _modelfieldsbll.Model.Precision.ToString();
        tbx_DefaultValue.Text = _modelfieldsbll.Model.DefaultValue;
        tbx_Description.Text = _modelfieldsbll.Model.Description;
        lbl_LastUpdateTime.Text = _modelfieldsbll.Model.LastUpdateTime.ToString();
        if (_modelfieldsbll.Model.RelationType > 0)
            rbl_RelationType.SelectedValue = _modelfieldsbll.Model.RelationType.ToString();
        rbl_RelationType_SelectedIndexChanged(null, null);
        tbx_SearchPageURL.Text = _modelfieldsbll.Model.SearchPageURL;

        if (ddl_RelationTableName != null || ddl_RelationTableName.Items.Count != 0)
        {
            ddl_RelationTableName.SelectedValue = _modelfieldsbll.Model.RelationTableName;
            {
                ddl_RelationTableName_SelectedIndexChanged(null, null);
            }
        }
        if (ddl_RelationTextField != null || ddl_RelationTextField.Items.Count != 0)
        {
            ddl_RelationTextField.SelectedValue = _modelfieldsbll.Model.RelationTextField;
        }
        if (ddl_RelationValueField != null || ddl_RelationValueField.Items.Count != 0)
        {
            ddl_RelationValueField.SelectedValue = _modelfieldsbll.Model.RelationValueField;
        }

        tbx_FieldName.Enabled = false;
        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        UD_ModelFieldsBLL _modelfieldsbll;
        if (ViewState["ID"] != null)
            _modelfieldsbll = new UD_ModelFieldsBLL((Guid)ViewState["ID"]);
        else
            _modelfieldsbll = new UD_ModelFieldsBLL();
        _modelfieldsbll.Model.TableID = (Guid)ViewState["TableID"];
        _modelfieldsbll.Model.FieldName = tbx_FieldName.Text;
        _modelfieldsbll.Model.DisplayName = tbx_DisplayName.Text;
        _modelfieldsbll.Model.DataType = int.Parse(ddl_DataType.SelectedValue);
        _modelfieldsbll.Model.DataLength = string.IsNullOrEmpty(tbx_Length.Text) ? 0 : int.Parse(tbx_Length.Text);
        _modelfieldsbll.Model.Precision = string.IsNullOrEmpty(tbx_Precision.Text) ? 0 : int.Parse(tbx_Precision.Text);
        _modelfieldsbll.Model.DefaultValue = tbx_DefaultValue.Text;
        _modelfieldsbll.Model.Description = tbx_Description.Text;
        _modelfieldsbll.Model.RelationType = int.Parse(rbl_RelationType.SelectedValue);
        _modelfieldsbll.Model.SearchPageURL = tbx_SearchPageURL.Text;

        if (ddl_RelationTableName.Visible)
        {
            _modelfieldsbll.Model.RelationTableName = ddl_RelationTableName.SelectedValue;
        }
        else
        {
            _modelfieldsbll.Model.RelationTableName = "";
        }
        if (ddl_RelationTextField.Visible)
        {
            _modelfieldsbll.Model.RelationTextField = ddl_RelationTextField.SelectedValue;
        }
        else
        {
            _modelfieldsbll.Model.RelationTextField = "";
        }
        if (ddl_RelationValueField.Visible)
        {
            _modelfieldsbll.Model.RelationValueField = ddl_RelationValueField.SelectedValue;
        }
        else
        {
            _modelfieldsbll.Model.RelationValueField = "";
        }

        _modelfieldsbll.Model.LastUpdateTime = DateTime.Now;

        if (ViewState["ID"] != null)
            _modelfieldsbll.Update();
        else
        {
            _modelfieldsbll.Model.Flag = "N";
            _modelfieldsbll.Add();
        }
        Response.Redirect("ModelFieldList.aspx?TableID=" + ViewState["TableID"].ToString());
    }

    protected void rbl_RelationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (rbl_RelationType.SelectedValue)
        {
            case "1":
                tr_1.Visible = true;
                tr_2.Visible = false;
                ddl_RelationTableName.DataTextField = "Name";
                ddl_RelationTableName.DataValueField = "TableName";
                ddl_RelationTableName.DataSource = DictionaryBLL.Dictionary_Type_GetAllList();
                ddl_RelationTableName.DataBind();
                break;
            case "2":
                tr_1.Visible = true;
                tr_2.Visible = true;
                ddl_RelationTableName.DataTextField = "DisplayName";
                ddl_RelationTableName.DataValueField = "Name";
                ddl_RelationTableName.DataSource = new UD_TableListBLL()._GetModelList("");
                ddl_RelationTableName.DataBind();
                ddl_RelationTableName_SelectedIndexChanged(null, null);
                break;
            case "3":
                tr_1.Visible = false;
                tr_2.Visible = false;
                break;
        }
    }

    protected void ddl_RelationTableName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RelationTableName.Visible && rbl_RelationType.SelectedValue=="2")
        {
            
            ddl_RelationTextField.DataSource = UD_ModelFieldsBLL.GetModelList("TableID='" + 
                new UD_TableListBLL(ddl_RelationTableName.SelectedValue).Model.ID.ToString() + "'");
            ddl_RelationTextField.DataBind();

            ddl_RelationValueField.DataSource = ddl_RelationTextField.DataSource;
            ddl_RelationValueField.DataBind();
        }
    }
}
