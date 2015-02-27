using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.SVM;
using MCSFramework.BLL;
using MCSControls.MCSWebControls;
using System.Collections.Generic;
using MCSFramework.Model.Pub;
using System.Linq;

public partial class SubModule_SVM_InventoryBatchInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["InventoryID"] = Request.QueryString["InventoryID"] == null ? 0 : int.Parse(Request.QueryString["InventoryID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            ViewState["IsCXP"] = Request.QueryString["IsCXP"] == null ? false : int.Parse(Request.QueryString["IsCXP"]) != 0;    //是否是赠品销量录入 0:成品 1:赠品
            #endregion

            ViewState["EditEnable"] = true;  
      
            if ((int)ViewState["InventoryID"] != 0)
            {
                if (Request.QueryString["Flag"] == null) cb_OnlyDisplayUnZero.Checked = true;
                BindData();
                btn_Inventory.Visible = false;
                TextBox tbx_InventoryDate = UC_DetailView1.FindControl("SVM_Inventory_InventoryDate") != null ? (TextBox)UC_DetailView1.FindControl("SVM_Inventory_InventoryDate") : null;                 
            }
            else
            {
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("InventoryList.aspx");

                #region 新增库存
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;

                MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_Inventory_Client");
                if (select_Client != null)
                {
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = c.FullName;
                    select_Client.Enabled = false;
                }

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_Inventory_OrganizeCity");
                if (tr_OrganizeCity != null)
                {
                    tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
                    tr_OrganizeCity.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_Inventory_AccountMonth");
                if (ddl_Month != null)
                {
                    double JXCDelayDays = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["JXCDelayDays"]);
                    ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays)).ToString();
                }

                #region 判断是否有未审核销售
                //if (c.ClientType == 2 && SVM_SalesVolumeBLL.GetModelList("Supplier=" + c.ID.ToString() + " AND ApproveFlag=2 AND AccountMonth=" + ddl_Month.SelectedValue).Count > 0)
                //{
                //    MessageBox.ShowAndRedirect(this, "该经销商本月还有未审核销量，请先审核销量再作此操作。", "SalesVolumeList.aspx?Type=2&SellOutClientID=" + c.ID.ToString());
                //    return;
                //}

                #endregion


                TextBox tbx_InventoryDate = (TextBox)UC_DetailView1.FindControl("SVM_Inventory_InventoryDate");
                if (tbx_InventoryDate != null)
                {
                    tbx_InventoryDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                bt_Del.Visible = false;
                bt_Save.Visible = false;
                bt_Approve.Visible = false;

                bt_Submit.Visible = false;
                bt_Approve.Visible = false;
                bt_Re_Approve.Visible = false;
                tb_AddProduct.Visible = false;
                #endregion
            }
            BindDropDown();

            #region 确定页面权限
            if ((int)ViewState["ClientID"] != 0)
            {
                CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                ViewState["ClientType"] = _r.ClientType;
                if (_r.ClientType == 3)
                {
                    Header.Attributes["WebPageSubCode"] += "ClientType=3";
                    bt_Submit.Enabled = false;
                    ViewState["EditEnable"] = false;
                    lbl_Notice.Text = "请以最小单位数量填报";
                }
                else if (_r.ClientType == 2)
                    Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + _r["DIClassify"];
            }
            #endregion
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_Inventory_AccountMonth");
            if (int.Parse(ddl_Month.SelectedValue) < AC_AccountMonthBLL.GetCurrentMonth() - 1)
            {
                bt_Re_Approve.Visible = false;
            }
        }

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if (ViewState["IsCXP"] == null)
            ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1','9')");
        else
        {
            if ((bool)ViewState["IsCXP"])
            {
                ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('9')");
                select_Product.PageUrl += "?IsOpponent=9";
                select_Product1.PageUrl += "?IsOpponent=9";
            }
            else
            {
                ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent in('1')");
                select_Product.PageUrl += "?IsOpponent=1";
                select_Product1.PageUrl += "?IsOpponent=1";
            }
        }
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand_SelectedIndexChanged(null, null);
    }

    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion


    private void BindData()
    {
        SVM_InventoryBLL bll = new SVM_InventoryBLL((int)ViewState["InventoryID"]);

        ViewState["ClientID"] = bll.Model.Client;
        ViewState["IsCXP"] = bll.Model["IsCXP"] == "1";

        UC_DetailView1.BindData(bll.Model);
        UC_DetailView1.SetControlsEnable(false);
        BindGrid();

        if (bll.Model.ApproveFlag == 1)
        {
            gv_List.SetControlsEnable(false);

            bt_Approve.Visible = false;
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Del.Visible = false;
            tb_AddProduct.Visible = false;
        }
        else
        {
            bt_Re_Approve.Visible = false;
            bt_Del.Visible = (int)Session["UserID"] == bll.Model.InsertStaff;

            if (bll.Model["SubmitFlag"] == "1")
                bt_Submit.Visible = false;       //已提交
            else
                bt_Approve.Visible = false;     //未提交
        }
        if (!(bool)ViewState["IsCXP"]) tb_AddProduct.Visible = false;
    }

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] == 0)
        {
            MessageBox.Show(this, "请先点击“填报库存”按钮，再填写具体的产品！");
            return;
        }
        if (bt_Save.Visible || bt_Approve.Visible) Save();
        BindGrid();
    }

    protected void cb_OnlyDisplayUnZero_CheckedChanged(object sender, EventArgs e)
    {
        if (bt_Save.Visible || bt_Approve.Visible) Save();
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void Save()
    {
        SVM_InventoryBLL bll = new SVM_InventoryBLL((int)ViewState["InventoryID"]);
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_Inventory_AccountMonth");
        bll.Model.InventoryDate = DateTime.Now.Date;
        bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
        bll.Model["SubmitFlag"] = "2";
        bll.Update();
        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex][0];
            SVM_Inventory_Detail item = bll.GetDetailModel(id);

            item.Quantity = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text) * new PDT_ProductBLL(item.Product).Model.ConvertFactor +
                int.Parse(((TextBox)gr.FindControl("tbx_Quantity2")).Text);
            item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;

            bll.UpdateDetail(item);
        }

    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        string condition = "SVM_Inventory_Detail.InventoryID=" + ViewState["InventoryID"].ToString();
        if (ddl_Classify.SelectedValue.CompareTo("0") > 0)
        {
            condition += " AND PDT_Product.Classify=" + ddl_Classify.SelectedValue;
        }
        else if (ddl_Brand.SelectedValue.CompareTo("0") > 0)
        {
            condition += "AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        }

        if (select_Product1.SelectValue != "")
        {
            condition += " AND SVM_Inventory_Detail.Product=" + select_Product1.SelectValue;
        }

        if (cb_OnlyDisplayUnZero.Checked)
        {
            condition += " AND SVM_Inventory_Detail.Quantity <> 0";
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();

        if (!bt_Save.Visible && !bt_Approve.Visible)
        {
            gv_List.SetControlsEnable(false);
        }
    }
    #endregion


    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Save();

        if (sender != null)
            MessageBox.ShowAndRedirect(this, "数据暂存成功,请在所有门店及经分销商的库存全部录入完成后，及时到汇总页面统一提交!",
                "InventoryBatchInput.aspx?InventoryID=" + ViewState["InventoryID"].ToString() + "&Flag=1&IsCXP=" + ((bool)ViewState["IsCXP"] ? "1" : "0"));
    }
    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        Save();

        if ((int)ViewState["InventoryID"] > 0)
        {
            SVM_InventoryBLL bll = new SVM_InventoryBLL((int)ViewState["InventoryID"]);
            bll.Model["SubmitFlag"] = "1";
            bll.Update();
            MessageBox.ShowAndRedirect(this, "数据保存并提交成功!", "InventoryList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }
    #endregion

    protected void btn_Inventory_Click(object sender, EventArgs e)
    {
        #region 创建空的销量列表
        MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_Inventory_Client");
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_Inventory_AccountMonth");

        if (select_Client.SelectValue != "")
        {
            TextBox tbx_InventoryDate = (TextBox)UC_DetailView1.FindControl("SVM_Inventory_InventoryDate");
            if (tbx_InventoryDate != null)
            {
                int id = SVM_InventoryBLL.InitProductList(int.Parse(ddl_Month.SelectedValue), int.Parse(select_Client.SelectValue), DateTime.Parse(tbx_InventoryDate.Text), (int)Session["UserID"], (bool)ViewState["IsCXP"]); //空的

                Response.Redirect("InventoryBatchInput.aspx?InventoryID=" + id.ToString() + "&Flag=1&IsCXP=" + ((bool)ViewState["IsCXP"] ? "1" : "0"));
            }
        }
        #endregion
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            Save();

            new SVM_InventoryBLL((int)ViewState["InventoryID"]).Approve((int)Session["UserID"]);

            Response.Redirect("InventoryList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            new SVM_InventoryBLL((int)ViewState["InventoryID"]).Delete();
            Response.Redirect("InventoryList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_Re_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["InventoryID"] != 0)
        {
            new SVM_InventoryBLL((int)ViewState["InventoryID"]).Cancel_Approve((int)Session["UserID"]);

            Response.Redirect("InventoryList.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }
    protected void bt_AddProduct_Click(object sender, EventArgs e)
    {
        SVM_InventoryBLL bll = new SVM_InventoryBLL((int)ViewState["InventoryID"]);
        int product = 0;
        if (int.TryParse(select_Product.SelectValue, out product) && product > 0)
        {
            PDT_Product pdt = new PDT_ProductBLL(product).Model;
            if (pdt == null) return;
            int quantity = int.Parse(tbx_Q1.Text) * pdt.ConvertFactor + int.Parse(tbx_Q2.Text);
            if (quantity == 0) return;
            SVM_Inventory_Detail _detail = bll.Items.FirstOrDefault(m => m.Product == product);
            if (_detail == null)
            {
                _detail = new SVM_Inventory_Detail();
                decimal factoryprice = 0, price = 0;
                PDT_ProductPriceBLL.GetPriceByClientAndType((int)ViewState["ClientID"], product, 2, out factoryprice, out price);
                _detail.FactoryPrice = factoryprice;
                _detail.Product = pdt.ID;
                _detail.Quantity = quantity;
                _detail.InventoryID = (int)ViewState["InventoryID"];
                bll.AddDetail(_detail);
            }
            else
            {
                _detail.Quantity = quantity;
                bll.UpdateDetail(_detail);
            }

            BindGrid();
        }
    }

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            TextBox tbx_Quantity1 = (TextBox)row.FindControl("tbx_Quantity1");
            if (tbx_Quantity1 != null) tbx_Quantity1.Attributes.Add("onkeydown", "javascript:keyDown(this)");

            TextBox tbx_Quantity2 = (TextBox)row.FindControl("tbx_Quantity2");
            if (tbx_Quantity2 != null) tbx_Quantity2.Attributes.Add("onkeydown", "javascript:keyDown(this)");

            TextBox tbx_Remark = (TextBox)row.FindControl("tbx_Remark");
            if (tbx_Remark != null) tbx_Remark.Attributes.Add("onkeydown", "javascript:keyDown(this)");

            if (row.RowIndex == 0)
            {
                row.BackColor = System.Drawing.Color.White;
                continue;
            }

            string p = (string)gv_List.DataKeys[row.RowIndex - 1][1];
            string c = (string)gv_List.DataKeys[row.RowIndex][1];

            if (p == c)
            {
                row.BackColor = gv_List.Rows[row.RowIndex - 1].BackColor;
            }
            else
            {
                if (gv_List.Rows[row.RowIndex - 1].BackColor == System.Drawing.Color.White)
                    row.BackColor = System.Drawing.Color.FromArgb(220, 220, 220);
                else
                    row.BackColor = System.Drawing.Color.White;
            }
        }
    }

}
