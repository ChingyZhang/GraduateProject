using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;

public partial class SubModule_Logistics_Order_OrderApplyDetail1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            if (Session["LogisticsOrderApplyDetail"] != null)
            {
                BindData();
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "参数错误!", "OrderApplyDetail0.aspx");
                return;
            }
        }
    }

    private void BindDropDown()
    {
    }

    #region 界面绑定
    private void BindData()
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL(cart.Publish);
        if (publish == null) Response.Redirect("OrderApplyDetail0.aspx");

        ViewState["Publish"] = cart.Publish;
        pl_ApplyPublish.BindData(publish.Model);

        IList<ORD_ApplyPublishDetail> productlists;
        if (cart.Type == 1)
        {
            #region 成品取客户价表
            productlists = new List<ORD_ApplyPublishDetail>();
            IList<PDT_ProductPrice> pricelists = PDT_ProductPriceBLL.GetModelList
                ("GETDATE() BETWEEN BeginDate AND EndDate AND Client=" + cart.Client.ToString());
            if (pricelists.Count == 0)
            {
                MessageBox.ShowAndRedirect(this, "对不起，该客户没有生效的价表，不可申请产品!", "OrderApplyDetail0.aspx");
                return;
            }

            PDT_ProductPriceBLL pricebll = new PDT_ProductPriceBLL(pricelists[0].ID);

            foreach (ORD_ApplyPublishDetail item in publish.Items)
            {
                PDT_ProductBLL _bll = new PDT_ProductBLL(item.Product);

                if (new PDT_BrandBLL(_bll.Model.Brand).Model.IsOpponent == "1")
                {
                    PDT_ProductPrice_Detail priceitem = pricebll.Items.FirstOrDefault(p => p.Product == item.Product);
                    if (priceitem == null) continue;
                    item.Price = priceitem.BuyingPrice;
                }
                else
                {
                    item.Price = _bll.Model.FactoryPrice;
                }

                productlists.Add(item);
            }
            #endregion
        }
        else
        {
            #region 绑定赠品申请预算信息
            BindBudgetInfo(cart.OrganizeCity, cart.AccountMonth, cart.Client, cart.GiftFeeType, cart.Brand, cart.GiftClassify,cart.Receiver);
            tb_GiftBudgetInfo.Visible = true;
            #endregion


            productlists = publish.Items;
        }

        ViewState["PublishDetails"] = productlists;
        gv_List.PageIndex = 0;
        BindGrid();

        lb_CartCount.Text = cart.Items.Count.ToString();
    }

    #region 获取当前管理片区的预算信息
    private void BindBudgetInfo(int city, int month, int client, int feetype, int productbrand, int giftclassify,int receiver)
    {
        lb_OrganizeCityName.Text = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", city);
        lb_ClientName.Text = client == 0 ? "" : new CM_ClientBLL(client).Model.FullName;
        lb_Receiver.Text = (receiver == 0 || receiver==null )? "" : new CM_ClientBLL(receiver).Model.FullName;

        decimal totalbudget = FNA_BudgetBLL.GetUsableAmount(month, city, feetype, false);
        lb_TotalBudget.Text = totalbudget.ToString("0.##");

        IList<ORD_GiftApplyAmount> giftamounts = ORD_GiftApplyAmountBLL.GetModelList(
            string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}",
            month, client, productbrand, giftclassify));
        if (giftamounts.Count > 0)
        {
            decimal available = giftamounts[0].AvailableAmount + giftamounts[0].PreBalance - giftamounts[0].DeductibleAmount;
            decimal balance = giftamounts[0].BalanceAmount;

            lb_AvailableAmount.Text = available.ToString("0.##");
            lb_BalanceAmount.Text = balance.ToString("0.##");
            //2012-3-27 暂时只取赠品额度，不取预算
            //lb_BalanceAmount.Text = (totalbudget > balance ? balance : totalbudget).ToString("0.##");
        }
        else
        {
            lb_AvailableAmount.Text = "0";
            lb_BalanceAmount.Text = "0";
        }

        hl_ViewBudget.NavigateUrl = "~/SubModule/FNA/Budget/BudgetBalance.aspx?OrganizeCity=" + city.ToString();
    }
    #endregion

    private void BindGrid()
    {
        IList<ORD_ApplyPublishDetail> PublishDetails = (IList<ORD_ApplyPublishDetail>)ViewState["PublishDetails"];
        IList<ORD_ApplyPublishDetail> _lists;

        if (tbx_Find.Text.Trim() != "")
        {
            _lists = new List<ORD_ApplyPublishDetail>();
            foreach (ORD_ApplyPublishDetail item in PublishDetails)
            {
                if (new PDT_ProductBLL(item.Product).Model.FullName.Contains(tbx_Find.Text)) _lists.Add(item);
            }
        }
        else
            _lists = PublishDetails;

        gv_List.DataSource = _lists.OrderByDescending(p => p.Price).ThenBy(p => p["GiveLevel"]).ToList();
        gv_List.DataBind();
        cb_SelectAll.Checked = false;
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (Session["LogisticsOrderApplyDetail"] != null)
        {
            ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
            foreach (GridViewRow row in gv_List.Rows)
            {
                int product = (int)gv_List.DataKeys[row.RowIndex]["Product"];

                if (cart.Items.FirstOrDefault(m => m.Product == product) != null)
                {
                    row.FindControl("cb_Check").Visible = false;
                    row.FindControl("imbt_Select").Visible = false;
                }
            }
        }
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    #endregion

    #region 提供界面需要的信息
    protected string GetProductInfo(int ProductID, string FieldName)
    {
        return new PDT_ProductBLL(ProductID, true).Model[FieldName];
    }

    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

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

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product, true).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    protected int GetSubmitQuantity(int product)
    {
        return ORD_OrderApplyBLL.GetSubmitQuantity((int)ViewState["Publish"], product);
    }
    protected string GetPublishDetailGiveLevel(int product)
    {
        ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL((int)ViewState["Publish"]);

        ORD_ApplyPublishDetail detail = publish.Items.FirstOrDefault(p => p.Product == product);
        if (detail != null)
            return detail["GiveLevel"];
        else
            return "";
    }
    protected string GetPublishDetailRemark(int product)
    {
        ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL((int)ViewState["Publish"]);

        ORD_ApplyPublishDetail detail = publish.Items.FirstOrDefault(p => p.Product == product);
        if (detail != null)
            return detail["Remark"];
        else
            return "";
    }
    #endregion

    #region 全选与购买事件
    protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            cb_check.Checked = cb_SelectAll.Checked;
        }
    }
    protected void bt_BuyIn_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["LogisticsOrderApplyDetail"] != null)
        {
            int count = 0;
            ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
                if (cb_check.Visible && cb_check.Checked)
                {
                    if (cart.AddProduct((int)gv_List.DataKeys[row.RowIndex]["Product"], (decimal)gv_List.DataKeys[row.RowIndex]["Price"]) == 0)
                    {
                        row.FindControl("cb_Check").Visible = false;
                        row.FindControl("imbt_Select").Visible = false;
                        count++;
                    }
                }
            }
            lb_CartCount.Text = cart.Items.Count.ToString();
            MessageBox.Show(this, "成功将" + count.ToString() + "个品项加入购物车中!");
        }
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (Session["LogisticsOrderApplyDetail"] != null)
        {
            ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
            switch (cart.AddProduct((int)gv_List.DataKeys[e.NewSelectedIndex]["Product"], (decimal)gv_List.DataKeys[e.NewSelectedIndex]["Price"]))
            {
                case 0:
                    gv_List.Rows[e.NewSelectedIndex].FindControl("cb_Check").Visible = false;
                    gv_List.Rows[e.NewSelectedIndex].FindControl("imbt_Select").Visible = false;
                    lb_CartCount.Text = cart.Items.Count.ToString();
                    //MessageBox.Show(this, "成功将该品项加入购物车中!");
                    break;
                case -1:
                    MessageBox.Show(this, "发布目录中不包括此品项!");
                    break;
                case -2:
                    MessageBox.Show(this, "已超可申请数量!");
                    break;
                case -3:
                    MessageBox.Show(this, "该产品已在购物车中!");
                    break;
                default:
                    MessageBox.Show(this, "加入购物车失败!");
                    break;
            }
        }
        e.Cancel = true;
    }
    #endregion

    protected void bt_Search_Click(object sender, ImageClickEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();        
    }

    protected void bt_ViewCart_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderApplyDetail2.aspx");
    }




}
