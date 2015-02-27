using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Promotor;

public partial class SubModule_PM_PM_GetAnalysisOverview : System.Web.UI.Page
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
        if (Request.QueryString["OrganizeCity"] != null)
        {
            tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"].ToString();
        }
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1).ToString();
        if (Request.QueryString["AccountMonth"] != null)
        {
            ddl_Month.SelectedValue = Request.QueryString["AccountMonth"].ToString();
        }
    }
    #endregion

    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        DataTable dt = PM_PromotorBLL.GetAnalysisOverview(organizecity, month);
        if (dt.Columns.Count > 0)
        {
            dt.Columns.Remove("Promotor");
            dt.Columns.Remove("ClientID");
            dt.Columns.Remove("BPMID");
            dt.Columns.Remove("BRTID");
            dt.Columns.Remove("取消人");
            dt.Columns.Remove("取消人职务");
            dt.Columns.Remove("取消时间");
        }
       // ViewState["dtSummary"] = dt;
        gv_List.DataSource = dt;
        gv_List.DataBind();
        if (dt.Columns.Count >= 24)
            gv_List.Width = new Unit(dt.Columns.Count * 65);
        else
            gv_List.Width = new Unit(100, UnitType.Percentage);
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
    
        string filename = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes("导购投产明细导出_" + DateTime.Now.ToString("yyyyMMddHHmmss")));
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        DataTable dt = PM_PromotorBLL.GetAnalysisOverview(organizecity, month);
        if (dt.Rows.Count>0)
        {
            dt.Columns.Remove("Promotor");
            dt.Columns.Remove("ClientID");
            dt.Columns.Remove("BPMID");
            dt.Columns.Remove("BRTID");
            dt.Columns.Remove("取消人");
            dt.Columns.Remove("取消人职务");
            dt.Columns.Remove("取消时间");
            CreateExcel(dt, filename);
        }
    }

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
   

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (MCSTabControl1.SelectedIndex < 4)
        {
            Response.Redirect("PM_PromotorSummary.aspx?AccountMonth=" + ddl_Month.SelectedValue + "&OrganizeCity=" + tr_OrganizeCity.SelectValue);
        }
    }
}
