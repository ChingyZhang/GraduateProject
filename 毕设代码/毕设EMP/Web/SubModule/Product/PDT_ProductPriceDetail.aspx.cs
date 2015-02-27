using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;

public partial class SubModule_Product_PDT_ProductPriceDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PriceID"] = Request.QueryString["PriceID"] == null ? 0 : int.Parse(Request.QueryString["PriceID"]);
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，２：经销商，３：终端门店
            
            if ((int)ViewState["PriceID"] != 0)//修改客户销量
            {
                BindData();
            }
            else
            {
                #region 获取页面参数
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = int.Parse(Request.QueryString["ClientID"].ToString());
                }
                else if (Session["ClientID"] != null)
                {
                    ViewState["ClientID"] = (int)Session["ClientID"];
                }
                else
                {
                   Response.Redirect("PDT_ProductPrice.aspx");
                }
                #endregion
                tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                tbx_end.Text = DateTime.Now.AddMonths(36).ToString("yyyy-MM-dd");
                BindClient();
                btn_Delete.Visible = false;
            }

            Header.Attributes["WebPageSubCode"] = "ClientType=" + ViewState["ClientType"].ToString();
            #region 创建空列表
            ListTable<PDT_ProductPrice_Detail> _details = new ListTable<PDT_ProductPrice_Detail>(new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Items, "Product");
            ViewState["Details"] = _details;
            BindGrid();
            #endregion
        }
    }

    #region 绑定信息
    private void BindClient()
    {
        CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        lbl_Client.Text = _r.FullName;
        ViewState["ClientType"] = _r.ClientType;
    }
    #endregion

    private void BindGrid()
    {
        ListTable<PDT_ProductPrice_Detail> _details = ViewState["Details"] as ListTable<PDT_ProductPrice_Detail>;
        gv_List.BindGrid<PDT_ProductPrice_Detail>(_details.GetListItem());

    }

    private void BindData()
    {
        #region 绑定销量头
        PDT_ProductPrice sv = new PDT_ProductPriceBLL(int.Parse(ViewState["PriceID"].ToString())).Model;
        ViewState["ClientID"] = sv.Client;
        BindClient();
        tbx_begin.Text = sv.BeginDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : sv.BeginDate.ToString("yyyy-MM-dd");
        tbx_end.Text = sv.EndDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : sv.EndDate.ToString("yyyy-MM-dd");
        #endregion

        if (sv.ApproveFlag == 1)
        {
            //已审核
            tr_AddProduct.Visible = false;
            btn_Save.Visible = false;
            btn_Approve.Visible = false;
            btn_Delete.Visible = false;
            gv_List.Columns[0].Visible = false;
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        PDT_ProductPriceBLL _bll;
        if ((int)ViewState["PriceID"] != 0)
            _bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
        else
            _bll = new PDT_ProductPriceBLL();

        _bll.Model.ApproveFlag = 2;
        _bll.Model.BeginDate = DateTime.Parse(this.tbx_begin.Text.Trim());
        _bll.Model.EndDate = ((DateTime.Parse(this.tbx_end.Text.Trim())).AddDays(1)).AddSeconds(-1);
        _bll.Model.Client = int.Parse(ViewState["ClientID"].ToString());
        _bll.Model.InsertStaff = (int)Session["UserID"];
        ListTable<PDT_ProductPrice_Detail> _details = ViewState["Details"] as ListTable<PDT_ProductPrice_Detail>;
        if ((int)ViewState["PriceID"] != 0)
        {
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Update();

            #region 修改明细
            _bll.Items = _details.GetListItem(ItemState.Added);
            _bll.AddDetail();

            foreach (PDT_ProductPrice_Detail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                _bll.DeleteDetail(_deleted.ID);
            }

            _bll.Items = _details.GetListItem(ItemState.Modified);
            _bll.UpdateDetail();
            #endregion
        }
        else
        {
            _bll.Model.Client = (int)ViewState["ClientID"];
            _bll.Model.InsertStaff = int.Parse(Session["UserID"].ToString());

            _bll.Items = _details.GetListItem();
            ViewState["PriceID"] = _bll.Add();
        }
        if (sender != null)
            Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    #region 选择产品控件事件处理
    protected void select_ProductCode_TextChange(object sender, MCSControls.MCSWebControls.TextChangeEventArgs e)
    {
        PDT_Product p = new PDT_ProductBLL(e.Code).Model;
        BindProduct(p);
    }

    protected void select_ProductCode_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        PDT_Product p = new PDT_ProductBLL(e.SelectValue).Model;
        BindProduct(p);
    }

    private bool BindProduct(PDT_Product product)
    {
        if (product != null)
        {
            #region 判断是否是竞品
            //仅能录入本公司产品及促销品
            if (new PDT_BrandBLL(product.Brand).Model.IsOpponent == "2")
            {
                lb_ProductName.Text = "对不起，只能录入本公司产品及促销品！";
                ViewState["Product"] = 0;
                return false;
            }
            #endregion

            ViewState["Product"] = product.ID;

            select_ProductCode.SelectValue = product.Code;
            select_ProductCode.SelectText = product.Code;

            lb_ProductName.Text = product.FullName;
            lb_ProductName.ForeColor = System.Drawing.Color.Black;
            lb_FactoryPrice.Text = product.FactoryPrice.ToString();
            //获取该产品的最小计量单位
            //lbl_TrafficPackaging.Text = DictionaryBLL.GetDicCollections("PDT_Packaging")[product.TrafficPackaging.ToString()].Name;


            //tbx_StdPrice.Text = product.StdPrice.ToString();PDT_ProductPriceBLL.GetPriceByClientAndType((int)ViewState["ClientID"], product.ID, (int)ViewState["Type"]).ToString();

            tbx_BuyingPrice.Focus();
            return true;
        }
        else
        {
            lb_ProductName.Text = "该产品编码不存在！！";
            lb_ProductName.ForeColor = System.Drawing.Color.Red;
            //tbx_StdPrice.Text = "0";

            ViewState["Product"] = 0;
            return false;
        }
    }
    #endregion

    #region 添加一条产品明细
    protected void bt_AddProduct_Click(object sender, EventArgs e)
    {
        #region 验证必填项
        if (ViewState["Product"] == null || (int)ViewState["Product"] == 0)
        {
            lb_ErrInfo1.Text = "产品必填！";
            return;
        }

        #endregion

        ListTable<PDT_ProductPrice_Detail> _details = ViewState["Details"] as ListTable<PDT_ProductPrice_Detail>;

        PDT_ProductPrice_Detail item;

        #region 产品存在与否判断
        if (ViewState["Selected"] == null)
        {
            //新增产品
            if (_details.Contains(ViewState["Product"].ToString()))
            {
                lb_ErrInfo1.Text = "该产品已添加！";
                return;
            }

            item = new PDT_ProductPrice_Detail();
            item.Product = (int)ViewState["Product"];
        }
        else
        {
            //修改科目
            if (!_details.Contains(ViewState["Product"].ToString()))
            {
                lb_ErrInfo1.Text = "要修改的产品不存在！";
                return;
            }
            item = _details[ViewState["Selected"].ToString()];

            select_ProductCode.Enabled = true;
            gv_List.SelectedIndex = -1;
        }
        #endregion

        lb_ProductName.ForeColor = System.Drawing.Color.Black;

        item.BuyingPrice = tbx_BuyingPrice.Text == "" ? 0 : decimal.Parse(tbx_BuyingPrice.Text);
        item.SalesPrice = tbx_SalesPrice.Text == "" ? 0 : decimal.Parse(tbx_SalesPrice.Text);

        if (ViewState["Selected"] == null)
            _details.Add(item);             //新增产品
        else
            _details.Update(item);          //更新产品

        BindGrid();

        lb_ErrInfo1.Text = "";
        tbx_BuyingPrice.Text = "";
        tbx_SalesPrice.Text = "";

        ViewState["Selected"] = null;
    }
    #endregion

    #region 删除一条产品明细
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int _product = int.Parse(this.gv_List.DataKeys[e.RowIndex]["Product"].ToString());
        ListTable<PDT_ProductPrice_Detail> _details = ViewState["Details"] as ListTable<PDT_ProductPrice_Detail>;
        _details.Remove(_product.ToString());
        ViewState["Details"] = _details;
        BindGrid();
    }
    #endregion

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

    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Approve((int)Session["UserID"]);
        Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Delete();
            Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _product = int.Parse(this.gv_List.DataKeys[e.NewSelectedIndex]["Product"].ToString());

        BindProduct(new PDT_ProductBLL(_product).Model);

        ListTable<PDT_ProductPrice_Detail> _details = (ListTable<PDT_ProductPrice_Detail>)ViewState["Details"];

        tbx_BuyingPrice.Text = _details[_product.ToString()].BuyingPrice.ToString("0.###");
        tbx_SalesPrice.Text = _details[_product.ToString()].SalesPrice.ToString("0.###");
        ViewState["Selected"] = _product.ToString();
        bt_AddProduct.Text = "修 改";
    }
}
