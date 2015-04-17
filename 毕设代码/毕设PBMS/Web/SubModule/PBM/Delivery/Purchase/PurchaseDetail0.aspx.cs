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

public partial class SubModule_PBM_Delivery_Purchase_PurchaseDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["Classify"] == 12)
            {
                bt_OK.Text = "新增退库单";
            }
        }
    }

    private void BindDropDown()
    {
        ddl_Supplier.DataSource = CM_ClientBLL.GetSupplierByTDP((int)Session["OwnerClient"]).OrderBy(p => p.FullName);
        ddl_Supplier.DataBind();

        CM_Client _c = new CM_ClientBLL((int)Session["OwnerClient"]).Model;
        if (_c != null && ddl_Supplier.Items.FindByValue(_c.OwnerClient.ToString()) != null)
            ddl_Supplier.SelectedValue = _c.OwnerClient.ToString();

        ddl_ClientWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify == 1);
        ddl_ClientWareHouse.DataBind();

        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString() + " AND Org_Staff.Dimission=1");
        ddl_Salesman.DataBind();
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int suppler = 0, warehouse = 0, salesman = 0;
        int.TryParse(ddl_Supplier.SelectedValue, out suppler);
        int.TryParse(ddl_ClientWareHouse.SelectedValue, out warehouse);
        int.TryParse(ddl_Salesman.SelectedValue, out salesman);

        if (suppler == 0)
        {
            MessageBox.Show(this, "请正确选择供货商!");
            return;
        }
        if (warehouse == 0)
        {
            MessageBox.Show(this, "请正确选择采购到货仓库!");
            return;
        }

        PBM_DeliveryBLL bll = new PBM_DeliveryBLL();
        bll.Model.Supplier = suppler;
        bll.Model.Client = (int)Session["OwnerClient"];
        bll.Model.ClientWareHouse = warehouse;
        bll.Model.SalesMan = salesman;
        bll.Model.Classify = (int)ViewState["Classify"] == 0 ? 11 : (int)ViewState["Classify"];
        bll.Model.PrepareMode = 1;
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model.State = 1;
        bll.Model.ApproveFlag = 2;

        int id = bll.Add();
        Response.Redirect("PurchaseDetail.aspx?ID=" + id.ToString());

        //Response.Redirect("PurchaseDeail.aspx?Supplier=" + ddl_Supplier.SelectedValue +
        //    "&WareHouse=" + ddl_ClientWareHouse.SelectedValue + "&Salesman=" + ddl_Salesman.SelectedValue);
    }
}