// ===================================================================
// 文件路径:CM/LinkMan/LinkManList.aspx.cs 
// 生成日期:2008-12-19 10:05:59 
// 作者:	  yangwei
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
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;

public partial class CM_LinkMan_LinkManList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsPostBack)
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

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);
                select_Retailer.SelectValue = ViewState["ClientID"].ToString();
                select_Retailer.SelectText = client.Model.FullName;
                BindGrid();
                bt_Add.Enabled = true;
            }
            else
                bt_Add.Enabled = false;
        }
    }

    private void BindGrid()
    {
        if (select_Retailer.SelectValue != "")
        {
            string ConditionStr = "MCS_CM.dbo.CM_LinkMan.ClientID = " + select_Retailer.SelectValue;
            gv_List.ConditionString = ConditionStr;
            gv_List.BindGrid();

            ViewState["ClientID"] = int.Parse(select_Retailer.SelectValue);
            bt_Add.Enabled = true;
        }
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("LinkManDetail.aspx?ClientID=" + ViewState["ClientID"].ToString());
    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Response.Redirect("LinkManDetail.aspx?ID=" + gv_List.DataKeys[e.NewSelectedIndex][0].ToString());
    }
    
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}