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

public partial class SubModule_FNA_StaffSalary_StaffSalarySetGenerateFlag : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropdown();
        }
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

        rbl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        rbl_ApproveFlag.DataBind();
        rbl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        rbl_ApproveFlag.SelectedValue = "0";
    }
    private void BindGrid()
    {        
        AC_AccountMonth month = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model;
        int monthdays = month.EndDate.Subtract(month.BeginDate).Days + 1;      
        ViewState["monthdays"] = monthdays;
        string condition = "FNA_StaffSalaryDataObject.AccountMonth=" + ddl_AccountMonth.SelectedValue;
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND  MCS_SYS.dbo.Org_Staff.OrganizeCity in(" + orgcitys + ") ";
        }
        if (select_Staff.SelectValue != "")
        {
            condition += " AND  MCS_SYS.dbo.Org_Staff.ID=" + select_Staff.SelectValue;
        }
        if (rbl_ApproveFlag.SelectedValue != "0")
        {
            condition += " AND  FNA_StaffSalaryDataObject.SubmitFlag=" + rbl_ApproveFlag.SelectedValue;
        }
        if (MCSTabControl1.SelectedIndex == 0)
        {
            condition += " AND  FNA_StaffSalaryDataObject.Position=210";
        }
        else
        {
            condition += " AND  FNA_StaffSalaryDataObject.Position!=210";
        }
        gv_List.ConditionString = condition +" ORDER BY  MCS_SYS.dbo.UF_GetSuperOrganizeCityByLevel02(Org_Staff.OrganizeCity,3),MCS_SYS.dbo.UF_GetSuperOrganizeCityByLevel02(Org_Staff.OrganizeCity,4)";
        gv_List.BindGrid();        
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        FNA_StaffSalaryDataObjectBLL bll = new FNA_StaffSalaryDataObjectBLL();
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["FNA_StaffSalaryDataObject_ID"];
            TextBox tbx_Remark = gv_List.Rows[row.RowIndex].FindControl("tbx_Remark") == null ? null : (TextBox)gv_List.Rows[row.RowIndex].FindControl("tbx_Remark");
            DropDownList ddl_Flag = row.FindControl("ddl_Flag") == null ? null : (DropDownList)row.FindControl("ddl_Flag");        

            if (tbx_Remark != null && ddl_Flag != null)
            {
                bll = new FNA_StaffSalaryDataObjectBLL(id);
                bll.Model["Remark"] = tbx_Remark.Text.Trim();
                bll.Model.Flag =int.Parse(ddl_Flag.SelectedValue);
                bll.Model.UpdateStaff = (int)Session["UserID"];
                bll.Model.UpdateTime = DateTime.Now;
                bll.Update();
            }
        }
        BindGrid();
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        
        FNA_StaffSalaryDataObjectBLL.SubmitFlag(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(tr_OrganizeCity.SelectValue), int.Parse(MCSTabControl1.SelectedTabItem.Value));

        BindGrid();
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        
        FNA_StaffSalaryDataObjectBLL.ApproveFlag(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(tr_OrganizeCity.SelectValue), int.Parse(MCSTabControl1.SelectedTabItem.Value));
        BindGrid();
    }
    protected void bt_CancelApprove_Click(object sender, EventArgs e)
    {
        FNA_StaffSalaryDataObjectBLL.UnApproveFlag(int.Parse(ddl_AccountMonth.SelectedValue), int.Parse(tr_OrganizeCity.SelectValue), int.Parse(MCSTabControl1.SelectedTabItem.Value));
        BindGrid();
    }
}
