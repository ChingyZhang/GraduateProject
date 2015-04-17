using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.IFStrategy;
using MCSFramework.BLL;
using MCSControls.MCSWebControls;

public partial class SubModule_Product_ProductDetail : System.Web.UI.Page
{
    protected DropDownList ddl_Brand;
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_Brand = (DropDownList)pl_detail.FindControl("PDT_Product_Brand");

        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)Session["OwnerType"] == 2)
            {
                ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=1");
                ddl_Brand.DataValueField = "ID";
                ddl_Brand.DataTextField = "Name";
                ddl_Brand.DataBind();
            }

            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            else
            {
                if (Request.QueryString["Brand"] != null && Request.QueryString["Brand"] != "0")
                {
                    ddl_Brand.SelectedValue = Request.QueryString["Brand"];
                    ddl_Brand.Enabled = false;
                }
                MCSTabControl1.Visible = false;
            }
        }

        MCSTreeControl tr_Category = (MCSTreeControl)pl_detail.FindControl("PDT_Product_Category");
        if (tr_Category != null)
        {
            tr_Category.DataSource = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"]);
            tr_Category.DataBind();
        }
    }

    private void BindData()
    {
        PDT_Product m = new PDT_ProductBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(m);

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PDT_ProductBLL _Product = null;
        if ((int)ViewState["ID"] == 0)
        {
            _Product = new PDT_ProductBLL();
        }
        else
        {
            _Product = new PDT_ProductBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_Product.Model);

        if ((int)ViewState["ID"] == 0)
        {
            if (_Product.Model.State == 0) _Product.Model.State = 1;
            if (_Product.Model.ApproveFlag == 0) _Product.Model.ApproveFlag = 1;
            _Product.Model.OwnerType = (int)Session["OwnerType"];
            _Product.Model.OwnerClient = (int)Session["OwnerClient"];
            _Product.Model.InsertStaff = (int)Session["UserID"];

            ViewState["ID"] = _Product.Add();
        }
        else
        {
            _Product.Model.UpdateStaff = (int)Session["UserID"];
            _Product.Update();
        }
        Response.Redirect("ProductList.aspx?Brand=" + _Product.Model.Brand.ToString());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("ProductPictureList.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
}
