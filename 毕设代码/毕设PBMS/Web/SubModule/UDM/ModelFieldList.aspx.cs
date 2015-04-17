using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.IFStrategy;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_UDM_ModelFieldList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["TableID"] != null)
                ViewState["TableID"] = new Guid(Request.QueryString["TableID"]);

            if (ViewState["TableID"] == null)
                Response.Redirect("TableList.aspx");
            else
            {
                lbl_TableName.Text = new UD_TableListBLL((Guid)ViewState["TableID"]).Model.Name;
                string CacheKey = "UD_TableList-ModelFields-" + ViewState["TableID"].ToString();
                DataCache.RemoveCache(CacheKey);
                UD_ModelFieldsBLL.Init((Guid)ViewState["TableID"]);
                BindGrid();
            }
        }
    }

    private void BindGrid()
    {
        UD_TableListBLL bll = new UD_TableListBLL((Guid)ViewState["TableID"]);
        gv_List.BindGrid<UD_ModelFields>( bll.GetModelFields());

        if (bll.Model.ExtFlag=="N")        bt_OK.Visible = false;
    }


    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid id = (Guid)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        Response.Redirect("ModelFieldDetail.aspx?TableID=" + ViewState["TableID"].ToString() + "&ID=" + id.ToString());

    }


    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Response.Redirect("ModelFieldDetail.aspx?TableID=" + ViewState["TableID"].ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            Response.Redirect("TableDetail.aspx?TableID=" + ViewState["TableID"].ToString());
        }
    }
}
