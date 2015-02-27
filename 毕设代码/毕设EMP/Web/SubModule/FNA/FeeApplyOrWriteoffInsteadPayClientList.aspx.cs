using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data;
using MCSFramework.BLL.CM;

public partial class SubModule_FNA_FeeApplyOrWriteoffByClientList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());

            BindDropDown();

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL _r = new CM_ClientBLL((int)ViewState["ClientID"]);
                select_Client.SelectText = _r.Model.FullName;
                select_Client.SelectValue = _r.Model.ID.ToString();
                tr_OrganizeCity.SelectValue = _r.Model.OrganizeCity.ToString();

                tr_OrganizeCity.Enabled = false;
                select_Client.Enabled = false;
                ViewState["ClientType"] = _r.Model.ClientType;
            }
            
            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeWriteOffState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("请选择", "0"));
    }
    #endregion

    private void BindGrid()
    {
        if (tr_OrganizeCity.SelectValue != "1")
        {
            string orgcitys = "";

            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            string condition = " FNA_FeeWriteOff.OrganizeCity in (" + orgcitys + ") ";
            condition += " AND FNA_FeeWriteOff.AccountMonth =" + ddl_BeginMonth.SelectedValue;

            if (ddl_State.SelectedValue != "0") condition += " AND FNA_FeeWriteOff.State=" + ddl_State.SelectedValue;
            if (select_Client.SelectValue != "") condition += " AND FNA_FeeWriteOff.InsteadPayClient=" + select_Client.SelectValue;

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
            //select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2&&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        }
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue.ToString();
        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        decimal total = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            Label lb = (Label)row.FindControl("lb_SumCost");
            decimal cost = 0;
            if (decimal.TryParse(lb.Text, out cost))
            {
                total += cost;
            }
        }

        lb_SumTotalCost.Text = total.ToString();
    }
}
