using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_SVM_AdvanceFind_SalesVolumeAdvanceFind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Type"] = Request.QueryString["Type"] == null ? 0 : int.Parse(Request.QueryString["Type"]); //客户类型，1:公司仓库 2：经销商 3：终端门店

            #region 设置页面Title
            if ((int)ViewState["Type"] == 1)
                lb_PageTitle.Text = "办事处出货查询";
            else if ((int)ViewState["Type"] == 2)
                lb_PageTitle.Text = "经销商出货查询";
            else if ((int)ViewState["Type"] == 3)
                lb_PageTitle.Text = "零售商销量查询";
            else if ((int)ViewState["Type"] == 4)
                lb_PageTitle.Text = "办事处进货查询";
            #endregion

            AdvancedSearch1.ExtCondition = "SVM_SalesVolume.Type=" + ViewState["Type"].ToString();

            #region 判断当前可查询的范围
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);

            if (staff.Model.OrganizeCity > 1)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity, true);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += staff.Model.OrganizeCity.ToString();

                AdvancedSearch1.ExtCondition += " AND SVM_SalesVolume.OrganizeCity IN (" + orgcitys + ")";
            }
            #endregion
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {

        switch (e.item.Value)
        {
            case "0"://快捷查询
                Response.Redirect("../SalesVolumeList.aspx?Type=" + ViewState["Type"].ToString());
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
