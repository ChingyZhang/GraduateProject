using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Product_Pop_Search_Product_TDP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();

            DataTable dt = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"], "");
            BindTree(dt, tr_List.Nodes, 0);

            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindTree(DataTable dt, TreeNodeCollection TNC, int SuperID)
    {
        dt.DefaultView.RowFilter = "SuperID=" + SuperID.ToString();

        foreach (DataRowView dr in dt.DefaultView)
        {
            TreeNode tn = new TreeNode();
            tn.Text = dr["Name"].ToString();
            tn.Value = dr["ID"].ToString();

            TNC.Add(tn);

            BindTree(dt, tn.ChildNodes, (int)dr["ID"]);
        }
    }
    protected void tr_List_SelectedNodeChanged(object sender, EventArgs e)
    {
        this.tr_List.SelectedNode.Expand();

        gv_List.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        string ConditionStr = " PDT_ProductExtInfo.Supplier=" + Session["OwnerClient"].ToString() + " AND PDT_ProductExtInfo.SalesState = 1";

        if (tr_List.SelectedValue != "" && tr_List.SelectedValue != "1")
        {
            string _categoryids = "";
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", tr_List.SelectedValue);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _categoryids += dt.Rows[i]["ID"].ToString() + ",";
            }
            _categoryids += tr_List.SelectedValue;

            ConditionStr += " AND PDT_Product.Category IN (" + _categoryids + ")";
        }

        if (tbx_SearchKey.Text != "")
        {
            ConditionStr += " AND (";
            ConditionStr += " PDT_Product.FullName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.ShortName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.BarCode LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.FactoryName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.FactoryCode LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += ")";
        }
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button bt_select = e.Row.FindControl("bt_select") != null ? (Button)e.Row.FindControl("bt_select") : null;
        if (bt_select != null)
        {
            bt_select.OnClientClick = "f_ReturnValue('" + gv_List.DataKeys[e.Row.RowIndex].Values[0].ToString() + "|" + gv_List.DataKeys[e.Row.RowIndex].Values[1].ToString() + "')";
        }
    }
}