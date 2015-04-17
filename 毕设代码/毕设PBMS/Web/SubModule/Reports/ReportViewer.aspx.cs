using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.RPT;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.RPT;

public partial class SubModule_Reports_ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Report"] = Request.QueryString["Report"] != null ? new Guid(Request.QueryString["Report"]) : Guid.Empty;
            ViewState["LoadCache"] = Request.QueryString["LoadCache"] != null ? Request.QueryString["LoadCache"] != "false" : true;
            #endregion

            if ((Guid)ViewState["Report"] == Guid.Empty)
            {
                Response.Redirect("Rpt_ReportList.aspx");
            }

            #region 判断有无浏览权限
            Rpt_Report rpt = new Rpt_ReportBLL((Guid)ViewState["Report"]).Model;
            if (rpt == null) Response.Redirect("Rpt_ReportList.aspx");

            IList<Rpt_FolderRight> rights = Rpt_FolderRightBLL.GetAssignedRightByUser(Session["UserName"].ToString()).Where(p => p.Folder == rpt.Folder).ToList();
            if (rights.Count == 0)
            {
                MessageBox.ShowAndRedirect(this, "对不起，您没有权限浏览该报表", "Rpt_ReportList.aspx");
                return;
            }
            #endregion

            //增加浏览记录
            Rpt_ReportBLL.AddViewTimes(new Guid(ViewState["Report"].ToString()), (int)Session["UserID"]);
        }
    }

    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        BindGrid(false, (bool)ViewState["LoadCache"]);
    }

    private void BindGrid(bool IsExport, bool LoadFromCache)
    {
        Guid reportid = new Guid(ViewState["Report"].ToString());

        Rpt_ReportBLL report = new Rpt_ReportBLL(reportid);
       
        lb_ReportTitle.Text = report.Model.Title;

        Dictionary<string, object> param;
        if (!pl_Param.GetParamsValue(out param))
        {
            MessageBox.Show(this, "请正确设定必填参数!");
            return;
        }

        if (report.LoadData(param, LoadFromCache))
        {
            if (!IsExport)
            {
                #region 绑定图表
                Chart1.Series.Clear();
                Chart1.ChartAreas.Clear();

                DataTable dt_chart = report.GetReportData();

                if (report.GetCharts().Count == 0)
                    Chart1.Visible = false;
                else
                {
                    Chart1.Legends.Clear();
                    Chart1.Series.Clear();
                    Chart1.ChartAreas.Clear();
                    int chartindex = 0;
                    foreach (Rpt_ReportCharts chart in report.GetCharts())
                    {
                        chartindex++;
                        ChartArea chartarea = new ChartArea("ChartArea" + chartindex.ToString());
                        Chart1.ChartAreas.Add(chartarea);

                        chartarea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                        chartarea.AxisX.MajorGrid.Interval = 1;
                        chartarea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                        chartarea.AxisX.MajorGrid.LineColor = Color.LightGray;
                        chartarea.AxisY.MajorGrid.LineColor = Color.LightGray;

                        chartarea.AxisX.LabelStyle.Angle = 0;
                        chartarea.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.IncreaseFont;
                        chartarea.AxisX.IsLabelAutoFit = true;
                        //chartarea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Blue;

                        chartarea.AxisX.LabelAutoFitMaxFontSize = 8;
                        chartarea.AxisX.LabelAutoFitMinFontSize = 5;
                        chartarea.AxisY.LabelAutoFitMaxFontSize = 8;
                        chartarea.AxisY.LabelAutoFitMinFontSize = 5;

                        //3D显示
                        if (chart["Enable3D"] == "Y") chartarea.Area3DStyle.Enable3D = true;

                        #region 第二轴坐标
                        if (chart["AxisX2Enabled"] == "Y")
                        {
                            chartarea.AxisX2.Enabled = AxisEnabled.True;
                            chartarea.AxisX2.LabelAutoFitStyle = LabelAutoFitStyles.IncreaseFont;
                            chartarea.AxisX2.LabelAutoFitMaxFontSize = 8;
                            chartarea.AxisX2.LabelAutoFitMinFontSize = 5;
                        }

                        if (chart["AxisY2Enabled"] == "Y")
                        {
                            chartarea.AxisY2.Enabled = AxisEnabled.True;
                            chartarea.AxisY2.LabelAutoFitStyle = LabelAutoFitStyles.IncreaseFont;
                            chartarea.AxisY2.LabelAutoFitMaxFontSize = 8;
                            chartarea.AxisY2.LabelAutoFitMinFontSize = 5;
                        }
                        #endregion



                        #region 处理图表系列
                        IList<string> list_series = new List<string>();
                        string[] series_array = chart.SeriesColumns.Split(new char[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);

                        if (report.Model.ReportType == 1)
                        {
                            //普通列表
                            for (int i = 0; i < series_array.Length; i++)
                            {
                                if (dt_chart.Columns.Contains(series_array[i])) list_series.Add(series_array[i]);
                            }
                        }

                        if (report.Model.ReportType == 2)
                        {
                            //矩阵表
                            IList<Rpt_ReportRowGroups> rowgroups = report.GetRowGroups();
                            IList<Rpt_ReportValueGroups> valuegroups = report.GetValueGroups();

                            for (int i = 0; i < series_array.Length; i++)
                            {
                                foreach (DataColumn column in dt_chart.Columns)
                                {
                                    if (rowgroups.FirstOrDefault(p => p.DisplayName == column.ColumnName) != null)
                                    {
                                        continue;
                                    }
                                    if (valuegroups.Count == 1)
                                        list_series.Add(column.ColumnName);
                                    else
                                    {
                                        if (column.ColumnName.EndsWith(series_array[i])) list_series.Add(column.ColumnName);
                                    }
                                }
                                chartarea.AxisY.Title += series_array[i] + "  ";
                                chartarea.AxisY.TitleFont = new Font("宋体", 9);
                                chartarea.AxisY.TitleForeColor = Color.Blue;
                            }
                        }

                        Legend legend = new Legend("Legend" + chartindex.ToString());
                        legend.Docking = Docking.Bottom;
                        legend.Alignment = StringAlignment.Center;
                        legend.Title = chartarea.AxisY.Title;
                        Chart1.Legends.Add(legend);

                        foreach (string seriesname in list_series)
                        {
                            Series series = new Series(seriesname);

                            string charttypedesc = DictionaryBLL.GetDicCollections("RPT_ReportChartType")[chart.ChartType.ToString()].Description;
                            series.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), charttypedesc, true);
                            series.ChartArea = chartarea.Name;
                            series.Legend = legend.Name;
                            series.YValueMembers = seriesname;
                            series.XValueMember = chart.AxisColumns;
                            series.ToolTip = seriesname;

                            if (series.ChartType == SeriesChartType.Spline || series.ChartType == SeriesChartType.Line)
                            {
                                series.BorderWidth = 3;
                                chartarea.AxisX.IsMarginVisible = false;
                            }

                            if (chart["IsValueShownAsLabel"] == "Y") series.IsValueShownAsLabel = true;        //显示值标签

                            if (DictionaryBLL.GetDicCollections("PRT_ChartDrawingStyle").ContainsKey(chart["DrawingStyle"]))
                            {
                                Dictionary_Data dic = DictionaryBLL.GetDicCollections("PRT_ChartDrawingStyle")[chart["DrawingStyle"]];
                                series.CustomProperties = "DrawingStyle=" + dic.Description;
                            }

                            Chart1.Series.Add(series);
                        }
                        #endregion

                        #region 设置横向轴分组式Label
                        IList<Rpt_ReportRowGroups> RowGroups = report.GetRowGroups();
                        for (int rowgroupindex = 0; rowgroupindex < RowGroups.Count; rowgroupindex++)
                        {
                            int startposition = 0;
                            for (int i = 0; i < dt_chart.Rows.Count; i++)
                            {
                                if (i == dt_chart.Rows.Count - 1 || dt_chart.Rows[i][RowGroups[rowgroupindex].DisplayName].ToString() !=
                                    dt_chart.Rows[i + 1][RowGroups[rowgroupindex].DisplayName].ToString())
                                {
                                    chartarea.AxisX.CustomLabels.Add(startposition + 0.5, i + 1.4, dt_chart.Rows[i][RowGroups[rowgroupindex].DisplayName].ToString(), RowGroups.Count - rowgroupindex - 1, LabelMarkStyle.LineSideMark);
                                    startposition = i + 1;
                                }
                            }
                        }
                        #endregion

                    }

                    Chart1.Height = new Unit(500 * Chart1.ChartAreas.Count);
                    Chart1.DataSource = dt_chart;
                    Chart1.DataBind();
                }
                #endregion
            }

            #region 绑定GridView
            DataTable dt_gridview = report.GetReportDataWithSummary();

            if (report.Model.ReportType == 1)
            {
                GridView1.AllowPaging = !IsExport;
                GridView1.PageSize = 50;
                GridView1.DataSource = dt_gridview;
                GridView1.DataBind();
            }
            else if (report.Model.ReportType == 2)
            {
                GridView1.AllowPaging = !IsExport;
                GridView1.PageSize = 100;
                GridView1.DataSource = dt_gridview;
                GridView1.DataBind();
                GridViewMatric(GridView1);
            }

            lb_PageInfo.Visible = false;
            bt_PrePage.Visible = false;
            bt_NextPage.Visible = false;

            if (GridView1.AllowPaging)
            {
                lb_PageInfo.Text = string.Format("每页<b><font color=red>{0}</font></b>条/共<b><font color=red>{1}</font></b>条 第<b><font color=red>{2}</font></b>页/共<b><font color=red>{3}</font></b>页",
                    GridView1.PageSize > dt_gridview.Rows.Count ? dt_gridview.Rows.Count : GridView1.PageSize,
                    dt_gridview.Rows.Count, GridView1.PageIndex + 1, GridView1.PageCount);
                lb_PageInfo.Visible = true;
                if (GridView1.PageIndex > 0) bt_PrePage.Visible = true;
                if (GridView1.PageIndex < GridView1.PageCount - 1) bt_NextPage.Visible = true;
            }
            if (dt_gridview.Columns.Count >= 22)
                GridView1.Width = new Unit(dt_gridview.Columns.Count * 65);
            else
                GridView1.Width = new Unit(100, UnitType.Percentage);

            if (!IsExport)
            {
                int rowindex = 0;
                foreach (Rpt_ReportRowGroups rowgroup in report.GetRowGroups())
                {
                    if (dt_gridview.Columns.Contains(rowgroup.DisplayName))
                    {
                        GridViewMergSampeValueRow(GridView1, rowindex);
                        rowindex++;
                    }
                }
            }
            #endregion

            if (report.DataCacheTime <= DateTime.Now.AddMinutes(-1))
            {
                bt_ClearDataCache.Visible = true;
                lb_DataSetCacheTime.Visible = true;
                lb_DataSetCacheTime.Text = " 数据源来自历史快照，获取时间:" + report.DataCacheTime.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                bt_ClearDataCache.Visible = false;
                lb_DataSetCacheTime.Visible = false;
            }
        }
        else
        {
            MessageBox.Show(this, "未能检索到数据!");
        }

    }

    #region 调整GridView控件的显示格式
    /// <summary>
    /// 将指定的GridView的列标题行矩阵化,配合MatrixTable使用
    /// </summary>
    /// <param name="gv"></param>
    private void GridViewMatric(GridView gv)
    {
        GridViewRow row = gv.HeaderRow;
        if (row == null) return;
        if (row.Cells.Count == 0) return;

        #region 将列中分隔符转为多行表头
        //求出有几个组
        int groups = 1;
        foreach (TableCell c in row.Cells)
        {
            groups = c.Text.Split('→').Length;
            if (groups > 1) break;
        }
        if (groups == 1) return;

        string[] columnname = new string[row.Cells.Count];
        for (int i = 0; i < columnname.Length; i++)
        {
            if (row.Cells[i].Visible)
                columnname[i] = row.Cells[i].Text;
            else
                columnname[i] = "";
        }

        TableCellCollection dtc = row.Cells;
        dtc.Clear();

        for (int gi = 0; gi <= groups - 1; gi++)
        {
            string groupname = "";
            for (int i = 0; i < columnname.Length; i++)
            {

                string s = columnname[i];
                if (s.IndexOf('→') == -1)
                {
                    if (s != "")
                    {
                        TableHeaderCell c = new TableHeaderCell();
                        c.Text = s;
                        c.RowSpan = groups - gi;
                        dtc.Add(c);
                        columnname[i] = "";
                    }
                }
                else
                {
                    if (s.Substring(0, s.IndexOf('→')) != groupname)
                    {
                        TableHeaderCell c = new TableHeaderCell();
                        c.Text = s.Substring(0, s.IndexOf('→'));

                        dtc.Add(c);

                        groupname = s.Substring(0, s.IndexOf('→'));
                        columnname[i] = s.Substring(s.IndexOf('→') + 1);
                    }
                    else
                    {
                        if (dtc[dtc.Count - 1].ColumnSpan == 0) dtc[dtc.Count - 1].ColumnSpan = 1;
                        dtc[dtc.Count - 1].ColumnSpan += 1;
                        columnname[i] = s.Substring(s.IndexOf('→') + 1);
                    }
                }
            }
            if (gi != groups - 1)
                dtc[dtc.Count - 1].Text += "</th></tr><tr>";
        }
        #endregion
    }

    private void GridViewMergSampeValueRow(GridView gv, int ColumnIndex)
    {
        GridViewMergSampeValueRow(gv, ColumnIndex, "", 0, "");
    }
    /// <summary>
    /// 将指定的GridView中指定列中相邻行相同值的单元格合并显示
    /// </summary>
    /// <param name="gv"></param>
    /// <param name="ColumnIndex">合并显示的列序</param>
    private void GridViewMergSampeValueRow(GridView gv, int ColumnIndex, string BackColor, int BorderWidth, string BorderColor)
    {
        if (gv.Rows.Count == 0) return;
        #region 合并行组中相邻行相同值
        int rowspan = 1;
        for (int i = gv.Rows.Count - 1; i > 0; i--)
        {
            if (BackColor != "")
                gv.Rows[i].Cells[ColumnIndex].Style.Add("background-color", BackColor);
            else
                gv.Rows[i].Cells[ColumnIndex].Style.Add("background-color", "#ffffff");

            if (ColumnIsSampe(gv, i, ColumnIndex))
            {
                gv.Rows[i].Cells[ColumnIndex].Visible = false;
                rowspan++;
            }
            else
            {
                gv.Rows[i].Cells[ColumnIndex].RowSpan = rowspan;

                if (BorderWidth > 0)
                {
                    gv.Rows[i].Cells[ColumnIndex].Style.Add("border-top-style", "solid");
                    gv.Rows[i].Cells[ColumnIndex].Style.Add("border-top-width", BorderWidth.ToString() + "px");
                    if (BorderColor != "") gv.Rows[i].Cells[ColumnIndex].Style.Add("border-color", BorderColor);
                }
                rowspan = 1;
            }
        }
        gv.Rows[0].Cells[ColumnIndex].RowSpan = rowspan;

        if (BackColor != "")
            gv.Rows[0].Cells[ColumnIndex].Style.Add("background-color", BackColor);
        else
            gv.Rows[0].Cells[ColumnIndex].Style.Add("background-color", "#ffffff");

        if (BorderWidth > 0)
        {
            gv.Rows[0].Cells[ColumnIndex].Style.Add("border-top-style", "solid");
            gv.Rows[0].Cells[ColumnIndex].Style.Add("border-top-width", BorderWidth.ToString() + "px");
            if (BorderColor != "") gv.Rows[0].Cells[ColumnIndex].Style.Add("border-color", BorderColor);
        }
        #endregion
    }

    private static bool ColumnIsSampe(GridView gv, int row, int column)
    {
        if (gv.Rows[row].Cells[column].Text.Trim() == "&nbsp;") return false;

        for (int i = column; i >= 0; i--)
        {
            if (gv.Rows[row].Cells[i].Text != gv.Rows[row - 1].Cells[i].Text) return false;
        }

        return true;
    }
    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        MessageBox.Show(this, e.NewPageIndex.ToString());
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid(false, true);
    }
    protected void bt_ClearDataCache_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        BindGrid(false, false);
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        BindGrid(true, false);

        string filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(lb_ReportTitle.Text.Trim() == "" ? "Export" : lb_ReportTitle.Text.Trim()));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        GridView1.RenderControl(hw);

        try
        {
            string tmp0 = "<tr align=\"left\" valign=\"middle\" OnMouseOver=\"this.style.cursor='hand';this.originalcolor=this.style.backgroundColor;this.style.backgroundColor='#FFCC66';\" OnMouseOut=\"this.style.backgroundColor=this.originalcolor;\" style=\"height:18px;\">";
            string tmp1 = "<tr align=\"left\" valign=\"middle\" OnMouseOver=\"this.style.cursor='hand';this.originalcolor=this.style.backgroundColor;this.style.backgroundColor='#FFCC66';\" OnMouseOut=\"this.style.backgroundColor=this.originalcolor;\" style=\"background-color:White;height:18px;\">";
            StringBuilder outhtml = new StringBuilder(tw.ToString());
            outhtml = outhtml.Replace(tmp0, "<tr>");
            outhtml = outhtml.Replace(tmp1, "<tr>");
            outhtml = outhtml.Replace("&nbsp;", "");

            Response.Write(outhtml.ToString());
        }
        catch
        {
            Response.Write(tw.ToString());
        }
        Response.End();

        BindGrid(false, false);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void bt_ExprotList_Click(object sender, EventArgs e)
    {
        string filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(lb_ReportTitle.Text.Trim() == "" ? "Export" : lb_ReportTitle.Text.Trim()));

        Guid reportid = new Guid(ViewState["Report"].ToString());

        Rpt_ReportBLL report = new Rpt_ReportBLL(reportid);

        lb_ReportTitle.Text = report.Model.Title;

        Dictionary<string, object> param;
        if (!pl_Param.GetParamsValue(out param))
        {
            MessageBox.Show(this, "请正确设定必填参数!");
            return;
        }

        if (report.LoadData(param, true))
        {
            CreateExcel(report.GetReportData(), filename);
        }
    }
    /// <summary>
    /// 由DataTable导出Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fileName"></param>
    private void CreateExcel(DataTable dt, string fileName)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.Charset = "UTF-8";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        string colHeaders = "", ls_item = "";

        ////定义表对象与行对象，同时用DataSet对其值进行初始化
        //DataTable dt = ds.Tables[0];
        DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
        int i = 0;
        int cl = dt.Columns.Count;

        //取得数据表各列标题，各标题之间以t分割，最后一个列标题后加回车符
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString() + "\t";
            }

        }
        resp.Write(colHeaders);
        //向HTTP输出流中写入取得的数据信息

        //逐行处理数据 
        foreach (DataRow row in myRow)
        {
            //当前行数据写入HTTP输出流，并且置空ls_item以便下行数据   
            for (i = 0; i < cl; i++)
            {
                string content = row[i].ToString();
                if (content.Contains('\t')) content = content.Replace('\t', ' ');

                long l = 0;
                if (content.Length >= 8 && long.TryParse(content, out l))
                    content = "'" + content;

                ls_item += content;

                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += "\r\n";
                }
                else
                {
                    ls_item += "\t";
                }

            }
            resp.Write(ls_item);
            ls_item = "";

        }
        resp.End();
    }
    protected void bt_PrePage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex--;
            BindGrid(false, true);
        }
    }
    protected void bt_NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex++;
            BindGrid(false, true);
        }
    }
}
