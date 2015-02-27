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
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Common;


public partial class SubModule_CM_Search_SelectClient : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindDropDown();

            if (Request.QueryString["ClientType"] != null)
            {
                ListItem item = ddl_ClientType.Items.FindByValue(Request.QueryString["ClientType"]);
                if (item != null && item.Enabled)
                {
                    ddl_ClientType.SelectedValue = Request.QueryString["ClientType"];
                }
                else
                {
                    MessageBox.ShowAndClose(this, "您无权查看该类型商业客户!");
                    return;
                }
                ddl_ClientType.Enabled = false;
            }

            if (Request.QueryString["OrganizeCity"] != null && Request.QueryString["OrganizeCity"] != "0")
            {
                tr_OrganizeCity.SelectValue = Request.QueryString["OrganizeCity"];
                if (Request.QueryString["OrganizeCityEnabled"] == null) tr_OrganizeCity.Enabled = false;
            }

            #region ShowParent字段：不显示上级片区的经销商及跨片区的经销商
            if (Request.QueryString["ShowParent"] != null)
            {
                ViewState["ShowParent"] = "false";
            }
            #endregion

            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_ClientType.DataSource = DictionaryBLL.GetDicCollections("CM_ClientType");
        ddl_ClientType.DataBind();
        ddl_ClientType.Items.Insert(0, new ListItem("请选择...", "0"));

        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1701, "ViewStoreInPopSearch"))
        {
            //在弹出窗口中查看仓库列表
            //if (ddl_ClientType.Items.FindByValue("1") != null) ddl_ClientType.Items.FindByValue("1").Enabled = false;
        }

        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1201, "ViewDIInPopSearch"))
        {
            //在弹出窗口中查看经销商列表
            if (ddl_ClientType.Items.FindByValue("2") != null) ddl_ClientType.Items.FindByValue("2").Enabled = false;
        }

        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1101, "ViewRTInPopSearch"))
        {
            //在弹出窗口中查看门店列表
            if (ddl_ClientType.Items.FindByValue("3") != null) ddl_ClientType.Items.FindByValue("3").Enabled = false;
        }

        if (!Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 801, "ViewHPInPopSearch"))
        {
            //在弹出窗口中查看医院列表
            if (ddl_ClientType.Items.FindByValue("5") != null) ddl_ClientType.Items.FindByValue("5").Enabled = false;
        }


        #region 绑定用户可管辖的管理片区
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

            #region 获取当前员工的关联经销商
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                ViewState["RelateClient"] = _relateclient;
            }
            #endregion
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
        #endregion


    }
    #endregion

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ActiveFlag=1 AND CM_Client.ApproveFlag=1 ";

        if (ddl_ClientType.SelectedValue != "0")
        {
            ConditionStr += " AND CM_Client.ClientType = " + ddl_ClientType.SelectedValue;
        }
        else
        {
            ConditionStr += " AND CM_Client.ClientType IN ( 0";
            foreach (ListItem item in ddl_ClientType.Items)
            {
                if (item.Enabled) ConditionStr += "," + item.Value;
            }
            ConditionStr += " )";
        }

        if (tbx_Condition.Text.Trim() != "")
        {
            ConditionStr += " AND " + ddl_SearchType.SelectedValue + " LIKE '%" + this.tbx_Condition.Text.Trim() + "%'";
        }

        if (Request.QueryString["ExtCondition"] != null)
        {
            ConditionStr += " AND (" + Request.QueryString["ExtCondition"].Replace("\"", "").Replace('~', '\'').Replace("$", " ") + ")";
        }

        #region 判断当前可查询的范围
        string orgcitys = "";
        if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;



            if (ViewState["ShowParent"] == null)
            {
                #region 在选择仓库或经销商时，如果选择的是片区，则可以选择到其上级城市的经销商
                if ((ddl_ClientType.SelectedValue == "1" || ddl_ClientType.SelectedValue == "2") && Request.QueryString["NoParent"] == null)
                {
                    DataTable dt_fullpath = orgcity.GetFullPath();
                    if (dt_fullpath != null)
                    {
                        for (int i = 0; i < dt_fullpath.Rows.Count; i++)
                        {
                            orgcitys += "," + dt_fullpath.Rows[i]["ID"].ToString();
                        }
                    }
                }
                #endregion
                if (orgcitys != "")
                {
                    ConditionStr += " AND( CM_Client.OrganizeCity IN (" + orgcitys + ") OR CM_Client.ID IN (SELECT Client FROM MCS_CM.dbo.CM_ClientInOrganizeCity WHERE CM_ClientInOrganizeCity.OrganizeCity IN (" + orgcitys + ")) )";
                }
            }
            else if (ViewState["ShowParent"].ToString() == "false")
            {
                if (orgcitys != "")
                {
                    ConditionStr += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
                }
                ConditionStr += " AND MCS_SYS.dbo.UF_Spilt(CM_Client.ExtPropertys,'|',7)!='2' ";
            }

        }
        #endregion

        #region 判断当前员工的关联经销商
        if (ViewState["RelateClient"] != null)
        {
            if (ddl_ClientType.SelectedValue == "3")
                ConditionStr += " AND CM_Client.Supplier = " + ViewState["RelateClient"].ToString();
            else if (ddl_ClientType.SelectedValue == "2")
                ConditionStr += " AND ( CM_Client.ID =" + ViewState["RelateClient"].ToString() + " OR CM_Client.Supplier = " + ViewState["RelateClient"].ToString() + ")";
        }
        #endregion

        if ((int)Session["AccountType"] == 1)
        {
            if (ddl_ClientType.SelectedValue == "3")
            {
                //是否有权限“仅查看自己的门店”，如果是，限制客户经理条件
                if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 1101, "OlnyViewMyClient"))
                {
                    ConditionStr += " And CM_Client.ClientManager=" + Session["UserID"];
                }
            }
            else if (ddl_ClientType.SelectedValue == "5")
            {
                //是否有权限“仅查看自己负责的医院”，如果是，限制客户经理条件
                if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 801, "OnlyFindSelfHospital"))
                {
                    ConditionStr += " And MCS_CM.dbo.CM_Client.ClientManager=" + Session["UserID"];
                }
            }
        }
        #region 如果登录用户是商业客户，只能查看自己及下游客户
        if ((int)Session["AccountType"] == 2)
        {
            ConditionStr += " AND (CM_Client.ID = " + Session["UserID"].ToString() + " OR CM_Client.Supplier=" + Session["UserID"].ToString() + ")";
        }
        #endregion


        ConditionStr += " Order by MCS_CM.dbo.CM_Client.Code";

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

    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button bt_select = e.Row.FindControl("bt_select") != null ? (Button)e.Row.FindControl("bt_select") : null;
        if (bt_select != null)
        {
            bt_select.OnClientClick = "f_ReturnValue('" + gv_List.DataKeys[e.Row.RowIndex].Values[0].ToString() + "|" + gv_List.DataKeys[e.Row.RowIndex].Values[1].ToString() + "')";
        }
    }
}
