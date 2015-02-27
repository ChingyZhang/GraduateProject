using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.CM;
using MCSFramework.Model.SVM;
using MCSControls.MCSWebControls;
using System.Collections.Generic;
using MCSFramework.Model.Pub;
using MCSFramework.Common;
using MCSFramework.BLL;

public partial class SubModule_SVM_SalesForcastDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ForcastID"] = Request.QueryString["ForcastID"] == null ? 0 : int.Parse(Request.QueryString["ForcastID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["ForcastID"] != 0)
            {
                BindData();
                btn_SalesForcast.Visible = false;
            }
            else
            {
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("SalesForcast.aspx");

                #region 新增销量预估
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;

                MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_SalesForcast_Client");
                if (select_Client != null)
                {
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = c.FullName;
                    select_Client.Enabled = false;
                }

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_SalesForcast_OrganizeCity");
                if (tr_OrganizeCity != null)
                {
                    tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
                    tr_OrganizeCity.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesForcast_AccountMonth");
                if (ddl_Month != null)
                {
                    ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-7).AddMonths(1)).ToString();
                    ddl_Month.Enabled = false;
                }

                bt_Del.Visible = false;
                bt_Save.Visible = false;
                bt_Approve.Visible = false;
                #endregion
            }

            #region 确定页面权限
            if ((int)ViewState["ClientID"] != 0)
            {
                CM_Client _r = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                ViewState["ClientType"] = _r.ClientType;
                if (_r.ClientType == 3)
                    Header.Attributes["WebPageSubCode"] += "ClientType=3";
                else if (_r.ClientType == 2)
                    Header.Attributes["WebPageSubCode"] += "ClientType=2&DIClassify=" + _r["DIClassify"];
            }
            #endregion
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Brand.DataSource = new PDT_BrandBLL()._GetModelList("IsOpponent='1'");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("所有品牌", "0"));
        ddl_Brand.SelectedValue = "0";
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
        SVM_SalesForcastBLL bll = new SVM_SalesForcastBLL((int)ViewState["ForcastID"]);

        ViewState["ClientID"] = bll.Model.Client;
        ViewState["Month"] = bll.Model.AccountMonth;

        UC_DetailView1.BindData(bll.Model);
        UC_DetailView1.SetControlsEnable(false);
        BindGrid();

        if (bll.Model.ApproveFlag == 1)
        {
            gv_List.SetControlsEnable(false);
            bt_Approve.Visible = false;
            bt_Save.Visible = false;
            bt_Del.Visible = false;
        }

    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        string condition = "SVM_SalesForcast_Detail.ForcastID=" + ViewState["ForcastID"].ToString();
        if (ddl_Classify.SelectedValue != "0")
        {
            condition += " AND PDT_Product.Classify=" + ddl_Classify.SelectedValue;
        }
        else if (ddl_Brand.SelectedValue != "0")
        {
            condition += "AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    #endregion


    protected void bt_Search_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] == 0)
        {
            MessageBox.Show(this, "请先点击“填报预估”按钮，再填写具体的产品！");
            return;
        }
        if (bt_Save.Visible) Save();
        BindGrid();
    }

    private void Save()
    {
        SVM_SalesForcastBLL bll = new SVM_SalesForcastBLL((int)ViewState["ForcastID"]);
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesForcast_AccountMonth");
        bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
        bll.Update();
        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex][0];
            SVM_SalesForcast_Detail item = bll.GetDetailModel(id);
            item.Quantity = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text) * new PDT_ProductBLL(item.Product).Model.ConvertFactor +
                int.Parse(((TextBox)gr.FindControl("tbx_Quantity2")).Text);
            item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;

            bll.UpdateDetail(item);
        }
    }

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Save();
        BindGrid();
    }
    #endregion

    protected void btn_SalesForcast_Click(object sender, EventArgs e)
    {
        #region 已有分配单展示,没有则生成
        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_SalesForcast_OrganizeCity");
        MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_SalesForcast_Client");
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesForcast_AccountMonth");

        if (tr_OrganizeCity.SelectValue == "0")
        {
            lb_Msg.Text = "必填先选择要填报的管理片区!";
            MessageBox.Show(this, "必填先选择要填报的管理片区!" + tr_OrganizeCity.SelectValue + "|" + select_Client.SelectValue);
            return;
        }

        int id = SVM_SalesForcastBLL.InitProductList(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_Month.SelectedValue), int.Parse(select_Client.SelectValue == "" ? "0" : select_Client.SelectValue), (int)Session["UserID"]);

        Response.Redirect("SalesForcastDetail.aspx?ForcastID=" + id.ToString());
        #endregion
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            Save();
            new SVM_SalesForcastBLL((int)ViewState["ForcastID"]).Approve((int)Session["UserID"]);
            Response.Redirect("SalesForcast.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            new SVM_SalesForcastBLL((int)ViewState["ForcastID"]).Delete();
            Response.Redirect("SalesForcast.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    public string GetSalesVolume(int Product)
    {
        PDT_Product p = new PDT_ProductBLL(Product).Model;
        IList<SVM_JXCSummary> jxclist = SVM_JXCSummaryBLL.GetModelList("AccountMonth=" + ((int)ViewState["Month"] - 1).ToString() + " AND Client=" + ViewState["ClientID"].ToString() + " AND Product=" + Product.ToString());
        if (jxclist.Count != 1) return "";

        int quantity = jxclist[0].PurchaseVolume;

        if (quantity == 0) return "0";
        string packing1 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.TrafficPackaging.ToString()].ToString();
        string packing2 = DictionaryBLL.GetDicCollections("PDT_Packaging")[p.Packaging.ToString()].ToString();

        return (quantity / p.ConvertFactor).ToString() + packing1 + " " + (quantity % p.ConvertFactor).ToString() + packing2;
    }

    protected void cbx_UpdateAccountMonth_CheckedChanged(object sender, EventArgs e)
    {
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesForcast_AccountMonth");
        ddl_Month.Enabled = cbx_UpdateAccountMonth.Checked;
    }
}

