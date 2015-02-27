using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_FNA_Budget_BudgetSource : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }

        #region 注册脚本
        //注册修改客户状态按钮script
        string script = "function OpenBudgetAssign(organizecity,month){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n statuschange = window.showModalDialog('" + Page.ResolveClientUrl("Pop_BudgetAssign.aspx") +
            "?OrganizeCity='+organizecity+'&AccountMonth='+month+'&tempid='+tempid+'&ViewFramework=false&CloseMode=close', window, 'dialogWidth:1000px;DialogHeight=530px;status:yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenBudgetAssign", script, true);
        #endregion
    }

    #region 绑定DropDownList
    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<DateAdd(month,1,GETDATE()) AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today).ToString();
    }
    #endregion

    #region 绑定GridView
    public void BindGrid()
    {
        string condition = " AccountMonth = " + ddl_AccountMonth.SelectedValue;
        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            if (new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue)).Model.Level == 1)
                condition += " AND OrganizeCity IN (SELECT ID FROM MCS_SYS.dbo.Addr_OrganizeCity WHERE Level=2 AND SuperID=" + tr_OrganizeCity.SelectValue + ")";
            else
                condition += " AND OrganizeCity = " + tr_OrganizeCity.SelectValue;
        }
        #endregion
        gv_List.BindGrid(FNA_BudgetSourceBLL.GetModelList(condition));
        foreach (GridViewRow row in gv_List.Rows)
        {
            TextBox tbx = (TextBox)row.FindControl("tbDepartmentBudget");
            if (tbx != null)
            {
                FNA_BudgetSourceBLL bll = new FNA_BudgetSourceBLL((int)gv_List.DataKeys[row.RowIndex].Value);
                if (!string.IsNullOrEmpty(bll.Model["DepartmentBudget"]))
                {
                    tbx.Text = bll.Model["DepartmentBudget"];
                    Label lab = (Label)row.FindControl("LabTotalBudget");
                    decimal fee = decimal.Parse(gv_List.DataKeys[row.RowIndex]["BaseBudget"].ToString()) + decimal.Parse(gv_List.DataKeys[row.RowIndex]["OverFullBudget"].ToString()) - decimal.Parse(bll.Model["DepartmentBudget"].ToString());
                    lab.Text = fee.ToString();
                }
            }
        }
    }
    #endregion

    #region 查找
    protected void btnFind_Click(object sender, EventArgs e)
    {
        Init();
        gv_List.PageIndex = 0;
        BindGrid();
    }
    #endregion

    #region 初始化
    private void Init()
    {
        IList<Addr_OrganizeCity> citys = Addr_OrganizeCityBLL.GetModelList
            ("Level=2 AND ID NOT IN (SELECT OrganizeCity FROM MCS_FNA.dbo.FNA_BudgetSource WHERE AccountMonth=" +
            ddl_AccountMonth.SelectedValue + ") ORDER BY SuperID,Code");
        foreach (Addr_OrganizeCity city in citys)
        {
            FNA_BudgetSourceBLL bll = new FNA_BudgetSourceBLL();
            bll.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
            bll.Model.OrganizeCity = city.ID;
            bll.Model.ApproveFlag = 2;
            bll.Model.InsertStaff = (int)Session["UserID"];
            bll.Model["DepartmentBudget"] = "0";
            bll.Add();
        }
    }

    #endregion

    #region 保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            #region 保存作业区可用费用
            FNA_BudgetSourceBLL bll = new FNA_BudgetSourceBLL((int)gv_List.DataKeys[row.RowIndex].Value);
            if (bll.Model.ApproveFlag == 1) continue;
            TextBox tbx = null;
            decimal fee = 0;
            tbx = (TextBox)row.FindControl("tbBaseVolume");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0)
            {
                bll.Model.BaseVolume = fee;
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "基础销量必须不能小于0");
                return;
            }
            tbx = (TextBox)row.FindControl("tbPlanVolume");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0)
            {
                bll.Model.PlanVolume = fee;
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "计划销量不能小于0");
                return;
            }
            tbx = (TextBox)row.FindControl("tbBaseBudget");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0)
            {
                bll.Model.BaseBudget = fee;
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "费用预算额度不能小于0");
                return;
            }
            tbx = (TextBox)row.FindControl("tbRetentionBudget");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0)
            {
                bll.Model.RetentionBudget = fee;
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "自留费用不能小于0");
                return;
            }
            bll.Model.UpdateStaff = (int)Session["UserID"];
            bll.Update();
            tbx = (TextBox)row.FindControl("tbDepartmentBudget");
            if (tbx != null && decimal.TryParse(tbx.Text, out fee) && fee >= 0 && fee <= (bll.Model.BaseBudget + bll.Model.OverFullBudget))
            {
                bll.Model["DepartmentBudget"] = fee.ToString();
                bll.Update();
            }
            else
            {
                tbx.Focus();
                MessageBox.Show(this, "市场部预算额度不能小于0");
                return;
            }
            #endregion
        }
        BindGrid();
    }

    #endregion

    #region 审核
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("CheckBox1");
            if (cb.Checked)
            {
                FNA_BudgetSourceBLL bll = new FNA_BudgetSourceBLL((int)gv_List.DataKeys[row.RowIndex].Value);
                bll.Model.ApproveFlag = 1;
                bll.Update();
                FNA_BudgetBLL budget = new FNA_BudgetBLL();
                budget.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
                budget.Model.ApproveFlag = 1;
                budget.Model.OrganizeCity = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model.SuperID;
                budget.Model.BudgetAmount = decimal.Parse(bll.Model["DepartmentBudget"]);
                budget.Model.FeeType = 0;
                budget.Model.BudgetType = 1;
                budget.Model.InsertStaff = (int)Session["UserID"];
                budget.Model.InsertTime = DateTime.Now;
                budget.Add();
            }
        }
        BindGrid();
    }
    #endregion

    protected decimal GetAssignedBudget(int OrganizeCity, int AccountMonth)
    {
        if (ConfigHelper.GetConfigString("OutSumBudgetFeeType") != "")
            return FNA_BudgetBLL.GetModelList("OrganizeCity=" + OrganizeCity.ToString() + " AND AccountMonth=" + AccountMonth.ToString() +
                " AND FeeType NOT IN (" + ConfigHelper.GetConfigString("OutSumBudgetFeeType") + ") ").Sum(p => p.BudgetAmount);
        else
            return FNA_BudgetBLL.GetModelList("OrganizeCity=" + OrganizeCity.ToString() + " AND AccountMonth=" + AccountMonth.ToString()).Sum(p => p.BudgetAmount);
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        BindGrid();
    }
}
