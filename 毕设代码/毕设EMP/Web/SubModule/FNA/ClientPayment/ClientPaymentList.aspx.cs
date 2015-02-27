using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.Common;

public partial class SubModule_FNA_ClientPayment_ClientPaymentList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            #endregion

            BindDropDown();

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (Request.QueryString["ApproveFlag"] != null)
            {
                Session["ClientID"] = null;
                ViewState["ClientID"] = null;

                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];
                BindGrid();
            }
            else if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;

                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();             
               
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

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
        #endregion
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
        string condition = " FNA_ClientPaymentInfo.PayDate BETWEEN '" + tbx_begin.Text + "' AND DateAdd(day,1,'" + tbx_end.Text + "')";

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

        if (rbl_ApproveFlag.SelectedValue != "0")
        {
            condition += " And FNA_ClientPaymentInfo.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            condition += " AND FNA_ClientPaymentInfo.Client = " + select_Client.SelectValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    #endregion

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
        {
            Response.Redirect("ClientPaymentDetail.aspx?ClientID=" + select_Client.SelectValue);
        }
    }

    protected void btn_pass_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["FNA_ClientPaymentInfo_ID"];
                FNA_ClientPaymentInfoBLL bll;
                if (CurrentJobID > 0)
                {
                    bll = new FNA_ClientPaymentInfoBLL(CurrentJobID);
                    if(bll.Model.ApproveFlag==2)
                    bll.Confirm((int)Session["UserID"], DateTime.Now);
                }
            }
        }
        Response.Redirect("~/SubModule/FNA/ClientPayment/ClientPaymentList.aspx");
    }
    protected void btn_CanclePass_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["FNA_ClientPaymentInfo_ID"];
                FNA_ClientPaymentInfoBLL bll;
                if (CurrentJobID > 0)
                {
                    bll = new FNA_ClientPaymentInfoBLL(CurrentJobID);
                    if (bll.Model.ApproveFlag == 1)
                    bll.CancleConfirm((int)Session["UserID"], DateTime.Now);
                }
            }
        }
        Response.Redirect("~/SubModule/FNA/ClientPayment/ClientPaymentList.aspx");
    }
}
