using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.PBM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Account_Pay_BalanceSupplierAP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["BalanceTradeClient"] == null || Session["BalanceAPIDs"] == null) Response.Redirect("APList.aspx");

            BindDropDown();

            if ((int)Session["BalanceTradeClient"] != 0)
            {
                CM_Client c = new CM_ClientBLL((int)Session["BalanceTradeClient"]).Model;
                if (c != null)
                {
                    lb_TradeClient.Text = c.FullName + "(" + c.LinkManName + ":" + c.Mobile + ")";
                }
            }

            string _ARIDs = (string)Session["BalanceAPIDs"];
            IList<AC_ARAPList> lists = AC_ARAPListBLL.GetModelList("Type = 2 AND OwnerClient=" + Session["OwnerClient"].ToString() +
                " AND TradeClient=" + Session["BalanceTradeClient"].ToString() + " AND ISNULL(BalanceFlag,1)=1" + " AND ID IN (" + _ARIDs + ")");
            if (lists.Count == 0)
            {
                MessageBox.ShowAndRedirect(this, "无待结算应收款记录!", "APList.aspx");
                return;
            }

            tbx_Amount.Text = lists.Sum(p => Math.Round(p.Amount, 2)).ToString("0.##");
        }
    }

    private void BindDropDown()
    {
        ddl_AgentStaff.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
        ddl_AgentStaff.DataBind();
        ddl_AgentStaff.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int tradeclient = 0, agentstaff = 0, paymode = 0;
        decimal amount = 0;

        tradeclient = (int)Session["BalanceTradeClient"];
        int.TryParse(ddl_AgentStaff.SelectedValue, out agentstaff);
        int.TryParse(ddl_PayMode.SelectedValue, out paymode);
        decimal.TryParse(tbx_Amount.Text, out amount);

        if (tradeclient == 0)
        {
            MessageBox.Show(this, "请选择收款客户!");
            return;
        }

        if (agentstaff == 0)
        {
            MessageBox.Show(this, "请选择收款经办人!");
            return;
        }

        if (paymode == 0)
        {
            MessageBox.Show(this, "请选择收款方式!");
            return;
        }

        if (amount == 0)
        {
            MessageBox.Show(this, "请选择收款金额!");
            return;
        }
        int ret = 0;

        if (paymode == 11)
        {
            ret = AC_BalanceUsageListBLL.BalanceAP((int)Session["OwnerClient"], tradeclient,
                agentstaff, amount, tbx_Remark.Text, (string)Session["BalanceAPIDs"]);
        }
        else
        {
            ret = AC_CashFlowListBLL.Receipt_BalanceAP((int)Session["OwnerClient"], tradeclient,
                agentstaff, paymode, amount, tbx_Remark.Text, (int)Session["UserID"], 0, (string)Session["BalanceAPIDs"]);
        }

        if (ret < 0)
        {
            MessageBox.Show(this, "结款失败!Ret=" + ret.ToString());
            return;
        }
        else
        {
            Session["BalanceAPIDs"] = null;
            Session["BalanceTradeClient"] = null;

            Response.Redirect("APList.aspx?TradeClient=" + tradeclient.ToString());
        }
    }
}