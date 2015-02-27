using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using Microsoft.Reporting.WebForms;

public partial class SubModule_ReportViewer_PubReportViewer08 : System.Web.UI.Page
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
            }
        }

    }

    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE()");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();
    }


    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        if (tr_OrganizeCity.SelectValue == "0")
        {
            MessageBox.Show(this, "请选择要查询的管理片区!");
            return;
        }

        ReportParameter[] _parms = {
            new ReportParameter("OrganizeCity", tr_OrganizeCity.SelectValue),
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
