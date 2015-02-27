// ===================================================================
// 文件路径:SubModule/FNA/Budget/BudgetExtraApplyDetail.aspx.cs 
// 生成日期:2010/8/19 13:17:22 
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
using MCSFramework.BLL.Pub;
using System.Collections.Specialized;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSControls.MCSWebControls;
using MCSFramework.Model;
public partial class SubModule_FNA_Budget_BudgetExtraApplyDetail : System.Web.UI.Page
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
                Label lb_AccountMonth = (Label)pl_detail.FindControl("FNA_BudgetExtraApply_AccountMonth");
                if (lb_AccountMonth != null) lb_AccountMonth.Text = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetCurrentMonth()).Model.Name;

                tbl_BudgetInfo.Visible = true;
                bt_Submit.Visible = false;
            }
        }

        DropDownList ddl_FeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_FeeType");
        if (ddl_FeeType != null)
        {
            ddl_FeeType.AutoPostBack = true;
            ddl_FeeType.SelectedIndexChanged += new EventHandler(ddl_FeeType_SelectedIndexChanged);
        }
        //费用支持部门树控件(引用Org_Staff的OrganizeCity字段)
        DropDownList ddl_SupportOrganizeCity = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_SupportOrganizeCity");
        ddl_SupportOrganizeCity.Enabled = false;
        DropDownList ddl_ExtraType = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_ExtraType");
        ddl_ExtraType.AutoPostBack = true;
        ddl_ExtraType.SelectedIndexChanged += new EventHandler(ddl_ExtraType_SelectedIndexChanged);


    }
    void ddl_ExtraType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_SupportOrganizeCity = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_SupportOrganizeCity");
        DropDownList ddl_ExtraType = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_ExtraType");
        if (ddl_ExtraType.SelectedValue == "3")
        {
            ddl_SupportOrganizeCity.Enabled = true;
        }
    }
    void ddl_FeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int organizecity = 0, month = 0, feetype = 0;

        month = AC_AccountMonthBLL.GetCurrentMonth();

        MCSTreeControl tr_OrganizeCity = (MCSTreeControl)pl_detail.FindControl("FNA_BudgetExtraApply_OrganizeCity");
        if (tr_OrganizeCity != null) int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);

        DropDownList ddl_FeeType = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_FeeType");
        if (ddl_FeeType != null) int.TryParse(ddl_FeeType.SelectedValue, out feetype);

        BindBudget(month, organizecity, feetype);
    }

    void BindBudget(int month, int organizecity, int feetype)
    {
        lb_Balance.Text = FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype).ToString("0.##");
        int city = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", organizecity, 1);
        lb_DepartmentBalance.Text = FNA_BudgetBLL.GetUsableAmount(month, city, feetype, false).ToString();

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        FNA_BudgetExtraApply m = new FNA_BudgetExtraApplyBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

            if (m.ApproveTask > 0 || m.ApproveFlag == 1 || m.InsertStaff != (int)Session["UserID"])
            {
                lb_ApproveAmount.Text = (m.ExtraAmount - m.AdjustAmount).ToString("0.##");
                pl_detail.SetControlsEnable(false);
                bt_OK.Visible = false;
                bt_Submit.Visible = false;

                //允许调整审批金额
                if (m.ApproveTask > 0 && m.ApproveFlag == 2 &&
                    Request.QueryString["Decision"] != null && Request.QueryString["Decision"] == "Y")
                {
                    TextBox tbx_AdjustAmount = (TextBox)pl_detail.FindControl("FNA_BudgetExtraApply_AdjustAmount");
                    tbx_AdjustAmount.Enabled = true;
                    bt_SaveAdjust.Visible = true;
                }
            }

            if (!string.IsNullOrEmpty(m.ApproveTask.ToString()) && m.ApproveTask != 0)
            {
                bt_Submit.Visible = false;
            }

            if (m.ApproveFlag == 2)
            {
                tbl_BudgetInfo.Visible = true;
                BindBudget(m.AccountMonth, m.OrganizeCity, m.FeeType);
            }

            #region 获取当月该费用类型累计已批复扩增金额
            //lb_SumExtraInfo.Text = new Addr_OrganizeCityBLL(m.OrganizeCity).Model.Name +
            //    ":<b><font color=red>" + FNA_BudgetExtraApplyBLL.GetExtraAmount(m.AccountMonth, m.OrganizeCity, m.FeeType, false).ToString("0.##") + "</b></font>  ";

            //int staffcity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
            //if (staffcity == 0) staffcity = 1;
            //if (staffcity != 1)
            //{
            //    staffcity = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", m.OrganizeCity, new Addr_OrganizeCityBLL(staffcity).Model.Level);
            //}

            //lb_SumExtraInfo.Text += new Addr_OrganizeCityBLL(staffcity).Model.Name +
            //    ":<b><font color=red>" + FNA_BudgetExtraApplyBLL.GetExtraAmount(m.AccountMonth, staffcity, m.FeeType, true).ToString("0.##") + "</b></font>";
            #endregion

            #region 显示各扩增类型已批复扩增金额
            DropDownList ddl_ExtraType = (DropDownList)pl_detail.FindControl("FNA_BudgetExtraApply_ExtraType");
            foreach (ListItem item in ddl_ExtraType.Items)
            {
                if (item .Value!="0")
                lb_SumExtraInfo1.Text += item.Text + "总计:<b><font color=red>" + FNA_BudgetExtraApplyBLL.GetModelList("").Where(p => p.ApproveFlag == 1 && p.AccountMonth == m.AccountMonth && p.OrganizeCity == m.OrganizeCity && p.FeeType == m.FeeType && p["ExtraType"] == item.Value).Select(p => p.ExtraAmount - p.AdjustAmount).Sum().ToString() + "</b></font>  ";
            }
            int staffcity = new Org_StaffBLL((int)Session["UserID"]).Model.OrganizeCity;
            if (staffcity == 0) staffcity = 1;
            if (staffcity != 1)
            staffcity = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", m.OrganizeCity, new Addr_OrganizeCityBLL(staffcity).Model.Level);
            foreach (ListItem item in ddl_ExtraType.Items)
            {
                if (item.Value != "0")
                lb_SumExtraInfo2.Text += item.Text + "总计:<b><font color=red>" + FNA_BudgetExtraApplyBLL.GetModelList(" MCS_SYS.dbo.UF_IsChildOrganizeCity(" + staffcity.ToString() + ",OrganizeCity)=0").Where(p => p.ApproveFlag == 1 && p.AccountMonth == m.AccountMonth && p.OrganizeCity == m.OrganizeCity && p.FeeType == m.FeeType && p["ExtraType"] == item.Value).Select(p => p.ExtraAmount - p.AdjustAmount).Sum().ToString() + "</b></font>  ";
            }
            #endregion
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        FNA_BudgetExtraApplyBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new FNA_BudgetExtraApplyBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new FNA_BudgetExtraApplyBLL();
            _bll.Model.AccountMonth = AC_AccountMonthBLL.GetCurrentMonth();
        }
        pl_detail.GetData(_bll.Model);

        //_bll.Model["SheetCode"] = FNA_BudgetExtraApplyBLL.GenerateSheetCode(_bll.Model.OrganizeCity);在存储过程中实现
        #region 判断必填项
        if (_bll.Model.OrganizeCity == 0)
        {
            MessageBox.Show(this, "必须正确选择管理片区!");
            return;
        }

        if (_bll.Model.FeeType == 0)
        {
            MessageBox.Show(this, "必须正确选择费用类型!");
            return;
        }
        if (_bll.Model["ExtraType"] == "0")
        {
            MessageBox.Show(this, "必须正确选择扩增费用类别!");
            return;
        }
        #endregion

        #region 判断上级直至部经理手中有无预算
        decimal sumbudget = 0;
        Addr_OrganizeCityBLL city = new Addr_OrganizeCityBLL(_bll.Model.OrganizeCity);
        DataTable dt = city.GetFullPath();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Addr_OrganizeCity c = new Addr_OrganizeCityBLL((int)dt.Rows[i]["ID"]).Model;
            if (c == null || c.ID == 1 || c.ID == _bll.Model.OrganizeCity || c.Level == 1) continue;       //忽略总部

            sumbudget += FNA_BudgetBLL.GetUsableAmount(_bll.Model.AccountMonth, c.ID, _bll.Model.FeeType);
        }

        if (sumbudget > _bll.Model.ExtraAmount)
        {
            MessageBox.Show(this, "对不起，您的上级领导预算余额大于您要扩增的金额，请与领导沟通申请要求分配预算给您，而无需提交扩增预算流程！");
            return;
        }
        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "修改成功!", "BudgetExtraApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
        else
        {
            //新增

            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            ViewState["ID"] = _bll.Add();
            bt_Submit.Visible = true;
            if ((int)ViewState["ID"] > 0)
            {
                if (sender != null) MessageBox.ShowAndRedirect(this, "新增成功!", "BudgetExtraApplyDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }

    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            bt_OK_Click(null, null);

            FNA_BudgetExtraApplyBLL bll = new FNA_BudgetExtraApplyBLL((int)ViewState["ID"]);

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("ExtraAmount", bll.Model.ExtraAmount.ToString("0.##"));
            dataobjects.Add("FeeType", bll.Model.FeeType.ToString());
            dataobjects.Add("ExtraType", bll.Model["ExtraType"]);
            dataobjects.Add("SupportOrganizeCity", bll.Model["SupportOrganizeCity"]);

            #region 组合审批任务主题
            string title = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", bll.Model.OrganizeCity) + " 申请扩增预算额度,申请扩增额度:" + bll.Model.ExtraAmount.ToString("0.##");
            #endregion

            int TaskID = EWF_TaskBLL.NewTask("FNA_BudgetExtraApplyFlow", (int)Session["UserID"], title, "~/SubModule/FNA/Budget/BudgetExtraApplyDetail.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
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
            TextBox tbx_AdjustAmount = (TextBox)pl_detail.FindControl("FNA_BudgetExtraApply_AdjustAmount");
            if (tbx_AdjustAmount != null) decimal.TryParse(tbx_AdjustAmount.Text, out adjustamount);

            FNA_BudgetExtraApplyBLL _bll = new FNA_BudgetExtraApplyBLL((int)ViewState["ID"]);
            if (_bll.Model.ExtraAmount < adjustamount)
            {
                MessageBox.Show(this, "扣减金额不能大于申请金额!");
                return;
            }
            decimal OldAdjustCost = _bll.Model.AdjustAmount;
            _bll.Model.AdjustAmount = adjustamount;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
            FNA_BudgetExtraApplyBLL.UpdateAdjustRecord(_bll.Model.ID, (int)Session["UserID"], _bll.Model.FeeType, OldAdjustCost.ToString("0.##"), _bll.Model.AdjustAmount.ToString("0.##"), _bll.Model["AdjustReason"]);
            BindData();
            MessageBox.Show(this, "保存扣减金额成功!");
        }
    }
}