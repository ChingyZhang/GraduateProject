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
public partial class SubModule_FNA_ClientPayment_ClientPaymentForcast : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            #endregion

            BindDropDown();

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if ((int)ViewState["ClientID"] != 0)
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

    #region 绑定回款预估
    private void BindGrid()
    {
        string condition = " FNA_ClientPaymentForcast.PayDate BETWEEN '" + tbx_begin.Text + "' AND DateAdd(day,1,'" + tbx_end.Text + "')";

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
            condition += " And FNA_ClientPaymentForcast.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
        }

        if (select_Client.SelectValue != "")
        {
            condition += " AND FNA_ClientPaymentForcast.Client = " + select_Client.SelectValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    #endregion

    #region 审核与反审核
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        ApproveData(1);
        BindGrid();
    }


    protected void btn_CancelApprove_Click(object sender, EventArgs e)
    {
        ApproveData(2);
        BindGrid();
    }
    private void ApproveData(int approveflag)
    {

        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["FNA_ClientPaymentForcast_ID"];
                FNA_ClientPaymentForcastBLL bll;
                if (CurrentJobID > 0)
                {
                    bll = new FNA_ClientPaymentForcastBLL(CurrentJobID);
                    bll.Model.ApproveFlag = approveflag;
                    bll.Update();
                }
            }
        }
    }
    #endregion
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void btn_delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["FNA_ClientPaymentForcast_ID"];
                FNA_ClientPaymentForcastBLL bll;
                if (CurrentJobID > 0)
                {
                    bll = new FNA_ClientPaymentForcastBLL(CurrentJobID);
                    bll.Delete();
                }
            }
        }
        BindGrid();
    }


    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
        {
            Response.Redirect("ClientPaymentForcasDetail.aspx?ClientID=" + select_Client.SelectValue);
        }
    }
    protected void btn_Init_Click(object sender, EventArgs e)
    {
        int client = 0;
        if (select_Client.SelectValue != "")
        {
            client = int.Parse(select_Client.SelectValue);
        }

        FNA_ClientPaymentForcastBLL.Init(int.Parse(tr_OrganizeCity.SelectValue), client);
        BindGrid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {  
            int CurrentJobID = (int)gv_List.DataKeys[row.RowIndex]["FNA_ClientPaymentForcast_ID"];           
            if (CurrentJobID > 0)
            {
                FNA_ClientPaymentForcastBLL bll = new FNA_ClientPaymentForcastBLL(CurrentJobID);
                bll.Model.PayDate=Convert.ToDateTime(((TextBox)row.FindControl("tbx_PayDate")).Text);
                bll.Model.PayAmount = Convert.ToDecimal(((TextBox)row.FindControl("tbx_PayAmount")).Text);
                bll.Model.UpdateStaff = (int)Session["UserID"];
                bll.Model.UpdateTime = DateTime.Now;
                bll.Update();
            }

        }
        BindGrid();
    }
  
}
