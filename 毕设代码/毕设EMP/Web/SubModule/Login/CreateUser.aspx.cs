using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using System.Web.Security;

public partial class SubModule_Login_CreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 300203, "LoginAssign"))
                Response.Redirect("~/SubModule/desktop.aspx");
            BindDropDown();
        }
    }

    private void BindDropDown()
    {
        ddl_Position.DataSource = Org_PositionBLL.GetAllPostion();
        ddl_Position.DataBind();

        if (Request.QueryString["StaffID"] != null)
        {
            int staffid = int.Parse(Request.QueryString["StaffID"]);
            Org_Staff staff = new Org_StaffBLL(staffid).Model;
            if (staff != null)
            {
                ddl_Position.SelectValue = staff.Position.ToString();
                ddl_Staff.DataSource = Org_StaffBLL.GetStaffList("Position=" + staff.Position.ToString());
                ddl_Staff.DataBind();
                ddl_Staff.SelectedValue = staff.ID.ToString();
            }
        }
        else
        {
            ddl_Staff.DataSource = Org_StaffBLL.GetStaffList("Position=" + ddl_Position.SelectValue);
            ddl_Staff.DataBind();
        }
    }

    protected void ddl_Position_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {

        if (e.CurSelectIndex != 0)
        {
            int position = e.CurSelectIndex;
            ddl_Staff.DataSource = Org_StaffBLL.GetStaffList("Position=" + position.ToString());
            ddl_Staff.DataBind();
        }
    }
    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        if (ddl_Staff.SelectedValue != "")
        {
            UserBLL.Membership_SetStaffID(CreateUserWizard1.UserName, int.Parse(ddl_Staff.SelectedValue));
            Roles.AddUserToRole(CreateUserWizard1.UserName, "全体员工");
        }
    }
    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
    protected void CreateUserWizard1_CancelButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx");
    }
}
