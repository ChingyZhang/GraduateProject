// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-30 14:26:38 
// 作者:	yangwei  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;

public partial class SubModule_PM_Search_SelectPromotor : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();
            if (Request.QueryString["OrganizeCity"] != null)
            {
                tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"];
                tr_OrganizeCity.Enabled = false;
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
    }
    #endregion

    private void BindGrid()
    {
        string condition = " PM_Promotor.Dimission=1 AND PM_Promotor.ApproveFlag=1 ";
        if (tbx_Condition.Text != string.Empty)
        {
            condition += " AND " + ddl_SearchType.SelectedValue + " Like '%" + this.tbx_Condition.Text.Trim() + "%'";
        }

        #region 判断当前可查询的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND PM_Promotor.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (Request.QueryString["ExtCondition"] != null)
        {
            condition += " AND (" + Request.QueryString["ExtCondition"] + ")";
        }

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        tbx_value.Text = gv_List.DataKeys[e.NewSelectedIndex][0].ToString();
        tbx_text.Text = (string)gv_List.DataKeys[e.NewSelectedIndex][1];
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }
}
