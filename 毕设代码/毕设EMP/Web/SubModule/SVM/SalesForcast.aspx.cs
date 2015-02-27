using System;
using System.Data;
using System.Web.UI;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using System.Web.UI.WebControls;


public partial class SubModule_SVM_SalesForcast : System.Web.UI.Page
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
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，２：经销商，３：终端门店
            #endregion

            BindDropDown();

            if (Request.QueryString["ApproveFlag"] != null)
            {
                Session["ClientID"] = null;
                ViewState["ClientID"] = null;
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];

                if ((int)ViewState["ClientType"] == 3)
                    Header.Attributes["WebPageSubCode"] += "ClientType=3";
                else if ((int)ViewState["ClientType"] == 2)
                {
                    ViewState["DIClassify"] = Request.QueryString["DIClassify"] == null ? 1 : int.Parse(Request.QueryString["DIClassify"]);
                    Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + ViewState["DIClassify"].ToString();
                }
                BindGrid();
            }
            else if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }
                ViewState["ClientType"] = client.Model.ClientType;

                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;

                if (client.Model.ClientType == 3)
                    Header.Attributes["WebPageSubCode"] += "ClientType=3";
                else if (client.Model.ClientType == 2)
                    Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + client.Model["DIClassify"];

                BindGrid();
            }
            //else
            //{
            //    if ((int)ViewState["ClientType"] == 2)
            //        MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            //    else
            //        MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            //}
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
        tr_OrganizeCity_Selected(null, null);

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<DateAdd(month,1,GetDate()) AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<DateAdd(month,1,GetDate()) AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(1)).ToString();

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
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
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void rbl_ApproveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        if (tr_list.Visible)
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

                condition += " AND SVM_SalesForcast.OrganizeCity IN (" + orgcitys + ")";
            }

            condition += " AND SVM_SalesForcast.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

            if (rbl_ApproveFlag.SelectedValue != "0")
            {
                condition += " AND SVM_SalesForcast.ApproveFlag= " + rbl_ApproveFlag.SelectedValue;
            }

            if (select_Client.SelectValue != "")
            {
                condition += " AND SVM_SalesForcast.Client = " + select_Client.SelectValue;
            }
            else
            {
                condition += " AND CM_Client.ClientType=" + ViewState["ClientType"].ToString();
                
                if ((int)ViewState["ClientType"] == 2 && ViewState["DIClassify"] != null)
                {
                    condition += " AND MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,'|',7)='" + ViewState["DIClassify"].ToString() + "'";
                }
            }
            #endregion

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else
        {
            int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
            int client = select_Client.SelectValue == "" ? 0 : int.Parse(select_Client.SelectValue);
            if (organizecity == 0) organizecity = 1;
            DataTable dt = SVM_SalesForcastBLL.GetSummary(organizecity, client, int.Parse(ddl_BeginMonth.SelectedValue), int.Parse(ddl_EndMonth.SelectedValue), int.Parse(ViewState["ClientType"].ToString()));
            int _quantity = 0;
            decimal _totalvalue = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _quantity += (int)dt.Rows[i]["SumQuantity"];
                _totalvalue += (decimal)dt.Rows[i]["SumMoney"];
            }
            DataRow dr = dt.NewRow();
            dr["ProductCode"] = "合计";
            dr["SumQuantity"] = _quantity;
            dr["SumMoney"] = _totalvalue;
            dt.Rows.Add(dr);
            gv_Summary.DataSource = dt;
            gv_Summary.DataBind();
        }
    }
    #endregion

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
            Response.Redirect("SalesForcastDetail.aspx?ClientID=" + select_Client.SelectValue + "&ClientType=" + ViewState["ClientType"].ToString());
        else
            Response.Redirect("SalesForcastDetail.aspx?ClientType=" + ViewState["ClientType"].ToString());
    }

    protected decimal GetForcastSumPrice(string ForcastID)
    {
        return SVM_SalesForcastBLL.GetForcastSumPrice(int.Parse(ForcastID));
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tr_list.Visible = true;
            tr_summary.Visible = false;
            //rbl_ApproveFlag.Visible = true;
            MCSTabControl1.SelectedIndex = 0;
        }
        else
        {
            tr_list.Visible = false;
            tr_summary.Visible = true;
            //    rbl_ApproveFlag.Visible = false;
            MCSTabControl1.SelectedIndex = 1;
        }
        BindGrid();
    }


    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            if (((CheckBox)row.FindControl("cb_Check")).Checked == true)
            {
                new SVM_SalesForcastBLL((int.Parse(gv_List.DataKeys[row.RowIndex]["SVM_SalesForcast_ID"].ToString()))).Approve((int)Session["UserID"]);
            }
        }
        BindGrid();
    }
}

