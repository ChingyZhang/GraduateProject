using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using System.Web.Security;


public partial class SubModule_StaffManage_StaffManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ViewState["Dimission"] = 1;
            ViewState["Position"] = 0;
            BindDropDown();


            BindGrid();
        }
    }

    private void BindDropDown()
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

        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
        if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")
        {
            //tr_Position.RootValue = staff.Model.Position.ToString();// p.SuperID.ToString();
            tr_Position.RootValue = p.SuperID.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        else
        {
            tr_Position.RootValue = "1";
            //tr_Position.SelectValue = "1";
        }
        tr_Position.SelectValue = tr_Position.RootValue;
        #endregion

        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("所有", "0"));
        ddl_ApproveFlag.SelectedValue = "1";

    }

    private void BindGrid()
    {
        string condition = " Org_Staff.Dimission=" + ViewState["Dimission"].ToString();

        #region 根据职位的范围查询
        if (tr_Position.SelectValue != tr_Position.RootValue && tr_Position.SelectValue != "0")
        {
            if (chb_ToPositionChild.Checked)
            {
                #region 绑定子职位
                Org_PositionBLL _bll = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                string ids = "";
                ids = _bll.GetAllChildPosition();
                #endregion

                if (ids != "")
                    condition += " and Org_Staff.Position in(" + tr_Position.SelectValue + "," + ids + ")";
                else
                    condition += " and Org_Staff.Position =" + tr_Position.SelectValue;
            }
            else
            {
                condition += " and Org_Staff.Position =" + tr_Position.SelectValue;
            }
        }
        #endregion

        #region 判断当前可查询的范围
        if (tr_OrganizeCity.SelectValue != "1")
        {
            Addr_OrganizeCityBLL orgcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
            string orgcitys = orgcity.GetAllChildNodeIDs();
            if (orgcitys != "") orgcitys += ",";
            orgcitys += tr_OrganizeCity.SelectValue;

            if (orgcitys != "") condition += " AND Org_Staff.OrganizeCity IN (" + orgcitys + ")";
        }
        #endregion

        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " And Org_Staff.ApproveFlag =" + ddl_ApproveFlag.SelectedValue;
        }


        ud_grid.ConditionString = condition;

        if (ViewState["ConditionString"] != null)
            ud_grid.ConditionString += " AND " + ViewState["ConditionString"].ToString();

        ud_grid.BindGrid();
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if (tbx_Search.Text != "")
            ViewState["ConditionString"] = " (Org_Staff.RealName like '%" + tbx_Search.Text + "%' or Org_Staff.StaffCode like '%" + tbx_Search.Text + "%') ";
        else
            ViewState["ConditionString"] = null;

        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void cmdOnPosition_Click(object sender, System.EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid.Rows)
        {
            if (((CheckBox)gr.FindControl("chkStaff_ID")).Checked == true)
            {
                new Org_StaffBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex][0].ToString())).DoRehab();
            }
        }
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void cmdOffPosition_Click(object sender, System.EventArgs e)
    {
        foreach (GridViewRow gr in ud_grid.Rows)
        {
            if (((CheckBox)gr.FindControl("chkStaff_ID")).Checked == true)
            {
                new Org_StaffBLL(int.Parse(ud_grid.DataKeys[gr.RowIndex][0].ToString())).DoDimission();
            }
        }
        ud_grid.PageIndex = 0;
        BindGrid();
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        switch (MCSTabControl1.SelectedTabItem.Value)
        {
            case "0":
                ViewState["Dimission"] = 1;
                cmdOffPosition.Visible = true;
                cmdOnPosition.Visible = false;
                break;
            case "1":
                ViewState["Dimission"] = 2;
                cmdOffPosition.Visible = false;
                cmdOnPosition.Visible = true;
                break;
        }
        ud_grid.PageIndex = 0;
        BindGrid();
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffDetail.aspx");
    }

    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
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
}
