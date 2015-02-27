using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using System.Security.Cryptography;
using MCSFramework.Common;

public partial class SubModule_Login_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ClientUserID"] == null)
            Response.Redirect("~/SubMoudle/desktop.aspx");
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_DIUsersBLL user = new CM_DIUsersBLL((int)Session["ClientUserID"]);
        CM_DIMembershipBLL ship = new CM_DIMembershipBLL(user.Model.ShipID);
        if (!DataEncrypter.Compare(ship.Model.Password,tbx_OrgPassword.Text))
            Literal1.Text = "原密码输入错误！";
        else
        {
            ship.Model.Password = DataEncrypter.EncrypteString(tbx_NewPassword.Text);
            ship.Update();
            Literal1.Text = "";
            tbx_NewPassword.Text = "";
            tbx_OrgPassword.Text = "";
            tbx_RePassword.Text = "";
            MessageBox.Show(this, "密码修改成功!");
        }

       
    }
}
