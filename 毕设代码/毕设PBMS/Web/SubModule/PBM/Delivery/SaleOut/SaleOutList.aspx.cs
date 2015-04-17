// ===================================================================
// 文件路径:SubModule/PBM/Delivery/SaleOut/SaleOutList.aspx.cs 
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

public partial class SubModule_PBM_Delivery_SaleOut_SaleOutList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PrepareMode"] = Request.QueryString["PrepareMode"] != null ? int.Parse(Request.QueryString["PrepareMode"]) : 0;

            tbx_begin.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Today.ToString("yyyy-MM-dd");

            BindDropDown();

            if (!string.IsNullOrEmpty(Request.QueryString["Classify"]) && ddl_Classify.Items.FindByValue(Request.QueryString["Classify"]) != null)
            {
                ddl_Classify.SelectedValue = Request.QueryString["Classify"];
                ddl_Classify.Enabled = false;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["State"]) && ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
            {
                ddl_State.SelectedValue = Request.QueryString["State"];
                ddl_State.Enabled = false;
            }
            if ((int)ViewState["PrepareMode"] > 1) bt_Add.Visible = false;

            if (!string.IsNullOrEmpty(Request.QueryString["BeginDate"]))
                tbx_begin.Text = Request.QueryString["BeginDate"];
            if (!string.IsNullOrEmpty(Request.QueryString["EndDate"]))
                tbx_end.Text = Request.QueryString["EndDate"];
            if (!string.IsNullOrEmpty(Request.QueryString["ClientID"]))
            {
                int clientid = 0;
                int.TryParse(Request.QueryString["ClientID"], out clientid);
                CM_Client c = new CM_ClientBLL(clientid).Model;
                if (c != null)
                {
                    select_Client.SelectValue = c.ID.ToString();
                    select_Client.SelectText = c.FullName;
                }
            }

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.ID IN (SELECT SalesMan FROM MCS_PBM.dbo.PBM_Delivery WHERE Supplier=" + Session["OwnerClient"].ToString() +
            " AND InsertTime>DATEADD(MONTH,-6,GETDATE()) ) AND Org_Staff.Dimission = 1");
        ddl_Salesman.DataBind();
        ddl_Salesman.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_DeliveryMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
        ddl_DeliveryMan.DataBind();
        ddl_DeliveryMan.Items.Insert(0, new ListItem("请选择", "0"));
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " PBM_Delivery.Classify IN (1,2,4) ";

        ConditionStr += " AND ISNULL(PBM_Delivery.ActArriveTime,PBM_Delivery.InsertTime) BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

        if ((int)ViewState["PrepareMode"] != 0)
        {
            ConditionStr += " AND PBM_Delivery.PrepareMode = " + ViewState["PrepareMode"].ToString();
        }

        if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += " AND PBM_Delivery.Supplier = " + Session["OwnerClient"].ToString();
        }

        if (ddl_Classify.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.Classify = " + ddl_Classify.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            ConditionStr += " AND PBM_Delivery.Client = " + select_Client.SelectValue;
        }

        if (ddl_State.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.State = " + ddl_State.SelectedValue;
        }

        if (ddl_Salesman.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.Salesman = " + ddl_Salesman.SelectedValue;
        }

        if (tbx_SheetCode.Text != "")
        {
            ConditionStr += " AND PBM_Delivery.SheetCode='" + tbx_SheetCode.Text + "'";
        }

        if (ddl_DeliveryMan.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Delivery.DeliveryMan=" + ddl_DeliveryMan.SelectedValue;
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
        Response.Redirect("SaleOutDetail0.aspx?Classify=" + ddl_Classify.SelectedValue);
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Visible)
            {
                cbx.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}