using System;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using Microsoft.Reporting.WebForms;

public partial class SubModule_ReportViewer_PubReportViewer07 : System.Web.UI.Page
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
    }


    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        if (tr_OrganizeCity.SelectValue == "0")
        {
            MessageBox.Show(this, "请选择要查询的管理片区!");
            return;
        }

        ReportViewer1.ServerReport.ReportServerCredentials = new MCSReportServerCredentials();
        ReportViewer1.ServerReport.ReportPath = ViewState["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(ViewState["ReportServerUrl"].ToString());

        if (ReportViewer1.ServerReport.GetParameters().Count == 3)
        {
            int staff = 0;
            int position = new Org_StaffBLL((int)Session["UserID"]).Model.Position;
            string remark = new Org_PositionBLL(position).Model.Remark;
            if (remark != null && remark.Contains("OnlyViewSelfReport"))
            {
                staff = (int)Session["UserID"];
            }

            ReportParameter[] _parms = {
                new ReportParameter("OrganizeCity", tr_OrganizeCity.SelectValue),
                new ReportParameter("Month", ddl_BeginMonth.SelectedValue),
                new ReportParameter("Staff",staff.ToString())
        };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }
        else
        {
            ReportParameter[] _parms = {
                new ReportParameter("OrganizeCity", tr_OrganizeCity.SelectValue),
                new ReportParameter("Month", ddl_BeginMonth.SelectedValue)
            };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }
    }
}
