using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using System.Web.Security;
using MCSFramework.Model;


namespace MCSCCS.SubModule.Login
{
    /// <summary>
    /// index 的摘要说明。
    /// </summary>
    public partial class index : System.Web.UI.Page
    {
        public string RandData;


        #region 页面初始化函数
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!Page.IsPostBack)
            {
                if (Request.Browser.Browser != "IE")
                {
                    //MessageBox.Show(this, "为正常使用本系统，建议您使用IE7以上的浏览器！");
                }
                else
                {
                    switch (Request.Browser.Version)
                    {
                        case "6.0":
                            MessageBox.Show(this, "您的浏览器为IE6，为正常使用本系统，请升级您的浏览器至IE7或IE8！");
                            break;
                        case "9.0":
                            MessageBox.Show(this, "您的浏览器为IE9，为正常使用本系统，请启用浏览器的兼容模式！");
                            break;
                        case "10.0":
                            MessageBox.Show(this, "您的浏览器为IE10，为正常使用本系统，请启用浏览器的兼容模式！");
                            break;
                        case "7.0":
                        case "8.0":
                            break;
                    }
                }
                lb_BrowserVersion.Text = "浏览器版本: " + Request.Browser.Browser + " " + Request.Browser.Version;

                ViewState["MacAddr"] = "";

                string title = ConfigHelper.GetConfigString("PageTitle");
                if (!string.IsNullOrEmpty(title))
                {
                    Head1.Title = title;
                }

                Session.Abandon();
                this.Login1.UserName = Request.Cookies["User"] == null ? "" : Server.UrlDecode(Request.Cookies["User"].Value);
                #region 来电弹出处理
                //this.txtUsername.Text = Request.Cookies["Username"] != null ? Request.Cookies["UserName"].Value.ToString() : "";

                //#region 来电弹出时，如已有窗口登录过，则不再登录
                //if (Request.QueryString["InComeTeleNum"] != null && Request.Cookies["Username"] != null)
                //{
                //    //判断用户是否在线
                //    string Username = Request.Cookies["UserName"].Value;
                //    string ReturnStr = SMS.CheckUpdate(Username, "", 0);
                //    string ReturnID = ReturnStr.Substring(0, ReturnStr.IndexOf("|"));
                //    if (ReturnID != "-1")
                //    {
                //        string strTeleNum = Request.QueryString["InComeTeleNum"];
                //        string strUserID = Request.Cookies["UserID"].Value;
                //        FormsAuthentication.SetAuthCookie(strUserID, false);        //通过验证

                //        Session["UserID"] = strUserID;
                //        Session["UserName"] = Username;
                //        Session["UserRealName"] = Staff.GetRealNameByUsername(Username);

                //        Response.Cookies["InComeTeleNum"].Value = strTeleNum;
                //        Response.Redirect("~/SubModule/UnitiveDocument/desktop.aspx");
                //    }
                //}
                //#endregion
                //this.Login1.Focus();
                #endregion
            }
        }
        #endregion

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            int staffid = UserBLL.LoginSuccess(Login1.UserName, Session.SessionID, Request.UserHostAddress, ViewState["MacAddr"].ToString());

            if (staffid > 0)
            {
                //员工登录
                Org_Staff staff = new Org_StaffBLL(staffid).Model;
                if (staff != null)
                {
                    Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                Session["UserID"] = staffid;
                    Session["UserName"] = Login1.UserName;
                    Session["UserRealName"] = staff.RealName;
                Session["ActiveModule"] = 0;
                    Session["AccountType"] = 1;     //账户类型 1：员工，2：商业客户 3:导购
                    Session["ClientType"] = 0;
                    Session["OrganizeCity"] = staff.OrganizeCity;
                }
                else
                    Response.Redirect("index.aspx");
            }
            else
            {
                int clientid = UserBLL.GetClientIDByUsername(Login1.UserName);
                if (clientid > 0)
                {
                //商业客户登录
                MCSFramework.BLL.CM.CM_ClientBLL _c = new MCSFramework.BLL.CM.CM_ClientBLL(clientid);
                if (_c.Model != null)
                {
                        Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                        Session["UserID"] = clientid;
                        Session["UserName"] = Login1.UserName;
                        Session["UserRealName"] = _c.Model.FullName;
                        Session["ActiveModule"] = 0;
                        Session["AccountType"] = 2;     //账户类型 1：员工，2：商业客户 3:导购
                        Session["ClientType"] = _c.Model != null ? _c.Model.ClientType : 0; //商业客户类别
                        Session["OrganizeCity"] = _c.Model.OrganizeCity;
                }
                    else
                        Response.Redirect("index.aspx");

                }
                else
                {
                    int promotorid = UserBLL.GetPromotorIDByUsername(Login1.UserName);
                    if (promotorid > 0)
                    {
                        //导购登录
                        MCSFramework.BLL.Promotor.PM_PromotorBLL _p = new MCSFramework.BLL.Promotor.PM_PromotorBLL(promotorid);
                        if (_p.Model != null)
                        {
                            Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                Session["UserID"] = clientid;
                            Session["UserName"] = Login1.UserName;
                            Session["UserRealName"] = _p.Model.Name;
                Session["ActiveModule"] = 0;
                            Session["AccountType"] = 3;     //账户类型 1：员工，2：商业客户 3:导购
                            Session["OrganizeCity"] = _p.Model.OrganizeCity;
                        }
                        else
                            Response.Redirect("index.aspx");
                    }
                }
            }


            Response.Cookies["User"].Value = Server.UrlEncode(Login1.UserName);
            Response.Cookies["User"].Expires = new DateTime(2101, 1, 1);
            //去除用户权限缓存      
            string CacheKey = "Right-AssignedRightList-" + Login1.UserName;
            DataCache.RemoveCache(CacheKey);
        }
        protected void Login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            TextBox tbx_VerifyCode = (TextBox)Login1.FindControl("tbx_VerifyCode");
            if (tbx_VerifyCode != null)
            {
                if (Session["ImageVerifyCode"] != null && tbx_VerifyCode.Text.Trim().ToUpper() != Session["ImageVerifyCode"].ToString().ToUpper())
                {
                    MessageBox.Show(this, "验证码输入不正确!");
                    e.Cancel = true;
                    return;
                }
            }

            //ViewState["MacAddr"] = Request.Form["tbx_MacAddr"];
            
            //if (Membership.FindUsersByName(Login1.UserName).Count == 0)
            //{
            //    MessageBox.Show(this, "对不起，用户名不存在!");
            //    return;
            //}

            //if (!Right_Assign_BLL.GetAccessRight(Login1.UserName, 1, "LoginMACUnLimited"))
            //{
            //    int Flag = 1;      //0:只有注册在自己名下的MAC才可登录，1:只要在系统中注册的MAC都可以登录

            //    if (Right_Assign_BLL.GetAccessRight(Login1.UserName, 1, "LoginMACSelfLimiited")) Flag = 0;

            //    string macaddr = Request.Form["tbx_MacAddr"];// tbx_MacAddr.Text;

            //    if (!User_RegisterMACBLL.CanLogin(Login1.UserName, ref macaddr, Flag))
            //    {
            //        MessageBox.ShowAndRedirect(this, "对不起，因系统安全性设定，您的计算机MAC地址受到登录限制，不能登录该系统，请联系系统管理员！" + macaddr, "index.aspx");
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        ViewState["MacAddr"] = macaddr;
            //    }
            //}
        }
    }
}
