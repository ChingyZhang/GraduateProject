using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using System.Web.Security;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using System.Security.Cryptography;
using MCSFramework.Common;

public partial class SubModule_Login_CreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 300203, "LoginAssign"))
            //    Response.Redirect("~/SubModule/desktop.aspx");
        }
    }

    protected void CreateUserWizard1_CancelButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("DIUserList.aspx");
    }
    
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_DIMembershipBLL ship = new CM_DIMembershipBLL();
        if (CM_DIMembershipBLL.GetByUserName(UserName.Text.Trim()).Rows.Count > 0)
        {
            MessageBox.Show(this, "用户名重复，请重新键入其他用户名！");
            return;
        }
        if (select_Client.SelectValue == "" || select_Client.SelectValue == "0")
        {
            MessageBox.Show(this, "请选择账号所属经销商!");
            return;
        }
        ship.Model.UserName = UserName.Text.Trim();
        ship.Model.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text, "md5");
        ship.Model.IsApproved = 1;
        ship.Model.IsLockedOut = 0;
        int shipid = ship.Add();
        if (shipid > 0)
        {
            CM_DIUsersBLL BLL = new CM_DIUsersBLL();
            BLL.Model.Client = int.Parse(select_Client.SelectValue);
            BLL.Model.ShipID = shipid;
            if (BLL.Add() > 0)
                MessageBox.ShowAndRedirect(this, "创建成功!", "DIUserList.aspx");
        }
        else
            MessageBox.Show(this, "账号创建出错，请联系管理员！");
    }
    protected void bt_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("DIUserList.aspx");
    }
}
