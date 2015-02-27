using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;

public partial class SubModule_AddressManage_OrganizeCityParam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        ddl_ParamType.DataSource = DictionaryBLL.GetDicCollections("Pub_OrganizeCityParam");
        ddl_ParamType.DataBind();
        ddl_ParamType.Items.Insert(0, new ListItem("请选择", "0"));
        ddl_ParamType.SelectedValue = "0";
        ddl_ParamType2.DataSource = DictionaryBLL.GetDicCollections("Pub_OrganizeCityParam");
        ddl_ParamType2.DataBind();
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {}
    #endregion


    private void BindGrid()
    {
        int City = int.Parse(tr_OrganizeCity.SelectValue);
        int paramType = int.Parse(ddl_ParamType.SelectedValue);
        int check = cb_include.Checked ? 1 : 0;
        div_detail.Visible = false;
        gv_List.DataSource = Addr_OrganizeCityParamBLL.GetByOrganizeCity(City, paramType, check);
        gv_List.DataBind();
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int detail = int.Parse(gv_List.DataKeys[e.RowIndex]["ID"].ToString());
        new Addr_OrganizeCityParamBLL().Delete(detail);
        BindGrid();
    }
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex >= 0)
        {
            int detail = int.Parse(gv_List.DataKeys[e.NewSelectedIndex]["ID"].ToString());
            bindDetail(detail);
        }
    }
    private void bindDetail(int detailID)
    {
        div_detail.Visible = true;
        if (detailID > 0)
        {
            Addr_OrganizeCityParamBLL param = new Addr_OrganizeCityParamBLL(detailID);
            ddl_ParamType2.SelectedValue = param.Model.ParamType.ToString();
            tbx_ParamValue.Text = param.Model.ParamValue;
            tbx_Remark.Text = param.Model.Remark;
        }
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        if (gv_List.SelectedIndex >= 0)
        {
            int detail = int.Parse(gv_List.DataKeys[gv_List.SelectedIndex]["ID"].ToString());
            Addr_OrganizeCityParamBLL param = new Addr_OrganizeCityParamBLL(detail);
            param.Model.ParamType = int.Parse(ddl_ParamType2.SelectedValue);
            param.Model.ParamValue = tbx_ParamValue.Text;
            param.Model.Remark = tbx_Remark.Text;
            param.Model.UpdateStaff = int.Parse(Session["UserID"].ToString());
            tr_OrganizeCity.SelectValue = param.Model.OrganizeCity.ToString();
            if (Addr_OrganizeCityParamBLL.GetModelList("OrganizeCity="
                + param.Model.OrganizeCity
                + " AND ParamType="
                + int.Parse(ddl_ParamType2.SelectedValue)
                + " AND ID <> " + detail).Count > 0)
            {
                MessageBox.Show(this, "当前片区已存在此标准!");
                return;
            }
            if (param.Update() < 0)
                MessageBox.Show(this, "保存失败！");

        }
        else
        {
            Addr_OrganizeCityParamBLL param = new Addr_OrganizeCityParamBLL();
            param.Model.ParamType = int.Parse(ddl_ParamType2.SelectedValue);
            param.Model.ParamValue = tbx_ParamValue.Text;
            param.Model.Remark = tbx_Remark.Text;
            param.Model.OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
            param.Model.InsertStaff = int.Parse(Session["UserID"].ToString());
            if (Addr_OrganizeCityParamBLL.GetModelList("OrganizeCity="
                + param.Model.OrganizeCity
                + " AND ParamType="
                + int.Parse(ddl_ParamType2.SelectedValue)).Count > 0)
            {
                MessageBox.Show(this, "当前片区已存在此标准!");
                return;
            }
            if (param.Add() < 0)
                MessageBox.Show(this, "保存失败！");

        }
        ddl_ParamType.SelectedValue = "0";
        cb_include.Checked = false;
        BindGrid();
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        bindDetail(0);
    }

}
