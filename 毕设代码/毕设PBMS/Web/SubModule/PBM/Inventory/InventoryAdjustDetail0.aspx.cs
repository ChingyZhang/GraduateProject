using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Inventory_InventoryAdjustDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 0;
            #endregion

            BindDropDown();
        }
    }

    private void BindDropDown()
    {
        ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_SupplierWareHouse.DataBind();
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int warehouse = 0;
        int.TryParse(ddl_SupplierWareHouse.SelectedValue, out warehouse);

        if (warehouse == 0)
        {
            MessageBox.Show(this, "请正确选择供货出库仓库!");
            return;
        }

        int id = PBM_DeliveryBLL.AdjustInit(warehouse, (int)Session["UserID"]);

        if (id < 0)
        {
            MessageBox.Show(this, "创建盘点单失败!RET=" + id.ToString());
            return;
        }

        Response.Redirect("InventoryAdjustDetail.aspx?ID=" + id.ToString());
    }
}