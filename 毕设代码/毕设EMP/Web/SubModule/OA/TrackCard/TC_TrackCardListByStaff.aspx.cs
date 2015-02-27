using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text ;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.OA;
using System.Data;
using MCSFramework.Common;

public partial class SubModule_OA_TrackCard_TC_TrackCardListByStaff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取参数
            ViewState["OrganizeCity"] = Request.QueryString["OrganizeCity"] == null ? 0 : int.Parse(Request.QueryString["OrganizeCity"]);
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            ViewState["Staff"] = Request.QueryString["Staff"] == null ? 0 : int.Parse(Request.QueryString["Staff"]);
            #endregion

            BindDropDown();

            if ((int)ViewState["OrganizeCity"] > 0) tr_OrganizeCity.SelectValue = ViewState["OrganizeCity"].ToString();
            if ((int)ViewState["AccountMonth"] > 0) ddl_AccountMonth.SelectedValue = ViewState["AccountMonth"].ToString();
            if ((int)ViewState["Staff"] > 0)
            {
                Org_StaffBLL staff = new Org_StaffBLL((int)ViewState["Staff"]);
                if (staff.Model != null)
                {
                    select_Staff.SelectValue = staff.Model.ID.ToString();
                    select_Staff.SelectText = staff.Model.RealName;
                    tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
                }
            }

            if (select_Staff.SelectValue != "") BindGrid();
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

            select_Staff.SelectValue = staff.Model.ID.ToString();
            select_Staff.SelectText = staff.Model.RealName;
        }
        #endregion

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<=DateAdd(day,0,GETDATE())");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();


        ddl_ISWanYuan.DataSource = DictionaryBLL.GetDicCollections("PUB_YesOrNo");
        ddl_ISWanYuan.DataBind();
        ddl_ISWanYuan.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ISWanYuan.SelectedValue = "0";

    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_OrganizeCity.SelectValue != "0")
        {
            select_Staff.PageUrl = "~/SubModule/StaffManage/Pop_Search_Staff.aspx?OrganizeCity=" + tr_OrganizeCity.SelectValue;
        }
    }

    protected void bt_Load_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    private void BindGrid()
    {
        int month = 0, organizecity = 0, staff = 0;         
        if (int.TryParse(tr_OrganizeCity.SelectValue, out organizecity) && int.TryParse(ddl_AccountMonth.SelectedValue, out month))
        {
            int.TryParse(select_Staff.SelectValue, out staff);        //员工不选时，查看该片区所有人员跟踪表

            DataTable dt = TC_TrackCardBLL.GetListByStaff(month, organizecity, staff, ddl_ISWanYuan.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                #region 矩阵表
                dt = MatrixTable.Matrix(dt, new string[] { "CityName1","CityName2","CityName3","CityName4","CityName5", 
                "StaffName", "ClientName","ShortName", "IsPromote", "Promotor", "Salesroom", "PreMonthSalesVolume",
                "TargetDate01", "TargetDate02", "TargetDate04", "TargetDate03"}, new string[] { "D", "TrackDate" },
                  new string[] { "Data01", "Data02", "Data04","Data03"  }, false, true);

                dt = MatrixTable.ColumnSummaryTotal(dt, new int[] { 1 }, new string[] { "Data01", "Data02", "Data04", "Data03" });

                #endregion

                #region 加入行小计
                MatrixTable.TableAddRowSubTotal_Matric(dt, new string[]{ "CityName1","CityName2","CityName3","CityName4","CityName5", 
                "StaffName"}, 11, new string[] { "Data01", "Data02", "Data04", "Data03" }, true);
                #endregion 

                #region 统计完成率
                dt.Columns.Add(new DataColumn("完成率%→Data01", Type.GetType("System.Int32"), "IIF([TargetDate01]=0,0,[合计→Data01]/[TargetDate01])*100"));
                dt.Columns.Add(new DataColumn("完成率%→Data02", Type.GetType("System.Int32"), "IIF([TargetDate02]=0,0,[合计→Data02]/[TargetDate02])*100"));
                dt.Columns.Add(new DataColumn("完成率%→Data04", Type.GetType("System.Int32"), "IIF([TargetDate04]=0,0,[合计→Data04]/[TargetDate04])*100"));
                dt.Columns.Add(new DataColumn("完成率%→Data03", Type.GetType("System.Int32"), "IIF([TargetDate03]=0,0,[合计→Data03]/[TargetDate03])*100"));
                #endregion

                #region 统计排名
                dt.Columns.Add(new DataColumn("排名→Data01", Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("排名→Data02", Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("排名→Data04", Type.GetType("System.Int32")));
                dt.Columns.Add(new DataColumn("排名→Data03", Type.GetType("System.Int32")));

                DataView dv = new DataView(dt);
                dv.RowFilter = "CityName1<>'总计' AND CityName2<>'小计' AND CityName3<>'小计' AND CityName4<>'小计' AND CityName5<>'小计' AND StaffName<>'小计' AND ClientName<>'小计'";
                int prevalue = -1, presort = 0;

                #region Data01完成率排名
                dv.Sort = "[完成率%→Data01] DESC";
                for (int i = 0; i < dv.Count; i++)
                {
                    if ((int)dv[i]["完成率%→Data01"] == prevalue)
                    {
                        dv[i]["排名→Data01"] = presort;
                    }
                    else
                    {
                        dv[i]["排名→Data01"] = i + 1;
                        prevalue = (int)dv[i]["完成率%→Data01"];
                        presort = i + 1;
                    }
                }
                #endregion

                #region Data02完成率排名
                dv.Sort = "[完成率%→Data02] DESC";
                for (int i = 0; i < dv.Count; i++)
                {
                    if ((int)dv[i]["完成率%→Data02"] == prevalue)
                    {
                        dv[i]["排名→Data02"] = presort;
                    }
                    else
                    {
                        dv[i]["排名→Data02"] = i + 1;
                        prevalue = (int)dv[i]["完成率%→Data02"];
                        presort = i + 1;

                    }
                }
                #endregion

                #region Data04完成率排名
                dv.Sort = "[完成率%→Data04] DESC";
                for (int i = 0; i < dv.Count; i++)
                {
                    if ((int)dv[i]["完成率%→Data04"] == prevalue)
                    {
                        dv[i]["排名→Data04"] = presort;
                    }
                    else
                    {
                        dv[i]["排名→Data04"] = i + 1;
                        prevalue = (int)dv[i]["完成率%→Data04"];
                        presort = i + 1;

                    }
                }
                #endregion

                #region Data03完成率排名
                dv.Sort = "[完成率%→Data03] DESC";
                for (int i = 0; i < dv.Count; i++)
                {
                    if ((int)dv[i]["完成率%→Data03"] == prevalue)
                    {
                        dv[i]["排名→Data03"] = presort;
                    }
                    else
                    {
                        dv[i]["排名→Data03"] = i + 1;
                        prevalue = (int)dv[i]["完成率%→Data03"];
                        presort = i + 1;

                    }
                }
                #endregion
                #endregion


                #region 列表字段名称替换
                dt.Columns["CityName1"].ColumnName = "部";
                dt.Columns["CityName2"].ColumnName = "省区";
                dt.Columns["CityName3"].ColumnName = "区域";
                dt.Columns["CityName4"].ColumnName = "城市";
                dt.Columns["CityName5"].ColumnName = "县城";

                dt.Columns["ClientName"].ColumnName = "门店全称";
                dt.Columns["ShortName"].ColumnName = "门店简称";
                dt.Columns["StaffName"].ColumnName = "员工";
                dt.Columns["IsPromote"].ColumnName = "促销店";
                dt.Columns["Promotor"].ColumnName = "促销员";
                dt.Columns["Salesroom"].ColumnName = "门店容量(元)";
                dt.Columns["PreMonthSalesVolume"].ColumnName = "上月销量";

                dt.Columns["TargetDate01"].ColumnName = "任务→ →销量(元)";
                dt.Columns["TargetDate02"].ColumnName = "任务→ →档案数(自抢档案)";
                dt.Columns["TargetDate04"].ColumnName = "任务→ →档案数(NE提供档案)";
                dt.Columns["TargetDate03"].ColumnName = "任务→ →送货上门数";

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName.EndsWith("→Data01")) c.ColumnName = c.ColumnName.Replace("→Data01", "→销量(元)");
                    if (c.ColumnName.EndsWith("→Data02")) c.ColumnName = c.ColumnName.Replace("→Data02", "→档案数(自抢档案)");
                    if (c.ColumnName.EndsWith("→Data04")) c.ColumnName = c.ColumnName.Replace("→Data04", "→档案数(NE提供档案)");
                    if (c.ColumnName.EndsWith("→Data03")) c.ColumnName = c.ColumnName.Replace("→Data03", "→送货上门数");
                }
                #endregion


                gv_List.DataSource = dt;
                gv_List.DataBind();
                gv_List.Width = new Unit(dt.Columns.Count * 50);
                MatrixTable.GridViewMatric(gv_List);
            }
            else
                gv_List.DataBind();
        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
   
    protected void bt_TrackEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("TC_TrackEdit.aspx");
    }
    protected void bt_Export_Click(object sender, EventArgs e)
    {
        gv_List.AllowPaging = false;
        BindGrid();
        string FileType = "application/ms-excel";
        string FileName = "Export_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xls";
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, Encoding.UTF8).ToString());
        Response.ContentType = FileType;
        this.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        gv_List.RenderControl(hw);
        Response.Write(tw.ToString());
        Response.End();
        gv_List.AllowPaging = true;
        BindGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}
