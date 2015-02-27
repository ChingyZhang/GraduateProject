using System;
using System.Linq;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Common;
using MCSFramework.Model.FNA;
using MCSFramework.BLL.Pub;
using System.Collections.Specialized;
using MCSFramework.Model;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Data;
using System.Collections;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public partial class SubModule_FNA_StaffSalary_StaffSalaryDetailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] > 0)
                BindData();
            else
                Response.Redirect("StaffSalaryList.aspx");
        }

        MatrixTable.GridViewMatric(gv_List.Visible ? gv_List : gv_list2);
    }

    #region 获取指定管理片区的预算信息
    private void BindBudgetInfo(int month, int organizecity)
    {
        //tbl_BudgetInfo.Visible = true;
        //int feetype = ConfigHelper.GetConfigInt("SalaryFeeType");

        //lb_BudgetAmount.Text = (FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, feetype) +
        //    FNA_BudgetBLL.GetSumBudgetAmount(month, organizecity, 0)).ToString("0.###");

        //lb_BudgetBalance.Text = (FNA_BudgetBLL.GetUsableAmount(month, organizecity, feetype) +
        //    FNA_BudgetBLL.GetUsableAmount(month, organizecity, 0)).ToString("0.###");
    }
    #endregion

    private void BindData()
    {
        #region 绑定用户可管辖的片区
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

        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);
        pn_detail.BindData(bll.Model);
        ViewState["State"] = bll.Model.State;

        BindGrid();

        if (bll.Model.State != 1 && bll.Model.State != 8)
        {
            pn_detail.SetControlsEnable(false);
            bt_Save.Visible = false;
            bt_Submit.Visible = false;
            bt_Delete.Visible = false;
        }

        if (bll.Model.State != 3)
        {
            BindBudgetInfo(bll.Model.AccountMonth, bll.Model.OrganizeCity);
        }
    }

    private void BindGrid()
    {
        string condition = "FNA_StaffSalaryDetail.SalaryID=" + ViewState["ID"].ToString();
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND  MCS_SYS.dbo.Org_Staff.OrganizeCity in(" + orgcitys + ") ";
        }
        if (select_Staff.SelectValue != "")
        {
            condition += " AND  MCS_SYS.dbo.Org_Staff.ID=" + select_Staff.SelectValue;
        }
        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);
        if (bll.Model.Classify == 1)
        {
            gv_List.ConditionString = condition + " Order By Org_Staff_OrganizeCity2,Org_Staff_OrganizeCity3,Org_Staff_OrganizeCity4,Org_Staff_RealName";
            gv_List.BindGrid();
            gv_list2.Visible = false;
            gv_List.Visible = true;
        }
        else
        {
            gv_list2.ConditionString = condition + " Order By Org_Staff_OrganizeCity2,Org_Staff_OrganizeCity3,Org_Staff_OrganizeCity4,Org_Staff_RealName";
            gv_list2.BindGrid();
            gv_List.Visible = false;
            gv_list2.Visible = true;
        }


        lb_TotalCost.Text = FNA_StaffSalaryBLL.GetSumSalary((int)ViewState["ID"]).ToString("0.##");

        if ((int)ViewState["State"] == 2 || (int)ViewState["State"] == 3)
        {
            gv_List.SetControlsEnable(false);
            gv_List.Columns[gv_List.Columns.Count - 1].Visible = true; //调整原因
            gv_List.Columns[gv_List.Columns.Count - 2].Visible = true; //调整金额

            foreach (GridViewRow gr in gv_List.Rows)
            {
                ((HyperLink)gr.FindControl("hy_StaffName")).Enabled = true;
            }

            if ((int)ViewState["State"] == 2 && Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            {
                bt_SaveChange.Visible = true;
                foreach (GridViewRow gr in gv_List.Rows)
                {
                    ((TextBox)gr.FindControl("tbx_PayAdjust_Approve")).Enabled = true;
                    ((TextBox)gr.FindControl("tbx_PayAdjust_Reason")).Enabled = true;
                }
            }
        }


        //MatrixTable.GridViewMatric(gv_List);

    }

    protected void bt_Save_Click(object sender, EventArgs e)
    {
        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);

        pn_detail.GetData(bll.Model);

        bll.Update();
        decimal Bounsdeduction, BonusAdd;

        #region 保存每一个员工工资情况
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex][0];

            if (decimal.TryParse(((TextBox)row.FindControl("tbx_Bounsdeduction")).Text, out Bounsdeduction) && decimal.TryParse(((TextBox)row.FindControl("tbx_BonusAdd")).Text, out BonusAdd) && (Bounsdeduction != 0 || BonusAdd != 0))
            {
                FNA_StaffSalaryDetail detail = bll.GetDetailModel(id);
                detail.Bounsdeduction = Bounsdeduction;
                detail.BonusAdd = BonusAdd;
                detail["Remark"] = ((TextBox)row.FindControl("tbx_Remark")).Text;
                detail.Bonus += detail.BonusAdd - detail.Bounsdeduction;
                bll.UpdateDetail(detail);
            }

        }
        #endregion

        if (sender != null)
            MessageBox.ShowAndRedirect(this, "员工工资保存成功!", "StaffSalaryDetailList.aspx?ID=" + ViewState["ID"].ToString());
        else
            BindGrid();
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            bt_Save_Click(null, null);

            FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);

            //#region 判断预算余额是否够工单申请
            int feetype = ConfigHelper.GetConfigInt("SalaryFeeType");

            //decimal budgetbalance = FNA_BudgetBLL.GetUsableAmount(bll.Model.AccountMonth, bll.Model.OrganizeCity, feetype) +
            //    FNA_BudgetBLL.GetUsableAmount(bll.Model.AccountMonth, bll.Model.OrganizeCity, 0);
            //if (budgetbalance < bll.GetSumSalary())
            //{
            //    MessageBox.Show(this, "对不起，您当前的预算余额不够申请此工资申请，您当前的预算余额为:" + budgetbalance.ToString());
            //    return;
            //}
            //#endregion

            #region 发起工作流
            NameValueCollection dataobjects = new NameValueCollection();
            dataobjects.Add("ID", ViewState["ID"].ToString());
            dataobjects.Add("OrganizeCity", bll.Model.OrganizeCity.ToString());
            dataobjects.Add("AccountMonth", bll.Model.AccountMonth.ToString());
            dataobjects.Add("ApplyCost", lb_TotalCost.Text);
            dataobjects.Add("StaffNum", bll.Items.Count.ToString());

            //组合审批任务主题
            Addr_OrganizeCity _city = new Addr_OrganizeCityBLL(bll.Model.OrganizeCity).Model;
            string title = _city.Name + ",员工工资总额:" + lb_TotalCost.Text + ",人数:" + bll.Items.Count.ToString();

            int TaskID = EWF_TaskBLL.NewTask("FNA_SalaryApplyFlow", (int)Session["UserID"], title, "~/SubModule/FNA/StaffSalary/StaffSalaryDetailList.aspx?ID=" + ViewState["ID"].ToString(), dataobjects);
            if (TaskID > 0)
            {
                bll.Submit((int)Session["UserID"], TaskID, feetype);

                new EWF_TaskBLL(TaskID).Start();        //直接启动流程
            }
            #endregion

            MessageBox.ShowAndRedirect(this, "员工工资提交申请成功!", "../../EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            new FNA_StaffSalaryBLL((int)ViewState["ID"]).Delete();

            MessageBox.ShowAndRedirect(this, "员工工资删除成功!", "StaffSalaryList.aspx");
        }
    }
    protected void bt_SaveChange_Click(object sender, EventArgs e)
    {
        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);

        #region 保存每一个员工工资情况
        foreach (GridViewRow row in gv_List.Rows)
        {
            try
            {
                string _staffname = ((HyperLink)(row.FindControl("hy_StaffName"))).Text;

                int id = (int)gv_List.DataKeys[row.RowIndex][0];
                FNA_StaffSalaryDetail detail = bll.Items.First<FNA_StaffSalaryDetail>(item => item.ID == id);

                decimal org_adjust = 0;
                decimal adjust = 0;

                decimal.TryParse(detail["PayAdjust_Approve"], out org_adjust);
                decimal.TryParse(((TextBox)row.FindControl("tbx_PayAdjust_Approve")).Text, out adjust);

                if (org_adjust != adjust)
                {
                    detail["PayAdjust_Approve"] = adjust.ToString();
                    detail["PayAdjust_Reason"] = ((TextBox)row.FindControl("tbx_PayAdjust_Reason")).Text;
                    if (detail["PayAdjust_Reason"] == "")
                    {
                        MessageBox.Show(this, "第" + (row.RowIndex + 1).ToString() + "行，" + _staffname + "的工资审批调整后，原因不能为空!");
                        return;
                    }
                    detail.TotalSalary += adjust - org_adjust;
                    bll.UpdateDetail(detail);
                    FNA_StaffSalaryBLL.UpdateAdjustRecord((int)ViewState["ID"], (int)Session["UserID"], org_adjust.ToString(), detail["PayAdjust_Approve"], _staffname);
                }
            }
            catch { continue; }
        }
        #endregion

        BindGrid();
        MessageBox.Show(this, "工资调整金额保存成功!");
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        /*
        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);
        if (bll.Model.Classify == 1)
        {
            gv_List.AllowPaging = false;
        }
        else
        {
            gv_list2.AllowPaging = false;
        }
        BindGrid();

        string filename = HttpUtility.UrlEncode("员工绩效导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"));
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
        Page.EnableViewState = false;

        StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        if (bll.Model.Classify == 1)
        {
            gv_List.RenderControl(hw);

        }
        else
        {
            gv_list2.RenderControl(hw);
        }

        StringBuilder outhtml = new StringBuilder(tw.ToString());
        outhtml = outhtml.Replace("&nbsp;", "").Replace("<br />", "");

        Response.Write(outhtml.ToString());
        Response.End();
        if (bll.Model.Classify == 1)
        {
            gv_List.AllowPaging = true;
        }
        else
        {
            gv_list2.AllowPaging = true;
        }
        BindGrid();
         * */
        string condition = "SalaryID=" + ViewState["ID"].ToString();
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;
            condition += " AND StaffOrganizeCity in(" + orgcitys + ") ";
        }
        if (select_Staff.SelectValue != "")
        {
            condition += " AND StaffID=" + select_Staff.SelectValue;
        }
        FNA_StaffSalaryBLL bll = new FNA_StaffSalaryBLL((int)ViewState["ID"]);
        DataTable dataTable = null;
        //Classify=1：导出办事处主任工资列表；Classify=2：导出业务代表工资列表
        dataTable = new FNA_StaffSalaryBLL().GetStaffSalaryDetail(condition, bll.Model.Classify);

        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "Attachment\\SalaryExport\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string filePath = path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
        DataTableToExcel(dataTable, filePath);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    #region DataTable数据写入Excel
    /// <summary>
    /// 将DataTable数据写入Excel 2007
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="filePath">Excel文件存储路径</param>
    public void DataTableToExcel(DataTable dt, string filePath)
    {

        IWorkbook hssfworkbook = new XSSFWorkbook();
        ISheet sheet = hssfworkbook.CreateSheet("sheet1");

        IRow row = sheet.CreateRow(0);
        ICell cell;

        ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
        cellStyle.Alignment = HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;

        //求出有几个组
        int groups = 1;
        foreach (DataColumn c in dt.Columns)
        {
            int len = c.ColumnName.Split('→').Length;
            if (groups < len) groups = len;
        }
        if (groups < 1) groups = 1;

        //预先创建足够的Excel行
        for (int i = 0; i < groups; i++) { sheet.CreateRow(i); }

        /***************************************/
        /***************添加表头****************/
        /***************************************/

        string groupName = String.Empty;//每一个分组的标题
        int groupStartIndex = 0;//每一个分组的起始位置,用于添加单元格合并区域
        for (int i = 0; i < dt.Columns.Count; i++)//i代表列索引
        {
            //如果Caption没有设置，则返回 ColumnName 的值
            string headCellStr = dt.Columns[i].ColumnName;

            //当前列所在分组的标题，不存在分组时标题设为空字符
            string groupNameNow = headCellStr.Substring(0, headCellStr.IndexOf('→') == -1 ? 0 : headCellStr.IndexOf('→'));
            //开始创建单元格的行索引
            int startRowIndex = !string.IsNullOrEmpty(groupName) && groupNameNow.Equals(groupName) ? 1 : 0;
            //当前列的分组数
            int titleGroups = headCellStr.IndexOf('→') != -1 ? headCellStr.Split('→').Length : 1;
            for (int j = startRowIndex; j < titleGroups; j++)
            {
                cell = sheet.GetRow(j).CreateCell(i);
                //获得子标题
                string subTitle = headCellStr.IndexOf('→') != -1 ? headCellStr.Split('→')[j].ToString() : headCellStr;
                cell.SetCellValue(subTitle);
            }
            if (titleGroups < groups)//当前列分组数小于最大分组数，为最后一列添加合并区域
            {
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(titleGroups - 1, groups - 1, i, i));
            }
            //当前分组标题与前一分组标题不同，要开始新的分组
            if (!groupName.Equals(groupNameNow))
            {
                if (i > 0 && groupStartIndex != i - 1)//剔除第一列出现新列头的情况
                {
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, groupStartIndex, i - 1));
                }
                groupStartIndex = i;//重置合并区域开始位置索引
                groupName = groupNameNow;
            }
            else if (string.IsNullOrEmpty(groupName))
            {
                groupStartIndex = i;//重置合并区域开始位置索引
                //groupName = string.Empty;
            }
            if (i == dt.Columns.Count - 1 && groupStartIndex < i)
            {
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, groupStartIndex, i));
            }
        }

        //添加table的主体数据
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            row = sheet.CreateRow(groups + i);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Rows[i][j] == null || string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                {
                    continue;
                }
                cell = row.CreateCell(j);
                cell.SetCellValue(dt.Rows[i][j].ToString().Trim());
            }
        }

        try
        {
            FileStream fs = File.Create(filePath);
            hssfworkbook.Write(fs);
            fs.Close();
        }
        catch (Exception e1)
        {
            MessageBox.Show(Page, "errorMessage=" + e1.Message + ";filePath=" + filePath);
        }


        Response.Clear();
        Response.BufferOutput = true;
        Response.ContentType = "application/ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        if (File.Exists(filePath))
        {
            Response.WriteFile(filePath);//通知浏览器下载文件
        }
        Response.Flush();

        File.Delete(filePath);

        Response.End();
    }
    #endregion

}
