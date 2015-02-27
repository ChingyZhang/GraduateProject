using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using System.Data;
using MCSFramework.Model;
using MCSFramework.Model.FNA;

public partial class SubModule_FNA_Budget_BudgetTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            LoadFromUsableBudget();
            LoadToUsableBudget();
        }
    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        DataTable dt = staff.GetStaffOrganizeCity();

        tr_FromOrganizeCity.DataSource = dt;

        Org_StaffBLL staff2 = new Org_StaffBLL((int)Session["UserID"]);
        DataTable dt2 = staff2.GetStaffOrganizeCity();

        tr_ToOrganizeCity.DataSource = dt2;
        if (dt.Select("ID = 1").Length > 0)
        {
            tr_FromOrganizeCity.RootValue = "0";
            tr_ToOrganizeCity.RootValue = "0";
        }
        else
        {
            tr_FromOrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_FromOrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
            //tr_FromOrganizeCity.Enabled = false;

            tr_ToOrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff2.Model.OrganizeCity).Model.SuperID.ToString();
            tr_ToOrganizeCity.SelectValue = staff2.Model.OrganizeCity.ToString();
        }

        #endregion

        ddl_FromMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>GETDATE() AND EndDate<DateAdd(month,1,GETDATE())");
        ddl_FromMonth.DataBind();
        //ddl_FromMonth.Enabled = false;

        ddl_ToMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>GETDATE() AND EndDate<DateAdd(month,1,GETDATE())");
        ddl_ToMonth.DataBind();
        //ddl_ToMonth.Enabled = false;
    }

    #region 刷新指定管理单元的可用预算额度
    private void LoadFromUsableBudget()
    {
        int city = int.Parse(tr_FromOrganizeCity.SelectValue);
        int month = int.Parse(ddl_FromMonth.SelectedValue);
        if (city > 0)
        {
            IList<FNA_BudgetBalance> balances = FNA_BudgetBalanceBLL.GetModelList("OrganizeCity=" + city.ToString() +
                " AND AccountMonth=" + month.ToString() + " ORDER BY FeeType");

            #region 根据费用类型加上余额中没有的类型,以便支持从下级向上调回该费用类型的预算额度
            Dictionary<string, Dictionary_Data> dicFeetype = DictionaryBLL.GetDicCollections("FNA_FeeType");
            foreach (Dictionary_Data dic in dicFeetype.Values)
            {
                if (balances.FirstOrDefault(p => p.FeeType == int.Parse(dic.Code)) == null)
                {
                    FNA_BudgetBalance balance = new FNA_BudgetBalance();
                    balance.AccountMonth = month;
                    balance.OrganizeCity = city;
                    balance.FeeType = int.Parse(dic.Code);
                    balance.CostBalance = 0;
                    balance.DDFInitialBalance = 0;
                    balances.Add(balance);
                }
            }
            #endregion

            gv_FromBalance.BindGrid(balances);
        }
    }

    private void LoadToUsableBudget()
    {
        int city = int.Parse(tr_ToOrganizeCity.SelectValue);
        int month = int.Parse(ddl_ToMonth.SelectedValue);
        if (city > 0)
        {
            gv_ToBalance.BindGrid(FNA_BudgetBalanceBLL.GetModelList("OrganizeCity=" + city.ToString() +
                " AND AccountMonth=" + month.ToString() + " ORDER BY FeeType"));
        }
    }

    protected void tr_FromOrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        LoadFromUsableBudget();
    }
    protected void ddl_FromMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFromUsableBudget();
    }

    protected void ddl_ToMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadToUsableBudget();
    }
    protected void tr_ToOrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        LoadToUsableBudget();
    }

    #endregion

    protected void bt_Transfer_Click(object sender, EventArgs e)
    {
        int frommonth = int.Parse(ddl_FromMonth.SelectedValue);
        int fromcity = int.Parse(tr_FromOrganizeCity.SelectValue);

        int tomonth = int.Parse(ddl_ToMonth.SelectedValue);
        int tocity = int.Parse(tr_ToOrganizeCity.SelectValue);

        if (frommonth != tomonth)
        {
            MessageBox.Show(this, "调拨的月份必须相同!");
            return;
        }

        if (fromcity == 0 || tocity == 0)
        {
            MessageBox.Show(this, "请正确选择要调拨预算的源及目的管理片区!");
            return;
        }

        if (new Addr_OrganizeCityBLL(fromcity).Model.Level > new Addr_OrganizeCityBLL(tocity).Model.Level)
        {
            MessageBox.Show(this, "源管理片区的级别不能低于目的管理片区的级别！如需调回金额，请在调拨金额中输入负数。");
            return;
        }

        if (!new Addr_OrganizeCityBLL(fromcity).GetAllChildNodeIDs().Split(new char[] { ',' }).Contains(tocity.ToString()))
        {
            MessageBox.Show(this, "目的管理单元并不是源管理单元的直接或间接下级，不可以调拨!");
            return;
        }

        if (fromcity == tocity && frommonth == tomonth)
        {
            MessageBox.Show(this, "源与目的管理片区及月份不能完全相同!");
            return;
        }
        int budgettype = 3;       //调拨预算
        decimal amount = 0;

        #region 判断调拨金额是否在可用余额之内
        foreach (GridViewRow row in gv_FromBalance.Rows)
        {
            int id = (int)gv_FromBalance.DataKeys[row.RowIndex]["ID"];
            int feetype = (int)gv_FromBalance.DataKeys[row.RowIndex]["FeeType"];

            if (decimal.TryParse(((TextBox)row.FindControl("tbx_TransferAmount")).Text, out amount))
            {
                if (amount > 0)
                {
                    if (FNA_BudgetBLL.GetUsableAmount(frommonth, fromcity, feetype) - amount < 0)
                    {
                        ((TextBox)row.FindControl("tbx_TransferAmount")).Focus();
                        MessageBox.Show(this, "对不起,调拨金额必须小于源管理片区的可用预算余额!");
                        return;
                    }
                }
                else
                {
                    if (FNA_BudgetBLL.GetUsableAmount(tomonth, tocity, feetype) + amount < 0)
                    {
                        ((TextBox)row.FindControl("tbx_TransferAmount")).Focus();
                        MessageBox.Show(this, "对不起,目的管理片区的可用余额不够调拨金额!");
                        return;
                    }
                }
            }
            else
            {
                ((TextBox)row.FindControl("tbx_TransferAmount")).Focus();
                MessageBox.Show(this, "对不起,调拨金额必须为数字格式!");
                return;
            }
        }
        #endregion

        foreach (GridViewRow row in gv_FromBalance.Rows)
        {
            int feetype = (int)gv_FromBalance.DataKeys[row.RowIndex]["FeeType"];
            if (decimal.TryParse(((TextBox)row.FindControl("tbx_TransferAmount")).Text, out amount))
            {
                if (amount != 0)
                {
                    FNA_BudgetBLL.Transfer(frommonth, fromcity, feetype, tomonth, tocity, feetype, amount, budgettype,
                        (int)Session["UserID"], ((TextBox)row.FindControl("tbx_Remark")).Text);
                }
            }
        }

        LoadFromUsableBudget();
        LoadToUsableBudget();

        MessageBox.Show(this, "预算调配成功！");
    }
}
