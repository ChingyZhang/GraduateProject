using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Account_Receipt_PreReceived : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["TradeClient"] = Request.QueryString["TradeClient"] != null ? int.Parse(Request.QueryString["TradeClient"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["TradeClient"] != 0)
            {
                CM_Client c = new CM_ClientBLL((int)ViewState["TradeClient"]).Model;
                if (c != null)
                {
                    select_Client.SelectText = c.FullName;
                    select_Client.SelectValue = c.ID.ToString();
                }
            }
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

        int.TryParse(select_Client.SelectValue, out tradeclient);
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

        int ret = AC_CashFlowListBLL.Receipt_PreReceived((int)Session["OwnerClient"], tradeclient, agentstaff, paymode, amount, 0, tbx_Remark.Text, (int)Session["UserID"], 0);

        if (ret < 0)
        {
            MessageBox.Show(this, "收款失败!Ret=" + ret.ToString());
            return;
        }
        else
        {
            Response.Redirect("CashFlowList.aspx?Classify=1&TradeClient=" + tradeclient.ToString());
        }
    }
}