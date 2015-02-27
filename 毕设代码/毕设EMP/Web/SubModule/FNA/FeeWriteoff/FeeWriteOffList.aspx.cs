using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;

public partial class SubModule_FNA_FeeWriteoff_FeeWriteOffList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]);
            BindDropDown();

            if ((int)ViewState["FeeType"] > 0) { ddl_FeeType.SelectedValue = ViewState["FeeType"].ToString(); ddl_FeeType.Enabled = false; }
            if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
            {
                //无查看营养教育费用权限
                ListItem item = ddl_FeeType.Items.FindByValue(ConfigHelper.GetConfigInt("CSOCostType").ToString());
                if (item != null) item.Enabled = false;
            }
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType");
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));
        //if (ddl_FeeType.Items.FindByValue("1") != null) ddl_FeeType.SelectedValue = "1";

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeWriteOffState");
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
            condition += " AND FNA_FeeWriteOff.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND FNA_FeeWriteOff.AccountMonth BETWEEN " + ddl_Month.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

        //申请单号
        if (tbx_SheetCode.Text != "")
        {
            condition += " AND FNA_FeeWriteOff.SheetCode like '%" + tbx_SheetCode.Text + "%'";
        }

        //费用类型
        if (ddl_FeeType.SelectedValue != "0")
        {
            condition += " AND FNA_FeeWriteOff.FeeType = " + ddl_FeeType.SelectedValue;
        }
        if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
        {
            //无查看营养教育费用权限
            condition += " AND ISNULL(FNA_FeeWriteOff.FeeType,0) <> " + ConfigHelper.GetConfigInt("CSOCostType").ToString();
        }

        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND FNA_FeeWriteOff.State = " + ddl_State.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            condition += " AND FNA_FeeWriteOff.InsteadPayClient=" + select_Client.SelectValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["FeeType"] != 0)
            Response.Redirect("FeeWriteOffDetail0.aspx?FeeType=" + ViewState["FeeType"].ToString());
        else
            Response.Redirect("FeeWriteOffDetail0.aspx");
    }

}
