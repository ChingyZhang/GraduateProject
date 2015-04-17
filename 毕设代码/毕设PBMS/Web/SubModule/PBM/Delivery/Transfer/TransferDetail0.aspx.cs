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

public partial class SubModule_PBM_Delivery_Transfer_TransferDetail0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 获取页面参数
            ViewState["Classify"] = Request.QueryString["Classify"] != null ? int.Parse(Request.QueryString["Classify"]) : 1;
            #endregion

            BindDropDown();

            if ((int)ViewState["Classify"] != 0)
            {
                ddl_Classify.SelectedValue = ViewState["Classify"].ToString();
                ddl_Classify.Enabled = false;
            }
            ddl_Classify_SelectedIndexChanged(null, null);
        }
    }

    private void BindDropDown()
    {
        ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_SupplierWareHouse.DataBind();
        ddl_SupplierWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_ClientWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]);
        ddl_ClientWareHouse.DataBind();
        ddl_ClientWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));

        ddl_Salesman.DataSource = Org_StaffBLL.GetStaffList("Org_Staff.OwnerClient=" + Session["OwnerClient"].ToString());
        ddl_Salesman.DataBind();
        ddl_Salesman.SelectedValue = Session["UserID"].ToString();
    }

    protected void ddl_Classify_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddl_Classify.SelectedValue)
        {
            case "3":   //仓库间调拨
                ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify != 3);
                ddl_SupplierWareHouse.DataBind();
                ddl_SupplierWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));

                ddl_ClientWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify != 3);
                ddl_ClientWareHouse.DataBind();
                ddl_ClientWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));
                break;
            case "5":   //车销移库单
                ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify != 3);
                ddl_SupplierWareHouse.DataBind();
                ddl_SupplierWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));

                ddl_ClientWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify == 3);
                ddl_ClientWareHouse.DataBind();
                ddl_ClientWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));
                break;
            case "6":   //车销退库单
                ddl_SupplierWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify == 3);
                ddl_SupplierWareHouse.DataBind();
                ddl_SupplierWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));

                ddl_ClientWareHouse.DataSource = CM_WareHouseBLL.GetByClient((int)Session["OwnerClient"]).Where(p => p.Classify != 3);
                ddl_ClientWareHouse.DataBind();
                ddl_ClientWareHouse.Items.Insert(0, new ListItem("请选择...", "0"));
                break;
            default:
                break;
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        int supplierwarehouse = 0, clientwarehouse = 0, salesman = 0;
        int.TryParse(ddl_SupplierWareHouse.SelectedValue, out supplierwarehouse);
        int.TryParse(ddl_ClientWareHouse.SelectedValue, out clientwarehouse);
        int.TryParse(ddl_Salesman.SelectedValue, out salesman);

        if (supplierwarehouse == 0)
        {
            MessageBox.Show(this, "请正确选择调出仓库!");
            return;
        }
        if (clientwarehouse == 0)
        {
            MessageBox.Show(this, "请正确选择调入仓库!");
            return;
        }

        PBM_DeliveryBLL bll = new PBM_DeliveryBLL();
        bll.Model.Supplier = (int)Session["OwnerClient"];
        bll.Model.SupplierWareHouse = supplierwarehouse;
        bll.Model.Client = (int)Session["OwnerClient"]; ;
        bll.Model.ClientWareHouse = clientwarehouse;
        bll.Model.Classify = int.Parse(ddl_Classify.SelectedValue);
        bll.Model.SalesMan = salesman;
        bll.Model.PrepareMode = 1;
        bll.Model.InsertStaff = (int)Session["UserID"];
        bll.Model.State = 1;
        bll.Model.ApproveFlag = 2;

        int id = bll.Add();
        Response.Redirect("TransferDetail.aspx?ID=" + id.ToString());

        //Response.Redirect("TransferDeail.aspx?Supplier=" + ddl_Supplier.SelectedValue +
        //    "&WareHouse=" + ddl_ClientWareHouse.SelectedValue + "&Salesman=" + ddl_Salesman.SelectedValue);
    }

}