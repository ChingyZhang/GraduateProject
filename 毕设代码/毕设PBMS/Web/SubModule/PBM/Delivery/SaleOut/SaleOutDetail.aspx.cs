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

public partial class SubModule_PBM_Delivery_SaleOut_SaleOutDetail : System.Web.UI.Page
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
                ViewState["WareHouse"] = Request.QueryString["WareHouse"] != null ? int.Parse(Request.QueryString["WareHouse"]) : 0;
                ViewState["Salesman"] = Request.QueryString["Salesman"] != null ? int.Parse(Request.QueryString["Salesman"]) : 0;

                PBM_Delivery d = new PBM_Delivery();
                d.Supplier = (int)Session["OwnerClient"];
                d.SupplierWareHouse = (int)ViewState["WareHouse"];
                d.Client = (int)ViewState["Client"];
                d.SalesMan = (int)ViewState["Salesman"];
                d.Classify = 1;
                d.PrepareMode = 1;

                pl_detail.BindData(d);

                ViewState["Details"] = new ListTable<PBM_DeliveryDetail>(new List<PBM_DeliveryDetail>(), "ID");
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
        PBM_DeliveryBLL bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        if (bll.Model != null)
        {
            pl_detail.BindData(bll.Model);

            ViewState["Client"] = bll.Model.Client;
            ViewState["Supplier"] = bll.Model.Supplier;
            ViewState["Details"] = new ListTable<PBM_DeliveryDetail>(bll.Items, "ID");
            ViewState["WareHouse"] = bll.Model.SupplierWareHouse;
            ViewState["State"] = bll.Model.State;
            ViewState["Classify"] = bll.Model.Classify;

            #region 绑定收款信息
            IList<PBM_DeliveryPayInfo> paylist = bll.GetPayInfoList();
            if (paylist.Count == 0)
            {
                if (bll.Model.State != 9) tbx_PayAmount1.Text = bll.Model.ActAmount.ToString("0.##");
            }
            else
            {
                if (paylist.Sum(p => p.Amount) != bll.Model.ActAmount)
                {
                    //收款金额与实际金额不符的，清除原收款信息
                    bll.ClearPayInfo();
                    if (bll.Model.State != 9) tbx_PayAmount1.Text = bll.Model.ActAmount.ToString("0.##");
                }
                else
                {
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
                }
            }
            #endregion

            BindGrid();

            #region 界面控件可视状态
            if (bll.Model.State >= 4 || bll.Model.ApproveFlag == 1)
            {
                bt_OK.Visible = false;
                tb_AddDetail.Visible = false;

                tr_AddDetail.Visible = false;

                gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;
            }

            if (bll.Model.State >= 4)
            {
                bt_Cancel.Visible = false;

            }
            if (bll.Model.State != 1) bt_Delete.Visible = false;
            if (bll.Model.State == 9) bt_Print.Visible = false;

            if (!((bll.Model.State == 1 && (bll.Model.PrepareMode == 1 || bll.Model.PrepareMode == 2))
                || ((bll.Model.State == 2 || bll.Model.State == 3) && bll.Model.PrepareMode == 3)))
            {
                pl_detail.SetControlsEnable(false);
                ddl_PayMode1.Enabled = false;
                ddl_PayMode2.Enabled = false;
                tbx_PayAmount1.Enabled = false;
                tbx_PayAmount2.Enabled = false;

                bt_Confirm.Visible = false;
            }

            if (bll.Model.PrepareMode != 1)
            {
                bt_Confirm.Text = "确认销售";
            }
            #endregion

            bt_Print.OnClientClick = "javascript:doprint(" + bll.Model.ID.ToString() + ");";
        }
    }

    private void BindGrid()
    {
        if (ViewState["Details"] != null)
        {
            ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];
            gv_List.BindGrid(Details.GetListItem());

            if (Details.GetListItem().Count == 0) bt_Confirm.Visible = false;
        }
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            PBM_DeliveryDetail d = Details[id.ToString()];

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
                if (d.SignInQuantity / product.ConvertFactor > 0)
                    lb_Quantity.Text = (d.SignInQuantity / product.ConvertFactor).ToString() + _T;

                if (d.SignInQuantity % product.ConvertFactor > 0)
                    lb_Quantity.Text += " " + (d.SignInQuantity % product.ConvertFactor).ToString() + _P;
            }
            #endregion

            #region 显示库存数量
            Label lb_InventoryQuantity = (Label)e.Row.FindControl("lb_InventoryQuantity");
            if (lb_InventoryQuantity != null && ViewState["State"] != null && (int)ViewState["State"] <= 4)
            {
                int inv_quantity = INV_InventoryBLL.GetProductQuantity((int)ViewState["WareHouse"], d.Product, d.LotNumber);
                lb_InventoryQuantity.Text = (inv_quantity / product.ConvertFactor).ToString() + _T;
                if (inv_quantity % product.ConvertFactor > 0)
                    lb_InventoryQuantity.Text += " " + (inv_quantity % product.ConvertFactor).ToString() + _P;

                if ((int)ViewState["Classify"] != 2)
                {
                    if (d.SignInQuantity > inv_quantity)
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
            e.Row.Cells[4].Text = Details.GetListItem().Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor).ToString("<font color=red size='larger'>0.##元</font>");
        }
    }

    protected void select_Product_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        int product = 0;
        int.TryParse(select_Product.SelectValue, out product);
        if (product != 0)
        {
            PDT_ProductBLL productbll = new PDT_ProductBLL(product);
            if (productbll.Model == null) return;
            PDT_Product p = productbll.Model;

            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[p.TrafficPackaging.ToString()].Name;
            string _P = dic[p.Packaging.ToString()].Name;

            #region 显示批号及库存信息
            ddl_LotNumber.Items.Clear();
            if ((int)ViewState["Classify"] == 2)
            {
                //退货，从供货商全部仓库中获取可退货的批号
                IList<INV_Inventory> list = INV_InventoryBLL.GetListByClientAndProduct((int)ViewState["Supplier"], product);
                foreach (INV_Inventory item in list)
                {
                    ddl_LotNumber.Items.Add(new ListItem((item.LotNumber == "" ? "无批号" : item.LotNumber), item.LotNumber));
                }
            }
            else
            {
                //销售，仅获取指定仓库可销售的批号
                foreach (INV_Inventory item in INV_InventoryBLL.GetInventoryListByProduct((int)ViewState["WareHouse"], product).OrderBy(t => t.LotNumber))
                {
                    string invstr = (item.Quantity / p.ConvertFactor).ToString() + _T;
                    if (item.Quantity % p.ConvertFactor > 0)
                        invstr += " " + (item.Quantity % p.ConvertFactor).ToString() + _P;

                    ddl_LotNumber.Items.Add(new ListItem((item.LotNumber == "" ? "无批号" : item.LotNumber) +
                        " -【库存量:" + invstr + "】", item.LotNumber));
                }

            }
            if (ddl_LotNumber.Items.Count == 0)
            {
                ddl_LotNumber.Items.Add(new ListItem("无批号", ""));
            }
            ddl_LotNumber_SelectedIndexChanged(null, null);
            #endregion

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

    protected void ddl_LotNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        int product = 0;
        int.TryParse(select_Product.SelectValue, out product);
        //int.TryParse(RadComboBox1.SelectedValue, out product);
        if (product != 0 && ddl_LotNumber.SelectedValue != "")
        {
            PDT_ProductBLL productbll = new PDT_ProductBLL(product, true);
            if (productbll.Model == null) return;
            PDT_Product p = productbll.Model;

            INV_Inventory inv = INV_InventoryBLL.GetInventoryByProductAndLotNumber((int)ViewState["WareHouse"], product, ddl_LotNumber.SelectedValue);
            if (inv == null) inv = INV_InventoryBLL.GetInventoryByClientAndProduct((int)ViewState["Supplier"], product, ddl_LotNumber.SelectedValue);
            if (inv != null)
            {
                //获取现有库存中的批号信息
                lb_ProductDate.Text = inv.ProductDate.ToString("yyyy-MM-dd");

                tbx_Quantity.Focus();
            }
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
        PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);

        if (ViewState["Details"] == null) return;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

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
            DateTime producedate = new DateTime(1900, 1, 1);

            int.TryParse(tbx_Quantity.Text, out quantity);
            decimal.TryParse(tbx_Price.Text, out price);

            if (ddl_Unit.SelectedValue == "T")
            {
                //整件单位
                quantity = quantity * productbll.Model.ConvertFactor;
                price = price / productbll.Model.ConvertFactor;
            }

            if (ViewState["SelectedDetail"] == null && quantity == 0)
            {
                MessageBox.Show(this, "请填写数量!");
                return;
            }

            INV_Inventory inv = null;
            if (quantity > 0)
            {
                if ((int)ViewState["Classify"] != 2)
                {
                    #region 销售时判断库存数量是否够销售
                    int inv_quantity = INV_InventoryBLL.GetProductQuantity((int)ViewState["WareHouse"], product, ddl_LotNumber.SelectedValue);
                    if (quantity > inv_quantity)
                    {
                        MessageBox.Show(this, "库存不足,不可销售出库!");
                        return;
                    }
                    #endregion
                }

                inv = INV_InventoryBLL.GetInventoryByProductAndLotNumber((int)ViewState["WareHouse"],
                    product, ddl_LotNumber.SelectedValue);
                if (inv == null)
                {
                    MessageBox.Show(this, "请正确选择销售批号!");
                    return;
                }
            }

            PBM_DeliveryDetail d = null;

            if (ViewState["SelectedDetail"] != null)
            {
                d = (PBM_DeliveryDetail)ViewState["SelectedDetail"];
            }
            else
            {
                d = new PBM_DeliveryDetail();
                d.DeliveryID = (int)ViewState["ID"];

                if (Details.GetListItem().Count == 0)
                    d.ID = 1;
                else
                    d.ID = Details.GetListItem().Max(p => p.ID) + 1;
            }

            d.Product = product;
            if (inv != null)
            {
                d.LotNumber = inv.LotNumber;
                d.CostPrice = inv.Price;            //成本价取当前库存表的价格
                d.ProductDate = inv.ProductDate;
            }
            d.Price = price;                    //实际销售价
            d.DiscountRate = 1;                 //默认全价
            d.ConvertFactor = productbll.Model.ConvertFactor;
            if (_bll.Model.State == 1) d.DeliveryQuantity = quantity;
            d.SignInQuantity = quantity;
            d.LostQuantity = 0;
            d.BadQuantity = 0;
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
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        int id = (int)gv_List.DataKeys[e.RowIndex]["ID"];
        Details.Remove(id.ToString());

        //PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        //_bll.DeleteDetail(id);
        BindGrid();

    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        int id = (int)gv_List.DataKeys[e.NewSelectedIndex]["ID"];

        PBM_DeliveryDetail d = Details[id.ToString()];

        PDT_Product product = new PDT_ProductBLL(d.Product, true).Model;
        if (product == null) return;

        //RadComboBox1.SelectedValue = d.Product.ToString();
        //RadComboBox1.Text = product.FullName;
        //RadComboBox1_SelectedIndexChanged(null, null);
        select_Product.SelectText = product.FullName;
        select_Product.SelectValue = product.ID.ToString();
        select_Product_SelectChange(null, null);

        if (ddl_LotNumber.Items.FindByValue(d.LotNumber) != null)
            ddl_LotNumber.SelectedValue = d.LotNumber;

        if (d.DeliveryQuantity % product.ConvertFactor == 0)
        {
            ddl_Unit.SelectedValue = "T";
            tbx_Price.Text = (d.Price * product.ConvertFactor).ToString("0.##");
            tbx_Quantity.Text = (d.DeliveryQuantity / product.ConvertFactor).ToString();
        }
        else
        {
            ddl_Unit.SelectedValue = "P";
            tbx_Price.Text = d.Price.ToString("0.##");
            tbx_Quantity.Text = d.DeliveryQuantity.ToString();
        }
        ddl_SalesMode.SelectedValue = d.SalesMode.ToString();
        ViewState["SelectedDetail"] = d;
        bt_AddDetail.Text = "修 改";

    }

    private bool Save()
    {
        if (ViewState["Details"] == null) return false;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        PBM_DeliveryBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new PBM_DeliveryBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.Supplier == 0)
        {
            MessageBox.Show(this, "请正确选择供货商!");
            return false;
        }

        if (_bll.Model.SupplierWareHouse == 0)
        {
            MessageBox.Show(this, "请正确选择出库仓库!");
            return false;
        }
        #endregion

        //折扣价
        _bll.Model.DiscountAmount = Details.GetListItem().Sum(p => (1 - p.DiscountRate) * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor);

        //实际成交价
        _bll.Model.ActAmount = Math.Round((_bll.Model.Classify == 2 ? -1 : 1) *
            Details.GetListItem().Sum(p => p.DiscountRate * Math.Round(p.Price * p.ConvertFactor, 2) * p.SignInQuantity / p.ConvertFactor)
             - _bll.Model.WipeAmount, 2);




        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];

            #region 保存明细
            if (ViewState["Details"] == null) return false;

            foreach (PBM_DeliveryDetail d in Details.GetListItem(ItemState.Added))
            {
                _bll.AddDetail(d);
            }
            foreach (PBM_DeliveryDetail d in Details.GetListItem(ItemState.Modified))
            {
                _bll.UpdateDetail(d);
            }
            foreach (PBM_DeliveryDetail d in Details.GetListItem(ItemState.Deleted))
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
            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
            _bll.ClearPayInfo();

            decimal amount = 0;
            if (decimal.TryParse(tbx_PayAmount1.Text, out amount) && amount != 0)
            {
                PBM_DeliveryPayInfoBLL paybll = new PBM_DeliveryPayInfoBLL();
                paybll.Model.DeliveryID = _bll.Model.ID;
                paybll.Model.PayMode = int.Parse(ddl_PayMode1.SelectedValue);
                paybll.Model.Amount = amount;
                paybll.Model.ApproveFlag = 2;
                paybll.Model.InsertStaff = (int)Session["UserID"];
                paybll.Add();
            }
            if (decimal.TryParse(tbx_PayAmount2.Text, out amount) && amount != 0)
            {
                PBM_DeliveryPayInfoBLL paybll = new PBM_DeliveryPayInfoBLL();
                paybll.Model.DeliveryID = _bll.Model.ID;
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
        if (Save()) Response.Redirect("SaleOutDetail.aspx?ID=" + ViewState["ID"].ToString());
    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
            _bll.Delete();
            Response.Redirect("SaleOutList.aspx?Classify=" + _bll.Model.Classify.ToString() + "&PrepareMode=" + _bll.Model.PrepareMode.ToString());
        }
    }
    protected void bt_Cancel_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
            _bll.Cancel((int)Session["UserID"], "");
            Response.Redirect("SaleOutList.aspx?Classify=" + _bll.Model.Classify.ToString() + "&PrepareMode=" + _bll.Model.PrepareMode.ToString());
        }
    }

    protected void bt_Confirm_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (!Save()) return;

            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);

            IList<PBM_DeliveryPayInfo> payinfos = _bll.GetPayInfoList();
            if (payinfos.Sum(p => p.Amount) != _bll.Model.ActAmount)
            {
                MessageBox.Show(this, "对不起，收款信息的总金额与实际销售单金额不符，不可确认!");
                BindData();
                return;
            }

            int confirmstaff = (int)Session["UserID"];
            if (_bll.Model.PrepareMode > 1 && _bll.Model.DeliveryMan > 0)
            {
                //车销与预售模式，在确认时，如果有送货人，则以送货人为确认人。以便将收款记录关联在该员工头上
                confirmstaff = _bll.Model.DeliveryMan;
            }

            int ret = _bll.Confirm(confirmstaff);
            switch (ret)
            {
                case 0:
                    Response.Redirect("SaleOutList.aspx?Classify=" + _bll.Model.Classify.ToString() + "&PrepareMode=" + _bll.Model.PrepareMode.ToString());
                    return;
                case -1:
                    MessageBox.Show(this, "对不起，单据确认失败! 单据状态不可操作");
                    return;
                case -2:
                case -3:
                    MessageBox.Show(this, "对不起，单据确认失败! 未指定正确的仓库");
                    return;
                case -10:
                    MessageBox.Show(this, "对不起，单据确认失败! 库存数量不够出库");
                    return;

                default:
                    MessageBox.Show(this, "对不起，单据确认失败! Ret=" + ret.ToString());
                    break;
            }
            Response.Redirect("SaleOutList.aspx?Classify=" + _bll.Model.Classify.ToString() + "&PrepareMode=" + _bll.Model.PrepareMode.ToString());
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

    //        #region 显示批号及库存信息
    //        ddl_LotNumber.Items.Clear();
    //        if ((int)ViewState["Classify"] == 2)
    //        {
    //            //退货，从供货商全部仓库中获取可退货的批号
    //            IList<INV_Inventory> list = INV_InventoryBLL.GetListByClientAndProduct((int)ViewState["Supplier"], product);
    //            foreach (INV_Inventory item in list)
    //            {
    //                ddl_LotNumber.Items.Add(new ListItem((item.LotNumber == "" ? "无批号" : item.LotNumber), item.LotNumber));
    //            }
    //        }
    //        else
    //        {
    //            //销售，仅获取指定仓库可销售的批号
    //            foreach (INV_Inventory item in INV_InventoryBLL.GetInventoryListByProduct((int)ViewState["WareHouse"], product))
    //            {
    //                string invstr = (item.Quantity / p.ConvertFactor).ToString() + _T;
    //                if (item.Quantity % p.ConvertFactor > 0)
    //                    invstr += " " + (item.Quantity % p.ConvertFactor).ToString() + _P;

    //                ddl_LotNumber.Items.Add(new ListItem((item.LotNumber == "" ? "无批号" : item.LotNumber) +
    //                    " -【库存量:" + invstr + "】", item.LotNumber));
    //            }

    //        }
    //        if (ddl_LotNumber.Items.Count == 0)
    //        {
    //            ddl_LotNumber.Items.Add(new ListItem("无批号", ""));
    //        }
    //        ddl_LotNumber_SelectedIndexChanged(null, null);
    //        #endregion

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

}