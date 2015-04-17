using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSControls.MCSWebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.EWF;
using MCSFramework.Common;
using MCSFramework.Model;

public partial class SubModule_StaffManage_StaffDetail_Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string title = ConfigHelper.GetConfigString("PageTitle");
            if (!String.IsNullOrEmpty(title))
            {
                lb_Header.Text = title;
            }
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
        }
    }
    private void BindData()
    {
        Org_StaffBLL bll = new Org_StaffBLL((int)ViewState["ID"]);
        panel1.BindData(bll.Model);
         if (bll.Model.OrganizeCity > 1)
         {
             gv_StaffInOrganizeCity.BindGrid<Addr_OrganizeCity>(bll.StaffInOrganizeCity_GetOrganizeCitys());
         }
         else
         {
             tr_StaffInOrganizeCity.Visible = false;
         }
        gv_List.DataSource = bll.GetUserList();
        gv_List.DataBind();
    }
}
