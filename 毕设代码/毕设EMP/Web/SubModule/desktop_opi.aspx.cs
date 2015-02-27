using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.OPI;
using System.Data;
using MCSFramework.Common;
using System.Drawing;

public partial class SubModule_desktop_opi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            BindClientCount();
            BindJXCSummary();
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

        ViewState["OrganizeCity"] = staff.Model.OrganizeCity;
    }
    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["OrganizeCity"] = int.Parse(tr_OrganizeCity.SelectValue);
        BindClientCount();
        BindJXCSummary();
    }

    private void BindClientCount()
    {
        gv_ClientCount.DataSource = OPI_ClientCountBLL.GetOverview((int)ViewState["OrganizeCity"], 0);
        gv_ClientCount.DataBind();

        DataTable dt = OPI_ClientCountBLL.GetSubCityCounts((int)ViewState["OrganizeCity"], "导购员", 0);
        chart_ClientCount.Series[0].Points.DataBind(dt.Rows, "CityName", "Activity", "");

        dt = OPI_ClientCountBLL.GetSubCityCounts((int)ViewState["OrganizeCity"], "员工", 0);
        chart_ClientCount.Series[1].Points.DataBind(dt.Rows, "CityName", "Activity", "");

        dt = OPI_ClientCountBLL.GetSubCityCounts((int)ViewState["OrganizeCity"], "零售商", 0);
        chart_ClientCount.Series[2].Points.DataBind(dt.Rows, "CityName", "Activity", "");

        dt = OPI_ClientCountBLL.GetSubCityCounts((int)ViewState["OrganizeCity"], "经销商", 0);
        chart_ClientCount.Series[3].Points.DataBind(dt.Rows, "CityName", "Activity", "");
        //chart_ClientCount.DataBind();

    }

    private void BindJXCSummary()
    {
        DataTable dt = OPI_JXCSummary_DIBLL.GetOverview((int)ViewState["OrganizeCity"], 0);

        if (dt.Rows.Count > 0)
        {
            DataTable matrix_dt = MatrixTable.Matrix(dt, new string[] { "Item" }, "MonthName", "Value", false, false);
            matrix_dt.Columns["Item"].ColumnName = "项目";
            gv_JXCSummary_DI.DataSource = matrix_dt;
            gv_JXCSummary_DI.DataBind();

            DataView dv0 = new DataView(dt, "Item='A.期初库存'", "MonthName", DataViewRowState.CurrentRows);
            chart_JXCSummary.Series[0].Points.DataBind(dv0, "MonthName", "Value", "");

            DataView dv1 = new DataView(dt, "Item='B.本期进货'", "MonthName", DataViewRowState.CurrentRows);
            chart_JXCSummary.Series[1].Points.DataBind(dv1, "MonthName", "Value", "");

            DataView dv2 = new DataView(dt, "Item='C.本月销售'", "MonthName", DataViewRowState.CurrentRows);
            chart_JXCSummary.Series[2].Points.DataBind(dv2, "MonthName", "Value", "");

            DataView dv3 = new DataView(dt, "Item='D.期末盘存'", "MonthName", DataViewRowState.CurrentRows);
            chart_JXCSummary.Series[3].Points.DataBind(dv3, "MonthName", "Value", "");

            DataView dv4 = new DataView(dt, "Item='E.预计销售'", "MonthName", DataViewRowState.CurrentRows);
            chart_JXCSummary.Series[4].Points.DataBind(dv4, "MonthName", "Value", "");

            //DataView dv5 = new DataView(dt, "Item='F.实际达成'", "MonthName", DataViewRowState.CurrentRows);
            //chart_JXCSummary.Series[5].Points.DataBind(dv5, "MonthName", "Value", "");

            //DataView dv6 = new DataView(dt, "Item='H.投入费比'", "MonthName", DataViewRowState.CurrentRows);
            //chart_JXCSummary.Series[6].Points.DataBind(dv6, "MonthName", "Value", "");
        }
        else
        {
            MessageBox.Show(this, "对不起，未检索到数据!");
            return;
        }
    }
    protected void gv_JXCSummary_DI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                decimal v = 0;
                if (decimal.TryParse(e.Row.Cells[i].Text, out v))
                {
                    if (e.Row.Cells[0].Text != "F.实际达成" && e.Row.Cells[0].Text != "H.投入费比")
                    {
                        e.Row.Cells[i].Text = v.ToString("#,0");
                    }
                    else
                    {
                        e.Row.Cells[i].Text = v.ToString("0.0%");
                        if (e.Row.Cells[0].Text == "F.实际达成")
                        {
                            if (v > 1)
                            {
                                e.Row.Cells[i].Text += "<img src='../Images/gif/gif-0309.gif' />";
                                e.Row.Cells[i].ForeColor = Color.Red;
                                e.Row.Cells[i].Font.Bold = true;
                            }
                            else if (v < (decimal)0.8)
                            {
                                e.Row.Cells[i].Text += "<img src='../Images/gif/gif-0310.gif' />";
                                e.Row.Cells[i].ForeColor = Color.Blue;
                                e.Row.Cells[i].Font.Bold = true;
                            }
                        }
                        else if (e.Row.Cells[0].Text == "H.投入费比")
                        {
                            if (v > (decimal)0.4 || v == 0)
                            {
                                e.Row.Cells[i].Text += "<img width=14 src='../Images/gif/gif-0339.gif' />";
                                e.Row.Cells[i].ForeColor = Color.Red;
                                e.Row.Cells[i].Font.Bold = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
