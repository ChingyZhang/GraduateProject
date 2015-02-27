using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;

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
                Session["ClientID"] = ViewState["ClientID"];
                btn_SalesForcast.Visible = false;
            }
            else
            {
                if ((int)ViewState["ClientID"] == 0) Response.Redirect("ClassifyForcast.aspx");

                #region 新增销量预估
                CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                ViewState["ClientType"] = c.ClientType;

                MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_ClassifyForcast_Client");
                if (select_Client != null)
                {
                    select_Client.SelectValue = ViewState["ClientID"].ToString();
                    select_Client.SelectText = c.FullName;
                    select_Client.Enabled = false;
                }

                MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_ClassifyForcast_OrganizeCity");
                if (tr_OrganizeCity != null)
                {
                    tr_OrganizeCity.SelectValue = c.OrganizeCity.ToString();
                    tr_OrganizeCity.Enabled = false;
                }

                DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_ClassifyForcast_AccountMonth");
                if (ddl_Month != null)
                {
                    ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-15).AddMonths(1)).ToString();
                    ddl_Month.Enabled = false;
                }

                bt_Del.Visible = false;
                bt_Save.Visible = false;
                bt_Approve.Visible = false;
                bt_DirectApprove.Visible = false;
                bt_Refresh.Visible = false;
                #endregion

                if (Request.QueryString["AccountMonth"] != null &&
                    ddl_Month.Items.FindByValue(Request.QueryString["AccountMonth"]) != null)
                {
                    ddl_Month.SelectedValue = Request.QueryString["AccountMonth"];
                    btn_SalesForcast_Click(null, null);
                }
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
        SVM_ClassifyForcastBLL bll = new SVM_ClassifyForcastBLL((int)ViewState["ForcastID"]);

        ViewState["ClientID"] = bll.Model.Client;
        ViewState["Month"] = bll.Model.AccountMonth;
        CM_Client c = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
        ViewState["ClientType"] = c.ClientType;

        UC_DetailView1.BindData(bll.Model);
        UC_DetailView1.SetControlsEnable(false);
        BindGrid();

        if (bll.Model.ApproveFlag == 1 || bll.Model.TaskID != 0)
        {
            gv_List.SetControlsEnable(false);
            bt_Approve.Visible = false;
            bt_Save.Visible = false;
            bt_Del.Visible = false;
            bt_DirectApprove.Visible = false;
            cbx_UpdateAccountMonth.Visible = false;
            bt_Refresh.Visible = false;
        }

    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        string condition = "SVM_ClassifyForcast_Detail.ForcastID=" + ViewState["ForcastID"].ToString();
        if (ddl_Classify.SelectedValue != "0")
        {
            condition += " AND PDT_Classify.ID=" + ddl_Classify.SelectedValue;
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();

        if (!bt_Save.Visible) gv_List.SetControlsEnable(false);
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
        SVM_ClassifyForcastBLL bll = new SVM_ClassifyForcastBLL((int)ViewState["ForcastID"]);
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_ClassifyForcast_AccountMonth");
        bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
        bll.Update();
        foreach (GridViewRow gr in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[gr.RowIndex][0];
            SVM_ClassifyForcast_Detail item = bll.GetDetailModel(id);

            TextBox tbx_Amount = (TextBox)gr.FindControl("tbx_Amount");
            if (tbx_Amount != null && tbx_Amount.Enabled)
                item.Amount = decimal.Parse(tbx_Amount.Text);
            else
            {
                Label lbl_Sales = (Label)gr.FindControl("lbl_Sales");
                TextBox tbx_Rate = (TextBox)gr.FindControl("tbx_Rate");
                if (lbl_Sales != null && tbx_Rate != null)
                    item.Rate = decimal.Parse(tbx_Rate.Text);
                item.Amount = decimal.Parse(lbl_Sales.Text) * decimal.Parse(tbx_Rate.Text) / 100;
            }
            item.Amount = Math.Round(item.Amount, 1);
            item.Remark = ((TextBox)gr.FindControl("tbx_Remark")).Text;

            bll.UpdateDetail(item);
        }
        MessageBox.Show(this, "保存成功！");
        BindGrid();
    }

    #region 保存按钮事件
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Save();
        MessageBox.Show(this, "保存成功！");
        BindGrid();
    }
    #endregion

    protected void btn_SalesForcast_Click(object sender, EventArgs e)
    {
        #region 已有分配单展示,没有则生成
        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)UC_DetailView1.FindControl("SVM_ClassifyForcast_OrganizeCity");
        MCSSelectControl select_Client = (MCSSelectControl)UC_DetailView1.FindControl("SVM_ClassifyForcast_Client");
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_ClassifyForcast_AccountMonth");


        int id = SVM_ClassifyForcastBLL.Init(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_Month.SelectedValue), int.Parse(select_Client.SelectValue == "" ? "0" : select_Client.SelectValue), (int)Session["UserID"]);

        Response.Redirect("ClassifyForcastDetail.aspx?ForcastID=" + id.ToString());
        #endregion
    }

    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            string wftitle = "经销商销量预估流程申请ID：";
            Save();
            if ((int)ViewState["ClientType"] == 3)
            {
                wftitle = "零售商销量预估流程申请ID：";
            }

            Org_StaffBLL bll = new Org_StaffBLL((int)Session["UserID"]);
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ForcastID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("ApplyFee", SVM_ClassifyForcastBLL.GetForcastSumPrice((int)ViewState["ForcastID"]).ToString());
            int TaskID = EWF_TaskBLL.NewTask("SVM_ClassifyForcast_Approve", (int)Session["UserID"], wftitle + ViewState["ForcastID"].ToString(), "~/SubModule/SVM/ClassifyForcastDetail.aspx?ClientID=" + ViewState["ClientID"].ToString() + "&ForcastID=" + ViewState["ForcastID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                SVM_ClassifyForcastBLL.Submit((int)ViewState["ForcastID"], TaskID);
                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            bt_Approve.Enabled = false;
            Response.Redirect("~/SubModule/SVM/ClassifyForcast.aspx");
        }
    }

    protected void bt_Del_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            new SVM_ClassifyForcastBLL((int)ViewState["ForcastID"]).Delete();
            Response.Redirect("ClassifyForcast.aspx?ClientType=" + ViewState["ClientType"].ToString());
        }
    }


    protected void cbx_UpdateAccountMonth_CheckedChanged(object sender, EventArgs e)
    {
        DropDownList ddl_Month = (DropDownList)UC_DetailView1.FindControl("SVM_ClassifyForcast_AccountMonth");
        ddl_Month.Enabled = cbx_UpdateAccountMonth.Checked;
    }
    protected void bt_DirectApprove_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            Save();
            new SVM_ClassifyForcastBLL((int)ViewState["ForcastID"]).Approve((int)Session["UserID"]);
            Response.Redirect("ClassifyForcast.aspx?ClientType=" + ViewState["ClientType"].ToString());
            bt_DirectApprove.Enabled = false;
        }
    }

    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ForcastID"] != 0)
        {
            SVM_ClassifyForcastBLL.RefreshAvgSales((int)ViewState["ForcastID"]);
            Response.Redirect("ClassifyForcastDetail.aspx?ForcastID=" + ViewState["ForcastID"].ToString());
        }
    }
}

