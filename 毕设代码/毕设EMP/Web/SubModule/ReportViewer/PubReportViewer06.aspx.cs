using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using Microsoft.Reporting.WebForms;

public partial class SubModule_ReportViewer_PubReportViewer06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

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

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
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
                new ReportParameter("BeginDate",tbx_begin.Text),
                new ReportParameter("EndDate", tbx_end.Text),
                new ReportParameter("Staff",staff.ToString())
             };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }
        else
        {
            ReportParameter[] _parms = {
                new ReportParameter("BeginDate",tbx_begin.Text),
                new ReportParameter("EndDate", tbx_end.Text)
            };
            ReportViewer1.ServerReport.SetParameters(_parms);
        }



    }
}

