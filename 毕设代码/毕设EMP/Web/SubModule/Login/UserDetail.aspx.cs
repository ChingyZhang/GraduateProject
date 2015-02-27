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
            else
            {
                CM_Client client = new CM_ClientBLL(UserBLL.GetClientIDByUsername(user.UserName)).Model;
                if (client != null)
                {
                    //商业客户
                    select_Client.SelectValue = client.ID.ToString();
                    select_Client.SelectText = client.FullName;
                }
                else
                {
                    //导购
                    MCSFramework.Model.Promotor.PM_Promotor p = 
                        new MCSFramework.BLL.Promotor.PM_PromotorBLL(UserBLL.GetPromotorIDByUsername(user.UserName)).Model;
                    if (p != null)
                    {
                        select_Promotor.SelectValue = p.ID.ToString();
                        select_Promotor.SelectText = p.Name;
                    }
                }
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
        if ((select_Staff.SelectValue == "" || select_Staff.SelectValue == "0") &&
            (select_Client.SelectValue == "" || select_Client.SelectValue == "0") &&
            (select_Promotor.SelectValue == "" || select_Promotor.SelectValue == "0"))
        {
            MessageBox.Show(this, "请正确选择关联的公司员工或商业客户或导购！");
            return;
        }

        if (select_Staff.SelectValue != "" && select_Client.SelectValue != "" && select_Promotor.SelectValue != "")
        {
            MessageBox.Show(this, "在公司员工及商业客户间或导购，用户名只能关联其中一个！");
            return;
        }

        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        if (select_Staff.SelectValue != "")
            UserBLL.Membership_SetStaffID(user.UserName, int.Parse(select_Staff.SelectValue));
        else if (select_Client.SelectValue != "")
            UserBLL.Membership_SetClientID(user.UserName, int.Parse(select_Client.SelectValue));
        else if (select_Promotor.SelectValue != "")
            UserBLL.Membership_SetPromotorID(user.UserName, int.Parse(select_Promotor.SelectValue));

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
        if (user.IsLockedOut)
        {
            MessageBox.Show(this, "请先解锁该帐户再修改密码!");
            return;
        }
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
}
