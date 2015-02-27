using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

public partial class SubModule_Logistics_Order_OrderApplyOrderTimes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 绑定管理片区控件
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
            select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
            select_Client2.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
            #endregion
            BindGrid();
        }
    }
    private void BindGrid()
    {
        int OrganizeCity = 0, Client = 0;
        int.TryParse(tr_OrganizeCity.SelectValue, out OrganizeCity);
        int.TryParse(select_Client.SelectValue, out Client);

        gv_List.DataSource = ORD_OrderApply_ClientTimesBLL.GetByOrganizeCity(OrganizeCity, Client);
        gv_List.BindGrid();

        tr_Detail.Visible = false;
    }
    protected void gv_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gv_List.SelectedIndex > -1)
        {
            ORD_OrderApply_ClientTimesBLL bll = new ORD_OrderApply_ClientTimesBLL((int)gv_List.DataKeys[gv_List.SelectedIndex].Value);
            select_Client2.SelectValue = bll.Model.Client.ToString();
            select_Client2.SelectText = new CM_ClientBLL(bll.Model.Client).Model.FullName;
            tbx_Times.Text = bll.Model.OrderTimes.ToString();
            tr_Detail.Visible = true;
        }
        else
            tr_Detail.Visible = false;
        
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        gv_List.SelectedIndex = -1;
        tr_Detail.Visible = true;
        select_Client2.SelectValue = "0";
        select_Client2.SelectText = "";
        tbx_Times.Text = "";
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_Save_Click(object sender, EventArgs e)
    {
        select_Client.SelectValue = "0";
        select_Client.SelectText = "";
        ORD_OrderApply_ClientTimesBLL bll;
        if (gv_List.SelectedIndex > -1)
            bll = new ORD_OrderApply_ClientTimesBLL((int)gv_List.DataKeys[gv_List.SelectedIndex].Value);
        else
            bll = new ORD_OrderApply_ClientTimesBLL();
        int Client=0,Times=1;
        int.TryParse(select_Client2.SelectValue,out Client);
        int.TryParse(tbx_Times.Text,out Times);
        bll.Model.Client = Client;
        bll.Model.OrderTimes = Times;
        if (bll.Model.ID > 0)
            bll.Update();
        else
            bll.Add();
        MessageBox.Show(this, "保存成功！");
        BindGrid();
    }
    protected void tr_OrganizeCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        select_Client.SelectValue = "0";
        select_Client.SelectText = "";
        select_Client.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
        select_Client2.PageUrl = "~/SubModule/CM/PopSearch/Search_SelectClient.aspx?ClientType=2&OrganizeCity=" + tr_OrganizeCity.SelectValue;
    }
    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        new ORD_OrderApply_ClientTimesBLL().Delete(int.Parse(gv_List.DataKeys[e.RowIndex].Value.ToString()));
        BindGrid();
    }
}
