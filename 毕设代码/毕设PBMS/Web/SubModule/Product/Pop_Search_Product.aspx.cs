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
        string brandcondition = "";
        if (Request.QueryString["IsOpponent"] != null)
        {
            if (Request.QueryString["IsOpponent"] == "10")
                brandcondition = "IsOpponent IN (1,9)";
            else
                brandcondition = "IsOpponent=" + Request.QueryString["IsOpponent"];
        }
        else
        {
            if ((int)Session["OwnerType"] == 2) brandcondition = "IsOpponent='1'";
        }
        IList<PDT_Brand> _brands = PDT_BrandBLL.GetModelList(brandcondition);

        foreach (PDT_Brand brand in _brands)
        {
            TreeNode tn_brand = new TreeNode(brand.Name, brand.ID.ToString());
            tr_Product.Nodes.Add(tn_brand);

            string ConditionStr = "PDT_Product.State = 1 AND PDT_Product.ApproveFlag = 1 AND PDT_Product.Brand = " + brand.ID.ToString();

            if ((int)Session["OwnerType"] == 2)
            {
                ConditionStr += "  AND PDT_Product.OwnerType = 2 AND PDT_Product.OwnerClient =" + Session["OwnerClient"].ToString();
            }
            else if ((int)Session["OwnerType"] == 1)
            {
                ConditionStr += "  AND PDT_Product.OwnerType IN (1, 2) ";
            }

            if (Request.QueryString["ExtCondition"] != null)
            {
                ConditionStr += " AND (" + Request.QueryString["ExtCondition"].Replace("\"", "").Replace('~', '\'') + ")";
            }

            IList<PDT_Product> _products = PDT_ProductBLL.GetModelList(ConditionStr);
            foreach (PDT_Product product in _products)
            {
                TreeNode tn = new TreeNode();
                tn.Text = product.FactoryCode + "  " + product.FullName;
                tn.Value = product.ID.ToString();
                tn_brand.ChildNodes.Add(tn);

                if (tn.Value == ViewState["ID"].ToString())
                {
                    tn_brand.Expand();
                    tn.Select();
                    tbx_SelectedProductID.Text = tn.Value;
                    tbx_SelectedProductName.Text = tn.Text;
                }
            }

        }
    }
    protected void tr_Product_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (tr_Product.SelectedValue != "" && tr_Product.SelectedNode.ChildNodes.Count == 0)
        {
            if (tr_Product.SelectedNode.Depth == 1)
            {
                tbx_SelectedProductID.Text = tr_Product.SelectedValue;
                tbx_SelectedProductName.Text = tr_Product.SelectedNode.Text;
            }
        }
    }
}
