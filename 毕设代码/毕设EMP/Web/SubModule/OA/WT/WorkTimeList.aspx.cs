using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using System.Data;
using MCSFramework.Common;

public partial class SubModule_OA_WT_index : System.Web.UI.Page
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
                if (viewstaff != null)
                {
                    //判断要查看的人所在的管理片区是否在当前人可管辖的范围之内
                    Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"], true);
                    DataTable dt = staff.GetStaffOrganizeCity();

                    if (dt.Select("ID=" + viewstaff.OrganizeCity).Length > 0)
                    {
                        select_Staff.SelectValue = viewstaff.ID.ToString();
                        select_Staff.SelectText = viewstaff.RealName;
                        MCSTabControl1.SelectedIndex = 1;
                    }
                }
            }
            BindGrid();
        }
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

        //ddl_JournalType.DataSource = DictionaryBLL.Dictionary_Data_GetAlllList("Type=908");
        //ddl_JournalType.DataBind();
        //ddl_JournalType.Items.Insert(0, new ListItem("请选择...", "0"));
    }
    #endregion

    private void BindGrid()
    {
        DateTime dt_begin = DateTime.Parse(tbx_begindate.Text);
        DateTime dt_end = DateTime.Parse(tbx_enddate.Text);

        string ConditionStr = "WT_WorkTime.WorkDate between '" + dt_begin.ToString("yyyy-MM-dd") + "' And '" + dt_end.ToString("yyyy-MM-dd") + " 23:59:59' ";

        //if (ddl_JournalType.SelectedValue != "0")
        //{
        //    ConditionStr += " AND JN_Journal.JournalType=" + ddl_JournalType.SelectedValue;
        //}

        if (MCSTabControl1.SelectedTabItem.Value == "0")
        {
            td_SelectStaff.Visible = false;
            td_OrganizeCity.Visible = false;
            //bt_Add.Enabled = true;
            ConditionStr += " AND WT_WorkTime.Staff=" + Session["UserID"].ToString();
        }
        else
        {
            td_SelectStaff.Visible = true;
            td_OrganizeCity.Visible = true;
            //bt_Add.Enabled = false;

            if (!string.IsNullOrEmpty(select_Staff.SelectValue))
            {
                ConditionStr += " AND WT_WorkTime.Staff=" + select_Staff.SelectValue;
            }
            else if (tr_OrganizeCity.Visible && tr_OrganizeCity.SelectValue != "0")
            {
                #region 判断当前可查询的范围
                string orgcitys = "";
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
                orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                if (orgcitys != "") ConditionStr += " AND WT_WorkTime.OrganizeCity IN (" + orgcitys + ") ";
                #endregion
            }
            else
            {
                MessageBox.Show(this, "请选择要查询的管理片区或员工！");
                return;
            }
        }

        ConditionStr += " Order by WT_WorkTime.WorkDate desc";
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
    }

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    //protected void bt_Add_Click(object sender, EventArgs e)
    //{
    //    Session["ClientID"] = null;
    //    Response.Redirect("JournalDetail.aspx");
    //}

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }
}
