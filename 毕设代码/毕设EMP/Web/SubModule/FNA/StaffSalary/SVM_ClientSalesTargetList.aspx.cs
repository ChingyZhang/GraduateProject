using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_StaffSalary_SVM_ClientSalesTargetList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }
    }
    #region 绑定下拉列表框
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
        #endregion
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();

    }
    #endregion

    private void BindGrid()
    {
        string condition = "SVM_ClientSalesTarget.AccountMonth=" + ddl_AccountMonth.SelectedValue;

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND SVM_ClientSalesTarget.OrganizeCity IN (" + orgcitys + ")";
        }
        if (select_client.SelectValue != "")
        {
            condition += " AND SVM_ClientSalesTarget.Client=" + select_client.SelectValue;
        }
        #endregion
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}