// ===================================================================
// 文件路径:CM/Distributor/DistributorList.aspx.cs 
// 生成日期:2008-12-19 10:11:21 
// 作者:	  yangwei
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
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
public partial class CM_Distributor_DistributorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

        if (!Page.IsPostBack)
        {
            BindDropDown();
            Session["ClientID"] = null;
            Session["MCSMenuControl_FirstSelectIndex"] = "12";
            BindGrid();
        }
    }

    #region 绑定下拉列表框
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

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "1";

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("CM_ActiveFlag");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("所有", "0"));
        ddl_State.SelectedValue = "1";
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ClientType = 2 ";
        if (tbx_Condition.Text.Trim() != "")
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";

        string orgcitys = "";

        #region 判断当前可查询的范围
        if ((int)Session["OwnerType"] != 3 && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND (CM_ClientManufactInfo.OrganizeCity IN (" + orgcitys +
                ") OR CM_ClientManufactInfo.ClientManager = " + Session["UserID"].ToString() + ")";
        }
        #endregion

        if ((int)Session["OwnerType"] == 2)
        {
            ConditionStr += " AND (CM_Client.OwnerType=2 AND CM_Client.OwnerClient=" + Session["Manufacturer"].ToString() + ")";
        }

        if (ddl_State.SelectedValue != "0")
        {
            ConditionStr += " And CM_ClientManufactInfo.State =" + ddl_State.SelectedValue;
        }

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            ConditionStr += " AND CM_ClientManufactInfo.ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }

        #region 获取当前员工的关联经销商
        #endregion

        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();


    }


    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询

                break;
            case "1"://高级查询
                Response.Redirect("AdvanceFind.aspx");
                break;
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Session["ClientID"] = null;
        Response.Redirect("DistributorDetail.aspx?Mode=New");
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //Response.Redirect("DistributorDetail.aspx?ClientID=" + gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Session["ClientID"] = id;
        if (Request.QueryString["URL"] != null) Response.Redirect(Request.QueryString["URL"]);
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }

    protected string setmap(int id)
    {
        CM_ClientBLL client = new CM_ClientBLL(id);
        CM_ClientGeoInfo info = CM_ClientGeoInfoBLL.GetGeoInfoByClient(id);
        if (info == null)
            return "showmap('',0,0)";
        else
            return string.Format("showmap(\"{0}\",{1},{2})", client.Model.FullName, info.Longitude, info.Latitude);
    }
}
