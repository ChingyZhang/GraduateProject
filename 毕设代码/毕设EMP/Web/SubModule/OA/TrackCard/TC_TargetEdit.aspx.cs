using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using System.Text;
using System.IO;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;

public partial class SubModule_OA_TrackCard_TC_TargetEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取参数
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["Staff"] = Request.QueryString["Staff"] == null ? 0 : int.Parse(Request.QueryString["Staff"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["OrganizeCity"] > 0) tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
            if ((int)ViewState["AccountMonth"] > 0) ddl_AccountMonth.SelectedValue = ViewState["AccountMonth"].ToString();
            if ((int)ViewState["Staff"] > 0)
            {
                Org_StaffBLL staff = new Org_StaffBLL((int)ViewState["Staff"]);
                if (staff.Model != null)
                {
                    select_Staff.SelectValue = staff.Model.ID.ToString();
                    select_Staff.SelectText = staff.Model.RealName;
                    tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
                }
            }

            if (select_Staff.SelectValue != "") BindGrid();
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

            select_Staff.SelectValue = staff.Model.ID.ToString();
            select_Staff.SelectText = staff.Model.RealName;
        }
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("EndDate >= DATEADD(day,-7,GETDATE())");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(6)).ToString();
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
        if (int.TryParse(ddl_AccountMonth.SelectedValue, out month) && int.TryParse(tr_OrganizeCity.SelectValue, out organizecity) &&
            int.TryParse(select_Staff.SelectValue, out staff))
        {
            TC_TrackCardBLL.InitTarget(month, organizecity, staff, (int)Session["UserID"]);
        }

        gv_List.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        int month = 0, organizecity = 0, staff = 0;
        if (int.TryParse(ddl_AccountMonth.SelectedValue, out month) && int.TryParse(tr_OrganizeCity.SelectValue, out organizecity))
        {
            string condition = "AccountMonth = " + month.ToString() + " AND TrackDate IS NULL";
            int.TryParse(select_Staff.SelectValue, out staff);

            if (staff > 0)
            {
                string client = string.Empty;
                IList<CM_Client> list = CM_ClientBLL.GetModelList(" OrganizeCity =" + organizecity + " AND ClientType = 3 AND ActiveFlag = 1 AND ApproveFlag = 1 AND [MCS_SYS].[dbo].[UF_Spilt](ExtPropertys,'|',27) = '1' AND ClientManager = " + staff.ToString());
                if (list != null && list.Count > 0)
                {
                    foreach (CM_Client item in list)
                    {
                        client += item.ID.ToString() + ",";
                    }
                    client = client.Substring(0, client.Length - 1);
                }
                if (!string.IsNullOrEmpty(client))
                    condition += " AND (Staff = " + staff.ToString() + " OR Client in(" + client + "))";
                else
                    condition += " AND Staff = " + staff.ToString();
            }
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

            gv_List.BindGrid(TC_TrackCardBLL.GetModelList(condition));
            cbx_CheckAll.Checked = false;
        }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (gv_List.Rows.Count > 0)
        {
            for (int i = 0; i < gv_List.Rows.Count; i++)
            {
                int id = (int)gv_List.DataKeys[i]["ID"];

                TC_TrackCardBLL bll = new TC_TrackCardBLL(id);
                if (bll.Model != null && bll.Model.ApproveFlag != 1)
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

                    if (bll.Model.Data01 != 0 || bll.Model.Data02 != 0 || bll.Model.Data03 != 0 || bll.Model.Data04 != 0 || bll.Model.Data05 != 0 || bll.Model.Data06 != 0 || bll.Model.Data07 != 0 || bll.Model.Data08 != 0 || bll.Model.Data09 != 0 || bll.Model.Data10 != 0)
                    {
                        bll.Model["IsSubmit"] = "是";
                    }
                    bll.Update();
                }
            }


            if (sender != null)
            {
                BindGrid();
                MessageBox.Show(this, "保存成功!");
            }
        }
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        if (gv_List.Rows.Count > 0)
        {
            bt_Save_Click(null, null);

            foreach (GridViewRow row in gv_List.Rows)
            {
                CheckBox cbx = (CheckBox)row.FindControl("cbx");
                if (cbx != null && cbx.Checked)
                {
                    int id = (int)gv_List.DataKeys[row.RowIndex]["ID"];
                    TC_TrackCardBLL bll = new TC_TrackCardBLL(id);
                    bll.Model.ApproveFlag = 1;
                    bll.Update();
                }
            }

            Response.Redirect("TC_TrackCardListByStaff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue + "&AccountMonth=" + ddl_AccountMonth.SelectedValue + "&Staff=" + (select_Staff.SelectValue == "" ? "0" : select_Staff.SelectValue));
        }
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null) cbx.Checked = cbx_CheckAll.Checked;
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
}
