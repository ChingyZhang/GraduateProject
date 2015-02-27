using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.Model.SVM;
using MCSControls.MCSWebControls;
using System.Collections.Generic;
using MCSFramework.Model.Pub;
using MCSFramework.Common;


public partial class SubModule_SVM_SalesTargetDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["TargetID"] = Request.QueryString["TargetID"] == null ? 0 : int.Parse(Request.QueryString["TargetID"]);
            ViewState["ClientID"] = Request.QueryString["ClientID"] == null ? 0 : int.Parse(Request.QueryString["ClientID"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["TargetID"] != 0)
            {
                BindData();
                btn_SalesTarget.Visible = false;
            }
            else
            {
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("SalesTarget.aspx");

                #region 新增销量目标
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;

                MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_SalesTarget_Client");
                if (select_Client != null)
                {
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = c.FullName;
                    select_Client.Enabled = false;
                }

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_SalesTarget_OrganizeCity");
                if (tr_OrganizeCity != null)
                {
                    tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
                    tr_OrganizeCity.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesTarget_AccountMonth");
                if (ddl_Month != null) ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-7).AddMonths(1)).ToString();

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
        ddl_Brand.DataSource = new PDT_BrandBLL()._GetModelList("IsOpponent=1");
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
        SVM_SalesTargetBLL bll = new SVM_SalesTargetBLL((int)ViewState["TargetID"]);
        ViewState["ClientID"] = bll.Model.Client;

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
        string condition = "SVM_SalesTarget_Detail.TargetID=" + ViewState["TargetID"].ToString();
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
        if ((int)ViewState["TargetID"] == 0)
        {
            MessageBox.Show(this, "请先点击“填报目标”按钮，再填写具体的产品！");
            return;
        }
        if (bt_Save.Visible) Save();
        BindGrid();
    }

    private void Save()
    {
        SVM_SalesTargetBLL bll = new SVM_SalesTargetBLL((int)ViewState["TargetID"]);

        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex][0];
            SVM_SalesTarget_Detail item = bll.GetDetailModel(id);

            item.Quantity = int.Parse(((TextBox)gr.FindControl("tbx_Quantity1")).Text) * new PDT_ProductBLL(item.Product).Model.ConvertFactor;
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

    protected void btn_SalesTarget_Click(object sender, EventArgs e)
    {
        #region 已有分配单展示,没有则生成
        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_SalesTarget_OrganizeCity");
        MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_SalesTarget_Client");
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_SalesTarget_AccountMonth");

        if (tr_OrganizeCity.SelectValue == "0" || select_Client.SelectValue == "")
        {
            lb_Msg.Text = "必填先选择要填报的客户!";
            MessageBox.Show(this, "必填先选择要填报的客户!" + tr_OrganizeCity.SelectValue + "|" + select_Client.SelectValue);
            return;
        }
        int id = SVM_SalesTargetBLL.InitProductList(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_Month.SelectedValue), int.Parse(select_Client.SelectValue == "" ? "0" : select_Client.SelectValue), (int)Session["UserID"]);

        Response.Redirect("SalesTargetDetail.aspx?TargetID=" + id.ToString());
        #endregion
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["TargetID"] != 0)
        {
            Save();
            new SVM_SalesTargetBLL((int)ViewState["TargetID"]).Approve((int)Session["UserID"]);
            Response.Redirect("SalesTarget.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        if (ViewState["TargetID"] != null)
        {
            new SVM_SalesTargetBLL((int)ViewState["TargetID"]).Delete();
            Response.Redirect("SalesTarget.aspx");
        }
    }
}

