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
    protected void Page_Load(object sender, EventArgs e)
    {
        DropDownList ddl_Brand = (DropDownList)pl_detail.FindControl("PDT_Product_Brand");
        if (ddl_Brand != null)
        {
            ddl_Brand.SelectedIndexChanged += new EventHandler(ddl_Brand_SelectedIndexChanged);
            ddl_Brand.AutoPostBack = true;
        }

        DropDownList ddl_Classify = (DropDownList)pl_detail.FindControl("PDT_Product_Classify");
        if (ddl_Classify != null)
        {
            ddl_Classify.SelectedIndexChanged += new EventHandler(ddl_Classify_SelectedIndexChanged);
            ddl_Classify.AutoPostBack = true;
        }

        DropDownList ddl_State = (DropDownList)pl_detail.FindControl("PDT_Product_State");
        if (ddl_State != null)
        {
            ddl_State.SelectedIndexChanged += new EventHandler(ddl_State_SelectedIndexChanged);
            ddl_State.AutoPostBack = true;
        }

        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["IsOpponent"] = Request.QueryString["IsOpponent"] == null ? 0 : int.Parse(Request.QueryString["IsOpponent"]);

            #region 如果没有IsOpponent参数传入，则自行获取
            if ((int)ViewState["IsOpponent"] == 0)
            {
                PDT_Product p = new PDT_ProductBLL((int)ViewState["ID"]).Model;
                PDT_Brand b = new PDT_BrandBLL(p.Brand).Model;
                ViewState["IsOpponent"] = int.Parse(b.IsOpponent);
            }
            #endregion
            
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent=" + ViewState["IsOpponent"].ToString());
            ddl_Brand.DataValueField = "ID";
            ddl_Brand.DataTextField = "Name";
            ddl_Brand.DataBind();

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
                    ddl_Brand_SelectedIndexChanged(ddl_Brand, null);
                }
                MCSTabControl1.Visible = false;
            }

            Header.Attributes["WebPageSubCode"] = "IsOpponent=" + ViewState["IsOpponent"].ToString();
            ddl_Brand_SelectedIndexChanged(null, null);
        }


    }

    void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_Brand = (DropDownList)pl_detail.FindControl("PDT_Product_Brand");
        DropDownList ddl_Classify = (DropDownList)pl_detail.FindControl("PDT_Product_Classify");
        if (ddl_Brand != null && ddl_Classify != null)
        {
            ddl_Classify.DataSource = PDT_ClassifyBLL.GetModelList("Brand=" + ddl_Brand.SelectedValue);
            ddl_Classify.DataTextField = "Name";
            ddl_Classify.DataValueField = "ID";
            ddl_Classify.DataBind();
            ddl_Classify_SelectedIndexChanged(null, null);
        }
    }

    void ddl_Classify_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_Classify = (DropDownList)pl_detail.FindControl("PDT_Product_Classify");
        DropDownList ddl_cxptype2 = (DropDownList)pl_detail.FindControl("PDT_Product_cxptype2");
        if (ddl_Classify != null && ddl_cxptype2 != null)
        {
            ddl_cxptype2.DataSource = DictionaryBLL.Dictionary_Data_GetAlllList(" type=1002 and Description='" + ddl_Classify.SelectedValue + "'");
            ddl_cxptype2.DataTextField = "Name";
            ddl_cxptype2.DataValueField = "Code";
            ddl_cxptype2.DataBind();
        }
    }
    void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        MCSSelectControl select_MasterProduct = (MCSSelectControl)pl_detail.FindControl("PDT_Product_MasterProduct");
        if (select_MasterProduct == null) return;

        PDT_Product m = new PDT_Product();
        pl_detail.GetData(m);

        if (m.State != 3)
        {
            select_MasterProduct.SelectText = "";
            select_MasterProduct.SelectValue = "";
            select_MasterProduct.Enabled = false;
        }
        else
        {
            select_MasterProduct.Enabled = true;
            select_MasterProduct.PageUrl = "~/SubModule/Product/Pop_Search_Product.aspx?ID=" + select_MasterProduct.SelectValue +
                "&IsOpponent=" + ViewState["IsOpponent"].ToString() +
                "&ExtCondition=\"Brand=" + m.Brand.ToString() + " AND Classify=" + m.Classify.ToString() + "\"";
        }
    }

    private void BindData()
    {
        PDT_Product m = new PDT_ProductBLL((int)ViewState["ID"]).Model;
        pl_detail.BindData(m);

        if (m.Brand > 0)
        {
            PDT_Brand b = new PDT_BrandBLL(m.Brand).Model;
            ViewState["IsOpponent"] = int.Parse(b.IsOpponent);
        }
        ddl_State_SelectedIndexChanged(null, null);

        bt_OK.Text = "修改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PDT_ProductBLL _Product = null;
        if (bt_OK.Text == "添加")
        {
            _Product = new PDT_ProductBLL();
        }
        else
        {
            _Product = new PDT_ProductBLL((int)ViewState["ID"]);
        }

        pl_detail.GetData(_Product.Model);
        if (bt_OK.Text == "添加")
        {
            ViewState["ID"] = _Product.Add();
        }
        else
        {
            _Product.Model.UpdateStaff = (int)Session["UserID"];
            _Product.Update();
            
        }
        Response.Redirect("ProductDetail.aspx?ID=" + ViewState["ID"].ToString() + "&IsOpponent=" + ViewState["IsOpponent"].ToString());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 1)
        {
            Response.Redirect("ProductPictureList.aspx?ID=" + ViewState["ID"].ToString());
        }
    }
}
