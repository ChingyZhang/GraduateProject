using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_PBM_Account_ARList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["TradeClient"] = Request.QueryString["TradeClient"] != null ? int.Parse(Request.QueryString["TradeClient"]) : 0;

            tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-01");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();

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
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " AC_ARAPList.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59' AND AC_ARAPList.Type = 1";
        if ((int)Session["OwnerType"] == 3) ConditionStr += " AND AC_ARAPList.OwnerClient = " + Session["OwnerClient"].ToString();

        if (select_TradeClient.SelectValue != "")
        {
            bt_Balance.Enabled = true;
            ConditionStr += " AND AC_ARAPList.TradeClient=" + select_TradeClient.SelectValue;
        }
        else
            bt_Balance.Enabled = false;

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
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
    protected void bt_Balance_Click(object sender, EventArgs e)
    {
        int _TradeClient = 0;
        int.TryParse(select_TradeClient.SelectValue, out _TradeClient);
        if (_TradeClient == 0)
        {
            MessageBox.Show(this, "请选择结算客户!");
            return;
        }


        string _ARIDs = "";
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["AC_ARAPList_ID"];
                _ARIDs += id.ToString() + ",";
            }
        }
        if (_ARIDs.EndsWith(",")) _ARIDs = _ARIDs.Substring(0, _ARIDs.Length - 1);

        if (_ARIDs == "")
        {
            MessageBox.Show(this, "请选择要结算的应收款记录!");
            return;
        }

        Session["BalanceTradeClient"] = _TradeClient;
        Session["BalanceARIDs"] = _ARIDs;
        Response.Redirect("Receipt_BalanceRetailerAR.aspx");
    }
}