// ===================================================================
// 文件路径:SubModule/PBM/Delivery/SaleOut/OrderList.aspx.cs 
// 生成日期:2015-03-05 13:39:36 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_PBM_Order_OrderList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Today.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Today.ToString("yyyy-MM-dd");

            BindDropDown();

            if (!string.IsNullOrEmpty(Request.QueryString["Classify"]) && ddl_Classify.Items.FindByValue(Request.QueryString["Classify"]) != null)
            {
                ddl_Classify.SelectedValue = Request.QueryString["Classify"];
                ddl_Classify.Enabled = false;
            }
            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.ID IN (SELECT SalesMan FROM MCS_PBM.dbo.PBM_Order WHERE Supplier=" + Session["OwnerClient"].ToString() +
            " AND InsertTime>DATEADD(MONTH,-6,GETDATE()) ) AND Org_Staff.Dimission = 1");
        ddl_Salesman.DataBind();
        ddl_Salesman.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " PBM_Order.Classify IN (1,2,4) ";

        ConditionStr += " AND PBM_Order.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

        if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += " AND PBM_Order.Supplier = " + Session["OwnerClient"].ToString();
        }

        if (ddl_Classify.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Order.Classify = " + ddl_Classify.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            ConditionStr += " AND PBM_Order.Client = " + select_Client.SelectValue;
        }

        if (ddl_State.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Order.State = " + ddl_State.SelectedValue;
        }

        if (ddl_Salesman.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Order.Salesman = " + ddl_Salesman.SelectedValue;
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
        Response.Redirect("OrderDetail0.aspx?Classify=" + ddl_Classify.SelectedValue);
    }

}