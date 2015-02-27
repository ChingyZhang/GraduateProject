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
        if (tbx_Find.Text == "")
        {
            gv_List.DataSource = Membership.GetAllUsers();
        }
        else
        {
            gv_List.DataSource = Membership.FindUsersByName("%" + tbx_Find.Text + "%");
        }
        gv_List.DataBind();

    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string username = gv_List.DataKeys[e.Row.RowIndex]["UserName"].ToString();
            Label staffname = (Label)e.Row.FindControl("lb_StaffName");

            Org_Staff staff = UserBLL.GetStaffByUsername(username);
            if (staff != null)
            {
                staffname.Text = staff.RealName;
            }
            else
            {
                CM_Client client = new CM_ClientBLL(UserBLL.GetClientIDByUsername(username)).Model;
                if (client != null)
                {
                    staffname.Text = client.FullName;
                }
            }
        }
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
}
