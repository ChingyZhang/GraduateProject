using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using System.IO;

public partial class SubModule_OA_TrackCard_TC_TrackEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            if (staff.Model != null)
            {
                select_Staff.SelectValue = staff.Model.ID.ToString();
                select_Staff.SelectText = staff.Model.RealName;
                tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
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
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            select_Staff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        }
    }
    protected void bt_Init_Click(object sender, EventArgs e)
    {
        int month = 0, organizecity = 0, staff = 0;
        DateTime trackdate = DateTime.Today;
        if (int.TryParse(tr_OrganizeCity.SelectValue, out organizecity) &&
            int.TryParse(select_Staff.SelectValue, out staff) && DateTime.TryParse(tbx_TrackDate.Text, out trackdate))
        {
            month = AC_AccountMonthBLL.GetMonthByDate(trackdate);

            #region 限制填报规则
            if (trackdate > DateTime.Today)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报今天之后的数据!");
                return;
            }

            if (trackdate < DateTime.Today.AddDays(-7))
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报7天之前的数据!");
                return;
            }

            if (DateTime.Today.Day > 5 && trackdate.Day <= 5)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报5日之前的数据！");
                return;
            }
            else if (DateTime.Today.Day > 10 && trackdate.Day <= 10)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报10日之前的数据！");
                return;
            }
            else if (DateTime.Today.Day > 15 && trackdate.Day <= 15)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报15日之前的数据！");
                return;
            }
            else if (DateTime.Today.Day > 20 && trackdate.Day <= 20)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报20日之前的数据！");
                return;
            }
            else if (DateTime.Today.Day > 25 && trackdate.Day <= 25)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报25日之前的数据！");
                return;
            }
            else if (DateTime.Today.Month != trackdate.Month)
            {
                tbx_TrackDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                MessageBox.Show(this, "对不起，不能填报上月的数据！");
                return;
            }
            #endregion

            TC_TrackCardBLL.InitTrack(month, organizecity, staff, trackdate, (int)Session["UserID"]);
        }
        BindGrid();
    }

    private void BindGrid()
    {
        int month = 0, organizecity = 0, staff = 0;
        DateTime trackdate = DateTime.Today;
        if (int.TryParse(tr_OrganizeCity.SelectValue, out organizecity) && DateTime.TryParse(tbx_TrackDate.Text, out trackdate))
        {
            month = AC_AccountMonthBLL.GetMonthByDate(trackdate);

            string condition = "TrackDate = '" + trackdate.ToString("yyyy-MM-dd") + "'";
            int.TryParse(select_Staff.SelectValue, out staff);

            if (staff > 0) condition += " AND Staff = " + staff.ToString();
            if (organizecity > 1)
            {
                //管理片区及所有下属管理片区
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND OrganizeCity IN (" + orgcitys + ")";
            }

            if (ddl_IsSubmit.SelectedValue != "0")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',5)='" + ddl_IsSubmit.SelectedItem.Text + "'";
            }

            IList<TC_TrackCard> list = TC_TrackCardBLL.GetModelList(condition);
            gv_List.BindGrid<TC_TrackCard>(list);
        }
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        bool IsCheck = false;
        for (int i = 0; i < gv_List.Rows.Count; i++)
        {
            int id = (int)gv_List.DataKeys[i]["ID"];

            CheckBox cbx = (CheckBox)gv_List.Rows[i].FindControl("cbx");
            TC_TrackCardBLL bll = new TC_TrackCardBLL(id);
            if (bll.Model != null && bll.Model.ApproveFlag != 1 && cbx.Checked)
            {
                TextBox tbx;
                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data01");
                if (tbx != null) bll.Model.Data01 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data02");
                if (tbx != null) bll.Model.Data02 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data03");
                if (tbx != null) bll.Model.Data03 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data04");
                if (tbx != null) bll.Model.Data04 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data05");
                if (tbx != null) bll.Model.Data05 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data06");
                if (tbx != null) bll.Model.Data06 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data07");
                if (tbx != null) bll.Model.Data07 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data08");
                if (tbx != null) bll.Model.Data08 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data09");
                if (tbx != null) bll.Model.Data09 = int.Parse(tbx.Text);

                tbx = (TextBox)gv_List.Rows[i].FindControl("tbx_Data10");
                if (tbx != null) bll.Model.Data10 = int.Parse(tbx.Text);

                bll.Model["IsSubmit"] = "是";
                bll.Update();
                IsCheck = true;
            }
        }
        if (!IsCheck)
        {
            MessageBox.Show(this,"请勾选要保存的日跟踪表数据");
            return;
        }
        BindGrid();

        MessageBox.Show(this, "保存成功!");
    }
    protected void bt_Preday_Click(object sender, EventArgs e)
    {
        DateTime trackdate = DateTime.Today;
        if (DateTime.TryParse(tbx_TrackDate.Text, out trackdate))
        {
            tbx_TrackDate.Text = trackdate.AddDays(-1).ToString("yyyy-MM-dd");
            bt_Init_Click(null, null);
        }
    }
    protected void bt_NextDay_Click(object sender, EventArgs e)
    {
        DateTime trackdate = DateTime.Today;
        if (DateTime.TryParse(tbx_TrackDate.Text, out trackdate))
        {
            tbx_TrackDate.Text = trackdate.AddDays(1).ToString("yyyy-MM-dd");
            bt_Init_Click(null, null);
        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();
        string FileType = "application/ms-excel";
        string FileName = "Export_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
        gv_List.AllowPaging = true;
        BindGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null) cbx.Checked = cbx_CheckAll.Checked;
        }
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex]["ID"];
            TC_TrackCardBLL bll = new TC_TrackCardBLL(id);
            CheckBox cbx = (CheckBox)e.Row.FindControl("cbx");
            if (bll != null && bll.Model["IsSubmit"] == "是")
            {
                cbx.Checked = true;
            }
            else
                cbx.Checked = false;
        }
    }
}
