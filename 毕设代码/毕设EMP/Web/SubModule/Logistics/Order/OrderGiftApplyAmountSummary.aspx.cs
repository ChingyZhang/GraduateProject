using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;

public partial class SubModule_Logistics_Order_OrderGiftApplyAmountSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }
    #region 绑定下拉列表框
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


        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList(" Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();


    }
    #endregion

    private void BindGrid()
    {
        int organizecity, accoutmonth;
        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        int.TryParse(ddl_Month.SelectedValue, out accoutmonth);
        DataTable dtSummary = ORD_GiftApplyAmountBLL.GetUsedInfo(accoutmonth, organizecity);
        if (dtSummary.Rows.Count == 0)
        {
            gv_List.DataBind();
            return;
        }

        dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "大区", "省区", "办事处", "代码", "经销商ID", "客户全称" },
                     new string[] { "ORD_GiftApplyAmount_Brand", "Classify" },
                     new string[] { "实际销量", "赠品费率%", "本月实销生成可申请额度",
                     "上月余额","赠品抵扣额","已申请赠品额度","还可申请赠品额度"}, false, false);
        dtSummary.Columns.Add("N.合计→-→含赠品抵扣额费率%");
        dtSummary.Columns["N.合计→-→含赠品抵扣额费率%"].SetOrdinal(dtSummary.Columns.Count - 6);
        foreach (DataRow row in dtSummary.Rows)
        {
            if ((decimal)row["N.合计→-→实际销量"] != 0)
            {
                row["N.合计→-→含赠品抵扣额费率%"] = (((decimal)row["N.合计→-→已申请赠品额度"] + (decimal)row["N.合计→-→赠品抵扣额"]) / (decimal)row["N.合计→-→实际销量"]).ToString("0.00%");
            }
            else
            {
                row["N.合计→-→含赠品抵扣额费率%"] = 0;
            }

        }
        gv_List.DataSource = dtSummary;
        gv_List.DataBind();

        if (dtSummary.Columns.Count >= 24)
            gv_List.Width = new Unit(dtSummary.Columns.Count * 55);
        else
            gv_List.Width = new Unit(100, UnitType.Percentage);

        MatrixTable.GridViewMatric(gv_List);



    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_List.Rows)
            {
                #region 金额数据格式化
                if (r.Cells.Count > 6)
                {
                    for (int i = 6; i < r.Cells.Count; i++)
                    {

                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            if (gv_List.HeaderRow.Cells[i].Text.EndsWith("%"))
                            {
                                r.Cells[i].Text = d.ToString("0.##")+"%";
                            }
                            else
                            {
                                r.Cells[i].Text = d.ToString("0.##");
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("费用申请汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_List.AllowPaging = true;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    { }
}
