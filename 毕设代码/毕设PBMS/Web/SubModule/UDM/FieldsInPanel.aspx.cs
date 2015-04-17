using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using System.Data;
using MCSFramework.SQLDAL;
using MCSFramework.Model;

public partial class SubModule_UDM_FieldsInPanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = 0;

            if (Request.QueryString["PanelID"] != null)
                ViewState["PanelID"] = new Guid(Request.QueryString["PanelID"]);
            else
                Response.Redirect("PanelList.aspx");

            #region Detail详细信息，不需要定义表关系
            UD_PanelBLL bll = new UD_PanelBLL((Guid)ViewState["PanelID"]);
            lbl_PanelName.Text = bll.Model.Name;
            if (bll.Model.DisplayType == 1)
            {
                //Detail详细信息，不需要定义表关系
                MCSTabControl1.Items[2].Visible = false;
            }
            #endregion
            BindGrid();
        }
    }

    private void BindGrid()
    {
        gv_List.BindGrid<UD_Panel_ModelFields>(UD_Panel_ModelFieldsBLL.GetModelList("PanelID='" + ViewState["PanelID"].ToString() + "'"));
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {

        Response.Redirect("FieldsInPanelDetail.aspx?PanelID=" + ViewState["PanelID"].ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                Response.Redirect("PanelDetail.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "1":
                Response.Redirect("Panel_Table.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "2":
                Response.Redirect("Panel_TableRelation.aspx?PanelID=" + ViewState["PanelID"].ToString());
                break;
            case "3":
                //Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString() + "&PageID=" + ViewState["PageID"].ToString());
                break;
        }
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["ID"] = (Guid)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        Response.Redirect("FieldsInPanelDetail.aspx?ID=" + ViewState["ID"].ToString() + "&PanelID=" + ViewState["PanelID"].ToString());
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        new UD_Panel_ModelFieldsBLL((Guid)gv_List.DataKeys[e.RowIndex]["ID"]).Delete();
        Response.Redirect("FieldsInPanel.aspx?PanelID=" + ViewState["PanelID"].ToString());
    }


    protected void bt_Increase_Click(object sender, EventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        UD_Panel_ModelFieldsBLL bll = new UD_Panel_ModelFieldsBLL(id);
        bll.Model.SortID++;
        bll.Update();

        BindGrid();
    }
    protected void bt_Decrease_Click(object sender, EventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[((GridViewRow)((Button)sender).Parent.Parent).RowIndex][0];
        UD_Panel_ModelFieldsBLL bll = new UD_Panel_ModelFieldsBLL(id);
        if (bll.Model.SortID > 0) bll.Model.SortID--;
        bll.Update();

        BindGrid();
    }
}
