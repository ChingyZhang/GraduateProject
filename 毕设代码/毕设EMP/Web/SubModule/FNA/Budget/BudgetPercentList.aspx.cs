using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using System.Data;
using MCSFramework.BLL.FNA;

public partial class SubModule_FNA_Budget_BudgetPercentList : System.Web.UI.Page
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
    }
    #endregion
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        int organizecity = 0;
        if (int.TryParse(tr_OrganizeCity.SelectValue, out organizecity))
        {
            DataTable dt = FNA_BudgetPercentFeeTypeBLL.GetList(organizecity, 0);
            DataTable dt_matrix = MatrixTable.Matrix(dt, new string[] { "区域ID", "区域名称", "区域级别" }, "FeeTypeName", "BudgetPercent", false, true);

            //dt_matrix.Columns.Add("机动费用", Type.GetType("System.Decimal"), "100-合计");

            gv_List.DataSource = dt_matrix;
            gv_List.DataBind();
        }

    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int organizecity = (int)gv_List.DataKeys[e.NewSelectedIndex][0];

        if (organizecity > 0)
        {
            Response.Redirect("BudgetPercentDetail.aspx?OrganizeCity=" + organizecity.ToString());
        }
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("BudgetPercentDetail.aspx");
    }
}
