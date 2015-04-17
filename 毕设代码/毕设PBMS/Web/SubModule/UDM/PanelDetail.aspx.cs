using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_UDM_PanelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PanelID"] != null)
                ViewState["PanelID"] = new Guid(Request.QueryString["PanelID"]);

            BindDropDown();

            if (ViewState["PanelID"] != null)
            {
                BindData();
            }
            else
            {
                if (Request.QueryString["DetailViewID"] != null)
                    ddl_DetailView.SelectedValue = Request.QueryString["DetailViewID"];
            }
        }

        if (ViewState["PanelID"] == null)
        {
            MCSTabControl1.Items[1].Visible = false;
            MCSTabControl1.Items[2].Visible = false;
            MCSTabControl1.Items[3].Visible = false;
        }
    }

    private void BindDropDown()
    {
        ddl_DetailView.DataSource = UD_DetailViewBLL.GetModelList("");
        ddl_DetailView.DataBind();
        ddl_DetailView.Items.Insert(0, new ListItem("请选择", "0"));
    }

    private void BindData()
    {
        Guid _id = (Guid)ViewState["PanelID"];
        UD_PanelBLL _pl = new UD_PanelBLL(_id);
        lbl_ID.Text = _pl.Model.ID.ToString();
        tbx_Code.Text = _pl.Model.Code;
        tbx_Name.Text = _pl.Model.Name;
        tbx_SortID.Text = _pl.Model.SortID.ToString();
        rbl_Enable.SelectedValue = _pl.Model.Enable;
        tbx_FieldCount.Text = _pl.Model.FieldCount.ToString();
        rbl_DisplayType.SelectedValue = _pl.Model.DisplayType.ToString();
        rbl_DisplayType_SelectedIndexChanged(null, null);
        if (!string.IsNullOrEmpty(_pl.Model.Description))
        {
            int pos = _pl.Model.Description.IndexOf("|");
            if (pos == -1)
                tbx_Description.Text = _pl.Model.Description;
            else
            {
                tbx_Description.Text = _pl.Model.Description.Substring(0, pos);
                if (pos + 1 < _pl.Model.Description.Length) tbx_DBConnectString.Text = _pl.Model.Description.Substring(pos + 1);
            }
        }
        ddl_DetailView.SelectedValue = _pl.Model.DetailViewID.ToString();
        rbl_AdvanceFind.SelectedValue = _pl.Model.AdvanceFind;
        ddl_TableStyle.SelectedValue = _pl.Model.TableStyle;
        tbx_DefaultSortFields.Text = _pl.Model.DefaultSortFields;

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;

        //Detail详细信息，不需要定义表关系
        if (_pl.Model.DisplayType == 1)
        {
            MCSTabControl1.Items[2].Visible = false;
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        UD_PanelBLL _pl;
        if (ViewState["PanelID"] != null)
            _pl = new UD_PanelBLL((Guid)ViewState["PanelID"]);
        else
            _pl = new UD_PanelBLL();
        _pl.Model.Code = tbx_Code.Text;
        _pl.Model.Name = tbx_Name.Text;
        if (tbx_SortID.Text != "") _pl.Model.SortID = int.Parse(tbx_SortID.Text);
        _pl.Model.Enable = rbl_Enable.SelectedValue;
        if (tbx_FieldCount.Text != "") _pl.Model.FieldCount = int.Parse(tbx_FieldCount.Text);
        _pl.Model.DisplayType = int.Parse(rbl_DisplayType.SelectedValue);
        if (ddl_DetailView.SelectedValue != "0")
            _pl.Model.DetailViewID = new Guid(ddl_DetailView.SelectedValue);
        _pl.Model.Description = tbx_Description.Text.Replace("|", "") + "|" + tbx_DBConnectString.Text.Replace("|", "");
        _pl.Model.AdvanceFind = rbl_AdvanceFind.SelectedValue;
        _pl.Model.DefaultSortFields = tbx_DefaultSortFields.Text;
        _pl.Model.TableStyle = ddl_TableStyle.SelectedValue;

        if (ViewState["PanelID"] != null)
            _pl.Update();
        else
            _pl.Add();

        Response.Redirect("PanelDetail.aspx?PanelID=" + _pl.Model.ID.ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                rbl_DisplayType_SelectedIndexChanged(null, null);
                break;
            case "1":
                Response.Redirect("Panel_Table.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "2":
                Response.Redirect("Panel_TableRelation.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "3":
                Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
        }
    }
    protected void rbl_DisplayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbl_DisplayType.SelectedValue == "2")
        {
            //列表视图
            ddl_DetailView.Enabled = false;
            tbx_FieldCount.Enabled = false;
            ddl_TableStyle.Enabled = false;
            tbx_DefaultSortFields.Enabled = true;
            rbl_AdvanceFind.Enabled = true;
        }
        else
        {
            //详细视图
            ddl_DetailView.Enabled = true;
            tbx_FieldCount.Enabled = true;
            ddl_TableStyle.Enabled = true;
            tbx_DefaultSortFields.Enabled = false;
            rbl_AdvanceFind.Enabled = false;
        }


        if (ViewState["PanelID"] != null)
        {
            if (rbl_DisplayType.SelectedValue == "2")
            {
                MCSTabControl1.Items[2].Visible = true;
            }
            else
            {
                MCSTabControl1.Items[2].Visible = false;
            }
        }
        else
        {
            MCSTabControl1.Items[1].Visible = false;
            MCSTabControl1.Items[2].Visible = false;
            MCSTabControl1.Items[3].Visible = false;
        }
    }
}
