using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;


public partial class SubModule_Logistics_Delivery_OrderDeliveryDetail_SignIn : System.Web.UI.Page
{
    protected bool bNoSignIn = false;        //未签收，界面签收数量字段可编辑
    private DropDownList ddl_BalanceLostCost;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 获取界面控件
        ddl_BalanceLostCost = (DropDownList)pn_OrderDelivery.FindControl("ORD_OrderDelivery_BalanceLostCost");

        ddl_BalanceLostCost.AutoPostBack = true;
        ddl_BalanceLostCost.SelectedIndexChanged += new EventHandler(ddl_BalanceLostCost_SelectedIndexChanged);
        #endregion

        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            BindDropDown();

            pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail00_01", false);

            if ((int)ViewState["ID"] == 0)
            {
                return;
            }
            else
            {
                BindData();
            }

        }
    }
    void ddl_BalanceLostCost_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox tbx_PayCost = (TextBox)pn_OrderDelivery.FindControl("ORD_OrderDelivery_PayCost");
        tbx_PayCost.Text = "0";
        tbx_PayCost.Enabled = ddl_BalanceLostCost.SelectedValue == "1";

    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        int id = (int)ViewState["ID"];
        ORD_OrderDelivery m = new ORD_OrderDeliveryBLL(id).Model;

        if (m == null) Response.Redirect("OrderDeliveryList.aspx");

        pn_OrderDelivery.BindData(m);

        #region 根据状态控制页面显示
        if (m.State == 2 || m.State == 3)
        {
            //在途状态、部分签收
            bNoSignIn = true;
        }
        else
        {
            bt_Save.Visible = false;
        }
        #endregion
        ddl_BalanceLostCost_SelectedIndexChanged(null, null);
        #region 创建空的列表
        ListTable<ORD_OrderDeliveryDetail> _details = new ListTable<ORD_OrderDeliveryDetail>
            (new ORD_OrderDeliveryBLL((int)ViewState["ID"]).Items, "Product");
        ViewState["Details"] = _details;
        #endregion

        BindGrid();
    }

    #region 绑定定单发放明细列表
    private void BindGrid()
    {
        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
        gv_OrderList.BindGrid<ORD_OrderDeliveryDetail>(_details.GetListItem());

        //求合计
        lb_TotalFactoryValue.Text = _details.GetListItem().Sum(p => p.DeliveryQuantity * p.FactoryPrice).ToString("合计厂价金额：0.##元");
        lb_TotalSalesValue.Text = _details.GetListItem().Sum(p => p.DeliveryQuantity * p.Price).ToString("合计销售金额：0.##元");
    }
    #endregion

    #region 转换产品数量为界面需要的格式
    protected string GetQuantityString(int product, int quantity)
    {
        if (quantity == 0) return "0";

        PDT_Product p = new PDT_ProductBLL(product).Model;

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

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity / p.ConvertFactor;
    }

    protected int GetPackagingQuantity(int product, int quantity)
    {
        if (quantity == 0) return 0;

        PDT_Product p = new PDT_ProductBLL(product).Model;

        return quantity % p.ConvertFactor;
    }

    protected string GetTrafficeName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
    }

    protected string GetPackagingName(int product)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;

        return DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();
    }
    #endregion

    protected void tbx_SignInQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_sender = (TextBox)sender;
        int Quantity = 0;
        if (int.TryParse(tbx_sender.Text, out Quantity))
        {
            int rowindex = ((GridViewRow)tbx_sender.Parent.Parent).RowIndex;
            int productid = (int)gv_OrderList.DataKeys[rowindex]["Product"];

            PDT_Product product = new PDT_ProductBLL(productid).Model;

            TextBox tbx_SignInQuantity_T = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_SignInQuantity_T");
            TextBox tbx_SignInQuantity = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_SignInQuantity");

            Quantity = int.Parse(tbx_SignInQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_SignInQuantity.Text);
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            ORD_OrderDeliveryDetail detail = _details[productid.ToString()];

            if (detail.DeliveryQuantity < detail.BadQuantity + detail.LostQuantity + Quantity)
            {
                tbx_sender.Text = "0";
                MessageBox.Show(this, "签收总数不能大于发货数量!");
                return;
            }

            detail.SignInQuantity = Quantity;
            _details.Update(detail);

            bNoSignIn = true;
            BindGrid();
        }
        else
        {
            tbx_sender.Text = "0";
        }
    }
    protected void tbx_BadQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_sender = (TextBox)sender;
        int Quantity = 0;
        if (int.TryParse(tbx_sender.Text, out Quantity))
        {
            int rowindex = ((GridViewRow)tbx_sender.Parent.Parent).RowIndex;
            int productid = (int)gv_OrderList.DataKeys[rowindex]["Product"];

            PDT_Product product = new PDT_ProductBLL(productid).Model;

            TextBox tbx_BadQuantity_T = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_BadQuantity_T");
            TextBox tbx_BadQuantity = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_BadQuantity");

            Quantity = int.Parse(tbx_BadQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_BadQuantity.Text);
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            ORD_OrderDeliveryDetail detail = _details[productid.ToString()];

            if (detail.DeliveryQuantity < detail.SignInQuantity + detail.LostQuantity + Quantity)
            {
                if (detail.DeliveryQuantity < detail.LostQuantity + Quantity)
                {
                    tbx_sender.Text = "0";
                    MessageBox.Show(this, "签收总数不能大于发货数量!");
                    return;
                }
                else
                {
                    detail.SignInQuantity = detail.DeliveryQuantity - detail.LostQuantity - Quantity;
                    detail.BadQuantity = Quantity;
                }
            }
            else
                detail.BadQuantity = Quantity;

            _details.Update(detail);

            bNoSignIn = true;
            BindGrid();
        }
        else
        {
            tbx_sender.Text = "0";
        }
    }
    protected void tbx_LostQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_sender = (TextBox)sender;
        int Quantity = 0;
        if (int.TryParse(tbx_sender.Text, out Quantity))
        {
            int rowindex = ((GridViewRow)tbx_sender.Parent.Parent).RowIndex;
            int productid = (int)gv_OrderList.DataKeys[rowindex]["Product"];

            PDT_Product product = new PDT_ProductBLL(productid).Model;

            TextBox tbx_LostQuantity_T = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_LostQuantity_T");
            TextBox tbx_LostQuantity = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_LostQuantity");

            Quantity = int.Parse(tbx_LostQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_LostQuantity.Text);
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            ORD_OrderDeliveryDetail detail = _details[productid.ToString()];

            if (detail.DeliveryQuantity < detail.SignInQuantity + detail.BadQuantity + Quantity)
            {
                if (detail.DeliveryQuantity < detail.BadQuantity + Quantity)
                {
                    tbx_sender.Text = "0";
                    MessageBox.Show(this, "签收总数不能大于发货数量!");
                    return;
                }
                else
                {
                    detail.SignInQuantity = detail.DeliveryQuantity - detail.BadQuantity - Quantity;
                    detail.LostQuantity = Quantity;
                }
            }
            else
                detail.LostQuantity = Quantity;

            _details.Update(detail);

            bNoSignIn = true;
            BindGrid();
        }
        else
        {
            tbx_sender.Text = "0";
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;

            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            pn_OrderDelivery.GetData(bll.Model);
            bll.Model["SignInStaff"] = Session["UserID"].ToString();
            if (bll.Model["SignInTime"] == "" || bll.Model["SignInTime"] == "1900-01-01")
                bll.Model["SignInTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            bll.Model.State = 3;   //设为部分签收
            bll.Update();

            #region 修改明细
            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();
            #endregion

        }
        if (sender != null)
            MessageBox.ShowAndRedirect(this, "保存成功", "OrderDeliveryDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void bt_SignInAll_Click(object sender, EventArgs e)
    {
        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
        foreach (GridViewRow row in gv_OrderList.Rows)
        {
            int productid = (int)gv_OrderList.DataKeys[row.RowIndex]["Product"];
            ORD_OrderDeliveryDetail detail = _details[productid.ToString()];

            detail.SignInQuantity = detail.DeliveryQuantity;
            detail.BadQuantity = 0;
            detail.LostQuantity = 0;
            _details.Update(detail);
        }
        bNoSignIn = true;
        BindGrid();
    }
}
