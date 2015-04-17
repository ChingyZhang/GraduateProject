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

public partial class SubModule_RM_RetailerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Today.AddYears(-5).ToString("yyyy-01-01");
            tbx_end.Text = DateTime.Today.ToString("yyyy-MM-dd");

            BindDropDown();
            Session["ClientType"] = null;
            Session["MCSMenuControl_FirstSelectIndex"] = "11";
            if (Session["OwnerClient"] != null)
            {
                int client = (int)Session["OwnerClient"];
                BindGrid();
            }

        }
        string script = "function PopShow(id){\r\n";
        script += "var tempid = Math.random() * 10000; \r\n window.showModalDialog('" + Page.ResolveClientUrl("../Map/ClientInMap.aspx") +
            "?ClientID=' + id + '&tempid='+tempid, window, 'dialogWidth:800px;DialogHeight=550px;status:yes;resizable=yes');}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PopShow", script, true);
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

        ddl_ActiveFlag.DataSource = DictionaryBLL.GetDicCollections("CM_CooperateState");
        ddl_ActiveFlag.DataBind();
        ddl_ActiveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ActiveFlag.SelectedValue = "1";

    }
    #endregion

    private void BindGrid()
    {
        string condition = " CM_Client.ClientType=3 AND CM_Client.OwnerClient=" + Session["OwnerClient"];

        if (ddl_ActiveFlag.SelectedValue != "0")
        {
            condition += " AND CM_Client.ID IN (SELECT Client FROM MCS_CM.dbo.CM_ClientSupplierInfo WHERE Supplier=" + Session["OwnerClient"] + " AND State = " + ddl_ActiveFlag.SelectedValue + ")";
        }


        if (tbx_Condition.Text != "" && tbx_Condition.Text != "输入客户编号/ 名称/ 联系人/ 电话查询")
        {
            condition += " AND ( EXISTS(SELECT 1 FROM MCS_CM.dbo.CM_ClientSupplierInfo WHERE Supplier=" + Session["OwnerClient"]
            + "AND Client = CM_Client.ID  AND Code  LIKE '%" + tbx_Condition.Text.Trim() + "%' )"
            + "OR CM_Client.FullName LIKE '%" + tbx_Condition.Text.Trim() + "%' "
            + "OR CM_Client.LinkManName LIKE '%" + tbx_Condition.Text.Trim() + "%'"
            + "OR CM_Client.TeleNum LIKE '%" + tbx_Condition.Text.Trim() + "%'"
            + "OR CM_Client.Mobile LIKE '%" + tbx_Condition.Text.Trim() + "%')";

        }

        if (tbx_begin.Text != "") condition += " AND CM_Client.InsertTime >='" + tbx_begin.Text + "'";
        if (tbx_end.Text != "") condition += " AND CM_Client.InsertTime <='" + tbx_end.Text + " 23:59:59'";

        gv_List.ConditionString = condition;

        gv_List.BindGrid();


    }

    #region 分页、排序、选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("RetailerDetail.aspx?ClientID=" + id.ToString());
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
        Response.Redirect("RetailerDetail.aspx?Mode=New");
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

    protected string setmap(int id)
    {
        CM_ClientBLL client = new CM_ClientBLL(id);
        CM_ClientGeoInfo info = CM_ClientGeoInfoBLL.GetGeoInfoByClient(id);
        if (info == null)
            return "showmap('',0,0)";
        else
            return string.Format("showmap(\"{0}\",{1},{2})", client.Model.FullName, info.Longitude, info.Latitude);
    }
}
