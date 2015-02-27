using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.BLL.SVM;
using System.Data;
using MCSFramework.Common;
using System.Text;
using System.IO;
using MCSFramework.BLL.CM;

public partial class SubModule_SVM_EndDateInventorySummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
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

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-10)).ToString();

        ddl_ActiveFlag.DataSource = DictionaryBLL.GetDicCollections("CM_ActiveFlag");
        ddl_ActiveFlag.DataBind();
        ddl_ActiveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ActiveFlag.SelectedValue = "1";
        
    }
    private void BindGrid()
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);   
        int iscxp = int.Parse(ddl_IsCXP.SelectedValue);
        int actvieflag;
        int.TryParse(ddl_ActiveFlag.SelectedValue, out actvieflag);

        DataTable dt_summary = SVM_InventoryBLL.GetOPIOverview(organizecity, month, iscxp, actvieflag);
      
        gv_Summary.DataSource = dt_summary;
        gv_Summary.DataBind();
        if (dt_summary.Columns.Count >= 24)
            gv_Summary.Width = new Unit(dt_summary.Columns.Count * 65);
        else
            gv_Summary.Width = new Unit(100, UnitType.Percentage);
    }
    protected void gv_Summary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gv_Summary.HeaderRow != null)
        {
            foreach (GridViewRow r in gv_Summary.Rows)
            {
                #region 金额数据格式化
                if (r.Cells.Count > 1)
                {
                    for (int i = 1; i < r.Cells.Count; i++)
                    {
                        decimal d;
                        if (decimal.TryParse(r.Cells[i].Text, out d))
                        {
                            r.Cells[i].Text = d.ToString("#,#.##");
                        }
                    }
                }
                #endregion
            }
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
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
   

    protected void bt_Export_Click(object sender, EventArgs e)
    {
        int month = int.Parse(ddl_Month.SelectedValue);
        int organizecity = int.Parse(tr_OrganizeCity.SelectValue);
        int iscxp = int.Parse(ddl_IsCXP.SelectedValue);
        int actvieflag;
        int.TryParse(ddl_ActiveFlag.SelectedValue, out actvieflag);

        DataTable dt_summary = SVM_InventoryBLL.GetOPIOverview(organizecity, month, iscxp, actvieflag);
      
        CreateExcel(dt_summary,HttpUtility.UrlEncode(Encoding.UTF8.GetBytes("20号库存汇总单导出_" + DateTime.Now.ToString("yyyyMMddHHmmss"))));

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
    
    protected void gv_Summary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Summary.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
