using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MCSControls.MCSTabControl;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.BLL.OA;
using MCSFramework.UD_Control;

public partial class SubModule_Default : System.Web.UI.Page
{
    protected DateTime _pageInitTime;//= DateTime.Now;           //用于计算页面生成时间
    protected void Page_Init(object sender, EventArgs e)
    {
        
        _pageInitTime = DateTime.Now;           //记录页面最初生成时间
        try
        {
            if (string.IsNullOrEmpty(Context.User.Identity.Name) || !Membership.GetUser().IsOnline || Session["UserID"] == null)
            {
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
        }
        catch
        {
            FormsAuthentication.SignOut();
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 注册脚本
        string script = "function searchkb() {";
        script += "window.open('" + Page.ResolveUrl("~/SubModule/OA/KB/Search.aspx") + "?Text=' + document.getElementById('tbx_KBSearch').value, '', 'Width=500,Height=600,status=yes,resizable=yes,scrollbars=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "searchkb", script, true);
        #endregion

        if (!IsPostBack)
        {
            //Response.Redirect("~/SubModule/PBM/Retailer/RetailerList.aspx");
            if (ConfigHelper.GetConfigBool("CheckRequstUrlReferrer"))
            {
                if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
                {
                    MessageBox.ShowAndRedirect(this.Page, "页面参数传递错误！", this.ResolveUrl("/PBM/Retailer/RetailerList.aspx"));//~/SubModule/Desktop.aspx
                    return;
                }
            }

            fr_onlineuser.Attributes["src"] = Page.ResolveUrl("~/SubModule/Login/onlineuser.aspx");
            fr_leftmenu.Attributes["src"] = Page.ResolveUrl("~/SubModule/LeftTreeMenu.aspx");

            string url = Request.QueryString["URL"];
            if (!string.IsNullOrEmpty(url))
            {
                url = Server.UrlDecode(url);
                fr_Main.Attributes["src"] = url;
            }
            else
            {
                fr_Main.Attributes["src"] = "/PBM/Retailer/RetailerList.aspx";//"desktop.aspx";
            }

            if (Session["UserName"] == null)
            {
                MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", "/PBM/Retailer/RetailerList.aspx");///Default.aspx
                return;
            }

            lb_UserName.Text = Session["UserRealName"].ToString();
            string softphoneurl = ConfigHelper.GetConfigString("SoftPhoneURL");
            if (!string.IsNullOrEmpty(softphoneurl))
            {
                //hy_OpenSoftPhone.NavigateUrl = softphoneurl;
                //hy_OpenSoftPhone.Visible = true;
            }
            #region

            #endregion


            string title = ConfigHelper.GetConfigString("PageTitle");
            if (!string.IsNullOrEmpty(title)) Head1.Title = title;

            body1.Attributes["onload"] = "GetServerTime();";
            NewMailCount();            
        }

        #region 设置页面Title 不能放在PostBack内
        string pagetitle = ConfigHelper.GetConfigString("PageTitle");
        if (!string.IsNullOrEmpty(pagetitle))
        {
            Head1.Title = pagetitle + " " + (string)ViewState["PageTitle"];
        }
        #endregion
    }

    protected void NewMailCount()
    {
        if (Session["UserName"] == null)
        {
            MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", "/Default.aspx");
            return;
        }
        else
        {
            string userName = Session["UserName"].ToString();
            ML_MailBLL bll = new ML_MailBLL();
            int count = bll.GetNewMailCountByReceiver(userName);
            //this.lb_NewMailCount.Text = count.ToString();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //页面生成完毕
        //TimeSpan ts = DateTime.Now - _pageInitTime;
        //lbPageResponseTime.Text = ts.Milliseconds.ToString();

    }
    protected void wc_SelectedNode(object sender, MCSControls.MCSWebControls.SelectedNodeEventArgs e)
    {
        Response.Redirect("~/SubModule/switch.aspx?Action=1&Module=" + e.NodeID.ToString());
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        UserBLL.Logout(Session["UserName"].ToString(), Session.SessionID);
        Response.Redirect("~/SubModule/Login/index.aspx");
    }
}