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

public partial class SubModule_PBM_Product_Pop_SelectMoreProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
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
        string ConditionStr = " PDT_Product.ID NOT IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString() + ")";
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

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["PDT_Product_ID"];

                PDT_ProductBLL bll = new PDT_ProductBLL(id);
                PDT_ProductExtInfo extinfo = new PDT_ProductExtInfo();
                extinfo.Code = bll.Model.FactoryCode == "" ? bll.Model.Code : bll.Model.FactoryCode;
                extinfo.Supplier = (int)Session["OwnerClient"];
                bll.SetProductExtInfo(extinfo);
                count++;
            }
        }
        if (count > 0)
        {
            MessageBox.Show(this, string.Format("成功将{0}个商品加入经营范围!", count));
            cbx_CheckAll.Checked = false;
            BindGrid();
        }
        else
            MessageBox.Show(this, "请选择要加入经营产商品!");
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null)
            {
                cbx.Checked = cbx_CheckAll.Checked;
            }
        }
    }
    protected void bt_AddAll_Click(object sender, EventArgs e)
    {
        string ConditionStr = " PDT_Product.ID NOT IN (SELECT Product FROM MCS_PUB.dbo.PDT_ProductExtInfo WHERE Supplier=" + Session["OwnerClient"].ToString() + ")";
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
        int count = 0;
        IList<PDT_Product> product = PDT_ProductBLL.GetModelList(ConditionStr);
        foreach (PDT_Product p in product)
        {
            int id = p.ID;

            PDT_ProductBLL bll = new PDT_ProductBLL(id);
            PDT_ProductExtInfo extinfo = new PDT_ProductExtInfo();
            extinfo.Code = bll.Model.FactoryCode == "" ? bll.Model.Code : bll.Model.FactoryCode;
            extinfo.Supplier = (int)Session["OwnerClient"];
            bll.SetProductExtInfo(extinfo);
            count++;
        }
        if (count > 0)
        {
            MessageBox.Show(this, string.Format("成功将{0}个商品加入经营范围!", count));
            BindGrid();
        }
        else
            MessageBox.Show(this, "请选择要加入经营产商品!");
    }

}