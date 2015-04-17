using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.PBM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Delivery_SaleOut_SaleOutDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["Classify"] == 2)
            {
                bt_OK.Text = "新建退货单";
            }
        }
    }

    private void BindDropDown()
    {
        ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify == 1);
        ddl_SupplierWareHouse.DataBind();

        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Dimission=1");
        ddl_Salesman.DataBind();
        ddl_Salesman.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int client = 0, warehouse = 0, salesman = 0;
        int.TryParse(select_Client.SelectValue, out client);
        int.TryParse(ddl_SupplierWareHouse.SelectedValue, out warehouse);
        int.TryParse(ddl_Salesman.SelectedValue, out salesman);

        if (client == 0)
        {
            MessageBox.Show(this, "请正确选择零售店!");
            return;
        }
        if (warehouse == 0)
        {
            MessageBox.Show(this, "请正确选择供货出库仓库!");
            return;
        }

        PBM_DeliveryBLL bll = new PBM_DeliveryBLL();
        bll.Model.Supplier = (int)Session["OwnerClient"];
        bll.Model.SupplierWareHouse = warehouse;
        bll.Model.Client = client;
        bll.Model.SalesMan = salesman;
        bll.Model.Classify = (int)ViewState["Classify"] == 0 ? 1 : (int)ViewState["Classify"];
        bll.Model.PrepareMode = 1;
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model.State = 1;
        bll.Model.ApproveFlag = 2;

        int id = bll.Add();
        Response.Redirect("SaleOutDetail.aspx?ID=" + id.ToString());
    }
}