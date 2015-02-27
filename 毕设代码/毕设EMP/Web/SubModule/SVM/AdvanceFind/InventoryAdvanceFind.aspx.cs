using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_SVM_AdvanceFind_InventoryAdvanceFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，1:公司仓库 2：经销商 3：终端门店

            #region 设置页面Title
            if ((int)ViewState["ClientType"] == 1)
                lb_PageTitle.Text = "公司库存查询";
            else if ((int)ViewState["ClientType"] == 2)
                lb_PageTitle.Text = "经销商库存查询";
            else if ((int)ViewState["ClientType"] == 3)
                lb_PageTitle.Text = "零售商库存查询";
            #endregion

            AdvancedSearch1.ExtCondition = "CM_Client.ClientType=" + ViewState["ClientType"].ToString();

            #region 判断当前可查询的范围
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);

            if (staff.Model.OrganizeCity > 1)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity, true);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += staff.Model.OrganizeCity.ToString();

                AdvancedSearch1.ExtCondition += " AND [MCS_SVM].[dbo].[SVM_Inventory].OrganizeCity IN (" + orgcitys + ")";
            }
            #endregion
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {

        switch (e.item.Value)
        {
            case "0"://快捷查询
                Response.Redirect("../InventoryList.aspx?ClientType=" + ViewState["ClientType"].ToString());
                break;
            case "1"://高级查询
                break;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
}
