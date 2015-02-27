// ===================================================================
// 文件路径:SubModule/Reports/Rpt_ReportChartsDetail.aspx.cs 
// 生成日期:2010/10/8 22:34:31 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;
public partial class SubModule_Reports_Rpt_ReportChartsDetail : System.Web.UI.Page
{
    DropDownList ddl_AxisColumns;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        ddl_AxisColumns = (DropDownList)pl_detail.FindControl("Rpt_ReportCharts_AxisColumns");

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["Report"] = Request.QueryString["Report"] != null ? new Guid(Request.QueryString["Report"]) : Guid.Empty;
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            #endregion

            BindDropDown();


            if ((Guid)ViewState["ID"] != Guid.Empty)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                if ((Guid)ViewState["Report"] == Guid.Empty) Response.Redirect("Rpt_ReportList.aspx");

                bt_Delete.Visible = false;
            }

        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            Rpt_ReportCharts m = new Rpt_ReportChartsBLL((Guid)ViewState["ID"]).Model;
            if (m != null)
            {
                pl_detail.BindData(m);
                ViewState["Report"] = m.Report;
            }
        }

        if ((Guid)ViewState["Report"] != Guid.Empty)
        {
            Rpt_ReportBLL report = new Rpt_ReportBLL((Guid)ViewState["Report"]);

            #region 绑定可选的轴下拉框
            IList<Rpt_ReportRowGroups> rowgroups = report.GetRowGroups();
            if (rowgroups.Count > 0)
            {
                //报表有行分组时，则从行分组中取最后一列作为轴，且不可变更
                ddl_AxisColumns.Items.Add(rowgroups[rowgroups.Count - 1].DisplayName);
                ddl_AxisColumns.Enabled = false;
            }
            else
            {
                //报表无行分组时，从数据集中取一列
                foreach (Rpt_DataSetFields f in new Rpt_DataSetBLL(report.Model.DataSet).GetFields())
                {
                    ddl_AxisColumns.Items.Add(f.DisplayName);
                }
            }
            #endregion

            #region 绑定可选的系列
            if (report.Model.ReportType == 1)
            {
                foreach (Rpt_DataSetFields f in new Rpt_DataSetBLL(report.Model.DataSet).GetFields())
                {
                    if (f.DataType == 1 || f.DataType == 2)
                    {
                        cbxl_Fields.Items.Add(f.DisplayName);
                    }
                }
            }
            else if (report.Model.ReportType == 2)
            {
                foreach (Rpt_ReportValueGroups f in report.GetValueGroups())
                {
                    cbxl_Fields.Items.Add(f.DisplayName);
                }
            }
            #endregion
        }
    }

    #endregion

    private void BindData()
    {
        Rpt_ReportCharts m = new Rpt_ReportChartsBLL((Guid)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);
            ViewState["Report"] = m.Report;

            Rpt_ReportBLL report = new Rpt_ReportBLL(m.Report);

            //设置轴字段下拉框的值
            if (m.AxisColumns != "" && ddl_AxisColumns.Items.FindByValue(m.AxisColumns) != null)
            {
                ddl_AxisColumns.SelectedValue = m.AxisColumns;
            }

            //设置系列字段列复选框的打勾选项
            foreach (string s in m.SeriesColumns.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                ListItem item = cbxl_Fields.Items.FindByValue(s);
                if (item != null) item.Selected = true;
            }
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Rpt_ReportChartsBLL _bll;
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll = new Rpt_ReportChartsBLL((Guid)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Rpt_ReportChartsBLL();
            _bll.Model.Report = (Guid)ViewState["Report"];
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项

        #endregion

        #region 获取勾选的系列
        _bll.Model.SeriesColumns = "";
        foreach (ListItem item in cbxl_Fields.Items)
        {
            if (item.Selected) _bll.Model.SeriesColumns += item.Value + "|";
        }
        #endregion

        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "Rpt_ReportCharts.aspx?ID=" + _bll.Model.Report.ToString());
            }
        }
        else
        {
            //新增
            if (_bll.Add() == 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_ReportCharts.aspx?ID=" + _bll.Model.Report.ToString());
            }
        }

    }

    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        new Rpt_ReportChartsBLL((Guid)ViewState["ID"]).Delete();
        Response.Redirect("Rpt_ReportCharts.aspx?ID=" + ViewState["Report"].ToString());
    }
}