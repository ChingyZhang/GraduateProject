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

public partial class SubModule_Store_StoreList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            BindDropDown();
            Session["ClientID"] = null;
            //Session["MCSMenuControl_FirstSelectIndex"] = "17";
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

    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " MCS_CM.dbo.CM_Client.ClientType = 1 ";
        if (tbx_Condition.Text.Trim() != "")
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";

        string orgcitys = "";

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue), true);
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND MCS_CM.dbo.CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        ConditionStr += "Order by MCS_CM.dbo.CM_Client.Code";
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();


    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Session["ClientID"] = id;
        if (Request.QueryString["URL"] != null) Response.Redirect(Request.QueryString["URL"]);
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Session["ClientID"] = null;
        Response.Redirect("StoreDetail.aspx?Mode=New");
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询

                break;
            case "1"://高级查询
                Response.Redirect("AdvanceFind.aspx");
                break;
        }
    }

    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        //    DataTable dt = Retailer.GetAllList(int.Parse(Session["UserID"].ToString()));;
        //    //获取数据源
        //    if (gv_List != null && gv_List.Rows.Count > 0)
        //    {
        //        string sFileName = "Export_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
        //        System.IO.StringWriter sw = new System.IO.StringWriter();
        //        sw.WriteLine("门店编号	区域	城市	客户名称	门店名称	电话	客户渠道	活跃状态	门店分类	销售代表");
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string str = "";
        //            str = dt.Rows[i]["Code"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesTerritoryName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesCityName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ClientName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["Name"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["TeleNum"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ChannelName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ActiveStatusName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["ClassifyName"].ToString().Replace("	", "");
        //            str += "	" + dt.Rows[i]["SalesPersonName"].ToString().Replace("	", "");
        //            sw.WriteLine(str);
        //        }
        //        Response.Buffer = true;
        //        Response.Charset = "gb2312";
        //        Response.AppendHeader("Content-Disposition", "attachment;filename=" + sFileName);
        //        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//设置输出流为简体中文
        //        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //        Response.Write(sw);
        //        sw.Close();
        //        Response.End();
        //    }
        //    else
        //    {
        //        MessageBox.Show(this, "无数据待导出！");
        //        return;
        //    }
    }
}
