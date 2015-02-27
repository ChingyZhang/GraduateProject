using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.EWF;
using System.Data;
using MCSFramework.Common;
using NPOI.SS.UserModel;
using System.IO;

public partial class SubModule_CM_RT_RetailContractApproveList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
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

        ddl_PayMode.DataSource = DictionaryBLL.GetDicCollections("PUB_PayMode");
        ddl_PayMode.DataBind();
        ddl_PayMode.Items.Insert(0, new ListItem("请选择", "0"));
    }

    private void BindGrid()
    {
        if (ddl_State.SelectedValue != "1")
        {
            btn_Approve.Visible = false;
            btn_UnApprove.Visible = false;
        }
        string condition = "";
        if (tbx_ContractID.Text.Trim() != "")
        {
            int id = 0;
            if (int.TryParse(tbx_ContractID.Text, out id))
            {
                condition = " AND CM_Contract.ID=" + id;
            }
            else
            {
                MessageBox.Show(this, "协议ID填写有问题！");
                tbx_ContractID.Focus();
            }
        }
        else
        {
            if (select_OrgSupplier.SelectValue != "" && select_OrgSupplier.SelectValue != "0")
            {
                condition = " AND CM_Contract.Client=" + select_OrgSupplier.SelectValue;
            }
        }
        gv_List.DataSource = CM_ContractBLL.GetApproveSummary(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_State.SelectedValue), (int)Session["UserID"], int.Parse(MCSTabControl1.SelectedTabItem.Value), int.Parse(ddl_PayMode.SelectedValue), condition);
        if (ddl_State.SelectedValue != "1") gv_List.Columns[1].Visible = false;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void Approve(int state)
    {
        int userid = (int)Session["UserID"];
        foreach (GridViewRow row in gv_List.Rows)
        {
            Object cbx = row.FindControl("chk_ID");
            if (cbx != null && ((CheckBox)cbx).Checked)
            {
                Object tbx = row.FindControl("tbx_Remark");
                string Remark = "";
                if (tbx != null && !string.IsNullOrEmpty(((TextBox)tbx).Text))
                    Remark = ((TextBox)tbx).Text;
                else Remark = state == 2 ? "汇总单批量审批通过!" : "汇总单批量审批不通过!";
                int contractid = (int)gv_List.DataKeys[row.RowIndex]["ID"];
                CM_ContractBLL con = new CM_ContractBLL(contractid);
                int taskid = con.Model.ApproveTask;
                if (taskid > 0)
                {
                    int jobid = EWF_TaskBLL.StaffCanApproveTask(taskid, userid);
                    if (jobid > 0)
                    {
                        EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                        if (job != null)
                        {
                            int decisionid = job.StaffCanDecide(userid);
                            if (decisionid > 0)
                                job.Decision(decisionid, userid, state, Remark);
                        }
                    }
                }
            }
        }
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        Approve(2);
        MessageBox.Show(this, "批量审批通过成功！");
        BindGrid();
    }
    protected void btn_UnApprove_Click(object sender, EventArgs e)
    {
        Approve(3);
        MessageBox.Show(this, "批量审批不通过成功！");
        BindGrid();
    }


    protected void bt_Export_Click(object sender, EventArgs e)
    {
        string condition = "";
        if (tbx_ContractID.Text.Trim() != "")
        {
            int id = 0;
            if (int.TryParse(tbx_ContractID.Text, out id))
            {
                condition = " AND CM_Contract.ID=" + id;
            }
            else
            {
                MessageBox.Show(this, "协议ID填写有问题！");
                tbx_ContractID.Focus();
            }
        }
        else
        {
            if (select_OrgSupplier.SelectValue != "" && select_OrgSupplier.SelectValue != "0")
            {
                condition = " AND CM_Contract.Client=" + select_OrgSupplier.SelectValue;
            }
        }
        DataTable dt = CM_ContractBLL.GetApproveSummary(int.Parse(tr_OrganizeCity.SelectValue), int.Parse(ddl_State.SelectedValue), (int)Session["UserID"], int.Parse(MCSTabControl1.SelectedTabItem.Value), int.Parse(ddl_PayMode.SelectedValue), condition); ;
        //string name = "";
        //if (MCSTabControl1.SelectedIndex == 0)
        //    name = "零售商导购协议导出";
        //else name = "零售商返利协议导出";
        // CreateExcel(dt, "Export-" + name + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
        getExcel(dt);
    }
    public void getExcel(DataTable dt)
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Download\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string name = "";
        if (MCSTabControl1.SelectedIndex == 0)
            name = "零售商导购协议导出";
        else name = "零售商返利协议导出";
        path += name;
        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        ISheet sheet = book.CreateSheet(name);
        IRow row = sheet.CreateRow(0);

        //创建表头
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
        }

        //创建内容
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row2 = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
                row2.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
        }


        //写入到客户端
        FileStream file = new FileStream(path, FileMode.Create);
        book.Write(file);

        file.Close();
        book = null;
        sheet = null;

        Downloadfile(path, "Export-" + name + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".xls");

    }
    private void Downloadfile(string path, string filename)
    {
        try
        {
            Response.Clear();
            Response.BufferOutput = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.WriteFile(path);
            Response.Flush();

            File.Delete(path);

            Response.End();
        }
        catch (System.Exception err)
        {
            string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                "StackTrace:" + err.StackTrace + "<br/>";


            MessageBox.Show(this, "系统错误-3!" + err.Message);
        }
    }
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
                if (content.Contains('\r')) content = content.Replace('\r', ' ');
                if (content.Contains('\n')) content = content.Replace('\n', ' ');

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


    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = "<a href='RetailerContractDetail.aspx?ContractID=" + e.Row.Cells[2].Text + "'>" + e.Row.Cells[2].Text + "</a>";
            string client = e.Row.Cells[7].Text;
            e.Row.Cells[7].Text = client;
            e.Row.Cells[8].Text = "<a href='RetailerDetail.aspx?ClientID=" + client + "'>" + e.Row.Cells[8].Text + "</a>";
        }
    }
}
