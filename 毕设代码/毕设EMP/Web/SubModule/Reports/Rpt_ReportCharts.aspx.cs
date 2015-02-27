using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;

public partial class SubModule_Reports_Rpt_ReportCharts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 获取页面参数
        ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
        #endregion

        if ((Guid)ViewState["ID"] == Guid.Empty) Response.Redirect("Rpt_ReportList.aspx");

        Rpt_Report m = new Rpt_ReportBLL((Guid)ViewState["ID"]).Model;
        if (m != null)
        {
            #region 根据报表类型控制Tab可见
            switch (m.ReportType)
            {
                case 1:
                    MCSTabControl1.Items[2].Visible = false;
                    break;
                case 2:
                    MCSTabControl1.Items[1].Visible = false;
                    break;
            }
            #endregion
        }

        BindGrid();
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " Report = '" + ViewState["ID"].ToString() + "'";

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("Rpt_ReportChartsDetail.aspx?Report=" + ViewState["ID"].ToString());
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                Response.Redirect("Rpt_ReportDetail.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "1":
                Response.Redirect("Rpt_ReportGridColumns.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "2":
                Response.Redirect("Rpt_ReportMatrixTable.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "3":
                Response.Redirect("Rpt_ReportCharts.aspx?ID=" + ViewState["ID"].ToString());
                break;
            default:
                break;
        }
    }
}
