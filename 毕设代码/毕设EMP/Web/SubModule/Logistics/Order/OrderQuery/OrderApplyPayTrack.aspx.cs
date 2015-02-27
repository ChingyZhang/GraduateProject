using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.Logistics;
using System.Text;
using System.IO;

public partial class SubModule_Logistics_Order_OrderQuery_OrderApplyPayTrack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
           
        }
    }
    private void BindDropDown()
    {
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate <= GETDATE() AND YEAR >= 2013");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
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
        #endregion

    }

    private void BindGrid()
    {
        int organizecity = 0, month = 0, classify = 0, client = 0;
        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        if (organizecity > 1 && new Addr_OrganizeCityBLL(organizecity).Model.Level > 3)
        {
            MessageBox.Show(this, "管理片区必须选到办事处及以上!");
            return;
        }
        if(!int.TryParse(ddl_Month.SelectedValue, out month))
        {
            MessageBox.Show(this, "请正确选择月份!");
            return;
        }
        int.TryParse(ddl_Classify.SelectedValue, out classify);
        int.TryParse(select_Client.SelectValue, out client);
        gv_ListDetail.DataSource = ORD_OrderApplyBLL.GetRPTOrderApplyPayTrack(month, client, classify, organizecity);
        gv_ListDetail.BindGrid();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_ListDetail.AllowPaging = false;
        
        BindGrid();

        string filename = HttpUtility.UrlEncode("月度订单到款追踪表_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_ListDetail.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_ListDetail.AllowPaging = true;
        
        BindGrid();
    }
    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        BindGrid();
        Timer1.Enabled = false;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    
}
