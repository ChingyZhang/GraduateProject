using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;

public partial class SubModule_Product_PDT_StandardPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }

    }
    private void BindDropDown()
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
    }

    private void BindGrid()
    {
        string condition = "1=1";

        #region 组织查询条件
        if (chb_ToOrganizecityChild.Checked)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND PDT_StandardPrice.OrganizeCity IN (" + orgcitys + ")";
        }
        else
        {
            condition += " AND PDT_StandardPrice.OrganizeCity=" + tr_OrganizeCity.SelectValue;
        }
        #endregion
        //if (MCSTabControl1.SelectedIndex == 0)
        //{
        //    condition += " AND PDT_StandardPrice.ActiveFlag=1 AND PDT_StandardPrice.ApproveFlag=1";
        //}
        //else
        //{
        //    condition += " AND (PDT_StandardPrice.ActiveFlag=2 OR PDT_StandardPrice.ApproveFlag=2)";
        //}
        condition += " AND PDT_StandardPrice.ActiveFlag=1 AND PDT_StandardPrice.ApproveFlag=1";
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDT_StandardPriceDetail.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue);
    }
}
