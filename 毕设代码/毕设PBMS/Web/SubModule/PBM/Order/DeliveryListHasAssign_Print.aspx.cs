using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Order_DeliveryListHasAssign_Print : System.Web.UI.Page
{
    private int PRINTPAGESIZE = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["PreArrivalDate_BeginDate"] = DateTime.Parse(Request.QueryString["PreArrivalDate_BeginDate"]);
            ViewState["PreArrivalDate_EndDate"] = DateTime.Parse(Request.QueryString["PreArrivalDate_EndDate"]);
            ViewState["SupplierWareHouse"] = Request.QueryString["SupplierWareHouse"] != null ? int.Parse(Request.QueryString["SupplierWareHouse"]) : 0;
            ViewState["SalesMan"] = Request.QueryString["SalesMan"] != null ? int.Parse(Request.QueryString["SalesMan"]) : 0;
            ViewState["DeliveryMan"] = Request.QueryString["DeliveryMan"] != null ? int.Parse(Request.QueryString["DeliveryMan"]) : 0;
            #endregion

            BindData();
        }
    }
    private void BindData()
    {
        DataTable dt = PBM_DeliveryBLL.GetNeedDeliverySummary((DateTime)ViewState["PreArrivalDate_BeginDate"], (DateTime)ViewState["PreArrivalDate_EndDate"],
           (int)Session["OwnerClient"], (int)ViewState["SupplierWareHouse"],
           (int)ViewState["SalesMan"], (int)ViewState["DeliveryMan"]);

        #region 求合计行
        int q_t = 0, q_p = 0;
        decimal w = 0, a = 0;
        foreach (DataRow row in dt.Rows)
        {
            q_t += (int)row["Quantity_T"];
            q_p += (int)row["Quantity_P"];
            a += (decimal)row["Amount"];
            w += (decimal)row["Weight"];
        }

        DataRow dr = dt.NewRow();
        dr["ProductName"] = "合计";
        dr["Quantity_T"] = q_t;
        dr["Packagint_T"] = "件";
        dr["Quantity_P"] = q_p;
        dr["Packagint_P"] = "散";
        dr["Amount"] = a;
        dr["Weight"] = w;

        dt.Rows.Add(dr);
        #endregion

        ViewState["DataTable"] = dt;

        //获取分页数量
        int totlaItem = dt.Rows.Count;
        int pageNum = (totlaItem / PRINTPAGESIZE) + (totlaItem % PRINTPAGESIZE == 0 ? 0 : 1);

        ViewState["TotalPageCount"] = pageNum;

        int[] intTemp = new int[pageNum];
        for (int i = 0; i < pageNum; i++) intTemp[i] = i;

        Repeater1.DataSource = intTemp;
        Repeater1.DataBind();
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //设置打印换页
        Literal _c = (Literal)e.Item.FindControl("lb_RepeaterNextPage");
        if (e.Item.ItemIndex > 0 && _c != null) _c.Text = "<br/><div class='PageNext'></div><br/>";

        Literal lbTitle = (Literal)e.Item.FindControl("lbTitle");//打印单标题
        if (lbTitle != null)
        {
            lbTitle.Text = string.Format("{0} - {1}", new CM_ClientBLL((int)Session["OwnerClient"]).Model.FullName, "预售出库汇总单");
        }

        //日期
        Label lbDeliveryTime = (Label)e.Item.FindControl("lbDeliveryTime");
        if (lbDeliveryTime != null)
        {
            lbDeliveryTime.Text = ((DateTime)ViewState["PreArrivalDate_BeginDate"]).ToString("yyyy-MM-dd");
            if ((DateTime)ViewState["PreArrivalDate_BeginDate"] != (DateTime)ViewState["PreArrivalDate_EndDate"])
                lbDeliveryTime.Text += "至" + ((DateTime)ViewState["PreArrivalDate_EndDate"]).ToString("yyyy-MM-dd");
        }

        //出库仓库
        Label lbSupplierWareHouse = (Label)e.Item.FindControl("lbSupplierWareHouse");
        if (lbSupplierWareHouse != null && (int)ViewState["SupplierWareHouse"] > 0)
            lbSupplierWareHouse.Text = new CM_WareHouseBLL((int)ViewState["SupplierWareHouse"]).Model.Name;

        //业务员
        Label lbSalesMan = (Label)e.Item.FindControl("lbSalesMan");
        if (lbSalesMan != null && (int)ViewState["SalesMan"] > 0)
            lbSalesMan.Text = "业务员:" + new Org_StaffBLL((int)ViewState["SalesMan"]).Model.RealName;

        //送货人
        Label lbDeliveryMan = (Label)e.Item.FindControl("lbDeliveryMan");
        if (lbDeliveryMan != null && (int)ViewState["DeliveryMan"] > 0)
            lbDeliveryMan.Text = "送货人:" + new Org_StaffBLL((int)ViewState["DeliveryMan"]).Model.RealName;

        Label lb_PageInfo = (Label)e.Item.FindControl("lb_PageInfo");
        if (lb_PageInfo != null) lb_PageInfo.Text = string.Format("第{0}页,共{1}页", e.Item.ItemIndex + 1, (int)ViewState["TotalPageCount"]);


        GridView gv_List = (GridView)e.Item.FindControl("gv_List");
        if (gv_List != null)
        {
            gv_List.AllowPaging = true;
            gv_List.PageSize = PRINTPAGESIZE;
            gv_List.PageIndex = e.Item.ItemIndex;
            gv_List.DataSource = (DataTable)ViewState["DataTable"];
            gv_List.DataBind();

            gv_List.BottomPagerRow.Cells[0].Text = string.Format("第{0}页，共{1}页", gv_List.PageIndex + 1, (int)ViewState["TotalPageCount"]);
        }
    }
}