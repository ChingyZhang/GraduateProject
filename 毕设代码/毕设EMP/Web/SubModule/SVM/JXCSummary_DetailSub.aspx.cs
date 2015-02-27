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

public partial class SubModule_SVM_JXCSummary_DetailSub : System.Web.UI.Page
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

            if (Request.QueryString["IsOpponent"] != null) rbl_IsOpponent.SelectedValue = Request.QueryString["IsOpponent"];
            #endregion

            BindDropDown();

            if (Request.QueryString["AccountMonth"] != null) ddl_Month.SelectedValue = Request.QueryString["AccountMonth"];

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }

                #region 载入客户信息
                ViewState["ClientType"] = client.Model.ClientType;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
         

                BindGrid();
                #endregion

                #region 判断传入客户参数的客户类别
                if (client.Model.ClientType == 2 && client.Model["DIClassify"] == "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=1";    //1:经销商
                else if (client.Model.ClientType == 2 && client.Model["DIClassify"] != "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=2";    //2:分销商
                else if (client.Model.ClientType == 3 && client.Model["IsPromote"] == "1")
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=3";    //3:促销门店
                else
                    Header.Attributes["WebPageSubCode"] = "ClientClassify=4";    //4:非促销门店
                #endregion
            }
            else
            {
                Response.Redirect("~/SubModule/desktop.aspx");
            }
        }
        if ((int)ViewState["ClientType"] == 2)
        {
            MCSTabControl1.Items[2].Visible = false;
            MCSTabControl1.Items[3].Visible = false;             //经销商客户,不显示零售价
            MCSTabControl1.Items[4].Visible = true;

        }
        if ((int)ViewState["ClientType"] == 3)
        {
            MCSTab_DisplayMode.Items[1].Visible = false;     //终端门店无下游客户
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;      //下游买赠
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;      //下游进货
            gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;      //下游退货
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent =" + rbl_IsOpponent.SelectedValue);
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand.SelectedValue = "0";
        rbl_Brand_SelectedIndexChanged(null, null);
    }
    protected void rbl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
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
        }
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

    protected void rbl_IsOpponent_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent =" + rbl_IsOpponent.SelectedValue);
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand.SelectedValue = "0";
        rbl_Brand_SelectedIndexChanged(null, null);

        gv_List.PageIndex = 0;
        BindGrid();
    }
    #endregion
    protected void ddl_Month_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("JXCSummary_Edit.aspx?ClientID=" + select_Client.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue + "&IsOpponent=" + rbl_IsOpponent.SelectedValue);
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);

        if (select_Client.SelectValue == "")
        {
            MessageBox.Show(this, "对不起，您必须选择一个客户进行查询！");
            return;
        }
        string condition = "AccountMonth=" + month.ToString() + " AND Client=" + select_Client.SelectValue;
        if (ddl_Classify.SelectedValue != "0")
        {
            condition += " AND Product IN (SELECT ID FROM MCS_PUB.dbo.PDT_Product WHERE Classify=" + ddl_Classify.SelectedValue + ")";
        }
        else if (ddl_Brand.SelectedValue != "0")
        {
            condition += " AND Product IN (SELECT ID FROM MCS_PUB.dbo.PDT_Product WHERE Brand=" + ddl_Brand.SelectedValue + ")";
        }
        else
        {
            condition += @" AND SVM_JXCSummary.Product IN (SELECT PDT_Product.ID FROM MCS_PUB.dbo.PDT_Product 
			            INNER JOIN MCS_Pub.dbo.PDT_Brand ON PDT_Product.Brand = PDT_Brand.ID AND PDT_Brand.IsOpponent=" + rbl_IsOpponent.SelectedValue + ")";
        }
        IList<SVM_JXCSummary> lists = SVM_JXCSummaryBLL.GetModelList(condition);
        gv_List.BindGrid(lists.Where(p => Math.Abs(p.BeginningInventory) + Math.Abs(p.PurchaseVolume) + Math.Abs(p.TransitInventory) + Math.Abs(p.RecallVolume) +
            Math.Abs(p.SalesVolume) + Math.Abs(p.GiftVolume) + Math.Abs(p.ReturnedVolume) + Math.Abs(p.EndingInventory) + Math.Abs(p.StaleInventory) +
            Math.Abs(p.TransferInVolume) + Math.Abs(p.TransferOutVolume) + Math.Abs(p.ExpiredInventory) > 0).OrderBy(p => p.SubUnit).ToList());

        if (lists.Count > 0 && lists.Where(p => p.ApproveFlag == 2).Count() > 0)
        {
            bt_Edit.Enabled = true;
            bt_Approve.Enabled = true;
        }
        else
        {
            bt_Edit.Enabled = false;
            bt_Approve.Enabled = false;
        }
    }

    protected decimal GetSubJXC(string fieldname, int product)
    {
        if ((int)ViewState["ClientType"] != 3)
        {
            if ((gv_List.Columns[gv_List.Columns.Count - 1].Visible && fieldname == "GiftVolume") ||
                (gv_List.Columns[gv_List.Columns.Count - 2].Visible && fieldname == "PurchaseVolume") ||
                (gv_List.Columns[gv_List.Columns.Count - 3].Visible && fieldname == "ReturnedVolume"))
            {
                int month = int.Parse(ddl_Month.SelectedValue);
                int pricelevel = int.Parse(MCSTabControl1.SelectedTabItem.Value);
                int supplier = int.Parse(select_Client.SelectValue);

                return SVM_JXCSummaryBLL.GetMonthSummaryBySupplier(month, pricelevel, supplier, fieldname, product, int.Parse(rbl_IsOpponent.SelectedValue));
            }
            else
                return 0;
        }
        return 0;
    }
    #endregion

    protected decimal GetJXCData(int product, int Quantity, decimal FactoryPrice, decimal SalesPrice, decimal RetailPrice)
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
            case 4:
                {
                    decimal preice = new PDT_ProductBLL(product).Model.FactoryPrice;
                    return Quantity * preice;
                }
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

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        int client = 0, month = 0;
        if (int.TryParse(select_Client.SelectValue, out client) && int.TryParse(ddl_Month.SelectedValue, out month))
        {
            SVM_JXCSummaryBLL.Approve(client, month, (int)Session["UserID"], int.Parse(rbl_IsOpponent.SelectedValue));
            MessageBox.ShowAndRedirect(this, "已将进销存设为审核通过!", "JXCSummary_List.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void MCSTab_DisplayMode_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            if (select_Client.SelectValue != "")
                Response.Redirect("JXCSummary_Detail2.aspx?ClientID=" + select_Client.SelectValue + "&AccountMonth=" + ddl_Month.SelectedValue);
            else
            {
                MessageBox.Show(this, "请选择要查询进销存的客户!");
                return;
            }
        }
    }
}
