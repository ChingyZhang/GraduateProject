using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;

public partial class SubModule_OA_Car_Car_Car_UsageList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["CarID"] = Request.QueryString["CarID"] == null ? 0 : int.Parse(Request.QueryString["CarID"]);

            if (Request.QueryString["State"] != null)
            {
                MCSTabControl1.SelectedIndex = int.Parse(Request.QueryString["State"]) - 1;
            }

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindDropDown();

            if ((int)ViewState["CarID"] > 0)
            {
                if (ddl_Car.Items.FindByValue(ViewState["CarID"].ToString()) != null)
                {
                    ddl_Car.SelectedValue = ViewState["CarID"].ToString();
                    ddl_Car.Enabled = false;
                    tr_OrganizeCity.Enabled = false;
                }
            }

            BindGrid();
        }
    }
    #region 绑定下拉列表框
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
        tr_OrganizeCity_Selected(null, null);
    }
    #endregion

    private void BindGrid()
    {
        #region 可查询的车辆目录
        string cars = "";
        if (tr_OrganizeCity.SelectValue != "1")
        {
            foreach (ListItem item in ddl_Car.Items)
            {
                if (item.Value != "0") cars += "'" + item.Value + "',";
            }
            if (cars.EndsWith(",")) cars = cars.Substring(0, cars.Length - 1);
        }

        #endregion

        if (MCSTabControl1.SelectedIndex == 0)
        {
            string ConditionStr = " ISNULL(Car_DispatchRide.ActGoBackTime,GETDATE()) BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

            if (ddl_Car.SelectedValue != "0")
                ConditionStr += " AND Car_DispatchRide.CarID = " + ddl_Car.SelectedValue;
            else if (cars != "")
            {
                ConditionStr += " AND Car_DispatchRide.CarID IN (" + cars + ")";
            }

            gv_List_Evection.ConditionString = ConditionStr;
            gv_List_Evection.BindGrid();
            gv_List_Evection.Visible = true;
            gv_List_FeeApply.Visible = false;
        }
        else
        {
            string ConditionStr = " FNA_FeeApply.State IN (2,3) AND FNA_FeeApply.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";

            if (ddl_Car.SelectedValue != "0")
                ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeApply',FNA_FeeApply.ExtPropertys,'RelateCar') = '" + ddl_Car.SelectedValue + "'";
            else if (cars != "")
            {
                ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeApply',FNA_FeeApply.ExtPropertys,'RelateCar') IN (" + cars + ")";
            }
            else
            {
                ConditionStr += " AND MCS_SYS.dbo.UF_Spilt2('MCS_FNA.dbo.FNA_FeeApply',FNA_FeeApply.ExtPropertys,'RelateCar') <> ''";
            }

            gv_List_FeeApply.ConditionString = ConditionStr;
            gv_List_FeeApply.BindGrid();
            gv_List_FeeApply.Visible = true;
            gv_List_Evection.Visible = false;
        }
    }
    #region 选中等事件
    protected void gv_List_Evection_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List_Evection.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("Car_DispatchRideDetail.aspx?ID=" + _id.ToString());
    }
    protected void gv_List_FeeApply_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List_FeeApply.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("~/SubModule/FNA/FeeApply/FeeApplyDetail3.aspx?ID=" + _id.ToString());
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List_Evection.PageIndex = 0;
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List_Evection.PageIndex = 0;
        BindGrid();
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        int city = int.Parse(tr_OrganizeCity.SelectValue);
        ddl_Car.DataSource = Car_CarListBLL.GetCarListByOrganizeCity(city).OrderBy(p => p.CarNo);
        ddl_Car.DataBind();
        ddl_Car.Items.Insert(0, new ListItem("请选择", "0"));
    }
}
