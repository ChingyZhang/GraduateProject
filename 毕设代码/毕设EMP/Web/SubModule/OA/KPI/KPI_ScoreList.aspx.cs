using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using System.IO;
using System.Text;

public partial class SubModule_OA_KPI_KPI_ScoreList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["CheckType"] = Request.QueryString["CheckType"] == null ? 0 : int.Parse(Request.QueryString["CheckType"]);//1为自评 2 为考核 3 为审核
        if (!Page.IsPostBack)
        {
            BindDropDown();
            ViewState["LabelVisiable"] = false;
        }
    }

    #region 绑定下拉列表
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        string condition = "ApproveFlag=1";
        #region 绑定考核方案
        switch ((int)ViewState["CheckType"])
        {
            case 0:
                // Response.Redirect("~/SubModule/desktop.aspx");
                break;
            case 1:
                condition += " and RelatePosition=" + staff.Model.Position.ToString() + " AND ID IN (SELECT Scheme FROM MCS_OA.dbo.KPI_SchemeDetail WHERE AllowSelfcheck=1)";
                gv_List.Columns[gv_List.Columns.Count - 4].Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;
                break;
            case 2:
                condition += " AND ID IN (SELECT Scheme FROM MCS_OA.dbo.KPI_SchemeDetail WHERE CheckPosition=" + staff.Model.Position.ToString() + ")";
                gv_List.Columns[gv_List.Columns.Count - 3].Visible = false;
                gv_List.Columns[gv_List.Columns.Count - 2].Visible = false;
                break;
            case 3:
                condition += " AND ID IN (SELECT Scheme FROM MCS_OA.dbo.KPI_SchemeDetail WHERE ApprovePosition=" + staff.Model.Position.ToString() + ")";
                break;

        }
        ddl_KPI_Scheme.DataSource = KPI_SchemeBLL.GetModelList(condition);
        ddl_KPI_Scheme.DataBind();
        ddl_KPI_Scheme.Items.Insert(0, new ListItem("请选择", "0"));
        #endregion

        #region
        ddl_Position.DataSource = Org_PositionBLL.GetModelList("ID IN(Select RelatePosition FROM MCS_OA.dbo.KPI_Scheme WHERE Cycle=1)");
        ddl_Position.DataBind();
        ddl_Position.Items.Insert(0, new ListItem("请选择", "0"));
        #endregion


        #region 绑定用户可管辖的片区
        ViewState["Position"] = staff.Model.Position;
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
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();



        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));

    }
    #endregion

    private void BindGrid()
    {
        int month = int.Parse(ddl_AccountMonth.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int scheme = int.Parse(ddl_KPI_Scheme.SelectedValue);
        int staff;
        int.TryParse(select_Staff.SelectValue, out staff);
        int approveflag = int.Parse(ddl_ApproveFlag.SelectedValue);
        int position = int.Parse(ddl_Position.SelectedValue);
        DataTable dt_detail = KPI_ScoreBLL.KPIGetByAccountMonth(organizecity, month, scheme, staff, approveflag, position);
        if (dt_detail.Rows.Count == 0)
        {
            gv_List.DataBind();
            return;
        }
        dt_detail = MatrixTable.Matrix(dt_detail, new string[] { "ID", "营业部", "办事处", "会计月", "员工姓名", "职位", "审核标志", "KPI总达成率" },
                       new string[] { "schemename", "detailname" }, "LastValue", false, false);
         
        gv_List.DataSource = dt_detail;
        gv_List.DataBind();

        if (dt_detail.Columns.Count >= 24)
            gv_List.Width = new Unit(dt_detail.Columns.Count * 55);
        else
            gv_List.Width = new Unit(100, UnitType.Percentage);

        MatrixTable.GridViewMatric(gv_List);
    }
    protected void bt_Check_Click(object sender, EventArgs e)
    {
        ViewState["LabelVisiable"] = false;
        KPI_ScoreBLL.Init(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_AccountMonth.SelectedValue), (int)Session["UserID"]);
        BindGrid();
    }


    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Save();
        gv_List.PageIndex = e.NewSelectedIndex;
        BindGrid();
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        Save();
        BindGrid();
        MessageBox.Show(this, "保存成功");
    }

    private void Save()
    {
        KPI_ScoreBLL bll; int id, detailID;
        foreach (GridViewRow row in gv_List.Rows)
        {
            id = (int)gv_List.DataKeys[row.RowIndex][0];
            detailID = (int)gv_List.DataKeys[row.RowIndex][1];
            bll = new KPI_ScoreBLL(id);
            KPI_ScoreDetail item = bll.GetDetailModel(detailID);
            item.SelfCheckValue = decimal.Parse(((TextBox)row.FindControl("tbx_SelfCheckValue")).Text);
            item.LeadCheckValue = decimal.Parse(((TextBox)row.FindControl("tbx_LeadCheckValue")).Text);
            item.ApprovedValue = decimal.Parse(((TextBox)row.FindControl("tbx_ApprovedValue")).Text);
            bll.UpdateDetail(item);
        }
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["LabelVisiable"] = true;
        KPI_ScoreBLL.Init(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_AccountMonth.SelectedValue), (int)Session["UserID"]);
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();

        string filename = HttpUtility.UrlEncode("员工KPI考核表导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
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
    protected void bt_Approve_Click(object sender, EventArgs e)
    {

        KPI_ScoreBLL bll; int id;
        foreach (GridViewRow row in gv_List.Rows)
        {

            CheckBox chk = (CheckBox)row.FindControl("chk_ID");
            if (chk.Checked)
            {
                id = (int)gv_List.DataKeys[row.RowIndex][0];
                bll = new KPI_ScoreBLL(id);
                bll.Model.ApproveFlag = 1;
                bll.Update();
            }
        }
        BindGrid();
        MessageBox.Show(this, "审核成功");
    }
    protected void bt_ImportKPI_Click(object sender, EventArgs e)
    {
        string url = ConfigHelper.GetConfigString("KPI_ImportToolsURL");
        if (!string.IsNullOrEmpty(url)) Response.Redirect(url);
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
