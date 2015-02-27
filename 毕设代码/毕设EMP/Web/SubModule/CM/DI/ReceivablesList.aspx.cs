using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;

public partial class SubModule_CM_DI_ReceivablesList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }

            BindDropDown();

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;

                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();

                select_Client.Enabled = false;
                tr_OrganizeCity.Enabled = false;
                BindGrid();
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
        }
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        string condition = "1=1";

        #region 组织查询条件
        if (select_Client.SelectValue != "")
        {
            condition += " AND Client = " + select_Client.SelectValue;
        }
        else
        {
            MessageBox.Show(this, "请先选择要查询应收账明细的客户！");
            return;
        }

        //会计月条件
        condition += " AND AccountMonth = " + ddl_Month.SelectedValue;
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    #endregion
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
            Response.Redirect("ReceivablesDetail.aspx?ClientID=" + select_Client.SelectValue);
        else
            MessageBox.Show(this, "必须选择客户!");
    }
}
