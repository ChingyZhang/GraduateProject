using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_UDM_WebPageDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                ViewState["ID"] = new Guid(Request.QueryString["ID"]);
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
        if (new UD_WebPageBLL((Guid)ViewState["ID"]).Model!=null)
            UC_DetailView1.BindData(new UD_WebPageBLL((Guid)ViewState["ID"]).Model);
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        UD_WebPageBLL bll;
        if (ViewState["ID"] == null)
            bll = new UD_WebPageBLL();
        else
            bll = new UD_WebPageBLL((Guid)ViewState["ID"]);

        UC_DetailView1.GetData(bll.Model);

        if (ViewState["ID"] == null)
        {
            bll.Add();
        }
        else
        {
            bll.Update();
        }
        Response.Redirect("WebPageDetail.aspx?ID=" + bll.Model.ID.ToString());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("WebPageControlList.aspx?WebPageID=" + ViewState["ID"].ToString());
        }
    }
}
