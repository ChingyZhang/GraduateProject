using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using System.Web.Security;
using System.Web.Profile;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 来电弹出时，如已有窗口登录过，则不再登录
        //if (Request.QueryString["TeleNum"] != null)
        //{
        //    string Username = Server.UrlDecode(Request.Cookies["User"].Value);
        //    FormsAuthentication.SetAuthCookie(Server.UrlEncode(Username), false);
        //    int staffid = UserBLL.LoginSuccess(Username, Session.SessionID, Request.UserHostAddress);
        //    ProfileCommon profile = (ProfileCommon)ProfileBase.Create(Username);
        //    //员工登录
        //    profile.StaffID = staffid;
        //    profile.RealName = new Org_StaffBLL(staffid).Model.RealName;

        //    Session["UserID"] = staffid;
        //    Session["UserName"] = profile.UserName;
        //    Session["UserRealName"] = profile.RealName;
        //    Session["ActiveModule"] = 0;
        //    Session["AccountType"] = 1;     //账户类型 1：员工，2：商业客户

        //    string strTeleNum = Request.QueryString["TeleNum"];
        //    int id = MCSFramework.BLL.CU.CU_ClientBLL.CheckTeleNum(strTeleNum);
        //    Org_StaffBLL staffbll = new Org_StaffBLL(staffid);
        //    if (staffbll.Model.Position == 10 || staffbll.Model.Position == 16)
        //    {
        //        if (id > 0)
        //        {
        //            Response.Redirect("~/SubModule/CU/CustomerOverview.aspx?CustomerID=" + id.ToString());
        //        }
        //        else
        //        {
        //            Response.Redirect("~/SubModule/Service/SVA/SVA_ServiceAcceptDetail.aspx?TeleNum=" + strTeleNum);
        //        }
        //    }



        //}
        //else
        //{
        //    //
        //}
        #endregion
        Response.Redirect("~/SubModule/Login/index.aspx");
    }
}
