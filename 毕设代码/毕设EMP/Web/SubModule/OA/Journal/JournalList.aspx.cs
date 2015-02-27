// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_OA_Journal_JournalList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();

            if (Request.QueryString["Date"] == null)
            {
                tbx_begindate.Text = DateTime.Now.ToString("yyyy-MM-01");
                tbx_enddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                tbx_begindate.Text = Request.QueryString["Date"];
                tbx_enddate.Text = Request.QueryString["Date"];
            }

            if (Request.QueryString["Staff"] != null)
            {
                Org_Staff viewstaff = new Org_StaffBLL(int.Parse(Request.QueryString["Staff"])).Model;
                if (viewstaff != null) tbx_StaffName.Text = viewstaff.RealName;
                tbl_Condition.Visible = true;
                bt_Add.Enabled = false;
                MCSTabControl1.SelectedIndex = 1;
            }

            BindGrid();
        }

        #region 注册脚本
        string script = "function OpenJournal(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?ID='+id+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenJournal", script, true);

        script = "function NewJournal(d){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('JournalDetail.aspx?Day='+d+'&tempid='+tempid, window, 'dialogWidth:860px;DialogHeight=600px;status:no');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NewJournal", script, true);
        #endregion
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
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

        ddl_JournalType.DataSource = DictionaryBLL.GetDicCollections("OA_JournalType");
        ddl_JournalType.DataBind();
        ddl_JournalType.Items.Insert(0, new ListItem("请选择...", "0"));

        #region 绑定职位
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 22, "ViewAllStaffJN"))
        {
            //有【查看所有员工工作日志】权限
            tr_Position.RootValue = "0";
            tr_Position.SelectValue = "0";
        }
        else
        {
            tr_Position.RootValue = staff.Model.Position.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        #endregion

        #endregion
    }
    #endregion

    private void BindGrid()
    {
        DateTime dt_begin = DateTime.Parse(tbx_begindate.Text);
        DateTime dt_end = DateTime.Parse(tbx_enddate.Text);

        string ConditionStr = "JN_Journal.BeginTime <= '" + dt_end.ToString("yyyy-MM-dd") + "' And JN_Journal.EndTime >='" + dt_begin.ToString("yyyy-MM-dd") + "'";

        if (ddl_JournalType.SelectedValue != "0")
        {
            ConditionStr += " AND JN_Journal.JournalType=" + ddl_JournalType.SelectedValue;
        }

        if (MCSTabControl1.SelectedTabItem.Value == "0")
        {
            ConditionStr += " AND JN_Journal.Staff=" + Session["UserID"].ToString();
        }
        else
        {
            if (tbx_StaffName.Text != "") ConditionStr += "AND Org_Staff.RealName LIKE '%" + tbx_StaffName.Text + "%'";

            #region 只显示当前员工的所有下级职位的员工计划
            if (tr_Position.SelectValue != "0")
            {
                if (cb_IncludeChild.Checked || tr_Position.SelectValue == tr_Position.RootValue)
                {
                    Org_PositionBLL p = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                    string positions = p.GetAllChildPosition();

                    if (tr_Position.SelectValue != tr_Position.RootValue)
                    {
                        if (positions != "") positions += ",";
                        positions += tr_Position.SelectValue;
                    }

                    ConditionStr += " AND Org_Staff.Position IN (" + positions + ")";
                }
                else
                    ConditionStr += " AND Org_Staff.Position = " + tr_Position.SelectValue;
            }
            #endregion

            #region 判断当前可查询的管理片区范围
            if (tr_OrganizeCity.SelectValue != "1")
            {
                string orgcitys = "";
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                if (orgcitys != "") ConditionStr += " AND JN_Journal.OrganizeCity IN (" + orgcitys + ") ";
            }
            #endregion
        }

        gv_List.OrderFields = "JN_Journal_BeginTime";
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        if (e.Index == 0)
        {
            tbl_Condition.Visible = false;
            bt_Add.Enabled = true;
        }
        else
        {
            tbl_Condition.Visible = true;
            bt_Add.Enabled = false;
        }

        BindGrid();
    }
    protected void bt_CalendarView_Click(object sender, EventArgs e)
    {
        Response.Redirect("JournalCalendar.aspx");
    }
    protected void gv_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}