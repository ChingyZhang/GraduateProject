using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubModule_PBM_Order_Pop_Order_Assign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptInclude("WdatePicker", Page.ResolveClientUrl("~/js/My97DatePicker/WdatePicker.js"));
        if (!IsPostBack)
        {
            BindDropDown();

            if (DateTime.Now.Hour <= 12)
                tbx_PreArrivalDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            else
                tbx_PreArrivalDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            Session["AssignFlag"] = null;
            Session["DeliveryMan"] = null;
            Session["DeliveryVehicle"] = null;
            Session["SupplierWareHouse"] = null;
            Session["PreArrivalDate"] = null;
        }
    }

    private void BindDropDown()
    {
        ddl_DeliveryMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString());
        ddl_DeliveryMan.DataBind();
        ddl_DeliveryMan.Items.Insert(0, new ListItem("请选择送货人员...", "0"));
    }

    protected void ddl_DeliveryMan_SelectedIndexChanged(object sender, EventArgs e)
    {
        int staff = 0;
        int.TryParse(ddl_DeliveryMan.SelectedValue, out staff);

        if (staff != 0)
        {
            ddl_DeliveryVehicle.DataSource = CM_VehicleInStaffBLL.GetVehicleByStaff(staff);
            ddl_DeliveryVehicle.DataBind();
            ddl_DeliveryVehicle_SelectedIndexChanged(null, null);
        }
    }

    protected void ddl_DeliveryVehicle_SelectedIndexChanged(object sender, EventArgs e)
    {
        int vehicle = 0;
        int.TryParse(ddl_DeliveryVehicle.SelectedValue, out vehicle);

        if (vehicle != 0)
        {
            CM_VehicleBLL v = new CM_VehicleBLL(vehicle);
            CM_WareHouse w = v.GetRelateWareHouse();

            ddl_SupplierWareHouse.Items.Clear();
            if (w != null)
            {
                ddl_SupplierWareHouse.Items.Add(new ListItem(w.Name, w.ID.ToString()));
            }
        }
        else
        {
            ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify == 1);
            ddl_SupplierWareHouse.DataBind();
        }

    }
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int deliveryman = 0, deliveryvehicle = 0, supplierwarehouse = 0;
        DateTime prearrivaldate = new DateTime(1900, 1, 1);

        int.TryParse(ddl_DeliveryMan.SelectedValue, out deliveryman);
        int.TryParse(ddl_DeliveryVehicle.SelectedValue, out deliveryvehicle);
        int.TryParse(ddl_SupplierWareHouse.SelectedValue, out supplierwarehouse);
        DateTime.TryParse(tbx_PreArrivalDate.Text, out prearrivaldate);

        if (deliveryman == 0)
        {
            MessageBox.Show(this, "请选择订单配货时的发货员工!");
            return;
        }

        if (supplierwarehouse == 0)
        {
            MessageBox.Show(this, "请选择订单配货时的发货仓库!");
            return;
        }

        if (prearrivaldate < DateTime.Today)
        {
            MessageBox.Show(this, "要求送货日期不能小于今天!");
            return;
        }

        Session["AssignFlag"] = true;
        Session["DeliveryMan"] = deliveryman;
        Session["DeliveryVehicle"] = deliveryvehicle;
        Session["SupplierWareHouse"] = supplierwarehouse;
        Session["PreArrivalDate"] = prearrivaldate;

        Response.Clear();
        Response.Write("<script>window.close();</script>");
    }
}