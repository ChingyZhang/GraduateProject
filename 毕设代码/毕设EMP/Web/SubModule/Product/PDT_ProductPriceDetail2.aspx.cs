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

public partial class SubModule_Product_PDT_ProductPriceDetail2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["PriceID"] = Request.QueryString["PriceID"] == null ? 0 : int.Parse(Request.QueryString["PriceID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，1.办事处 ２：经销商，３：终端门店 4.总部
            ViewState["StandardPrice"] = Request.QueryString["StandardPrice"] == null ? 0 : int.Parse(Request.QueryString["StandardPrice"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["PriceID"] != 0)//修改客户价表
            {
                BindData();
            }
            else if ((int)ViewState["ClientID"] > 0 && (int)ViewState["StandardPrice"] > 0)
            {
                int priceid = PDT_ProductPriceBLL.CopyFromStandardPrice((int)ViewState["StandardPrice"], (int)ViewState["ClientID"], (int)Session["UserID"]);
                if (priceid > 0)
                {
                    MessageBox.ShowAndRedirect(this, "成功从标准价表关联成功！", "PDT_ProductPriceDetail2.aspx?PriceID=" + priceid.ToString());
                }
                else
                {
                    tbx_begin.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    tbx_end.Text = DateTime.Now.AddMonths(12).ToString("yyyy-MM-dd");

                    BindClient();
                    btn_Delete.Visible = false;
                    btn_Approve.Visible = false;
                    btn_UnApprove.Visible = false;
                }
            }
            else
                Response.Redirect("PDT_ProductPrice.aspx");
        }
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

    #region 绑定信息
    private void BindClient()
    {
        CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        lbl_Client.Text = _r.FullName;
        ViewState["ClientType"] = _r.ClientType;

        if (_r.ClientType == 3)
            Header.Attributes["WebPageSubCode"] += "ClientType=3";
        else if (_r.ClientType == 2)
            Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + _r["DIClassify"];
    }
    #endregion

    private void BindData()
    {
        PDT_ProductPrice sv = new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Model;
        ViewState["ClientID"] = sv.Client;
        ViewState["StandardPrice"] = sv.StandardPrice;

        if ((int)ViewState["StandardPrice"] > 0)
        {
            PDT_StandardPrice s = new PDT_StandardPriceBLL((int)ViewState["StandardPrice"]).Model;
            if (s != null) lbl_StandardPrice.Text = s.FullName;
        }

        BindClient();
        tbx_begin.Text = sv.BeginDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : sv.BeginDate.ToString("yyyy-MM-dd");
        tbx_end.Text = sv.EndDate.ToString("yyyy-MM-dd") == "1900-01-01" ? "" : sv.EndDate.ToString("yyyy-MM-dd");

        if (sv.ApproveFlag == 1)
        {
            tbx_begin.Enabled = false;
            tbx_end.Enabled = false;
            gv_List.Columns[0].Visible = false;

            btn_Save.Visible = false;
            btn_Delete.Visible = false;
            tr_tab.Visible = false;
            lb_ApproveFlag.Text = "已审核";
            btn_Approve.Visible = false;

            if (sv.EndDate < DateTime.Now)
            {
                bt_Stop.Visible = false;
                btn_UnApprove.Visible = false;
            }
        }
        else
        {
            bt_Stop.Visible = false;
            btn_UnApprove.Visible = false;
        }

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
            PDT_ProductPriceBLL bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
            gv_List.BindGrid(bll.GetDetail(condition));
        }
        else
        {
            tr1.Visible = true;
            tr_Product.Visible = false;

            //获取非价表产品列表
            string condition = "State = 1 AND ID NOT IN (SELECT Product FROM MCS_Pub.dbo.PDT_ProductPrice_Detail WHERE PriceID=" + ViewState["PriceID"].ToString() + ")";

            if (ddl_Brand.SelectedValue == "0")
                condition += " AND Brand in (SELECT ID FROM PDT_Brand WHERE IsOpponent in ('1'))";
            else
            {
                if (ddl_Classify.SelectedValue == "0")
                    condition += " AND Brand =" + ddl_Brand.SelectedValue;
                else
                    condition += " AND Classify =" + ddl_Classify.SelectedValue;
            }

            if ((int)ViewState["StandardPrice"] != 0)
                condition += " AND ID IN (SELECT Product FROM MCS_Pub.dbo.PDT_StandardPrice_Detail WHERE StandardPrice=" + ViewState["StandardPrice"].ToString() + ")";

            IList<PDT_Product> products = PDT_ProductBLL.GetModelList(condition);

            gv_List_FacProd.BindGrid<PDT_Product>(products);
        }

        cb_SelectAll.Checked = false;
    }

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

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List_FacProd.PageIndex = 0;
        BindGrid();
    }

    protected void cb_SelectAll_CheckedChanged(object sender, EventArgs e)
    {
        if (tr_Product.Visible)
        {
            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
                cb_check.Checked = cb_SelectAll.Checked;
            }
        }
        else
        {
            foreach (GridViewRow row in gv_List_FacProd.Rows)
            {
                CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
                cb_check.Checked = cb_SelectAll.Checked;
            }
        }
    }

    #region 移入经营产品
    protected void bt_In_Click(object sender, EventArgs e)
    {
        Save();

        CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        if (client == null) return;

        PDT_StandardPriceBLL standardprice = new PDT_StandardPriceBLL((int)ViewState["StandardPrice"]);

        foreach (GridViewRow row in gv_List_FacProd.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                PDT_ProductPriceBLL _bll;
                if ((int)ViewState["PriceID"] != 0)
                    _bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
                else
                    return;

                PDT_ProductPrice_Detail pd = new PDT_ProductPrice_Detail();
                pd.PriceID = (int)ViewState["PriceID"];
                pd.Product = int.Parse(gv_List_FacProd.DataKeys[row.RowIndex]["ID"].ToString());

                #region 将标准价表中的价格设置到价表中
                if ((int)ViewState["StandardPrice"] > 0)
                {
                    PDT_StandardPrice_Detail d = standardprice.Items.FirstOrDefault(p => p.Product == pd.Product);
                    if (d != null)
                    {
                        pd.FactoryPrice = d.FactoryPrice;

                        if (client.ClientType == 3)      //门店
                        {
                            pd.BuyingPrice = d.TradeInPrice;
                            pd.SalesPrice = d.StdPrice;
                        }
                        else if (client.ClientType == 2)
                        {
                            if (client["DIClassify"] == "1")    //一级经销商
                            {
                                pd.BuyingPrice = d.FactoryPrice;
                                pd.SalesPrice = d.TradeInPrice;
                            }
                            else if (client["DIClassify"] == "2")     //分销商
                            {
                                pd.BuyingPrice = d.TradeOutPrice;
                                pd.SalesPrice = d.TradeInPrice;
                            }
                        }
                    }
                }
                #endregion

                _bll.AddDetail(pd);
            }
        }
        Response.Redirect("PDT_ProductPriceDetail2.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }

    protected void bt_Out_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb_check = (CheckBox)row.FindControl("cb_Check");
            if (cb_check.Checked)
            {
                PDT_ProductPriceBLL _bll;
                if ((int)ViewState["PriceID"] != 0)
                    _bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
                else
                    return;
                _bll.DeleteDetail(int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString()));
            }
        }
        Response.Redirect("PDT_ProductPriceDetail2.aspx?PriceID=" + ViewState["PriceID"].ToString());
    }
    #endregion

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    private void Save()
    {
        PDT_ProductPriceBLL _bll;
        if ((int)ViewState["PriceID"] != 0)
            _bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
        else
            _bll = new PDT_ProductPriceBLL();

        _bll.Model.ApproveFlag = 2;
        _bll.Model.BeginDate = DateTime.Parse(this.tbx_begin.Text.Trim());
        _bll.Model.EndDate = ((DateTime.Parse(this.tbx_end.Text.Trim())).AddDays(1)).AddSeconds(-1);
        _bll.Model.Client = int.Parse(ViewState["ClientID"].ToString());

        if ((int)ViewState["PriceID"] != 0)
        {
            _bll.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            _bll.Update();

            #region 修改明细
            foreach (GridViewRow row in gv_List.Rows)
            {
                int detailid = int.Parse(gv_List.DataKeys[row.RowIndex]["ID"].ToString());
                PDT_ProductPrice_Detail item1 = _bll.GetDetailModel(detailid);
                item1.BuyingPrice = decimal.Parse(((TextBox)row.FindControl("lbl_BuyingPrice")).Text);
                item1.SalesPrice = decimal.Parse(((TextBox)row.FindControl("lbl_FactoryPrice")).Text);
                _bll.UpdateDetail(item1);
            }

            #endregion
        }
        else
        {
            _bll.Model.Client = (int)ViewState["ClientID"];
            _bll.Model.InsertStaff = int.Parse(Session["UserID"].ToString());
            _bll.Add();
            ViewState["PriceID"] = _bll.Model.ID;
            foreach (GridViewRow row in gv_List.Rows)
            {
                PDT_ProductPrice_Detail item2 = new PDT_ProductPrice_Detail();
                item2.PriceID = _bll.Model.ID;
                item2.Product = int.Parse(gv_List.DataKeys[row.RowIndex]["Product"].ToString());
                item2.BuyingPrice = decimal.Parse(gv_List.DataKeys[row.RowIndex]["BuyingPrice"].ToString());
                item2.SalesPrice = decimal.Parse(gv_List.DataKeys[row.RowIndex]["SalesPrice"].ToString());
                _bll.AddDetail(item2);
            }
        }
    }

    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Approve((int)Session["UserID"]);
        Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }
    protected void btn_UnApprove_Click(object sender, EventArgs e)
    {
        new PDT_ProductPriceBLL((int)ViewState["PriceID"]).UnApprove((int)Session["UserID"]);
        Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    public string GetERPCode(string ProductID)
    {
        return new PDT_ProductBLL(int.Parse(ProductID)).Model.Code;
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }

    protected void gv_List_FacProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List_FacProd.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            new PDT_ProductPriceBLL((int)ViewState["PriceID"]).Delete();
            Response.Redirect("PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }
    protected void bt_Stop_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["PriceID"] > 0)
        {
            PDT_ProductPriceBLL bll = new PDT_ProductPriceBLL((int)ViewState["PriceID"]);
            bll.Model.EndDate = DateTime.Today.AddMinutes(-1);
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();

            MessageBox.ShowAndRedirect(this, "价表停用成功!", "PDT_ProductPrice.aspx?ClientID=" + ViewState["ClientID"].ToString());
        }
    }
}
