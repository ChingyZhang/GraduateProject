using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Model.EWF;
using MCSFramework.Common;

public partial class SubModule_EWF_ApproveAgencyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            tbx_begin.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            select_PrincipalStaff.SelectText = Session["UserRealName"].ToString();
            select_PrincipalStaff.SelectValue = Session["UserID"].ToString();

            BindDropDown();
            BindGrid();
        }
    }

    private void BindDropDown()
    {
        IList<EWF_Flow_App> apps = EWF_Flow_AppBLL.GetModelList(" EnableFlag='Y' ");
        ddl_App.DataSource = apps;
        ddl_App.DataBind();
        ddl_App.Items.Insert(0, new ListItem("请选择...", "0"));
    }

    private void BindGrid()
    {
        string condition = " EWF_ApproveAgency.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59' ";

        if (MCSTabControl1.SelectedIndex == 0)
            condition += " AND EWF_ApproveAgency.EnableFlag='Y' AND DATEADD(day,1,ISNULL(EWF_ApproveAgency.EndDate,GETDATE()))>GETDATE()";
        else
            condition += " AND (EWF_ApproveAgency.EnableFlag='N' OR DATEADD(day,1,EWF_ApproveAgency.EndDate)<GETDATE() )";

        if (ddl_App.SelectedValue != "0")
            condition += " AND EWF_ApproveAgency.App = '" + ddl_App.SelectedValue + "'";

        if (select_PrincipalStaff.SelectValue != "")
            condition += " AND EWF_ApproveAgency.PrincipalStaff=" + select_PrincipalStaff.SelectValue;

        if (select_AgentStaff.SelectValue != "")
            condition += " AND EWF_ApproveAgency.AgentStaff=" + select_AgentStaff.SelectValue;

        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void ddl_App_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }

    protected void btn_Search_Click(object sender, System.EventArgs e)
    {
        ViewState["PageIndex"] = 0;
        BindGrid();
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApproveAgencyDetail.aspx");
    }
    protected void gv_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = (int)gv_List.DataKeys[e.Row.RowIndex][0];
            EWF_ApproveAgency agency = new EWF_ApproveAgencyBLL(id).Model;

            if (agency != null)
            {
                if (agency.EnableFlag == "N" || agency.PrincipalStaff != (int)Session["UserID"]
                    || (agency.EndDate < DateTime.Today && agency.EndDate.Year != 1900))
                {
                    CheckBox cbx_Check = (CheckBox)e.Row.FindControl("cbx_Check");
                    if (cbx_Check != null)
                        cbx_Check.Visible = false;
                }
            }
        }
    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx_Check = (CheckBox)row.FindControl("cbx_Check");
            if (cbx_Check != null && cbx_Check.Visible) cbx_Check.Checked = cbx_CheckAll.Checked;
        }
    }
    protected void bt_Disable_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx_Check = (CheckBox)row.FindControl("cbx_Check");

            if (cbx_Check != null && cbx_Check.Visible && cbx_Check.Checked)
            {
                int id = (int)gv_List.DataKeys[row.RowIndex][0];
                EWF_ApproveAgencyBLL agencybll = new EWF_ApproveAgencyBLL(id);

                agencybll.Model.EnableFlag = "N";
                agencybll.Model.UpdateStaff = (int)Session["UserID"];
                agencybll.Update();
            }
        }

        BindGrid();
    }
}
