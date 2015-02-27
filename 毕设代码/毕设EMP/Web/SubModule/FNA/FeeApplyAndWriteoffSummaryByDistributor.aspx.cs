using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Common;
using System.Data;
using MCSFramework.BLL.FNA;
using MCSFramework.BLL.Pub;

public partial class SubModule_FNA_FeeApplyAndWriteoffSummaryByDistributor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取当前员工的关联经销商
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                ViewState["ClientID"] = _relateclient;
                select_Client.Enabled = false;
            }
            else
            {
                if (Request.QueryString["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                    Session["ClientID"] = ViewState["ClientID"];
                }
                else if (Session["ClientID"] != null)
                {
                    ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
                }
            }
            #endregion

            BindDropDown();

            if (ViewState["ClientID"] != null)      
            {
                CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.FullName;
                if (client.ClientType != 2)
                {
                    MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
                }
                BindGrid();
            }
            else
            {
                MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            }
        }

    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_BeginMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_BeginMonth.DataBind();
        ddl_BeginMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddMonths(-3)).ToString();

        ddl_EndMonth.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<GETDATE() AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_EndMonth.DataBind();
        ddl_EndMonth.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));
    }
    #endregion

    private void BindGrid()
    {
        ViewState["ClientID"] = int.Parse(select_Client.SelectValue);

        if (MCSTabControl1.SelectedIndex == 0)
        {
            DataTable dtSummary = FNA_FeeApplyBLL.GetSummaryTotalByDistributor((int)ViewState["ClientID"], int.Parse(ddl_BeginMonth.SelectedValue),
                int.Parse(ddl_EndMonth.SelectedValue), int.Parse(ddl_FeeType.SelectedValue));
            if (dtSummary.Rows.Count == 0)
            {
                gv_List.DataBind();
                return;
            }

            dtSummary = MatrixTable.Matrix(dtSummary, new string[] { "客户名称" },
                       new string[] { "FeeTypeName", "AccountTitleName" }, "ApplyCost", true, true);
            dtSummary = MatrixTable.ColumnSummaryTotal(dtSummary, new int[] { 1 }, new string[] { "ApplyCost" });

            gv_List.DataSource = dtSummary;
            gv_List.DataBind();

            MatrixTable.GridViewMatric(gv_List);
        }
        else
        {
            string condition = " FNA_FeeApply.State =3 ";

            #region 组织查询条件
            //管理片区及所有下属管理片区
            CM_Client client = new CM_ClientBLL((int)ViewState["ClientID"]).Model;
            int city = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OrganizeCity", client.OrganizeCity, 2);
            if (city != 1)
            {
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(city);
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += city.ToString();

                condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
            }

            //会计月条件
            condition += " AND FNA_FeeApply.AccountMonth BETWEEN " + ddl_BeginMonth.SelectedValue + " AND " + ddl_EndMonth.SelectedValue;

            //费用类型
            if (ddl_FeeType.SelectedValue != "0")
            {
                condition += " AND FNA_FeeApply.FeeType = " + ddl_FeeType.SelectedValue;
            }           

            //指定经销商
            condition += " AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE MCS_CM.dbo.uf_GetDistributorIDByClient(FNA_FeeApplyDetail.Client) =" + ViewState["ClientID"].ToString() + ")";
            #endregion

            gv_ListDetail.ConditionString = condition;
            gv_ListDetail.BindGrid();
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        gv_ListDetail.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        gv_List.Visible = MCSTabControl1.SelectedIndex == 0;
        gv_ListDetail.Visible = !gv_List.Visible;

        gv_List.PageIndex = 0;
        gv_ListDetail.PageIndex = 0;
        BindGrid();
    }
    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
