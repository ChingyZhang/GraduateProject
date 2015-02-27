using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.BLL.OA;

public partial class SubModule_FNA_FeeApply_FeeApply_CarFeeApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]); //费用类型

            BindDropDown();

            if ((int)ViewState["FeeType"] != 0)
            {
                ddl_FeeType.SelectedValue = ViewState["FeeType"].ToString();
                ddl_FeeType.Enabled = false;
            }

        }
    }

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
        tr_OrganizeCity_Selected(null, null);
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name).ToList();
        ddl_FeeType.DataBind();
        ddl_FeeType.SelectedValue = ConfigHelper.GetConfigInt("CarFeeType").ToString();
        ddl_FeeType.Enabled = false;
        //foreach (ListItem item in ddl_FeeType.Items)
        //{
        //    if (AC_AccountTitleBLL.GetListByFeeType(int.Parse(item.Value)).Where(p => p.ID > 1).ToList().Count == 0)
        //    {
        //        item.Enabled = false;
        //    }
        //}
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        ddl_Car.DataSource = Car_CarListBLL.GetCarListByOrganizeCity(int.Parse(tr_OrganizeCity.SelectValue));
        ddl_Car.DataBind();
        ddl_Car.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_Car.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择要申请费用的车辆车号！");
            return;
        }
        int CarAccountTitle = ConfigHelper.GetConfigInt("CarAccountTitle");
        Response.Redirect("FeeApplyDetail3.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue +
            "&FeeType=" + ddl_FeeType.SelectedValue +
            "&AccountTitle2=" + CarAccountTitle.ToString() +
            "&AccountMonth=" + ddl_AccountMonth.SelectedValue +
            "&RelateCar=" + ddl_Car.SelectedValue);
    }

}
