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

public partial class SubModule_PBM_Account_Pay_PrePayment : System.Web.UI.Page
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
                if (c != null && ddl_Supplier.Items.FindByValue(c.ToString()) != null)
                {
                    ddl_Supplier.SelectedValue = c.ID.ToString();
                }
            }
        }
    }

    private void BindDropDown()
    {
        ddl_Supplier.DataSource = CM_ClientBLL.GetSupplierByTDP((int)Session["OwnerClient"]).OrderBy(p => p.FullName);
        ddl_Supplier.DataBind();

        CM_Client _c = new CM_ClientBLL((int)Session["OwnerClient"]).Model;
        if (_c != null && ddl_Supplier.Items.FindByValue(_c.OwnerClient.ToString()) != null)
            ddl_Supplier.SelectedValue = _c.OwnerClient.ToString();

        ddl_AgentStaff.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Org_Staff.Dimission=1");
        ddl_AgentStaff.DataBind();
        ddl_AgentStaff.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int tradeclient = 0, agentstaff = 0, paymode = 0;
        decimal amount = 0;

        int.TryParse(ddl_Supplier.SelectedValue, out tradeclient);
        int.TryParse(ddl_AgentStaff.SelectedValue, out agentstaff);
        int.TryParse(ddl_PayMode.SelectedValue, out paymode);
        decimal.TryParse(tbx_Amount.Text, out amount);

        if (tradeclient == 0)
        {
            MessageBox.Show(this, "请选择付款单位!");
            return;
        }

        if (agentstaff == 0)
        {
            MessageBox.Show(this, "请选择付款经办人!");
            return;
        }

        if (paymode == 0)
        {
            MessageBox.Show(this, "请选择付款方式!");
            return;
        }

        if (amount == 0)
        {
            MessageBox.Show(this, "请选择付款金额!");
            return;
        }

        int ret = AC_CashFlowListBLL.Receipt_PrePayment((int)Session["OwnerClient"], tradeclient, agentstaff, paymode, amount, 0, tbx_Remark.Text, (int)Session["UserID"]);

        if (ret < 0)
        {
            MessageBox.Show(this, "付款失败!Ret=" + ret.ToString());
            return;
        }
        else
        {
            Response.Redirect("CashFlowList.aspx?Classify=100&TradeClient=" + tradeclient.ToString());
        }
    }
}