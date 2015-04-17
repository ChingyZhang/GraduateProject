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
            BindDropDown();
            if (tr_Catalog.Nodes.Count > 0)
            {
                ViewState["Catalog"] = int.Parse(tr_Catalog.Nodes[0].Value);
                BindGrid();
            }
        }
    }

    
    private void BindDropDown()
    {
        BindTree(tr_Catalog.Nodes, 0);
    }

    private void BindGrid()
    {
        string ids = new KB_CatalogBLL((int)ViewState["Catalog"]).GetAllChildPosition();
        if (ids != "") ids += ",";
        ids += ViewState["Catalog"].ToString();

        string condition = "KB_Article.Catalog in (" + ids + ")";

        if (cb_OnlyNoApprove.Checked)
            condition += " AND KB_Article.HasApproved= 'N'";
        else
            condition += " AND KB_Article.HasApproved= 'Y'";

        if (cb_DisplayDeleted.Checked)
            condition += " AND KB_Article.IsDelete = 'Y'";
        else
            condition += " AND KB_Article.IsDelete = 'N'";

        ud_grid.ConditionString = condition;
        ud_grid.BindGrid();
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

    protected void btn_Catalog_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatalogManager.aspx");
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewArticle.aspx");
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                new KB_ArticleBLL().DeleteByID(int.Parse(ud_grid.DataKeys[gr.RowIndex]["KB_Article_ID"].ToString()));
                //new KB_ArticleBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex]["ID"].ToString())).DeleteByID();
            }
        }
        ud_grid.PageIndex = 0;
        BindGrid();
    }
    protected void cb_DisplayDeleted_CheckedChanged(object sender, EventArgs e)
    {
        ud_grid.PageIndex = 0;
        BindGrid();
    }
    protected void cb_OnlyNoApprove_CheckedChanged(object sender, EventArgs e)
    {
        ud_grid.PageIndex = 0;
        BindGrid();
    }
}
