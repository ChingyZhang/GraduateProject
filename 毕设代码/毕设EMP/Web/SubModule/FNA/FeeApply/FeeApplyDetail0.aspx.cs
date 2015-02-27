using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;

public partial class SubModule_FNA_FeeApply_FeeApplyDetail0 : System.Web.UI.Page
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
        #endregion

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate>=GETDATE() AND BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name).ToList();
        ddl_FeeType.DataBind();

        foreach (ListItem item in ddl_FeeType.Items)
        {
            if (AC_AccountTitleBLL.GetListByFeeType(int.Parse(item.Value)).Where(p => p.ID > 1).ToList().Count == 0)
            {
                item.Enabled = false;
            }
        }

        if (ConfigHelper.GetConfigBool("FeeApplyByAccountTitleLevel2"))
        {
            tr_AccountTitle2.Visible = true;
            ddl_FeeType.AutoPostBack = true;
            ddl_FeeType.Items.Insert(0, new ListItem("请选择...", "0"));
            ddl_FeeType_SelectedIndexChanged(null, null);
        }

        ddl_Brand.DataSource = PDT_BrandBLL.GetModelList("IsOpponent='1'");
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("多品牌", "0"));
    }

    protected void ddl_FeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ConfigHelper.GetConfigBool("FeeApplyByAccountTitleLevel2"))
        {
            if (ddl_FeeType.SelectedValue != "0")
            {
                ddl_AccountTitle2.DataSource = AC_AccountTitleBLL.GetListByFeeType(int.Parse(ddl_FeeType.SelectedValue)).Where(p => p.Level == 2);
                ddl_AccountTitle2.DataBind();
            }
            else
            {
                ddl_AccountTitle2.Items.Clear();
            }
            ddl_AccountTitle2.Items.Insert(0, new ListItem("请选择...", "0"));
        }
    }
    protected void bt_Confirm_Click(object sender, ImageClickEventArgs e)
    {
        if (ddl_FeeType.SelectedValue != "0")
        {
            if (ConfigHelper.GetConfigBool("FeeApplyByAccountTitleLevel2"))
            {
                if (ddl_AccountTitle2.SelectedValue != "0")
                    Response.Redirect("FeeApplyDetail3.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&FeeType=" + ddl_FeeType.SelectedValue + "&AccountTitle2=" + ddl_AccountTitle2.SelectedValue + "&AccountMonth=" + ddl_AccountMonth.SelectedValue + "&Brand=" + ddl_Brand.SelectedValue+"&FromGeneralFlow=Y");
                else
                    MessageBox.Show(this, "请正确选择申请的费用科目类别!");
            }
            else
            {
                Response.Redirect("FeeApplyDetail3.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&FeeType=" + ddl_FeeType.SelectedValue + "&AccountMonth=" + ddl_AccountMonth.SelectedValue + "&Brand=" + ddl_Brand.SelectedValue + "&FromGeneralFlow=Y");
            }
        }
        else
            MessageBox.Show(this, "请正确选择申请的费用类型!");
    }

}
