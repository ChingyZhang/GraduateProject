using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CSO;
using MCSFramework.Model;
using MCSFramework.Model.CSO;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

public partial class SubModule_CSO_CSO_OfferGetBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDropDown();
        }
    }

    private void BindDropDown()
    {
        #region 绑定管理片区
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

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate>DateAdd(month,-4,getdate()) AND EndDate<=Getdate()");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();
        ddl_AccountMonth_SelectedIndexChanged(null, null);
    }

    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Staff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Staff.SelectText = "";
        select_Staff.SelectValue = "";

        select_Doctor.PageUrl = "~/SubModule/CM/Hospital/Pop_Search_SelectDoctor.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Doctor.SelectText = "";
        select_Doctor.SelectValue = "";
    }

    protected void ddl_AccountMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        int month = 0;
        if (int.TryParse(ddl_AccountMonth.SelectedValue, out month) && month > 0)
        {
            AC_AccountMonth m = new AC_AccountMonthBLL(month).Model;
            if (m != null)
            {
                tbx_begin.Text = m.BeginDate.ToString("yyyy-MM-dd");
                tbx_end.Text = m.EndDate.ToString("yyyy-MM-dd");
            }
        }
    }

    protected void bt_Balance_Click(object sender, EventArgs e)
    {
        int _city = 0, _distributor = 0, _trackstaff = 0, _offerman = 0;
        int _month = 0;

        int.TryParse(ddl_AccountMonth.SelectedValue, out _month);
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1).AddSeconds(-1);

        int.TryParse(tr_OrganizeCity.SelectValue, out _city);
        int.TryParse(select_Client.SelectValue, out _distributor);
        int.TryParse(select_Staff.SelectValue, out _trackstaff);
        int.TryParse(select_Doctor.SelectValue, out _offerman);

        if (_city == 0)
        {
            MessageBox.Show(this, "请选择要提取的管理区域");
            return;
        }

        if (new Addr_OrganizeCityBLL(_city).Model.Level < 3)
        {
            MessageBox.Show(this, "提取的管理区域必须为营业部或办事处!");
            return;
        }

        int ret = CSO_SampleOfferBLL.BalanceFee(_city, _month, dtBegin, dtEnd, _distributor, _trackstaff, _offerman, (int)Session["UserID"]);
        if (ret == -1)
        {
            MessageBox.Show(this, "没有可提取的费用记录！");
            return;
        }

        if (ret > 0)
        {
            MessageBox.ShowAndRedirect(this, "成功提取营养教育结算单！", "CSO_OfferBalanceDetail.aspx?OfferBalanceID=" + ret.ToString());
        }
    }


}
