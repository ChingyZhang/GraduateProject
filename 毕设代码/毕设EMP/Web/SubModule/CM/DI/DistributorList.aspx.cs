// ===================================================================
// 文件路径:CM/Distributor/DistributorList.aspx.cs 
// 生成日期:2008-12-19 10:11:21 
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
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
public partial class CM_Distributor_DistributorList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

        if (!Page.IsPostBack)
        {
            BindDropDown();
            Session["ClientID"] = null;
            //Session["MCSMenuControl_FirstSelectIndex"] = "12";
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        if ((int)Session["AccountType"] == 1)
        {
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
            }
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;
            if (client != null)
            {
                Addr_OrganizeCityBLL citybll = new Addr_OrganizeCityBLL(client.OrganizeCity);
                tr_OrganizeCity.DataSource = citybll.GetAllChildNodeIncludeSelf();
                tr_OrganizeCity.RootValue = citybll.Model.SuperID.ToString();
                tr_OrganizeCity.SelectValue = client.OrganizeCity.ToString();
            }
        }

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "1";

        ddl_ActiveFlag.DataSource = DictionaryBLL.GetDicCollections("CM_ActiveFlag");
        ddl_ActiveFlag.DataBind();
        ddl_ActiveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ActiveFlag.SelectedValue = "1";


        ddl_ClientClassify.DataSource = DictionaryBLL.GetDicCollections("CM_DI_Classify");
        ddl_ClientClassify.DataBind();
        ddl_ClientClassify.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ClientClassify.SelectedValue = "1";
    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ClientType = 2 ";
        if (tbx_Condition.Text.Trim() != "")
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";

        string orgcitys = "";

        #region 判断当前可查询的范围

        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") ConditionStr += " AND (CM_Client.OrganizeCity IN (" + orgcitys +
                ") OR CM_Client.ClientManager = " + Session["UserID"].ToString() + ")";
        }
        #endregion

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            ConditionStr += " And MCS_CM.dbo.CM_Client.ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }

        if (ddl_ActiveFlag.SelectedValue != "0")
        {
            ConditionStr += " And MCS_CM.dbo.CM_Client.ActiveFlag =" + ddl_ActiveFlag.SelectedValue;
        }

        if (ddl_ClientClassify.SelectedValue != "0")
        {
            ConditionStr += " And (MCS_SYS.dbo.UF_Spilt(MCS_CM.dbo.CM_Client.ExtPropertys,'|',7) = '" + ddl_ClientClassify.SelectedValue + "')";
        }
        if ((int)Session["AccountType"] == 1)
        {
            #region 获取当前员工的关联经销商
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);

            if (!string.IsNullOrEmpty(staff.Model["RelateClient"]))
            {
                ConditionStr += " And CM_Client.Supplier=" + staff.Model["RelateClient"] + " OR CM_Client.ID=" + staff.Model["RelateClient"];
            }
            #endregion
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;

            if (client.ClientType == 3)
            {
                ConditionStr += " And CM_Client.ID = " + Session["UserID"].ToString();
            }
            else if (client.ClientType == 2)
            {
                ConditionStr += " And (CM_Client.Supplier = " + Session["UserID"].ToString() + " OR CM_Client.ID =" + Session["UserID"].ToString() + "  )";
            }


        }
        if (ViewState["PageIndex"] != null)
        {
            gv_List.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        ConditionStr += " Order by CM_Client.Code";
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();
        //Response.Write(ConditionStr);
    }


    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (e.item.Value)
        {
            case "0"://快捷查询

                break;
            //case "1"://高级查询
            //    Response.Redirect("AdvanceFind.aspx");
            //    break;
        }
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Session["ClientID"] = null;
        Response.Redirect("DistributorDetail.aspx?Mode=New");
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //Response.Redirect("DistributorDetail.aspx?ClientID=" + gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Session["ClientID"] = id;
        if (Request.QueryString["URL"] != null) Response.Redirect(Request.QueryString["URL"]);
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int clientid = (int)gv_List.DataKeys[e.Row.RowIndex]["CM_Client_ID"];

            Label lb_Supplier = (Label)e.Row.FindControl("lb_Supplier");
            if (lb_Supplier != null)
            {
                CM_Client s = new CM_ClientBLL(new CM_ClientBLL(clientid).Model.Supplier).Model;
                if (s != null)
                {
                    if (s.Code != "") lb_Supplier.Text = string.Format("({0}) ", s.Code);
                    lb_Supplier.Text += string.Format("{0}", s.FullName);
                }
            }
        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {

    }
    protected void ddl_SearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tbx_Condition.Text = "";
    }


}
