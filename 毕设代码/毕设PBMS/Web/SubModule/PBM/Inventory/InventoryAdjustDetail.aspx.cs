using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.PBM;
using MCSFramework.Model;
using MCSFramework.Model.PBM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using System.Data;

public partial class SubModule_PBM_Inventory_InventoryAdjustDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                Response.Redirect("InventoryAdjustDetail0.aspx");
            }

        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        tr_Category.DataSource = PDT_CategoryBLL.GetListByOwnerClient((int)Session["OwnerType"], (int)Session["OwnerClient"]);
        tr_Category.DataBind();
        tr_Category.SelectValue = "1";
    }
    #endregion

    private void BindData()
    {
        PBM_DeliveryBLL bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
        if (bll.Model != null)
        {
            pl_detail.BindData(bll.Model);

            ViewState["Details"] = new ListTable<PBM_DeliveryDetail>(bll.Items, "ID");
            ViewState["WareHouse"] = bll.Model.ClientWareHouse;
            ViewState["State"] = bll.Model.State;
            ViewState["Classify"] = bll.Model.Classify;

            BindGrid();

            #region 界面控件可视状态
            if (bll.Model.State != 1 || bll.Model.ApproveFlag != 2)
            {
                bt_OK.Visible = false;

                bt_Delete.Visible = false;
                pl_detail.SetControlsEnable(false);

                gv_List.SetControlsEnable(false);
            }

            if (!((bll.Model.State == 1 && bll.Model.PrepareMode == 1) || (bll.Model.State == 3 && bll.Model.PrepareMode == 3)))
            {

                bt_Confirm.Visible = false;
            }

            #endregion
        }
    }

    private void BindGrid()
    {
        if (ViewState["Details"] != null)
        {
            ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

            int category = 1;
            int.TryParse(tr_Category.SelectValue, out category);
            if (category > 1)
            {
                DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_PUB.dbo.PDT_Category", "ID", "SuperID", category.ToString());
                List<int> categorys = new List<int>();
                foreach (DataRow dr in dt.Rows)
                {
                    categorys.Add((int)dr["ID"]);
                }
                categorys.Add(category);

                gv_List.BindGrid(Details.GetListItem().Where(p => categorys.Contains(new PDT_ProductBLL(p.Product).GetProductExtInfo((int)Session["OwnerClient"]).Category)).ToList());
            }
            else
                gv_List.BindGrid(Details.GetListItem());

            if (Details.GetListItem().Count == 0) bt_Confirm.Visible = false;
        }
    }

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Details"] == null) return;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            PBM_DeliveryDetail d = Details[id.ToString()];

            PDT_Product product = new PDT_ProductBLL(d.Product, true).Model;
            PDT_ProductExtInfo extInfo = new PDT_ProductBLL(d.Product, true).GetProductExtInfo((int)Session["OwnerClient"]);

            if (product == null || extInfo == null) return;
            Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("PDT_Packaging");
            string _T = dic[product.TrafficPackaging.ToString()].Name;
            string _P = dic[product.Packaging.ToString()].Name;

            #region 显示产品分类价格包装信息
            Label lb_ProductCategory = (Label)e.Row.FindControl("lb_ProductCategory");
            if (lb_ProductCategory != null || extInfo.Category > 0)
                lb_ProductCategory.Text = TreeTableBLL.GetFullPathName("MCS_PUB.dbo.PDT_Category", extInfo.Category);

            Label lb_Price = (Label)e.Row.FindControl("lb_Price");
            if (lb_Price != null)
            {
                lb_Price.Text = (d.Price * product.ConvertFactor).ToString("0.##") + "元 / " + _T + "(" + product.ConvertFactor.ToString() + _P + ")";
            }
            #endregion

            #region 显示产品调整数量信息
            Label lb_Quantity = (Label)e.Row.FindControl("lb_Quantity");
            DropDownList ddl_Mode = (DropDownList)e.Row.FindControl("ddl_Mode");
            TextBox tbx_Quantity_T = (TextBox)e.Row.FindControl("tbx_Quantity_T");
            TextBox tbx_Quantity_P = (TextBox)e.Row.FindControl("tbx_Quantity_P");
            Label lb_P_T = (Label)e.Row.FindControl("lb_P_T");
            Label lb_P_P = (Label)e.Row.FindControl("lb_P_P");
            if ((int)ViewState["State"] == 1)
            {
                lb_Quantity.Visible = false;

                if (ddl_Mode != null) ddl_Mode.SelectedValue = (d.DeliveryQuantity >= 0 ? "I" : "D");
                if (tbx_Quantity_T != null) tbx_Quantity_T.Text = Math.Abs(d.DeliveryQuantity / product.ConvertFactor).ToString();
                if (tbx_Quantity_P != null) tbx_Quantity_P.Text = Math.Abs(d.DeliveryQuantity % product.ConvertFactor).ToString();
                if (lb_P_T != null) lb_P_T.Text = _T;
                if (lb_P_P != null) lb_P_P.Text = _P;
            }
            else
            {
                if (d.DeliveryQuantity != 0)
                {
                    lb_Quantity.ForeColor = d.DeliveryQuantity >= 0 ? System.Drawing.Color.Blue : System.Drawing.Color.Red;
                    lb_Quantity.Text = d.DeliveryQuantity >= 0 ? "盘盈 " : "盘亏 ";

                    if (d.DeliveryQuantity / d.ConvertFactor != 0)
                        lb_Quantity.Text += (d.DeliveryQuantity / d.ConvertFactor).ToString() + _T;
                    if (d.DeliveryQuantity % d.ConvertFactor != 0)
                        lb_Quantity.Text += " " + (d.DeliveryQuantity % d.ConvertFactor).ToString() + _P;
                }
                ddl_Mode.Visible = false;
                tbx_Quantity_T.Visible = false;
                tbx_Quantity_P.Visible = false;
                lb_P_T.Visible = false;
                lb_P_P.Visible = false;
            }
            #endregion

            #region 显示库存数量
            Label lb_InventoryQuantity = (Label)e.Row.FindControl("lb_InventoryQuantity");
            if (lb_InventoryQuantity != null)
            {
                int inv_quantity = 0;
                int.TryParse(d["PreInventoryQuantity"], out inv_quantity);

                if (inv_quantity / product.ConvertFactor != 0)
                    lb_InventoryQuantity.Text = (inv_quantity / product.ConvertFactor).ToString() + _T;
                if (inv_quantity % product.ConvertFactor != 0)
                    lb_InventoryQuantity.Text += " " + (inv_quantity % product.ConvertFactor).ToString() + _P;
            }
            #endregion
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "盈亏小计";
            e.Row.Cells[5].Text = Details.GetListItem().Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.DeliveryQuantity / p.ConvertFactor).ToString("<font color=red size='larger'>0.##元</font>");
        }
    }

    protected void tr_Category_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        Save();
        BindGrid();
    }

    private bool Save()
    {
        if (ViewState["Details"] == null) return false;
        ListTable<PBM_DeliveryDetail> Details = (ListTable<PBM_DeliveryDetail>)ViewState["Details"];

        PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.Supplier == 0)
        {
            MessageBox.Show(this, "请正确选择供货商!");
            return false;
        }

        if (_bll.Model.SupplierWareHouse == 0)
        {
            MessageBox.Show(this, "请正确选择盘点仓库!");
            return false;
        }
        #endregion

        #region 循环设置盘点调整数量
        foreach (GridViewRow row in gv_List.Rows)
        {
            int _id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            PBM_DeliveryDetail d = Details[_id.ToString()];

            if (d == null) continue;
            int quantity = 0;
            TextBox tbx_Quantity_T = (TextBox)row.FindControl("tbx_Quantity_T");
            TextBox tbx_Quantity_P = (TextBox)row.FindControl("tbx_Quantity_P");

            if (tbx_Quantity_T != null)
            {
                int.TryParse(tbx_Quantity_T.Text, out quantity);
                d.DeliveryQuantity = quantity * d.ConvertFactor;
            }

            if (tbx_Quantity_P != null)
            {
                int.TryParse(tbx_Quantity_P.Text, out quantity);
                d.DeliveryQuantity += quantity;
            }

            DropDownList ddl_Mode = (DropDownList)row.FindControl("ddl_Mode");
            if (ddl_Mode != null && ddl_Mode.SelectedValue == "D")
            {
                int inv_quantity = 0;
                int.TryParse(d["PreInventoryQuantity"], out inv_quantity);
                if (inv_quantity < d.DeliveryQuantity)
                {
                    MessageBox.Show(this, "盘亏数量不能小于当前库存!");
                    return false;
                }

                d.DeliveryQuantity = -1 * d.DeliveryQuantity;
                d.SignInQuantity = d.DeliveryQuantity;
            }

            Details.Update(d);
        }
        #endregion

        _bll.Model.DiscountAmount = 0;
        _bll.Model.WipeAmount = 0;

        //实际成交价
        _bll.Model.ActAmount = Math.Round(Details.GetListItem().Sum(p => Math.Round(p.Price * p.ConvertFactor, 2) * p.DeliveryQuantity / p.ConvertFactor), 2);

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];

            #region 保存明细
            foreach (PBM_DeliveryDetail d in Details.GetListItem(ItemState.Modified))
            {
                _bll.UpdateDetail(d);
            }
            #endregion

            if (_bll.Update() == 0)
            {
                return true;
            }
        }

        return false;
    }



    protected void bt_OK_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            BindData();
        }


    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);
            _bll.Delete();
            Response.Redirect("InventoryAdjustList.aspx");
        }
    }
    protected void bt_Confirm_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (!Save()) return;

            PBM_DeliveryBLL _bll = new PBM_DeliveryBLL((int)ViewState["ID"]);


            int ret = _bll.Confirm((int)Session["UserID"]);
            switch (ret)
            {
                case 0:
                    Response.Redirect("InventoryAdjustList.aspx");
                    return;
                case -1:
                    MessageBox.Show(this, "对不起，单据确认失败! 单据状态不可操作");
                    return;
                case -2:
                    MessageBox.Show(this, "对不起，单据确认失败! 未指定正确的仓库");
                    return;
                case -10:
                    MessageBox.Show(this, "对不起，单据确认失败! 库存数量不够盘亏");
                    return;

                default:
                    MessageBox.Show(this, "对不起，单据确认失败! Ret=" + ret.ToString());
                    break;
            }
            Response.Redirect("InventoryAdjustList.aspx");
        }
    }

}