using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSControls.MCSTabControl;
using MCSFramework.BLL;

public partial class SubModule_StaffManage_RoleInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
            tr_Position.DataBind();
            tr_Position.SelectValue = "0";

            BindTree(tr_Role.Nodes);


        }
    }

    private void BindTree(TreeNodeCollection TNC)
    {
        TNC.Clear();
        string[] roles = Roles.GetAllRoles();

        foreach (string role in roles)
        {
            TreeNode tn = new TreeNode();
            tn.Text = role;
            tn.Value = role;
            TNC.Add(tn);
        }

        if (tr_Role.Nodes.Count > 0)
            tr_Role.Nodes[0].Selected = true;


        BindGrid();
    }

    protected void tr_Role_SelectedNodeChanged(object sender, EventArgs e)
    {
        //if (MCSTabControl1.SelectedIndex==0)
        tr_Position.SelectValue = "0";
        BindGrid();

    }

    protected void tr_Position_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            bt_In.Enabled = false;
            bt_Out.Enabled = true;
        }
        else
        {
            bt_In.Enabled = true;
            bt_Out.Enabled = false;
        }
        BindGrid();
    }
    private void BindGrid()
    {
        if (tr_Role.SelectedValue == "") return;
        lb_RoleName.Text = tr_Role.SelectedValue;

        string condition1= "";
        string condition2 = "";

        if (MCSTabControl1.SelectedIndex == 0)
            condition1 = "aspnet_Users.UserName IN (SELECT u.UserName  FROM aspnet_Users u, aspnet_UsersInRoles ur,aspnet_Roles r WHERE u.UserId = ur.UserId AND r.RoleId = ur.RoleId AND r.LoweredRoleName = LOWER('" + tr_Role.SelectedValue + "') )";
        else
            condition1 = "aspnet_Users.UserName NOT IN (SELECT u.UserName  FROM aspnet_Users u, aspnet_UsersInRoles ur,aspnet_Roles r WHERE u.UserId = ur.UserId AND r.RoleId = ur.RoleId AND r.LoweredRoleName = LOWER('" + tr_Role.SelectedValue + "') )";
        condition2 = condition1;

        if (tbx_Search.Text != "")
        {
            condition1 += " AND ( aspnet_Users.UserName LIKE '%" + tbx_Search.Text + "%' OR Org_Staff.RealName like '%" + tbx_Search.Text + "%')";
            condition2 += " AND ( aspnet_Users.UserName LIKE '%" + tbx_Search.Text + "%' OR CM_Client.FullName like '%" + tbx_Search.Text + "%')";
        }

        if (tr_Position.SelectValue != "0")
            condition1 += " AND Org_Staff.Position=" + tr_Position.SelectValue;

        condition1 += " AND Org_Staff.Dimission=1";
        condition2 += " AND CM_Client.ActiveFlag=1";

        gv_List.ConditionString = condition1;
        gv_List.BindGrid();

        gv_List2.ConditionString = condition2;
        gv_List2.BindGrid();

        cb_SelectAll.Checked = false;
    }

    #region 增加删除角色
    protected void bt_AddRole_Click(object sender, EventArgs e)
    {
        if (!Roles.RoleExists(tbx_RoleName.Text.Trim()))
        {
            Roles.CreateRole(tbx_RoleName.Text.Trim());
            BindTree(tr_Role.Nodes);
        }
        else
            MessageBox.Show(this, "对不起该角色已存在！");
    }
    protected void bt_DeleteRole_Click(object sender, EventArgs e)
    {
        if (tr_Role.SelectedValue == "") return;

        if (Roles.GetUsersInRole(tr_Role.SelectedValue).Length > 0)
        {
            lbl_AlertInfo.Text = "对不起，当前角色中有用户存在，不可删除！";
            return;
        }
        else
        {
            Roles.DeleteRole(tr_Role.SelectedValue);
        }

        BindTree(tr_Role.Nodes);
    }
    #endregion

    #region 移入移出角色
    protected void bt_In_Click(object sender, EventArgs e)
    {
        if (tr_Role.SelectedValue == "") return;
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                Roles.AddUserToRole(gv_List.DataKeys[row.RowIndex][0].ToString(), tr_Role.SelectedValue);
            }
        }

        foreach (GridViewRow row in gv_List2.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                Roles.AddUserToRole(gv_List2.DataKeys[row.RowIndex][0].ToString(), tr_Role.SelectedValue);
            }
        }
        BindGrid();
    }
    protected void bt_Out_Click(object sender, EventArgs e)
    {
        if (tr_Role.SelectedValue == "") return;
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                Roles.RemoveUserFromRole(gv_List.DataKeys[row.RowIndex][0].ToString(), tr_Role.SelectedValue);
            }
        }

        foreach (GridViewRow row in gv_List2.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                Roles.RemoveUserFromRole(gv_List2.DataKeys[row.RowIndex][0].ToString(), tr_Role.SelectedValue);
            }
        }
        BindGrid();
    }
    #endregion

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        gv_List2.PageIndex = 0;
        BindGrid();
    }
    protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            cb_check.Checked = cb_SelectAll.Checked;
        }

        foreach (GridViewRow row in gv_List2.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            cb_check.Checked = cb_SelectAll.Checked;
        }
    }
    protected void bt_Right_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Login/RightAssign.aspx?RoleName=" + tr_Role.SelectedValue);
    }
    protected void bt_UpdateRole_Click(object sender, EventArgs e)
    {
        if (tr_Role.SelectedValue == "") return;
        if (!Roles.RoleExists(tbx_RoleName.Text.Trim()))
        {
            MCSFramework.DBUtility.SQLDatabase.RunSQL("MCS_SYS_ConnectionString", "update mcs_sys.dbo.aspnet_Roles set RoleName='" + tbx_RoleName.Text.Trim() + "',LoweredRoleName='" + tbx_RoleName.Text.ToLower().Trim() + "' where RoleName='" + tr_Role.SelectedValue + "'", null, 90);
            MCSFramework.DBUtility.SQLDatabase.RunSQL("MCS_SYS_ConnectionString", "update mcs_sys.dbo.Right_Assign set RoleName='" + tbx_RoleName.Text.Trim() + "' where RoleName='" + tr_Role.SelectedValue + "'", null, 90);
            BindTree(tr_Role.Nodes);
        }
    }
}
