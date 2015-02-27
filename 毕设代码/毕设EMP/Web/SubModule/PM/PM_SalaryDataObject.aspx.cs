using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using MCSFramework.Model.Pub;
using MCSFramework.Common;
using System.Data;

public partial class SubModule_PM_PM_SalaryDataObject : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["PromotorID"] = Request.QueryString["PromotorID"] == null ? 0 : int.Parse(Request.QueryString["PromotorID"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            BindDropdown();
        }
    }


    private void BindDropdown()
    {
        #region 绑定用户可管辖的片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if ((int)ViewState["PromotorID"] != 0)
        {
            PM_Promotor pm = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
            select_promotor.SelectText = pm.Name;
            select_promotor.SelectValue = pm.ID.ToString();
        }

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

        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";

        if ((int)ViewState["PromotorID"] != 0)
        {
            PM_Promotor pm = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
            select_promotor.SelectText = pm.Name;
            select_promotor.SelectValue = pm.ID.ToString();
            tr_OrganizeCity.SelectValue = pm.OrganizeCity.ToString();
            BtnDelete.Enabled = false;
            BtnSave.Enabled = false;
            gv_List.Enabled = false;
        }
        if ((int)ViewState["AccountMonth"] != 0)
        {
            ddl_AccountMonth.SelectedValue = ViewState["AccountMonth"].ToString();
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string promotors = "";
        AC_AccountMonth month = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model;
        int monthdays = month.EndDate.Subtract(month.BeginDate).Days + 1;
        lbl_message.Text = "注意：所选会计月天数为：" + monthdays.ToString() + "天，实际工作天数不能大于会计月天数";
        ViewState["monthdays"] = monthdays;
        string condition = "PM_SalaryDataObject.AccountMonth=" + ddl_AccountMonth.SelectedValue;
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND  MCS_Promotor.dbo.PM_Promotor.OrganizeCity in(" + orgcitys + ") ";
        }
        if (select_promotor.SelectValue != "")
        {
            condition += " AND  MCS_Promotor.dbo.PM_Promotor.ID=" + select_promotor.SelectValue;
        }
        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " AND  PM_SalaryDataObject.ApproveFlag=" + ddl_ApproveFlag.SelectedValue;
        }
        if (select_Client.SelectValue != "")
        {
            DataTable tb_promotor = PM_PromotorBLL.GetByDIClient(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(select_Client.SelectValue), month.ID);
            foreach (DataRow row in tb_promotor.Rows)
            {
                promotors += row["Promotor"].ToString() + ",";
            }
            if (promotors != "")
                promotors = " AND PM_SalaryDataObject.Promotor IN (" + promotors.Substring(0, promotors.Length - 1) + ")";
            else
                promotors = " AND 1 = 2";

        }
        gv_List.ConditionString = condition + promotors;
        gv_List.BindGrid();
        chkHeader.Checked = false;
    }

    protected bool GetEnable(int promotor, int approveflag)
    {
        bool enableflag = true;
        try
        {
            int SalaryState = PM_SalaryBLL.PM_Salary_GetStateByPromotor(promotor, int.Parse(ddl_AccountMonth.SelectedValue));
            if (SalaryState>0&& SalaryState <= 3|| approveflag == 1)//已提交或审核通过的工资表不可修改底量与奖惩
            {
                enableflag = false;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return enableflag;
    }

    protected bool GetIsFloatingEnable(int promotor)
    {
        bool enableflag = false;
        if (PM_PromotorSalaryBLL.GetModelList("Promotor=" + promotor.ToString() + " AND State=3 AND BasePayMode>3").Count > 0)
        {
            enableflag = true;
        }
        return enableflag;
    }

    protected bool GetInsureEnable(int promotor, int approveflag)
    {
        bool enableflag = true;
        try
        {
            int SalaryState = PM_SalaryBLL.PM_Salary_GetStateByPromotor(promotor, int.Parse(ddl_AccountMonth.SelectedValue));
            if (SalaryState>0&& SalaryState <= 3 || approveflag == 1)//已提交或审核通过的工资表不可修改底量与奖惩
            {
                enableflag = false;
            }
            //社保模式为【自购保险】才可填写社保报销额
            IList<PM_PromotorSalary> PM_SalaryList = PM_PromotorSalaryBLL.GetModelList("Promotor=" + promotor.ToString() + " AND State=3 AND ApproveFlag=1");
            if (PM_SalaryList.Count > 0 && PM_SalaryList[0].InsuranceMode != 8)
            {
                enableflag = false;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return enableflag;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["PM_SalaryDataObject_ID"];
            PM_SalaryDataObjectBLL bll = new PM_SalaryDataObjectBLL(id);
            bll.Model.ActWorkDays = int.Parse(((TextBox)row.FindControl("tbx_ActWorkDays")).Text);
            if (bll.Model.ActWorkDays > Convert.ToDecimal(ViewState["monthdays"]))
            {
                MessageBox.Show(this, "实际工作天数不能大于会计月天数！错误行：" + (row.RowIndex + 1).ToString() + ";导购员姓名：" + row.Cells[2].Text);
                BindGrid();
                return;

            }

            bll.Model["Remark1"] = ((TextBox)row.FindControl("tbx_Remark1")).Text;
            bll.Model["Remark2"] = ((TextBox)row.FindControl("tbx_Remark2")).Text;
            bll.Model["Remark3"] = ((TextBox)row.FindControl("tbx_Remark3")).Text;
            bll.Model["Remark4"] = ((TextBox)row.FindControl("tbx_Remark4")).Text;

            //判断是否使用浮动底薪
            DropDownList ddl_IsFloating = row.FindControl("ddl_ISFloating") == null ? null : (DropDownList)row.FindControl("ddl_ISFloating");
            if (ddl_IsFloating != null)
                bll.Model["ISFloating"] = ddl_IsFloating.SelectedValue;

            //奖惩Data01~Data09; Data10为小计;社保报销额 Data17;提成调整 Data18；经销商补贴调整金额 Data19
            //卖场导购其他管理费用 Data20~Data29；Data20 为管理费调整金额，Data30 为小计(21~29)         
            bll.Model.Data10 = 0; bll.Model.Data30 = 0;
            for (int i = 1; i < 30; i++)
            {
                TextBox txt = row.FindControl("tbx_Data" + (i < 10 ? "0" : "") + i.ToString()) == null ? null : (TextBox)row.FindControl("tbx_Data" + (i < 10 ? "0" : "") + i.ToString());
                if (txt != null)
                {
                    bll.Model["Data" + (i < 10 ? "0" : "") + i.ToString()] = txt.Text == "" ? "0" : txt.Text;
                    if (i < 10) bll.Model.Data10 += decimal.Parse(bll.Model["Data0" + i.ToString()]);//奖惩小计

                    if (i > 20) bll.Model.Data30 += decimal.Parse(bll.Model["Data" + i.ToString()]);//为管理费调整金额小计
                }
            }
            if (bll.Model.Data11+bll.Model.Data18 < 0)
            {
                MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 提成总金额和调整金额之和不可小于0！");
                return;
            }
            if (bll.Model.Data30 < 0)
            {
                MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 卖场导购管理费小计不可小于0！");
                return;
            }
            if (decimal.Parse((row.FindControl("PM_PromotorSalary_RTManageCost") as Label).Text) + bll.Model.Data20 < 0)
            {
                MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 导购管理费和调整金额之和不可小于0！");
                return;
            }
            if (bll.Model.Data11 == 0)
            {
                if (bll.Model.Data01 < 0)
                {
                    MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 提成总金额为0时，销售奖罚不可小于0！");
                    return;
                }
                if (bll.Model.Data02 < 0)
                {
                    MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 提成总金额为0时，VIP奖罚不可小于0！");
                    return;
                }
                if (bll.Model.Data09 < 0)
                {
                    MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 提成总金额为0时，奖惩项其他不可小于0！");
                    return;
                }
                if (bll.Model.Data19 < 0)
                {
                    MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 提成总金额为0时，经销商承担调整金额不可小于0！");
                    return;
                }
            }
            //社保模式为【自购保险】,社保报销额必填
            IList<PM_PromotorSalary> PM_SalaryList = PM_PromotorSalaryBLL.GetModelList("Promotor=" + bll.Model.Promotor + " AND State=3 AND ApproveFlag=1");
            if (PM_SalaryList.Count > 0 && PM_SalaryList[0].InsuranceMode == 8 && bll.Model.Data17 == 0)
            {
                MessageBox.Show(this, "错误行：" + (row.RowIndex + 1).ToString() + ";导购员：" + row.Cells[2].Text + " 社保为自购，社保报销额必填！");
                BindGrid();
                return;
            }
            bll.Update();
        }
        if (sender != null)
        {
            BindGrid();
            MessageBox.Show(this, "保存成功");
        }
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        PM_SalaryDataObjectBLL.Init(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_AccountMonth.SelectedValue));
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["PM_SalaryDataObject_ID"];
                new PM_SalaryDataObjectBLL().Delete(id);
            }
        }
        BindGrid();
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        AC_AccountMonth month = new AC_AccountMonthBLL(int.Parse(ddl_AccountMonth.SelectedValue)).Model;
        select_promotor.PageUrl = "Search_SelectPromotor.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_promotor.SelectText = "";
        select_promotor.SelectValue = "";
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        MessageBox.Show(this, e.NewPageIndex.ToString());
        BindGrid();
    }
    #region 审核与反审核
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        ApproveData("1");
        BindGrid();
    }
    protected void bt_CancelApprove_Click(object sender, EventArgs e)
    {
        ApproveData("2");
        BindGrid();
    }

    private void ApproveData(string approveflag)
    {
        if (approveflag == "1")
        {
            BtnSave_Click(null, null);
        }
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            if (chk.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex]["PM_SalaryDataObject_ID"];
                PM_SalaryDataObjectBLL bll = new PM_SalaryDataObjectBLL(id);
                bll.Model["ApproveFlag"] = approveflag;
                bll.Update();
            }
        }
    }
    #endregion

    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        MatrixTable.GridViewMatric(gv_List);

    }

    protected void bt_refresh_Click(object sender, EventArgs e)
    {
        Button t = (Button)sender;
        GridViewRow drv = (GridViewRow)t.NamingContainer;
        int rowIndex = drv.RowIndex;
        int id = (int)gv_List.DataKeys[rowIndex]["PM_SalaryDataObject_ID"];
        PM_SalaryDataObjectBLL.Refresh(id);
        BindGrid();
    }
}
