using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_StaffSalary_FNA_StaffBounsLevelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }

    }
    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
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
        #endregion
        ddl_Quarter.DataSource = AC_AccountQuarterBLL.GetModelList("");
        ddl_Quarter.DataBind();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        FNA_StaffBounsLevelDetailBLL.Init(int.Parse(ddl_Quarter.SelectedValue), (int)Session["UserID"]);
        string condition = " 1=1 ";

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND FNA_StaffBounsLevelDetail.OrganizeCity IN (" + orgcitys + ")";
        }
        if (ddl_Quarter.SelectedValue != "0")
        {
            condition += " AND FNA_StaffBounsLevelDetail.AccountQuarter=" + ddl_Quarter.SelectedValue.Trim();
        }
        #endregion

        gvList.ConditionString = condition;
        gvList.BindGrid();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvList.Rows)
        {
            TextBox tbx_adjust = row.FindControl("txt_SalesAdjust") as TextBox;
            TextBox tbx_remark = row.FindControl("txt_Remark") as TextBox;
            TextBox tbx_budgetfeerate=row.FindControl("txt_BudgetFeeRate") as TextBox;
            TextBox tbx_actfeerate=row.FindControl("txt_ActFeeRate") as TextBox;
            FNA_StaffBounsLevelDetailBLL bll = new FNA_StaffBounsLevelDetailBLL((int)gvList.DataKeys[row.RowIndex][0]);
            decimal adjust = 0;
            if ((decimal.TryParse(tbx_adjust.Text, out adjust)))
            {
                bll.Model.SalesAdjust = adjust;
            }
            else
            {
                MessageBox.Show(this, "数据填写有误，请查实！");
                tbx_adjust.Focus();
            }
            if (tbx_remark.Text.Trim() != "")
                    bll.Model.Remark = tbx_remark.Text.Trim();
            decimal budgetfeerate = 0;
            if (decimal.TryParse(tbx_budgetfeerate.Text, out budgetfeerate))
                bll.Model.BudgetFeeRate = budgetfeerate;
            decimal actfeerate=0;
            if (decimal.TryParse(tbx_actfeerate.Text, out actfeerate))
                bll.Model.ActFeeRate = actfeerate;
            bll.Update();
        }
        BindGrid();
    }
}
