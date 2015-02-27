using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.EWF;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.Promotor;
using MCSFramework.Model.Pub;

public partial class SubModule_PM_PM_PromotorApproveList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
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
        #endregion
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            Addr_OrganizeCity city = new Addr_OrganizeCityBLL((int.Parse(tr_OrganizeCity.SelectValue))).Model;
            ddl_Level.DataSource = DictionaryBLL.GetDicCollections("Addr_OrganizeCityLevel").Where(p => int.Parse(p.Key) > city.Level).ToList().OrderBy(p => p.Key);
            ddl_Level.DataBind();
            if (ddl_Level.Items.Count == 0)
            {
                ddl_Level.Items.Add(new ListItem("本级", city.Level.ToString()));
            }
        }
    }
    #endregion
    protected void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_State.SelectedValue == "2")
        {
            ddl_App.Enabled = false;
            btn_Approve.Visible = false;
            btn_UnApprove.Visible = false;
        }
        else
        {
            ddl_App.Enabled = true;
        }
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.Visible = e.item.Value == "1";
        gv_Consult.Visible = !gv_List.Visible;
        btn_Approve.Visible = gv_List.Visible;
        btn_UnApprove.Visible = gv_List.Visible;

        ddl_Level.Enabled = !gv_List.Visible;

        BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        DataTable dtlist = new DataTable();
        if (MCSTabControl1.SelectedTabItem.Value == "2")
        {
            dtlist = PM_PromotorBLL.GetApproveConsult(organizecity, int.Parse(ddl_Level.SelectedValue));
            if (dtlist.Rows.Count > 0)
            {
                DataRow dtrow = dtlist.NewRow();
                dtrow["管理片区"] = "合计";
                dtrow["导购人数"] = dtlist.Compute("Sum(导购人数)", "true");
                dtrow["上上月实际销量"] = dtlist.Compute("Sum(上上月实际销量)", "true");
                dtrow["上月实际销量"] = dtlist.Compute("Sum(上月实际销量)", "true");
                dtrow["预估本月销量"] = dtlist.Compute("Sum(预估本月销量)", "true");
                dtrow["导购人数"] = dtlist.Compute("Sum(导购人数)", "true");
                dtrow["陈列费"] = dtlist.Compute("Sum(陈列费)", "true");
                dtrow["促销员管理费"] = dtlist.Compute("Sum(促销员管理费)", "true");
                dtrow["底薪"] = dtlist.Compute("Sum(底薪)", "true");
                //dtrow["社保"] = dtlist.Compute("Sum(社保)", "true");
                dtrow["费率B"] = dtrow["预估本月销量"].ToString() == "0" ? "0" : 
                    ((decimal.Parse(dtrow["底薪"].ToString()) + decimal.Parse(dtrow["促销员管理费"].ToString())) / decimal.Parse(dtrow["预估本月销量"].ToString()) * 100).ToString("0.00");
                dtlist.Rows.Add(dtrow);
                gv_Consult.DataSource = dtlist;
                gv_Consult.DataBind();
            }
        }
        else
        {
            string appcode = "";
            if (ddl_State.SelectedValue != "2") appcode = ddl_App.SelectedValue;

            DataTable dt = PM_PromotorBLL.GetApproveList(organizecity, appcode, (int)Session["UserID"]);
            gv_List.DataSource = dt;
            gv_List.DataBind();
            gv_List.Width = dt.Columns.Count * 60;

            gv_List.Columns[0].Visible = ddl_State.SelectedValue != "2";


            btn_Approve.Visible = (gv_List.Rows.Count > 0 && ddl_State.SelectedValue != "2");
            btn_UnApprove.Visible = btn_Approve.Visible;
        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gv_List_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow r in gv_List.Rows)
        {
            if (r.Cells[1].Text == "0")
                r.Cells[1].Text = "";
            else
                r.Cells[1].Text = "<a href='../EWF/TaskDetail.aspx?TaskID=" + r.Cells[1].Text + "' target='_blank' class='listViewTdLinkS1'>" + r.Cells[1].Text + "</a>";
            r.Cells[2].Text = "<a href='PM_PromotorDetail.aspx?PromotorID=" + r.Cells[2].Text + "' target='_blank' class='listViewTdLinkS1'>" + r.Cells[2].Text + "</a>";
        }
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        DoApprove(2, "汇总单批量审批通过!");
    }
    protected void btn_UnApprove_Click(object sender, EventArgs e)
    {
        DoApprove(3, "汇总单批量未能审批通过!");
    }
    private void DoApprove(int State, string remark)
    {
        foreach (GridViewRow gr in gv_List.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked)
            {
                int taskid = (int)gv_List.DataKeys[gr.RowIndex][0];
                if (taskid > 0)
                {
                    int jobid = EWF_TaskBLL.StaffCanApproveTask(taskid, (int)Session["UserID"]);
                    if (jobid > 0)
                    {
                        EWF_Task_JobBLL job = new EWF_Task_JobBLL(jobid);
                        if (job != null)
                        {
                            int decisionid = job.StaffCanDecide((int)Session["UserID"]);
                            if (decisionid > 0)
                            {
                                if (State == 2)
                                    job.Decision(decisionid, (int)Session["UserID"], 2, "汇总单批量审批通过!");         //2:审批已通过
                                else
                                    job.Decision(decisionid, (int)Session["UserID"], 3, "汇总单批量审批不通过!");       //3:审批不通过
                            }
                        }
                    }
                }
            }
        }
        BindGrid();
        MessageBox.Show(this, "审批成功！");
    }

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        string appcode = "";
        if (ddl_State.SelectedValue != "2") appcode = ddl_App.SelectedValue;
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);

        DataTable dt = PM_PromotorBLL.GetApproveList(organizecity, appcode, (int)Session["UserID"]);
        CreateExcel(dt, "Export-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
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
}
