using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;

public partial class SubModule_CM_DI_KPIList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["Flag"] != null)
            {
                //有Flag参数,表示从销售管理主菜单进入,否则表示从客户管理菜单进入
                Session["ClientID"] = null;
            }
            else
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
            }

            //客户类型，２：经销商，３：终端门店
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]);

            #endregion

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

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
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
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND CM_KPI.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

        if (select_Client.SelectValue != "")
        {
            condition += " AND CM_KPI.Client = " + select_Client.SelectValue;
        }
        else
        {
            condition += " AND CM_Client.ClientType=" + ViewState["ClientType"].ToString();
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
            Response.Redirect("KPIDetail.aspx?ClientID=" + select_Client.SelectValue + "&ClientType=" + ViewState["ClientType"].ToString());
        else
            Response.Redirect("KPIDetail.aspx?ClientType=" + ViewState["ClientType"].ToString());
    }
}
