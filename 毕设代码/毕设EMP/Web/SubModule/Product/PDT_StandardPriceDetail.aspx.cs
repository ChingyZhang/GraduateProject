using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using MCSControls.MCSTabControl;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSControls.MCSWebControls;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;

public partial class SubModule_Product_PDT_StandardPriceDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["PriceID"] = Request.QueryString["PriceID"] == null ? 0 : int.Parse(Request.QueryString["PriceID"]);
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            #endregion

            BindDropDown();
            ViewState["LabelVisiable"] = false;
            if ((int)ViewState["PriceID"] != 0)//修改客户价表
            {
                BindData();
            }
            else
            {
                btn_Apply.Visible = false;
                btn_UnActive.Visible = false;
                btn_Approve.Visible = false;
                btn_UnApprove.Visible = false;
                btn_ApplyCity.Visible = false;

                if ((int)ViewState["OrganizeCity"] != 0)
                {
                    MCSTreeControl tr_OrganizeCity = panel1.FindControl("PDT_StandardPrice_OrganizeCity") != null ? (MCSTreeControl)panel1.FindControl("PDT_StandardPrice_OrganizeCity") : null;
                    tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
                }
            }

        }

        DropDownList ddl_IsRebatePrice = panel1.FindControl("PDT_StandardPrice_IsRebatePrice") != null ? (DropDownList)panel1.FindControl("PDT_StandardPrice_IsRebatePrice") : null;

        if (ddl_IsRebatePrice != null)
        {
            ddl_IsRebatePrice.SelectedIndexChanged += new EventHandler(ddl_IsRebatePrice_SelectedIndexChanged);
            ddl_IsRebatePrice.AutoPostBack = true;
        }

        #region 注册弹出窗口
        string script = "function PopApplyCity(){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("PDT_StandardPrice_ApplyCity.aspx") +
            "?PriceID=" + ViewState["PriceID"].ToString() + "&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopApplyCity", script, true);
        string script2 = "function PopChangeHistory(){\r\n";
        script2 += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("PDT_StandardPriceChangeHistory.aspx") +
            "?ID=" + ViewState["PriceID"].ToString() + "&tempid='+tempid, window, 'dialogWidth:500px;DialogHeight=260px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopChangeHistory", script2, true);
        #endregion
    }

    private void BindData()
    {
        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        panel1.BindData(_bll.Model);

        if (_bll.Model.ApproveFlag == 1 || (_bll.Model.TaskID != 0 && _bll.Model.ApproveFlag == 2))
        {
            btn_Save.Visible = false;
            tr_tab.Visible = false;
            btn_Apply.Visible = false;
            panel1.SetControlsEnable(false);
            btn_Approve.Visible = false;

            if (_bll.Model.ActiveFlag == 2)
            {
                btn_UnActive.Visible = false;
                btn_UnApprove.Visible = false;
                btn_PublishProduct.Visible = false;
            }

        }

        if (_bll.Model.ApproveFlag == 2)
        {
            btn_UnActive.Visible = false;
            btn_UnApprove.Visible = false;
            btn_PublishProduct.Visible = false;
        }
        if (_bll.Model["IsRebatePrice"] == "1")
        {
            gv_List.Columns[gv_List.Columns.Count - 3].Visible = true;//是否计算我司返利
            gv_List.Columns[gv_List.Columns.Count - 4].Visible = true;//经销商返利价
            gv_List.Columns[gv_List.Columns.Count - 5].Visible = true;//我司返利价
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = true;//是否需要判断积分店状态
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = true;//是否计算经销商返利
        }
        BindGrid();

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Brand.DataSource = new PDT_BrandBLL()._GetModelList("IsOpponent in('1')");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand.SelectedValue = "0";
        rbl_Brand_SelectedIndexChanged(null, null);
    }
    protected void rbl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Classify.DataSource = new PDT_ClassifyBLL()._GetModelList("Brand=" + ddl_Brand.SelectedValue);
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("全部", "0"));
        ddl_Classify.SelectedValue = "0";
    }
    #endregion

    protected void MCSTabControl1_OnTabClicked(object sender, MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            bt_In.Visible = false;
            bt_Out.Visible = true;
            btn_Save.Visible = true;
        }
        else
        {
            bt_In.Visible = true;
            bt_Out.Visible = false;
            btn_Save.Visible = false;
        }
        gv_List.PageIndex = 0;
        gv_List_FacProd.PageIndex = 0;
        BindGrid();
    }
    private void BindGrid()
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            tr1.Visible = false;
            tr_Product.Visible = true;

            string condition = " 1 = 1 ";

            if (ddl_Brand.SelectedValue != "0")
            {
                if (ddl_Classify.SelectedValue == "0")
                    condition = " Brand =" + ddl_Brand.SelectedValue;
                else
                    condition = " Classify =" + ddl_Classify.SelectedValue;
            }

            condition += " ORDER BY PDT_Product.Code";

            PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
            gv_List.BindGrid(bll.GetDetail(condition));

            if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 1410, "ModifyFactoryPrice"))
            {
                foreach (GridViewRow row in gv_List.Rows)
                {
                    TextBox tbx = (TextBox)row.FindControl("tbx_FactoryPrice");
                    if (tbx != null) tbx.Enabled = false;
                }
            }
        }
        else
        {
            tr1.Visible = true;
            tr_Product.Visible = false;

            //获取非价表产品列表
            string condition = "State=1 AND ID NOT IN (SELECT Product FROM PDT_StandardPrice_Detail WHERE StandardPrice=" + ViewState["PriceID"].ToString() + ")";

            if (ddl_Brand.SelectedValue == "0")
                condition += " AND Brand in (SELECT ID FROM PDT_Brand WHERE IsOpponent in ('1'))";
            else
            {
                if (ddl_Classify.SelectedValue == "0")
                    condition += " AND Brand =" + ddl_Brand.SelectedValue;
                else
                    condition += " AND Classify =" + ddl_Classify.SelectedValue;
            }

            IList<PDT_Product> products = PDT_ProductBLL.GetModelList(condition);

            gv_List_FacProd.BindGrid<PDT_Product>(products);
        }


    }

    #region 移入经营产品
    protected void bt_In_Click(object sender, EventArgs e)
    {
        Save();
        if ((int)ViewState["PriceID"] == 0) return;

        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

        foreach (GridViewRow row in gv_List_FacProd.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                PDT_Product product = new PDT_ProductBLL((int)gv_List_FacProd.DataKeys[row.RowIndex]["ID"]).Model;
                if (product != null)
                {
                    PDT_StandardPrice_Detail detail = new PDT_StandardPrice_Detail();
                    detail.StandardPrice = (int)ViewState["PriceID"];
                    detail.Product = product.ID;
                    detail.FactoryPrice = product.FactoryPrice;
                    detail.TradeOutPrice = product.TradePrice;
                    detail.TradeInPrice = product.NetPrice;
                    detail.StdPrice = product.StdPrice;
                    PDT_StandPriceChangeHistoryBLL history = new PDT_StandPriceChangeHistoryBLL();
                    history.Model.StandardPrice = _bll.Model.ID;
                    history.Model.ChangeType = 1;
                    history.Model.ChageTime = DateTime.Now;
                    history.Model.ChangeStaff = (int)Session["UserID"];
                    history.Model.Product = detail.Product;
                    history.Model.AftFactoryPrice = detail.FactoryPrice;
                    history.Model.AftTradeOutPrice = detail.TradeOutPrice;
                    history.Model.AftTradeInPrice = detail.TradeInPrice;
                    history.Model.AftStdPrice = detail.StdPrice;
                    history.Add();

                    _bll.AddDetail(detail);
                }
            }
        }

        Response.Redirect("PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }

    protected void bt_Out_Click(object sender, EventArgs e)
    {
        Save();
        if ((int)ViewState["PriceID"] == 0) return;

        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                PDT_StandardPrice_Detail item2 = _bll.GetDetailModel(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
                PDT_StandPriceChangeHistoryBLL history = new PDT_StandPriceChangeHistoryBLL();
                history.Model.StandardPrice = _bll.Model.ID;
                history.Model.ChangeType = 3;
                history.Model.ChageTime = DateTime.Now;
                history.Model.ChangeStaff = (int)Session["UserID"];
                history.Model.Product = item2.Product;
                history.Model.PreFactoryPrice = item2.FactoryPrice;
                history.Model.PreTradeOutPrice = item2.TradeOutPrice;
                history.Model.PreTradeInPrice = item2.TradeInPrice;
                history.Model.PreStdPrice = item2.StdPrice;
                history.Add();
                _bll.DeleteDetail(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            }
        }
        Response.Redirect("PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }
    #endregion

    private void Save()
    {
        PDT_StandardPriceBLL _bll;
        if ((int)ViewState["PriceID"] != 0)
            _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        else
            _bll = new PDT_StandardPriceBLL();

        panel1.GetData(_bll.Model);

        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "适用管理片区必填!");
            return;
        }

        if ((int)ViewState["PriceID"] != 0)
        {
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Update();

            #region 修改明细
            foreach (GridViewRow row in gv_List.Rows)
            {
                PDT_StandardPrice_Detail item1 = new PDT_StandardPrice_Detail();
                item1.ID = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
                item1.StandardPrice = _bll.Model.ID;
                item1.Product = int.Parse(gv_List.DataKeys[row.RowIndex]["Product"].ToString());
                item1.FactoryPrice = decimal.Parse(((TextBox)row.FindControl("tbx_FactoryPrice")).Text);
                item1.TradeOutPrice = decimal.Parse(((TextBox)row.FindControl("tbx_TradeOutPrice")).Text);
                item1.TradeInPrice = decimal.Parse(((TextBox)row.FindControl("tbx_TradeInPrice")).Text);
                item1.StdPrice = decimal.Parse(((TextBox)row.FindControl("tbx_StdPrice")).Text);
                TextBox tbx_RebatePrice = row.FindControl("tbx_RebatePrice") == null ? null : (TextBox)row.FindControl("tbx_RebatePrice");
                if (tbx_RebatePrice != null)
                {
                    decimal rebateprice = 0;
                    decimal.TryParse(tbx_RebatePrice.Text, out rebateprice);
                    item1.RebatePrice = rebateprice;
                }
                TextBox tbx_DIRebatePrice = row.FindControl("tbx_DIRebatePrice") == null ? null : (TextBox)row.FindControl("tbx_DIRebatePrice");
                if (tbx_DIRebatePrice != null)
                {
                    decimal direbateprice = 0;
                    decimal.TryParse(tbx_DIRebatePrice.Text, out direbateprice);
                    item1.DIRebatePrice = direbateprice;
                }
                CheckBox cbx_ISFL = row.FindControl("cbx_ISFL") == null ? null : (CheckBox)row.FindControl("cbx_ISFL");
                if (cbx_ISFL != null)
                {
                    item1.ISFL = cbx_ISFL.Checked ? 1 : 2;
                }
                CheckBox cbx_ISJH = row.FindControl("cbx_ISJH") == null ? null : (CheckBox)row.FindControl("cbx_ISJH");
                if (cbx_ISJH != null)
                {
                    item1.ISJH = cbx_ISJH.Checked ? 1 : 2;
                }

                CheckBox cbx_ISCheckJF = row.FindControl("cbx_ISCheckJF") == null ? null : (CheckBox)row.FindControl("cbx_ISCheckJF");
                if (cbx_ISCheckJF != null)
                {
                    item1.ISCheckJF = cbx_ISCheckJF.Checked ? 1 : 2;
                }
                PDT_StandardPrice_Detail item2 = _bll.GetDetailModel(item1.ID);
                if (item1.FactoryPrice != item2.FactoryPrice || item1.TradeInPrice != item2.TradeInPrice || item1.TradeOutPrice != item2.TradeOutPrice || item1.StdPrice != item2.StdPrice)
                {
                    PDT_StandPriceChangeHistoryBLL history = new PDT_StandPriceChangeHistoryBLL();
                    history.Model.StandardPrice = _bll.Model.ID;
                    history.Model.ChangeType = 2;
                    history.Model.ChageTime = DateTime.Now;
                    history.Model.ChangeStaff = (int)Session["UserID"];
                    history.Model.Product = item1.Product;
                    history.Model.PreFactoryPrice = item2.FactoryPrice;
                    history.Model.PreTradeOutPrice = item2.TradeOutPrice;
                    history.Model.PreTradeInPrice = item2.TradeInPrice;
                    history.Model.PreStdPrice = item2.StdPrice;
                    history.Model.AftFactoryPrice = item1.FactoryPrice;
                    history.Model.AftTradeInPrice = item1.TradeInPrice;
                    history.Model.AftTradeOutPrice = item1.TradeOutPrice;
                    history.Model.AftStdPrice = item1.StdPrice;
                    history.Add();
                }
                _bll.UpdateDetail(item1);
            }
            #endregion
        }
        else
        {
            _bll.Model.ApproveFlag = 2;
            _bll.Model.ActiveFlag = 2;
            _bll.Model.InsertStaff = int.Parse(Session["UserID"].ToString());
            _bll.Add();
            ViewState["PriceID"] = _bll.Model.ID;
        }
    }
    public string GetERPCode(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.Code;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Save();
        MessageBox.Show(this, "保存成功!");
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        if (_bll.GetApplyCityDetail().Count == 0 && _bll.Model["IsRebatePrice"] != "1")
        {
            MessageBox.Show(this, "请点击【适用区域】按钮，选择该标准价表适用于的区域!");
            return;
        }

        _bll.Approve((int)Session["UserID"]);
        MessageBox.ShowAndRedirect(this, "审核成功！", "PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }
    protected void btn_UnApprove_Click(object sender, EventArgs e)
    {
        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        _bll.Model.ActiveFlag = 1;
        _bll.Model.ApproveFlag = 2;
        _bll.Model.TaskID = 0;
        _bll.Model.UpdateStaff = (int)Session["UserID"];
        _bll.Update();
        MessageBox.ShowAndRedirect(this, "取消审核成功！", "PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List_FacProd.PageIndex = 0;
        BindGrid();
    }

    protected void gv_List_FacProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List_FacProd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btn_UnActive_Click(object sender, EventArgs e)
    {
        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

        if (_bll.Model.ActiveFlag == 1)
        {
            _bll.UnActive((int)Session["UserID"]);
            MessageBox.ShowAndRedirect(this, "标准价表停用成功！", "PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
        }
    }
    protected void btn_Apply_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] == 0)
        {
            MessageBox.Show(this, "对不起，请您先保存后在发起申请");
            return;
        }

        PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        if (bll.GetApplyCityDetail().Count == 0)
        {
            MessageBox.Show(this, "请点击【适用区域】按钮，选择该标准价表适用于的区域!");
            return;
        }

        bt_CompareStdPrice_Click(null, null);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", bll.Model.ID.ToString());
        dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
        dataobjects.Add("FullName", bll.Model.FullName);
        dataobjects.Add("MaxRate", ((decimal)ViewState["MaxRate"] * 100).ToString());

        int TaskID = EWF_TaskBLL.NewTask("PDT_StandardPrice_Apply", (int)Session["UserID"], "标准价表名称：" + bll.Model.FullName, "~/SubModule/Product/PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString(), dataobjects);
        if (TaskID > 0)
        {
            bll.Model.TaskID = TaskID;
            bll.Update();
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
        }
        Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
    }
    protected void btn_PublishProduct_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] == 0) return;

        PDT_StandardPriceBLL _bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);

        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                _bll.PublishProduct(int.Parse(gv_List.DataKeys[row.RowIndex]["Product"].ToString()));
            }
        }

        MessageBox.ShowAndRedirect(this, "产品发布成功！", "PDT_StandardPriceDetail.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }
    protected void bt_CompareStdPrice_Click(object sender, EventArgs e)
    {
        decimal MaxRate = 0.000M;

        PDT_StandardPriceBLL bll = new PDT_StandardPriceBLL((int)ViewState["PriceID"]);
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            PDT_StandardPrice_Detail d = bll.Items.FirstOrDefault(p => p.ID == id);
            if (d == null) continue;

            PDT_Product product = new PDT_ProductBLL(d.Product).Model;
            if (product == null) continue;


            Label lb_FactoryPrice_Rate = (Label)row.FindControl("lb_FactoryPrice_Rate");
            Label lb_TradeOutPrice_Rate = (Label)row.FindControl("lb_TradeOutPrice_Rate");
            Label lb_TradeInPrice_Rate = (Label)row.FindControl("lb_TradeInPrice_Rate");
            Label lb_StdPrice_Rate = (Label)row.FindControl("lb_StdPrice_Rate");

            if (product.FactoryPrice != 0 && lb_FactoryPrice_Rate != null)
            {
                decimal price = decimal.Parse(((TextBox)row.FindControl("tbx_FactoryPrice")).Text);
                decimal rate = (price - product.FactoryPrice) / product.FactoryPrice;
                if (sender != null) lb_FactoryPrice_Rate.Text = rate.ToString("0.#%");

                if (Math.Abs(rate) > MaxRate) MaxRate = Math.Abs(rate);
            }

            if (product.TradePrice != 0 && lb_TradeOutPrice_Rate != null)
            {
                decimal price = decimal.Parse(((TextBox)row.FindControl("tbx_TradeOutPrice")).Text);
                decimal rate = (price - product.TradePrice) / product.TradePrice;
                if (sender != null) lb_TradeOutPrice_Rate.Text = rate.ToString("0.#%");

                if (Math.Abs(rate) > MaxRate) MaxRate = Math.Abs(rate);
            }

            if (product.NetPrice != 0 && lb_TradeInPrice_Rate != null)
            {
                decimal price = decimal.Parse(((TextBox)row.FindControl("tbx_TradeInPrice")).Text);
                decimal rate = (price - product.NetPrice) / product.NetPrice;
                if (sender != null) lb_TradeInPrice_Rate.Text = rate.ToString("0.#%");

                if (Math.Abs(rate) > MaxRate) MaxRate = Math.Abs(rate);
            }

            if (product.StdPrice != 0 && lb_StdPrice_Rate != null)
            {
                decimal price = decimal.Parse(((TextBox)row.FindControl("tbx_StdPrice")).Text);
                decimal rate = (price - product.StdPrice) / product.StdPrice;
                if (sender != null) lb_StdPrice_Rate.Text = rate.ToString("0.#%");

                if (Math.Abs(rate) > MaxRate) MaxRate = Math.Abs(rate);
            }
        }

        if (sender != null) lb_MaxRate.Text = string.Format("价格最大偏差{0:0.#%}", MaxRate);
        ViewState["MaxRate"] = MaxRate;

    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        ViewState["LabelVisiable"] = true;
        gv_List.AllowPaging = false;
        BindGrid();
        string filename = HttpUtility.UrlEncode("价盘导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();

        ViewState["LabelVisiable"] = false;
        gv_List.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    public void ddl_IsRebatePrice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_IsRebatePrice = (DropDownList)sender;
        gv_List.Columns[gv_List.Columns.Count - 1].Visible = ddl_IsRebatePrice.SelectedValue == "1";
        gv_List.Columns[gv_List.Columns.Count - 2].Visible = ddl_IsRebatePrice.SelectedValue == "1";
    }
}
