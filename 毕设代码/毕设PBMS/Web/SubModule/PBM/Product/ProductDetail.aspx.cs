using MCSControls.MCSWebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Product_ProductDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MCSTreeControl tr_Category = (MCSTreeControl)pl_detail.FindControl("PDT_ProductExtInfo_Category");
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["Category"] = Request.QueryString["Category"] == null ? 0 : int.Parse(Request.QueryString["Category"]);

            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            else
            {
                if (tr_Category != null && (int)ViewState["Category"] > 0) tr_Category.SelectValue = ViewState["Category"].ToString();
            }

        }

        if (tr_Category != null)
        {
            tr_Category.DataSource = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"]);
            tr_Category.DataBind();
        }
    }

    private void BindData()
    {
        PDT_ProductBLL bll = new PDT_ProductBLL((int)ViewState["ID"]);
        if (bll.Model == null)
        {
            Response.Redirect("ProductList.aspx.aspx");
            return;
        }
        if (bll.Model.ConvertFactor == 0) bll.Model.ConvertFactor = 1;
        pl_detail.BindData(bll.Model);

        PDT_ProductExtInfo extinfo = bll.GetProductExtInfo((int)Session["OwnerClient"]);
        if (extinfo != null)
        {
            #region 将价格折算为整件单位
            extinfo.BuyPrice = extinfo.BuyPrice * bll.Model.ConvertFactor;
            extinfo.SalesPrice = extinfo.SalesPrice * bll.Model.ConvertFactor;
            extinfo.MaxSalesPrice = extinfo.MaxSalesPrice * bll.Model.ConvertFactor;
            extinfo.MinSalesPrice = extinfo.MinSalesPrice * bll.Model.ConvertFactor;
            #endregion
            pl_detail.BindData(extinfo);
        }

        if (bll.Model.OwnerClient != (int)Session["OwnerClient"])
        {
            pl_detail.SetPanelEnable("Panel_TDP_PDT_Product_Detail_01", false);
        }

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PDT_ProductBLL _Product = null;
        PDT_ProductExtInfo extinfo = null;

        if ((int)ViewState["ID"] == 0)
        {
            _Product = new PDT_ProductBLL();
        }
        else
        {
            _Product = new PDT_ProductBLL((int)ViewState["ID"]);
            extinfo = _Product.GetProductExtInfo((int)Session["OwnerClient"]);
        }

        if (extinfo == null)
        {
            extinfo = new PDT_ProductExtInfo();
            extinfo.Supplier = (int)Session["OwnerClient"];
        }

        pl_detail.GetData(_Product.Model);
        pl_detail.GetData(extinfo);

        if ((int)ViewState["ID"] == 0)
        {
            _Product.Model.Category = extinfo.Category;
            _Product.Model.State = 1;
            _Product.Model.ApproveFlag = 1;
            _Product.Model.InsertStaff = (int)Session["UserID"];
            _Product.Model.OwnerType = 3;
            _Product.Model.OwnerClient = (int)Session["OwnerClient"];
            extinfo.InsertStaff = (int)Session["UserID"];

            ViewState["ID"] = _Product.Add();
        }
        else
        {
            if ((int)Session["OwnerClient"] == _Product.Model.OwnerClient)
            {
                _Product.Model.UpdateStaff = (int)Session["UserID"];
                _Product.Update();
            }
        }
        #region 将价格折算为最小单位
        extinfo.BuyPrice = extinfo.BuyPrice / _Product.Model.ConvertFactor;
        extinfo.SalesPrice = extinfo.SalesPrice / _Product.Model.ConvertFactor;
        extinfo.MaxSalesPrice = extinfo.MaxSalesPrice / _Product.Model.ConvertFactor;
        extinfo.MinSalesPrice = extinfo.MinSalesPrice / _Product.Model.ConvertFactor;
        #endregion

        _Product.SetProductExtInfo(extinfo);

        Response.Redirect("ProductList.aspx?Category=" + extinfo.Category.ToString());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("ProductPictureList.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
}