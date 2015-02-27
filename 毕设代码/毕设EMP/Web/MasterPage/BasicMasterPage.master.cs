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

public partial class MasterPage_BasicMasterPage : System.Web.UI.MasterPage
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
        Page.ClientScript.RegisterClientScriptInclude("Cookie", Page.ResolveClientUrl("~/App_Themes/basic/cookie.js"));
        Page.ClientScript.RegisterClientScriptInclude("meizzDate", Page.ResolveClientUrl("~/App_Themes/basic/meizzDate.js"));
        Page.ClientScript.RegisterClientScriptInclude("ShowLeftColMenu", Page.ResolveClientUrl("~/App_Themes/basic/ShowLeftColMenu.js"));
        Page.ClientScript.RegisterClientScriptInclude("MCSTabMenu", Page.ResolveClientUrl("~/App_Themes/basic/MCSTabMenu.js"));

        string script = "function searchkb() {";
        script += "window.open('" + Page.ResolveUrl("~/SubModule/OA/KB/Search.aspx") + "?Text=' + document.getElementById('tbx_KBSearch').value, '', 'Width=500,Height=600,status=yes,resizable=yes,scrollbars=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "searchkb", script, true);
        #endregion

        if (!IsPostBack)
        {
            if (ConfigHelper.GetConfigBool("CheckRequstUrlReferrer"))
            {
                if (Request.UrlReferrer == null || Request.UrlReferrer.Host != Request.Url.Host)
                {
                    MessageBox.ShowAndRedirect(this.Page, "页面参数传递错误！", this.ResolveUrl("~/SubModule/Desktop.aspx"));
                    return;
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "script1", "HideLeftCol('leftCol');", true);
            fr_onlineuser.Attributes["src"] = Page.ResolveUrl("~/SubModule/Login/onlineuser.aspx");
            fr_leftmenu.Attributes["src"] = Page.ResolveUrl("~/SubModule/LeftTreeMenu.aspx");
            if (Session["UserName"] == null)
            {
                MessageBox.ShowAndRedirect(this.Page, "对不起，会话超时，请重新登录!", "/Default.aspx");
                return;
            }

            lb_UserName.Text = Session["UserRealName"].ToString();
            string softphoneurl = ConfigHelper.GetConfigString("SoftPhoneURL");
            if (!string.IsNullOrEmpty(softphoneurl))
            {
                hy_OpenSoftPhone.NavigateUrl = softphoneurl;
                hy_OpenSoftPhone.Visible = true;
            }
            #region
            if (new WT_WorkTimeBLL().IsBeginWork(int.Parse(Session["UserID"].ToString())))
            {
                this.bt_On.Visible = false;
            }
            else
            {
                this.bt_Out.Visible = false;
            }
            #endregion

            if (Request.QueryString["ViewFramework"] != null)
            {
                tr_Head.Visible = false;
                tr_Bottom.Visible = false;
                tr_Menu.Visible = false;
                tr_Body.Height = "300px";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script1", "HideLeftCol('leftCol');", true);
                HideHandle.Width = 0;
            }
            else
            {
                string title = ConfigHelper.GetConfigString("PageTitle");
                if (!string.IsNullOrEmpty(title)) Head1.Title = title;

                NewMailCount();
            }

            #region 通用权限判断
            ViewState["PageTitle"] = "";
            if (Request.FilePath.IndexOf("SubModule") >= 0)
            {
                string Path = Request.FilePath.Substring(Request.FilePath.IndexOf("SubModule"));

                IList<UD_WebPage> pages = UD_WebPageBLL.GetModelList("Path='" + Path + "' AND SubCode='" + Head1.Attributes["WebPageSubCode"] + "'");
                if (pages.Count > 0)
                {
                    UD_WebPageBLL webpagebll = new UD_WebPageBLL(pages[0].ID, true);

                    if (!Right_Assign_BLL.GetAccessRight(Context.User.Identity.Name, webpagebll.Model.Module, "Browse"))
                    {
                        //无浏览权限
                        //Response.Redirect("~/SubModule/noaccessright.aspx");
                    }

                    IList<UD_WebPageControl> controls = webpagebll.GetWebControls();
                    ViewState["PageTitle"] = pages[0].Title;

                    if (controls.Count > 0)
                    {
                        CheckWebControl(ContentPlaceHolder1.Controls, webpagebll.Model.Module, controls);
                    }
                    else
                    {
                        foreach (Control c in ContentPlaceHolder1.Controls)
                        {
                            if (!string.IsNullOrEmpty(c.ID))
                            {
                                if (c.ID == "lb_PageTitle")
                                {
                                    ((Label)c).Text = (string)ViewState["PageTitle"];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }
        else
        {
            #region 如果页面内含MCSTabControl控件，再次运行一下权限判断
            if (ViewState["AlwaysCheckRight"] != null && (bool)ViewState["AlwaysCheckRight"])
            {
                string Path = Request.FilePath.Substring(Request.FilePath.IndexOf("SubModule"));

                IList<UD_WebPage> pages = UD_WebPageBLL.GetModelList("Path='" + Path + "' AND SubCode='" + Head1.Attributes["WebPageSubCode"] + "'");
                if (pages.Count > 0)
                {
                    UD_WebPageBLL webpagebll = new UD_WebPageBLL(pages[0].ID, true);

                    IList<UD_WebPageControl> controls = webpagebll.GetWebControls();

                    if (controls.Count > 0)
                    {
                        CheckWebControl(ContentPlaceHolder1.Controls, webpagebll.Model.Module, controls);
                    }
                }
            }
            #endregion
        }

        #region 设置页面Title 不能放在PostBack内
        string pagetitle = ConfigHelper.GetConfigString("PageTitle");
        if (!string.IsNullOrEmpty(pagetitle))
        {
            Head1.Title = pagetitle + " " + (string)ViewState["PageTitle"];
        }
        #endregion

    }

    #region 判断页面控件权限
    /// <summary>
    /// 判断页面控件权限
    /// </summary>
    /// <param name="Controls"></param>
    /// <param name="Module"></param>
    /// <param name="CheckControlList"></param>
    private void CheckWebControl(ControlCollection Controls, int Module, IList<UD_WebPageControl> CheckControlList)
    {
        foreach (Control c in Controls)
        {
            if (!string.IsNullOrEmpty(c.ID))
            {
                if (c.ID == "lb_PageTitle") ((Label)c).Text = (string)ViewState["PageTitle"];

                if (CheckControlList != null)
                {
                    foreach (UD_WebPageControl m in CheckControlList.Where<UD_WebPageControl>(cl => cl.ControlName == c.ID))
                    {
                        #region 获取定义的权限
                        bool visible = true;
                        bool enable = true;
                        if (m.VisibleActionCode != "")
                            visible = Right_Assign_BLL.GetAccessRight(Context.User.Identity.Name, Module, m.VisibleActionCode);

                        if (m.EnableActionCode != "")
                            enable = Right_Assign_BLL.GetAccessRight(Context.User.Identity.Name, Module, m.EnableActionCode);

                        #endregion

                        #region 根据控件类型判断控件的可见与有效
                        switch (c.GetType().BaseType.Name)
                        {
                            case "WebControl":
                                switch (m.ControlType)
                                {
                                    case "MCSTabControl":
                                        #region MCSTabControl
                                        MCSTabControl tab = (MCSTabControl)c;
                                        if (m.ControlIndex == 0)
                                        {
                                            if (!visible) tab.Visible = false;
                                            if (!enable) tab.Enabled = false;
                                        }
                                        else
                                        {
                                            if (m.ControlIndex > 0 && m.ControlIndex <= tab.Items.Count)
                                            {
                                                if (!visible) tab.Items[m.ControlIndex - 1].Visible = false;
                                                if (!enable) tab.Items[m.ControlIndex - 1].Enabled = false;

                                                ViewState["AlwaysCheckRight"] = true;
                                            }
                                        }
                                        #endregion
                                        break;
                                    default:
                                        WebControl wc = (WebControl)c;
                                        if (!visible) wc.Visible = false;
                                        if (!enable) wc.Enabled = false;

                                        break;
                                }
                                break;
                            case "GridView":                        //UC_GridView控件
                            case "CompositeDataBoundControl":       //GridView控件
                                #region GridView
                                if (m.ControlType == "GridView" || m.ControlType == "UC_GridView")
                                {
                                    GridView gv = (GridView)c;
                                    if (m.ControlIndex == 0)
                                    {
                                        if (!visible) gv.Visible = false;
                                        if (!enable) gv.Enabled = false;
                                    }
                                    else
                                    {
                                        if (m.ControlIndex > 0 && m.ControlIndex <= gv.Columns.Count)
                                        {
                                            if (!visible) gv.Columns[m.ControlIndex - 1].Visible = false;
                                        }
                                    }
                                    //ViewState["AlwaysCheckRight"] = true;
                                }
                                #endregion
                                break;
                            case "CheckBox":
                                if (m.ControlType == "RadioButton")
                                {
                                    RadioButton rb = (RadioButton)c;
                                    if (!visible) rb.Visible = false;
                                    if (!enable) rb.Enabled = false;
                                }
                                break;
                            case "Image":
                                if (m.ControlType == "ImageButton")
                                {
                                    ImageButton imb = (ImageButton)c;
                                    if (!visible) imb.Visible = false;
                                    if (!enable) imb.Enabled = false;
                                }
                                break;
                            case "ListControl":
                                //包括DropDownList RadioButtonList控件
                                ListControl lc = (ListControl)c;
                                if (m.ControlIndex == 0)
                                {
                                    if (!visible) lc.Visible = false;
                                    if (!enable) lc.Enabled = false;
                                }
                                else
                                {
                                    if (m.ControlIndex > 0 && m.ControlIndex <= lc.Items.Count)
                                    {
                                        if (!visible || !enable) lc.Items[m.ControlIndex - 1].Enabled = false;
                                    }
                                }
                                break;
                            case "Panel":
                                if (c.GetType().Name == "UC_DetailView")
                                {
                                    UC_DetailView uc_view = (UC_DetailView)c;
                                    if (m.Description == "")
                                    {
                                        if (!visible) uc_view.Visible = false;
                                        if (!enable) uc_view.SetControlsEnable(false);
                                    }
                                    else
                                    {
                                        if (!visible) uc_view.SetPanelVisible(m.Description, false);
                                        if (!enable) uc_view.SetPanelEnable(m.Description, false);
                                    }
                                }
                                else if (c.GetType().Name == "")
                                {
                                    UC_EWFPanel uc_view = (UC_EWFPanel)c;
                                    if (!visible) uc_view.Visible = false;
                                    if (!enable) uc_view.Enabled = false;
                                }
                                else
                                {
                                    Panel pl = (Panel)c;
                                    if (!visible) pl.Visible = false;
                                    if (!enable) pl.Enabled = false;
                                }
                                break;
                            default:
                                if (!visible) c.Visible = false;
                                break;
                        }
                        #endregion

                    }

                }
            }
            if (c.HasControls())
            {
                CheckWebControl(c.Controls, Module, CheckControlList);
            }
        }
    }

    #endregion

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
            this.lb_NewMailCount.Text = count.ToString();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //页面生成完毕
        TimeSpan ts = DateTime.Now - _pageInitTime;
        lbPageResponseTime.Text = ts.Milliseconds.ToString();

    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        UserBLL.Logout(Session["UserName"].ToString(), Session.SessionID);
        Response.Redirect("~/SubModule/Login/index.aspx");
    }

    protected void bt_On_Click(object sender, EventArgs e)
    {
        WT_WorkTimeBLL _bll = new WT_WorkTimeBLL();
        Org_Staff _s = new Org_StaffBLL(int.Parse(Session["UserID"].ToString())).Model;
        if (_bll.IsBeginWork(int.Parse(Session["UserID"].ToString())))
        {
            MessageBox.Show(this.Page, "你今天已经上班了！");
        }
        else
        {
            _bll.BeginWork(int.Parse(Session["UserID"].ToString()), _s.OrganizeCity);
            bt_On.Visible = false;
            bt_Out.Visible = true;
        }
    }

    protected void bt_Out_Click(object sender, EventArgs e)
    {
        WT_WorkTimeBLL _bll = new WT_WorkTimeBLL();
        _bll.EndWork(int.Parse(Session["UserID"].ToString()), Request.UserHostAddress);
        bt_On.Visible = true;
        bt_Out.Visible = false;
    }
}
