using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.FNA;

public partial class SubModule_FNA_Budget_Pop_BudgetAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取参数
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 1 : int.Parse(Request.QueryString["AccountMonth"]);

            if ((int)ViewState["OrganizeCity"] == 0 || (int)ViewState["AccountMonth"] == 0)
            {
                Response.Redirect("BudgetSource.aspx");
            }
            #endregion
            BindDropDown();
            BindGrid();

            if (ConfigHelper.GetConfigString("OutSumBudgetFeeType") != "")
            {
                string[] outbudgets = ConfigHelper.GetConfigString("OutSumBudgetFeeType").Split(',');
                for (int i = 0; i < outbudgets.Length; i++)
                {
                    lb_OutSumBudgetFeeType.Text += DictionaryBLL.GetDicCollections("FNA_FeeType")[outbudgets[i].ToString()] + ",";
                }
            }
        }
    }

    #region 绑定DropDownList
    private void BindDropDown()
    {

    }
    #endregion

    #region 绑定GridView
    private void BindGrid()
    {
        //权限判断
        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1511, "ApproveBudget"))
        {
            btnApprove.Visible = false;
        }

        IList<FNA_BudgetSource> sourcelists = FNA_BudgetSourceBLL.GetModelList("OrganizeCity=" + ViewState["OrganizeCity"].ToString()
            + " and AccountMonth=" + ViewState["AccountMonth"].ToString() + " AND ApproveFlag=1");
        if (sourcelists.Count == 0)
        {
            MessageBox.ShowAndRedirect(this, "对不起当前作业区可用费用计算表还未填写或未审核！", "BudgetSource.aspx");
            return;
        }

        IList<FNA_Budget> budgets = FNA_BudgetBLL.GetModelList("OrganizeCity=" + ViewState["OrganizeCity"].ToString()
           + " AND AccountMonth=" + ViewState["AccountMonth"].ToString() + " AND BudgetType=1");

        lb_PlanVolume.Text = sourcelists[0].PlanVolume.ToString("0.##");
        lb_SumBudget.Text = (sourcelists[0].BaseBudget + sourcelists[0].OverFullBudget - decimal.Parse(sourcelists[0]["DepartmentBudget"])).ToString("0.##");
        lb_RetentionBudget.Text = sourcelists[0].RetentionBudget.ToString("0.##");
        lb_DepartmentBudget.Text = sourcelists[0]["DepartmentBudget"];

        string[] outbudgets = ConfigHelper.GetConfigString("OutSumBudgetFeeType").Split(',');

        lb_AssignBalance.Text = (sourcelists[0].BaseBudget + sourcelists[0].OverFullBudget - decimal.Parse(sourcelists[0]["DepartmentBudget"]) - budgets.Sum(p => outbudgets.Contains(p.FeeType.ToString()) ? 0 : p.BudgetAmount)).ToString("0.##");

        #region 初始化预算分配
        Dictionary<string, Dictionary_Data> dic = DictionaryBLL.GetDicCollections("FNA_FeeType", true);
        foreach (Dictionary_Data item in dic.Values)
        {
            if (budgets.FirstOrDefault(p => p.FeeType == int.Parse(item.Code)) == null)
            {
                FNA_BudgetBLL bll = new FNA_BudgetBLL();
                bll.Model.AccountMonth = (int)ViewState["AccountMonth"];
                bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
                bll.Model.BudgetType = 1;
                bll.Model.FeeType = int.Parse(item.Code);

                if (bll.Model.FeeType == 20)
                    bll.Model.BudgetAmount = decimal.Parse(lb_RetentionBudget.Text);
                else
                    bll.Model.BudgetAmount = 0;

                bll.Model.ApproveFlag = 2;
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Add();
            }
        }
        #endregion

        budgets = FNA_BudgetBLL.GetModelList("OrganizeCity=" + ViewState["OrganizeCity"].ToString()
           + " and AccountMonth=" + ViewState["AccountMonth"].ToString() + " AND BudgetType=1").OrderBy(p => p.FeeType).ToList();

        gv_List.BindGrid(budgets);

        if (budgets.FirstOrDefault(p => p.ApproveFlag == 2) == null)
        {
            btnSave.Visible = false;
            btnApprove.Visible = false;
            cbx_CheckAll.Visible = false;
        }
    }

    protected decimal GetBudgetRate(decimal budget)
    {
        if (decimal.Parse(lb_PlanVolume.Text) == 0) return 0;
        return budget / decimal.Parse(lb_PlanVolume.Text);
    }
    #endregion

    private bool Save()
    {
        #region 判断是否超过可用预算合计值
        TextBox tbx = null;
        decimal sumBuget = 0, fee = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            int feetype = (int)gv_List.DataKeys[row.RowIndex]["FeeType"];

            tbx = (TextBox)row.FindControl("tbx_BudgetAmount");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0)
            {
                if (feetype == 20)
                {
                    if (fee > decimal.Parse(lb_RetentionBudget.Text))
                    {
                        tbx.Focus();
                        MessageBox.Show(this, "自留费用不超过可用自留费用!");
                        return false;
                    }
                }
                else
                {
                    string[] outbudgets = ConfigHelper.GetConfigString("OutSumBudgetFeeType").Split(',');
                    if (!outbudgets.Contains(feetype.ToString()))
                    {
                        sumBuget += fee;
                    }
                }
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "费用预算额度格式必须为数字值，且不能小于0!");
                return false;
            }
        }

        if (sumBuget > decimal.Parse(lb_SumBudget.Text))
        {
            tbx.Focus();
            MessageBox.Show(this, "分配的总费用不能大于总可用预算合计值！当前合计值：" + sumBuget.ToString("0.##") + ",可用费用值:" + lb_SumBudget.Text);
            return false;
        }
        #endregion

        foreach (GridViewRow row in gv_List.Rows)
        {
            FNA_BudgetBLL bll = new FNA_BudgetBLL((int)gv_List.DataKeys[row.RowIndex]["ID"]);
            if (bll.Model.ApproveFlag == 2)
            {
                bll.Model.BudgetAmount = decimal.Parse(((TextBox)row.FindControl("tbx_BudgetAmount")).Text);
                bll.Model.Remark = ((TextBox)row.FindControl("tbx_Remark")).Text;
                bll.Model.UpdateStaff = (int)Session["UserID"];
                bll.Update();
            }
        }

        return true;
    }
    #region 保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            BindGrid();
            MessageBox.Show(this, "保存成功!");
        }
    }
    #endregion

    #region 审核
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cbx = (CheckBox)row.FindControl("cbx");
                if (cbx.Checked)
                {
                    new FNA_BudgetBLL((int)gv_List.DataKeys[row.RowIndex]["ID"]).Approve((int)Session["UserID"]);
                }
            }
            BindGrid();

            MessageBox.Show(this, "审核成功!");
        }
    }
    #endregion

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            cbx.Checked = cbx_CheckAll.Checked;
        }
    }
}

