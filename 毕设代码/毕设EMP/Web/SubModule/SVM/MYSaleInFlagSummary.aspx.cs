using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.SVM;
using System.Text;
using System.IO;
using System.Data;

public partial class SubModule_SVM_MYSaleInFlagSummary : System.Web.UI.Page
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
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate <= GETDATE() AND YEAR >= 2013");
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate <= GETDATE() AND YEAR >= 2013");
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = (AC_AccountMonthBLL.GetMonthByDate(DateTime.Now) - 1).ToString();

        #region 绑定用户可管辖的管理片区
        if (Session["UserID"] != null)
        {
            int staffid = (int)Session["UserID"];
            Org_StaffBLL staff = new Org_StaffBLL(staffid);

            //select_Staff.SelectText = staff.Model.RealName;
            //select_Staff.SelectValue = staffid.ToString();

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

        }
        #endregion

    }

    private DataTable GetExtTable()
    {
        int _beginmonth = int.Parse(ddl_BeginMonth.SelectedValue);
        int _endmonth = int.Parse(ddl_EndMonth.SelectedValue);
        int _city = 0;
        if (tr_OrganizeCity.SelectValue != "")
            _city = int.Parse(tr_OrganizeCity.SelectValue);
        string Condition = "";
        int _staff = 0;
        if (select_Staff.SelectValue != "")
            _staff = int.Parse(select_Staff.SelectValue);

        //if (ddl_DI.SelectedValue == "0")
        //{
            if (select_DI.SelectValue != "0" && select_DI.SelectValue != "")
            {
                Condition += " AND Supplier.ID =" + select_DI.SelectValue;
            }
        //}
        //else
        //{
        //    if (tbx_DICode.Text != "")
        //    {
        //        Condition += (ddl_DI.SelectedValue == "1" ? " AND Supplier.FullName" : " AND Supplier.Code") + " LIKE '%" + tbx_DICode.Text.Trim() + "%'";
        //    }
        //}
        //if (ddl_RT.SelectedValue == "0")
        //{
            if (select_RT.SelectValue != "0" && select_RT.SelectValue != "")
            {
                Condition += " AND CM_Client.ID =" + select_RT.SelectValue;
            }
        //}
        //else
        //{
        //    if (tbx_RTCode.Text != "")
        //    {
        //        Condition += (ddl_RT.SelectedValue == "1" ? " AND CM_Client.FullName" : " AND CM_Client.Code") + " LIKE '%" + tbx_RTCode.Text.Trim() + "%'";
        //    }
        //}

        return SVM_MYSaleInFlagBLL.GetRPTSummary(_beginmonth, _endmonth, _city, _staff ,Condition);
    }

    private void BindGrid()
    {
        gv_ListDetail.DataSource = GetExtTable();
        gv_ListDetail.BindGrid();
    }
    //protected void ddl_DI_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    select_DI.Visible = ddl_DI.SelectedIndex == 0;
    //    tbx_DICode.Visible = ddl_DI.SelectedIndex != 0;
    //}
    //protected void ddl_RT_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    select_RT.Visible = ddl_RT.SelectedIndex == 0;
    //    tbx_RTCode.Visible = ddl_RT.SelectedIndex != 0;
    //}
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {

        string filename = HttpUtility.UrlEncode("月度母婴进货门店明细查询_" + DateTime.Now.ToString("yyyyMMddHHmmss"));

        CreateExcel(GetExtTable(), filename);
        

    }
    private void CreateExcel(DataTable dt, string filename)
    {
        HttpResponse resp;
        resp = Page.Response;
        resp.Charset = "UTF-8";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
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
    protected void gv_ListDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_ListDetail.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }
}
