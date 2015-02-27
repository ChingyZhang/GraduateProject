using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Logistics;
public partial class SubModule_Logistics_Order_Pop_AdjustGiftApplyAmount : System.Web.UI.Page
{
    private TextBox txt_FeeRate;
    private TextBox txt_AvailableAmount;
    private Label lbl_BalanceAmount;
    private TextBox txt_AdjustAmount;
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_FeeRate = pn_Detail.FindControl("ORD_GiftApplyAmount_FeeRate") != null ? (TextBox)pn_Detail.FindControl("ORD_GiftApplyAmount_FeeRate") : null;
        txt_AvailableAmount = pn_Detail.FindControl("ORD_GiftApplyAmount_AvailableAmount") != null ? (TextBox)pn_Detail.FindControl("ORD_GiftApplyAmount_AvailableAmount") : null;
        lbl_BalanceAmount = pn_Detail.FindControl("ORD_GiftApplyAmount_BalanceAmount") != null ? (Label)pn_Detail.FindControl("ORD_GiftApplyAmount_BalanceAmount") : null;
        txt_AdjustAmount = pn_Detail.FindControl("ORD_GiftApplyAmount_AdjustAmount") != null ? (TextBox)pn_Detail.FindControl("ORD_GiftApplyAmount_AdjustAmount") : null;

        if (txt_FeeRate != null)
        {
            //txt_FeeRate.TextChanged += new EventHandler(txt_FeeRate_TextChanged);
            //txt_FeeRate.AutoPostBack = true;
            txt_FeeRate.Enabled = false;
        }
        if (txt_AvailableAmount != null)
        {
            txt_AdjustAmount.TextChanged += new EventHandler(txt_AdjustAmount_TextChanged);
            txt_AdjustAmount.AutoPostBack = true;
        }
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            BindData();
        }

    }
    private void BindData()
    {
        ORD_GiftApplyAmountBLL applyamount = new ORD_GiftApplyAmountBLL((int)ViewState["ID"]);
        if (applyamount.Model == null) ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
        pn_Detail.BindData(applyamount.Model);
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ORD_GiftApplyAmountBLL applyamount = new ORD_GiftApplyAmountBLL((int)ViewState["ID"]);
        pn_Detail.GetData(applyamount.Model);
        applyamount.Model.BalanceAmount = decimal.Parse(lbl_BalanceAmount.Text);
        applyamount.Model["AdjustAmount"] = "0";
        applyamount.Model.UpdateStaff = (int)Session["UserID"];
        applyamount.Update();
        Session["SuccessFlag"] = true;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "<script language='javascript'>window.close();</script>", false);
    }
    protected void txt_FeeRate_TextChanged(object sender, EventArgs e)
    {
        //ORD_GiftApplyAmountBLL applyamount = new ORD_GiftApplyAmountBLL((int)ViewState["ID"]);
        //decimal feerate = 0, AvailableAmount = 0;
        //if (decimal.TryParse(txt_FeeRate.Text.Trim(), out feerate) && applyamount.Model.SalesVolume > 0)
        //{
        //    AvailableAmount = Math.Round(applyamount.Model.SalesVolume * (feerate) / 100, 3);
        //    txt_AdjustAmount.Text = "";
        //    txt_AvailableAmount.Text = AvailableAmount.ToString("0.###");
        //    if (lbl_BalanceAmount != null) lbl_BalanceAmount.Text = (AvailableAmount + applyamount.Model.PreBalance - applyamount.Model.AppliedAmount - applyamount.Model.DeductibleAmount).ToString("0.###");
        //}
    }
    protected void txt_AdjustAmount_TextChanged(object sender, EventArgs e)
    {
        ORD_GiftApplyAmountBLL applyamount = new ORD_GiftApplyAmountBLL((int)ViewState["ID"]);
        decimal adjustAmount = 0;
        if (decimal.TryParse(txt_AdjustAmount.Text.Trim(), out adjustAmount) && applyamount.Model.SalesVolume > 0)
        {
            //txt_FeeRate.Text = Math.Round((applyamount.Model.AvailableAmount + adjustAmount) / applyamount.Model.SalesVolume * 100, 3).ToString("0.###");
        }
        if (lbl_BalanceAmount != null) lbl_BalanceAmount.Text = (applyamount.Model.AvailableAmount + adjustAmount + applyamount.Model.PreBalance - applyamount.Model.AppliedAmount - applyamount.Model.DeductibleAmount).ToString("0.###");
        if (txt_AvailableAmount != null) txt_AvailableAmount.Text = (applyamount.Model.AvailableAmount + adjustAmount).ToString("0.###");
    }
}
