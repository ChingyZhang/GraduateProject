// ===================================================================
// 文件路径:PM/PM_PromotorList.aspx.cs 
// 生成日期:2008-12-30 10:07:41 
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
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using System.Collections.Generic;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class PM_PM_PromotorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
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

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "1";

        ddl_Dimission.DataSource = DictionaryBLL.GetDicCollections("PUB_Dimission");
        ddl_Dimission.DataBind();
        ddl_Dimission.Items.Insert(0, new ListItem("所有", "0"));
        ddl_Dimission.SelectedValue = "1";

    }
    #endregion

    private void BindGrid()
    {
        string condition = " PM_Promotor.ID IN (SELECT TOP 1000 ID FROM MCS_Promotor.dbo.PM_Promotor WHERE 1 = 1";
        if (tbx_Condition.Text != string.Empty)
        {
            condition += " And " + ddl_SearchType.SelectedValue + " Like '%" + this.tbx_Condition.Text.Trim() + "%'";
        }

        string orgcitys = "";

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            //if (condition != "") condition += " AND ";
            condition += " AND PM_Promotor.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " And PM_Promotor.ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }

        if (ddl_Dimission.SelectedValue != "0")
        {
            condition += " And PM_Promotor.Dimission =" + ddl_Dimission.SelectedValue;
        }

        condition += ")";

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM_PromotorDetail.aspx");
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

    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Redirect("PM_PromotorDetail.aspx?ID=" + gv_List.DataKeys[e.NewSelectedIndex][0].ToString());
    }

    protected string PromotorInClient(int promotor)
    {
        string clientname = "";
        IList<PM_PromotorInRetailer> lists = PM_PromotorInRetailerBLL.GetModelList("Promotor=" + promotor.ToString());
        foreach (PM_PromotorInRetailer item in lists)
        {
            CM_Client c = new CM_ClientBLL(item.Client).Model;
            if (c != null) clientname += "<a href='../CM/RT/RetailerDetail.aspx?ClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                + c.FullName + "</a><br/>";
        }
        return clientname;
    }

}