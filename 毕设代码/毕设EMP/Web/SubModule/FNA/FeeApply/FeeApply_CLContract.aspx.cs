using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;

public partial class SubModule_FNA_FeeApply_FeeApply_CLContract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
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

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();

        rbl_IsNKA.SelectedValue = Request.QueryString["ISNKA"] != null ? Request.QueryString["ISNKA"].ToString() : "2";
    }
    #endregion

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1)\"&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizectiy = 0;
        int client = 0;

        int.TryParse(tr_OrganizeCity.SelectValue, out organizectiy);
        int.TryParse(select_Client.SelectValue, out client);

        if (organizectiy == 0)
        {
            MessageBox.Show(this, "请正确选择管理片区");
            return;
        }

        if (client == 0)
        {
            MessageBox.Show(this, "请正确选择经销商");
            return;
        }

        int FeeType = 0;
        if (rbl_IsNKA.SelectedValue == "1")
            FeeType = ConfigHelper.GetConfigInt("ContractFeeType-KA");
        else
            FeeType = ConfigHelper.GetConfigInt("ContractFeeType");

        int id = CM_ContractBLL.CreateCLFeeApply(organizectiy, month, client, (int)Session["UserID"], rbl_IsNKA.SelectedValue == "1", FeeType);

        if (id > 0)
            MessageBox.ShowAndRedirect(this, "陈列费用申请单生成成功！", "FeeApplyDetail3.aspx?ID=" + id.ToString());
        else if (id == 0)
            MessageBox.Show(this, "对不起，目前尚无陈列合同需要申请费用!");
        else if (id == -1)
            MessageBox.Show(this, "对不起，已经生成过陈列合同费用申请单,请勿重复申请！");
        else
            MessageBox.Show(this, "对不起，陈列合同费用申请单生成失败！错误码:" + id.ToString());
    }

}
