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

public partial class SubModule_ReportViewer_PubReportViewer04 : System.Web.UI.Page
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

    }


    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
        }
        else
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
                new ReportParameter("OrganizeCitys", orgcitys),
                new ReportParameter("Month", ddl_Month.SelectedValue),
                new ReportParameter("Staff",staff.ToString())
             };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }
        else
        {
            ReportParameter[] _parms = {
                new ReportParameter("OrganizeCitys", orgcitys),
                new ReportParameter("Month", ddl_Month.SelectedValue)
            };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }


    }

}
