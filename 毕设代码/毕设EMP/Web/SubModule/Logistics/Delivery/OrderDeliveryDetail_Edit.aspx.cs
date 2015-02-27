using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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


public partial class SubModule_Logistics_Delivery_OrderDeliveryDetail_Edit : System.Web.UI.Page
{
    protected bool bNoDelivery = false;      //未发货，界面发货数量字段可编辑

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["Client"] = Request.QueryString["Client"] == null ? 0 : int.Parse(Request.QueryString["Client"]);
            BindDropDown();

            pn_OrderDelivery.SetPanelVisible("Panel_LGS_OrderDeliveryDetail00_02", false);

            if ((int)ViewState["ID"] == 0)
            {
                if ((int)ViewState["Client"] == 0) return;

                #region 新增发货单时，初始化界面
                CM_Client client = new CM_ClientBLL((int)ViewState["Client"]).Model;
                if (client != null)
                {
                    ViewState["OrganizeCity"] = client.OrganizeCity;
                    Label lb_Client = (Label)pn_OrderDelivery.FindControl("ORD_OrderDelivery_Client");
                    if (lb_Client != null) lb_Client.Text = client.FullName;

                    MCSSelectControl select_Store = (MCSSelectControl)pn_OrderDelivery.FindControl("ORD_OrderDelivery_Store");
                    if (select_Store != null && client.Supplier != 0)
                    {
                        select_Store.SelectText = new CM_ClientBLL(client.Supplier).Model.FullName;
                        select_Store.SelectValue = client.Supplier.ToString();
                    }

                    TextBox tbx_DeliveryTime = (TextBox)pn_OrderDelivery.FindControl("ORD_OrderDelivery_DeliveryTime");
                    if (tbx_DeliveryTime != null) tbx_DeliveryTime.Text = DateTime.Today.ToString("yyyy-MM-dd");

                    TextBox tbx_PreArrivalDate = (TextBox)pn_OrderDelivery.FindControl("ORD_OrderDelivery_PreArrivalDate");
                    if (tbx_PreArrivalDate != null) tbx_PreArrivalDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

                    

                    bNoDelivery = true;
                    bt_Delete.Visible = false;
                    
                }
                #endregion

                #region 创建空的列表
                ListTable<ORD_OrderDeliveryDetail> _details = new ListTable<ORD_OrderDeliveryDetail>
                    (new List<ORD_OrderDeliveryDetail>(), "Product");
                DataTable dtProduct = ORD_OrderDeliveryBLL.InitProductList((int)ViewState["Client"], 0);
                foreach (DataRow row in dtProduct.Rows)
                {
                    ORD_OrderDeliveryDetail item = new ORD_OrderDeliveryDetail();
                    item.Product = (int)row["Product"];
                    item.FactoryPrice = (decimal)row["FactoryPrice"];
                    item.Price = (decimal)row["SalesPrice"];
                    item.Client = (int)ViewState["Client"];
                    item.DeliveryQuantity = 0;
                    item.SignInQuantity = 0;
                    item.BadQuantity = 0;
                    item.LostQuantity = 0;

                    _details.Add(item);
                }

                ViewState["Details"] = _details;
                #endregion

                BindGrid();
            }
            else
            {
                BindData();
            }

        }
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
        if (m.ApproveFlag == 2)
        {
            //未审核
            bNoDelivery = true;
        }
        else
        {
            bt_Save.Visible = false;
            bt_Delete.Visible = false;
            pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail_01", false);
        }
        #endregion

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

    protected void tbx_DeliveryQuantity_TextChanged(object sender, EventArgs e)
    {
        TextBox tbx_sender = (TextBox)sender;
        int Quantity = 0;
        if (int.TryParse(tbx_sender.Text, out Quantity))
        {
            int rowindex = ((GridViewRow)tbx_sender.Parent.Parent).RowIndex;
            int productid = (int)gv_OrderList.DataKeys[rowindex]["Product"];

            PDT_Product product = new PDT_ProductBLL(productid).Model;

            TextBox tbx_DeliveryQuantity_T = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_DeliveryQuantity_T");
            TextBox tbx_DeliveryQuantity = (TextBox)gv_OrderList.Rows[rowindex].FindControl("tbx_DeliveryQuantity");

            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            ORD_OrderDeliveryDetail detail = _details[productid.ToString()];
            detail.DeliveryQuantity = int.Parse(tbx_DeliveryQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_DeliveryQuantity.Text);

            _details.Update(detail);

            Label lb_FactoryValue = (Label)((GridViewRow)tbx_sender.Parent.Parent).FindControl("lb_FactoryValue");
            if (lb_FactoryValue != null) lb_FactoryValue.Text = (detail.FactoryPrice * detail.DeliveryQuantity).ToString("0.##");

            Label lb_SalesValue = (Label)((GridViewRow)tbx_sender.Parent.Parent).FindControl("lb_SalesValue");
            if (lb_SalesValue != null) lb_SalesValue.Text = (detail.Price * detail.DeliveryQuantity).ToString("0.##");

            lb_TotalFactoryValue.Text = _details.GetListItem().Sum(p => p.DeliveryQuantity * p.FactoryPrice).ToString("0.##");
            lb_TotalSalesValue.Text = _details.GetListItem().Sum(p => p.DeliveryQuantity * p.Price).ToString("0.##");
        }
        else
        {
            tbx_sender.Text = "0";
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;

        if (_details.GetListItem().Count == 0)
        {
            MessageBox.Show(this, "在保存之前，发货明细不能为空!");
            return;
        }
        ORD_OrderDeliveryBLL bll;

        if ((int)ViewState["ID"] == 0)
            bll = new ORD_OrderDeliveryBLL();
        else
            bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);

        pn_OrderDelivery.GetData(bll.Model);

        if ((int)ViewState["ID"] == 0)
        {
            bll.Model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model.Client = (int)ViewState["Client"];
            bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
            bll.Model.State = 1;
            bll.Model.ApproveFlag = 2;
            bll.Model.SheetCode = ORD_OrderDeliveryBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);
            bll.Items = _details.GetListItem();

            ViewState["ID"] = bll.Add();

        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            #region 修改明细
            foreach (ORD_OrderDeliveryDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                bll.DeleteDetail(_deleted.ID);
            }

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();
            #endregion

        }
        if (sender != null)
            MessageBox.ShowAndRedirect(this, "保存成功", "OrderDeliveryDetail_Edit.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_Save_Click(null, null);

            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            bll.Delete();

            MessageBox.ShowAndRedirect(this, "删除发货单成功", "OrderDeliveryList.aspx");
        }
    }


}
