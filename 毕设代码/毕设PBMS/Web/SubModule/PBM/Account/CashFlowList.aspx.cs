using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Account_CashFlowList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            //Classify: 1：收款单  100：付款单
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 1;
            ViewState["TradeClient"] = Request.QueryString["TradeClient"] != null ? int.Parse(Request.QueryString["TradeClient"]) : 1;
            #endregion

            tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();

            if ((int)ViewState["Classify"] == 1)
            {
                lb_PageTitle.Text = "现金收款列表";
                bt_PayPrePayment.Visible = false;
            }
            else
            {
                lb_PageTitle.Text = "现金付款列表";
                bt_ReceiptPreReceived.Visible = false;
            }

            if ((int)ViewState["TradeClient"] != 0)
            {
                CM_Client c = new CM_ClientBLL((int)ViewState["TradeClient"]).Model;
                if (c != null)
                {
                    select_TradeClient.SelectText = c.FullName;
                    select_TradeClient.SelectValue = c.ID.ToString();
                }
            }

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_AgentStaff.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.ID IN (SELECT AgentStaff FROM MCS_PBM.dbo.AC_CashFlowList WHERE OwnerClient=" + Session["OwnerClient"].ToString() +
            " AND InsertTime>DATEADD(MONTH,-6,GETDATE()) ) AND Org_Staff.Dimission = 1");
        ddl_AgentStaff.DataBind();
        ddl_AgentStaff.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " AC_CashFlowList.PayDate BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59' ";
        if ((int)ViewState["Classify"] == 1)
        {
            ConditionStr += " AND AC_CashFlowList.PayClassify < 100";
        }
        else
        {
            ConditionStr += " AND AC_CashFlowList.PayClassify >= 100";
        }

        if (select_TradeClient.SelectValue != "")
        {
            ConditionStr += " AND AC_CashFlowList.TradeClient=" + select_TradeClient.SelectValue;
        }

        if (ddl_AgentStaff.SelectedValue != "0")
        {
            ConditionStr += " AND AC_CashFlowList.AgentStaff=" + ddl_AgentStaff.SelectedValue;
        }

        if ((int)Session["OwnerType"] == 3) ConditionStr += " AND AC_CashFlowList.OwnerClient = " + Session["OwnerClient"].ToString();

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }
    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("BalanceUsageDetail.aspx?ID=" + _id.ToString());
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_ReceiptPreReceived_Click(object sender, EventArgs e)
    {
        Response.Redirect("Receipt_PreReceived.aspx");
    }
    protected void bt_PayPrePayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("Pay_PrePayment.aspx");
    }
}