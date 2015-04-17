// ===================================================================
// 文件路径:SubModule/Reports/Rpt_ReportDetail.aspx.cs 
// 生成日期:2010/10/7 20:23:57 
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
using MCSControls.MCSWebControls;

public partial class SubModule_Reports_Rpt_ReportDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? new Guid(Request.QueryString["ID"]) : Guid.Empty;
            ViewState["Folder"] = Request.QueryString["Folder"] != null ? int.Parse(Request.QueryString["Folder"]) : 0;
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
                MCSTabControl1.Items[1].Visible = false;
                MCSTabControl1.Items[2].Visible = false;
                MCSTabControl1.Items[3].Visible = false;

                #region 限制报表目录为参数值
                if ((int)ViewState["Folder"] == 0) Response.Redirect("Rpt_ReportList.aspx");
                MCSTreeControl tr_Folder = (MCSTreeControl)pl_detail.FindControl("Rpt_Report_Folder");
                DropDownList ddl_DataSet = (DropDownList)pl_detail.FindControl("Rpt_Report_DataSet");

                if (tr_Folder != null && ddl_DataSet != null)
                {
                    tr_Folder.SelectValue = ViewState["Folder"].ToString();
                    tr_Folder.Enabled = false;

                    ddl_DataSet.DataValueField = "ID";
                    ddl_DataSet.DataTextField = "Name";
                    ddl_DataSet.DataSource = Rpt_DataSetBLL.GetDataByFolder((int)ViewState["Folder"], true);
                    ddl_DataSet.DataBind();
                    ddl_DataSet.Items.Insert(0, new ListItem("请选择...", Guid.Empty.ToString()));
                }
                #endregion

                RadioButtonList rbl_AddRowTotal = (RadioButtonList)pl_detail.FindControl("Rpt_Report_AddRowTotal");
                if (rbl_AddRowTotal != null) rbl_AddRowTotal.SelectedValue = "N";

                RadioButtonList rbl_AddColumnTotal = (RadioButtonList)pl_detail.FindControl("Rpt_Report_AddColumnTotal");
                if (rbl_AddColumnTotal != null) rbl_AddColumnTotal.SelectedValue = "N";

                bt_ViewReport.Visible = false;
                bt_Delete.Visible = false;
            }
        }
    }
    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindData()
    {
        Rpt_Report m = new Rpt_ReportBLL((Guid)ViewState["ID"]).Model;
        if (m != null)
        {
            pl_detail.BindData(m);

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
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        Rpt_ReportBLL _bll;
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll = new Rpt_ReportBLL((Guid)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new Rpt_ReportBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.DataSet == Guid.Empty)
        {
            MessageBox.Show(this, "请正确选择报表使用的数据集!");
            return;
        }
        #endregion

        if (_bll.Model.Title == "") _bll.Model.Title = _bll.Model.Name;

        if (_bll.Model.ReportType == 1 && _bll.Model.AddColumnTotal != "N")
        {
            _bll.Model.AddColumnTotal = "N";
        }
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "Rpt_ReportDetail.aspx?ID=" + _bll.Model.ID.ToString());
            }
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];

            if (_bll.Add() == 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "Rpt_ReportDetail.aspx?ID=" + _bll.Model.ID.ToString());
            }
        }
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if ((Guid)ViewState["ID"] != Guid.Empty)
        {
            switch (e.item.Value)
            {
                case "0":
                    Response.Redirect("Rpt_DataSetDetail.aspx?ID=" + ViewState["ID"].ToString());
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
        else
        {
            MCSTabControl1.SelectedIndex = 0;
        }
    }
    protected void bt_ViewReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportViewer.aspx?Report=" + ViewState["ID"].ToString());
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        new Rpt_ReportBLL((Guid)ViewState["ID"]).Delete();
        MessageBox.ShowAndRedirect(this, "删除成功!", "Rpt_ReportList.aspx");
    }
}