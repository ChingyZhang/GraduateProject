using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using MCSFramework.Common;

public partial class SubModule_ReportViewer_PubReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string path = Request.QueryString["ReportPath"] != null ? Request.QueryString["ReportPath"] : "";

            if (path == "" && Session["ReportPath"] != null)
                path = Session["ReportPath"].ToString();

            if (path != "")
            {
                ViewState["ReportPath"] = path;
                ViewState["ReportServerUrl"] = Request.QueryString["ReportServerUrl"] != null ? Request.QueryString["ReportServerUrl"] : "";

                if (ViewState["ReportServerUrl"].ToString() == string.Empty)
                    ViewState["ReportServerUrl"] = ConfigurationManager.AppSettings["ReportServerUrl"];

                ReportViewer1.ServerReport.ReportServerCredentials = new MCSReportServerCredentials();
                ReportViewer1.ServerReport.ReportPath = ViewState["ReportPath"].ToString();
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(ViewState["ReportServerUrl"].ToString());
            }
        }
    }
    protected void bt_Refresh_Click(object sender, EventArgs e)
    {

        //ReportParameter[] _parms = {
        //    new ReportParameter("BeginDate", "2007-9-1"),
        //    new ReportParameter("EndDate", "2007-9-10")
        //    //new ReportParameter("SrvGroup", "120")
        //};


        //ReportViewer1.ServerReport.ReportServerCredentials = new MCSReportServerCredentials();
        //ReportViewer1.ServerReport.ReportPath = ViewState["ReportPath"].ToString();
        //ReportViewer1.ServerReport.ReportServerUrl = new Uri(ViewState["ReportServerUrl"].ToString());
        //ReportViewer1.ServerReport.SetParameters(_parms);
        ReportViewer1.ServerReport.Refresh();

    }
}
