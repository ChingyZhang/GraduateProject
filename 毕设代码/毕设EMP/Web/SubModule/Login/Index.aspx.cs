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
    /// index ��ժҪ˵����
    /// </summary>
    public partial class index : System.Web.UI.Page
    {
        public string RandData;


        #region ҳ���ʼ������
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // �ڴ˴������û������Գ�ʼ��ҳ��
            if (!Page.IsPostBack)
            {
                if (Request.Browser.Browser != "IE")
                {
                    //MessageBox.Show(this, "Ϊ����ʹ�ñ�ϵͳ��������ʹ��IE7���ϵ��������");
                }
                else
                {
                    switch (Request.Browser.Version)
                    {
                        case "6.0":
                            MessageBox.Show(this, "���������ΪIE6��Ϊ����ʹ�ñ�ϵͳ�������������������IE7��IE8��");
                            break;
                        case "9.0":
                            MessageBox.Show(this, "���������ΪIE9��Ϊ����ʹ�ñ�ϵͳ��������������ļ���ģʽ��");
                            break;
                        case "10.0":
                            MessageBox.Show(this, "���������ΪIE10��Ϊ����ʹ�ñ�ϵͳ��������������ļ���ģʽ��");
                            break;
                        case "7.0":
                        case "8.0":
                            break;
                    }
                }
                lb_BrowserVersion.Text = "������汾: " + Request.Browser.Browser + " " + Request.Browser.Version;

                ViewState["MacAddr"] = "";

                string title = ConfigHelper.GetConfigString("PageTitle");
                if (!string.IsNullOrEmpty(title))
                {
                    Head1.Title = title;
                }

                Session.Abandon();
                this.Login1.UserName = Request.Cookies["User"] == null ? "" : Server.UrlDecode(Request.Cookies["User"].Value);
                #region ���絯������
                //this.txtUsername.Text = Request.Cookies["Username"] != null ? Request.Cookies["UserName"].Value.ToString() : "";

                //#region ���絯��ʱ�������д��ڵ�¼�������ٵ�¼
                //if (Request.QueryString["InComeTeleNum"] != null && Request.Cookies["Username"] != null)
                //{
                //    //�ж��û��Ƿ�����
                //    string Username = Request.Cookies["UserName"].Value;
                //    string ReturnStr = SMS.CheckUpdate(Username, "", 0);
                //    string ReturnID = ReturnStr.Substring(0, ReturnStr.IndexOf("|"));
                //    if (ReturnID != "-1")
                //    {
                //        string strTeleNum = Request.QueryString["InComeTeleNum"];
                //        string strUserID = Request.Cookies["UserID"].Value;
                //        FormsAuthentication.SetAuthCookie(strUserID, false);        //ͨ����֤

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
                //Ա����¼
                Org_Staff staff = new Org_StaffBLL(staffid).Model;
                if (staff != null)
                {
                    Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                Session["UserID"] = staffid;
                    Session["UserName"] = Login1.UserName;
                    Session["UserRealName"] = staff.RealName;
                Session["ActiveModule"] = 0;
                    Session["AccountType"] = 1;     //�˻����� 1��Ա����2����ҵ�ͻ� 3:����
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
                //��ҵ�ͻ���¼
                MCSFramework.BLL.CM.CM_ClientBLL _c = new MCSFramework.BLL.CM.CM_ClientBLL(clientid);
                if (_c.Model != null)
                {
                        Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                        Session["UserID"] = clientid;
                        Session["UserName"] = Login1.UserName;
                        Session["UserRealName"] = _c.Model.FullName;
                        Session["ActiveModule"] = 0;
                        Session["AccountType"] = 2;     //�˻����� 1��Ա����2����ҵ�ͻ� 3:����
                        Session["ClientType"] = _c.Model != null ? _c.Model.ClientType : 0; //��ҵ�ͻ����
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
                        //������¼
                        MCSFramework.BLL.Promotor.PM_PromotorBLL _p = new MCSFramework.BLL.Promotor.PM_PromotorBLL(promotorid);
                        if (_p.Model != null)
                        {
                            Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                Session["UserID"] = clientid;
                            Session["UserName"] = Login1.UserName;
                            Session["UserRealName"] = _p.Model.Name;
                Session["ActiveModule"] = 0;
                            Session["AccountType"] = 3;     //�˻����� 1��Ա����2����ҵ�ͻ� 3:����
                            Session["OrganizeCity"] = _p.Model.OrganizeCity;
                        }
                        else
                            Response.Redirect("index.aspx");
                    }
                }
            }


            Response.Cookies["User"].Value = Server.UrlEncode(Login1.UserName);
            Response.Cookies["User"].Expires = new DateTime(2101, 1, 1);
            //ȥ���û�Ȩ�޻���      
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
                    MessageBox.Show(this, "��֤�����벻��ȷ!");
                    e.Cancel = true;
                    return;
                }
            }

            //ViewState["MacAddr"] = Request.Form["tbx_MacAddr"];
            
            //if (Membership.FindUsersByName(Login1.UserName).Count == 0)
            //{
            //    MessageBox.Show(this, "�Բ����û���������!");
            //    return;
            //}

            //if (!Right_Assign_BLL.GetAccessRight(Login1.UserName, 1, "LoginMACUnLimited"))
            //{
            //    int Flag = 1;      //0:ֻ��ע�����Լ����µ�MAC�ſɵ�¼��1:ֻҪ��ϵͳ��ע���MAC�����Ե�¼

            //    if (Right_Assign_BLL.GetAccessRight(Login1.UserName, 1, "LoginMACSelfLimiited")) Flag = 0;

            //    string macaddr = Request.Form["tbx_MacAddr"];// tbx_MacAddr.Text;

            //    if (!User_RegisterMACBLL.CanLogin(Login1.UserName, ref macaddr, Flag))
            //    {
            //        MessageBox.ShowAndRedirect(this, "�Բ�����ϵͳ��ȫ���趨�����ļ����MAC��ַ�ܵ���¼���ƣ����ܵ�¼��ϵͳ������ϵϵͳ����Ա��" + macaddr, "index.aspx");
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
