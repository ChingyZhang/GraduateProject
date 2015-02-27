using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.Promotor;
using System.Text;
using MCSFramework.Common;

public partial class SubModule_PM_PM_SalaryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            BindGrid();
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
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth()-1).ToString();

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("PM_SalaryState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

        #region 绑定导购员工资类别
        ddl_PMClassify.DataSource = DictionaryBLL.GetDicCollections("PM_SalaryClassify");
        ddl_PMClassify.DataBind();
        ddl_PMClassify.Items.Insert(0, new ListItem("所有", "0"));
        ddl_PMClassify.SelectedValue = "0";
        #endregion

        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,7)=1\"";

        if (Request.QueryString["Client"] != null)
        {
            select_Client.SelectValue = Request.QueryString["Client"].ToString();
        }
    }
    #endregion

    private void BindGrid()
    {
        string condition = "1=1";

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND PM_Salary.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件
        condition += " AND PM_Salary.AccountMonth =" + ddl_AccountMonth.SelectedValue;
        //导购员分类
        if (ddl_PMClassify.SelectedValue != "0")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',PM_Salary.ExtPropertys,'PMClassfiy')="+ddl_PMClassify.SelectedValue;
        }
        if (select_Client.SelectValue != "")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt2('MCS_Promotor.dbo.PM_Salary',PM_Salary.ExtPropertys,'Client')=" + select_Client.SelectValue;
        }
        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND PM_Salary.State = " + ddl_State.SelectedValue;
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Generate_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM_SalaryGenerate.aspx");
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];

        Response.Redirect("PM_SalaryDetailList.aspx?ID=" + id.ToString());
    }
    protected void bt_Merge_Click(object sender, EventArgs e)
    {
        int result=0;      
        StringBuilder SalaryIDs = new StringBuilder();
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("cbx");
            
            if (chk.Checked)
            {
             SalaryIDs.Append(gv_List.DataKeys[row.RowIndex]["PM_Salary_ID"] +",");
             
            }
        }
        if(!string.IsNullOrEmpty(SalaryIDs.ToString()))
        {
            SalaryIDs.Remove(SalaryIDs.Length - 1, 1);
           result= PM_SalaryBLL.Merge(int.Parse(ddl_AccountMonth.SelectedValue),SalaryIDs.ToString(),(int)Session["UserID"]);
        }
        if (result > 0)
        {
            MessageBox.ShowAndRedirect(this, "合并成功!", "PM_SalaryDetailList.aspx?ID=" + result.ToString());
        }
        else
        {
            MessageBox.Show(this,"请确认所选工资单的管理片区上级城市属于同一城市。");
        }      

                
    }
}
