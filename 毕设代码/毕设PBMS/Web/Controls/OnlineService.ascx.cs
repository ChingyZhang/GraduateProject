using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_OnlineService : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hy_OnlineService.NavigateUrl = "http://wpa.qq.com/msgrd?v=3&uin=" + MCSFramework.Common.ConfigHelper.GetConfigString("OnlineServiceQQ") + "&site=qq&menu=yes";
            //Image1.ImageUrl = "http://wpa.qq.com/pa?p=2:" + MCSFramework.Common.ConfigHelper.GetConfigString("OnlineServiceQQ") + ":53";
        }
    }
}