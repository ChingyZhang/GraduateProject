using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_PM_AdvanceFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 判断当前可查询的范围
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);

            if (staff.Model.OrganizeCity > 1 && staff.GetStaffOrganizeCity().Select("ID = 1").Length == 0)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity, true);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += staff.Model.OrganizeCity.ToString();

                as_uc_staff.ExtCondition = " MCS_SYS.dbo.Org_Staff.OrganizeCity IN (" + orgcitys + ")";
            }
            #endregion
            as_uc_staff.DataKeyNames = new string[] { "Org_Staff_ID" };
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询
                Response.Redirect("StaffManage.aspx");
                break;
            case "1"://高级查询             
                break;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void as_uc_staff_OnSelectedChanging(object sender, AdvancedSearch_OnSelectedChangingEventArgs e)
    {
        int _id = (int)e.SelectedRowDataKey[0];

        Response.Redirect("StaffDetail.aspx?ID=" + _id.ToString());
    }
}
