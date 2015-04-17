using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_UDM_TableDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TableID"] != null)
                ViewState["TableID"] = new Guid(Request.QueryString["TableID"]);

            if (ViewState["TableID"] != null)
            {
                BindData();
            }
            else
            {
                MCSTabControl1.Items[1].Visible = false;
            }
        }
    }

    private void BindData()
    {
        UC_DetailView1.BindData(new UD_TableListBLL((Guid)ViewState["TableID"]).Model);
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        UD_TableListBLL bll;
        if (ViewState["TableID"] == null)
            bll = new UD_TableListBLL();
        else
            bll = new UD_TableListBLL((Guid)ViewState["TableID"]);

        UC_DetailView1.GetData(bll.Model);

        if (ViewState["TableID"] == null)
        {
            bll.Add();
        }
        else
        {
            bll.Update();
        }
        Response.Redirect("TableDetail.aspx?TableID=" + bll.Model.ID.ToString());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("ModelFieldList.aspx?TableID=" + ViewState["TableID"].ToString());
        }
    }
}
