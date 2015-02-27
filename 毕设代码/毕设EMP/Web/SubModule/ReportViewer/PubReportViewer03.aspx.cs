using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Configuration;
using MCSFramework.Common;
using Microsoft.Reporting.WebForms;
using MCSFramework.BLL.CM;

public partial class SubModule_ReportViewer_PubReportViewer03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            string path = Request.QueryString["ReportPath"] != null ? Request.QueryString["ReportPath"] : "";

            if (path == "" && Session["ReportPath"] != null)
                path = Session["ReportPath"].ToString();

            if (path != "")
            {
                ViewState["ReportPath"] = path;
                ViewState["ReportServerUrl"] = Request.QueryString["ReportServerUrl"] != null ? Request.QueryString["ReportServerUrl"] : "";

                if (ViewState["ReportServerUrl"].ToString() == string.Empty)
                    ViewState["ReportServerUrl"] = ConfigHelper.GetConfigString("ReportServerUrl");

                //ReportViewer1.ServerReport.ReportServerCredentials = new MCSReportServerCredentials();
                //ReportViewer1.ServerReport.ReportPath = ViewState["ReportPath"].ToString();
                //ReportViewer1.ServerReport.ReportServerUrl = new Uri(ViewState["ReportServerUrl"].ToString());
            }
            string ClientID = Request.QueryString["ClientID"] != null ? Request.QueryString["ClientID"] : Session["ClientID"] == null ? "" : Session["ClientID"].ToString();
            if (string.IsNullOrEmpty(ClientID))
            {
                Response.Redirect("../CM/RT/RetailerList.aspx");
            }
            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=3";
            CM_ClientBLL client = new CM_ClientBLL(int.Parse(ClientID));
            select_Client.SelectText = client.Model.FullName;
            select_Client.SelectValue = ClientID;
        }

    }

    private void BindDropDown()
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE()");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();


        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE()");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

    }


    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue == "")
        {
            MessageBox.Show(this, "请选择要查询的零售商!");
            return;
        }

        ReportParameter[] _parms = {
            new ReportParameter("Client", select_Client.SelectValue),
            new ReportParameter("BeginMonth", ddl_BeginMonth.SelectedValue),
            new ReportParameter("EndMonth", ddl_EndMonth.SelectedValue)
        };

        ReportViewer1.ServerReport.ReportServerCredentials = new MCSReportServerCredentials();
        ReportViewer1.ServerReport.ReportPath = ViewState["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(ViewState["ReportServerUrl"].ToString());
        ReportViewer1.ServerReport.SetParameters(_parms);
       // ReportViewer1.ServerReport.Refresh();


    }

}
