using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_Login_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ChangePassword1_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/desktop.aspx");
    }
}
