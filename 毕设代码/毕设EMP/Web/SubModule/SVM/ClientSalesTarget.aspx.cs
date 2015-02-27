using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;

public partial class SubModule_SVM_ClientSalesTarget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            BindDropDown();
            BindGrid();
            select_Client.PageUrl = "../PopSearch/Search_SelectClient.aspx?ClientType=3";

        }

    }
    private void BindDropDown()
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
      
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Now.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = "0";
        if (ViewState["AccountMonth"] != null)
            ddl_Month.SelectedValue = ViewState["AccountMonth"].ToString();

    }
    private void BindGrid()
    {
        string condtion = "1=1";
        if (select_Client.SelectValue != "")
        {
            condtion += " AND SVM_ClientSalesTarget.Client=" + select_Client.SelectValue;
        }
        if (ddl_Month.SelectedValue != "")
        {
            condtion += " AND SVM_ClientSalesTarget.AccountMonth=" + ddl_Month.SelectedValue;
        }

        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") condtion += " AND SVM_ClientSalesTarget.OrganizeCity IN (" + orgcitys + ")";
        }
        gv_List.ConditionString = condtion;
        gv_List.OrderFields = "CM_Client_OrganizeCity3,CM_Client_OrganizeCity4";
        gv_List.BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        gv_List.DataBind();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SVM_ClientSalesTargetBLL _bll;
        foreach(GridViewRow row in gv_List.Rows)
        {
            int id =int.Parse( gv_List.DataKeys[row.RowIndex]["ID"].ToString());
            _bll = new SVM_ClientSalesTargetBLL(id);
            TextBox txt_SalesTargetAdjust =row.FindControl("tbx_SalesTargetAdjust")!=null? (TextBox)row.FindControl("tbx_SalesTargetAdjust"):null;
            decimal SalesTargetAdjust = 0;
            if (txt_SalesTargetAdjust != null && decimal.TryParse(txt_SalesTargetAdjust.Text.Trim(),out SalesTargetAdjust))
            {
                _bll.Model.SalesTargetAdjust = SalesTargetAdjust;
                _bll.Update();
            }
        }
    }
}
