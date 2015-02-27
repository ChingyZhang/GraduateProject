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
            if (Request.QueryString["ShipID"] != null)
            {
                ViewState["ShipID"] = int.Parse(Request.QueryString["ShipID"]);
                BindData();
                BindGrid();
            }
        }
    }

    private void BindData()
    {
        if ((int)ViewState["ShipID"] > 0)
        {
            CM_DIMembershipBLL shipbll = new CM_DIMembershipBLL((int)ViewState["ShipID"]);
            lb_Username.Text = shipbll.Model.UserName;
            lb_IsApproved.Text = shipbll.Model.IsApproved == 1 ? "启用" : "停用";
            lb_IsLockedOut.Text = shipbll.Model.IsLockedOut == 1 ? "已锁定" : "未锁定";
            bt_Approve.Visible = !(shipbll.Model.IsApproved == 1);
            bt_UnApprove.Visible = (shipbll.Model.IsApproved == 1);
            bt_Unlock.Visible = shipbll.Model.IsLockedOut == 1;
        }
        else
        {
            MessageBox.ShowAndRedirect(this, "用户名不存在!", "UserList.aspx");
        }
    }

    private void BindGrid()
    {
        gv_DI.ConditionString = "CM_DIUsers.ShipID=" + ViewState["ShipID"];
        gv_DI.BindGrid();
    }

    protected void bt_Unlock_Click(object sender, EventArgs e)
    {
        CM_DIMembershipBLL shipbll = new CM_DIMembershipBLL((int)ViewState["ShipID"]);
        shipbll.Model.IsLockedOut = 0;
        shipbll.Update();
        BindData();
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        CM_DIMembershipBLL shipbll = new CM_DIMembershipBLL((int)ViewState["ShipID"]);
        shipbll.Model.IsApproved = 1;
        shipbll.Update();
        BindData();
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        CM_DIMembershipBLL shipbll = new CM_DIMembershipBLL((int)ViewState["ShipID"]);
        shipbll.Model.IsApproved = 0;
        shipbll.Update();
        BindData();
        Response.Redirect("DIUserList.aspx");
    }
    protected void bt_SaveResetPwd_Click(object sender, EventArgs e)
    {
        if (tbx_ConfirmPwd.Text.Length < 6)
        {
            MessageBox.Show(this, "密码长度必需大于6!");
            return;
        }

        CM_DIMembershipBLL shipbll = new CM_DIMembershipBLL((int)ViewState["ShipID"]);
        if (shipbll.Model.IsLockedOut==1)
        {
            MessageBox.Show(this, "请先解锁该帐户再修改密码!");
            return;
        }

        string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tbx_Pwd.Text, "md5");//MCSFramework.Common.DataEncrypter.EncrypteString(tbx_Pwd.Text);
        shipbll.Model.Password = pwd;
        if (shipbll.Update()<0)
        {
            MessageBox.Show(this, "输入的密码无效,密码修改失败!");
            return;
        }
        else
            MessageBox.ShowAndRedirect(this, "密码重置成功!", "DIUserList.aspx");
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

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "" && select_Client.SelectValue != "0")
        {
            int client = int.Parse(select_Client.SelectValue);
            if (CM_DIUsersBLL.GetModelList("ShipID=" + ViewState["ShipID"] + " AND Client=" + client).Count > 0)
            {
                MessageBox.Show(this,"该经销商已添加!");
                return;
            }
            CM_DIUsersBLL user = new CM_DIUsersBLL();
            user.Model.Client = client;
            user.Model.ShipID = (int)ViewState["ShipID"];
            if (user.Add() > 0)
                BindGrid();
            else MessageBox.Show(this, "添加失败！");
        }
    }
    protected void gv_DI_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CM_DIUsersBLL user = new CM_DIUsersBLL();
        user.Delete((int)gv_DI.DataKeys[e.RowIndex][0]);
        BindGrid();
    }
}
