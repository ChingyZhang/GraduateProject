using System;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CSO;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;

public partial class SubModule_CSO_CSO_OfferBalanceList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //select_Staff.SelectValue = Session["UserID"].ToString();
            //select_Staff.SelectText = Session["UserRealName"].ToString();
            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID=1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>DateAdd(year,-2,getdate()) AND EndDate<=Getdate()");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10)) - 1).ToString();
    }

    private void BindGrid()
    {
        gv_List.ConditionString = GetCondition();
        gv_List.BindGrid();
    }

    private string GetCondition()
    {
        string ConditionStr = "";

        ConditionStr = " CSO_OfferBalance.AccountMonth = " + ddl_AccountMonth.SelectedValue;

        #region 判断当前可查询的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND CSO_OfferBalance.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        //if (select_Staff.SelectValue != "" && select_Staff.SelectValue != "0")
        //{
        //    ConditionStr += " AND (CSO_OfferBalance.InsertStaff=" + select_Staff.SelectValue + " OR ISNULL(CSO_OfferBalance.InsertStaff,1)=1)";
        //}
        return ConditionStr;

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        string selectids = "";
        foreach (GridViewRow item in gv_List.Rows)
        {
            if (((CheckBox)item.FindControl("cb_Select")).Checked == true)
            {
                selectids += gv_List.DataKeys[item.RowIndex].Value.ToString() + ",";
                int id = int.Parse(gv_List.DataKeys[item.RowIndex].Value.ToString());
                CSO_OfferBalanceBLL bll = new CSO_OfferBalanceBLL(id);
                if (bll.Model.State == 1 && bll.Model.ApproveFlag == 2) bll.Delete();
            }

        }
        if (string.IsNullOrEmpty(selectids))
        {
            MessageBox.Show(this, "请选择要删除的费用单！");
            return;
        }
        MessageBox.Show(this, "删除成功！");
        BindGrid();

    }


    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("CSO_OfferGetBalance.aspx");
    }
}
