using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;

public partial class SubModule_EWF_App_PositionChangeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
            tbx_end.Text = DateTime.Now.ToShortDateString();

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
            tr_Position.DataSource = Org_PositionBLL.GetAllPostion();

            BindData();
        }
    }

    public void BindData()
    {
        string condition = "";
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);

        condition = "HR_ZhaoPinApply.InsertTime BETWEEN '" + dtBegin.ToString("yyyy-MM-dd") + "' AND '" + dtEnd.ToString("yyyy-MM-dd") + "' ";
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND HR_ZhaoPinApply.OrganizeCity IN (" + orgcitys + ")";
        }

        if (tr_Position.SelectValue != "0")
        {
            condition += " AND HR_ZhaoPinApply.sqgw =" + tr_Position.SelectValue;
        }

        //Response.Write(condition);
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindData();
    }
}
