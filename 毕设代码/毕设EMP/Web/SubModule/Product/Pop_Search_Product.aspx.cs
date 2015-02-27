using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_Product_Pop_Search_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"];

            BindTree();
            
        }
    }

    private void BindTree()
    {
        //获取品牌
        string condition = "";
        if (Request.QueryString["IsOpponent"] != null)
            condition = "IsOpponent=" + Request.QueryString["IsOpponent"];

        IList<PDT_Brand> _brands = PDT_BrandBLL.GetModelList(condition);
       
        dl_brand.DataSource = _brands;
        dl_brand.DataBind();
        dl_brand.Items.Insert(0, new ListItem("请选择", "0"));
        dl_brand_SelectedIndexChanged(null, null);

        foreach (PDT_Brand brand in _brands)
        {
            TreeNode tn_brand = new TreeNode(brand.Name, brand.ID.ToString());
            tr_Product.Nodes.Add(tn_brand);
            IList<PDT_Classify> _classifys = new PDT_ClassifyBLL()._GetModelList("PDT_Classify.Brand=" + brand.ID.ToString());
            foreach (PDT_Classify classify in _classifys)
            {
                TreeNode tn_classify = new TreeNode(classify.Name, classify.ID.ToString());
                tn_brand.ChildNodes.Add(tn_classify);

                string ConditionStr = "PDT_Product.State = 1 AND PDT_Product.Classify=" + classify.ID.ToString()+"and pdt_product.approveflag=1";
                if (Request.QueryString["ExtCondition"] != null)
                {
                    ConditionStr += " AND (" + Request.QueryString["ExtCondition"].Replace("\"", "").Replace('~', '\'') + ")";
                }

                IList<PDT_Product> _products = PDT_ProductBLL.GetModelList(ConditionStr);
                foreach (PDT_Product product in _products)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = product.Code + "  " + product.FullName;
                    tn.Value = product.ID.ToString();
                    tn_classify.ChildNodes.Add(tn);

                    if (tn.Value == ViewState["ID"].ToString())
                    {
                        tn_classify.Expand();
                        tn.Select();
                        tbx_SelectedProductID.Text = tn.Value;
                        tbx_SelectedProductName.Text = tn.Text;
                    }
                }

            }
        }
    }
    protected void tr_Product_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tr_Product.SelectedValue != "" && tr_Product.SelectedNode.ChildNodes.Count == 0)
        {
            if (tr_Product.SelectedNode.Depth == 2)
            {
                tbx_SelectedProductID.Text = tr_Product.SelectedValue;
                tbx_SelectedProductName.Text = tr_Product.SelectedNode.Text;
            }
        }
    }
    protected void dl_brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        IList<PDT_Classify> _classify = PDT_ClassifyBLL.GetModelList("Pdt_classify.brand="+dl_brand.SelectedValue);
        dl_classify.DataSource = _classify;
        dl_classify.DataBind();
        dl_classify_SelectedIndexChanged(null, null);
    }
    protected void dl_classify_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(dl_classify.SelectedValue.ToString()))
        {
           PDT_PackagingBLL _p = new PDT_PackagingBLL();
           IList<PDT_Packaging> _packaging= _p._GetModelList("1=1");
           dl_packaging.DataSource = _packaging;
           dl_packaging.DataBind();
           dl_packaging_SelectedIndexChanged(null, null);

        }
        
    }
    protected void dl_packaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        IList<PDT_Product> _product = PDT_ProductBLL.GetModelList("classify=" + dl_classify.SelectedValue + " and packaging=" + dl_packaging.SelectedValue+"and pdt_product.approveflag=1");
        dl_product.DataSource = _product;
        dl_product.DataBind();
        dl_product_SelectedIndexChanged(null, null);
       
    }
    protected void dl_product_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(dl_product.SelectedValue))
        {
            PDT_ProductBLL _p = new PDT_ProductBLL(Int32.Parse(dl_product.SelectedValue));
            tbx_SelectedProductName.Text = _p.Model.FullName;
            tbx_SelectedProductID.Text = dl_product.SelectedValue;
        }
    }
}
