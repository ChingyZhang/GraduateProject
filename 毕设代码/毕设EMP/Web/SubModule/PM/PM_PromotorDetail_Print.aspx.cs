using System;
using System.Web.UI;
using MCSFramework.Common;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;

public partial class SubModule_PM_PM_PromotorDetail_Print : System.Web.UI.Page
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

            ViewState["PromotorID"] = Request.QueryString["PromotorID"] == null ? 0 : int.Parse(Request.QueryString["PromotorID"]);
            if ((int)ViewState["PromotorID"] > 0)
            {
                BindData();
            }
        }
    }
    private void BindData()
    {

        PM_Promotor m = new PM_PromotorBLL((int)ViewState["PromotorID"]).Model;
        UC_DetailView1.BindData(m);
        gv_list.ConditionString = " Promotor= " + (int)ViewState["PromotorID"];
        gv_list.BindGrid();
    }
}
