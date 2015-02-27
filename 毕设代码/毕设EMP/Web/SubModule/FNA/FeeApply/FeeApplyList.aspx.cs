using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.FNA;
using MCSFramework.Model.FNA;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using System.Data;
using MCSFramework.Model.Pub;

public partial class SubModule_FNA_FeeApply_FeeApplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)     
    {
        if (!IsPostBack)
        {
            ViewState["FeeType"] = Request.QueryString["FeeType"] == null ? 0 : int.Parse(Request.QueryString["FeeType"]);
            ViewState["AccountTitle"] = Request.QueryString["AccountTitle"] == null ? 1 : int.Parse(Request.QueryString["AccountTitle"]);
            BindDropDown();

            if ((int)ViewState["FeeType"] > 0) { ddl_FeeType.SelectedValue = ViewState["FeeType"].ToString(); ddl_FeeType.Enabled = false; }

            if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
            {
                //无查看营养教育费用权限
                ListItem item = ddl_FeeType.Items.FindByValue(ConfigHelper.GetConfigInt("CSOCostType").ToString());
                if (item != null) item.Enabled = false;
            }

            BindGrid();

            if ((int)ViewState["AccountTitle"] != 1)
            {
                if ((int)ViewState["AccountTitle"] != 73 && (int)ViewState["AccountTitle"] != 176) bt_Add_CL.Visible = false;
                if ((int)ViewState["AccountTitle"] != 82) bt_Add_FL.Visible = false;
                if ((int)ViewState["AccountTitle"] != 35) bt_Add_Gift.Visible = false;
                if ((int)ViewState["AccountTitle"] != 47) bt_Add_Car.Visible = false;
                //if ((int)ViewState["AccountTitle"] != 36) bt_Add_Promotor.Visible = false;
            }
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

        int forwarddays = ConfigHelper.GetConfigInt("FeeApplyForwardDays");
        ddl_Month.DataSource = AC_AccountMonthBLL.GetModelList("BeginDate<='" + DateTime.Today.AddDays(forwarddays).ToString("yyyy-MM-dd") +
            "' AND Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_Month.DataBind();
        ddl_Month.SelectedValue = AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(forwarddays)).ToString();

        ddl_FeeType.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeType").OrderBy(p => p.Value.Name);
        ddl_FeeType.DataBind();
        ddl_FeeType.Items.Insert(0, new ListItem("全部", "0"));

        ddl_State.DataSource = DictionaryBLL.GetDicCollections("FNA_FeeApplyState");
        ddl_State.DataBind();
        ddl_State.Items.Insert(0, new ListItem("全部", "0"));

        AC_AccountTitle title = new AC_AccountTitleBLL(int.Parse(ViewState["AccountTitle"].ToString())).Model;
        if (title != null)
        {
            DataTable dt = TreeTableBLL.GetAllChildNodeByNodes("MCS_Pub.dbo.AC_AccountTitle", "ID", "SuperID", title.ID.ToString());

            DataRow dr = dt.NewRow();
            dr["ID"] = title.ID;
            dr["SuperID"] = 1;
            dr["Name"] = title.Name;
            dt.Rows.Add(dr);

            tr_AccountTitle.DataSource = dt;
            tr_AccountTitle.SelectValue = title.ID.ToString();
            tr_AccountTitle.RootValue = "1";
        }
    }
    #endregion

    private void BindGrid()
    {
        string condition = "1=1";

        #region 组织查询条件
        //管理片区及所有下属管理片区
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            condition += " AND FNA_FeeApply.OrganizeCity IN (" + orgcitys + ")";
        }

        //会计月条件

        if ((int)ViewState["AccountTitle"] == 82)
        {
            condition += " AND EXISTS (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE FNA_FeeApplyDetail.AccountTitle=82 AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID AND BeginMonth=" + ddl_Month.SelectedValue + ")";
        }
        else
        {
            condition += " AND FNA_FeeApply.AccountMonth = " + ddl_Month.SelectedValue;
        }
        //申请单号
        if (tbx_SheetCode.Text != "")
        {
            condition += " AND FNA_FeeApply.SheetCode like '%" + tbx_SheetCode.Text + "%'";
        }

        if (!string.IsNullOrEmpty(Select_InsertStaff.SelectValue))
        {
            condition += " AND FNA_FeeApply.InsertStaff=" + Select_InsertStaff.SelectValue;
        }
        if (!string.IsNullOrEmpty(select_ApplyClient.SelectValue))
        {
            condition += " AND FNA_FeeApply.Client=" + select_ApplyClient.SelectValue;
        }

        //费用类型
        if (ddl_FeeType.SelectedValue != "0")
        {
            condition += " AND FNA_FeeApply.FeeType = " + ddl_FeeType.SelectedValue;
        }
        if (!Right_Assign_BLL.GetAccessRight((string)Session["UserName"], 4703, "Browse"))
        {
            //无查看营养教育费用权限
            condition += " AND FNA_FeeApply.FeeType <> " + ConfigHelper.GetConfigInt("CSOCostType").ToString();
        }

        //审批状态
        if (ddl_State.SelectedValue != "0")
        {
            condition += " AND FNA_FeeApply.State = " + ddl_State.SelectedValue;
        }

        //核销状态
        if (ddl_WriteOffState.SelectedValue == "1")
        {
            condition += " AND FNA_FeeApply.State=3 AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE AvailCost > 0 AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";
        }
        else if (ddl_WriteOffState.SelectedValue == "2")
        {
            condition += " AND FNA_FeeApply.State=3 AND FNA_FeeApply.ID NOT IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE AvailCost > 0 AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";
        }

        //会计科目
        int accounttile = 0;

        if (int.TryParse(tr_AccountTitle.SelectValue, out accounttile) && accounttile > 1)
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", accounttile.ToString());
            string ids = "";
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr["ID"].ToString() + ",";
            }
            ids += accounttile.ToString();

            condition += " AND FNA_FeeApply.ID IN (SELECT ApplyID FROM MCS_FNA.dbo.FNA_FeeApplyDetail WHERE AccountTitle IN(" + ids + ") AND FNA_FeeApplyDetail.ApplyID=FNA_FeeApply.ID)";
        }

        //标题
        if (tbx_Title.Text != "")
        {
            condition += " AND MCS_SYS.dbo.UF_Spilt(FNA_FeeApply.ExtPropertys,'|',4) LIKE '%" + tbx_Title.Text + "%'";
        }
        #endregion

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApplyDetail0.aspx");
    }

    protected void bt_Add_CL_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApply_CLContract.aspx?ISNKA=" + ((int)ViewState["AccountTitle"] == 176 ? "1" : "2"));
    }
    protected void bt_Add_FL_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApply_FLContract.aspx");
    }
    protected void bt_Add_Car_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApply_CarFeeApply.aspx");
    }
    protected void bt_Add_Gift_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApply_GiftFeeApply.aspx");
    }
    protected void bt_Add_Promotor_Click(object sender, EventArgs e)
    {
        Response.Redirect("FeeApply_PMFeeContract.aspx");
    }
}
