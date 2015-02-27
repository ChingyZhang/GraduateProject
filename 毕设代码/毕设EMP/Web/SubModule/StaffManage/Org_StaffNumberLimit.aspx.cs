using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using System.Data;

public partial class SubModule_StaffManage_Org_StaffNumberLimit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            BindGrid();
        }
    }

    private void BindDropDown()
    {
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

        ddl_CityLevel.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
        ddl_CityLevel.DataBind();
        ddl_CityLevel.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_CityLevel.SelectedValue = "4";

        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.SelectValue = staff.Model.Position.ToString();
    }

    protected void CBAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("CBSelect");
            cb_check.Checked = CBAll.Checked;
        }
    }
    public void BindGrid()
    {
        string condition = " 1 = 1 ";

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") condition += " AND OrganizeCity IN (" + orgcitys + ")";
        }

        if (tr_Position.SelectValue != "1")
        {
            Org_PositionBLL org_position = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
            string positions = org_position.GetAllChildPosition();
            if (positions != "") positions += ",";
            positions += tr_Position.SelectValue;

            if (positions != "") condition += " AND Position IN (" + positions + ")";
        }
        #endregion

        gv_List.BindGrid(Org_StaffNumberLimitBLL.GetModelList(condition));

    }
    protected int GetActualNumber(int OrganizeCity, int Position)
    {
        return Org_StaffNumberLimitBLL.GetActualNumber(OrganizeCity, Position);
    }

    protected void BtnInit_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_CityLevel.SelectedValue);
        int position = int.Parse(tr_Position.SelectValue);

        Org_StaffNumberLimitBLL.Init(organizecity, level, position, false, (int)Session["UserID"]);
        BindGrid();
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        #region 修改明细
        foreach (GridViewRow row in gv_List.Rows)
        {
            Org_StaffNumberLimitBLL _bll = new Org_StaffNumberLimitBLL(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Model.BudgetNumber = int.Parse(((TextBox)row.FindControl("TBBudget")).Text);
            _bll.Model.NumberLimit = int.Parse(((TextBox)row.FindControl("TBLimit")).Text);
            _bll.Update();
        }

        #endregion
        BindGrid();

    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Org_StaffNumberLimitBLL _bll = new Org_StaffNumberLimitBLL(int.Parse(gv_List.DataKeys[e.RowIndex]["ID"].ToString()));
        _bll.Delete();
        BindGrid();
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_select = (CheckBox)row.FindControl("CBSelect");
            if (cb_select.Checked)
            {
                Org_StaffNumberLimitBLL _bll = new Org_StaffNumberLimitBLL(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
                _bll.Delete();
            }
        }
        BindGrid();
    }
}
