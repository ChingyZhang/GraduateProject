using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;

public partial class SubModule_Logistics_Delivery_OrderDeliveryDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            BindDropDown();



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
            bt_ConfirmDelivery.Visible = false;
            gv_OrderList.Columns[gv_OrderList.Columns.Count - 1].Visible = false;
            gv_OrderList.Columns[gv_OrderList.Columns.Count - 2].Visible = false;
            gv_OrderList.Columns[gv_OrderList.Columns.Count - 3].Visible = false;
            gv_OrderList.Columns[gv_OrderList.Columns.Count - 4].Visible = false;
        }
        else
        {
            bt_Edit.Visible = false;
            bt_Approve.Visible = false;

        }
        if (m.State != 1) bt_ConfirmDelivery.Visible = false;
        if (m.State != 2 && m.State != 3) bt_SignIn.Visible = false;
        if (m.State != 3 && m.State != 4) pn_OrderDelivery.SetPanelVisible("Panel_LGS_OrderDeliveryDetail01_02", false);
        if (m.State != 3)
        {
            pn_OrderDelivery.SetPanelEnable("Panel_LGS_OrderDeliveryDetail01_02", false);
            bt_ConfirmSignIn.Visible = false;
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
        gv_OrderList.BindGrid<ORD_OrderDeliveryDetail>(_details.GetListItem().Where(p => p.DeliveryQuantity > 0).ToList());

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


    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            bll.Approve((int)Session["UserID"]);

            #region 审核同时默认为发放
            bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            if (bll.Model["DeliveryTime"] != "1900-01-01") bll.Model["DeliveryTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bll.Model["DeliveryStaff"] = Session["UserID"].ToString();
            bll.Update();

            bll.Delivery((int)Session["UserID"]);
            #endregion


            MessageBox.ShowAndRedirect(this, "发货单审核成功！", "OrderDeliveryList.aspx");
        }
    }
    protected void bt_Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderDeliveryDetail_Edit.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void bt_SignIn_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderDeliveryDetail_SignIn.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void bt_ConfirmDelivery_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);

            if (bll.Model["DeliveryTime"] != "1900-01-01") bll.Model["DeliveryTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            bll.Model["DeliveryStaff"] = Session["UserID"].ToString();
            bll.Update();

            bll.Delivery((int)Session["UserID"]);

            MessageBox.ShowAndRedirect(this, "发货单发放成功，已为在途状态，待签收！", "OrderDeliveryList.aspx");
        }
    }
    protected void bt_ConfirmSignIn_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ListTable<ORD_OrderDeliveryDetail> _details = ViewState["Details"] as ListTable<ORD_OrderDeliveryDetail>;
            if (_details.GetListItem().Where(p => p.DeliveryQuantity != p.SignInQuantity + p.BadQuantity + p.LostQuantity).Count() > 0)
            {
                MessageBox.Show(this, "对不起，该笔发货单仍有部分品项为在途状态，不能确认签收!");
                return;
            }

            ORD_OrderDeliveryBLL bll = new ORD_OrderDeliveryBLL((int)ViewState["ID"]);
            pn_OrderDelivery.GetData(bll.Model);

            if (bll.Model["BalanceLostCost"] == "0" && 
                _details.GetListItem().Where(p => p.LostQuantity + p.BadQuantity > 0).Count() > 0)
            {
                MessageBox.Show(this, "对不起，该笔发货单有部分货品丢失或破损，请确认是否物流司机已赔款!");
                return;
            }
            bll.Update();

            bll.SignIn((int)Session["UserID"]);
            
            MessageBox.ShowAndRedirect(this, "完成签收成功！", "OrderDeliveryList.aspx");
        }
    }
}
