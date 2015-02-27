using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
public partial class SubModule_Logistics_ORD_OrderApplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Type"] = Request.QueryString["Type"] == null ? 0 : int.Parse(Request.QueryString["Type"]);
            ViewState["Publish"] = Request.QueryString["Publish"] == null ? 0 : int.Parse(Request.QueryString["Publish"]);

            Header.Attributes["WebPageSubCode"] = "Type=" + ViewState["Type"].ToString();
            BindDropDown();
            BindGrid();
        }
    }

    #region 绑定下拉列表框
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
        }
        #endregion

        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now).ToString();

        ddl_Type.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderType");
        ddl_Type.DataBind();
        ddl_Type.Items.Insert(0, new ListItem("全部", "0"));

        if ((int)ViewState["Type"] > 0)
        {
            ddl_Type.SelectedValue = ViewState["Type"].ToString();
            ddl_Type.Enabled = false;
        }
        ddl_State.DataSource = DictionaryBLL.GetDicCollections("ORD_OrderApplyState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

        IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("(IsOpponent IN (1) OR ID IN (4,5))");
        ddl_Brand.DataTextField = "Name";
        ddl_Brand.DataValueField = "ID";
        ddl_Brand.DataSource = _brandList;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("全部", "0"));

        ddl_ProductType.DataSource = DictionaryBLL.GetDicCollections("ORD_ProductOrderType");
        ddl_ProductType.DataBind();
        ddl_ProductType.Items.Insert(0, new ListItem("全部", "0"));
        if ((int)ViewState["Type"] == 2)
        {
            ddl_ProductType.Enabled = false;

        }

    }
    #endregion

    private void BindGrid()
    {
        string condition = " 1=1 ";

        #region 组织查询条件
        if ((int)ViewState["Publish"] != 0)
        {
            tbl_Condition.Visible = false;
            tbl_Condition_title.Visible = false;
            condition = " ORD_OrderApply.PublishID = " + ViewState["Publish"].ToString();
        }
        else
        {
            if (tr_OrganizeCity.SelectValue != "1")
            {
                //管理片区及所有下属管理片区
                Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string orgcitys = orgcity.GetAllChildNodeIDs();
                if (orgcitys != "") orgcitys += ",";
                orgcitys += tr_OrganizeCity.SelectValue;

                condition += " AND ORD_OrderApply.OrganizeCity IN (" + orgcitys + ")";
            }

            //会计月条件
            condition += " AND ORD_OrderApply.AccountMonth = " + ddl_Month.SelectedValue;

            //申请单号
            if (tbx_SheetCode.Text != "")
            {
                condition += " AND ORD_OrderApply.SheetCode like '%" + tbx_SheetCode.Text + "%'";
            }

            if (ddl_ProductType.SelectedValue != "0")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt(ORD_OrderApply.ExtPropertys,'|',4)=" + ddl_ProductType.SelectedValue;
            }

            if (ddl_Brand.SelectedValue != "0")
            {
                condition += " AND MCS_SYS.dbo.UF_Spilt(ORD_OrderApply.ExtPropertys,'|',5)=" + ddl_Brand.SelectedValue;
            }
            //定单类型
            if (ddl_Type.SelectedValue != "0")
            {
                condition += " AND ORD_OrderApply.Type = " + ddl_Type.SelectedValue;
            }

            //审批状态
            if (ddl_State.SelectedValue != "0")
            {
                condition += " AND ORD_OrderApply.State = " + ddl_State.SelectedValue;
            }
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();

    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["Type"] == 1)
        {
            Response.Redirect("OrderApplyDetail01.aspx?Type=" + ddl_Type.SelectedValue);
        }
        else
        {
            Response.Redirect("OrderApplyDetail0.aspx?Type=" + ddl_Type.SelectedValue);
        }
    }
    protected string GetUrl(int applyid)
    {
        string url = "";
        if ((int)ViewState["Type"] == 1 || ddl_Type.SelectedValue == "1")
        {
            url = "OrderProductApplyDetail.aspx?ID=" + applyid.ToString();
        }
        else
        {
            url = "OrderApplyDetail3.aspx?ID=" + applyid.ToString();
        }
        return url;
    }
}
