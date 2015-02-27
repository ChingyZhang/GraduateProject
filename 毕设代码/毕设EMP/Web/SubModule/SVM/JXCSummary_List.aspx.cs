using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;

public partial class SubModule_SVM_JXCSummary_List : System.Web.UI.Page
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
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 0 : int.Parse(Request.QueryString["ClientType"]);

            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" +
                            ViewState["ClientType"].ToString() + "&NoParent=Y";
            if ((int)ViewState["ClientType"] == 2)
                Header.Attributes["WebPageSubCode"] = "ClientClassify=1";    //1:经销商               
            else if ((int)ViewState["ClientType"] == 2)
                Header.Attributes["WebPageSubCode"] = "ClientClassify=3";    //3:促销门店
            #endregion

            BindDropDown();

            if (Request.QueryString["ApproveFlag"] != null)
            {
                //查找过滤添加：0：所有 1：审核 2：未审核
                if (Request.QueryString["ClientClassify"] != null)
                    ViewState["ClientClassify"] = int.Parse(Request.QueryString["ClientClassify"]);
                else
                    Response.Redirect("~/SubModule/Desktop.aspx");

                Session["ClientID"] = null;
                ViewState["ClientID"] = null;
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];

                ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddMonths(-1)).ToString();
                ddl_EndMonth.SelectedValue = ddl_BeginMonth.SelectedValue;

                Header.Attributes["WebPageSubCode"] = "ClientClassify=" + ViewState["ClientClassify"].ToString();
            }
            else if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                if (client.Model["DIClassify"] == "2")
                {
                    Response.Redirect("JXCSummary_ListSub.aspx?ClientID=" + client.Model.ID.ToString() + "&ClientType=2");
                }

                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }

                #region 载入客户信息
                ViewState["ClientType"] = client.Model.ClientType;

                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" +
                    client.Model.ClientType.ToString() + "&OrganizeCity=" + client.Model.OrganizeCity.ToString() + "&NoParent=Y";
                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                #endregion

                #region 判断传入客户参数的客户类别
                //if (client.Model.ClientType == 2 && client.Model["DIClassify"] == "1")
                //    Header.Attributes["WebPageSubCode"] = "ClientClassify=1";    //1:经销商
                //else if (client.Model.ClientType == 2 && client.Model["DIClassify"] != "1")
                //    Header.Attributes["WebPageSubCode"] = "ClientClassify=2";    //2:分销商
                //else if (client.Model.ClientType == 3 && client.Model["IsPromote"] == "1")
                //    Header.Attributes["WebPageSubCode"] = "ClientClassify=3";    //3:促销门店
                //else
                //    Header.Attributes["WebPageSubCode"] = "ClientClassify=4";    //4:非促销门店
                #endregion

                BindGrid();
            }
            else
            {
              
            }
            //else
            //{
            //    if ((int)ViewState["ClientType"] == 2)
            //        MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            //    else
            //        MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);

            //    return;
            //}

            #region 判断是否有权限查看出厂价的权限
            switch (Header.Attributes["WebPageSubCode"])
            {
                case "ClientClassify=1":
                    if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 1220, "ViewFactoryPrice")) MCSTabControl1.SelectedIndex = 1;
                    break;
                case "ClientClassify=2":
                    if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 1220, "ViewFactoryPrice2")) MCSTabControl1.SelectedIndex = 1;
                    break;
                case "ClientClassify=3":
                    if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 1120, "ViewFactoryPrice")) MCSTabControl1.SelectedIndex = 1;
                    break;
                case "ClientClassify=4":
                    if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 1120, "ViewFactoryPrice2")) MCSTabControl1.SelectedIndex = 1;
                    break;
                default:
                    MCSTabControl1.SelectedIndex = 1;
                    break;
            }
            #endregion

         
        }

        if ((int)ViewState["ClientType"] == 2)
        {
            MCSTabControl1.Items[1].Visible = false; 
            MCSTabControl1.Items[2].Visible = false;   //经销商客户,不显示零售价
            MCSTabControl1.Items[3].Visible = true;
        }

        if ((int)ViewState["ClientType"] == 3)
        {
            gv_List.Columns[8].Visible = false;                              //本期签收
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;      //下游买赠
            gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;      //下游进货
            gv_List.Columns[gv_List.Columns.Count - 4].Visible = false;      //下游退货
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

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = ddl_BeginMonth.DataSource;
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today).ToString();

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        switch ((int)ViewState["ClientType"])
        {
            case 2:
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&NoParent=Y";
                break;
            case 3:
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=3&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&NoParent=Y";
                break;
            default:
                break;
        }
        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        int client = 0;
        if (int.TryParse(select_Client.SelectValue, out client))
        {
            CM_ClientBLL _bll = new CM_ClientBLL(client);
            if (_bll.Model["DIClassify"] == "2")
            {
                Response.Redirect("JXCSummary_ListSub.aspx?ClientID=" + _bll.Model.ID.ToString() + "&ClientType=2");
            }
        }
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void rbl_IsOpponent_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    #region 绑定销量明细列表
    private void BindGrid()
    {
        int beginmonth = int.Parse(ddl_BeginMonth.SelectedValue);
        int endmonth = int.Parse(ddl_EndMonth.SelectedValue);
        int pricelevel = int.Parse(MCSTabControl1.SelectedTabItem.Value);

        DataTable dt = null;
        if (select_Client.SelectValue != "")
            dt = SVM_JXCSummaryBLL.GetSummaryListByClient(beginmonth, endmonth, pricelevel, int.Parse(select_Client.SelectValue), int.Parse(rbl_IsOpponent.SelectedValue));
        else if (ViewState["ClientClassify"] != null)
            dt = SVM_JXCSummaryBLL.GetSummaryListByClientClassify(beginmonth, endmonth, pricelevel,
                int.Parse(tr_OrganizeCity.SelectValue), (int)ViewState["ClientClassify"], int.Parse(rbl_IsOpponent.SelectedValue));
        else
        {
            //MessageBox.Show(this, "请选择要查询进销存的客户!");
            //return;
        }

        if (rbl_ApproveFlag.SelectedValue != "0")
            dt.DefaultView.RowFilter = "ApproveFlag='" + (rbl_ApproveFlag.SelectedValue == "1" ? "已审核" : "未审核") + "'";
        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();
    }

    protected decimal GetSubJXC(int month, int supplier, string fieldname)
    {
        int pricelevel = int.Parse(MCSTabControl1.SelectedTabItem.Value);
        return SVM_JXCSummaryBLL.GetMonthSummaryBySupplier(month, pricelevel, supplier, fieldname, int.Parse(rbl_IsOpponent.SelectedValue));
    }
    #endregion

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void rbl_ApproveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Edit_Click(object sender, EventArgs e)
    {
        if (select_Client.SelectValue != "")
            Response.Redirect("JXCSummary_Edit.aspx?ClientID=" + select_Client.SelectValue + "&IsOpponent=" + rbl_IsOpponent.SelectedValue);
        else
        {
            //MessageBox.Show(this, "请选择要填报进销存的客户!");
            //return;
        }
    }

    protected void bt_BathApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                int client = int.Parse(gv_List.DataKeys[gr.RowIndex]["Client"].ToString());
                int month = int.Parse(gv_List.DataKeys[gr.RowIndex]["AccountMonth"].ToString());
                SVM_JXCSummaryBLL.Approve(client, month, (int)Session["UserID"], int.Parse(rbl_IsOpponent.SelectedValue));
            }
        }
        BindGrid();
    }


}
