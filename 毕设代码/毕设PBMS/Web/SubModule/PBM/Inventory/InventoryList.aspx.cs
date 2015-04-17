using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Inventory_InventoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["WareHouseID"] = Request.QueryString["WareHouseID"] != null ? int.Parse(Request.QueryString["WareHouseID"]) : 0;
            #endregion

            BindDropDown();
            if ((int)ViewState["WareHouseID"] > 0 && ddl_WareHouse.Items.FindByValue(ViewState["WareHouseID"].ToString()) != null)
            {
                ddl_WareHouse.SelectedValue = ViewState["WareHouseID"].ToString();
            }
            DataTable dt = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"], "");
            BindTree(dt, tr_List.Nodes, 0);

            BindGrid();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_WareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_WareHouse.DataBind();
        ddl_WareHouse.Items.Insert(0, new ListItem("请选择...", "0"));
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
    protected void ddl_WareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();


    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    private void BindGrid()
    {
        string ConditionStr = "INV_Inventory.Quantity <> 0 ";

        if (ddl_WareHouse.SelectedValue != "0")
            ConditionStr += " AND Inv_Inventory.WareHouse = " + ddl_WareHouse.SelectedValue;
        else
            ConditionStr += " AND INV_Inventory.WareHouse IN (SELECT ID FROM MCS_CM.dbo.CM_WareHouse WHERE Client=" + Session["OwnerClient"].ToString() + ")";

        if (tr_List.SelectedValue != "" && tr_List.SelectedValue != "1")
        {
            string _categoryids = "";
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_Pub.dbo.PDT_Category", "ID", "SuperID", tr_List.SelectedValue);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _categoryids += dt.Rows[i]["ID"].ToString() + ",";
            }
            _categoryids += tr_List.SelectedValue;

            ConditionStr += " AND INV_Inventory.Product IN (SELECT ID FROM MCS_PUB.dbo.PDT_Product WHERE Category IN (" + _categoryids + "))";
        }

        if (tbx_SearchKey.Text != "")
        {
            ConditionStr += " AND (INV_Inventory.Product IN (SELECT ID FROM MCS_PUB.dbo.PDT_Product WHERE ";
            ConditionStr += " FullName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR ShortName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR BarCode LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR FactoryName LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += "OR FactoryCode LIKE '%" + tbx_SearchKey.Text + "%' ";
            ConditionStr += ") )";
        }

        IList<INV_Inventory> lists = INV_InventoryBLL.GetModelList(ConditionStr);
        ViewState["TotalInventory"] = lists.Sum(p => p.Price * p.Quantity);
        gv_List.BindGrid(lists);

        gv_List.Columns[gv_List.Columns.Count - 1].Visible = ddl_WareHouse.SelectedValue != "0";
        bt_Adjust.Enabled = ddl_WareHouse.SelectedValue != "0";
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            long id = (long)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            INV_Inventory inv = new INV_InventoryBLL(id).Model;
            if (inv == null) return;

            PDT_Product product = new PDT_ProductBLL(inv.Product, true).Model;
            if (product == null) return;
            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[product.TrafficPackaging.ToString()].Name;
            string _P = dic[product.Packaging.ToString()].Name;

            #region 显示产品价格包装信息
            Label lb_Price_T = (Label)e.Row.FindControl("lb_Price_T");
            if (lb_Price_T != null)
            {
                lb_Price_T.Text = (inv.Price * product.ConvertFactor).ToString("0.##") + "元 /" + _T + "(" + product.ConvertFactor.ToString() + _P + ")";
            }

            Label lb_Price_P = (Label)e.Row.FindControl("lb_Price_P");
            if (lb_Price_P != null)
            {
                lb_Price_P.Text = inv.Price.ToString("0.##") + "元 /" + _P;
            }
            #endregion

            #region 显示产品数量信息
            Label lb_Quantity = (Label)e.Row.FindControl("lb_Quantity");
            if (lb_Quantity != null)
            {
                lb_Quantity.Text = (inv.Quantity / product.ConvertFactor).ToString() + _T;
                if (inv.Quantity % product.ConvertFactor > 0)
                    lb_Quantity.Text += " " + (inv.Quantity % product.ConvertFactor).ToString() + _P;
            }
            #endregion
        }
        else if (e.Row.RowType == DataControlRowType.Footer && ViewState["TotalInventory"] != null)
        {
            e.Row.Cells[1].Text = "小计库存额";
            e.Row.Cells[5].Text = ((decimal)ViewState["TotalInventory"]).ToString("<font color=red>0.##元</font>");
        }
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }




    protected void bt_Adjust_Click(object sender, EventArgs e)
    {
        int warehouse = 0;
        int.TryParse(ddl_WareHouse.SelectedValue, out warehouse);

        string ids = "";
        foreach (GridViewRow row in gv_List.Rows)
        {
            long id = (long)gv_List.DataKeys[row.RowIndex]["ID"];
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                ids += id.ToString() + ",";
            }
        }
        if (ids.EndsWith(",")) ids = ids.Substring(0, ids.Length - 1);
        if (warehouse == 0)
        {
            MessageBox.Show(this, "请指定要调整库存的仓库!");
            return;
        }
        if (ids == "")
        {
            MessageBox.Show(this, "请打勾选中要调整库存的产品!");
            return;
        }

        int deliveryid = PBM_DeliveryBLL.AdjustInit(warehouse, (int)Session["UserID"], ids);
        if (deliveryid > 0)
        {
            Response.Redirect("InventoryAdjustDetail.aspx?ID=" + deliveryid.ToString());
        }
    }
}