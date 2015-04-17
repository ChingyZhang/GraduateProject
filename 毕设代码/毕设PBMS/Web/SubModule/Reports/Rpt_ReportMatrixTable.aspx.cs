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

public partial class SubModule_Reports_Rpt_ReportMatrixTable : System.Web.UI.Page
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

            BindGrid_ColumnGroup();
            BindGrid_RowGroup();
            BindGrid_ValueGroup();
        }
    }

    #region 绑定下拉列表框
    private void BindDataSetFields()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_DataSetFields> fields = new Rpt_DataSetBLL((Guid)ViewState["DataSet"]).GetFields();

        foreach (Rpt_ReportColumnGroups col in report.GetColumnGroups())
        {
            Rpt_DataSetFields f = fields.FirstOrDefault(p => p.ID == col.DataSetField);
            if (f != null) fields.Remove(f);
        }

        foreach (Rpt_ReportRowGroups col in report.GetRowGroups())
        {
            Rpt_DataSetFields f = fields.FirstOrDefault(p => p.ID == col.DataSetField);
            if (f != null) fields.Remove(f);
        }

        foreach (Rpt_ReportValueGroups col in report.GetValueGroups())
        {
            Rpt_DataSetFields f = fields.FirstOrDefault(p => p.ID == col.DataSetField);
            if (f != null) fields.Remove(f);
        }

        cbxl_Fields.DataSource = fields;
        cbxl_Fields.DataBind();

    }
    #endregion

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

    #region 列组操作
    private void BindGrid_ColumnGroup()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        gv_List_ColumnGroup.BindGrid(report.GetColumnGroups());
    }
    private void SaveGrid_ColumnGroup()
    {
        foreach (GridViewRow row in gv_List_ColumnGroup.Rows)
        {
            Guid id = (Guid)gv_List_ColumnGroup.DataKeys[row.RowIndex][0];

            Rpt_ReportColumnGroupsBLL column = new Rpt_ReportColumnGroupsBLL(id);

            column.Model.DisplayName = ((TextBox)row.FindControl("tbx_DisplayName")).Text;
            column.Model.AddSummary = ((RadioButtonList)row.FindControl("rbl_AddSummary")).SelectedValue;
            column.Update();
        }
    }
    protected void gv_List_ColumnGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid id = (Guid)gv_List_ColumnGroup.DataKeys[e.Row.RowIndex][0];

            Rpt_ReportColumnGroups c = new Rpt_ReportColumnGroupsBLL(id).Model;

            if (c != null)
            {
                RadioButtonList rbl_AddSummary = (RadioButtonList)e.Row.FindControl("rbl_AddSummary");
                rbl_AddSummary.SelectedValue = c.AddSummary == "Y" ? "Y" : "N";
            }

            if (e.Row.RowIndex == 0)
                e.Row.FindControl("bt_Decrease_ColumnGroup").Visible = false;
            
            if (e.Row.RowIndex == new Rpt_ReportBLL((Guid)ViewState["ID"]).GetColumnGroups().Count - 1)
                e.Row.FindControl("bt_Increase_ColumnGroup").Visible = false;
        }
    }

    protected void bt_Increase_ColumnGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ColumnGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex < gv_List_ColumnGroup.Rows.Count - 1)
        {
            Guid id = (Guid)gv_List_ColumnGroup.DataKeys[rowindex][0];

            Rpt_ReportColumnGroupsBLL bll = new Rpt_ReportColumnGroupsBLL(id);
            bll.Model.GroupSortID++;
            bll.Update();


            id = (Guid)gv_List_ColumnGroup.DataKeys[rowindex + 1][0];
            bll = new Rpt_ReportColumnGroupsBLL(id);
            if (bll.Model.GroupSortID > 0) bll.Model.GroupSortID--;
            bll.Update();
        }
        BindGrid_ColumnGroup();
    }
    protected void bt_Decrease_ColumnGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ColumnGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex > 0)
        {
            Guid id = (Guid)gv_List_ColumnGroup.DataKeys[rowindex][0];
            Rpt_ReportColumnGroupsBLL bll = new Rpt_ReportColumnGroupsBLL(id);
            if (bll.Model.GroupSortID > 0) bll.Model.GroupSortID--;
            bll.Update();


            id = (Guid)gv_List_ColumnGroup.DataKeys[rowindex - 1][0];
            bll = new Rpt_ReportColumnGroupsBLL(id);
            bll.Model.GroupSortID++;
            bll.Update();
        }

        BindGrid_ColumnGroup();
    }


    protected void gv_List_ColumnGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveGrid_ColumnGroup();
        Guid id = (Guid)gv_List_ColumnGroup.DataKeys[e.RowIndex][0];

        new Rpt_ReportColumnGroupsBLL(id).Delete();

        BindDataSetFields();
        BindGrid_ColumnGroup();
    }

    protected void bt_Add_ColumnGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ColumnGroup();

        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_ReportColumnGroups> columns = report.GetColumnGroups();

        int maxsortid = 0;
        if (columns.Count > 0) maxsortid = columns.Max(p => p.GroupSortID);

        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected && columns.FirstOrDefault(p => p.DataSetField == new Guid(item.Value)) == null)
            {
                maxsortid++;

                Rpt_ReportColumnGroupsBLL column = new Rpt_ReportColumnGroupsBLL();

                column.Model.Report = (Guid)ViewState["ID"];
                column.Model.DataSetField = new Guid(item.Value);
                column.Model.DisplayName = item.Text;
                column.Model.GroupSortID = maxsortid;
                column.Model.AddSummary = "N";
                column.Add();
            }
        }
        BindDataSetFields();
        BindGrid_ColumnGroup();
    }
    #endregion

    #region 行组操作
    private void BindGrid_RowGroup()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        gv_List_RowGroup.BindGrid(report.GetRowGroups());
    }
    private void SaveGrid_RowGroup()
    {
        foreach (GridViewRow row in gv_List_RowGroup.Rows)
        {
            Guid id = (Guid)gv_List_RowGroup.DataKeys[row.RowIndex][0];

            Rpt_ReportRowGroupsBLL column = new Rpt_ReportRowGroupsBLL(id);

            column.Model.DisplayName = ((TextBox)row.FindControl("tbx_DisplayName")).Text;
            column.Model.AddSummary = ((RadioButtonList)row.FindControl("rbl_AddSummary")).SelectedValue;
            column.Update();
        }
    }
    protected void gv_List_RowGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid id = (Guid)gv_List_RowGroup.DataKeys[e.Row.RowIndex][0];

            Rpt_ReportRowGroups c = new Rpt_ReportRowGroupsBLL(id).Model;

            if (c != null)
            {
                RadioButtonList rbl_AddSummary = (RadioButtonList)e.Row.FindControl("rbl_AddSummary");
                rbl_AddSummary.SelectedValue = c.AddSummary == "Y" ? "Y" : "N";
            }

            if (e.Row.RowIndex == 0)
                e.Row.FindControl("bt_Decrease_RowGroup").Visible = false;

            if (e.Row.RowIndex == new Rpt_ReportBLL((Guid)ViewState["ID"]).GetRowGroups().Count - 1)
                e.Row.FindControl("bt_Increase_RowGroup").Visible = false;
        }
    }

    protected void bt_Increase_RowGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_RowGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex < gv_List_RowGroup.Rows.Count - 1)
        {
            Guid id = (Guid)gv_List_RowGroup.DataKeys[rowindex][0];

            Rpt_ReportRowGroupsBLL bll = new Rpt_ReportRowGroupsBLL(id);
            bll.Model.GroupSortID++;
            bll.Update();


            id = (Guid)gv_List_RowGroup.DataKeys[rowindex + 1][0];
            bll = new Rpt_ReportRowGroupsBLL(id);
            if (bll.Model.GroupSortID > 0) bll.Model.GroupSortID--;
            bll.Update();
        }
        BindGrid_RowGroup();
    }
    protected void bt_Decrease_RowGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_RowGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex > 0)
        {
            Guid id = (Guid)gv_List_RowGroup.DataKeys[rowindex][0];
            Rpt_ReportRowGroupsBLL bll = new Rpt_ReportRowGroupsBLL(id);
            if (bll.Model.GroupSortID > 0) bll.Model.GroupSortID--;
            bll.Update();


            id = (Guid)gv_List_RowGroup.DataKeys[rowindex - 1][0];
            bll = new Rpt_ReportRowGroupsBLL(id);
            bll.Model.GroupSortID++;
            bll.Update();
        }

        BindGrid_RowGroup();
    }

    protected void gv_List_RowGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveGrid_RowGroup();
        Guid id = (Guid)gv_List_RowGroup.DataKeys[e.RowIndex][0];
        new Rpt_ReportRowGroupsBLL(id).Delete();

        BindDataSetFields();
        BindGrid_RowGroup();
    }

    protected void bt_Add_RowGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_RowGroup();

        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_ReportRowGroups> columns = report.GetRowGroups();

        int maxsortid = 0;
        if (columns.Count > 0) maxsortid = columns.Max(p => p.GroupSortID);

        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected && columns.FirstOrDefault(p => p.DataSetField == new Guid(item.Value)) == null)
            {
                maxsortid++;

                Rpt_ReportRowGroupsBLL column = new Rpt_ReportRowGroupsBLL();

                column.Model.Report = (Guid)ViewState["ID"];
                column.Model.DataSetField = new Guid(item.Value);
                column.Model.DisplayName = item.Text;
                column.Model.GroupSortID = maxsortid;
                column.Model.AddSummary = "N";
                column.Add();
            }
        }
        BindDataSetFields();
        BindGrid_RowGroup();
    }
    #endregion

    #region 值组操作
    private void BindGrid_ValueGroup()
    {
        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        gv_List_ValueGroup.BindGrid(report.GetValueGroups());
    }
    private void SaveGrid_ValueGroup()
    {
        foreach (GridViewRow row in gv_List_ValueGroup.Rows)
        {
            Guid id = (Guid)gv_List_ValueGroup.DataKeys[row.RowIndex][0];

            Rpt_ReportValueGroupsBLL column = new Rpt_ReportValueGroupsBLL(id);

            column.Model.DisplayName = ((TextBox)row.FindControl("tbx_DisplayName")).Text;
            column.Model.StatisticMode = int.Parse(((DropDownList)row.FindControl("ddl_StatisticMode")).SelectedValue);
            column.Update();
        }
    }
    protected void gv_List_ValueGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Guid id = (Guid)gv_List_ValueGroup.DataKeys[e.Row.RowIndex][0];

            Rpt_ReportValueGroups c = new Rpt_ReportValueGroupsBLL(id).Model;

            if (c != null)
            {
                DropDownList ddl_StatisticMode = (DropDownList)e.Row.FindControl("ddl_StatisticMode");
                ddl_StatisticMode.DataSource = DictionaryBLL.GetDicCollections("RPT_ValueStatisticMode");
                ddl_StatisticMode.DataBind();
                ddl_StatisticMode.SelectedValue = c.StatisticMode.ToString();
            }

            if (e.Row.RowIndex == 0)
                e.Row.FindControl("bt_Decrease_ValueGroup").Visible = false;

            if (e.Row.RowIndex == new Rpt_ReportBLL((Guid)ViewState["ID"]).GetValueGroups().Count - 1)
                e.Row.FindControl("bt_Increase_ValueGroup").Visible = false;
        }
    }

    protected void bt_Increase_ValueGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ValueGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex < gv_List_ValueGroup.Rows.Count - 1)
        {
            Guid id = (Guid)gv_List_ValueGroup.DataKeys[rowindex][0];

            Rpt_ReportValueGroupsBLL bll = new Rpt_ReportValueGroupsBLL(id);
            bll.Model.ValueSortID++;
            bll.Update();


            id = (Guid)gv_List_ValueGroup.DataKeys[rowindex + 1][0];
            bll = new Rpt_ReportValueGroupsBLL(id);
            if (bll.Model.ValueSortID > 0) bll.Model.ValueSortID--;
            bll.Update();
        }
        BindGrid_ValueGroup();
    }
    protected void bt_Decrease_ValueGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ValueGroup();

        int rowindex = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
        if (rowindex > 0)
        {
            Guid id = (Guid)gv_List_ValueGroup.DataKeys[rowindex][0];
            Rpt_ReportValueGroupsBLL bll = new Rpt_ReportValueGroupsBLL(id);
            if (bll.Model.ValueSortID > 0) bll.Model.ValueSortID--;
            bll.Update();


            id = (Guid)gv_List_ValueGroup.DataKeys[rowindex - 1][0];
            bll = new Rpt_ReportValueGroupsBLL(id);
            bll.Model.ValueSortID++;
            bll.Update();
        }

        BindGrid_ValueGroup();
    }
    protected void gv_List_ValueGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SaveGrid_ValueGroup();

        Guid id = (Guid)gv_List_ValueGroup.DataKeys[e.RowIndex][0];

        new Rpt_ReportValueGroupsBLL(id).Delete();

        BindDataSetFields();
        BindGrid_ValueGroup();
    }
    protected void bt_Add_ValueGroup_Click(object sender, EventArgs e)
    {
        SaveGrid_ValueGroup();

        Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        IList<Rpt_ReportValueGroups> columns = report.GetValueGroups();

        int maxsortid = 0;
        if (columns.Count > 0) maxsortid = columns.Max(p => p.ValueSortID);

        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected && columns.FirstOrDefault(p => p.DataSetField == new Guid(item.Value)) == null)
            {
                maxsortid++;

                Rpt_ReportValueGroupsBLL column = new Rpt_ReportValueGroupsBLL();

                column.Model.Report = (Guid)ViewState["ID"];
                column.Model.DataSetField = new Guid(item.Value);
                column.Model.DisplayName = item.Text;
                column.Model.ValueSortID = maxsortid;
                column.Model.StatisticMode = 1;
                column.Add();
            }
        }
        BindDataSetFields();
        BindGrid_ValueGroup();
    }
    #endregion



    protected void bt_Save_Click(object sender, EventArgs e)
    {
        SaveGrid_ColumnGroup();
        SaveGrid_RowGroup();
        SaveGrid_ValueGroup();
        MessageBox.ShowAndRedirect(this, "保存成功!", "Rpt_ReportMatrixTable.aspx?ID=" + ViewState["ID"].ToString());
    }
}
