using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_PopShowProcessDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Type"] = Request.QueryString["Type"] == null ? "1" : Request.QueryString["Type"].ToString();
        ViewState["StaffID"] = Request.QueryString["StaffID"] == null ? "0" : Request.QueryString["StaffID"].ToString();
        Bind();
    }
    private void Bind()
    {
        int StaffID = int.Parse(ViewState["StaffID"].ToString());
        if (StaffID <= 0)
            StaffID = int.Parse(Session["UserID"].ToString());
        Org_StaffBLL _staff = new Org_StaffBLL(StaffID);
        switch (ViewState["Type"].ToString())
        { 
            case "4":
                p_headr.InnerText = "已录进货的零售店数(成品)-未完成";
                gv_Detail.DataSource = _staff.GetFillProcessDetail(4);
                break;
            case "5":
                p_headr.InnerText = "已录销量的零售店数(成品)-未完成";
                gv_Detail.DataSource = _staff.GetFillProcessDetail(5);
                break;
            case "2":
                p_headr.InnerText = "返利费用申请门店数-未完成";
                gv_Detail.DataSource = _staff.GetFillProcessDetail(2);
                break;
            case "3":
                p_headr.InnerText = "导购工资申请导购数-未完成";
                gv_Detail.DataSource = _staff.GetFillProcessDetail(3);
                break;
            case "1":
            default:
                p_headr.InnerText = "陈列费用申请门店数-未完成";
                gv_Detail.DataSource = _staff.GetFillProcessDetail(1);
                break;
        }
        gv_Detail.DataBind();
        gv_Detail.BindGrid();
    }
    protected void gv_Detail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Detail.PageIndex = e.NewPageIndex;
        gv_Detail.DataBind();
        gv_Detail.BindGrid();
    }
}
