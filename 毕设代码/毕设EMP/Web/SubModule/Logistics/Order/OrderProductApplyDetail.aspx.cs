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

public partial class SubModule_Logistics_Order_OrderProductApplyDetail : System.Web.UI.Page
{
    /// <summary>
    /// 设置页面gv_ProductList控件中，调整金额是否只读
    /// </summary>
    protected bool priceEnable = false;
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

            BindDropDown();
            ViewState["Product"] = 0;

            #region 创建空的列表
            ListTable<ORD_OrderApplyDetail> _details = new ListTable<ORD_OrderApplyDetail>(new ORD_OrderApplyBLL((int)ViewState["ID"]).Items, "Product");
            ViewState["Details"] = _details;
            #endregion

            if (Session["LogisticsOrderApplyDetail"] != null && (int)ViewState["ID"] == 0)
            {
                #region 新费用申请时，初始化申请信
                ORD_OrderCartBLL cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];
                ORD_OrderApply model = new ORD_OrderApply();
                if (cart.Client != 0)
                {
                    model.OrganizeCity = new CM_ClientBLL(cart.Client).Model.OrganizeCity;
                    model.Client = cart.Client;
                }
                model.InsertStaff = (int)Session["UserID"];
                model.InsertTime = DateTime.Now;
                model["IsSpecial"] = cart.IsSpecial.ToString();
                model.Type = cart.Type;
                ViewState["Type"] = cart.Type;
                model["ProductBrand"] = cart.Brand.ToString();
                model["ProductType"] = cart.OrderType.ToString();
                if (cart.OrderType != 1) priceEnable = true;
                model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();

                ORD_OrderLimitFactorBLL limitbll = new ORD_OrderLimitFactorBLL();
                ViewState["Limit"] = limitbll.GetLimitInfo(Convert.ToInt32(model.AccountMonth), cart.Client);
                bt_Submit.Visible = false;


                foreach (ORD_OrderCart item in cart.Items)
                {
                    ORD_OrderApplyDetail _detailmodel = new ORD_OrderApplyDetail();
                    _detailmodel.Price = item.Price;
                    _detailmodel.Product = item.Product;
                    _detailmodel.BookQuantity = item.BookQuantity;
                    _details.Add(_detailmodel);
                }
                ViewState["Details"] = _details;
                pn_OrderApply.BindData(model);
                BindGrid();
                #endregion
            }
            else
            {
                BindData();
            }

            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    #region 获取当前管理片区的预算信息

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

        if (apply == null) Response.Redirect("OrderApplyList.aspx");

        if (apply["ProductType"] != "1") priceEnable = true;

        pn_OrderApply.BindData(apply);
        ViewState["Type"] = apply.Type;

        int month = new AC_AccountMonthBLL(apply.AccountMonth).Model.Month;

        ORD_OrderLimitFactorBLL limitbll = new ORD_OrderLimitFactorBLL();
        ViewState["Limit"] = limitbll.GetLimitInfo(apply.AccountMonth, apply.Client);

        #region 绑定当前申请单的管理片区
        Label lb_OrganizeCity = (Label)pn_OrderApply.FindControl("ORD_OrderApply_OrganizeCity");
        lb_OrganizeCity.Text = TreeTableBLL.GetFullPathName("MCS_Sys.dbo.Addr_OrganizeCity", apply.OrganizeCity);
        #endregion

        #region 根据审批状态控制页面

        if (apply.State != 1 && apply.State != 8)
        {
            //提交 状态

            pn_OrderApply.SetControlsEnable(false);
            gv_ProductList.Columns[0].Visible = false;                             //选择 列
            gv_ProductList.Columns[gv_ProductList.Columns.Count - 1].Visible = false;     //删除 列
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Delete.Visible = false;
            //可见调整数量及原因
            gv_ProductList.Columns[gv_ProductList.Columns.Count - 4].Visible = true;      //调整原因
            gv_ProductList.Columns[gv_ProductList.Columns.Count - 5].Visible = true;      //调整金额
            bt_SaveAdjust.Visible = false;
        }

        if (apply.State == 2)
        {

            bt_SaveAdjust.Visible = false;

            ///已提交状态，审批过程中，可以作申请数量调整
            if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            {
                bt_SaveAdjust.Visible = true;
            }

        }

        if (apply.State >= 3)
        {
            bt_SaveAdjust.Visible = false;

            //审批通过
            gv_ProductList.Columns[gv_ProductList.Columns.Count - 3].Visible = true;      //已发放数量
        }
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






    protected void bt_Save_Click(object sender, EventArgs e)
    {
        ORD_OrderCartBLL cart = null;
        if (Session["LogisticsOrderApplyDetail"] != null) cart = (ORD_OrderCartBLL)Session["LogisticsOrderApplyDetail"];

        ListTable<ORD_OrderApplyDetail> _details = ViewState["Details"] as ListTable<ORD_OrderApplyDetail>;
        ORD_OrderApplyBLL bll;
        if ((int)ViewState["ID"] == 0)
            bll = new ORD_OrderApplyBLL();
        else
            bll = new ORD_OrderApplyBLL((int)ViewState["ID"]);

        pn_OrderApply.GetData(bll.Model);

        #region 保存明细
        foreach (GridViewRow row in gv_ProductList.Rows)
        {
            int productid = (int)gv_ProductList.DataKeys[row.RowIndex]["Product"];

            ORD_OrderApplyDetail m = _details[productid.ToString()];
            PDT_Product product = new PDT_ProductBLL(m.Product).Model;

            TextBox tbx_price = (TextBox)row.FindControl("tbx_Price");
            TextBox tbx_BookQuantity_T = (TextBox)row.FindControl("tbx_BookQuantity_T");
            TextBox tbx_BookQuantity = (TextBox)row.FindControl("tbx_BookQuantity");

            int quantity = int.Parse(tbx_BookQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_BookQuantity.Text);

            m.BookQuantity = quantity;
            m.Price = decimal.Parse(tbx_price.Text);

            _details.Update(m);
        }
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            double DelayDays = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["OrderDelayDays"]);
            bll.Model.OrganizeCity = cart.OrganizeCity;
            bll.Model.Client = cart.Client;
            bll.Model.PublishID = cart.Publish;
            bll.Model.AccountMonth = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-DelayDays));
            bll.Model.SheetCode = ORD_OrderApplyBLL.GenerateSheetCode(bll.Model.OrganizeCity, bll.Model.AccountMonth);   //自动产生备案号
            bll.Model.ApproveFlag = 2;
            bll.Model.State = 1;
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["IsSpecial"] = cart.IsSpecial.ToString();
            bll.Model.Type = cart.Type;
            bll.Model["ProductBrand"] = cart.Brand.ToString();
            bll.Model["ProductType"] = cart.OrderType.ToString();
            bll.Items = _details.GetListItem();

            ViewState["ID"] = bll.Add();
        }
        else
        {
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            #region 修改明细

            bll.Items = _details.GetListItem(ItemState.Modified);
            bll.UpdateDetail();

            #endregion
        }

        if (sender != null)
            Response.Redirect("OrderProductApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_Save_Click(null, null);

            ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL((int)ViewState["ID"]);

            if (bll.Items.Count == 0)
            {
                MessageBox.Show(this, "对不起，定单请购明细不能为空!");
                return;
            }
            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("TotalFee", lb_TotalCost.Text);
            dataobjects.Add("IsSpecial", bll.Model["IsSpecial"]);
            dataobjects.Add("ProductType", bll.Model["ProductType"]);
            dataobjects.Add("ProductBrand", bll.Model["ProductBrand"]);
            int TaskID;
            TaskID = EWF_TaskBLL.NewTask("Product_Apply", (int)Session["UserID"], "订单产品申请流程,订单号:" + bll.Model.SheetCode, "~/SubModule/Logistics/Order/OrderProductApplyDetail.aspx?ID=" + ViewState["ID"].ToString() + "&Type=1", dataobjects);
            new EWF_TaskBLL(TaskID).Start();
            #endregion

            bll.Model["TaskID"] = TaskID.ToString();
            bll.Model.State = 2;
            bll.Update();
            Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }

    #region 保存调整后的数量
    protected void bt_SaveAdjust_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            ListTable<ORD_OrderApplyDetail> _details = ViewState["Details"] as ListTable<ORD_OrderApplyDetail>;

            foreach (GridViewRow row in gv_ProductList.Rows)
            {
                int productid = (int)gv_ProductList.DataKeys[row.RowIndex]["Product"];

                ORD_OrderApplyDetail m = _details[productid.ToString()];
                PDT_Product product = new PDT_ProductBLL(m.Product).Model;

                TextBox tbx_AdjustQuantity_T = (TextBox)row.FindControl("tbx_AdjustQuantity_T");
                TextBox tbx_AdjustQuantity = (TextBox)row.FindControl("tbx_AdjustQuantity");

                int quantity = int.Parse(tbx_AdjustQuantity_T.Text) * product.ConvertFactor + int.Parse(tbx_AdjustQuantity.Text);
                if (m.BookQuantity + quantity < 0)
                {
                    MessageBox.Show(this, "对不起，扣减的调整数量不能大于定货数量!");
                    return;
                }
                m.AdjustQuantity = quantity;
                m.AdjustReason = ((TextBox)row.FindControl("tbx_AdjustReason")).Text;

                _details.Update(m);
            }

            ORD_OrderApplyBLL bll = new ORD_OrderApplyBLL((int)ViewState["ID"]);
            bll.Items = _details.GetListItem(ItemState.Modified);

            bll.UpdateDetail();

            BindGrid();
        }
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

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new ORD_OrderApplyBLL((int)ViewState["ID"]).Delete();

            MessageBox.ShowAndRedirect(this, "订单删除成功!", "OrderApplyList.aspx?Type=1");
        }
    }
    protected void bt_print_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderProductApplyDetail_Print.aspx?ID=" + ViewState["ID"].ToString());
    }
}
