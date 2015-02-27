// ===================================================================
// 文件路径:SubModule/CAT/CAT_ActivityList.aspx.cs 
// 生成日期:2009/12/23 21:28:58 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;

public partial class SubModule_CAT_CAT_ActivityList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["Index"] != null)
            {
                MCSTabControl1.SelectedIndex = int.Parse(Request.QueryString["Index"]) - 1;
            }

            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");

            select_Staff.SelectValue = Session["UserID"].ToString();
            select_Staff.SelectText = Session["UserRealName"].ToString();

            BindDropDown();
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

        ddl_Classify.DataSource = DictionaryBLL.GetDicCollections("CAT_Classify");
        ddl_Classify.DataBind();
        ddl_Classify.Items.Insert(0, new ListItem("请选择...", "0"));

    }
    #endregion

    private void BindGrid()
    {
        if (!MCSTabControl1.SelectedTabItem.Visible)
        {
            MessageBox.Show(this, "请先选择要查询的活动的状态!");
            return;
        }
        gv_List.ConditionString = GetCondition() ;
        gv_List.BindGrid();
    }

    private string GetCondition()
    {
       
        #region 组织查询条件
        string ConditionStr = "CAT_Activity.State = " + MCSTabControl1.SelectedTabItem.Value;

        ConditionStr += " AND " + ddl_SelectDateMethod.SelectedValue + " BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59' ";

        #region 判断当前可查询管理片区的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND CAT_Activity.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion


        //活动主题
        if (tbx_Topic.Text.Trim() != "")
        {
            ConditionStr += " AND CAT_Activity.Topic like '%" + this.tbx_Topic.Text.Trim() + "%' ";
        }

        //活动分类
        if (ddl_Classify.SelectedValue != "0")
        {
            ConditionStr += " AND CAT_Activity.Classify =" + ddl_Classify.SelectedValue;
        }

        //录入人员
        if (select_Staff.SelectValue != "")
        {
            ConditionStr += " AND CAT_Activity.InsertStaff IN (" + select_Staff.SelectValue + ")";
        }

        //举办客户
        if (select_Client.SelectValue != "")
        {
            ConditionStr += " AND CAT_Activity.StageClient = " + select_Client.SelectValue;
        }
        return ConditionStr;
        #endregion
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("CAT_ActivityDetail.aspx?State=11");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add2_Click(object sender, EventArgs e)
    {
        Response.Redirect("CAT_ActivityDetail.aspx?State=1");
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        gv_List.Columns[0].Visible = false;
        gv_List.ConditionString = GetCondition();
        gv_List.BindGrid();
        ToExcel(gv_List, "ExtportFile.xls");
        gv_List.AllowPaging = true;
        gv_List.Columns[0].Visible = false;
        gv_List.BindGrid();
    }

    private void ToExcel(Control ctl, string FileName)
    {
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

}