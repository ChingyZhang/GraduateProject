// ===================================================================
// 文件路径:SubModule/PBM/Delivery/SaleOut/OrderList.aspx.cs 
// 生成日期:2015-03-05 13:39:36 
// 作者:	  Shen Gang
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.BLL.CM;
using MCSFramework.Model.CM;
using MCSFramework.BLL.PBM;

public partial class SubModule_PBM_Order_OrderListNeedAssign : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            tbx_begin.Text = DateTime.Today.ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            
            if (DateTime.Now.Hour <= 12)
                tbx_PreArrivalDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            else
                tbx_PreArrivalDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            BindDropDown();
            BindGrid();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString());
        ddl_Salesman.DataBind();
        ddl_Salesman.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_DeliveryMan.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString());
        ddl_DeliveryMan.DataBind();
        ddl_DeliveryMan.Items.Insert(0, new ListItem("请选择送货人员...", "0"));
    }
    #endregion

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

    private void BindGrid()
    {
        string ConditionStr = " PBM_Order.Classify IN (1,2,4) ";

        ConditionStr += " AND PBM_Order.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '" + tbx_end.Text + " 23:59:59'";
        ConditionStr += " AND PBM_Order.Classify IN (1,4)";
        ConditionStr += " AND PBM_Order.State = 2";

        if ((int)Session["OwnerType"] == 3)
        {
            ConditionStr += " AND PBM_Order.Supplier = " + Session["OwnerClient"].ToString();
        }

        if (ddl_Salesman.SelectedValue != "0")
        {
            ConditionStr += " AND PBM_Order.Salesman = " + ddl_Salesman.SelectedValue;
        }

        gv_List.ConditionString = ConditionStr;
        gv_List.BindGrid();

    }


    protected void bt_Find_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void bt_Assign_Click(object sender, EventArgs e)
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

        int count = 0;
        foreach (GridViewRow row in gv_List.Rows)
        {
            int id = (int)gv_List.DataKeys[row.RowIndex]["PBM_Order_ID"];
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Checked)
            {
                PBM_OrderBLL _bll = new PBM_OrderBLL(id);
                if (_bll.Model.State == 2)
                {
                    int ret = _bll.CreateDelivery(supplierwarehouse, deliveryman, deliveryvehicle, prearrivaldate, (int)Session["UserID"]);
                    if (ret > 0)
                        count++;                        
                }
            }
        }

        MessageBox.Show(this, "成功派单" + count.ToString() + "条预售订单!");

        BindGrid();

    }
    protected void cbx_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_List.Rows)
        {
            CheckBox cbx = (CheckBox)row.FindControl("cbx");
            if (cbx != null && cbx.Visible)
            {
                cbx.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}