using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_Login_UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }

    }

    private void BindGrid()
    {
        if (tbx_Find.Text.Trim() == "")
        {
            gv_List.DataSource = CM_DIMembershipBLL.GetByUserName("");
        }
        else
        {
            gv_List.DataSource = CM_DIMembershipBLL.GetByUserName(tbx_Find.Text.Trim());
        }
        gv_List.DataBind();

    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("DICreateUser.aspx");
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected string ShowClient(int id)
    {
        string clientname = "";
        IList<CM_DIUsers> lists = CM_DIUsersBLL.GetModelList("ShipID=" + id.ToString());
        foreach (CM_DIUsers item in lists)
        {
            CM_Client c = new CM_ClientBLL(item.Client).Model;
            if (c != null) clientname += "<a href='DistributorDetail.aspx?ClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                + c.FullName + "</a><br/>";
        }
        return clientname;
    }
}
