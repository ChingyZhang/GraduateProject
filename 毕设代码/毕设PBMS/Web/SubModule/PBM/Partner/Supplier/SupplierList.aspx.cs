using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.Common;

public partial class SubModule_PBM_Partner_SuppilerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();

    }

    private void BindGrid()
    {
        string ConditionStr = " CM_Client.ClientType = 1 AND CM_Client.OwnerType=" + Session["OwnerType"].ToString()
            + " AND CM_Client.OwnerClient=" + Session["OwnerClient"].ToString();
        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }
    #region 选中等事件
    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int _id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];


        Response.Redirect("SupplierDetail.aspx?ID=" + _id.ToString());
    }
    #endregion
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierDetail.aspx");
    }
    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }
}