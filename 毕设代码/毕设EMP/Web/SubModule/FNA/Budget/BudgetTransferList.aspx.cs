using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using System.Data;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_Budget_BudgetTransferList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            tbx_BeginDate.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_EndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        DataTable dt = staff.GetStaffOrganizeCity();

        tr_OrganizeCity.DataSource = dt;
        if (dt.Select("ID = 1").Length > 0)
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

    private void BindGrid()
    {
        string condition = " 1 = 1 ";

        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (rbl_Flag.SelectedValue == "0")
                condition += " AND FNA_BudgetTransfer.FromOrganizeCity IN (" + orgcitys + ")";
            else
                condition += " AND FNA_BudgetTransfer.ToOrganizeCity IN (" + orgcitys + ")";
        }
        condition += " AND FNA_BudgetTransfer.InsertTime Between '" + tbx_BeginDate.Text + "' AND DateAdd(day,1,'" + tbx_EndDate.Text + "')";

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
        
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
