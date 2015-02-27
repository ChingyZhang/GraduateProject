using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.Pub;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.Model;

public partial class SubModule_FNA_StaffSalary_StaffSalaryDataObjectBetaList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropdown();
        }
        MCSTabControl1.Items[1].Enable = ViewState["Item1Enable"] == null ? true : (bool)ViewState["Item1Enable"];
    }
    private void BindDropdown()
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


        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));

    }
    private void BindGrid()
    {
        AC_AccountMonth month = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model;
        int monthdays = month.EndDate.Subtract(month.BeginDate).Days + 1;

        ViewState["monthdays"] = monthdays;
        string condition = "FNA_StaffSalaryDataObjectBeta.AccountMonth=" + ddl_AccountMonth.SelectedValue;
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND  FNA_StaffSalaryDataObjectBeta.OrganizeCity in(" + orgcitys + ") ";
        }
        if (MCSTabControl1.SelectedIndex == 0)
        {
            condition += " AND  FNA_StaffSalaryDataObjectBeta.PositionType=1";
        }
        else
        {
            condition += " AND  FNA_StaffSalaryDataObjectBeta.PositionType!=1";
        }
        if (select_Staff.SelectValue != "")
        {
            condition += " AND  MCS_SYS.dbo.Org_Staff.ID=" + select_Staff.SelectValue;
        }
        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " AND  FNA_StaffSalaryDataObjectBeta.ApproveFlag=" + ddl_ApproveFlag.SelectedValue;
        }
        gv_List.ConditionString = condition + " Order By FNA_StaffSalaryDataObjectBeta_OrganizeCity1,FNA_StaffSalaryDataObjectBeta_OrganizeCity3,FNA_StaffSalaryDataObjectBeta_OrganizeCity4,Org_Staff_RealName";
        gv_List.BindGrid();
        if (MCSTabControl1.SelectedIndex == 0)
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;
        else
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = gv_List.Columns[gv_List.Columns.Count - 3].Visible = true;
        chk_Header.Checked = false;
        ddl_AccountMonth_SelectedIndexChanged(null, null);
    }

    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        int month = 0, staff = 0, city = 0;
        int.TryParse(ddl_AccountMonth.SelectedValue, out month);
        int.TryParse(select_Staff.SelectValue, out staff);
        int.TryParse(tr_OrganizeCity.SelectValue, out city); ;
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Addr_OrganizeCity mcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue)).Model;
        if (MCSTabControl1.SelectedTabItem.Value == "1" && mcity != null && mcity.Level != ConfigHelper.GetConfigInt("OrganizePartCity-CityLevel"))
        {
            MessageBox.Show(this, "请选择营业部再调整！");
            return;
        }
        if (MCSTabControl1.SelectedTabItem.Value == "2" && mcity != null && mcity.Level != ConfigHelper.GetConfigInt("OrganizeCity-CityLevel"))
        {
            MessageBox.Show(this, "请选择办事处再调整！");
            return;
        }
        FNA_StaffSalaryDataObjectBetaBLL bll = new FNA_StaffSalaryDataObjectBetaBLL();
        if (!CheckSalesTargetAdujst())
        {
            return;
        }
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["FNA_StaffSalaryDataObjectBeta_ID"];
            TextBox txt_SalesTargetAdjust = gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujst") == null ? null : (TextBox)gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujst");
            TextBox txt_SalesTargetAdjustRate = gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujstRate") == null ? null : (TextBox)gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujstRate");
            decimal SalesTargetAdujst = 0, SalesTargetAdujstRate = 0;
            TextBox tbx_Remark = gv_List.Rows[row.RowIndex].FindControl("tbx_Remark") == null ? null : (TextBox)gv_List.Rows[row.RowIndex].FindControl("tbx_Remark");
            DropDownList ddl_Flag = row.FindControl("ddl_Flag") == null ? null : (DropDownList)row.FindControl("ddl_Flag");  
            if (txt_SalesTargetAdjust != null && txt_SalesTargetAdjustRate != null && decimal.TryParse(txt_SalesTargetAdjust.Text.Trim(), out SalesTargetAdujst)
                && decimal.TryParse(txt_SalesTargetAdjustRate.Text.Trim(), out SalesTargetAdujstRate))
            {
                bll = new FNA_StaffSalaryDataObjectBetaBLL(id);
                decimal oldAdujstRate = bll.Model.Data08;
                bll.Model.SalesTargetAdjust = SalesTargetAdujst;
                bll.Model.Data08 = SalesTargetAdujstRate;
                bll.Model.UpdateStaff = (int)Session["UserID"];
                bll.Model.UpdateTime = DateTime.Now;
                bll.Model["Remark"] = tbx_Remark.Text.Trim();
                bll.Model.Flag = int.Parse(ddl_Flag.SelectedValue);
                bll.Update();
                if (bll.Model.ApproveFlag == 2 && oldAdujstRate != SalesTargetAdujstRate)
                {
                    FNA_StaffSalaryDataObjectBetaBLL.Adjust(bll.Model.AccountMonth, SalesTargetAdujst, bll.Model.Staff);
                }
            }

        }
        BindGrid();
    }
    private bool CheckSalesTargetAdujst()
    {
        decimal Adujst = 0, AdujstTotal = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            TextBox tbx_SalesTargetAdujst = gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujst") == null ? null : (TextBox)gv_List.Rows[row.RowIndex].FindControl("tbx_SalesTargetAdujst");
            if (tbx_SalesTargetAdujst != null && decimal.TryParse(tbx_SalesTargetAdujst.Text.Trim(), out Adujst))
            {
                AdujstTotal += Adujst;
            }
        }
        if (AdujstTotal < 0)
        {
            MessageBox.Show(this, MCSTabControl1.SelectedTabItem.Value == "1" ? "营业部总目标调整后不能小于原目标" : "办事处内目标调整后总值应不变");
            return false;
        }
        if (AdujstTotal != 0 && MCSTabControl1.SelectedTabItem.Value == "2")
        {
            MessageBox.Show(this, "办事处内目标调整后总值应不变");
            return false;
        }
        return true;
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if (chk_Header.Checked)
        {
            FNA_StaffSalaryDataObjectBetaBLL.Approve(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(tr_OrganizeCity.SelectValue), int.Parse(MCSTabControl1.SelectedTabItem.Value), 1);
        }
        else
        {
            ApproveData(1);
        }
        BindGrid();
    }
    protected void bt_CancelApprove_Click(object sender, EventArgs e)
    {
        if (chk_Header.Checked)
        {
            FNA_StaffSalaryDataObjectBetaBLL.Approve(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(tr_OrganizeCity.SelectValue), int.Parse(MCSTabControl1.SelectedTabItem.Value), 2);
        }
        else
        {
            ApproveData(2);
        }
        BindGrid();
    }
    private void ApproveData(int approveflag)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["FNA_StaffSalaryDataObjectBeta_ID"];
                FNA_StaffSalaryDataObjectBetaBLL bll = new FNA_StaffSalaryDataObjectBetaBLL(id);
                bll.Approve(approveflag);
            }
        }
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void tbx_SalesTargetAdujst_TextChanged(object sender, EventArgs e)
    {
        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        decimal SalesTarget = 0, SalesTargetAdujst = 0;
        Label lbl_SalesTarget = gv_List.Rows[rowIndex].FindControl("lbl_SalesTarget") == null ? null : (Label)gv_List.Rows[rowIndex].FindControl("lbl_SalesTarget");
        TextBox tbx_SalesTargetAdujstRate = gv_List.Rows[rowIndex].FindControl("tbx_SalesTargetAdujstRate") == null ? null : (TextBox)gv_List.Rows[rowIndex].FindControl("tbx_SalesTargetAdujstRate");
        decimal.TryParse(t.Text.Trim(), out SalesTargetAdujst);
        if (lbl_SalesTarget != null && decimal.TryParse(lbl_SalesTarget.Text.Trim(), out SalesTarget) && Math.Abs(SalesTargetAdujst) > SalesTarget * 0.05m)
        {
            t.Text = "0";
            MessageBox.Show(this, "销售目标调整值不能超过目标值的5%：" + (SalesTarget * 0.05m).ToString());
        }
        else if (tbx_SalesTargetAdujstRate != null)
        {
            tbx_SalesTargetAdujstRate.Text = (Math.Round(SalesTargetAdujst / SalesTarget * 100, 2, MidpointRounding.AwayFromZero)).ToString();
        }
    }
    protected void tbx_SalesTargetAdujstRate_TextChanged(object sender, EventArgs e)
    {
        TextBox t = (TextBox)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        decimal SalesTarget = 0, SalesTargetAdujstRate = 0;
        decimal.TryParse(t.Text.Trim(), out SalesTargetAdujstRate);

        if (decimal.TryParse(t.Text.Trim(), out SalesTargetAdujstRate) && Math.Abs(SalesTargetAdujstRate) > 5m)
        {
            t.Text = "0";
            MessageBox.Show(this, "销售目标调整比例不能超过5%");
        }
        else
        {
            Label lbl_SalesTarget = gv_List.Rows[rowIndex].FindControl("lbl_SalesTarget") == null ? null : (Label)gv_List.Rows[rowIndex].FindControl("lbl_SalesTarget");
            TextBox tbx_SalesTargetAdujst = gv_List.Rows[rowIndex].FindControl("tbx_SalesTargetAdujst") == null ? null : (TextBox)gv_List.Rows[rowIndex].FindControl("tbx_SalesTargetAdujst");
            if (lbl_SalesTarget != null && tbx_SalesTargetAdujst != null && decimal.TryParse(lbl_SalesTarget.Text.Trim(), out SalesTarget))
            {
                tbx_SalesTargetAdujst.Text = (SalesTarget * SalesTargetAdujstRate / 100).ToString();
            }
        }

    }
    protected void bt_refresh_Click(object sender, EventArgs e)
    {
        Button t = (Button)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        int id = (int)gv_List.DataKeys[rowIndex]["FNA_StaffSalaryDataObjectBeta_ID"];

        gv_List.PageIndex = gv_List.PageIndex;
        BindGrid();
    }
    protected void ddl_AccountMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (FNA_StaffSalaryDataObjectBetaBLL.GetModelList("Position=210 AND ApproveFlag=2 AND AccountMonth=" + ddl_AccountMonth.SelectedValue).Count > 0)
        {
            MCSTabControl1.SelectedIndex = 0;
            MCSTabControl1.Enabled = false;
            MCSTabControl1.Items[1].Enable = false;
            ViewState["Item1Enable"] = false;
            lbl_message.Text = "请先处理并审核办事处目标后再调整业代目标！";
        }
        else
        {
            MCSTabControl1.Enabled = true;
            MCSTabControl1.Items[1].Enable = true;
            ViewState["Item1Enable"] = true;
        }

    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        BindGrid();
    }
}
