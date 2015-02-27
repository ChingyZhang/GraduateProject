// ===================================================================
// 文件路径:SubModule/FNA/Budget/BudgetTransferApplyDetail.aspx.cs 
// 生成日期:2010/8/19 13:19:48 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using System.Collections.Specialized;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSControls.MCSWebControls;
public partial class SubModule_FNA_Budget_BudgetTransferApplyDetail : System.Web.UI.Page
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
                Label lb_AccountMonth = (Label)pl_detail.FindControl("FNA_BudgetTransferApply_ToAccountMonth");
                if (lb_AccountMonth != null) lb_AccountMonth.Text = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth()).Model.Name;

                tbl_BudgetInfo.Visible = true;
                bt_Submit.Visible = false;
            }
        }

        DropDownList ddl_FromFeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetTransferApply_FromFeeType");
        if (ddl_FromFeeType != null)
        {
            ddl_FromFeeType.AutoPostBack = true;
            ddl_FromFeeType.SelectedIndexChanged += new EventHandler(ddl_FromFeeType_SelectedIndexChanged);
        }

        DropDownList ddl_ToFeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetTransferApply_ToFeeType");
        if (ddl_ToFeeType != null)
        {
            ddl_ToFeeType.AutoPostBack = true;
            ddl_ToFeeType.SelectedIndexChanged += new EventHandler(ddl_ToFeeType_SelectedIndexChanged);
        }
    }

    void ddl_FromFeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int organizecity = 0, month = 0, feetype = 0;

        month = AC_AccountMonthBLL.GetCurrentMonth();

        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("FNA_BudgetTransferApply_ToOrganizeCity");   //暂不支持跨片区调拨费用申请
        if (tr_OrganizeCity != null) int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);

        DropDownList ddl_FromFeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetTransferApply_FromFeeType");
        if (ddl_FromFeeType != null) int.TryParse(ddl_FromFeeType.SelectedValue, out feetype);

        BindFromBudget(month, organizecity, feetype);
    }

    void ddl_ToFeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int organizecity = 0, month = 0, feetype = 0;

        month = AC_AccountMonthBLL.GetCurrentMonth();

        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("FNA_BudgetTransferApply_ToOrganizeCity");
        if (tr_OrganizeCity != null) int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);

        DropDownList ddl_FromFeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetTransferApply_ToFeeType");
        if (ddl_FromFeeType != null) int.TryParse(ddl_FromFeeType.SelectedValue, out feetype);

        BindToBudget(month, organizecity, feetype);
    }

    void BindFromBudget(int month, int organizecity, int feetype)
    {
        lb_FromBalance.Text = FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype).ToString("0.##");
    }

    void BindToBudget(int month, int organizecity, int feetype)
    {
        lb_ToBalance.Text = FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype).ToString("0.##");
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        FNA_BudgetTransferApply m = new FNA_BudgetTransferApplyBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            if (m.ApproveTask > 0 || m.ApproveFlag == 1 || m.InsertStaff != (int)Session["UserID"])
            {
                lb_ApproveAmount.Text = (m.TransferAmount - m.AdjustAmount).ToString("0.##");
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;

                //允许调整审批金额
                if (m.ApproveTask > 0 && m.ApproveFlag == 2 &&
                    Request.QueryString["Decision"] != null && Request.QueryString["Decision"] == "Y")
                {
                    TextBox tbx_AdjustAmount = (TextBox)pl_detail.FindControl("FNA_BudgetTransferApply_AdjustAmount");
                    tbx_AdjustAmount.Enabled = true;
                    bt_SaveAdjust.Visible = true;
                }
            }

            if (m.ApproveFlag == 2)
            {
                tbl_BudgetInfo.Visible = true;
                BindFromBudget(m.FromAccountMonth, m.FromOrganizeCity, m.FromFeeType);
                BindToBudget(m.ToAccountMonth, m.ToOrganizeCity, m.ToFeeType);
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        FNA_BudgetTransferApplyBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new FNA_BudgetTransferApplyBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new FNA_BudgetTransferApplyBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.ToOrganizeCity == 0)
        {
            MessageBox.Show(this, "必须正确选择管理片区!");
            return;
        }

        if (_bll.Model.ToFeeType == 0)
        {
            MessageBox.Show(this, "必须正确选择目的费用类型!");
            return;
        }

        if (_bll.Model.FromFeeType == 0)
        {
            MessageBox.Show(this, "必须正确选择源费用类型!");
            return;
        }

        if (_bll.Model.TransferAmount <= 0)
        {
            MessageBox.Show(this, "调拨金额必须大于0!");
            return;
        }
        #endregion

        _bll.Model.FromOrganizeCity = _bll.Model.ToOrganizeCity;

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "修改成功!", "BudgetTransferApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
        else
        {
            //新增
            _bll.Model.FromAccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
            _bll.Model.ToAccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "新增成功!", "BudgetTransferApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }

    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_OK_Click(null, null);

            FNA_BudgetTransferApplyBLL bll = new FNA_BudgetTransferApplyBLL((int)ViewState["ID"]);

            #region 判断源预算余额是否够调拨
            decimal balance= FNA_BudgetBLL.GetUsableAmount(bll.Model.FromAccountMonth,bll.Model.FromOrganizeCity,bll.Model.FromFeeType);
            if (balance < bll.Model.TransferAmount)
            {
                MessageBox.Show(this, "对不起，源预算余额不够调拨！源余额为"+balance.ToString("0.##"));
                return;
            }
            #endregion
            
            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.ToOrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.ToAccountMonth.ToString());
            dataobjects.Add("FromFeeType", bll.Model.FromFeeType.ToString());
            dataobjects.Add("ToFeeType", bll.Model.ToFeeType.ToString());
            dataobjects.Add("TransferAmount", bll.Model.TransferAmount.ToString("0.##"));

            #region 组合审批任务主题
            string title = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", bll.Model.ToOrganizeCity) + " 申请扩增预算额度,扩增额度:" + bll.Model.TransferAmount.ToString("0.##");
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("FNA_BudgetTransferApplyFlow", (int)Session["UserID"], title, "~/SubModule/FNA/Budget/BudgetTransferApplyDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID <= 0)
            {
                MessageBox.Show(this, "对不起，工作流发起失败，请与管理员联系！");
                return;
            }
            bll.Submit((int)Session["UserID"], TaskID);
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            #endregion

            MessageBox.ShowAndRedirect(this, "预算扩增提交成功！", Page.ResolveClientUrl("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString()));
        }
    }

    protected void bt_SaveAdjust_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            decimal adjustamount = 0;
            TextBox tbx_AdjustAmount = (TextBox)pl_detail.FindControl("FNA_BudgetTransferApply_AdjustAmount");
            if (tbx_AdjustAmount != null) decimal.TryParse(tbx_AdjustAmount.Text, out adjustamount);

            FNA_BudgetTransferApplyBLL _bll = new FNA_BudgetTransferApplyBLL((int)ViewState["ID"]);
            if (_bll.Model.TransferAmount < adjustamount)
            {
                MessageBox.Show(this, "扣减金额不能大于申请金额!");
                return;
            }

            _bll.Model.AdjustAmount = adjustamount;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();

            BindData();
            MessageBox.Show(this, "保存扣减金额成功!");
        }
    }
}