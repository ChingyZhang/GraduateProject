using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_HR_SpecialApplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();

        ddl_AccountTitleType.DataSource = DictionaryBLL.GetDicCollections("Special_FeeApply").OrderBy(p => p.Value.Name);
        ddl_AccountTitleType.DataBind();
        ddl_AccountTitleType.Items.Insert(0, new ListItem("全部", "0"));

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeApplyState").OrderBy(p => p.Value.Name);
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("全部", "0"));

    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " HR_SpecialApply.AccountMonth = " + ddl_Month.SelectedValue;

        if (!string.IsNullOrEmpty(tbx_SheetCode.Text))
        {
            ConditionStr += " and HR_SpecialApply.SheetCode='" + tbx_SheetCode.Text + "'";
        }
        if (!string.IsNullOrEmpty(Select_InsertStaff.SelectValue))
        {
            ConditionStr += " and HR_SpecialApply.InsertStaff=" + Select_InsertStaff.SelectValue;
        }
        if (ddl_AccountTitleType.SelectedValue !="0")
        {
            ConditionStr += " and HR_SpecialApply.AccountTitleType=" + ddl_AccountTitleType.SelectedValue;
        }
        if (ddl_ApproveFlag.SelectedValue !="0")
        {
            ConditionStr += " and HR_SpecialApply.ApproveFlag=" + ddl_ApproveFlag.SelectedValue;
        }

        #region 判断当前可查询管理片区的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND HR_SpecialApply.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion
        IList<HR_SpecialApply> list = HR_SpecialApplyBLL.GetModelList(ConditionStr);
        gv_List.BindGrid(list);

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("HR_SpecialApplyDetail.aspx");
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}