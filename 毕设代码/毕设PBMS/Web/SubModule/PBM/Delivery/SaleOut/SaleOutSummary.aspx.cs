using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Delivery_SaleOut_SaleOutSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PrepareMode"] = Request.QueryString["PrepareMode"] != null ? int.Parse(Request.QueryString["PrepareMode"]) : 0;

            tbx_begin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Today.ToString("yyyy-MM-dd");

            BindDropDown();

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_SalesMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.ID IN (SELECT SalesMan FROM MCS_PBM.dbo.PBM_Delivery WHERE Supplier=" + Session["OwnerClient"].ToString() +
            " AND InsertTime>DATEADD(MONTH,-6,GETDATE()) ) AND Org_Staff.Dimission = 1");
        ddl_SalesMan.DataBind();
        ddl_SalesMan.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_DeliveryMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
        ddl_DeliveryMan.DataBind();
        ddl_DeliveryMan.Items.Insert(0, new ListItem("请选择", "0"));
    }
    #endregion

    private void BindGrid()
    {
        DateTime dtbegin, dtend;
        int salesman, deliveryman;

        DateTime.TryParse(tbx_begin.Text, out dtbegin);
        DateTime.TryParse(tbx_end.Text, out dtend);
        int.TryParse(ddl_SalesMan.SelectedValue, out salesman);
        int.TryParse(ddl_DeliveryMan.SelectedValue, out deliveryman);

        if (gv_List_Product.Visible)
        {
            DataTable dt = PBM_DeliveryBLL.GetDeliverySummary_ByProduct((int)Session["OwnerClient"], salesman, deliveryman, 0, dtbegin, dtend);


            int _quantity_t = 0, _quantity_p = 0;
            decimal _amount = 0;
            foreach (DataRow dr in dt.Rows)
            {
                _quantity_t += (int)dr["Quantity_T"];
                _quantity_p += (int)dr["Quantity_P"];
                _amount += (decimal)dr["TotalAmount"];
            }

            DataRow row = dt.NewRow();
            row["ProductName"] = "合计";
            row["Quantity_T"] = _quantity_t;
            row["Quantity_P"] = _quantity_p;
            row["TotalAmount"] = _amount;
            dt.Rows.Add(row);

            gv_List_Product.DataSource = dt;
            gv_List_Product.DataBind();

        }
        else if (gv_List_Client.Visible)
        {
            DataTable dt = PBM_DeliveryBLL.GetDeliverySummary_ByClient((int)Session["OwnerClient"], salesman, deliveryman, 0, dtbegin, dtend);
            gv_List_Client.DataSource = dt;
            gv_List_Client.DataBind();
        }
        else if (gv_PayInfoSummary.Visible)
        {
            DataTable dt = PBM_DeliveryBLL.GetPayInfoSummary((int)Session["OwnerClient"], salesman, deliveryman, dtbegin, dtend);

            gv_PayInfoSummary.DataSource = dt;
            gv_PayInfoSummary.DataBind();
            gv_PayInfoSummary.Visible = true;




        }
        else if (gv_PayInfoDetail.Visible)
        {
            DataTable dt = PBM_DeliveryBLL.GetPayInfoDetail((int)Session["OwnerClient"], salesman, deliveryman, dtbegin, dtend);
            dt = MatrixTable.Matrix(dt, new string[] { "SheetCode", "ClientName", "DeliveryManName" }, "PayModeName", "Amount");
            dt.Columns["SheetCode"].ColumnName = "单号";
            dt.Columns["ClientName"].ColumnName = "客户";
            dt.Columns["DeliveryManName"].ColumnName = "送货人";

            gv_PayInfoDetail.DataSource = dt;
            gv_PayInfoDetail.DataBind();

        }
    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List_Client.PageIndex = 0;
        BindGrid();
    }
    protected void gv_List_Client_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int client = (int)gv_List_Client.DataKeys[e.NewSelectedIndex]["Client"];

        Response.Redirect("SaleOutList.aspx?ClientID=" + client.ToString() + "&BeginDate=" + tbx_begin.Text + "&EndDate=" + tbx_end.Text + "&State=4");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List_Product.Visible = e.item.Value == "0";
        gv_List_Client.Visible = e.item.Value == "1";
        gv_PayInfoSummary.Visible = e.item.Value == "2";
        gv_PayInfoDetail.Visible = e.item.Value == "3";

        BindGrid();

    }
    protected void gv_PayInfoDetail_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_PayInfoDetail.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                decimal d = 0;
                foreach (TableCell c in row.Cells)
                {
                    if (decimal.TryParse(c.Text, out d))
                    {
                        c.Text = d.ToString("0.##");
                    }
                }
            }
        }
    }
    protected void gv_List_Client_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List_Client.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_Product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List_Product.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_PayInfoSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_PayInfoSummary.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_PayInfoDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_PayInfoDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }

}