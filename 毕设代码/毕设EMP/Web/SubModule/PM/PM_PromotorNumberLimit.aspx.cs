using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Promotor;
using MCSFramework.Common;
using MCSFramework.Model.Promotor;
using System.Data;

public partial class SubModule_PM_PM_PromotorNumberLimit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            BindDropdown();
            BindGrid();
        }

    }

    private void BindDropdown()
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

        ddl_CityLevel.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel");
        ddl_CityLevel.DataBind();
        ddl_CityLevel.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_CityLevel.SelectedValue = "4";

        ddl_Classify.DataSource = DictionaryBLL.GetDicCollections("PM_PromotorClassify");
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("不限制类别", "0"));
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            PM_PromotorNumberLimitBLL _bll = new PM_PromotorNumberLimitBLL(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Model.ID = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
            _bll.Model.NumberLimit = int.Parse(((TextBox)row.FindControl("TBLimit")).Text);
            _bll.Model.BudgetNumber = int.Parse(((TextBox)row.FindControl("TBBudget")).Text);
            _bll.Model.Remark = ((TextBox)row.FindControl("TBRemark")).Text;
            _bll.Update();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
                new PM_PromotorNumberLimitBLL().Delete(id);
            }
        }
        BindGrid();

    }
    public void BindGrid()
    {
        if (tr_OrganizeCity.SelectValue != "1")
        {
            gv_List.BindGrid(PM_PromotorNumberLimitBLL.GetByOrganizeCity(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_Classify.SelectedValue)));
        }
    }

    protected int GetActualNumber(int OrganizeCity, int Classify)
    {
        return PM_PromotorNumberLimitBLL.GetActualNumber(OrganizeCity, Classify);
    }
    protected void BtnInit_Click(object sender, EventArgs e)
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_CityLevel.SelectedValue);
        int classify = int.Parse(ddl_Classify.SelectedValue);

        if (organizecity == 1 || level <= 1)
        {
            MessageBox.Show(this, "请正确选择管理片区和城市级别");
            return;
        }

        PM_PromotorNumberLimitBLL.Init(organizecity, level, classify, (int)Session["UserID"]);
        BindGrid();
    }
}
