using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.BLL;
using System.Data;

public partial class SubModule_EWF_TaskList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ViewState["PageIndex"] = 0;
            BindDropDown();

            BindGrid();
        }
    }
    private void BindDropDown()
    {
        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(" EnableFlag='Y' ");
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));

        #region 绑定当前操作员所能查看的管理片区
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
        #endregion

        ddl_FinishStatus.DataSource = DictionaryBLL.GetDicCollections("EWF_Flow_FinishStatus");
        ddl_FinishStatus.DataBind();
        ddl_FinishStatus.Items.Insert(0, new ListItem("全部", "0"));

    }

    private void BindGrid()
    {
        string condition = "(StartTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59')";

        condition += " AND Status=" + MCSTabControl1.SelectedTabItem.Value.ToString();

        if (ddl_App.SelectedValue != "0") condition += " AND (App = '" + ddl_App.SelectedValue + "')";


        if (select_Staff.SelectValue != "" && select_Staff.SelectValue != "0")
        {
            condition += " AND Initiator = " + select_Staff.SelectValue;
        }
        else
        {
            #region 判断当前可查询的范围
            string orgcitys = "";
            if (tr_OrganizeCity.SelectValue != "1")
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND Initiator IN (SELECT ID FROM MCS_SYS.dbo.Org_Staff WHERE OrganizeCity IN (" + orgcitys + "))";
            }
            #endregion
        }
        if (tbx_KeyWords.Text != "")
        {
            condition += " AND (Title like '%" + tbx_KeyWords.Text +
                "%' OR  CAST (ID AS VARCHAR)='" + tbx_KeyWords.Text + "')";
        }

        if (ddl_FinishStatus.SelectedValue != "0")
        {
            condition += " AND FinishStatus = " + ddl_FinishStatus.SelectedValue;
        }

        //condition += " ORDER BY ID desc";
        gv_List.PageIndex = (int)ViewState["PageIndex"];
        IList<EWF_Task> tasks = EWF_TaskBLL.GetModelList(condition);
        gv_List.BindGrid<EWF_Task>(tasks);
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void ddl_App_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }

    protected void btn_Search_Click(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
}
