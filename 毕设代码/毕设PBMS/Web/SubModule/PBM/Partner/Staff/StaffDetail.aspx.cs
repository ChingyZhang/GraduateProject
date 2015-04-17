using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSControls.MCSWebControls;
using System.Data;
using System.Web.Security;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.VST;

public partial class SubModule_PBM_Partner_Staff_StaffDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                bt_CreateUser.Visible = false;
                pl_RoleList.Visible = false;
                pl_RoleList.Visible = false;
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        MCSTreeControl tr_Position = (MCSTreeControl)pl_detail.FindControl("Org_Staff_Position");
        if (tr_Position != null)
        {
            tr_Position.RootValue = ConfigHelper.GetConfigInt("TDP-MainPosition").ToString();
        }

        foreach (string rolename in Roles.GetAllRoles().Where(p => p.StartsWith("TDP")))
        {
            cbx_Roles.Items.Add(new ListItem(rolename, rolename));
        }

    }
    #endregion

    private void BindData()
    {
        Org_Staff m = new Org_StaffBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            if (m.OwnerClient != (int)Session["OwnerClient"])
            {
                Response.Redirect("~/SubModule/Desktop.aspx");
            }

            pl_detail.BindData(m);

            if (m.Position == ConfigHelper.GetConfigInt("TDP-MainPosition"))
            {
                pl_detail.SetControlsEnable(false);
                bt_CreateUser.Visible = false;
                bt_OK.Visible = false;

                bt_OnApprove.Enabled = false;
                bt_UnApprove.Enabled = false;
                bt_ResetPwd.Enabled = false;
            }

            if (m.aspnetUserId != null && m.aspnetUserId != Guid.Empty)
            {
                bt_CreateUser.Visible = false;

                MembershipUser user = Membership.GetUser(m.aspnetUserId);
                if (user != null)
                {
                    lb_Username.Text = user.UserName;

                    #region 绑定所属角色
                    foreach (string rolename in Roles.GetRolesForUser(user.UserName))
                    {
                        ListItem item = cbx_Roles.Items.FindByValue(rolename);
                        if (item != null)
                        {
                            item.Selected = true;
                            item.Text = "<b>" + item.Text + "</b>";
                        }
                    }
                    #endregion

                    #region 绑定用户状态
                    bt_Unlock.Visible = user.IsLockedOut;
                    bt_UnApprove.Visible = user.IsApproved;
                    bt_OnApprove.Visible = !user.IsApproved;
                    #endregion
                }

            }
            else
            {
                pl_RoleList.Visible = false;
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Org_StaffBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new Org_StaffBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Org_StaffBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.Position == 0 || _bll.Model.Position == 1000)
        {
            MessageBox.Show(this, "请选择员工所属的职位!");
            return;
        }
        #endregion

        #region 判断在职标志
        if (_bll.Model.Dimission == 1)
        {
            if (_bll.Model.BeginWorkTime.Year == 1900) _bll.Model.BeginWorkTime = DateTime.Today;
            if (_bll.Model.EndWorkTime.Year != 1900) _bll.Model.EndWorkTime = new DateTime(1900, 1, 1);
        }
        else if (_bll.Model.Dimission == 2)
        {
            if (_bll.Model.EndWorkTime.Year == 1900) _bll.Model.EndWorkTime = DateTime.Today;
        }
        #endregion

        #region 判断手机号码有效性
        if (_bll.Model.Mobile.StartsWith("01")) _bll.Model.Mobile = _bll.Model.Mobile.Substring(1);
        if (Org_StaffBLL.GetStaffList("Dimission=1 AND Mobile='" + _bll.Model.Mobile + "' AND ID <> " + _bll.Model.ID.ToString()).Count > 0)
        {
            MessageBox.Show(this, "手机号码和系统中其他员工的手机号码重复,请检查是否正确!");
            return;
        }
        #endregion


        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                #region 创建默认职位为业务员的线路
                if (_bll.Model.Position == 1030 && VST_RouteBLL.GetRouteListByStaff(_bll.Model.ID).Count == 0)
                {
                    CreateRoute(_bll.Model.RealName, _bll.Model.OrganizeCity);
                }
                #endregion

                MessageBox.ShowAndRedirect(this, "保存成功!", "StaffDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
        else
        {
            //新增

            CM_ClientBLL c = new CM_ClientBLL((int)Session["OwnerClient"]);
            CM_ClientManufactInfo manufactinfo = c.GetManufactInfo();
            if (c != null && manufactinfo != null)
            {
                _bll.Model.OrganizeCity = manufactinfo.OrganizeCity;
                _bll.Model.OfficialCity = c.Model.OfficialCity;
            }

            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.OwnerClient = (int)Session["OwnerClient"];
            _bll.Model.OwnerType = (int)Session["OwnerType"];
            if (_bll.Model.Dimission == 0) _bll.Model.Dimission = 1;

            _bll.Model.ApproveFlag = 1;
            ViewState["ID"] = _bll.Add();

            if ((int)ViewState["ID"] > 0)
            {
                #region 创建默认职位为业务员的线路
                if (_bll.Model.Position == 1030)
                {
                    CreateRoute(_bll.Model.RealName, _bll.Model.OrganizeCity);
                }
                #endregion

                Response.Redirect("StaffDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }

    }

    private void CreateRoute(string name, int organizecity)
    {
        VST_RouteBLL routebll = new VST_RouteBLL();
        routebll.Model.Code = "R" + ViewState["ID"].ToString();
        routebll.Model.Name = "线路-" + name;
        routebll.Model.RelateStaff = (int)ViewState["ID"];
        routebll.Model.OrganizeCity = organizecity;
        routebll.Model.OwnerClient = (int)Session["OwnerClient"];
        routebll.Model.OwnerType = (int)Session["OwnerType"];
        routebll.Model.ApproveFlag = 1;
        routebll.Model.EnableFlag = "Y";
        routebll.Model.InsertStaff = (int)Session["UserID"];
        routebll.Add();
    }

    protected void bt_SetUserInRole_Click(object sender, EventArgs e)
    {
        Org_Staff m = new Org_StaffBLL((int)ViewState["ID"]).Model;
        if (m == null) return;
        MembershipUser u = Membership.GetUser(m.aspnetUserId);
        if (u == null) return;

        if (cbx_Roles.SelectedIndex == -1)
        {
            MessageBox.Show(this, "至少要选择该员工关联的角色!");
            return;
        }

        string[] CurrentUserInRoles = Roles.GetRolesForUser(u.UserName);
        foreach (ListItem item in cbx_Roles.Items)
        {
            if (item.Selected)
            {
                if (!Roles.IsUserInRole(u.UserName, item.Value)) Roles.AddUserToRole(u.UserName, item.Value);
            }
            else
            {
                if (Roles.IsUserInRole(u.UserName, item.Value)) Roles.RemoveUserFromRole(u.UserName, item.Value);
            }
        }
        BindData();
        MessageBox.Show(this, "操作成功!");
    }

    protected void bt_Unlock_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.UnlockUser();
        Membership.UpdateUser(user);
        BindData();
    }
    protected void bt_OnApprove_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.IsApproved = true;

        Membership.UpdateUser(user);
        Response.Redirect("StaffList.aspx");
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(lb_Username.Text, false);
        user.IsApproved = false;

        Membership.UpdateUser(user);
        Response.Redirect("StaffList.aspx");
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

        MessageBox.ShowAndRedirect(this, "密码重置成功!", "StaffList.aspx");
    }


    protected void bt_DeleteUser_Click(object sender, EventArgs e)
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)ViewState["ID"]);
        if (staff.Model == null) return;

        Membership.DeleteUser(lb_Username.Text, true);

        staff.Model.aspnetUserId = Guid.Empty;
        staff.Update();
        Response.Redirect("StaffList.aspx");
    }

    protected void bt_ResetPwd_Click(object sender, EventArgs e)
    {
        tr_resetpwd.Visible = true;
    }
    protected void bt_CreateUser_Click(object sender, EventArgs e)
    {
        string UserName = "", Password = "", PositionName = "";

        Org_StaffBLL staff = new Org_StaffBLL((int)ViewState["ID"]);
        if (staff.Model == null) return;
        if (staff.Model.aspnetUserId != null && staff.Model.aspnetUserId != Guid.Empty) return;

        PositionName = new Org_PositionBLL(staff.Model.Position).Model.Name;

        CM_ClientBLL tdp = new CM_ClientBLL((int)Session["OwnerClient"]);
        CM_ClientManufactInfo manufactinfo = tdp.GetManufactInfo(tdp.Model.OwnerClient);
        if (tdp.Model == null) return;

        //用户名默认为当前:TDP代码-员工姓名
        if (manufactinfo != null && !string.IsNullOrEmpty(manufactinfo.Code))
            UserName = manufactinfo.Code;
        else if (!string.IsNullOrEmpty(tdp.Model.Code))
            UserName = tdp.Model.Code;
        else
            UserName = tdp.Model.ID.ToString();
        UserName += "-" + staff.Model.RealName;

        if (Membership.GetUser(UserName) != null) UserName += staff.Model.ID.ToString();

        //密码默认为员工手机号码，为空时则为123456
        Password = staff.Model.Mobile.Trim();
        if (Password == "") Password = "123456";

        //创建用户，并加入角色        
        MembershipUser u = Membership.CreateUser(UserName, Password);
        foreach (string rolename in Roles.GetAllRoles().Where(p => p.StartsWith("TDP")))
        {
            if (rolename == PositionName)
            {
                Roles.AddUserToRole(u.UserName, rolename);
            }
        }

        UserBLL.Membership_SetStaffID(u.UserName, staff.Model.ID);
        staff.Model.aspnetUserId = (Guid)u.ProviderUserKey;
        staff.Update();
        
        MessageBox.ShowAndRedirect(this, "用户创建成功！【用户名：" + UserName + ",初始密码:" + Password + "】", "StaffList.aspx");
    }
}