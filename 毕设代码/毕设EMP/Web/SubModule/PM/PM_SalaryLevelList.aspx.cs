using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_PM_PM_SalaryLevelList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindDropDown();
            BindDate();
        }
    }
    
    protected void btn_Salary_Search_Click(object sender, EventArgs e)
    {
        BindDate();
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
    }
    #endregion

    public void BindDate()
    {
        string ConditionStr = "";

        string orgcitys = "";
        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " MCS_Promotor.dbo.PM_SalaryLevel.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    protected void btn_Salary_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM_SalaryLevelDetail.aspx");
    }
}
