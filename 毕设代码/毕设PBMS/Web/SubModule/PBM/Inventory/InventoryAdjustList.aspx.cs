using MCSFramework.BLL.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Inventory_InventoryAdjustList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["WareHouse"] = Request.QueryString["WareHouse"] != null ? int.Parse(Request.QueryString["WareHouse"]) : 0;

            tbx_begin.Text = DateTime.Today.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Today.ToString("yyyy-MM-dd");

            BindDropDown();

            if ((int)ViewState["WareHouse"] > 0 && ddl_WareHouse.Items.FindByValue(ViewState["WareHouse"].ToString()) != null)
            {
                ddl_WareHouse.SelectedValue = ViewState["WareHouse"].ToString();
            }
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_WareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_WareHouse.DataBind();
        ddl_WareHouse.Items.Insert(0, new ListItem("请选择...", "0"));        
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " PBM_Delivery.Classify = 21 ";

        ConditionStr += " AND ISNULL(PBM_Delivery.ActArriveTime,PBM_Delivery.InsertTime) BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

        if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += " AND PBM_Delivery.Supplier = " + Session["OwnerClient"].ToString();
        }

        if (ddl_State.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.State = " + ddl_State.SelectedValue;
        }

        if (ddl_WareHouse.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.SupplierWareHouse = " + ddl_WareHouse.SelectedValue;
        }

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventoryAdjustDetail0.aspx?WareHouse=" + ddl_WareHouse.SelectedValue);
    }
}