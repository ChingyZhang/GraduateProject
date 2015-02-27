using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_SVM_SalesVolumeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["Flag"] != null)
            {
                //有Flag参数,表示从销售管理主菜单进入,否则表示从客户管理菜单进入
                Session["ClientID"] = null;
                Header.Attributes["WebPageSubCode"] = "Flag=0&";
            }
            else
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null)
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                Header.Attributes["WebPageSubCode"] = "";
            }

            //销量类型，1:经销商进货 2:门店进货，3:门店销量
            ViewState["Type"] = Request.QueryString["Type"] == null ? 1 : int.Parse(Request.QueryString["Type"]);

            Header.Attributes["WebPageSubCode"] += "Type=" + ViewState["Type"].ToString();
            #endregion

            BindDropDown();

            if (Request.QueryString["SalesFlag"] != null && ddl_SalesFlag.Items.FindByValue(Request.QueryString["SalesFlag"]) != null)
            {
                ViewState["SalesFlag"] = int.Parse(Request.QueryString["SalesFlag"]);
                ddl_SalesFlag.SelectedValue = Request.QueryString["SalesFlag"];
            }

            //查找过滤添加：0：所有 1：审核 2：未审核
            if (Request.QueryString["ApproveFlag"] != null)
            {
                Session["ClientID"] = null;
                ViewState["ClientID"] = null;
                rbl_ApproveFlag.SelectedValue = Request.QueryString["ApproveFlag"];
            }

            tbx_begin.Text = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10))).Model.BeginDate.ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            switch ((int)ViewState["Type"])
            {
                case 1:     //办事处仓库出货（经销商进货）
                    lb_Supplier.Text = "办事处仓库:";
                    select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=1";
                    lb_Client.Text = "经销商:";
                    select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
                    break;
                case 2:     //经销商出货（终端门店进货）
                    lb_Supplier.Text = "经销商:";
                    select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2";
                    lb_Client.Text = "分销商、零售商:";
                    select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ExtCondition=\" ClientType=3 OR MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=2\"";
                    break;
                case 3:     //终端门店销售
                    lb_Supplier.Text = "零售商:";
                    select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=3";
                    lb_Client.Visible = false;
                    select_Client.Visible = false;
                    gv_List.Columns[5].Visible = false;

                    break;
                case 4:     //总部发货(办事处仓库进货)
                    lb_Supplier.Visible = false;
                    select_Supplier.Visible = false;
                    lb_Client.Text = "办事处仓库:";
                    select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=1";
                    break;
            }

            if (Request.QueryString["SellOutClientID"] != null || Request.QueryString["SellInClientID"] != null)
            {
                switch ((int)ViewState["Type"])
                {
                    case 1:     //经销商进货
                    case 2:     //经销商出货（终端门店进货）
                        if (Request.QueryString["SellOutClientID"] != null)
                        {
                            CM_ClientBLL _s = new CM_ClientBLL(int.Parse(Request.QueryString["SellOutClientID"]));
                            select_Supplier.SelectText = _s.Model.FullName;
                            select_Supplier.SelectValue = _s.Model.ID.ToString();
                            select_Supplier.Enabled = false;
                            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ExtCondition=\"Supplier = " + _s.Model.ID.ToString() + "\"";
                        }
                        if (Request.QueryString["SellInClientID"] != null)
                        {
                            CM_ClientBLL _r = new CM_ClientBLL(int.Parse(Request.QueryString["SellInClientID"]));
                            select_Client.SelectText = _r.Model.FullName;
                            select_Client.SelectValue = _r.Model.ID.ToString();
                            select_Client.Enabled = false;                             
                            //一级商进货，在发货单中显示
                            if ((int)ViewState["Type"] == 1 && _r.Model["DIClassify"] == "1"&&Request.QueryString["SalesFlag"] == null)
                            {
                                Response.Redirect("~/SubModule/Logistics/Delivery/OrderDeliveryList.aspx?ClientID=" + _r.Model.ID.ToString());
                            }
                        }
                        break;
                    case 3:     //终端门店销售
                        if (Request.QueryString["SellOutClientID"] != null)
                        {
                            CM_ClientBLL _s = new CM_ClientBLL(int.Parse(Request.QueryString["SellOutClientID"]));
                            select_Supplier.SelectText = _s.Model.FullName;
                            select_Supplier.SelectValue = _s.Model.ID.ToString();
                            select_Supplier.Enabled = false;
                        }
                        break;
                    case 4:     //办事处仓库进货
                        if (Request.QueryString["SellInClientID"] != null)
                        {
                            CM_ClientBLL _r = new CM_ClientBLL(int.Parse(Request.QueryString["SellInClientID"]));
                            select_Client.SelectText = _r.Model.FullName;
                            select_Client.SelectValue = _r.Model.ID.ToString();
                            select_Client.Enabled = false;
                        }
                        break;
                }
            }
            else if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL _r = new CM_ClientBLL((int)ViewState["ClientID"]);

                switch ((int)ViewState["Type"])
                {
                    case 1:     //办事处仓库出货（经销商进货）
                    case 2:     //经销商出货（终端门店进货）
                    case 3:     //终端门店销售
                        if (_r.Model.ClientType == (int)ViewState["Type"])
                        {
                            Response.Redirect("SalesVolumeList.aspx?Type=" + ViewState["Type"].ToString() + "&SellOutClientID=" + ViewState["ClientID"].ToString());
                            return;
                        }
                        else if (_r.Model.ClientType == (int)ViewState["Type"] + 1)
                        {
                            Response.Redirect("SalesVolumeList.aspx?Type=" + ViewState["Type"].ToString() + "&SellInClientID=" + ViewState["ClientID"].ToString());
                            return;
                        }
                        else
                        {
                            Session["ClientID"] = null;
                            Response.Redirect(Request.Url.PathAndQuery);
                        }
                        break;
                    case 4:     //办事处仓库进货
                        if (_r.Model.ClientType == 1)
                        {
                            Response.Redirect("SalesVolumeList.aspx?Type=" + ViewState["Type"].ToString() + "&SellInClientID=" + ViewState["ClientID"].ToString());
                            return;
                        }
                        else
                        {
                            Session["ClientID"] = null;
                            Response.Redirect(Request.Url.PathAndQuery);
                        }
                        break;
                }
            }
            else if (Request.QueryString["ApproveFlag"] == null)
            {
                switch ((int)ViewState["Type"])
                {
                    case 1:     //办事处仓库出货（经销商进货）
                    case 2:     //经销商出货（终端门店进货）
                        MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                        break;
                    case 3:     //终端门店销售
                        MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
                        break;
                    case 4:     //办事处仓库进货
                        MessageBox.ShowAndRedirect(this, "请先在‘仓库列表’中选择要查看的仓库！", "../CM/Store/StoreList.aspx?URL=" + Request.Url.PathAndQuery);
                        break;
                }
            }

            switch ((int)ViewState["Type"])
            {
                case 1:     //经销商进货
                case 4:     //仓库进货
                    foreach (DataControlField item in gv_List.Columns)
                    {
                        if (item.HeaderText == "导购员") item.Visible = false;
                    }
                    gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;         //隐藏批发价
                    gv_Summary.Columns[gv_Summary.Columns.Count - 1].Visible = false;   //隐藏批发价
                    break;
                case 2:     //经销商出货（终端门店进货）
                    foreach (DataControlField item in gv_List.Columns)
                    {
                        if (item.HeaderText == "导购员") item.Visible = false;
                    }
                    gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;         //隐藏出厂价
                    gv_Summary.Columns[gv_Summary.Columns.Count - 2].Visible = false;   //隐藏出厂价
                    break;
                case 3:     //终端门店销售
                    foreach (DataControlField item in gv_List.Columns)
                    {
                        if (item.HeaderText == "销售单号") item.Visible = false;
                    }
                    //gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;         //隐藏出厂价
                    //gv_Summary.Columns[gv_Summary.Columns.Count - 2].Visible = false;   //隐藏出厂价
                    break;
            }

            BindGrid();
        }
        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeBack") && !Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1114, "AddSalesVolumeIN"))
        {
            bt_BatchInput.Visible = false;
            bt_BatchInput2.Visible = false;
        }

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        #region 检查门店销量
        if ((int)ViewState["Type"] == 3 && Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1103, "CheckSalesVolume"))
        {
            bt_BatchInput.Visible = true;
        }
        #endregion
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

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";

        ddl_SalesFlag.DataSource = DictionaryBLL.GetDicCollections("SVM_SalesFlag");
        ddl_SalesFlag.DataBind();
        ddl_SalesFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_SalesFlag.SelectedValue = "0";
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        switch ((int)ViewState["Type"])
        {
            case 1:     //办事处仓库出货（经销商进货）
                select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=1" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
            case 2:     //经销商出货（终端门店进货）
                select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=2" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=3" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
            case 3:     //终端门店销售
                select_Supplier.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=3" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
            case 4:     //办事处仓库进货
                select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ClientType=1" + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                break;
        }

        select_Supplier.SelectText = "";
        select_Supplier.SelectValue = "";

        select_Client.SelectText = "";
        select_Client.SelectValue = "";
    }

    protected void select_Supplier_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
            select_Client.PageUrl = "../CM/PopSearch/Search_SelectClient.aspx?ExtCondition=\"Supplier = " + e.SelectValue + "\"";
            select_Client.SelectValue = "";
            select_Client.SelectText = "";
        }
    }
    #endregion
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        
        if (e.SelectValue != "" && (int)ViewState["Type"] == 1)
        {
            if (ViewState["SalesFlag"] == null)
            {
                Response.Redirect("SalesVolumeList.aspx?Type=1&SellInClientID=" + e.SelectValue);
            }
            else
            {
                Response.Redirect("SalesVolumeList.aspx?Type=1&SellInClientID=" + e.SelectValue + "&SalesFlag=" + ViewState["SalesFlag"].ToString());
            }
        }
    }
    private void BindGrid()
    {
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);

        if (tr_detail.Visible)
        {
            string condition = " SVM_SalesVolume.SalesDate between '" + dtBegin.ToString() + "' AND '" + dtEnd.ToString() + "'";

            if (rbl_ApproveFlag.SelectedValue != "0")
            {
                condition += " And SVM_SalesVolume.ApproveFlag =" + rbl_ApproveFlag.SelectedValue;
            }

            if (ddl_SalesFlag.SelectedValue != "0")
            {
                condition += " And SVM_SalesVolume.Flag =" + ddl_SalesFlag.SelectedValue;
            }
            //如果是经销商登录，只显示该经销商录入的门店进货
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);

            if (!string.IsNullOrEmpty(staff.Model["RelateClient"]))
            {
                select_Supplier.SelectValue = "";
                condition += " AND SVM_SalesVolume.Supplier = " + staff.Model["RelateClient"];
            }


            //管理片区及所有下属管理片区
            //销量查询可以不根据某个指定的客户，根据一个片区范围来查找
            if (select_Supplier.SelectValue != "")
            {
                condition += " AND SVM_SalesVolume.Supplier = " + select_Supplier.SelectValue;
            }

            if (select_Client.SelectValue != "")
            {
                condition += " AND SVM_SalesVolume.Client =" + select_Client.SelectValue;
            }

            if (select_Supplier.SelectValue == "" && select_Client.SelectValue == "")
                condition += " AND SVM_SalesVolume.Type = " + ViewState["Type"].ToString();


            if (organizecity > 1 && select_Supplier.SelectValue == "" && select_Client.SelectValue == "")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND SVM_SalesVolume.OrganizeCity IN (" + orgcitys + ")";
            }

            gv_List.ConditionString = condition;
            gv_List.BindGrid();
        }
        else
        {
            int supplier = select_Supplier.SelectValue == "" ? 0 : int.Parse(select_Supplier.SelectValue);
            int client = select_Client.SelectValue == "" ? 0 : int.Parse(select_Client.SelectValue);
            if (organizecity == 0) organizecity = 1;

            DataTable dt = SVM_SalesVolumeBLL.GetSummary(organizecity, supplier, client, dtBegin, dtEnd, (int)ViewState["Type"], int.Parse(ddl_SalesFlag.SelectedValue));
            int _quantity = 0;
            decimal _totalvalue = 0, _totalfactoryvalue = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _quantity += (int)dt.Rows[i]["SumQuantity"];
                _totalvalue += (decimal)dt.Rows[i]["SumMoney"];
                _totalfactoryvalue += (decimal)dt.Rows[i]["SumFactoryMoney"];
            }
            DataRow dr = dt.NewRow();
            dr["ProductCode"] = "合计";
            dr["SumQuantity"] = _quantity;
            dr["SumMoney"] = _totalvalue;
            dr["SumFactoryMoney"] = _totalfactoryvalue;
            dt.Rows.Add(dr);
            gv_Summary.DataSource = dt;
            gv_Summary.DataBind();
        }


    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tr_detail.Visible = true;
            tr_summary.Visible = false;
            rbl_ApproveFlag.Visible = true;
            MCSTabControl1.SelectedIndex = 0;
            gv_List.PageIndex = 0;
        }
        else
        {
            tr_detail.Visible = false;
            tr_summary.Visible = true;
            rbl_ApproveFlag.Visible = false;
            MCSTabControl1.SelectedIndex = 1;
        }
        BindGrid();
    }

    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.item.Value == "1")
            Response.Redirect("AdvanceFind/SalesVolumeAdvanceFind.aspx?Type=" + ViewState["Type"].ToString());
    }

    protected void rbl_ApproveFlag_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    public string GetTotalValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalValue().ToString("f2");
    }

    public string GetTotalFactoryPriceValue(string SalesVolumeID)
    {
        return new SVM_SalesVolumeBLL(int.Parse(SalesVolumeID)).GetTotalFactoryPriceValue().ToString("f2");
    }

    protected void bt_BatchInput_Click(object sender, EventArgs e)
    {
        int clientid = 0;

        switch ((int)ViewState["Type"])
        {
            case 1:     //办事处仓库出货（经销商进货）
            case 2:     //经销商出货（终端门店进货）
            case 4:     //办事处仓库进货
                int.TryParse(select_Client.SelectValue, out clientid);
                break;
            case 3:     //终端门店销售
                int.TryParse(select_Supplier.SelectValue, out clientid);
                break;
        }

        if (clientid == 0)
        {
            MessageBox.Show(this, "请选择客户!");
            return;
        }

        CM_ClientBLL _r = new CM_ClientBLL(clientid);
        if (_r.Model.ExtPropertys["DIClassify"].ToString() == "3")
        {
            MessageBox.Show(this, "经销商子户头不可以录入销量!");
            return;
        }
        if (ViewState["SalesFlag"] == null)
        {
            Response.Redirect("SalesVolumeBatchInput.aspx?Type=" + ViewState["Type"].ToString() + "&ClientID=" + clientid.ToString());
        }
        else
        {
            Response.Redirect("SalesVolumeBatchInput.aspx?Type=" + ViewState["Type"].ToString() + "&ClientID=" + clientid.ToString() + "&SalesFlag=" + ViewState["SalesFlag"].ToString());
        }
    }

    protected void bt_BatchInput2_Click(object sender, EventArgs e)
    {
        int clientid = 0;

        switch ((int)ViewState["Type"])
        {
            case 1:     //办事处仓库出货（经销商进货）
            case 2:     //经销商出货（终端门店进货）
            case 4:     //办事处仓库进货
                int.TryParse(select_Client.SelectValue, out clientid);
                break;
            case 3:     //终端门店销售
                int.TryParse(select_Supplier.SelectValue, out clientid);
                break;
        }

        if (clientid == 0)
        {
            MessageBox.Show(this, "请选择客户!");
            return;
        }

        CM_ClientBLL _r = new CM_ClientBLL(clientid);
        if (_r.Model.ExtPropertys["DIClassify"].ToString() == "3")
        {
            MessageBox.Show(this, "经销商子户头不可以录入销量!");
            return;
        }

        Response.Redirect("SalesVolumeBatchInput.aspx?Type=" + ViewState["Type"].ToString() + "&ClientID=" + clientid.ToString() + "&IsCXP=1");
    }

    protected void bt_BathApprove_Click(object sender, EventArgs e)
    {
        string ids = "";
        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                ids += gv_List.DataKeys[gr.RowIndex]["SVM_SalesVolume_ID"].ToString() + ",";
            }
        }

        SVM_SalesVolumeBLL.BatApprove(ids, (int)Session["UserID"]);
        BindGrid();
    }
}
