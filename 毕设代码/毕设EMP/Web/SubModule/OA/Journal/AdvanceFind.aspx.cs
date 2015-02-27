using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_CM_RT_AdvanceFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 判断当前可查询的范围
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);

            if (staff.Model.OrganizeCity >1)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity, true);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += staff.Model.OrganizeCity.ToString();
                as_uc_Journal.ExtCondition = " MCS_OA.dbo.JN_Journal.OrganizeCity IN (" + orgcitys + ") order by MCS_OA.dbo.JN_Journal.InsertTime desc";
            }
            #endregion
            as_uc_Journal.DataKeyNames = new string[] { "JN_Journal_ID" };
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询
                Response.Redirect("JournalList.aspx");
                break;
            case "1"://高级查询             
                break;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void as_uc_Journal_OnSelectedChanging(object sender, AdvancedSearch_OnSelectedChangingEventArgs e)
    {
        int _id = (int)e.SelectedRowDataKey[0];

        Response.Redirect("JournalDetail.aspx?ID=" + _id.ToString());
    }
}
