using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_FNA_Budget_BudgetSourceAssignInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDownList();

            BindGrid();
        }

    }

    #region 绑定DropDownList
    private void BindDropDownList()
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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today).ToString();
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        int c = 0;
        if (int.TryParse(tr_OrganizeCity.SelectValue, out c))
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL(c).Model;
            if (city.Level == 5)
                ddl_CityLevel.SelectedValue = "5";
            else if (city.Level > 2)
                ddl_CityLevel.SelectedValue = "4";
            else
                ddl_CityLevel.SelectedValue = "2";
        }
    }
    #endregion

    #region 绑定GridView
    public void BindGrid()
    {
        if (tr_OrganizeCity.SelectValue == "0")
            return;

        DataTable dt = FNA_BudgetBLL.GetAssignInfo(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(ddl_CityLevel.SelectedValue));
        DataTable dt_Matrix = MatrixTable.Matrix(dt, new string[] { "OrganizeCity", "区域名称" }, "FeeTypeName", "BudgetAmount", true, true);

        gv_List.DataSource = dt_Matrix;
        gv_List.DataBind();

    }
    #endregion

    #region 查找
    protected void btnFind_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    #endregion

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            gv_List.HeaderRow.Cells[0].Text = "";
            foreach (GridViewRow r in gv_List.Rows)
            {
                r.Cells[0].Text = "";
            }
        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

}

