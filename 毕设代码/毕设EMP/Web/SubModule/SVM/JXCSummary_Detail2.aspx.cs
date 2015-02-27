using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using MCSFramework.Model.Pub;

public partial class SubModule_SVM_JXCSummary_Detail2 : System.Web.UI.Page
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

            //客户分类：0:仓库 1:经销商 2:分销商
            ViewState["ClientClassify"] = Request.QueryString["ClientClassify"] == null ? 0 : int.Parse(Request.QueryString["ClientClassify"]);
            #endregion

            BindDropDown();

            if (Request.QueryString["AccountMonth"] != null) ddl_Month.SelectedValue = Request.QueryString["AccountMonth"];

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                #region 判断传入客户参数的客户类别
                int classify = 0;
                if (client.Model.ClientType == 1)
                    classify = 0;
                else if (client.Model.ClientType == 2 && client.Model["DIClassify"] == "1")
                    classify = 1;    //1:经销商
                else if (client.Model.ClientType == 2 && client.Model["DIClassify"] != "1")
                    classify = 2;    //2:分销商
                else
                {
                    MessageBox.ShowAndRedirect(this, "当前客户类型无下游客户", "../desktop.aspx");
                    return;
                }
                #endregion

                #region 载入客户信息
                if (Request.QueryString["ClientClassify"] == null || (int)ViewState["ClientClassify"] == classify)
                {
                    select_Supplier.SelectValue = ViewState["ClientID"].ToString();
                    select_Supplier.SelectText = client.Model.FullName;

                    tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                    ViewState["ClientClassify"] = classify;

                    select_Supplier.Enabled = false;
                    tr_OrganizeCity.Enabled = false;
                    BindGrid();
                }
                #endregion

                Header.Attributes["WebPageSubCode"] = "ClientClassify=" + ViewState["ClientClassify"].ToString();
            }
            //else
            //{
            //    switch ((int)ViewState["ClientClassify"])
            //    {
            //        case 0:
            //            MessageBox.ShowAndRedirect(this, "请先在‘办事处列表’中选择要查看的办事处！", "../CM/Store/StoreList.aspx?URL=" + Request.Url.PathAndQuery);
            //            break;
            //        case 1:
            //        case 2:
            //            MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }
        if ((int)ViewState["ClientClassify"] == 0) MCSTab_DisplayMode.Items[0].Visible = false;     //仓库无本身的进销存数据
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        switch ((int)ViewState["ClientClassify"])
        {
            case 0:
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=1&ExtCondition=\"CM_Client.OrganizeCity>1\"&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&NoParent=Y";
                break;
            case 1:
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.uf_spilt(CM_Client.ExtPropertys,~|~,7)=1\"&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&NoParent=Y";
                break;
            case 2:
                select_Supplier.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.uf_spilt(CM_Client.ExtPropertys,~|~,7)=2\"&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&NoParent=Y";
                break;
            default:
                break;
        }
        select_Supplier.SelectText = "";
        select_Supplier.SelectValue = "";
    }
    protected void select_Supplier_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        if (e.SelectValue != "")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(e.SelectValue)).Model.OrganizeCity.ToString();
        }
    }
    #endregion

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        gv_ListByProduct.PageIndex = 0;
        BindGrid();
    }
    protected void rbl_IsOpponent_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_ListByProduct.PageIndex = 0;
        BindGrid();
    }
    protected void rbl_DisplayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("JXCSummary_Edit.aspx?ClientID=" + select_Supplier.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue);
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int pricelevel = int.Parse(MCSTabControl1.SelectedTabItem.Value);
        int supplier = 0;

        if (!int.TryParse(select_Supplier.SelectValue, out supplier))
        {
            MessageBox.Show(this, "请选择要查询进销存的供货应或客户!");
            return;
        }

        if (rbl_DisplayType.SelectedValue == "1")
        {
            //按客户显示
            MCSTabControl1.Items[0].Visible = false;
            if (pricelevel == 0)
            {
                //按客户显示时，无按数量统计
                MCSTabControl1.SelectedIndex = 1;
                MCSTabControl1.SelectedIndex = 1;
                pricelevel = 1;
            }
            DataTable dt = SVM_JXCSummaryBLL.GetSummaryListBySupplier(month, month, pricelevel, supplier, int.Parse(rbl_IsOpponent.SelectedValue));

            MatrixTable.TableAddSummaryRow(dt, "OrganizeCityName",
                new string[] { "BeginningInventory", "PurchaseVolume", "TransitInventory", "RecallVolume", "SalesVolume", 
                    "ReturnedVolume", "GiftVolume","EndingInventory","ComputInventory","StaleInventory","TransferInVolume","TransferOutVolume"});
            gv_ListByClient.DataSource = dt;
            gv_ListByClient.DataBind();

            int lastrow = gv_ListByClient.Rows.Count - 1;
            if (gv_ListByClient.Rows[lastrow].Cells[1].Text == "总计") gv_ListByClient.Rows[lastrow].Cells[0].Text = "";
        }
        else
        {
            //按产品显示
            MCSTabControl1.Items[0].Visible = true;
            DataTable dt = SVM_JXCSummaryBLL.GetProductListBySupplier(supplier, month, pricelevel, int.Parse(rbl_IsOpponent.SelectedValue));
            dt.DefaultView.Sort = "ProductName ASC";
            MatrixTable.TableAddSummaryRow(dt, "ProductName",
                new string[] { "BeginningInventory", "PurchaseVolume", "TransitInventory", "RecallVolume", "SalesVolume", 
                    "ReturnedVolume", "GiftVolume","EndingInventory","ComputInventory","StaleInventory","TransferInVolume","TransferOutVolume"});

            dt.DefaultView.RowFilter = "BeginningInventory + PurchaseVolume + TransitInventory + RecallVolume + SalesVolume + GiftVolume + ReturnedVolume + EndingInventory + StaleInventory+TransferInVolume+TransferOutVolume<>0";
            gv_ListByProduct.DataSource = dt.DefaultView;
            gv_ListByProduct.DataBind();
        }

        gv_ListByClient.Visible = rbl_DisplayType.SelectedValue == "1";
        gv_ListByProduct.Visible = !gv_ListByClient.Visible;
    }
    #endregion

    protected void gv_ListByClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListByClient.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected decimal GetJXCData(int Quantity, decimal FactoryPrice, decimal SalesPrice, decimal RetailPrice)
    {
        switch (MCSTabControl1.SelectedIndex)
        {
            case 0:
                return Quantity;
            case 1:
                return Quantity * FactoryPrice;
            case 2:
                return Quantity * SalesPrice;
            case 3:
                return Quantity * RetailPrice;
            default:
                return Quantity;
        }
    }
    protected string GetPDTBrandName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;
        PDT_Brand b = new PDT_BrandBLL(p.Brand).Model;

        if (b != null)
            return b.Name;
        else
            return "";
    }
    protected string GetPDTClassifyName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;
        PDT_Classify c = new PDT_ClassifyBLL(p.Classify).Model;

        if (c != null)
            return c.Name;
        else
            return "";
    }

    protected void MCSTab_DisplayMode_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            if (select_Supplier.SelectValue == "")
                Response.Redirect("JXCSummary_Detail.aspx?ClientClassify=" + ViewState["ClientClassify"].ToString());
            else
                Response.Redirect("JXCSummary_Detail.aspx?ClientID=" + select_Supplier.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue);
        }
    }



}
