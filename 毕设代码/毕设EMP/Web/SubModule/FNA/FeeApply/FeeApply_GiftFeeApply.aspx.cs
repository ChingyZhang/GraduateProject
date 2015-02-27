using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.Model.FNA;
using MCSFramework.Model.Pub;

public partial class SubModule_FNA_FeeApply_FeeApply_GiftFeeApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["GiftFeeType"] = ConfigHelper.GetConfigInt("GiftFeeType");

            BindDropDown();
        }
    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        tr_OrganizeCity_Selected(null, null);
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_ApplyMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_ApplyMonth.DataBind();
        ddl_ApplyMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();

        rbl_GiftClassify.DataSource = DictionaryBLL.GetDicCollections("ORD_GiftClassify");
        rbl_GiftClassify.DataBind();
        rbl_GiftClassify.SelectedValue = "1";

        ddl_AccountTitle.DataSource = AC_AccountTitleBLL.GetListByFeeType((int)ViewState["GiftFeeType"]);
        ddl_AccountTitle.DataBind();

        ddl_LastWriteOffMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>=GETDATE() AND BeginDate<DATEADD(month,5,GETDATE())");
        ddl_LastWriteOffMonth.DataBind();

        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent='1'");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        decimal giftamountbalance = 0;
        int month = 0, city = 0, feetype = 0, client = 0, productbrand = 0, giftclassify = 0;
        feetype = (int)ViewState["GiftFeeType"];

        int.TryParse(ddl_ApplyMonth.SelectedValue, out month);
        int.TryParse(tr_OrganizeCity.SelectValue, out city);
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_Brand.SelectedValue, out productbrand);
        int.TryParse(rbl_GiftClassify.SelectedValue, out giftclassify);

        #region 判断有效性
        if (city == 0)
        {
            MessageBox.Show(this, "请正确选择管理片区");
            return;
        }

        if (client == 0)
        {
            MessageBox.Show(this, "请正确选择经销商");
            return;
        }

        if (productbrand == 0)
        {
            MessageBox.Show(this, "请正确选择赠品费用归属品牌");
            return;
        }
        giftamountbalance = GetGiftAmountBalance();
        if (giftamountbalance == 0)
        {
            MessageBox.Show(this, "可申请赠品金额为0，不可申请赠品");
            return;
        }

        int AccountTitle = int.Parse(ddl_AccountTitle.SelectedValue);
        if (TreeTableBLL.GetChild("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", AccountTitle).Rows.Count > 0)
        {
            MessageBox.Show(this, "费用科目必须选择最底级会计科目!" + ddl_AccountTitle.SelectedItem.Text);
            return;
        }

        decimal applycost = 0;
        decimal.TryParse(tbx_ApplyCost.Text, out applycost);
        if (applycost == 0)
        {
            MessageBox.Show(this, "申请赠品金额不可为0");
            return;
        }

        if (applycost > giftamountbalance)
        {
            MessageBox.Show(this, "赠品申请金额:" + tbx_ApplyCost.Text + "不能大于可用申请赠品金额:" + lb_GiftAmountBalance.Text);
            return;
        }
        #endregion

        #region 创建费用申请单
        FNA_FeeApplyBLL bll = new FNA_FeeApplyBLL();

        bll.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        bll.Model.AccountMonth = int.Parse(ddl_ApplyMonth.SelectedValue);
        bll.Model.Client = client;         //费用代垫客户
        bll.Model.State = 1;               //草稿
        bll.Model.ApproveFlag = 2;         //未审核
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model["Title"] = ddl_ApplyMonth.SelectedItem.Text + " 赠品费用申请单" + " " + select_Client.SelectText + " " + Session["UserRealName"].ToString();
        bll.Model.FeeType = feetype;
        bll.Model["GiftFeeClassify"] = giftclassify.ToString();
        bll.Model.ProductBrand = productbrand;

        AC_AccountMonth accountmonth = new AC_AccountMonthBLL(month).Model;
        FNA_FeeApplyDetail detail = new FNA_FeeApplyDetail();
        detail.Client = client;
        detail.AccountTitle = AccountTitle;
        detail.BeginMonth = month;
        detail.EndMonth = month;
        detail.BeginDate = accountmonth.BeginDate;
        detail.EndDate = accountmonth.EndDate;
        detail.ApplyCost = applycost * (100 - decimal.Parse(tbx_DIPercent.Text)) / 100;
        detail.DICost = applycost * decimal.Parse(tbx_DIPercent.Text) / 100;
        detail.SalesForcast = decimal.Parse(tbx_SalesForcast.Text);
        detail.Remark = tbx_Remark.Text;
        detail.RelateBrands = productbrand.ToString();
        detail.LastWriteOffMonth = int.Parse(ddl_LastWriteOffMonth.SelectedValue);
        detail.Flag = 1;  //未报销
        detail["FeeApplyType"] = "1";
        bll.Items.Add(detail);

        bll.Model.SheetCode = FNA_FeeApplyBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);
        int id = bll.Add();
        #endregion


        if (id > 0)
            Response.Redirect("FeeApplyDetail3.aspx?ID=" + id.ToString());
        else
            MessageBox.Show(this, "对不起，赠品费用申请单生成失败！错误码:" + id.ToString());
    }
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        GetGiftAmountBalance();
    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGiftAmountBalance();
    }
    protected void rbl_GiftClassify_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGiftAmountBalance();
    }
    private decimal GetGiftAmountBalance()
    {
        decimal giftamountbalance = 0;
        int month = 0, city = 0, feetype = 0, client = 0, productbrand = 0, giftclassify = 0;
        feetype = (int)ViewState["GiftFeeType"];

        int.TryParse(ddl_ApplyMonth.SelectedValue, out month);
        int.TryParse(tr_OrganizeCity.SelectValue, out city);
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_Brand.SelectedValue, out productbrand);
        int.TryParse(rbl_GiftClassify.SelectedValue, out giftclassify);

        IList<ORD_GiftApplyAmount> giftamounts = ORD_GiftApplyAmountBLL.GetModelList(
            string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}",
            month, client, productbrand, giftclassify));
        if (giftamounts.Count > 0)
        {
            decimal budget = FNA_BudgetBLL.GetUsableAmount(month, city, feetype, false);
            decimal balance = giftamounts[0].BalanceAmount;

            giftamountbalance = budget > balance ? balance : budget;
        }

        lb_GiftAmountBalance.Text = giftamountbalance.ToString("0.0#");
        return giftamountbalance;
    }


}
