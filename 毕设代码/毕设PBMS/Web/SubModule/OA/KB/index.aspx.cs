using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_OA_KB_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["RootCatalog"] == null)
            {
                BindTree(tr_Catalog.Nodes, 0);
                ViewState["Catalog"] = tr_Catalog.Nodes[0].Value != null ? int.Parse(tr_Catalog.Nodes[0].Value) : 0;
            }
            else
            {
                BindTree(tr_Catalog.Nodes, int.Parse(Request.QueryString["RootCatalog"]));
                ViewState["Catalog"] = int.Parse(Request.QueryString["RootCatalog"]);
            }

            BindGrid();
        }
    }

    protected void trPosition_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["Position"] = int.Parse(tr_Catalog.SelectedValue);
        BindGrid();
    }

    private void BindGrid()
    {
        if (tr_Catalog.Nodes.Count > 0 || (int)ViewState["Catalog"] > 0)
        {
            string ids = new KB_CatalogBLL((int)ViewState["Catalog"]).GetAllChildPosition();

            if (ids != "") ids += ",";
            ids += ViewState["Catalog"].ToString();
            string condition = " KB_Article.IsDelete = 'N' AND KB_Article.Catalog in (" + ids + ") AND (KB_Article.HasApproved = 'Y' OR KB_Article.UploadStaff=" + Session["UserID"].ToString() + ")";

            ud_grid.ConditionString = condition;
            
            if (ViewState["ConditionString"] != null)
                ud_grid.ConditionString += " AND " + ViewState["ConditionString"].ToString();
            ud_grid.BindGrid();
        }
    }

    private void BindTree(TreeNodeCollection TNC, int SuperID)
    {
        IList<KB_Catalog> _list = KB_CatalogBLL.GetModelList("SuperID=" + SuperID.ToString()); ;
        foreach (KB_Catalog _m in _list)
        {
            TreeNode tn = new TreeNode();
            tn.Text = _m.Name;
            tn.Value = _m.ID.ToString();
            TNC.Add(tn);
            BindTree(tn.ChildNodes, _m.ID);
        }
    }

    protected void tr_Catalog_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["Catalog"] = int.Parse(this.tr_Catalog.SelectedNode.Value);
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Search_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_keyword.Text != "")
        {
            string condition = "";
            if (this.chb_title.Checked == true)
            {
                condition = " KB_Article.Title like '%" + this.txt_keyword.Text + "%' ";
            }
            if (this.chb_content.Checked == true)
            {
                if (condition != "") condition += " OR ";
                condition += " KB_Article.Content like '%" + this.txt_keyword.Text + "%' ";
            }
            if (this.chb_keyword.Checked == true)
            {
                if (condition != "") condition += " OR ";
                condition += " KB_Article.KeyWord like '%" + this.txt_keyword.Text + "%' ";
            }
            if (this.chb_author.Checked == true)
            {
                if (condition != "") condition += " OR ";
                condition += " KB_Article.Author like '%" + this.txt_keyword.Text + "%' ";
            }

            if (condition == "")
            {
                MessageBox.Show(this, "请选择要查询的范围");
                return;
            }
            else
            {
                ViewState["ConditionString"] = "(" + condition + ")";
                ud_grid.PageIndex = 0;
                BindGrid();
            }
        }
        else
        {
            ViewState["ConditionString"] = null;
            ud_grid.PageIndex = 0;
            BindGrid();
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tr_Catalog.SelectedValue))
            Response.Redirect("NewArticle.aspx?Catalog=" + tr_Catalog.SelectedValue);
        else
            Response.Redirect("NewArticle.aspx");
    }
}
