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

public partial class SubModule_PBM_Product_ProductList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["Category"] = Request.QueryString["Category"] != null ? int.Parse(Request.QueryString["Category"]) : 0;

            BindDropDown();

            DataTable dt = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"], "");
            BindTree(dt, tr_List.Nodes, 0);

            if ((int)ViewState["Category"] != 0) ExpandNode();

            BindGrid();
        }

        string script = "function PopMore(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("Pop_SelectMoreProduct.aspx") +
            "?tempid='+tempid, window, 'dialogWidth:900px;DialogHeight=650px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopMore", script, true);
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_SalesState.DataSource = DictionaryBLL.GetDicCollections("PDT_SalesState");
        ddl_SalesState.DataBind();
        ddl_SalesState.Items.Insert(0, new ListItem("全部", "0"));
        ddl_SalesState.SelectedValue = "1";
    }
    #endregion

    #region Reload the TreeNode by special id
    private void ExpandNode()
    {
        DataTable _fullpath = TreeTableBLL.GetFullPath("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", (int)ViewState["Category"]);
        for (int i = 0; i < _fullpath.Rows.Count; i++)
        {
            int _id = int.Parse(_fullpath.Rows[i]["ID"].ToString());
            if (_id != 1)
            {
                string _valuepath = "";
                for (int j = 0; j <= i; j++)
                {
                    _valuepath += _fullpath.Rows[j]["ID"].ToString() + "/";
                }
                _valuepath = _valuepath.Substring(0, _valuepath.Length - 1);

                TreeNode node = tr_List.FindNode(_valuepath);
                if (node != null)
                {
                    node.Expand();
                    node.Selected = true;
                }
            }
        }

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
            //tn.ImageUrl = "~/Images/gif/gif-0030.gif";
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
        string ConditionStr = " PDT_ProductExtInfo.Supplier=" + Session["OwnerClient"].ToString();
        if (tr_List.SelectedValue != "" && tr_List.SelectedValue != "1")
        {
            string _categoryids = "";
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", tr_List.SelectedValue);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _categoryids += dt.Rows[i]["ID"].ToString() + ",";
            }
            _categoryids += tr_List.SelectedValue;

            ConditionStr += " AND PDT_ProductExtInfo.Category IN (" + _categoryids + ")";
        }

        if (ddl_SalesState.SelectedValue != "0")
            ConditionStr += " AND PDT_ProductExtInfo.SalesState=" + ddl_SalesState.SelectedValue;

        if (tbx_SearchKey.Text != "")
        {
            ConditionStr += " AND (";
            ConditionStr += " PDT_Product.FullName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.ShortName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.BarCode LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_Product.FactoryName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR PDT_ProductExtInfo.Code LIKE '%" + tbx_SearchKey.Text + "%' ";
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

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if (tr_List.SelectedValue == "" || tr_List.SelectedValue == "1")
            MessageBox.Show(this, "请选择要新增商品的目录!");
        else
            Response.Redirect("ProductDetail.aspx?Category=" + tr_List.SelectedValue);
    }
    protected void bt_AddMoreProduct_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void ddl_SalesState_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}