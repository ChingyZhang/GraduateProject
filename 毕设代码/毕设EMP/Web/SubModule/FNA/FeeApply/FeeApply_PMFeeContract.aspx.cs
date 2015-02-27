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
using MCSFramework.BLL.SVM;

public partial class SubModule_FNA_FeeApply_FeeApply_PMFeeContract : System.Web.UI.Page
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate BETWEEN DateAdd(month,-3,GETDATE()) AND GETDATE()");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();
    }
    #endregion

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue + "&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7) IN (1)\"";
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = 0;
        int client = 0;

        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        int.TryParse(select_Client.SelectValue, out client);

        if (organizecity == 0)
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
        FeeType = ConfigHelper.GetConfigInt("ContractFeeType-PM");

        int id = CM_ContractBLL.CreatePMFeeApply(organizecity, month, client, (int)Session["UserID"], FeeType);

        if (id > 0)
            MessageBox.ShowAndRedirect(this, "导购管理费申请单生成成功！", "FeeApplyDetail3.aspx?ID=" + id.ToString());
        else if (id == 0)
            MessageBox.Show(this, "对不起，目前尚无返利合同需要申请费用!");
        else if (id == -1)
            MessageBox.Show(this, "对不起，无导购管理费用生成！请检查是否之前已经生成该月的导购管理费用！");
        else
            MessageBox.Show(this, "对不起，导购管理费申请单生成失败！错误码:" + id.ToString());
    }
}
