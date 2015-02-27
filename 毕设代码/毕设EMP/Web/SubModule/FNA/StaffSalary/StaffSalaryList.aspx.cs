using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Promotor;

public partial class SubModule_PM_PM_StaffSalaryList : System.Web.UI.Page
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

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate < getdate()");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-5)).ToString();

        ddl_EndMonth.DataSource = ddl_BeginMonth.DataSource;
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("PM_SalaryState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

    }
    #endregion

    private void BindGrid()
    {
        string condition = "1=1";

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND FNA_StaffSalary.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND FNA_StaffSalary.AccountMonth Between " + ddl_BeginMonth.SelectedValue;
        condition += " AND " + ddl_EndMonth.SelectedValue;


        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND FNA_StaffSalary.State = " + ddl_State.SelectedValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffSalaryGenerate.aspx");
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];

        Response.Redirect("StaffSalaryDetailList.aspx?ID=" + id.ToString());
    }
}
