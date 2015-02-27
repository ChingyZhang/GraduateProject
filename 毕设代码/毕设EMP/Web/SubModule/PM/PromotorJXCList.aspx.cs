using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;
using System.Data;
using MCSFramework.BLL.Promotor;

public partial class SubModule_SVM_PromotorJXCList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
            #region 获取页面参数
            if (Request.QueryString["PromotorID"] != null)
            {
                ViewState["PromotorID"] = Int32.Parse(Request.QueryString["PromotorID"]);
                PM_PromotorBLL pro = new PM_PromotorBLL((int)ViewState["PromotorID"]);
                select_Promotor.SelectValue = pro.Model.ID.ToString();
                select_Promotor.SelectText = pro.Model.Name;
                select_Promotor_SelectChange(null, null);
            }

            if (Request.QueryString["AccountMonth"] != null)
            {
                ddl_AccountMonth.SelectedValue = Request.QueryString["AccountMonth"];
            }
            #endregion

            BindGrid();
        }
    }

    #region 绑定下拉列表框
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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE()");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Promotor.PageUrl = "Search_SelectPromotor.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Promotor.SelectText = "";
        select_Promotor.SelectValue = "";
    }

    protected void select_Promotor_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        try
        {
            if (select_Promotor.SelectValue != "")
            {
                tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(select_Promotor.SelectValue)).Model.OrganizeCity.ToString();
            }
        }
        catch
        {
        }
    }
    #endregion

    protected void bt_Search_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    #region 绑定销量明细列表
    private void BindGrid()
    {
        DataTable dt = null;
        int month = int.Parse(ddl_AccountMonth.SelectedValue);

        if (select_Promotor.SelectValue != "")
        {
            string orgcitys = "";
            DataTable dtclients = PM_PromotorBLL.GetClientList(int.Parse(select_Promotor.SelectValue));
            foreach (DataRow dr in dtclients.Rows)
            {
                orgcitys += "," + dr["ID"].ToString();
            }
            if (orgcitys != "")
                orgcitys = orgcitys.Substring(1, orgcitys.Length - 1);
            dt = null;// SVM_JXCBLL.GetSummaryJXC(orgcitys, month);
        }
        gv_List.DataSource = dt;
        gv_List.DataBind();
    }
    #endregion

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
