using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;

public partial class SubModule_FNA_FeeApply_FeeApply_FLContract : System.Web.UI.Page
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

        #region 判断指定区域下是否还有门店销量未审核
        string citys = new Addr_OrganizeCityBLL(organizecity).GetAllChildNodeIDs();
        if (citys == "")
            citys = organizecity.ToString();
        else
            citys += "," + organizecity.ToString();
        string condition = "Type=3 AND Supplier=" + client.ToString() + " AND AccountMonth=" + month.ToString() +
            " AND OrganizeCity IN (" + citys + ") AND ApproveFlag=2 AND Flag=1 AND EXISTS (SELECT 1 FROM MCS_CM.dbo.CM_Client WHERE ClientType=3 AND CM_Client.ID=SVM_SalesVolume.Client)";

        int counts = SVM_SalesVolumeBLL.GetModelList(condition).Count;
        if (counts > 0)
        {
            MessageBox.Show(this, "对不起，您区域还有" + counts.ToString() + "条门店销量未审核");
            return;
        }
        #endregion

        int FeeType = 0;
        FeeType = ConfigHelper.GetConfigInt("ContractFeeType-FL");

        int id = CM_ContractBLL.CreateFLFeeApply(organizecity, month, client, (int)Session["UserID"], FeeType);

        if (id > 0)
            MessageBox.ShowAndRedirect(this, "返利费用申请单生成成功！", "FeeApplyDetail3.aspx?ID=" + id.ToString());
        else if (id == 0)
            MessageBox.Show(this, "对不起，目前尚无返利合同需要申请费用!");
        else if (id == -1)
            MessageBox.Show(this, "对不起，无返利费用生成！请检查是否有返利店有销量，或是之前已经生成该月的返利费用！");
        else
            MessageBox.Show(this, "对不起，返利合同费用申请单生成失败！错误码:" + id.ToString());
    }
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {       
        if (tr_OrganizeCity.SelectValue == "1")
        {
            tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(select_Client.SelectValue)).Model.OrganizeCity.ToString();
        }
        BindGrid();

    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = 0;
        int client = 0;

        int.TryParse(tr_OrganizeCity.SelectValue, out organizecity);
        int.TryParse(select_Client.SelectValue, out client);

        DataTable dt = CM_FLApply_BaseBLL.GetByDIClient(organizecity, month, client);

        gv_List.DataSource = dt;
        gv_List.DataBind();
        bt_Generate.Enabled = true;
        if (dt.Select("是否母婴大='是'AND 进货门店数=0").Count() > 0)
        {
            MessageBox.Show(this, "母婴大门店必须先填写本月进货门店个数，再生成费用！");
            bt_Generate.Enabled = false;
        }

    }
}
