using System;
using System.Data;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;


public partial class SubModule_SVM_InventoryDifferencesDifferencesList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString();
            Header.Attributes["WebPageSubCode"] = "ClientType=" + ViewState["ClientType"].ToString();
            #endregion

            BindDropDown();

            //查找过滤添加：0：所有 1：审核 2：未审核
            if (Request.QueryString["ApproveFlag"] != null)
            {
                Session["ClientID"] = null;
                ViewState["ClientID"] = null;
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];

                if ((int)ViewState["ClientType"] == 3)
                    Header.Attributes["WebPageSubCode"] = "ClientType=3";
                else if ((int)ViewState["ClientType"] == 2)
                {
                    ViewState["DIClassify"] = Request.QueryString["DIClassify"] == null ? 1 : int.Parse(Request.QueryString["DIClassify"]);
                    Header.Attributes["WebPageSubCode"] = "ClientType=2&DIClassify=" + ViewState["DIClassify"].ToString();
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
                CM_ClientBLL _bll = new CM_ClientBLL(int.Parse(ViewState["ClientID"].ToString()));
                if (_bll.Model["LastMonthIsRMSClient"] == "1")
                {
                    bt_BatchInput.Visible = false;
                    bt_BatchInput2.Visible = false;
                }
                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;

                BindGrid();
                #region 设置页面Title
                if ((int)ViewState["ClientType"] == 1)
                    lb_PageTitle.Text = "仓库库存盘盈盘亏";
                else if ((int)ViewState["ClientType"] == 2)
                    lb_PageTitle.Text = "经销商库存盘盈盘亏";
                else if ((int)ViewState["ClientType"] == 3)
                    lb_PageTitle.Text = "零售商库存盘盈盘亏";
                #endregion

                if (client.Model.ClientType == 3)
                    Header.Attributes["WebPageSubCode"] = "ClientType=3";
                else if (client.Model.ClientType == 2)
                    Header.Attributes["WebPageSubCode"] = "ClientType=2&DIClassify=" + client.Model["DIClassify"];
            }
            else
            {
                //if ((int)ViewState["ClientType"] == 2)
                //    MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                //else if ((int)ViewState["ClientType"] == 3)
                //    MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
                //else
                //    MessageBox.ShowAndRedirect(this, "请先在‘仓库列表’中选择要查看的仓库！", "../CM/Store/StoreList.aspx?URL=" + Request.Url.PathAndQuery);
               
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
        #endregion

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (e.CurSelectIndex != 0)
        {
            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + e.CurSelectIndex;
        }
    }
    #endregion

    private void BindGrid()
    {
        if (tr_detail.Visible)
        {
            string condition = " 1=1 ";

            #region 组织查询条件
            //管理片区及所有下属管理片区
            if (select_Client.SelectValue == "" && tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND SVM_InventoryDifferences.OrganizeCity IN (" + orgcitys + ")";
            }

            if (ddl_IsCXP.SelectedValue != "0")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_SVM.dbo.SVM_InventoryDifferences',SVM_InventoryDifferences.ExtPropertys,'IsCXP')=" + ddl_IsCXP.SelectedValue;
            }
            //会计月条件
            condition += " AND SVM_InventoryDifferences.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

            if (select_Client.SelectValue != "")
                condition += " AND SVM_InventoryDifferences.Client = " + select_Client.SelectValue;
            else
            {
                condition += " AND CM_Client.ClientType=" + ViewState["ClientType"].ToString();
                if ((int)ViewState["ClientType"] == 2 && ViewState["DIClassify"] != null)
                {
                    condition += " AND MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,'|',7)='" + ViewState["DIClassify"].ToString() + "'";
                }
            }
            if (rbl_ApproveFlag.SelectedValue != "0")
                condition += " AND SVM_InventoryDifferences.ApproveFlag=" + rbl_ApproveFlag.SelectedValue;
            #endregion

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else
        {
            int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
            int client = select_Client.SelectValue == "" ? 0 : int.Parse(select_Client.SelectValue);
            if (organizecity == 0) organizecity = 1;
            DataTable dt = SVM_InventoryDifferencesBLL.GetSummary(organizecity, client, int.Parse(ddl_BeginMonth.SelectedValue), int.Parse(ddl_EndMonth.SelectedValue), int.Parse(ViewState["ClientType"].ToString()));
            DataRow dr = dt.NewRow();
            dr["ProductCode"] = "合计";
            dr["SumQuantity"] = dt.Compute("Sum(SumQuantity)", "true"); ;
            dr["SumMoney"] = dt.Compute("Sum(SumMoney)", "true"); ;
            dr["SumComput"] = dt.Compute("Sum(SumComput)", "true");
            dr["SumDifferenceCost"] = dt.Compute("Sum(SumDifferenceCost)", "true");
            dt.Columns.Add(new DataColumn("SumDifferenceRate", Type.GetType("System.Decimal")));
            dt.Rows.Add(dr);
            if (dt.Rows.Count > 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["SumDifferenceRate"] = decimal.Parse(row["SumComput"].ToString()) == 0 ? "0" : (decimal.Parse(row["SumDifferenceCost"].ToString()) / decimal.Parse(row["SumComput"].ToString())).ToString("#,#.#");
                }
            }
            gv_Summary.DataSource = dt;
            gv_Summary.DataBind();
        }
    }


    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        new SVM_InventoryDifferencesBLL(int.Parse(gv_List.DataKeys[e.NewSelectedIndex]["SVM_InventoryDifferences_ID"].ToString())).Approve((int)Session["UserID"]);
        BindGrid();
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

    protected void bt_BatchInput_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(select_Client.SelectValue) && select_Client.SelectValue != "0")
        {
            CM_ClientBLL _r = new CM_ClientBLL(int.Parse(select_Client.SelectValue));
            if (_r.Model.ExtPropertys["DIClassify"].ToString() == "3")
            {
                MessageBox.Show(this, "经销商子户头不可以录入库存!");
                return;
            }
            Response.Redirect("InventoryDifferenceInput.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&ClientID=" + select_Client.SelectValue);
        }
        else
        {
            Response.Redirect("InventoryDifferenceInput.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "" && e.SelectValue != "0")
        {
            CM_ClientBLL _bll = new CM_ClientBLL(int.Parse(e.SelectValue));
            if (_bll.Model["LastMonthIsRMSClient"] == "1")
            {
                bt_BatchInput.Visible = false;
                bt_BatchInput2.Visible = false;
            }
            tr_OrganizeCity.SelectValue = _bll.Model.OrganizeCity.ToString();
        }
    }

    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tr_detail.Visible = true;
            tr_summary.Visible = false;
            rbl_ApproveFlag.Visible = true;
            MCSTabControl2.SelectedIndex = 0;
            gv_List.PageIndex = 0;
        }
        else
        {
            tr_detail.Visible = false;
            tr_summary.Visible = true;
            rbl_ApproveFlag.Visible = false;
            MCSTabControl2.SelectedIndex = 1;
        }
        BindGrid();
    }

    public string GetTotalFactoryPriceValue(string deifferenceID)
    {
        return new SVM_InventoryDifferencesBLL(int.Parse(deifferenceID)).GetTotalFactoryPriceValue().ToString("f2");
    }

    protected void bt_BathApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                int id = int.Parse(gv_List.DataKeys[gr.RowIndex]["SVM_InventoryDifferences_ID"].ToString());
                new SVM_InventoryDifferencesBLL(id).Approve((int)Session["UserID"]);
            }
        }
        BindGrid();
    }

    protected void bt_BatchInput2_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(select_Client.SelectValue) && select_Client.SelectValue != "0")
        {
            CM_ClientBLL _r = new CM_ClientBLL(int.Parse(select_Client.SelectValue));
            if (_r.Model.ExtPropertys["DIClassify"].ToString() == "3")
            {
                MessageBox.Show(this, "经销商子户头不可以录入库存!");
                return;
            }
            Response.Redirect("InventoryDifferenceInput.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&ClientID=" + select_Client.SelectValue + "&IsCXP=1");
        }
        else
        {
            Response.Redirect("InventoryDifferenceInput.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&IsCXP=1");
        }

    }
    protected string GetDifferenceCost(string differenceID)
    {
        int ID = 0;
        if (int.TryParse(differenceID, out  ID))
        {
            SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL(ID);
            decimal computeinventory = GetCompute(differenceID);
            return Math.Abs(bll.GetTotalFactoryPriceValue() - computeinventory).ToString();
        }
        else
        {
            return "";
        }
    }

    protected string GetDifferenceRate(string differenceID)
    {
        int ID = 0;
        if (int.TryParse(differenceID, out  ID))
        {
            SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL(ID);
            decimal computeinventory = GetCompute(differenceID);
            return computeinventory == 0 ? "0" : (Math.Abs(bll.GetTotalFactoryPriceValue() - computeinventory) / computeinventory).ToString();
        }
        else
        {
            return "";
        }
    }
    protected decimal GetCompute(string differenceID)
    {
        int ID = 0; decimal computeinventory = 0;
        if (int.TryParse(differenceID, out  ID))
        {
            SVM_InventoryDifferencesBLL bll = new SVM_InventoryDifferencesBLL(ID);
            DataTable dtCompute = SVM_JXCSummaryBLL.GetSummaryListByClient(bll.Model.AccountMonth, bll.Model.AccountMonth, 1, bll.Model.Client, 1);
            computeinventory = dtCompute.Rows.Count > 0 ? Convert.ToDecimal(dtCompute.Rows[0]["ComputInventory"]) : 0;
            return computeinventory;
        }
        return computeinventory;
    }
    protected string GetDifferenceRate(string difference, string ComputInventory)
    {
        int differenceQ = 0, ComputInventoryQ = 0;
        int.TryParse(difference, out differenceQ);
        int.TryParse(ComputInventory, out ComputInventoryQ);
        return ComputInventoryQ == 0 ? "0" : (Math.Abs(ComputInventoryQ - differenceQ) / ComputInventoryQ).ToString("#,#.#");
    }
}