// ===================================================================
// 文件路径:SubModule/RM/RetailerList.aspx.cs 
// 生成日期:2007-12-29 14:26:38 
// 作者:	  
// ===================================================================
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Retailer_Pop_Search_SelectClient : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindDropDown();

            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ClientType = 3 ";

        if ((int)Session["OwnerType"] == 2)
        {
            ConditionStr += @" AND CM_Client.ID IN (SELECT Client FROM MCS_CM.dbo.CM_ClientSupplierInfo INNER JOIN MCS_CM.dbo.CM_Client s ON
               CM_ClientSupplierInfo.Supplier = s.ID WHERE OwnerClient=" + Session["OwnerClient"].ToString() + " )";
        }
        else if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += @" AND CM_Client.ID IN (SELECT Client FROM MCS_CM.dbo.CM_ClientSupplierInfo WHERE Supplier=" + Session["OwnerClient"].ToString() + " )";
        }
        ConditionStr += " AND CM_Client.ClientType = 3";

        if (tbx_Condition.Text.Trim() != "")
        {
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";
        }

        if (Request.QueryString["ExtCondition"] != null)
        {
            ConditionStr += " AND (" + Request.QueryString["ExtCondition"].Replace("\"", "").Replace('~', '\'') + ")";
        }
        
        ConditionStr += " Order by MCS_CM.dbo.CM_Client.FullName";

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();        
    }

    #region 选中等事件

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        tbx_value.Text = gv_List.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        tbx_text.Text = gv_List.DataKeys[e.NewSelectedIndex].Values[1].ToString();
    }

    #endregion

    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }
}
