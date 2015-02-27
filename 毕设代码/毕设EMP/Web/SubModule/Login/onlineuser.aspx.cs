using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using MCSFramework.BLL;

/// <summary>
/// Online ��ժҪ˵����
/// </summary>
public partial class OnlineUser : System.Web.UI.Page
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //�������û�������script
        string script = "function showonlineuser()";
        script += "{onlinewin = window.open('" + Page.ResolveClientUrl("~/SubModule/Login/onlineperson.aspx") + "', 'online', 'width:600,height:800,toolbar=no,status=no,scrollbars=yes,resizable=yes');";
        script += " onlinewin.resizeTo(650, 500);}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "showonlinepersion", script, true);

        script = "var msgwin=null;function show_sm() {";
        script += " if (msgwin != null && !msgwin.closed) { msgwin.focus(); }";
        script += " else { mytop = screen.availHeight - 310; myleft = 0;";
        script += "msgwin = window.open('" + Page.ResolveUrl("~/SubModule/OA/SM/MsgDetail.aspx") + "', 'auto_call_show', 'height=230,width=400,status=0,toolbar=no,menubar=no,location=no,scrollbars=yes,top=' + mytop + ',left=' + myleft + ',resizable=yes'); } }";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show_sm", script, true);

        if (!Page.IsPostBack)
        {
            CheckUpdate();
            //if (Request.Cookies["MCS_RefreshTime"] != null)
            //    Timer1.Interval = int.Parse(Request.Cookies["MCS_RefreshTime"].Value);
            //else
            //    Timer1.Interval = 60000;
        }
    }

    public void CheckUpdate()
    {
        if (Session["LastCheckUpdateTime"] == null || (DateTime)Session["LastCheckUpdateTime"] < DateTime.Now.AddSeconds(-30))
        {
            #region 1.����activetime 2.�ж�sessionid 3.ɾ��ʮ������δ��� 4.�õ�������������
            try
            {
                // ReturnID -1 ��ָ�Ƿ���½ -2ָ���µĶ���Ϣ
                string SessionID = Session.SessionID;
                string Username = Context.User.Identity.Name;
                int ActiveModule = (Session["ActiveModule"] != null) ? (int)Session["ActiveModule"] : 0;
                int NewMsgCount = 0;

                int OnlineUser = UserBLL.CheckUpdate(Username, SessionID, ActiveModule, out NewMsgCount);

                if (OnlineUser != -1)
                {
                    if (NewMsgCount > 0) //incoming a new msg
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "message",
                            "<script language='javascript'>show_sm();</script>", false);

                        lit.Text = "<a href='#' onclick='show_sm()'><img src='" + Page.ResolveUrl("~/Images/smsremind.gif") + "' border=0></a>";
                    }
                    else
                    {
                        lit.Text = "";
                    }
                    this.lb_OnlineUsers.Text = OnlineUser.ToString();
                }
                else
                {
                    // clear everything
                    UserBLL.Logout(Username, SessionID);

                    FormsAuthentication.SignOut();
                    Session.Clear();

                    Response.Write("<script language=javascript>alert('��ϵͳ����ʱ�����������ͬ�û���½��ͬһ�������û���½,���ڽ��Զ��ر�!');top.close();</script>");
                }


            }
            catch (Exception ex)
            {
                Response.Write("��ѯ����!<br/>" + ex.Message);
            }
            #endregion

            Session["LastCheckUpdateTime"] = DateTime.Now;
        }
        else
        {
            lb_OnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["LastGetNewMsgCount"] == null || (DateTime)Session["LastGetNewMsgCount"] < DateTime.Now.AddSeconds(Timer1.Interval / 1000))
            {
                if (UserBLL.GetNewMsgCount((string)Session["UserName"]) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "message",
                        "<script language='javascript'>show_sm();</script>", false);

                    lit.Text = "<a href='#' onclick='show_sm()'><img src='" + Page.ResolveUrl("~/Images/smsremind.gif") + "' border=0></a>";
                }
                else
                {
                    lit.Text = "";
                }
                Session["LastGetNewMsgCount"] = DateTime.Now;
            }
        }
        else
        {
        }
    }
    protected void Timer2_Tick(object sender, EventArgs e)
    {
        CheckUpdate();
    }
}
