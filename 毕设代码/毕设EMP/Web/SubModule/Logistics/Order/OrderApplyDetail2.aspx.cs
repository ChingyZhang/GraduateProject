using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.CM;

public partial class SubModule_Logistics_Order_OrderApplyDetail2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LogisticsOrderApplyDetail"] == null)
            {
                MessageBox.ShowAndRedirect(this, "加载购物车失败，请重新选购!", "OrderApplyDetail0.aspx");
                return;
            }
            else
            {
                ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
                ViewState["Publish"] = cart.Publish;
                if (cart.Type == 2)
                {
                    //促销品申请
                    #region 绑定赠品申请预算信息
                    BindBudgetInfo(cart.OrganizeCity, cart.AccountMonth, cart.Client, cart.GiftFeeType, cart.Brand, cart.GiftClassify,cart.Receiver);
                    tb_GiftBudgetInfo.Visible = true;
                    #endregion
                }

                BindGrid();
            }
        }
    }

    #region 获取当前管理片区的预算信息
    private void BindBudgetInfo(int city, int month, int client, int feetype, int productbrand, int giftclassify,int receiver)
    {
        lb_OrganizeCityName.Text = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", city);
        lb_ClientName.Text = client == 0 ? "" : new CM_ClientBLL(client).Model.FullName;
        lb_Receiver.Text = (receiver == 0 || receiver == null) ? "" : new CM_ClientBLL(receiver).Model.FullName;

        decimal totalbudget = FNA_BudgetBLL.GetUsableAmount(month, city, feetype, false);
        lb_TotalBudget.Text = totalbudget.ToString("0.##");

        IList<ORD_GiftApplyAmount> giftamounts = ORD_GiftApplyAmountBLL.GetModelList(
            string.Format("AccountMonth={0} AND Client={1} AND Brand={2} AND Classify={3}",
            month, client, productbrand, giftclassify));
        if (giftamounts.Count > 0)
        {
            decimal available = giftamounts[0].AvailableAmount + giftamounts[0].PreBalance - giftamounts[0].DeductibleAmount;
            decimal balance = giftamounts[0].BalanceAmount;
            ViewState["SalesVolume"] = giftamounts[0].SalesVolume;
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
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];

        gv_List.DataSource = cart.Items.OrderByDescending(p=>p.Price).ToList();
        gv_List.DataBind();

        #region 求合计
        lb_TotalCost.Text = cart.Items.Sum(m => m.BookQuantity * m.Price).ToString("0.00");
        lb_AfterSubmitBalance.Text = (decimal.Parse(lb_BalanceAmount.Text) - decimal.Parse(lb_TotalCost.Text)).ToString("0.00");
        if (decimal.Parse(lb_BalanceAmount.Text) != 0)
            lb_Percent.Text = (decimal.Parse(lb_TotalCost.Text) / decimal.Parse(lb_BalanceAmount.Text)).ToString("0.##%");
        if (lb_TotalCost.Text != "" && decimal.Parse(lb_TotalCost.Text) != 0 && ViewState["SalesVolume"] != null && (decimal)ViewState["SalesVolume"] > 0)
        {
            lb_ActFeeRate.Text = Math.Round((decimal.Parse(lb_TotalCost.Text) / (decimal)ViewState["SalesVolume"] * 100), 2).ToString();            
        }
        #endregion
    }

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

    protected void bt_Continue_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderApplyDetail1.aspx");
    }

    protected void bt_Confirm_Click(object sender, EventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        if (cart.Type == 2)
        {
            //促销品申请
            //预算不足允许保存，但不可提交申请
            //int month = AC_AccountMonthBLL.GetCurrentMonth();

            //ORD_ApplyPublish publish = new ORD_ApplyPublishBLL((int)ViewState["Publish"]).Model;
            //decimal budget = FNA_BudgetBLL.GetUsableAmount(month, cart.OrganizeCity, publish.FeeType);
            //decimal totalcost = cart.Items.Sum(m => m.BookQuantity * m.Price);

            //if (budget < totalcost)
            //{
            //    MessageBox.Show(this, "对不起，当前预算不够申请当前申请的品项，请申请预算或调整申请品项！");
            //    return;
            //}
            Response.Redirect("OrderApplyDetail3.aspx");
        }
        else
        {
            Response.Redirect("OrderProductApplyDetail.aspx");
        }
    }

    #region 全选与删除
    protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            cb_check.Checked = cb_SelectAll.Checked;
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_SelectAll.Checked)
            {
                int product = (int)gv_List.DataKeys[row.RowIndex]["Product"];

                cart.RemoveProduct(product);
            }
        }

        BindGrid();
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        int product = (int)gv_List.DataKeys[e.RowIndex]["Product"];

        cart.RemoveProduct(product);
        BindGrid();
    }
    #endregion

    #region gv_List控件内子控件事件触发
    protected void tbx_BookQuantity_T_TextChanged(object sender, EventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        TextBox tbx_BookQuantity_T = (TextBox)sender;
        GridViewRow row = (GridViewRow)tbx_BookQuantity_T.Parent.Parent;

        int product = (int)gv_List.DataKeys[row.RowIndex]["Product"];
        int quantity = 0;

        if (int.TryParse(((TextBox)sender).Text, out quantity))
        {

            int factor = new PDT_ProductBLL(product, true).Model.ConvertFactor;
            if (factor > 0) quantity = quantity * factor;
            switch (cart.ModifyQuantity(product, quantity))
            {
                case 0:
                    break;
                case -1:
                    MessageBox.Show(this, "购物车中不包括此产品！");
                    break;
                case -2:
                    MessageBox.Show(this, "申请数量已超可申请数量！");
                    break;
                case -3:
                    MessageBox.Show(this, "申请数量小于最小单次申请数量！");
                    break;
                case -4:
                    MessageBox.Show(this, "申请数量大于最大单次申请数量！");
                    break;
                default:
                    MessageBox.Show(this, "数量更新错误！");
                    break;
            }
        }
        else
        {
            MessageBox.Show(this, "数字格式不对，必须为整数类型！");
        }

        BindGrid();
    }
    protected void bt_Sub_Click(object sender, ImageClickEventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        ImageButton b = (ImageButton)sender;
        TextBox tbx_BookQuantity_T = (TextBox)b.Parent.FindControl("tbx_BookQuantity_T");
        GridViewRow row = (GridViewRow)tbx_BookQuantity_T.Parent.Parent;

        int quantity = int.Parse(tbx_BookQuantity_T.Text);
        int product = (int)gv_List.DataKeys[row.RowIndex]["Product"];
        int factor = new PDT_ProductBLL(product, true).Model.ConvertFactor;

        cart.ModifyQuantity(product, (quantity * factor) - factor);
        BindGrid();
    }
    protected void bt_Add_Click(object sender, ImageClickEventArgs e)
    {
        ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
        ImageButton b = (ImageButton)sender;
        TextBox tbx_BookQuantity_T = (TextBox)b.Parent.FindControl("tbx_BookQuantity_T");
        GridViewRow row = (GridViewRow)tbx_BookQuantity_T.Parent.Parent;

        int quantity = int.Parse(tbx_BookQuantity_T.Text);
        int product = (int)gv_List.DataKeys[row.RowIndex]["Product"];
        int factor = new PDT_ProductBLL(product, true).Model.ConvertFactor;

        cart.ModifyQuantity(product, quantity * factor + factor);
        BindGrid();
    }
    #endregion

}
