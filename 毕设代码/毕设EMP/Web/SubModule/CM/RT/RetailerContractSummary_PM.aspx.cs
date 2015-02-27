using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using System.IO;
using System.Text;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.FNA;
using System.Data;
using MCSFramework.BLL.CM;

public partial class SubModule_CM_RT_RetailerContractSummary_PM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["State"] != null)
            {
                if (ddl_State.Items.FindByValue(Request.QueryString["State"]) != null)
                    ddl_State.SelectedValue = Request.QueryString["State"];
            }
            BindDropDown();
           // MCSTabControl1_OnTabClicked(null, null);
            BindGrid();
        }
    }
    #region 绑定下拉列表框
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
        tr_OrganizeCity_Selected(null, null);

        if (Request.QueryString["OrganizeCity"] != null)
        {
            tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"].ToString();
        }
        #endregion

        ddl_RTChannel.DataSource = DictionaryBLL.GetDicCollections("CM_RT_Channel");
        ddl_RTChannel.DataBind();
        ddl_RTChannel.Items.Insert(0, new ListItem("所有", "0"));
        ddl_RTChannel.SelectedValue = "0";

    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) >= city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0 || tr_OrganizeCity.SelectValue == "1")
            {
                ddl_Level.Items.Insert(0, new ListItem("总部", city.Level.ToString()));
            }
        }
    }
    #endregion
    #region 仅查看待我审批的费用申请单
    private string GetNeedMeApproveTaskIDs()
    {
        if (MCSTabControl1.SelectedIndex == 0)
        {
            int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
            int level = int.Parse(ddl_Level.SelectedValue);
            int state = int.Parse(ddl_State.SelectedValue);
            int rtchannel = int.Parse(ddl_RTChannel.SelectedValue);
            string taskids = "";

            DataTable dt = CM_ContractBLL.GetPMList(organizecity, level, state, (int)Session["UserID"], rtchannel);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ApproveTask"].ToString() != "" && dt.Rows[i]["ApproveTask"].ToString() != "NULL")
                    taskids += dt.Rows[i]["ApproveTask"].ToString() + ",";
            }

            if (taskids.EndsWith(",")) taskids = taskids.Substring(0, taskids.Length - 1);

            return taskids;
        }
        else if (MCSTabControl1.SelectedIndex == 1)
        {
            string ids="";
            foreach (GridViewRow row in gv_ListDetail.Rows)
            {
                Object cbx = row.FindControl("chk_ID");
                if (cbx != null && ((CheckBox)cbx).Checked)
                {
                    if (ids != "") ids += ",";
                    ids += gv_ListDetail.DataKeys[row.RowIndex][1].ToString();
                }
            }
            return ids;
        }
        return "";
    }
    #endregion
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex == 4)
        {
            //Response.Redirect("PM_GetAnalysisOverview.aspx?AccountMonth=" + ddl_Month.SelectedValue + "&OrganizeCity=" + tr_OrganizeCity.SelectValue);
        }
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int level = int.Parse(ddl_Level.SelectedValue);
        int state = int.Parse(ddl_State.SelectedValue);
        int rtchannel = int.Parse(ddl_RTChannel.SelectedValue);

        gv_List.Visible = (MCSTabControl1.SelectedIndex == 0);
        gv_ListDetail.Visible = (MCSTabControl1.SelectedIndex == 1);
        bt_Approved.Visible = (MCSTabControl1.SelectedIndex == 0);
        bt_UnApproved.Visible = (MCSTabControl1.SelectedIndex == 1);

        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                {
                    #region 显示统计分析
                    DataTable dtSummary = CM_ContractBLL.GetPMSummary(organizecity, level, state, (int)Session["UserID"], rtchannel);
                    if (dtSummary.Rows.Count == 0)
                    {
                        gv_List.DataBind();
                        
                    }

                    gv_List.DataSource = dtSummary;
                    gv_List.DataBind();
                    MatrixTable.GridViewMatric(gv_List);
                    #endregion
                }
                break;

            case "1":
                {
                    DataTable dtList = CM_ContractBLL.GetPMListDetail(organizecity, level, state, (int)Session["UserID"], rtchannel);
                    gv_ListDetail.DataSource = dtList;
                    gv_ListDetail.BindGrid();
                }
                break;
        }

        #region 是否可以批量审批
        if (state != 1)
        {
            bt_Approved.Visible = false;
            bt_UnApproved.Visible = false;
            if (gv_ListDetail.Visible)
            {
                gv_ListDetail.Columns[0].Visible = false;
                gv_ListDetail.Columns[1].Visible = false;
            }
        }
        
       
        #endregion
    }
    #region 合并行组中相邻行相同值
    public void GridViewMergSampeValueRow(GridView gv, int ColumnIndex, string BackColor, int BorderWidth, string BorderColor)
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
    private bool ColumnIsSampe(GridView gv, int row, int column)
    {
        if (gv.Rows[row].Cells[column].Text.Trim() == "&nbsp;") return false;

        for (int i = column; i > 0; i--)
        {
            if (gv.Rows[row].Cells[i].Text != gv.Rows[row - 1].Cells[i].Text) return false;
        }

        return true;
    }
    #endregion
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void DoApprove(bool ApproveFlag)
    {

        #region 仅查看待我审批的工资申请单
        string taskids = "";
        if (ddl_State.SelectedValue == "1")
        {
            taskids = GetNeedMeApproveTaskIDs();

            if (taskids == "")
            {
                MessageBox.Show(this, "对不起，没有需要待您审批的费用申请单!");
                return;
            }
        }
        #endregion
        string[] TaskIDs = taskids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);



        foreach (string taskid in TaskIDs)
        {

            int jobid = EWF_TaskBLL.StaffCanApproveTask(int.Parse(taskid), (int)Session["UserID"]);
            EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
            if (job.Model != null)
            {
                int decision = job.StaffCanDecide((int)Session["UserID"]);
                if (decision > 0)
                {
                    if (ApproveFlag)
                    {
                        job.Decision(decision, (int)Session["UserID"], 2, "汇总单批量审批通过!");       //2:审批已通过
                    }
                    else
                    {
                        job.Decision(decision, (int)Session["UserID"], 3, "汇总单批量未能审批通过!");    //3:审批未通过
                    }
                }
            }

        }

        BindGrid();
        MessageBox.Show(this, ApproveFlag ? "审批成功！" : "已成功将选择区域的申请单，设为批复未通过！");
        return;

    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                {
                    gv_List.AllowPaging = false;
                    BindGrid();
                    Response.Clear();
                    string filename = HttpUtility.UrlEncode("预付导购管理费用汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.BufferOutput = true;
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                    Page.EnableViewState = false;
                    StringWriter tw = new System.IO.StringWriter();
                    HtmlTextWriter hw = new HtmlTextWriter(tw);
                    gv_List.RenderControl(hw);
                    StringBuilder outhtml = new StringBuilder(tw.ToString());
                    outhtml = outhtml.Replace("&nbsp;", "");
                    Response.Write(outhtml.ToString());
                    Response.End();
                    Page.EnableViewState = true;
                    gv_List.AllowPaging = true;
                    break;
                }
            case "1":
                {
                    int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
                    int level = int.Parse(ddl_Level.SelectedValue);
                    int state = int.Parse(ddl_State.SelectedValue);
                    int rtchannel = int.Parse(ddl_RTChannel.SelectedValue);
                    DataTable dtList = CM_ContractBLL.GetPMListDetail(organizecity, level, state, (int)Session["UserID"], rtchannel);
                    dtList.Columns["ApproveTask"].ColumnName = "流程ID";
                    string filename = HttpUtility.UrlEncode("预付导购管理费用明细导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    DataTableToExcel(dtList,filename);
                    break;
                }

        }
        BindGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        if (gv_List.HeaderRow != null)
        {
            gv_List.HeaderRow.Cells[0].Visible = false;
            foreach (GridViewRow r in gv_List.Rows)
            {
                r.Cells[0].Visible = false;

                #region 金额数据格式化
                if (r.Cells.Count > 2)
                {
                    for (int i = 2; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            if (gv_List.HeaderRow.Cells[i].Text.EndsWith("(%)") || r.Cells[3].Text.EndsWith("%"))
                            {
                                r.Cells[i].Text = d.ToString("#,0.##");
                            }
                            else
                            {
                                r.Cells[i].Text = d.ToString("#,0.#");
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }



    protected void bt_UnApproved_Click(object sender, EventArgs e)
    {
        DoApprove(false);
    }
    protected void bt_Approved_Click(object sender, EventArgs e)
    {
        DoApprove(true);
    }
    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    private void DataTableToExcel(System.Data.DataTable dtData,String filename)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        //当前对话  
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件  
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;
        if (dtData != null)
        {
            // 设置编码和附件格式  
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "UTF-8";
            curContext.Response.BufferOutput = true;
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            curContext.Response.Clear();
            // 导出excel文件  
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid  
            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            //返回客户端  
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }
}
