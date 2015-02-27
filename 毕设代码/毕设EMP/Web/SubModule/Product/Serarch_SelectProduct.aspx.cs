using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;


public partial class SubModule_Product_Serarch_SelectProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ERPCode"] = Request.QueryString["ERPCode"] == null ? "" : Request.QueryString["ERPCode"];

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

        foreach (PDT_Brand brand in _brands)
        {
            TreeNode tn_brand = new TreeNode(brand.Name, brand.ID.ToString());
            tr_Product.Nodes.Add(tn_brand);

            IList<PDT_Classify> _classifys = new PDT_ClassifyBLL()._GetModelList("PDT_Classify.Brand=" + brand.ID.ToString());
            foreach (PDT_Classify classify in _classifys)
            {
                TreeNode tn_classify = new TreeNode(classify.Name, classify.ID.ToString());
                tn_brand.ChildNodes.Add(tn_classify);

                IList<PDT_Product> _products = new PDT_ProductBLL()._GetModelList("PDT_Product.State = 1 AND PDT_Product.Classify=" + classify.ID.ToString());
                foreach (PDT_Product product in _products)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = product.Code + "  " + product.FullName;
                    tn.Value = product.Code;
                    tn_classify.ChildNodes.Add(tn);

                    if (tn.Value == ViewState["ERPCode"].ToString())
                    {
                        tn_classify.Expand();
                        tn.Select();
                        tbx_SelectedProductCode.Text = tn.Value;
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
            tbx_SelectedProductCode.Text = tr_Product.SelectedValue;
            tbx_SelectedProductName.Text = tr_Product.SelectedValue;
        }
    }
}
