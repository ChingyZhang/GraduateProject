using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using MCSFramework.Common;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL;

public partial class SubModule_Logistics_Order_OrderQuery_OrderApplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
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
        tbx_BeginDate.Text = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
        tbx_EndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }
    private void BindGrid()
    {
        int organizecity = 0, client = 0;
        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        if (organizecity > 1 && new Addr_OrganizeCityBLL(organizecity).Model.Level > 3)
        {
            MessageBox.Show(this, "管理片区必须选到办事处及以上!");
            return;
        }
        DateTime beginDate, endDate;
        if (tbx_BeginDate.Text == "" || !DateTime.TryParse(tbx_BeginDate.Text, out beginDate))
        {
            MessageBox.Show(this, "请重新填写开始日期!");
            tbx_BeginDate.Focus();
            return;
        }
        if (tbx_EndDate.Text == "" || !DateTime.TryParse(tbx_EndDate.Text, out endDate))
        {
            MessageBox.Show(this, "请重新填写截至日期!");
            tbx_EndDate.Focus();
            return;
        }
        int.TryParse(select_Client.SelectValue, out client);

        gv_ListDetail.DataSource = ORD_OrderApplyBLL.GetRPTOrderList(organizecity, client, beginDate, endDate, tbx_OrderCode.Text.Trim());
        gv_ListDetail.BindGrid();
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_ListDetail.AllowPaging = false;
        gv_ListDetail.Columns[2].Visible = true;
        gv_ListDetail.Columns[3].Visible = false;
        gv_ListDetail.GridLines = GridLines.Both;
        BindGrid();

        string filename = HttpUtility.UrlEncode("订单出货汇总表_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
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
        gv_ListDetail.Columns[3].Visible = true;
        gv_ListDetail.Columns[2].Visible = false;
        gv_ListDetail.GridLines = GridLines.None;
        BindGrid();
    }
    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Timer1.Enabled = false;

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    
}
