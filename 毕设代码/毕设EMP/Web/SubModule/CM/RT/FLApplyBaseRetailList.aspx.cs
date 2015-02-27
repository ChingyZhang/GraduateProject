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
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.BLL.SVM;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using System.Collections.Generic;

public partial class SubModule_RT_FLApplyBaseRetailList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["AccountMonth"] = Request.QueryString["AccountMonth"] == null ? 0 : int.Parse(Request.QueryString["AccountMonth"]);
            BindDropDown();
            BindGrid();
            select_OrgSupplier.PageUrl = "../PopSearch/Search_SelectClient.aspx?ClientType=3&ExtCondition=\"MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,~|~,5)=2\"";

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
        ddl_FLType.DataSource = DictionaryBLL.GetDicCollections("RT_FLType");
        ddl_FLType.DataBind();
        ddl_FLType.Items.Insert(0, new ListItem("全部", "0"));

        ddl_FLTypeDetail.DataSource = DictionaryBLL.GetDicCollections("RT_FLType");
        ddl_FLTypeDetail.DataBind();

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Now.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = "0";
        if (ViewState["AccountMonth"] != null)
            ddl_Month.SelectedValue = ViewState["AccountMonth"].ToString();

    }
    #endregion

    private void BindGrid()
    {

        int city, month, ismyd, fltype, client;
        int.TryParse(tr_OrganizeCity.SelectValue, out city);
        month = AC_AccountMonthBLL.GetCurrentMonth() - 1;
        int.TryParse(ddl_IsMYD.SelectedValue, out ismyd);
        int.TryParse(ddl_FLType.SelectedValue, out fltype);

        string ConditionStr = "";
        DataTable dt = CM_FLApply_BaseBLL.GetByOrganizeCity(city, month, 0, ismyd, fltype);

        if (tbx_Condition.Text.Trim() != "")
            ConditionStr = ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";
        dt.DefaultView.RowFilter = ConditionStr;

        gv_List.DataSource = dt.DefaultView;
        gv_List.DataBind();

    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        CM_FLApply_BaseBLL bll = new CM_FLApply_BaseBLL(id);
        select_OrgSupplier.SelectText = new CM_ClientBLL(bll.Model.Client).Model.FullName;
        ddl_FLTypeDetail.SelectedValue = bll.Model.FLType.ToString();
        rbl_IsMYD.SelectedValue = bll.Model.ISMYD.ToString();

        tbx_Amount.Text = bll.Model.FLBase.ToString("0.###");
        select_OrgSupplier.Enabled = false;
        btn_AddDetail.Text = "修 改";
    }
    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
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

    protected void btn_AddDetail_Click(object sender, EventArgs e)
    {
        int RTcount = 0; decimal flbase = 0;

        if (!decimal.TryParse(tbx_Amount.Text.Trim(), out flbase))
        {
            MessageBox.Show(this, "请正确填写金额数！");
            tbx_Amount.Focus();
            return;
        }
        CM_FLApply_BaseBLL bll;
        if (gv_List.SelectedIndex >= 0)
        {
            bll = new CM_FLApply_BaseBLL(int.Parse(gv_List.DataKeys[gv_List.SelectedIndex][0].ToString()));
            bll.Model.FLBase = flbase;
            bll.Model.FLType = int.Parse(ddl_FLTypeDetail.SelectedValue);
            bll.Model.ISMYD = int.Parse(rbl_IsMYD.SelectedValue);
            if (rbl_IsMYD.SelectedValue == "1")
                bll.Model.RTCount = RTcount;
            bll.Update();
            btn_AddDetail.Text = "新 增";
        }
        else
        {
            if (select_OrgSupplier.SelectValue == "" || select_OrgSupplier.SelectValue == "0")
            {
                MessageBox.Show(this, "请选择对应的零售商！");
                return;
            }
            if (CM_FLApply_BaseBLL.GetModelList("Client=" + select_OrgSupplier.SelectValue + " AND ISNULL(AccountMonth," + ddl_Month.SelectedValue + ")=" + ddl_Month.SelectedValue).Count > 0)
            {
                MessageBox.Show(this, "对不起，该门店的已在列表中存在，请重新选择门店！");
                return;
            }
            bll = new CM_FLApply_BaseBLL();
            bll.Model.AccountMonth = int.Parse(ddl_Month.SelectedValue);
            bll.Model.Client = int.Parse(select_OrgSupplier.SelectValue);
            bll.Model.FLBase = flbase;
            bll.Model.FLType = int.Parse(ddl_FLTypeDetail.SelectedValue);
            bll.Model.ISMYD = int.Parse(rbl_IsMYD.SelectedValue);
            if (rbl_IsMYD.SelectedValue == "1")
                bll.Model.RTCount = RTcount;
            bll.Add();
        }
        select_OrgSupplier.SelectText = "";
        select_OrgSupplier.SelectValue = "";

        tbx_Amount.Text = "0";
        select_OrgSupplier.Enabled = true;
        btn_AddDetail.Text = "新 增";
        gv_List.SelectedIndex = -1;
        BindGrid();
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.RowIndex][0];
        CM_FLApply_BaseBLL bll = new CM_FLApply_BaseBLL(id);
        bll.Delete(id);
        gv_List.SelectedIndex = -1;
        BindGrid();
    }
    protected void rbl_IsMYD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void select_OrgSupplier_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        int client = 0;
        int.TryParse(select_OrgSupplier.SelectValue, out client);
        CM_ClientBLL _bll = new CM_ClientBLL(client);
        if (_bll.Model != null)
        {
            rbl_IsMYD.SelectedValue = _bll.Model["Classification"] == "4" ? "1" : "2";
        }

    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0 && gv_List.Rows.Count >= e.Row.RowIndex && gv_List.Columns.Count >= 1)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex][0];
            CM_FLApply_BaseBLL bll = new CM_FLApply_BaseBLL(id);
            int month = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-1));
            IList<FNA_FeeApplyDetail> _list = new FNA_FeeApplyBLL().GetDetail("Client=" + bll.Model.Client.ToString()
                + " AND AccountTitle=82 AND BeginMonth=" + month.ToString()
                + "AND EXISTS(SELECT 1 FROM MCS_FNA.dbo.FNA_FeeApply WHERE FNA_FeeApply.ID=FNA_FeeApplyDetail.ApplyID AND State IN (1,2,3))");
            if (e.Row.RowType == DataControlRowType.DataRow && _list.Count > 0 || bll.Model.ISMYD == 2)
            {
                e.Row.Cells[0].Enabled = false; e.Row.Cells[gv_List.Columns.Count-1].Enabled = false;

                if (e.Row.FindControl("tbx_RTCount") != null)
                {
                    (e.Row.FindControl("tbx_RTCount") as TextBox).Enabled = false;
                }
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int id = 0, RTcount=0;
        CM_FLApply_BaseBLL bll;
        foreach (GridViewRow row in gv_List.Rows)
        {
            id = (int)gv_List.DataKeys[row.RowIndex][0];
            bll = new CM_FLApply_BaseBLL(id);
            if (bll.Model.ISMYD == 1 && row.FindControl("tbx_RTCount") != null && !int.TryParse((row.FindControl("tbx_RTCount") as TextBox).Text.Trim(), out RTcount))
            {
                MessageBox.Show(this, "请先填写【"+row.Cells[3].Text+"】本月进货门店数再保存");
                break;
            }

            bll.Model.RTCount = RTcount;
            bll.Update();


        }
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
