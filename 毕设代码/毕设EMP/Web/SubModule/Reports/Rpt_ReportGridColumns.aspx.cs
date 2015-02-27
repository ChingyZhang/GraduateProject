using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.RPT;
using MCSFramework.BLL.RPT;
using MCSFramework.BLL;
using MCSFramework.Common;

public partial class SubModule_Reports_Rpt_ReportGridColumns : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion

            if ((Guid)ViewState["ID"] == Guid.Empty) Response.Redirect("Rpt_ReportList.aspx");


            Rpt_Report m = new Rpt_ReportBLL((Guid)ViewState["ID"]).Model;
            if (m != null)
            {
                #region 根据报表类型控制Tab可见
                switch (m.ReportType)
                {
                    case 1:
                        MCSTabControl1.Items[2].Visible = false;
                        break;
                    case 2:
                        MCSTabControl1.Items[1].Visible = false;
                        break;
                }
                #endregion

                ViewState["DataSet"] = m.DataSet;
            }

            BindDataSetFields();

            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDataSetFields()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_ReportGridColumns> columns = report.GetGridColumns();

        IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).GetFields();

        foreach (Rpt_ReportGridColumns col in columns)
        {
            Rpt_DataSetFields f = fields.FirstOrDefault(p => p.ID == col.DataSetField);
            if (f != null) fields.Remove(f);
        }

        cbxl_Fields.DataSource = fields;
        cbxl_Fields.DataBind();
    }
    #endregion

    private void BindGrid()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);

        gv_List.BindGrid(report.GetGridColumns());
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0":
                Response.Redirect("Rpt_ReportDetail.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "1":
                Response.Redirect("Rpt_ReportGridColumns.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "2":
                Response.Redirect("Rpt_ReportMatrixTable.aspx?ID=" + ViewState["ID"].ToString());
                break;
            case "3":
                Response.Redirect("Rpt_ReportCharts.aspx?ID=" + ViewState["ID"].ToString());
                break;
            default:
                break;
        }
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid id = (Guid)gv_List.DataKeys[e.Row.RowIndex][0];

            Rpt_ReportGridColumns c = new Rpt_ReportGridColumnsBLL(id).Model;

            if (c != null)
            {
                RadioButtonList rbl_Visible = (RadioButtonList)e.Row.FindControl("rbl_Visible");
                rbl_Visible.SelectedValue = c.Visible == "Y" ? "Y" : "N";

                RadioButtonList rbl_AddSummary = (RadioButtonList)e.Row.FindControl("rbl_AddSummary");
                rbl_AddSummary.SelectedValue = c.AddSummary == "Y" ? "Y" : "N";
            }
        }
    }

    protected void bt_Increase_Click(object sender, EventArgs e)
    {
        SaveGrid();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex < gv_List.Rows.Count - 1)
        {
            Guid id = (Guid)gv_List.DataKeys[rowindex][0];

            Rpt_ReportGridColumnsBLL bll = new Rpt_ReportGridColumnsBLL(id);
            bll.Model.ColumnSortID++;
            bll.Update();


            id = (Guid)gv_List.DataKeys[rowindex + 1][0];
            bll = new Rpt_ReportGridColumnsBLL(id);
            if (bll.Model.ColumnSortID > 0) bll.Model.ColumnSortID--;
            bll.Update();
        }
        BindGrid();
    }
    protected void bt_Decrease_Click(object sender, EventArgs e)
    {
        SaveGrid();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex > 0)
        {
            Guid id = (Guid)gv_List.DataKeys[rowindex][0];
            Rpt_ReportGridColumnsBLL bll = new Rpt_ReportGridColumnsBLL(id);
            if (bll.Model.ColumnSortID > 0) bll.Model.ColumnSortID--;
            bll.Update();


            id = (Guid)gv_List.DataKeys[rowindex - 1][0];
            bll = new Rpt_ReportGridColumnsBLL(id);
            bll.Model.ColumnSortID++;
            bll.Update();
        }

        BindGrid();
    }

    protected void cbx_SelectAllFields_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbxl_Fields.Items)
        {
            item.Selected = cbx_SelectAllFields.Checked;
        }
    }
    protected void cbx_SelectAllColumn_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            cbx.Checked = cbx_SelectAllColumn.Checked;
        }
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        SaveGrid();

        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_ReportGridColumns> columns = report.GetGridColumns();

        int maxsortid = 0;
        if (columns.Count > 0) maxsortid = columns.Max(p => p.ColumnSortID);

        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected && columns.FirstOrDefault(p => p.DataSetField == new Guid(item.Value)) == null)
            {
                maxsortid++;

                Rpt_ReportGridColumnsBLL column = new Rpt_ReportGridColumnsBLL();

                column.Model.Report = (Guid)ViewState["ID"];
                column.Model.DataSetField = new Guid(item.Value);
                column.Model.DisplayName = item.Text;
                column.Model.ColumnSortID = maxsortid;
                column.Model.AddSummary = "N";
                column.Model.Visible = "Y";
                column.Add();
            }
        }
        BindDataSetFields();
        BindGrid();
    }

    protected void bt_DeleteColumn_Click(object sender, EventArgs e)
    {
        SaveGrid();

        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx.Checked)
            {
                Guid id = (Guid)gv_List.DataKeys[row.RowIndex][0];

                new Rpt_ReportGridColumnsBLL(id).Delete();
            }
        }

        BindDataSetFields();
        BindGrid();
    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SaveGrid();

        BindGrid();
    }

    private void SaveGrid()
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            Guid id = (Guid)gv_List.DataKeys[row.RowIndex][0];

            Rpt_ReportGridColumnsBLL column = new Rpt_ReportGridColumnsBLL(id);

            column.Model.DisplayName = ((TextBox)row.FindControl("tbx_DisplayName")).Text;
            column.Model.Visible = ((RadioButtonList)row.FindControl("rbl_Visible")).SelectedValue;
            column.Model.AddSummary = ((RadioButtonList)row.FindControl("rbl_AddSummary")).SelectedValue;
            column.Update();
        }
    }
}
