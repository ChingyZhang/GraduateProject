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
//NPOI命名空间
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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

    /// <summary>
    /// 绑定GridView控件
    /// </summary>
    /// <param name="IsExport">为true则不允许分页，为false允许分页</param>
    /// <param name="LoadFromCache">是否从缓存中加载</param>
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
                foreach (DataColumn dc in dt_gridview.Columns)
                {
                    if (dc.ColumnName.IndexOf("→") > 0)
                    {
                        GridViewMatric(GridView1);
                        break;
                    }
                }
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

            //if (!IsExport)
            //{
            //    int rowindex = 0;
            //    foreach (Rpt_ReportRowGroups rowgroup in report.GetRowGroups())
            //    {
            //        if (dt_gridview.Columns.Contains(rowgroup.DisplayName))
            //        {
            //            GridViewMergSampeValueRow(GridView1, rowindex);
            //            rowindex++;
            //        }
            //    }
            //}
            if (true)
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
            bt_SaveDataCacache.Visible = true;
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

            if (columnname[i] == "" && gv.Columns.Count > i)
            {
                columnname[i] = gv.Columns[i].HeaderText;
            }

            if (columnname[i].EndsWith("→")) columnname[i] += "&nbsp;";
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
        /*
         * NPOI导出代码，设置文件路径
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Download\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string filePath = path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
        */
        BindGrid(true, false);

        /*
         * NPOI导出代码，调用EXCEL文件生成方法
        OptimizaToExcel(GridView1, filePath);
        Directory.Delete(path);
        */

        string filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(lb_ReportTitle.Text.Trim() == "" ? "Export" : lb_ReportTitle.Text.Trim())) + DateTime.Now.ToString("yyyyMMddHHmmss");


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
        string filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(lb_ReportTitle.Text.Trim() == "" ? "Export" : lb_ReportTitle.Text.Trim())) + DateTime.Now.ToString("yyyyMMddHHmmss");

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
        resp.ContentEncoding = System.Text.Encoding.Default;
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
    protected void bt_SaveDataCacache_Click(object sender, EventArgs e)
    {

        Dictionary<string, object> param;
        if (!pl_Param.GetParamsValue(out param))
        {
            MessageBox.Show(this, "请正确设定必填参数!");
            return;
        }
        Guid reportid = new Guid(ViewState["Report"].ToString());

        Rpt_ReportBLL report = new Rpt_ReportBLL(reportid);
        report.SaveForever(param);
        MessageBox.Show(this, "保存成功");
    }
    
    #region 利用NPOI组件将GridView导出为Excel 2007（dll类库更新为NPOI 2.1.1）

    #region 模块外部调用方法

    /// <summary>
    /// 将GridView中的数据导出为Excel2007文件并下载
    /// </summary>
    /// <param name="gv">需要处理的GridView</param>
    /// <param name="fileName">Excel文件路径</param>
    public void OptimizaToExcel(GridView gv, string filePath)
    {
        IWorkbook hssfworkbook = GvToWorkBook(gv);
        try
        {
            FileStream fs = File.Create(filePath);//new FileStream(filePath, FileMode.Create);
            hssfworkbook.Write(fs);
            fs.Close();
        }
        catch (Exception e1)
        {
            MessageBox.Show(Page, "errorMessage=" + e1.Message + ";filePath=" + filePath);
        }
        this.DownLoadFile(filePath);
    }


    public void OptimizaToExcel(GridView gv)
    {
        IWorkbook hssfworkbook = GvToWorkBook(gv);
        MemoryStream memory = new MemoryStream();
        try
        {
            hssfworkbook.Write(memory);
            memory.Close();
        }
        catch (Exception e1)
        {
            MessageBox.Show(Page, "errorMessage=" + e1.Message);
        }

        Guid reportGuid = Request.QueryString["Report"] != null ? new Guid(Request.QueryString["Report"]) : Guid.Empty;
        //增加浏览记录
        string fileName = new Rpt_ReportBLL(reportGuid).Model.Name;
        this.DownLoadFile(memory,fileName);
    }
    #endregion

    #region 将GridView转为NPOI工作簿
    /// <summary>
    /// 将GridView转为NPOI工作簿
    /// </summary>
    /// <param name="gv">需要处理的GridView</param>
    /// <returns></returns>
    private IWorkbook GvToWorkBook(GridView gv)
    {
        List<int> colTypeList = GetColumnsType(gv);

        IWorkbook hssfworkbook = new XSSFWorkbook();
        ISheet sheet = hssfworkbook.CreateSheet("sheet1");

        ICellStyle cellStyle = GetCellStyleCommon(hssfworkbook);

        int colCount = 0;//记录GridView列数
        //rowInex记录表头的行数
        int rowIndex = AddSheetHeader(sheet, gv.HeaderRow, cellStyle, "</th></tr><tr>", out colCount);//添加表头
        AddSheetBody(sheet, gv, cellStyle, colTypeList, colCount, rowIndex);

        return hssfworkbook;
    }
    #endregion

    #region 添加表头
    /// <summary>
    /// 为Excel添加表头
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="headerRow">GridView的HeaderRow属性</param>
    /// <param name="headerCellStyle">表头格式</param>
    /// <param name="flagNewLine">转行标志</param>
    /// <param name="colCount">Excel表列数</param>
    /// <returns>Excel表格行数</returns>
    private int AddSheetHeader(ISheet sheet, GridViewRow headerRow, ICellStyle headerCellStyle, string flagNewLine, out int colCount)
    {
        //int 
        colCount = 0;//记录GridView列数
        int rowInex = 0;//记录表头的行数

        IRow row = sheet.CreateRow(0);
        ICell cell;

        int groupCount = 0;//记录分组数
        int colIndex = 0;//记录列索引，并于结束表头遍历后记录总列数
        for (int i = 0; i < headerRow.Cells.Count; i++)
        {
            if (rowInex != groupCount)//新增了标题行时重新创建
            {
                row = sheet.CreateRow(rowInex);
                groupCount = rowInex;
            }

            #region 是否跳过当前单元格

            for (int m = 0; m < sheet.NumMergedRegions; m++)//遍历所有合并区域
            {
                NPOI.SS.Util.CellRangeAddress a = sheet.GetMergedRegion(m);
                //当前单元格是处于合并区域内
                if (a.FirstColumn <= colIndex && a.LastColumn >= colIndex
                    && a.FirstRow <= rowInex && a.LastRow >= rowInex)
                {
                    colIndex++;
                    m = 0;//重新遍历所有合并区域判断新单元格是否位于合并区域
                }
            }


            #endregion

            cell = row.CreateCell(colIndex);
            cell.CellStyle = headerCellStyle;

            TableCell tablecell = headerRow.Cells[i];

            //跨列属性可能为添加了html属性colspan，也可能是由cell的ColumnSpan属性指定
            int colSpan = 0;
            int rowSpan = 0;

            #region 获取跨行跨列属性值
            //跨列
            if (!string.IsNullOrEmpty(tablecell.Attributes["colspan"]))
            {
                colSpan = int.Parse(tablecell.Attributes["colspan"].ToString());
                colSpan--;
            }
            if (tablecell.ColumnSpan > 1)
            {
                colSpan = tablecell.ColumnSpan;
                colSpan--;
            }

            //跨行
            if (!string.IsNullOrEmpty(tablecell.Attributes["rowSpan"]))
            {
                rowSpan = int.Parse(tablecell.Attributes["rowSpan"].ToString());
                rowSpan--;
            }
            if (tablecell.RowSpan > 1)
            {
                rowSpan = tablecell.RowSpan;
                rowSpan--;
            }
            #endregion

            //添加excel合并区域
            if (colSpan > 0 || rowSpan > 0)
            {
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowInex, rowInex + rowSpan, colIndex, colIndex + colSpan));
                colIndex += colSpan + 1;//重新设置列索引
            }
            else
            {
                colIndex++;
            }
            string strHeader = headerRow.Cells[i].Text;

            if (strHeader.Contains(flagNewLine))//换行标记，当只存在一行标题时不存在</th></tr><tr>，此时colCount无法被赋值
            {
                rowInex++;
                colCount = colIndex;
                colIndex = 0;

                strHeader = strHeader.Substring(0, strHeader.IndexOf("</th></tr><tr>"));
            }
            cell.SetCellValue(strHeader);
        }
        if (groupCount == 0)//只有一行标题时另外为colCount赋值
        {
            colCount = colIndex;
        }
        rowInex++;//表头结束后另起一行开始记录控件数据行索引

        return rowInex;
    }
    #endregion

    #region 添加表数据
    /// <summary>
    /// 为Excel添加数据
    /// </summary>
    /// <param name="sheet"></param>
    /// <param name="gv"></param>
    /// <param name="colTypeList">GridView每一列的数据类型</param>
    /// <param name="colCount">GridView的总列数</param>
    /// <param name="rowInex">添加Excel数据行的起始索引号</param>
    /// <param name="cellStyle">表格基础格式</param>
    private void AddSheetBody(ISheet sheet, GridView gv, ICellStyle cellStyle, List<int> colTypeList, int colCount, int rowInex)
    {
        IRow row;
        ICell cell;
        ICellStyle cellStyleDecimal = GetCellStyleDecimal(sheet.Workbook);
        ICellStyle cellStyleDateTime = GetCellStyleDateTime(sheet.Workbook);

        int rowCount = gv.Rows.Count;

        for (int i = 0; i < rowCount; i++)
        {
            row = sheet.CreateRow(rowInex);

            for (int j = 0; j < colCount; j++)
            {
                if (gv.Rows[i].Cells[j].Visible == false) continue;

                string cellText = gv.Rows[i].Cells[j].Text.Trim();
                cellText = cellText.Replace("&nbsp;", "");//替换空字符占位符
                cellText = cellText.Replace("&gt;", ">");//替换 > 占位符

                if (string.IsNullOrEmpty(cellText)) continue;//单元格为空跳过

                cell = row.CreateCell(j);
                if (colTypeList.Count == 0 || colTypeList.Count < j || colTypeList[j] <= 0)//无法获取到该列类型
                {
                    cell.SetCellValue(cellText);
                    cell.CellStyle = cellStyle;
                }
                else
                {
                    try
                    {
                        switch (colTypeList[j])
                        {
                            case 1: cell.SetCellValue(int.Parse(cellText));//int类型
                                cell.CellStyle = cellStyle;
                                break;
                            case 2: cell.SetCellValue(double.Parse(cellText));//decimal数据类型
                                cell.CellStyle = cellStyleDecimal;
                                break;
                            case 3: cell.SetCellValue(DateTime.Parse(cellText));//日期类型
                                cell.CellStyle = cellStyleDateTime;
                                break;
                            default: cell.SetCellValue(cellText);
                                cell.CellStyle = cellStyle;
                                break;
                        }
                    }
                    catch
                    {
                        cell.SetCellValue("单元格导出失败");
                        MCSFramework.Common.LogWriter.FILE_PATH = GetAttachmentDirectory();
                        MCSFramework.Common.LogWriter.WriteLog("\r\n第j=" + j + "类发生错误，数据类型为" + colTypeList[j].ToString() + "，数值为" + cellText + "，报表GUID=" + Request.QueryString["Report"] != null ? Request.QueryString["Report"] : "无GUID值" + "\r\n");
                    }
                }

                int MergeAcross = gv.Rows[i].Cells[j].ColumnSpan > 0 ? gv.Rows[i].Cells[j].ColumnSpan - 1 : 0;//跨列，即合并的列数

                int MergeDown = gv.Rows[i].Cells[j].RowSpan > 0 ? gv.Rows[i].Cells[j].RowSpan - 1 : 0;//跨行，即合并的行数

                if (MergeAcross > 0 || MergeDown > 0)//存在要合并的行
                {
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowInex, rowInex + MergeDown, j, j + MergeAcross));
                    j += MergeAcross;
                }
            }
            rowInex++;
        }
    }
    #endregion

    #region 根据GridView数据源获取列类型
    /// <summary>
    /// 根据GridView数据源获取列类型
    /// </summary>
    /// <param name="gv"></param>
    /// <returns>1：Int32；2：Decimal；3：DateTime；4：String</returns>
    private List<int> GetColumnsType(GridView gv)
    {
        DataTable tb = (DataTable)gv.DataSource;
        List<int> colTypeList = new List<int>();
        foreach (DataColumn col in tb.Columns)
        {
            int dataType = 0;
            if (col.DataType.FullName == "System.Int32") dataType = 1;
            else if (col.DataType.FullName == "System.Decimal") dataType = 2;
            else if (col.DataType.FullName == "System.DateTime") dataType = 3;
            else dataType = 4;
            colTypeList.Add(dataType);
        }
        return colTypeList;
    }
    #endregion

    #region 获取单元格类型
    /// <summary>
    /// 单元格居中，作为单元格类型基础方法不在外部调用
    /// </summary>
    /// <param name="cellStyle"></param>
    /// <returns></returns>
    private ICellStyle CellStyleBasic(ICellStyle cellStyle)
    {
        cellStyle.Alignment = HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;
        return cellStyle;
    }
    /// <summary>
    /// 通用单元格类型
    /// </summary>
    /// <param name="hssfworkbook"></param>
    /// <returns></returns>
    private ICellStyle GetCellStyleCommon(IWorkbook hssfworkbook)
    {
        ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
        CellStyleBasic(cellStyle);
        return cellStyle;
    }
    private ICellStyle GetCellStyleDecimal(IWorkbook hssfworkbook)
    {
        ICellStyle cellStyleDecimal = hssfworkbook.CreateCellStyle();
        CellStyleBasic(cellStyleDecimal);
        cellStyleDecimal.DataFormat = NPOI.HSSF.UserModel.HSSFDataFormat.GetBuiltinFormat("0.000");
        return cellStyleDecimal;
    }
    private ICellStyle GetCellStyleDateTime(IWorkbook hssfworkbook)
    {
        ICellStyle cellStyleDateTime = hssfworkbook.CreateCellStyle();
        CellStyleBasic(cellStyleDateTime);
        cellStyleDateTime.DataFormat = hssfworkbook.CreateDataFormat().GetFormat("yyyy/m/d h:mm:ss");
        return cellStyleDateTime;
    }
    #endregion

    #region Excel文件下载

    public void DownLoadFile(string filePath)
    {
        Response.Clear();
        Response.BufferOutput = true;
        //Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        string fileName = FileNameEncode(DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.ContentType = "application/ms-excel";
        if (File.Exists(filePath))
        {
            Response.WriteFile(filePath);//通知浏览器下载文件
        }
        Response.Flush();
        Response.End();
    }

    /// <summary>
    /// Excel文件下载,不需提供文件后缀名
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="fileName"></param>
    public void DownLoadFile(MemoryStream stream, string fileName)
    {
        fileName = FileNameEncode(fileName);
        Response.Clear();
        Response.BufferOutput = true;
        //Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        //string fileName = HttpUtility.UrlEncode(string.Format("{1}_{0:yyyy-MM-dd_HH_mm}.xls", System.DateTime.Now, "你好"), System.Text.Encoding.UTF8);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.ContentType = "application/ms-excel";
        Response.BinaryWrite(stream.ToArray());//通知浏览器下载文件            
        Response.Flush();
        Response.End();
    }

    /// <summary>
    /// 对提供下载的附件名进行编码
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private string FileNameEncode(string fileName)
    {
        fileName = string.Format("{1}_{0:yyyy_MM_dd_HH_mm}.xlsx", System.DateTime.Now, fileName);
        bool isFireFox = false;
        if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
        {
            isFireFox = true;
        }
        if (isFireFox == true)
        {
            //文件名前后加双引号
            fileName = "\"" + fileName + "\"";
        }
        else
        {
            //非火狐浏览器对中文文件名进行HTML编码
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
        }
        return fileName;
    }
    #endregion

    #region 获取附件文件夹路径
    /// <summary>
    /// 获取下载文件夹路径
    /// </summary>
    /// <returns></returns>
    public string GetAttachmentDirectory()
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Download\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        return path;
    }
    #endregion

    #endregion

}
