using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;
using MCSFramework.Common;
using MCSFramework.BLL.Pub;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;

public partial class SubModule_SVM_SalesOrganizeTargetList : System.Web.UI.Page
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
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Today.AddMonths(-1)).ToString();

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "0";

    }
    #endregion

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("办事处销售目标导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();

        gv_List.AllowPaging = true;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);

        string condition = " AccountMonth=" + month.ToString();

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " And ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }
        if (organizecity > 1)
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(organizecity);
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND OrganizeCity IN (" + orgcitys + ")";
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected decimal GetSum(int id)
    {
        return new SVM_OrganizeTargetBLL().GetSumByID(id);
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        SVM_OrganizeTargetBLL.Approve(month, organizecity);
        BindGrid();
    }
    protected void bt_UnApprove_Click(object sender, EventArgs e)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        SVM_OrganizeTargetBLL.UnApprove(month, organizecity);
        BindGrid();
    }
}
