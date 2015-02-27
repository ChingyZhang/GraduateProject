using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;

public partial class SubModule_Product_PDT_ProductCost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);

            BindDropDown();

            if ((int)ViewState["OrganizeCity"] != 0)
            {
                tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
            }
            else
                tr_AddProduct.Visible = false;
            ListTable<PDT_ProductCost> _details = new ListTable<PDT_ProductCost>
                   (PDT_ProductCostBLL.GetListByOrganizeCity(int.Parse(tr_OrganizeCity.SelectValue)), "Product");
            ViewState["Details"] = _details;
            tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();

            BindGrid();
        }
    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        Response.Redirect("PDT_ProductCost.aspx?OrganizeCity=" + e.CurSelectIndex.ToString());
    }

    private void BindGrid()
    {
        ListTable<PDT_ProductCost> _details = (ListTable<PDT_ProductCost>)ViewState["Details"];
        gv_List.BindGrid<PDT_ProductCost>(_details.GetListItem());
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        ListTable<PDT_ProductCost> _details = (ListTable<PDT_ProductCost>)ViewState["Details"];

        PDT_ProductCostBLL _bll = new PDT_ProductCostBLL();

        foreach (PDT_ProductCost m in _details.GetListItem(ItemState.Added))
        {
            _bll.Model = m;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 2;

            _bll.Add();
        }

        foreach (PDT_ProductCost m in _details.GetListItem(ItemState.Modified))
        {
            _bll.Model = m;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
        }

        foreach (PDT_ProductCost m in _details.GetListItem(ItemState.Deleted))
        {
            _bll.Model = m;
            _bll.Delete();
        }

        Response.Redirect("PDT_ProductCost.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue);
    }


    #region 添加一条产品明细
    protected void bt_AddProduct_Click(object sender, EventArgs e)
    {
        #region 验证必填项
        if (ViewState["Product"] == null || (int)ViewState["Product"] == 0)
        {
            lb_ProductName.Text = "产品必填！";
            lb_ProductName.ForeColor = System.Drawing.Color.Red;
            return;
        }
        #endregion

        ListTable<PDT_ProductCost> _details = (ListTable<PDT_ProductCost>)ViewState["Details"];

        PDT_ProductCost item;
        #region 产品存在与否判断
        if (ViewState["Selected"] == null)
        {
            //新增产品
            if (_details.Contains(ViewState["Product"].ToString()))
            {
                lb_ProductName.Text = "该产品已添加！";
                return;
            }

            item = new PDT_ProductCost();
            item.Product = (int)ViewState["Product"];
            item.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        }
        else
        {
            //修改科目
            if (!_details.Contains(ViewState["Product"].ToString()))
            {
                lb_ProductName.Text = "要修改的产品不存在！";
                return;
            }
            item = _details[ViewState["Selected"].ToString()];

            select_ProductCode.Enabled = true;
            gv_List.SelectedIndex = -1;
        }
        #endregion

        item["CostPrice"] = tbx_CostPrice.Text == "" ? "0" : tbx_CostPrice.Text;
        item["CostPrice2"] = tbx_CostPrice2.Text == "" ? "0" : tbx_CostPrice2.Text;
        item["CostPrice3"] = tbx_CostPrice3.Text == "" ? "0" : tbx_CostPrice3.Text;
        item["QuantityFactor"] = tbx_QuantityFactor.Text;


        if (ViewState["Selected"] == null)
            _details.Add(item);             //新增产品
        else
            _details.Update(item);          //更新产品

        BindGrid();

        lb_ProductName.ForeColor = System.Drawing.Color.Black;
        select_ProductCode.SelectText = "";
        tbx_CostPrice.Text = "0";
        tbx_CostPrice2.Text = "0";
        tbx_CostPrice3.Text = "0";
        tbx_QuantityFactor.Text = "1";
        ViewState["Selected"] = null;
        bt_AddProduct.Text = "增加";
    }
    #endregion

    protected void select_ProductCode_TextChange(object sender, MCSControls.MCSWebControls.TextChangeEventArgs e)
    {
        PDT_Product product = new PDT_ProductBLL(e.Code).Model;

        BindProduct(product);
    }

    protected void select_ProductCode_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        PDT_Product product = new PDT_ProductBLL(e.SelectValue).Model;
        BindProduct(product);
    }

    private bool BindProduct(PDT_Product product)
    {
        if (product != null)
        {
            ViewState["Product"] = product.ID;

            select_ProductCode.SelectValue = product.Code;
            select_ProductCode.SelectText = product.Code;

            lb_ProductName.Text = product.FullName;
            lb_ProductName.ForeColor = System.Drawing.Color.Black;
            lbl_FactoryPrice.Text = product.FactoryPrice.ToString();
            tbx_CostPrice.Focus();

            return true;
        }
        else
        {
            lb_ProductName.Text = " 该产品编码不存在！！";
            lb_ProductName.ForeColor = System.Drawing.Color.Red;
            ViewState["Product"] = null;
            lbl_FactoryPrice.Text = "";

            return false;
        }
    }

    public string GetERPCode(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.Code;
    }
    public string GetFactoryPrice(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.FactoryPrice.ToString("0.###"); ;
    }
    public string GetFullName(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.FullName;
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _product = int.Parse(this.gv_List.DataKeys[e.NewSelectedIndex]["Product"].ToString());

        BindProduct(new PDT_ProductBLL(_product).Model);

        ListTable<PDT_ProductCost> _details = (ListTable<PDT_ProductCost>)ViewState["Details"];

        tbx_CostPrice.Text = _details[_product.ToString()].CostPrice.ToString("0.###");
        tbx_CostPrice2.Text = _details[_product.ToString()]["CostPrice2"];
        tbx_CostPrice3.Text = _details[_product.ToString()]["CostPrice3"];
        tbx_QuantityFactor.Text = _details[_product.ToString()]["QuantityFactor"];
        ViewState["Selected"] = _product.ToString();
        bt_AddProduct.Text = "修 改";
    }

    #region 删除一条产品明细
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ListTable<PDT_ProductCost> _details = (ListTable<PDT_ProductCost>)ViewState["Details"];
        int _product = int.Parse(this.gv_List.DataKeys[e.RowIndex]["Product"].ToString());

        _details.Remove(_product.ToString());
        BindGrid();
    }
    #endregion

}
