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
            ViewState["ID"] = Request.QueryString["KB_Article_ID"] == null ? 0 : int.Parse(Request.QueryString["KB_Article_ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindGrid1();
            }
            //ViewState["Dimission"] = 1;
            ViewState["ConditionString"] = null;
            ViewState["Position"] = 0;
            ViewState["Catalog"] = 0;
            BindDropDown();
            BindGrid();
        }
    }

    protected void trPosition_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["Position"] = int.Parse(tr_Catalog.SelectedValue);
        BindGrid();
    }

    private void BindDropDown()
    {
        BindTree(tr_Catalog.Nodes, 0);
    }

    private void BindGrid()
    {
        string condition = " KB_Article.IsDelete= 'N'" ;
        if ((int)ViewState["Catalog"] != 0)
        {
            condition += " and KB_Article.Catalog =" + ViewState["Catalog"].ToString();
            ud_grid.ConditionString = condition;

            if (ViewState["ConditionString"] != null)
            {
                //ud_grid.ConditionString += " and " + ViewState["ConditionString"].ToString();
                ud_grid.ConditionString += " AND " + ViewState["ConditionString"].ToString();
            }
        }
        //ud_grid.ConditionString = condition;
        //string condition = "org_staff.Dimission=" + ViewState["Dimission"].ToString();
        //if ((int)ViewState["Position"] != 0)
        //    condition += " and org_staff.Position=" + ViewState["Position"].ToString();
        //ud_grid.ConditionString = condition;
        else
        {
            if (ViewState["ConditionString"] != null)
            {
                //ud_grid.ConditionString += " and " + ViewState["ConditionString"].ToString();
                ud_grid.ConditionString = ViewState["ConditionString"].ToString() + " and " + condition;
            }
            else
            {
                ud_grid.ConditionString = condition;
            }
        }
        //ud_grid.ConditionString = "";
        //string condition = "";
        ud_grid.BindGrid();
        
    }

    public void BindGrid1()
    {
        ud_grid1.ConditionString = " KB_Comment.Article =" + (int)ViewState["ID"];

        ud_grid1.BindGrid();
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
        BindGrid();
    }
    
    
    
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid1.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                new KB_ArticleBLL().DeleteByID(int.Parse(ud_grid1.DataKeys[gr.RowIndex]["KB_Comment_ID"].ToString()));
                //new KB_ArticleBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex]["ID"].ToString())).DeleteByID();
            }
        }
        BindGrid();
    }
}
