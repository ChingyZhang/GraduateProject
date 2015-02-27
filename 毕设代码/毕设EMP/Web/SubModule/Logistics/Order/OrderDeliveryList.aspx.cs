using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;

public partial class SubModule_Logistics_ORD_OrderApplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderDeliveryState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

    }
    #endregion

    private void BindGrid()
    {
        string condition = " 1=1 ";

        #region 组织查询条件
        if (string.IsNullOrEmpty(tr_OrganizeCity.SelectValue) && tr_OrganizeCity.SelectValue != "0")
        {
            //管理片区及所有下属管理片区
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND ORD_OrderDelivery.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND ORD_OrderDelivery.AccountMonth = " + ddl_Month.SelectedValue;

        //申请单号
        if (tbx_SheetCode.Text != "")
        {
            condition += " AND ORD_OrderDelivery.SheetCode like '%" + tbx_SheetCode.Text + "%'";
        }

        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND ORD_OrderDelivery.State = " + ddl_State.SelectedValue;
        }
        #endregion
       // gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderDeliveryDetail.aspx");
    }
}
