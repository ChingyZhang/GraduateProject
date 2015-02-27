using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;

public partial class SubModule_Pop_ShowLowerPositionTask : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Type"] = Request.QueryString["Type"] == null ? "1" : Request.QueryString["Type"].ToString();
        ViewState["StaffID"] = Request.QueryString["StaffID"] == null ? "0" : Request.QueryString["StaffID"].ToString();
        ViewState["City"] = Request.QueryString["City"] == null ? "0" : Request.QueryString["City"].ToString();
        ViewState["Month"] = Request.QueryString["Month"] == null ? AC_AccountMonthBLL.GetCurrentMonth() : int.Parse(Request.QueryString["Month"].ToString());
        Bind();
    }
    private void Bind()
    {
        int StaffID = int.Parse(ViewState["StaffID"].ToString());
        if (StaffID <= 0)
            StaffID = int.Parse(Session["UserID"].ToString());
        Org_StaffBLL _staff = new Org_StaffBLL(StaffID);
        int type = 0;
        int.TryParse(ViewState["Type"].ToString(),out type);
        int city=0;
        int.TryParse(ViewState["City"].ToString(),out city);
    
        switch (type)
        {
            case 1:
                p_headr.InnerText = "陈列费用下游待审批";break;
            case 2:
                p_headr.InnerText = "返利费用下游待审批";break;
            case 3:
                p_headr.InnerText = "导购工资下游待审批";break;
            case 4:
                p_headr.InnerText = "费用核销下游待审批";break;
            case 5:
                p_headr.InnerText = "促销品请购下游待审批";break;
            default:
                p_headr.InnerText = "其他费用下游待审批";break;
        }
        gv_Detail.DataSource = _staff.GetLowerPositionTask(type, city, (int)ViewState["Month"]);
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
