using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.Logistics;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;

public partial class SubModule_Logistics_Order_Pop_OrderApplyDetailAdjustHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

            if ((int)ViewState["ID"] == 0)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }

            BindData();
        }
    }

    private void BindData()
    {
        ORD_GiftApplyAmountBLL amount = new ORD_GiftApplyAmountBLL((int)ViewState["ID"]);
        gv_List.DataSource = amount.GetChangeHistory();
        gv_List.BindGrid();
    }
  
}
