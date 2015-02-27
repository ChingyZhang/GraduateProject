using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_UDM_WebPageControlList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["WebPageID"] != null)
            {
                ViewState["WebPageID"] = new Guid(Request.QueryString["WebPageID"]);

                lbl_WebPage.Text = new UD_WebPageBLL((Guid)ViewState["WebPageID"]).Model.Title;
                BindGrid();
            }
        }
    }

    private void BindGrid()
    {
        IList<UD_WebPageControl> fields = new UD_WebPageBLL((Guid)ViewState["WebPageID"]).GetWebControls();
        gv_List.BindGrid<UD_WebPageControl>(fields);
    }


    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ViewState["ID"] = (Guid)this.gv_List.DataKeys[e.NewSelectedIndex]["ID"];
        Response.Redirect("WebPageControlDetail.aspx?ID=" + ViewState["ID"].ToString());

    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebPageControlDetail.aspx?WebPageID=" + ViewState["WebPageID"].ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            Response.Redirect("WebPageDetail.aspx?ID=" + ViewState["WebPageID"].ToString());
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)gv_List.DataKeys[e.RowIndex][0];
        new UD_WebPageControlBLL(id).Delete();
        DataCache.RemoveCache("UD_WebPage-WebControls-" + ViewState["WebPageID"].ToString());
        BindGrid();
    }
}
