using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;
using MCSFramework.Common;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;

public partial class SubModule_PM_PM_SalaryDetail_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Promotor"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["Promotor"] != 0)
            {
                select_promotor.SelectText = new PM_PromotorBLL((int)ViewState["Promotor"]).Model.Name;
                select_promotor.SelectValue = ViewState["Promotor"].ToString();
            }
            BindDropdown();
            BindGrid();
        }
    }
    protected string PromotorInClient(string RetailerS)
    {
        if (RetailerS.Equals("")) return "";
        string clientname = "";

        IList<CM_Client> lists = CM_ClientBLL.GetModelList("ID IN (" + RetailerS + ")");
        int count = 0;
        foreach (CM_Client c in lists)
        {
            if (count < 2)
            {
                clientname += "<a href='../CM/RT/RetailerDetail.aspx?ClientID=" + c.ID.ToString() + "' target='_blank' class='listViewTdLinkS1'>"
                    + c.FullName + "</a><br/>";
            }
            else
            {
                break;
            }
            count++;
        }
        if (count > 1) clientname += "共" + lists.Count.ToString() + "个门名";
        return clientname;
    }

    protected string GetActComplete(string actsales, string salestarget)
    {
        if (salestarget != "" && decimal.Parse(salestarget) != 0)
        {
            return (decimal.Parse(actsales) / decimal.Parse(salestarget) * 100).ToString("0.##");
        }
        return actsales;
    }
    protected string GetAccountMonth()
    {
        return ViewState["AccountMonth"].ToString();
    }
    private void BindDropdown()
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

        #region 绑定导购员类别
        ddl_PMClassify.DataSource = DictionaryBLL.GetDicCollections("PM_PromotorClassify");
        ddl_PMClassify.DataBind();
        ddl_PMClassify.Items.Insert(0, new ListItem("所有", "0"));
        ddl_PMClassify.SelectedValue = "0";
        #endregion

        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

        ViewState["AccountMonth"] = ddl_BeginMonth.SelectedValue;
    }
    protected void ddl_AccountMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["AccountMonth"] = ddl_BeginMonth.SelectedValue;
    }

    private void BindGrid()
    {
        string condition = "PM_Salary.State<8 AND PM_Salary.AccountMonth=PM_SalaryDataObject.AccountMonth AND PM_SalaryDataObject.AccountMonth Between " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND PM_SalaryDetail.Promotor IN (SELECT ID FROM MCS_Promotor.dbo.PM_Promotor Where OrganizeCity in(" + orgcitys + ") )";
        }
        if (ddl_PMClassify.SelectedValue != "0")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt(MCS_Promotor.dbo.PM_Promotor.ExtPropertys,'|',19)=" + ddl_PMClassify.SelectedValue;
        }
        if (cb_OnlyDisplayZero.Checked)
        {
            condition += " AND PM_SalaryDetail.Bonus=0";
        }
        if (select_promotor.SelectValue != "")
        {
            condition += " AND PM_SalaryDetail.Promotor=" + select_promotor.SelectValue;
        }

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
        MatrixTable.GridViewMatric(gv_List);
        
    }
    protected void bt_search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}
