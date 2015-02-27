using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.EWF;
using System.Collections.Specialized;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL.CM;

public partial class SubModule_Logistics_Order_OrderProductApplyDetail_Print : System.Web.UI.Page
{
    /// <summary>
    /// 设置页面gv_ProductList控件中，调整金额是否只读
    /// </summary>
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 判断传入参数是否为SheetCode
            if (Request.QueryString["SheetCode"] != null)
            {
                string code = Request.QueryString["SheetCode"];

                IList<ORD_OrderApply> list = ORD_OrderApplyBLL.GetModelList("SheetCode='" + code + "'");
                if (list.Count > 0)
                {
                    Response.Redirect("OrderProductApplyDetail.aspx?ID=" + list[0].ID.ToString());
                }
                else
                    Response.Redirect("OrderApplyList.aspx");
            }
            #endregion
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);            
          
           
            ViewState["Product"] = 0;

            #region 创建空的列表
            ListTable<ORD_OrderApplyDetail> _details = new ListTable<ORD_OrderApplyDetail>(new ORD_OrderApplyBLL((int)ViewState["ID"]).Items, "Product");
            ViewState["Details"] = _details;
            #endregion

            if ( (int)ViewState["ID"]!=0)
            {  
                BindData();
            }
            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();

        }
    }

  

    #region 获取当前管理片区

    protected void tr_OrganizeCity_Selected(object sender, SelectedEventArgs e)
    {
        Label lb_OrganizeCity = (Label)pn_OrderApply.FindControl("ORD_OrderApply_OrganizeCity");
        lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_Sys.dbo.Addr_OrganizeCity", e.CurSelectIndex);

        Label lb_SheetCode = (Label)pn_OrderApply.FindControl("ORD_OrderApply_SheetCode");
        lb_SheetCode.Text = ORD_OrderApplyBLL.GenerateSheetCode(e.CurSelectIndex, AC_AccountMonthBLL.GetCurrentMonth());

        MCSSelectControl select_Client = (MCSSelectControl)pn_OrderApply.FindControl("ORD_OrderApply_Client");
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + e.CurSelectIndex;


    }
 
    #endregion

    private void BindData()
    {
        int id = (int)ViewState["ID"];

        ORD_OrderApply apply = new ORD_OrderApplyBLL(id).Model;

        if (apply == null) Response.Redirect("FeeApplyList.aspx");

       

        pn_OrderApply.BindData(apply);
        ViewState["Type"] = apply.Type;

        int month = new AC_AccountMonthBLL(apply.AccountMonth).Model.Month;

        ORD_OrderLimitFactorBLL limitbll = new ORD_OrderLimitFactorBLL();
        ViewState["Limit"] = limitbll.GetLimitInfo(apply.AccountMonth, apply.Client);

        #region 绑定当前申请单的管理片区
        Label lb_OrganizeCity = (Label)pn_OrderApply.FindControl("ORD_OrderApply_OrganizeCity");
        lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_Sys.dbo.Addr_OrganizeCity", apply.OrganizeCity);
        #endregion 

        BindGrid();
    }

    #region 绑定费用申请明细列表
    private void BindGrid()
    {

        ListTable<ORD_OrderApplyDetail> _details = ViewState["Details"] as ListTable<ORD_OrderApplyDetail>;
        gv_ProductList.BindGrid<ORD_OrderApplyDetail>(_details.GetListItem());

        foreach (GridViewRow row in gv_ProductList.Rows)
        {
            DataTable dt = ViewState["Limit"] as DataTable;
            int OrderVolume = 0;
            string Product = gv_ProductList.DataKeys[row.RowIndex]["Product"].ToString();
            DataRow[] productrow = dt.Select("Product=" + Product);
            if (productrow.Count() > 0)
            {
                OrderVolume = Convert.ToInt32(productrow[0]["OrderVolume"]);
                if ((int)ViewState["ID"] != 0 && new ORD_OrderApplyBLL((int)ViewState["ID"]).Model.State == 3)
                {
                    OrderVolume = 0;
                }
                ORD_OrderApplyDetail m = _details[Product];
                Label lb_state = (Label)row.FindControl("lb_State");
                lb_state.ForeColor = System.Drawing.Color.Red;
                lb_state.Font.Bold = true;
                lb_state.Font.Size = 10;
                if ((OrderVolume + m.BookQuantity) > Convert.ToInt32(productrow[0]["MaxPurchaseVolume"]))
                {
                    row.BackColor = System.Drawing.Color.FromName("#b7d8f9");
                    lb_state.Visible = true;
                    lb_state.Text = "↑";
                }
                if ((OrderVolume + m.BookQuantity) < Convert.ToInt32(productrow[0]["MinPurchaseVolume"]))
                {
                    row.BackColor = System.Drawing.Color.FromName("#7d9cbb");
                    lb_state.Visible = true;
                    lb_state.Text = "↓";
                }


            }
        }

        //求销售额合计
        decimal _totalcost = 0;
        foreach (ORD_OrderApplyDetail _detail in _details.GetListItem())
        {
            _totalcost += _detail.Price * (_detail.BookQuantity + _detail.AdjustQuantity);
        }
        lb_TotalCost.Text = _totalcost.ToString("0.###");


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

    protected int GetReplyQuantity(int appProduct, int appQuantity, int adjQuantity)
    {
        PDT_Product p = new PDT_ProductBLL(appProduct).Model;

        int quantity = appQuantity + adjQuantity;
        return quantity / p.ConvertFactor;


    }

    protected int GetReplyPackaging(int product, int appQuantity, int adjQuantity)
    {
        PDT_Product p = new PDT_ProductBLL(product).Model;
        return (appQuantity + adjQuantity) % p.ConvertFactor;
    }

    #endregion



    protected string GetJXCQuantityString(int product, string FieldName)
    {

        int quantity = 0;
        DataTable dt = ViewState["Limit"] as DataTable;
        if (dt.Select("Product=" + product.ToString()).Count() > 0)
        {
            quantity = Convert.ToInt32(dt.Select("Product=" + product.ToString())[0][FieldName]);
        }
        PDT_Product p = new PDT_ProductBLL(product).Model;
        string packing1 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
        string packing2 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

        string ret = "";
        if (quantity / p.ConvertFactor != 0) ret += (quantity / p.ConvertFactor).ToString() + packing1 + " ";
        if (quantity % p.ConvertFactor != 0) ret += (quantity % p.ConvertFactor).ToString() + packing2 + " ";
        return ret;

    }
 
}
