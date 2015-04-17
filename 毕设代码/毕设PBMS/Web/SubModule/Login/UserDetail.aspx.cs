using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MCSFramework.Common;
using System.Data;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_Login_UserDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            if (Request.QueryString["Username"] != null)
            {
                BindData(Request.QueryString["Username"]);
            }
        }
    }

    private void BindDropDown()
    {
    }

    protected void BindUserInRole()
    {
        ddl_Role.Items.Clear();
        foreach (string role in Roles.GetAllRoles())
        {
            ddl_Role.Items.Add(role);
        }
        ddl_Role.Items.Insert(0, new ListItem("请选择要加入的角色...", ""));

        cbx_UserInRoles.Items.Clear();
        foreach (string s in Roles.GetRolesForUser(lb_Username.Text))
        {
            cbx_UserInRoles.Items.Add(s);

            ddl_Role.Items.FindByText(s).Enabled = false;
        }
    }

    private void BindData(string username)
    {
        MembershipUser user = Membership.GetUser(username, false);
        if (user != null)
        {
            lb_Username.Text = user.UserName;

            Org_Staff staff = UserBLL.GetStaffByUsername(user.UserName);
            if (staff != null)
            {
                select_Staff.SelectText = staff.RealName;
                select_Staff.SelectValue = staff.ID.ToString();
            }
            
            tbx_Email.Text = user.Email;

            bt_Unlock.Visible = user.IsLockedOut;
            bt_UnApprove.Visible = user.IsApproved;
            bt_Approve.Visible = !user.IsApproved;

            BindUserInRole();
            BindGrid();
        }
        else
        {
            MessageBox.ShowAndRedirect(this, "用户名不存在!", "UserList.aspx");
        }
    }

    private void BindGrid()
    {
        gv_MACAddrList.DataSource = User_RegisterMACBLL.GetModelList("UserName='" + lb_Username.Text + "'");
        gv_MACAddrList.DataBind();
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if ((select_Staff.SelectValue == "" || select_Staff.SelectValue == "0"))
        {
            MessageBox.Show(this, "请正确选择关联的公司员工！");
            return;
        }


        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        if (select_Staff.SelectValue != "")
            UserBLL.Membership_SetStaffID(user.UserName, int.Parse(select_Staff.SelectValue));
        
        user.Email = tbx_Email.Text;
        Membership.UpdateUser(user);

        Response.Redirect("UserList.aspx");
    }
    protected void bt_Unlock_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.UnlockUser();
        Membership.UpdateUser(user);
        BindData(user.UserName);
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.IsApproved = true;

        Membership.UpdateUser(user);
        Response.Redirect("UserList.aspx");
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.IsApproved = false;

        Membership.UpdateUser(user);
        Response.Redirect("UserList.aspx");
    }
    protected void bt_SaveResetPwd_Click(object sender, EventArgs e)
    {
        if (tbx_ConfirmPwd.Text.Length < 6)
        {
            MessageBox.Show(this, "密码长度必需大于6!");
            return;
        }

        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        string pwd = user.ResetPassword();
        if (!user.ChangePassword(pwd, tbx_ConfirmPwd.Text))
        {
            MessageBox.Show(this, "输入的密码无效,密码修改失败!");
            return;
        }
        Membership.UpdateUser(user);

        MessageBox.ShowAndRedirect(this, "密码重置成功!", "UserList.aspx");
    }


    protected void bt_DeleteUser_Click(object sender, EventArgs e)
    {
        Membership.DeleteUser(lb_Username.Text, true);
        Response.Redirect("UserList.aspx");
    }

    protected void bt_ResetPwd_Click(object sender, EventArgs e)
    {
        tr_resetpwd.Visible = true;
    }
    protected void gv_MACAddrList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_MACAddrList.DataKeys[e.RowIndex][0];
        if (id > 0)
        {
            new User_RegisterMACBLL(id).Delete();
            BindGrid();
        }
    }

    protected void bt_AddMAC_Click(object sender, EventArgs e)
    {
        if (tbx_MACAddr.Text != "")
        {
            string[] macaddrs = tbx_MACAddr.Text.Split(new char[] { ',', '，', '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (macaddrs.Length > 0)
            {
                foreach (string addr in macaddrs)
                {
                    if (User_RegisterMACBLL.GetModelList(string.Format("UserName='{0}' AND MACAddr='{1}'", lb_Username.Text, addr)).Count == 0)
                    {
                        User_RegisterMACBLL bll = new User_RegisterMACBLL();
                        bll.Model.UserName = lb_Username.Text;
                        bll.Model.MacAddr = addr;
                        bll.Model.Enabled = "Y";
                        bll.Model.ApproveFlag = 1;
                        bll.Model.InsertStaff = (int)Session["UserID"];
                        bll.Add();
                    }
                }
                BindGrid();
            }
        }
    }


    protected void bt_AddRole_Click(object sender, EventArgs e)
    {
        if (ddl_Role.SelectedValue != "")
        {
            Roles.AddUserToRole(lb_Username.Text, ddl_Role.SelectedValue);

            BindUserInRole();
        }
    }
    protected void bt_DeleteRole_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in cbx_UserInRoles.Items)
        {
            if (item.Selected)
            {
                Roles.RemoveUserFromRole(lb_Username.Text, item.Text);
            }
        }
        BindUserInRole();
    }
    protected void gv_MACAddrList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_MACAddrList.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_FindMacAddr_Click(object sender, EventArgs e)
    {
        gv_MACAddrList.DataSource = User_RegisterMACBLL.GetModelList("UserName='" + lb_Username.Text + "' AND MacAddr LIKE '"+tbx_MACAddr.Text+"%'");
        gv_MACAddrList.PageIndex = 0;
        gv_MACAddrList.DataBind();
    }
}
