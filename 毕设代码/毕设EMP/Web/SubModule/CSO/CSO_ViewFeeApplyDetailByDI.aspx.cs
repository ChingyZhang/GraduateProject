using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;

public partial class SubModule_CSO_CSO_ViewFeeApplyDetailByDI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取当前员工的关联经销商
            if ((int)Session["AccountType"] == 1)
            {
                Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
                int _relateclient = 0;
                if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
                {
                    ViewState["ClientID"] = _relateclient;
                }
            }
            else if ((int)Session["AccountType"] == 2)
            {
                ViewState["ClientID"] = (int)Session["UserID"];
            }
            #endregion

            BindDropDown();

            if (ViewState["ClientID"] != null && (int)ViewState["ClientID"] > 0)
            {
                CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.FullName;
                select_Client.Enabled = false;
            }
        }
    }

    private void BindDropDown()
    {
        if ((int)Session["AccountType"] == 1)
        {
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
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
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;
            if (client != null)
            {
                Addr_OrganizeCityBLL citybll = new Addr_OrganizeCityBLL(client.OrganizeCity);
                tr_OrganizeCity.DataSource = citybll.GetAllChildNodeIncludeSelf();
                tr_OrganizeCity.RootValue = citybll.Model.SuperID.ToString();
                tr_OrganizeCity.SelectValue = client.OrganizeCity.ToString();
            }
        }


        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>DateAdd(year,-2,getdate()) AND EndDate<=Getdate()");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10)) - 1).ToString();
    }

    private void BindGrid()
    {
        int client = 0, month = 0;
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_AccountMonth.SelectedValue, out month);

        int feetype = ConfigHelper.GetConfigInt("CSOCostType");                 //营养教育费用类型
        int feeaccounttitle = ConfigHelper.GetConfigInt("CSOOfferFeeACTitle");  //营养教育新客费科目

        string condition = "";
        if (client > 0)
        {
            condition = string.Format(@"ApplyID IN (SELECT ID FROM MCS_FNA.dbo.FNA_FeeApply WHERE AccountMonth={0} AND Client={1}   
AND FeeType={2} AND State=3) AND AccountTitle={3}",
                month, client, feetype, feeaccounttitle);
        }
        else
        {
            #region 判断当前可查询的范围
            string orgcitys = "";
            if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                if (orgcitys != "")
                    condition = string.Format(@"ApplyID IN (SELECT ID FROM MCS_FNA.dbo.FNA_FeeApply WHERE AccountMonth={0} AND OrganizeCity IN {1}   
AND FeeType={2} AND State=3) AND AccountTitle={3}",
                month, orgcitys, feetype, feeaccounttitle);

            }
            else
                condition = string.Format(@"ApplyID IN (SELECT ID FROM MCS_FNA.dbo.FNA_FeeApply WHERE AccountMonth={0} 
AND FeeType={1} AND State=3) AND AccountTitle={2}",
                month, feetype, feeaccounttitle);

            #endregion


        }
        if (rbl_Flag.SelectedValue == "Y")
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeApplyDetail',FNA_FeeApplyDetail.ExtPropertys,'BankVoucherNo')<>'' ";
        else if (rbl_Flag.SelectedValue == "N")
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeApplyDetail',FNA_FeeApplyDetail.ExtPropertys,'BankVoucherNo')='' ";

        gv_List.BindGrid(new FNA_FeeApplyBLL().GetDetail(condition));
    }

    protected string SplitString(string value, int position)
    {
        return MCSFramework.Common.Tools.SplitString(value, ',', position);
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Confirm_Click(object sender, EventArgs e)
    {

    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (bt_Save.Visible) bt_Save_Click(null, null);

        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
            TextBox tbx_BankVoucherNo = (TextBox)row.FindControl("tbx_BankVoucherNo");
            if (tbx_BankVoucherNo != null)
            {
                if (tbx_BankVoucherNo.Text != "")
                {
                    string No = tbx_BankVoucherNo.Text;
                    if (No == "空" || No == "无") No = "";

                    FNA_FeeApplyDetail d = new FNA_FeeApplyBLL().GetDetailModel(id);
                    d["BankVoucherNo"] = No;
                    new FNA_FeeApplyBLL(d.ApplyID).UpdateDetail(d);
                    count++;
                }
            }
        }

        if (sender != null) { MessageBox.Show(this, "成功更新" + count.ToString() + "行记录！"); BindGrid(); }
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
        BindGrid();
        ToExcel(gv_List, "ExtportFile.xls");

        gv_List.AllowPaging = true;
        gv_List.Columns[gv_List.Columns.Count - 1].Visible = false;
        BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

}
