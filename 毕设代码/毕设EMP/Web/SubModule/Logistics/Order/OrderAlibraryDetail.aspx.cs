using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data.SqlClient;
using System.Data;
using MCSFramework.DBUtility;

public partial class SubModule_Logistics_Order_OrderAlibraryDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["OrderNo"] = Request.QueryString["OrderNo"].ToString();
            BindData();

        }
    }

    private void BindData()
    {
        string strID = ViewState["OrderNo"].ToString();
        ORD_OrderDeliveryBLL ordBll = new ORD_OrderDeliveryBLL();
        DataTable dt = ordBll.GetOrderAlibrayDetailList(strID);
        gv_OrderList.DataSource = dt;
        gv_OrderList.DataBind();

    }
   
}


