using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
public partial class SubModule_Product_PDT_ProductPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 2 : int.Parse(Request.QueryString["ClientType"]); //客户类型，２：经销商，３：终端门店
            #endregion

            BindDropDown();

            if (ViewState["ClientID"] != null)
            {
                CM_ClientBLL client = new CM_ClientBLL((int)ViewState["ClientID"]);

                if (Request.QueryString["ClientType"] != null && client.Model.ClientType != (int)ViewState["ClientType"])
                {
                    Session["ClientID"] = null;
                    Response.Redirect(Request.Url.PathAndQuery);
                }
                ViewState["ClientType"] = client.Model.ClientType;

                tr_OrganizeCity.SelectValue = client.Model.OrganizeCity.ToString();
                tr_OrganizeCity.Enabled = false;
                select_Client.SelectValue = ViewState["ClientID"].ToString();
                select_Client.SelectText = client.Model.FullName;
                select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
                select_Client_SelectChange(null, null);

                BindGrid();

            }
            else
            {
            
            }
            Header.Attributes["WebPageSubCode"] = "ClientType=" + ViewState["ClientType"].ToString();
            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString();
            //else
            //{
            //    if ((int)ViewState["ClientType"] == 2)
            //        MessageBox.ShowAndRedirect(this, "请先在‘经销商列表’中选择要查看的经销商！", "../CM/DI/DistributorList.aspx?URL=" + Request.Url.PathAndQuery);
            //    else if ((int)ViewState["ClientType"] == 3)
            //        MessageBox.ShowAndRedirect(this, "请先在‘零售商列表’中选择要查看的零售商！", "../CM/RT/RetailerList.aspx?URL=" + Request.Url.PathAndQuery);
            //    else
            //        MessageBox.ShowAndRedirect(this, "请先在‘仓库列表’中选择要查看的仓库！", "../CM/Store/StoreList.aspx?URL=" + Request.Url.PathAndQuery);
            //}
        }
    }

    private void BindDropDown()
    {
        #region 绑定用户可管辖的片区
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
        #endregion


    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=" + ViewState["ClientType"].ToString() + "&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Client.SelectText = "";
        select_Client.SelectValue = "";

    }
    protected void select_Client_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
        tr_OrganizeCity.SelectValue = new CM_ClientBLL(int.Parse(select_Client.SelectValue)).Model.OrganizeCity.ToString();
        Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
        ddl_StandardPrice.DataSource = PDT_StandardPriceBLL.GetModelList("ActiveFlag=1 AND ApproveFlag=1 AND ID IN (SELECT StandardPrice FROM PDT_StandardPrice_ApplyCity WHERE OrganizeCity IN (" + orgcity.GetAllSuperNodeIDs() + "," + tr_OrganizeCity.SelectValue + ")) ");
        ddl_StandardPrice.DataBind();
        ddl_StandardPrice.Items.Insert(0, new ListItem("请选择....", "0"));
        ddl_StandardPrice.SelectedValue = "0";
    }

    private void BindGrid()
    {
        string condition = "1=1";
        #region 组织查询条件
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND CM_Client.OrganizeCity IN (" + orgcitys + ")";
        }

        if (cbx_Valid.Checked)
        {
            condition += " AND getdate() between PDT_ProductPrice.BeginDate and PDT_ProductPrice.EndDate ";
        }

        if (select_Client.SelectValue != "")
        {
            condition += " AND PDT_ProductPrice.Client = " + select_Client.SelectValue;
        }
        else
        {
            condition += " AND CM_Client.ClientType=" + ViewState["ClientType"].ToString();
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(select_Client.SelectValue))
        {
            MessageBox.Show(this, "请先选择要添加价表的客户！");
        }
        else
        {
            if (ddl_StandardPrice.SelectedValue == "0")
            {
                MessageBox.Show(this, "请先选择标准价表！");
                return;
            }
            #region 判断当前是否已有有效的价表
            string condition = " PDT_ProductPrice.Client = " + select_Client.SelectValue + " AND GETDATE() BETWEEN PDT_ProductPrice.BeginDate AND PDT_ProductPrice.EndDate ";
            if (PDT_ProductPriceBLL.GetModelList(condition).Count > 0)
            {
                MessageBox.Show(this, "对不起，当前客户在当前有效期内已有一个价表，您可以在原价表基础上修改，而不要重复添加价表！");
                return;
            }
            #endregion
            Response.Redirect("PDT_ProductPriceDetail2.aspx?ClientID=" + select_Client.SelectValue + "&ClientType=" + ViewState["ClientType"].ToString() + "&StandardPrice=" + ddl_StandardPrice.SelectedValue);
        }

    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGrid();
    }


}
