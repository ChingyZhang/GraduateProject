// ===================================================================
// 文件路径:SubModule/PBM/Delivery/SaleOut/SaleOutDeail.aspx.cs 
// 生成日期:2015-03-05 13:39:36 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.PBM;
using MCSFramework.Model;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;

public partial class SubModule_PBM_Order_OrderDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                ViewState["Client"] = Request.QueryString["Client"] != null ? int.Parse(Request.QueryString["Client"]) : 0;
                ViewState["Salesman"] = Request.QueryString["Salesman"] != null ? int.Parse(Request.QueryString["Salesman"]) : 0;

                PBM_Order d = new PBM_Order();
                d.Supplier = (int)Session["OwnerClient"];
                d.Client = (int)ViewState["Client"];
                d.SalesMan = (int)ViewState["Salesman"];
                d.Classify = 1;

                pl_detail.BindData(d);

                ViewState["Details"] = new ListTable<PBM_OrderDetail>(new List<PBM_OrderDetail>(), "ID");
            }

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_SalesMode.DataSource = DictionaryBLL.GetDicCollections("PBM_SalseMode");
        ddl_SalesMode.DataBind();
        
        //DataTable dt_Product = PDT_ProductExtInfoBLL.GetByClient((int)Session["OwnerClient"], "");

        //RadComboBox1.DataSource = dt_Product;
        //RadComboBox1.DataBind();
    }
    #endregion

    private void BindData()
    {
        PBM_OrderBLL bll = new PBM_OrderBLL((int)ViewState["ID"]);
        if (bll.Model != null)
        {
            pl_detail.BindData(bll.Model);

            ViewState["Client"] = bll.Model.Client;
            ViewState["Supplier"] = bll.Model.Supplier;
            ViewState["Details"] = new ListTable<PBM_OrderDetail>(bll.Items, "ID");
            ViewState["State"] = bll.Model.State;
            ViewState["Classify"] = bll.Model.Classify;

            #region 绑定收款信息
            IList<PBM_OrderPayInfo> paylist = bll.GetPayInfoList();

            if (paylist.Count > 0)
            {
                ddl_PayMode1.SelectedValue = paylist[0].PayMode.ToString();
                tbx_PayAmount1.Text = paylist[0].Amount.ToString("0.##");
            }
            if (paylist.Count > 1)
            {
                ddl_PayMode2.SelectedValue = paylist[1].PayMode.ToString();
                tbx_PayAmount2.Text = paylist[1].Amount.ToString("0.##");
            }
            #endregion

            BindGrid();

            #region 界面控件可视状态
            if (bll.Model.State != 1 || bll.Model.ApproveFlag != 2)
            {
                bt_OK.Visible = false;
                tb_AddDetail.Visible = false;
                bt_Delete.Visible = false;
                tr_AddDetail.Visible = false;
                pl_detail.SetControlsEnable(false);

                gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;

                ddl_PayMode1.Enabled = false;
                ddl_PayMode2.Enabled = false;
                tbx_PayAmount1.Enabled = false;
                tbx_PayAmount2.Enabled = false;
            }

            if (bll.Model.State != 1)
            {
                bt_Submit.Visible = false;
            }

            if (bll.Model.State != 2)
            {
                bt_Cancel.Visible = false;
                bt_Assign.Visible = false;
            }
            #endregion
        }
    }

    private void BindGrid()
    {
        if (ViewState["Details"] != null)
        {
            ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];
            gv_List.BindGrid(Details.GetListItem());            
        }
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            PBM_OrderDetail d = Details[id.ToString()];

            PDT_Product product = new PDT_ProductBLL(d.Product, true).Model;
            if (product == null) return;
            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[product.TrafficPackaging.ToString()].Name;
            string _P = dic[product.Packaging.ToString()].Name;

            #region 显示产品价格包装信息
            Label lb_Price = (Label)e.Row.FindControl("lb_Price");
            if (lb_Price != null)
            {
                lb_Price.Text = (d.Price * product.ConvertFactor).ToString("0.##") + "元 / " + _T + "(" + product.ConvertFactor.ToString() + _P + ")";
            }
            #endregion

            #region 显示产品数量信息
            Label lb_Quantity = (Label)e.Row.FindControl("lb_Quantity");
            if (lb_Quantity != null)
            {
                if (d.BookQuantity / product.ConvertFactor > 0)
                    lb_Quantity.Text = (d.BookQuantity / product.ConvertFactor).ToString() + _T;

                if (d.BookQuantity % product.ConvertFactor > 0)
                    lb_Quantity.Text += " " + (d.BookQuantity % product.ConvertFactor).ToString() + _P;
            }
            #endregion

            #region 显示库存数量
            Label lb_InventoryQuantity = (Label)e.Row.FindControl("lb_InventoryQuantity");
            if (lb_InventoryQuantity != null && ViewState["State"] != null && (int)ViewState["State"] == 1)
            {
                int inv_quantity = INV_InventoryBLL.GetProductQuantityAllWareHouse((int)ViewState["Supplier"], d.Product);
                lb_InventoryQuantity.Text = (inv_quantity / product.ConvertFactor).ToString() + _T;
                if (inv_quantity % product.ConvertFactor > 0)
                    lb_InventoryQuantity.Text += " " + (inv_quantity % product.ConvertFactor).ToString() + _P;

                if ((int)ViewState["Classify"] != 2)
                {
                    if (d.BookQuantity > inv_quantity)
                    {
                        lb_InventoryQuantity.Text = "<font color=red>" + lb_InventoryQuantity.Text + "【库存不足】</font>";
                    }
                }
            }
            #endregion
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "小计";
            e.Row.Cells[4].Text = Details.GetListItem().Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.BookQuantity / p.ConvertFactor).ToString("<font color=red size='larger'>0.##元</red>");
        }
    }

    protected void select_Product_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        int product = 0;
        int.TryParse(select_Product.SelectValue, out product);
        //int.TryParse(RadComboBox1.SelectedValue, out product);
        if (product != 0)
        {
            PDT_ProductBLL productbll = new PDT_ProductBLL(product);
            if (productbll.Model == null) return;
            PDT_Product p = productbll.Model;

            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[p.TrafficPackaging.ToString()].Name;
            string _P = dic[p.Packaging.ToString()].Name;


            #region 显示产品包装信息
            lb_TrafficPackagingName.Text = "元/" + _T + "(" + p.ConvertFactor.ToString() + _P + ")";
            #endregion

            #region 获取销售该门店的价格
            decimal saleprice = PDT_StandardPriceBLL.GetSalePrice((int)ViewState["Client"], (int)Session["OwnerClient"], product);
            if (saleprice > 0)
            {
                if (ddl_Unit.SelectedValue == "T")
                    tbx_Price.Text = (saleprice * p.ConvertFactor).ToString("0.###");
                else
                    tbx_Price.Text = saleprice.ToString("0.###");
                //tbx_Price.Enabled = false;
            }
            else
            {
                tbx_Price.Text = "0";
                //tbx_Price.Enabled = true;
            }
            #endregion

        }
    }

    protected void select_Product_TextChange(object sender, MCSControls.MCSWebControls.TextChangeEventArgs e)
    {
        PDT_ProductExtInfo p = PDT_ProductExtInfoBLL.GetProductExtInfo_ByCode((int)Session["OwnerClient"], e.Code);
        if (p != null)
        {
            PDT_ProductBLL productbll = new PDT_ProductBLL(p.Product);
            if (productbll.Model == null) return;

            select_Product.SelectValue = p.Product.ToString();
            select_Product.SelectText = productbll.Model.FullName;

            select_Product_SelectChange(null, null);
            tbx_Quantity.Text = "";
            tbx_Quantity.Focus();
        }        
    }
   
    protected void ddl_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        int product = 0;
        int.TryParse(select_Product.SelectValue, out product);
        //int.TryParse(RadComboBox1.SelectedValue, out product);
        if (product != 0)
        {
            decimal price = 0;
            decimal.TryParse(tbx_Price.Text, out price);

            if (price > 0)
            {
                PDT_ProductBLL productbll = new PDT_ProductBLL(product);
                if (productbll.Model == null) return;
                PDT_Product p = productbll.Model;
                if (ddl_Unit.SelectedValue == "T")
                    tbx_Price.Text = (price * p.ConvertFactor).ToString("0.##");
                else
                    tbx_Price.Text = (price / p.ConvertFactor).ToString("0.##");

            }

        }
    }
    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] == 0) return;
        PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);

        if (ViewState["Details"] == null) return;
        ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];

        int product = 0;
        int.TryParse(select_Product.SelectValue, out product);
        //int.TryParse(RadComboBox1.SelectedValue, out product);
        if (product != 0)
        {
            PDT_ProductBLL productbll = new PDT_ProductBLL(product, true);
            if (productbll.Model == null) return;
            if (productbll.Model.ConvertFactor == 0)
            {
                productbll.Model.ConvertFactor = 1;
                productbll.Update();
            }

            int quantity = 0;
            decimal price = 0;

            int.TryParse(tbx_Quantity.Text, out quantity);
            decimal.TryParse(tbx_Price.Text, out price);

            if (ddl_Unit.SelectedValue == "T")
            {
                //整件单位
                quantity = quantity * productbll.Model.ConvertFactor;
                price = price / productbll.Model.ConvertFactor;
            }

            if (quantity == 0)
            {
                MessageBox.Show(this, "请填写数量!");
                return;
            }

            PBM_OrderDetail d = null;

            if (ViewState["SelectedDetail"] != null)
            {
                d = (PBM_OrderDetail)ViewState["SelectedDetail"];
            }
            else
            {
                d = new PBM_OrderDetail();
                d.OrderID = (int)ViewState["ID"];

                if (Details.GetListItem().Count == 0)
                    d.ID = 1;
                else
                    d.ID = Details.GetListItem().Max(p => p.ID) + 1;
            }

            d.Product = product;
            d.Price = price;                    //实际销售价
            d.DiscountRate = 1;                 //默认全价
            d.ConvertFactor = productbll.Model.ConvertFactor;
            d.BookQuantity = quantity;
            d.ConfirmQuantity = quantity;
            d.DeliveredQuantity = 0;
            d.SalesMode = int.Parse(ddl_SalesMode.SelectedValue);
            if (d.SalesMode == 2)
            { d.Price = 0; }

            if (ViewState["SelectedDetail"] != null)
            {
                Details.Update(d);
                ViewState["SelectedDetail"] = null;
                bt_AddDetail.Text = "新 增";
                gv_List.SelectedIndex = -1;
            }
            else
            {
                Details.Add(d);
            }

            tbx_Quantity.Text = "0";

            BindGrid();
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];

        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];
        Details.Remove(id.ToString());

        BindGrid();

    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];

        int id = (int)gv_List.DataKeys[e.NewSelectedIndex]["ID"];

        PBM_OrderDetail d = Details[id.ToString()];

        PDT_Product product = new PDT_ProductBLL(d.Product, true).Model;
        if (product == null) return;

        //RadComboBox1.SelectedValue = d.Product.ToString();
        //RadComboBox1.Text = product.FullName;
        //RadComboBox1_SelectedIndexChanged(null, null);
        select_Product.SelectText = product.FullName;
        select_Product.SelectValue = product.ID.ToString();
        select_Product_SelectChange(null, null);

        if (d.BookQuantity % product.ConvertFactor == 0)
        {
            ddl_Unit.SelectedValue = "T";
            tbx_Price.Text = (d.Price * product.ConvertFactor).ToString("0.##");
            tbx_Quantity.Text = (d.BookQuantity / product.ConvertFactor).ToString();
        }
        else
        {
            ddl_Unit.SelectedValue = "P";
            tbx_Price.Text = d.Price.ToString("0.##");
            tbx_Quantity.Text = d.BookQuantity.ToString();
        }
        ddl_SalesMode.SelectedValue = d.SalesMode.ToString();
        ViewState["SelectedDetail"] = d;
        bt_AddDetail.Text = "修 改";

    }

    private bool Save()
    {
        if (ViewState["Details"] == null) return false;
        ListTable<PBM_OrderDetail> Details = (ListTable<PBM_OrderDetail>)ViewState["Details"];

        PBM_OrderBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new PBM_OrderBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new PBM_OrderBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.Supplier == 0)
        {
            MessageBox.Show(this, "请正确选择供货商!");
            return false;
        }

        if (_bll.Model.Client == 0)
        {
            MessageBox.Show(this, "请正确选择订货客户!");
            return false;
        }
        #endregion

        //折扣价
        _bll.Model.DiscountAmount = Details.GetListItem().Sum(p => (1 - p.DiscountRate) * Math.Round(p.Price * p.ConvertFactor, 2) * p.BookQuantity / p.ConvertFactor);

        //实际成交价
        _bll.Model.ActAmount = Details.GetListItem().Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.BookQuantity / p.ConvertFactor)
            - _bll.Model.DiscountAmount - _bll.Model.WipeAmount;
        _bll.Model.ActAmount = Math.Round(_bll.Model.ActAmount, 2);

        if (_bll.Model.Classify == 2) _bll.Model.ActAmount = 0 - _bll.Model.ActAmount; //退库时，金额以负数计

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];

            #region 保存明细
            if (ViewState["Details"] == null) return false;

            foreach (PBM_OrderDetail d in Details.GetListItem(ItemState.Added))
            {
                _bll.AddDetail(d);
            }
            foreach (PBM_OrderDetail d in Details.GetListItem(ItemState.Modified))
            {
                _bll.UpdateDetail(d);
            }
            foreach (PBM_OrderDetail d in Details.GetListItem(ItemState.Deleted))
            {
                _bll.DeleteDetail(d.ID);
            }
            #endregion

            if (_bll.Update() == 0)
            {
                SavePayInfo();
                return true;
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Items = Details.GetListItem();
            ViewState["ID"] = _bll.Add();

            if ((int)ViewState["ID"] > 0)
            {
                SavePayInfo();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 保存收款信息
    /// </summary>
    private void SavePayInfo()
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);
            _bll.ClearPayInfo();

            decimal amount = 0;
            if (decimal.TryParse(tbx_PayAmount1.Text, out amount) && amount != 0)
            {
                PBM_OrderPayInfoBLL paybll = new PBM_OrderPayInfoBLL();
                paybll.Model.OrderID = _bll.Model.ID;
                paybll.Model.PayMode = int.Parse(ddl_PayMode1.SelectedValue);
                paybll.Model.Amount = amount;
                paybll.Model.ApproveFlag = 2;
                paybll.Model.InsertStaff = (int)Session["UserID"];
                paybll.Add();
            }
            if (decimal.TryParse(tbx_PayAmount2.Text, out amount) && amount != 0)
            {
                PBM_OrderPayInfoBLL paybll = new PBM_OrderPayInfoBLL();
                paybll.Model.OrderID = _bll.Model.ID;
                paybll.Model.PayMode = int.Parse(ddl_PayMode2.SelectedValue);
                paybll.Model.Amount = amount;
                paybll.Model.ApproveFlag = 2;
                paybll.Model.InsertStaff = (int)Session["UserID"];
                paybll.Add();
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if (Save()) Response.Redirect("OrderDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);
            int ret = _bll.Delete();

            if (ret == 0)
                Response.Redirect("OrderList.aspx?Classify=" + _bll.Model.Classify.ToString());
            else
                MessageBox.Show(this, "操作失败，返回值:" + ret.ToString());
        }
    }

    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);
            int ret = _bll.Cancel((int)Session["UserID"], "");

            if (ret == 0)
                Response.Redirect("OrderList.aspx?Classify=" + _bll.Model.Classify.ToString());
            else
                MessageBox.Show(this, "操作失败，返回值:" + ret.ToString());
        }
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (Save())
            {
                PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);
                int ret = _bll.Submit((int)Session["UserID"]);

                if (ret == 0)
                    Response.Redirect("OrderList.aspx?Classify=" + _bll.Model.Classify.ToString());
                else
                    MessageBox.Show(this, "操作失败，返回值:" + ret.ToString());
            }
        }
    }

    protected void bt_Confirm_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (!Save()) return;

            PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);

            int ret = _bll.Submit((int)Session["UserID"]);
            if (ret < 0)
            {
                MessageBox.Show(this, "对不起，订单提交失败!Ret=" + ret.ToString());
                return;
            }
            else
            {
                Response.Redirect("OrderList.aspx?Classify=" + _bll.Model.Classify.ToString());
            }
        }
    }
    //protected void RadComboBox1_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    //{
    //    int product = 0;
    //    int.TryParse(RadComboBox1.SelectedValue, out product);
    //    if (product != 0)
    //    {
    //        PDT_ProductBLL productbll = new PDT_ProductBLL(product);
    //        if (productbll.Model == null) return;
    //        PDT_Product p = productbll.Model;

    //        Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
    //        string _T = dic[p.TrafficPackaging.ToString()].Name;
    //        string _P = dic[p.Packaging.ToString()].Name;


    //        #region 显示产品包装信息
    //        lb_TrafficPackagingName.Text = "元/" + _T + "(" + p.ConvertFactor.ToString() + _P + ")";
    //        #endregion

    //        #region 获取销售该门店的价格
    //        decimal saleprice = PDT_StandardPriceBLL.GetSalePrice((int)ViewState["Client"], (int)Session["OwnerClient"], product);
    //        if (saleprice > 0)
    //        {
    //            if (ddl_Unit.SelectedValue == "T")
    //                tbx_Price.Text = (saleprice * p.ConvertFactor).ToString("0.###");
    //            else
    //                tbx_Price.Text = saleprice.ToString("0.###");
    //            //tbx_Price.Enabled = false;
    //        }
    //        else
    //        {
    //            tbx_Price.Text = "0";
    //            //tbx_Price.Enabled = true;
    //        }
    //        #endregion

    //    }
    //}

    protected void bt_Assign_Click(object sender, EventArgs e)
    {
        int deliveryman = 0, deliveryvehicle = 0, supplierwarehouse = 0;
        DateTime prearrivaldate = new DateTime(1900, 1, 1);

        if (Session["AssignFlag"] != null && (bool)Session["AssignFlag"])
        {
            deliveryman = (int)Session["DeliveryMan"];
            deliveryvehicle = (int)Session["DeliveryVehicle"];
            supplierwarehouse = (int)Session["SupplierWareHouse"];
            prearrivaldate = (DateTime)Session["PreArrivalDate"];

            if ((int)ViewState["ID"] != 0)
            {
                PBM_OrderBLL _bll = new PBM_OrderBLL((int)ViewState["ID"]);
                if (_bll.Model.State == 2)
                {
                    int ret = _bll.CreateDelivery(supplierwarehouse, deliveryman, deliveryvehicle, prearrivaldate, (int)Session["UserID"]);
                    if (ret > 0)
                        Response.Redirect("../Delivery/SaleOut/SaleOutDetail.aspx?ID=" + ret.ToString());
                    else
                        MessageBox.Show(this, "派单失败!ret=" + ret.ToString());
                }
            }
        }
    }
}