using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Delivery_Transfer_TransferDetail_Print : System.Web.UI.Page
{
    private int PRINTPAGESIZE = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            string[] strIDs = Request.QueryString["ID"] != null ? Request.QueryString["ID"].ToString().Split(new char[] { ',' }, System.StringSplitOptions.None) : new string[] { "" };
            int[] ids = new int[strIDs.Length];
            bool flag = false;//为true标识转换出错退回列表界面
            int classify = 0;
            if (strIDs == null || strIDs.Length == 0 || (strIDs.Length == 1 && string.IsNullOrEmpty(strIDs[0]))) { flag = true; }
            else
            {
                for (int i = 0; i < strIDs.Length; i++)
                {
                    try
                    {
                        int _id = int.Parse(strIDs[i]);
                        PBM_DeliveryBLL _bllDelivery = new PBM_DeliveryBLL(_id);
                        if (_bllDelivery == null || _bllDelivery.Model.ID == 0) { flag = true; break; }
                        if (classify == 0) classify = _bllDelivery.Model.Classify;//初始化分类
                        else if (classify != _bllDelivery.Model.Classify) { flag = true; break; }//分类不一致也退回列表界面
                        ids[i] = _id;
                    }
                    catch { flag = true; break; }
                }
            }
            if (flag)
            {
                if (classify > 0) Response.Redirect("PurchaseList.aspx?Classify=" + classify.ToString());
                else Response.Redirect("PurchaseList.aspx");
            }
            ViewState["ids"] = ids;
            #endregion
            Repeater2.DataSource = ids;
            Repeater2.DataBind();
            //BindData();
        }
    }
    private void BindData()
    {
        PBM_DeliveryBLL bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        if ((int)ViewState["ID"] == 0 || bll.Model == null) Response.Redirect("TransferList.aspx");
        if ((int)Session["OwnerType"] == 3 && bll.Model.Supplier != (int)Session["OwnerClient"]) Response.Redirect("TransferList.aspx");

        ListTable<PBM_DeliveryDetail> listDelivery = new ListTable<PBM_DeliveryDetail>(bll.Items, "ID");
        ViewState["Details"] = listDelivery;

        //获取分页数量
        int totlaItem = ((ListTable<PBM_DeliveryDetail>)ViewState["Details"]).GetListItem().Count;
        int pageNum = (totlaItem / PRINTPAGESIZE) + (totlaItem % PRINTPAGESIZE == 0 ? 0 : 1);

        ViewState["TotalPageCount"] = pageNum;

        //int[] intTemp = new int[pageNum];
        //for (int i = 0; i < pageNum; i++) intTemp[i] = i;

        //Repeater1.DataSource = intTemp;
        //Repeater1.DataBind();
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)((GridView)sender).DataKeys[e.Row.RowIndex]["ID"];
            PBM_DeliveryDetail d = Details[id.ToString()];
            if (d == null) return;

            PDT_Product product = new PDT_ProductBLL(d.Product, true).Model;
            if (product == null) return;
            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[product.TrafficPackaging.ToString()].Name;
            string _P = dic[product.Packaging.ToString()].Name;

            Label lbProduct = (Label)e.Row.FindControl("lbProduct");
            if (lbProduct != null) lbProduct.Text = product.FullName;

            #region 显示产品价格包装信息
            Label lbPrice = (Label)e.Row.FindControl("lbPrice");
            if (lbPrice != null)
            {
                lbPrice.Text = (d.Price * product.ConvertFactor).ToString("0.##") + "元 / " + _T;// +"(" + product.ConvertFactor.ToString() + _P + ")";
            }
            #endregion

            #region 显示产品数量信息
            Label lb_Quantity = (Label)e.Row.FindControl("lb_Quantity");
            if (lb_Quantity != null)
            {
                if (d.DeliveryQuantity / product.ConvertFactor > 0)
                    lb_Quantity.Text = (d.DeliveryQuantity / product.ConvertFactor).ToString() + _T;

                if (d.DeliveryQuantity % product.ConvertFactor > 0)
                    lb_Quantity.Text += "" + (d.DeliveryQuantity % product.ConvertFactor).ToString() + _P;
            }
            #endregion

            Label lb_Fee = (Label)e.Row.FindControl("lb_Fee");
            if (lb_Fee != null) lb_Fee.Text = (Math.Round(d.Price * d.ConvertFactor, 2) * d.SignInQuantity / d.ConvertFactor).ToString("0.##");

            Label lbDiscountRate = (Label)e.Row.FindControl("lbDiscountRate");
            if (lbDiscountRate != null) lbDiscountRate.Text = d.DiscountRate.ToString("0.##%");

        }
    }

    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int[] ids = ViewState["ids"] as int[];
            ViewState["ID"] = ids[e.Item.ItemIndex];

            PBM_DeliveryBLL bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
            if ((int)ViewState["ID"] == 0 || bll.Model == null) Response.Redirect("SaleOutList.aspx");
            if (Session["OwnerType"] != null && (int)Session["OwnerType"] == 3 && bll.Model.Supplier != (int)Session["OwnerClient"]) Response.Redirect("SaleOutList.aspx");

            ListTable<PBM_DeliveryDetail> listDelivery = new ListTable<PBM_DeliveryDetail>(bll.Items, "ID");
            ViewState["Details"] = listDelivery;

            //获取分页数量
            int totlaItem = ((ListTable<PBM_DeliveryDetail>)ViewState["Details"]).GetListItem().Count;
            int pageNum = (totlaItem / PRINTPAGESIZE) + (totlaItem % PRINTPAGESIZE == 0 ? 0 : 1);
            ViewState["TotalPageCount"] = pageNum;

            int[] intTemp = new int[pageNum];
            for (int i = 0; i < pageNum; i++) intTemp[i] = i;
            Repeater Repeater1 = (Repeater)e.Item.FindControl("Repeater1");
            if (intTemp.Count() > 0 && Repeater1 != null)
            {
                Repeater1.DataSource = intTemp;
                Repeater1.DataBind();
            }
            Literal _c = (Literal)e.Item.FindControl("lb_RepeaterNextPage2");
            if (e.Item.ItemIndex > 0 && _c != null) _c.Text = "<br/><div class='PageNext'></div><br/>";
        }
    }


    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        PBM_DeliveryBLL bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        if (bll.Model == null) return;

        //设置打印换页
        Literal _c = (Literal)e.Item.FindControl("lb_RepeaterNextPage");
        if (e.Item.ItemIndex > 0 && _c != null) _c.Text = "<br/><div class='PageNext'></div><br/>";

        Literal lbTitle = (Literal)e.Item.FindControl("lbTitle");//打印单标题
        if (lbTitle != null)
        {
            MCSFramework.Model.Dictionary_Data dicClassify = DictionaryBLL.GetDicCollections("PBM_DeliveryClassify", true).FirstOrDefault(m => m.Key == bll.Model.Classify.ToString()).Value;
            lbTitle.Text = string.Format("{0} - {1}", new CM_ClientBLL(bll.Model.Supplier).Model.FullName, dicClassify);
        }
        //单号
        Label lbSheetCode = (Label)e.Item.FindControl("lbSheetCode");
        if (lbSheetCode != null) lbSheetCode.Text = bll.Model.SheetCode;
        //日期
        Label lbDeliveryTime = (Label)e.Item.FindControl("lbDeliveryTime");
        if (lbDeliveryTime != null) lbDeliveryTime.Text = bll.Model.InsertTime.ToString("yyyy-MM-dd");
        //业务员
        Label lbSalesMan = (Label)e.Item.FindControl("lbSalesMan");
        if (lbSalesMan != null && bll.Model.SalesMan > 0) lbSalesMan.Text = new Org_StaffBLL(bll.Model.SalesMan).Model.RealName;
        //出库仓库
        Label lbSupplierWareHouse = (Label)e.Item.FindControl("lbSupplierWareHouse");
        if (lbSupplierWareHouse != null && bll.Model.SupplierWareHouse > 0) lbSupplierWareHouse.Text = new CM_WareHouseBLL(bll.Model.SupplierWareHouse).Model.Name;
        //入库仓库
        Label lbClientWareHouse = (Label)e.Item.FindControl("lbClientWareHouse");
        if (lbClientWareHouse != null && bll.Model.ClientWareHouse > 0) lbClientWareHouse.Text = new CM_WareHouseBLL(bll.Model.ClientWareHouse).Model.Name;

        if (e.Item.ItemIndex == (int)ViewState["TotalPageCount"] - 1)
        {
            //大写金额
            Label lbTotalCostCN = (Label)e.Item.FindControl("lbTotalCostCN");
            if (lbTotalCostCN != null) lbTotalCostCN.Text = MCSFramework.Common.Rmb.CmycurD(bll.Model.ActAmount);
            //小写金额
            Label lbTotalCost = (Label)e.Item.FindControl("lbTotalCost");
            if (lbTotalCost != null) lbTotalCost.Text = bll.Model.ActAmount.ToString("0.##元");
            //总数量
            Label lbTotalCount = (Label)e.Item.FindControl("lbTotalCount");
            if (lbTotalCount != null)
            {
                int t, p;
                GetTotoalCount(out t, out p);
                lbTotalCount.Text = t.ToString() + "件";
                if (p != 0) lbTotalCount.Text += p.ToString() + "散";
            }
        }
        else
        {
            e.Item.FindControl("tr_TotalInfo").Visible = false;
        }

        Label lb_PageInfo = (Label)e.Item.FindControl("lb_PageInfo");
        if (lb_PageInfo != null) lb_PageInfo.Text = string.Format("第{0}页,共{1}页", e.Item.ItemIndex + 1, (int)ViewState["TotalPageCount"]);

        IList<PBM_DeliveryDetail> listD = null;
        if (ViewState["Details"] != null)
        {
            IList<PBM_DeliveryDetail> listDelivery = ((ListTable<PBM_DeliveryDetail>)ViewState["Details"]).GetListItem();
            listD = GetListNow(e.Item.ItemIndex, listDelivery);
        }
        GridView gv_List = (GridView)e.Item.FindControl("gv_List");
        if (gv_List != null)
        {
            gv_List.DataSource = listD;
            gv_List.DataBind();
        }
    }

    /// <summary>
    /// 获取所有产品数量
    /// </summary>
    /// <returns></returns>
    void GetTotoalCount(out int t, out int p)
    {
        t = 0; p = 0;
        if (ViewState["Details"] != null)
        {
            IList<PBM_DeliveryDetail> tempList = ((ListTable<PBM_DeliveryDetail>)ViewState["Details"]).GetListItem();
            foreach (var item in tempList)
            {
                t += item.DeliveryQuantity / item.ConvertFactor;
                p += item.DeliveryQuantity % item.ConvertFactor;
            }
        }
    }

    /// <summary>
    /// 获取当前分页的数据源
    /// </summary>
    /// <param name="Index">分页索引</param>
    /// <param name="DataSource">源数据</param>
    /// <returns>每个分页要显示的数据</returns>
    IList<PBM_DeliveryDetail> GetListNow(int Index, IList<PBM_DeliveryDetail> DataSource)
    {
        int minIndex = Index * PRINTPAGESIZE;
        int maxIndex = minIndex + PRINTPAGESIZE - 1 < DataSource.Count - 1 ? minIndex + PRINTPAGESIZE - 1 : DataSource.Count - 1;
        IList<PBM_DeliveryDetail> _list = new List<PBM_DeliveryDetail>();
        for (int i = minIndex; i <= maxIndex; i++) _list.Add(DataSource[i]);
        int rowcount = _list.Count;
        for (int i = 0; i < PRINTPAGESIZE - rowcount; i++)
        {
            _list.Add(new PBM_DeliveryDetail());
        }
        return _list;
    }

}