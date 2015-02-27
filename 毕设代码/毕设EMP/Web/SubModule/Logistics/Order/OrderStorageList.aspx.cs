using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using System.Data;
using MCSFramework.Common;

public partial class SubModule_Logistics_Order_OrderStorageList: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                ViewState["ClientID"] = _relateclient;
                select_Client.Enabled = false;
            }
            else
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null && Request.QueryString["State"] == null && Request.QueryString["ApproveFlag"] == null)
                {
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                }
            }
            #endregion

            BindDropDown();
            if (Request.QueryString["ApproveFlag"] != null)
            {
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];
            }
            else if (Request.QueryString["State"] != null)
            {
                ddl_State.SelectedValue = Request.QueryString["State"];
            }

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();

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

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderDeliveryState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }
    #endregion

    private void BindGrid()
    {
        string condition = " 1=1 ";

        #region 组织查询条件
        if (tr_OrganizeCity.SelectValue != "1")
        {
            //管理片区及所有下属管理片区
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND ORD_OrderDelivery.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND ORD_OrderDelivery.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

        //申请单号
        if (tbx_SheetCode.Text != "")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt(MCS_Logistics.dbo.ORD_OrderDelivery.ExtPropertys,'|',11)= '" + tbx_SheetCode.Text + "'";
        }

        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND ORD_OrderDelivery.State = " + ddl_State.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            condition += " AND ORD_OrderDelivery.Client=" + select_Client.SelectValue;
        }

        if (rbl_ApproveFlag.SelectedValue != "0")
        {
            condition += " AND ORD_OrderDelivery.ApproveFlag=" + rbl_ApproveFlag.SelectedValue;
        }
        #endregion

        gv_List.ConditionString = condition + "Order By ORD_OrderDelivery.AccountMonth DESC";
        gv_List.BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
       
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void rbl_ApproveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    #region 转换产品数量为界面需要的格式
    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product).Model;

        string packing1 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
        string packing2 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

        string ret = "";
        if (quantity / p.ConvertFactor != 0) ret += (quantity / p.ConvertFactor).ToString() + packing1 + " ";
        if (quantity % p.ConvertFactor != 0) ret += (quantity % p.ConvertFactor).ToString() + packing2 + " ";
        return ret;
    }

    protected int GetTrafficeQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    #endregion
}
