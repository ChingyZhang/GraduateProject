using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using System.Web.Security;
using MCSFramework.Model;
using System.Data;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;


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
                //if (Request.Browser.Browser != "IE")
                //{
                //    MessageBox.Show(this, "Ϊ����ʹ�ñ�ϵͳ��������ʹ��IE7���ϵ��������");
                //}
                //else
                //{
                //    switch (Request.Browser.Version)
                //    {
                //        case "6.0":
                //            MessageBox.Show(this, "���������ΪIE6��Ϊ����ʹ�ñ�ϵͳ�������������������IE7��IE8��");
                //            break;
                //        case "9.0":
                //            MessageBox.Show(this, "���������ΪIE9��Ϊ����ʹ�ñ�ϵͳ��������������ļ���ģʽ��");
                //            break;
                //        case "10.0":
                //            MessageBox.Show(this, "���������ΪIE10��Ϊ����ʹ�ñ�ϵͳ��������������ļ���ģʽ��");
                //            break;
                //        case "7.0":
                //        case "8.0":
                //            break;
                //    }
                //}
                //lb_BrowserVersion.Text = "������汾: " + Request.Browser.Browser + " " + Request.Browser.Version;

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
                    if (staff.Dimission == 2) { Response.Redirect("index.aspx"); return; }

                    Session["aspnetUserId"] = new Guid(Membership.GetUser(Login1.UserName).ProviderUserKey.ToString());
                    Session["UserID"] = staffid;
                    Session["UserName"] = Login1.UserName;
                    Session["UserRealName"] = staff.RealName;
                    Session["Position"] = staff.Position;           //ְλ
                    Session["OrganizeCity"] = staff.OrganizeCity;   //��Ͻ����
                    Session["ActiveModule"] = 0;
                    Session["AccountType"] = 1;                     //�˻����� 1��Ա����2����ҵ�ͻ� 3:����
                    Session["OwnerType"] = staff.OwnerType;         //1:ƽ̨����2:���̼���3:�����̼�
                    Session["OwnerClient"] = staff.OwnerClient;     //�����ͻ�

                    Session["Manufacturer"] = 0;                 //Ĭ�ϳ���

                    
                    if (staff.OwnerClient > 0)
                    {
                        CM_Client _c = new CM_ClientBLL(staff.OwnerClient).Model;
                        if (_c != null)
                        {
                            Session["ClientType"] = _c.ClientType;  //�����ͻ�����
                            Session["OwnerClientName"] = _c.FullName == "" ? _c.ShortName : _c.FullName;     //�����ͻ�����
                            
                            #region ��ȡ��ǰԱ�������ĳ���ID
                            if (staff.OwnerType == 2)
                            {
                                Session["Manufacturer"] = staff.OwnerClient;
                            }
                            else if (staff.OwnerType == 3 && _c.OwnerType == 2)
                            {
                                Session["Manufacturer"] = _c.OwnerClient;
                            }
                            #endregion
                            
                        }
                        else
                        {
                            Session["ClientType"] = -1;
                            Session["OwnerClientName"] = "";
                        }
                    }


                }
                else
                    Response.Redirect("index.aspx");
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
